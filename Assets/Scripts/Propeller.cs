using UnityEngine;

public class Propeller : MonoBehaviour
{

    //Throttle bilgisini al, pervane dönme hızını ona göre ayarla ayrıca uçağa itkiyi de bu script içerisinde uygula

    [SerializeField] private Transform propeller;
    [SerializeField] private SimpleSpinBlur propBlur;
    [SerializeField] private MeshRenderer propMesh;
    private float timer = 0f;
    private bool isEngineOn = false;
    private bool isEngineStarting = false;

    [SerializeField] private float targetRPM_startUp = 33f;      // 33 RPM
    [SerializeField] private float targetRPM_idle = 417f;        // 417 RPM

    [SerializeField] private float propellerAcceleration_fromCold = 33f;      // 33 RPM/s
    [SerializeField] private float propellerAcceleration_idle = 250f;       // 250 RPM/s

    private float currentRPM = 0f; 

    void Start()
    {
        StartEngine();
        propBlur.enabled = false;
        propMesh.enabled = true;
    }

    void Update()
    {
        if (isEngineStarting && !isEngineOn)
        {
            StartPropellerTurn();
        }
        else if (isEngineOn)
        {
            PropellerTurn();
        }
    }

    private void FixedUpdate()
    {

    }

    public void StartEngine()
    {
        if(!isEngineStarting)
        {
            isEngineStarting = true;
        }
    }

    public void StartPropellerTurn()
    {
        timer += Time.deltaTime;

        float targetRPM;
        float targetAcceleration;

        if (timer < 4f)
        {
            targetRPM = targetRPM_startUp;
            targetAcceleration = propellerAcceleration_fromCold;
        }
        else
        {
            if (!propBlur.enabled)
            {
                propBlur.enabled = true;
            }
            targetRPM = targetRPM_idle;
            targetAcceleration = propellerAcceleration_idle;
        }

        currentRPM = Mathf.MoveTowards(currentRPM, targetRPM, targetAcceleration * Time.deltaTime);
        propeller.Rotate(0f, 0f, -RPMtoDegrees(currentRPM) * Time.deltaTime, Space.Self);

        if (Mathf.Abs(currentRPM - targetRPM_idle) < 0.1f && propMesh.enabled)
        {
            propMesh.enabled = false;
        }

        if (Mathf.Abs(currentRPM - targetRPM_idle) < 0.1f && timer >= 7f)
        {
            currentRPM = targetRPM_idle;
            isEngineOn = true;
            isEngineStarting = false;
        }
    }

    public void PropellerTurn()
    {
        propeller.Rotate(0f, 0f, -RPMtoDegrees(currentRPM) * Time.deltaTime, Space.Self);
    }

    public float RPMtoDegrees(float rpm)
    {
        return rpm * 6f;
    }

    public float DegreesToRPM(float degrees)
    {
        return degrees / 6f;
    }

}
