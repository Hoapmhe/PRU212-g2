using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeText;

    private float startTime;

    void Awake() 
    {
        GameObject timeDisplayObject = GameObject.Find("TimeDisplay");
        if (timeDisplayObject != null)
        {
            timeText = timeDisplayObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Không tìm thấy GameObject tên 'TimeDisplay'!");
        }
    }

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60); 

        timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}