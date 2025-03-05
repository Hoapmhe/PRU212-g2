using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script;

public class Collisions : MonoBehaviour
{
    [SerializeField] private int maxPackages = 3;
    [SerializeField] private float pickupTime = 1f;
    [SerializeField] private float deliveryTime = 3f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultCar;
    [SerializeField] private Sprite greenCar;
    [SerializeField] private Sprite blueCar;
    [SerializeField] private Sprite yellowCar;

    private int packageCount = 0;
    private DriverController driverController;
    private Coroutine pickupCoroutine;
    private Coroutine deliveryCoroutine;
    private TextDisplay textDisplay;
    private Rigidbody2D rb;

    private Stack<CarColor> carColorStack = new Stack<CarColor>(); // Lưu lịch sử màu
    private CarColor currentCarColor = CarColor.Default;

    private void Start()
    {
        driverController = GetComponent<DriverController>();
        textDisplay = FindFirstObjectByType<TextDisplay>();
        rb = GetComponent<Rigidbody2D>();

        if (textDisplay == null)
        {
            Debug.LogError("Không tìm thấy ReceiveDisplay trong Scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && packageCount < maxPackages)
        {
            if (pickupCoroutine == null)
            {
                pickupCoroutine = StartCoroutine(PickupPackage(other.gameObject));
            }
        }

        if (other.CompareTag("Location") && packageCount > 0)
        {
            if (deliveryCoroutine == null)
            {
                deliveryCoroutine = StartCoroutine(DeliverPackages(other.gameObject));
            }
        }
    }

    private IEnumerator PickupPackage(GameObject package)
    {
        Debug.Log("Bắt đầu nhặt quà... ⏳ (1s)");
        yield return new WaitForSeconds(pickupTime);

        packageCount++;
        textDisplay.CountBoxOnCar(packageCount);
        driverController.DecreaseSpeed();
        Destroy(package);

        // Lưu màu cũ vào Stack
        carColorStack.Push(currentCarColor);

        // Random màu mới cho xe (trừ Default)
        currentCarColor = (CarColor)Random.Range(1, 4);
        UpdateCarColor(currentCarColor);

        pickupCoroutine = null;
    }

    private IEnumerator DeliverPackages(GameObject location)
    {
        Location locComponent = location.GetComponent<Location>();
        if (locComponent == null)
        {
            Debug.LogError("Location không có component Location!");
            yield break;
        }

        CarColor locationColor = locComponent.locationColor;
        if (locationColor != currentCarColor)
        {
            Debug.Log("Sai địa điểm giao hàng! ❌ Đi tìm đúng địa điểm.");
            yield break;
        }

        Debug.Log("Bắt đầu giao hàng... 📦 (3s)");
        yield return new WaitForSeconds(deliveryTime);

        Debug.Log("Giao hàng thành công! ✅");
        textDisplay.IncrementReceivedCount(1);
        packageCount--;
        textDisplay.CountBoxOnCar(packageCount);
        driverController.ResetSpeed();

        // Lấy lại màu trước đó
        if (carColorStack.Count > 0)
        {
            currentCarColor = carColorStack.Pop();
        }
        else
        {
            currentCarColor = CarColor.Default;
        }
        UpdateCarColor(currentCarColor);

        deliveryCoroutine = null;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Package") && pickupCoroutine != null)
        {
            StopCoroutine(pickupCoroutine);
            pickupCoroutine = null;
        }
        if (other.CompareTag("Location") && deliveryCoroutine != null)
        {
            Debug.Log("Bỏ lỡ giao hàng vì đi quá nhanh! ❌");
            StopCoroutine(deliveryCoroutine);
            deliveryCoroutine = null;
        }
    }

    private void UpdateCarColor(CarColor color)
    {
        switch (color)
        {
            case CarColor.Green:
                spriteRenderer.sprite = greenCar;
                break;
            case CarColor.Blue:
                spriteRenderer.sprite = blueCar;
                break;
            case CarColor.Yellow:
                spriteRenderer.sprite = yellowCar;
                break;
            default:
                spriteRenderer.sprite = defaultCar;
                break;
        }
    }
}
