using UnityEngine;

public class RoadBoundary : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		DriverController driver = collision.gameObject.GetComponent<DriverController>();
		if (driver != null)
		{
			Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				Vector2 bounceDirection = collision.contacts[0].normal; // Hướng phản lực từ điểm va chạm
				rb.AddForce(bounceDirection * 10f, ForceMode2D.Impulse); // Điều chỉnh lực đẩy
			}
		}
	}
}
