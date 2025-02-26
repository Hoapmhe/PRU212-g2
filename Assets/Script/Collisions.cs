using UnityEngine;
using System.Collections; // Cần import để dùng Coroutine

public class Collisions : MonoBehaviour
{
    [SerializeField] private int maxPackages = 3;
    [SerializeField] private float pickupTime = 1f; // Thời gian nhặt quà (1s)
    [SerializeField] private float deliveryTime = 3f; // Thời gian giao hàng (3s)

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultCar; // Xe màu hồng
    [SerializeField] private Sprite yellowCar;   // Xe màu vang
    [SerializeField] private Sprite blueCar;     // Xe màu xanh


    private int packageCount = 0;
    private DriverController driverController;
    private Coroutine pickupCoroutine;
    private Coroutine deliveryCoroutine;
    private ReceiveDisplay receiveDisplay; //hien thi so luong Package da giao
    private Rigidbody2D rb; // Rigidbody2D của xe

    private void Start()
    {
        driverController = GetComponent<DriverController>(); // Lấy script DriverController từ xe
        receiveDisplay = FindFirstObjectByType<ReceiveDisplay>();
        rb = GetComponent<Rigidbody2D>();


        if (receiveDisplay == null)
        {
            Debug.LogError("Không tìm thấy ReceiveDisplay trong Scene!");
        }
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
        Debug.Log("Bắt đầu nhặt quà... ⏳ (2s)");
        yield return new WaitForSeconds(pickupTime);

        packageCount++;
        driverController.DecreaseSpeed();
        Destroy(package);

        // Thay đổi màu xe
        if (packageCount == 1 || packageCount == 2)
        {
            spriteRenderer.sprite = yellowCar; // Xe xanh khi có 1 hoặc 2 quà
        }
        else if (packageCount == 3)
        {
            spriteRenderer.sprite = blueCar; // Xe đỏ khi đủ 3 quà
        }

        pickupCoroutine = null;
    }


    private IEnumerator DeliverPackages()
    {
        Debug.Log("Bắt đầu giao hàng... 📦 (3s)");
        yield return new WaitForSeconds(deliveryTime); // Chờ 3 giây

        Debug.Log("Giao hàng thành công! ✅");
        receiveDisplay.IncrementReceivedCount(packageCount);
        packageCount = 0; // Reset số quà sau khi giao
        driverController.ResetSpeed(); // Khôi phục tốc độ

        // Đổi lại xe về màu hồng sau khi giao hàng
        spriteRenderer.sprite = defaultCar;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock")) // Hòn đá có tag là "Rock"
        {
            if (rb != null)
            {
                float impactForce = rb.linearVelocity.magnitude; // Lấy tốc độ xe
                Debug.Log("Va vào hòn đá! Lực va chạm: " + impactForce);

                // Tạo phản lực nhẹ nếu xe đang đi nhanh
                if (impactForce > 2f) // Ngưỡng lực va chạm
                {
                    Vector2 bounceDirection = collision.contacts[0].normal; // Hướng phản lực
                    rb.AddForce(bounceDirection * impactForce * 50f); // Điều chỉnh độ nảy
                }
            }
        }
    }

}
