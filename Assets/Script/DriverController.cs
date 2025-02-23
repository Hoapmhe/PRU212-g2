using UnityEngine;

public class DriverController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float speedDecrease = 1f;
    [SerializeField] private float steerSpeed = 30f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        HandleMoving();
    }

    void HandleMoving()
    {
        float changeSteer = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float changeMove = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
        transform.Translate(0, changeMove, 0);
        transform.Rotate(0, 0, -changeSteer);
    }

    public void DecreaseSpeed()
    {
        currentSpeed = Mathf.Max(minSpeed, currentSpeed - speedDecrease); // Đảm bảo tốc độ không giảm xuống dưới minSpeed
    }

    public void ResetSpeed()
    {
        currentSpeed = baseSpeed; // Khôi phục tốc độ ban đầu khi giao hàng
    }
}
