using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI displayText;
    private int receivedCount = 00;
    private int onDelivery = 00;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        UpdateUI(); // Cập nhật UI ban đầu
    }
    void Update()
    {
        UpdateUI();
    }
    public void IncrementReceivedCount(int packageCount)
    {
        receivedCount += packageCount;
        UpdateUI();
    }

    public void CountBoxOnCar(int number)
    {
        onDelivery = number;
        UpdateUI();
    }

    private void UpdateUI()
    {
        float elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);


        displayText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + " Delivered: " + receivedCount + ", on delivery: " + onDelivery; ;
    }
}