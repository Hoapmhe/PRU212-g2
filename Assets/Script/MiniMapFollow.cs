using UnityEngine;

public class MiniMapFollow2D : MonoBehaviour
{
    [SerializeField] public Transform target; // Xe cần theo dõi
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Độ cao của Camera so với xe

    void LateUpdate()
    {
        if (target == null) return;

        // Cập nhật vị trí của MiniMapCamera theo xe, giữ nguyên Z
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
    }
}
