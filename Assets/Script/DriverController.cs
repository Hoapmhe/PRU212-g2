using UnityEngine;

public class DriverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] float steerSpeed = 90f;
    void Start()
    {

    }

    void Update()
    {
        HandleMoving();
    }

    void HandleMoving()
    {
        float changeSteer = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float changeMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, changeMove, 0);
        transform.Rotate(0, 0, -changeSteer);
    }
}
