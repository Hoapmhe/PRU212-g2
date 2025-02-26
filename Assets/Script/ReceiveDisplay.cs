using TMPro;
using UnityEngine;

public class ReceiveDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI receivedText;
    private int receivedCount = 00;
    private int onDelivery = 00;
    void Awake()
    {
        GameObject timeDisplayObject = GameObject.Find("CollectedBoxesText");
        if (timeDisplayObject != null)
        {
            receivedText = timeDisplayObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Không tìm thấy GameObject tên 'CollectedBoxesText'!");
        }
    }

    void Start()
    {
        UpdateReceivedUI(); // Cập nhật UI ban đầu
    }

    public void IncrementReceivedCount(int packageCount)
    {
        receivedCount += packageCount;
        UpdateReceivedUI();
    }

    public void CountBoxOnCar(int number)
    {
        onDelivery = number;
        UpdateReceivedUI();
    }

    private void UpdateReceivedUI()
    {
        if (receivedText != null)
        {
            receivedText.text = "Delivered: " + receivedCount + ", on delivery: " + onDelivery;
        }
        else
        {
            Debug.LogError("Chưa gán TextMeshProUGUI vào ReceiveDisplay!");
        }
    }
}
