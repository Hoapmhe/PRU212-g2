using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collisions : MonoBehaviour
{
    [SerializeField] private int maxPackages = 3;
    [SerializeField] private float pickupTime = 1f;
    [SerializeField] private float deliveryTime = 3f;

<<<<<<< Updated upstream
=======
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultCar;
    [SerializeField] private Sprite greenCar;
    [SerializeField] private Sprite blueCar;
    [SerializeField] private Sprite yellowCar;

>>>>>>> Stashed changes
    private int packageCount = 0;
    private DriverController driverController;
    private Coroutine pickupCoroutine;
    private Coroutine deliveryCoroutine;
<<<<<<< Updated upstream

    private void Start()
    {
        driverController = GetComponent<DriverController>(); // Lấy script DriverController từ xe
=======
    private ReceiveDisplay receiveDisplay;
    private Rigidbody2D rb;

    private Stack<CarColor> carColorStack = new Stack<CarColor>(); // Lưu lịch sử màu
    private CarColor currentCarColor = CarColor.Default;

    private void Start()
    {
        driverController = GetComponent<DriverController>();
        receiveDisplay = FindFirstObjectByType<ReceiveDisplay>();
        rb = GetComponent<Rigidbody2D>();

        if (receiveDisplay == null)
        {
            Debug.LogError("Không tìm thấy ReceiveDisplay trong Scene!");
        }
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        yield return new WaitForSeconds(pickupTime); // Chờ 1 giây
=======
        yield return new WaitForSeconds(pickupTime);
>>>>>>> Stashed changes

        Debug.Log("Nhặt quà thành công! 🎁");
        packageCount++;
        driverController.DecreaseSpeed(); // Giảm tốc độ
        Destroy(package);

<<<<<<< Updated upstream
        pickupCoroutine = null; // Reset Coroutine sau khi hoàn thành
    }

    private IEnumerator DeliverPackages()
=======
        // Lưu màu cũ vào Stack
        carColorStack.Push(currentCarColor);

        // Random màu mới cho xe (trừ Default)
        currentCarColor = (CarColor)Random.Range(1, 4);
        UpdateCarColor(currentCarColor);

        pickupCoroutine = null;
    }

    private IEnumerator DeliverPackages(GameObject location)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        packageCount = 0; // Reset số quà sau khi giao
        driverController.ResetSpeed(); // Khôi phục tốc độ

        deliveryCoroutine = null; // Reset Coroutine sau khi hoàn thành
=======
        receiveDisplay.IncrementReceivedCount(1);
        packageCount--;
        receiveDisplay.CountBoxOnCar(packageCount);
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
>>>>>>> Stashed changes
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Package") && pickupCoroutine != null)
        {
            Debug.Log("Bỏ lỡ quà vì đi quá nhanh! ❌");
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
<<<<<<< Updated upstream
=======

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
>>>>>>> Stashed changes
}
