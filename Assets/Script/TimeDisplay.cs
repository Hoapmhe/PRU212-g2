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
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");
        timeText.text = minutes + ":" + seconds;
    }
}