using UnityEngine;
using System.Collections; // Cần import để dùng Coroutine

public class Collisions : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;
    [SerializeField] private int maxPackages = 3;
    [SerializeField] private float pickupTime = 1f; // Thời gian nhặt quà (2s)
    [SerializeField] private float deliveryTime = 3f; // Thời gian giao hàng (3s)

    private int packageCount = 0;
    private DriverController driverController;
    private Coroutine pickupCoroutine;
    private Coroutine deliveryCoroutine;

    private void Start()
    {
        driverController = GetComponent<DriverController>(); // Lấy script DriverController từ xe
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && packageCount < maxPackages)
        {
            if (pickupCoroutine == null) // Kiểm tra xem có đang nhặt quà không
            {
                pickupCoroutine = StartCoroutine(PickupPackage(other.gameObject));
            }
        }

        if (other.CompareTag("Location") && packageCount > 0)
        {
            if (deliveryCoroutine == null)
            {
                deliveryCoroutine = StartCoroutine(DeliverPackages());
            }
        }
    }

    private IEnumerator PickupPackage(GameObject package)
    {
        Debug.Log("Bắt đầu nhặt quà... ⏳ (1s)");
        yield return new WaitForSeconds(pickupTime); // Chờ 1 giây

        Debug.Log("Nhặt quà thành công! 🎁");
        packageCount++;
        driverController.DecreaseSpeed(); // Giảm tốc độ
        Destroy(package);

        pickupCoroutine = null; // Reset Coroutine sau khi hoàn thành
    }

    private IEnumerator DeliverPackages()
    {
        Debug.Log("Bắt đầu giao hàng... 📦 (3s)");
        yield return new WaitForSeconds(deliveryTime); // Chờ 3 giây

        Debug.Log("Giao hàng thành công! ✅");
        packageCount = 0; // Reset số quà sau khi giao
        driverController.ResetSpeed(); // Khôi phục tốc độ

        deliveryCoroutine = null; // Reset Coroutine sau khi hoàn thành
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
}
