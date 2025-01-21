
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField]
    public static float timerDuration = 5f * 60;
    [SerializeField]
    public static float time;
    [SerializeField] 
    private bool timeRunning;

    [SerializeField]
    private string firstMinute;
    [SerializeField]
    private string secondMinute;
    [SerializeField]
    private string seperator;
    [SerializeField]
    private string firstSecond;
    [SerializeField]
    private string secondSecond;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < timerDuration)
        {
            time += Time.deltaTime;
            UpdateTimer(time);
        }
    }
   private void ResetTimer()
    {
        time = 0f;
    }
    private void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}",minutes,seconds);
        firstMinute = currentTime[0].ToString();
        secondMinute = currentTime[1].ToString();
        seperator = ":";
        firstSecond = currentTime[2].ToString();
        secondSecond = currentTime[3].ToString();
    }

}
