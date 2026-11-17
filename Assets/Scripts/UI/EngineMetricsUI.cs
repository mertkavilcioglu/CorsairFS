using TMPro;
using UnityEngine;

public class EngineMetricsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI throttleText;
    [SerializeField] private TextMeshProUGUI rpmText;
    void Start()
    {
        SetSpeed(0);
        SetThrottle(0);
        SetRPM(0);
    }

    void Update()
    {
        
    }

    public void SetSpeed(double speed)
    {
        speedText.text = speed.ToString("F0") + " km/h"; 
    }

    public void SetThrottle(double throttle)
    {
        throttleText.text = throttle.ToString("F0");  
    }

    public void SetRPM(double rpm)
    {
        rpmText.text = rpm.ToString("F0");
    }
}
