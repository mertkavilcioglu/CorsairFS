using UnityEngine;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;

public class FlapControls : MonoBehaviour
{
    [SerializeField] private Transform rightFlapOuter;
    [SerializeField] private Transform leftFlapOuter;
    [SerializeField] private Transform rightFlapInner;
    [SerializeField] private Transform leftFlapInner;

    private Quaternion rightFlapOuterDeployedRotation;
    private Quaternion leftFlapOuterDeployedRotation; 
    private Quaternion rightFlapInnerDeployedRotation;
    private Quaternion leftFlapInnerDeployedRotation;


    [SerializeField] private Transform rightAileron;
    [SerializeField] private Transform leftAileron;

    [SerializeField] private float foldSpeed = 15f;
    [SerializeField] private float foldAngle = 50f;

    private bool areFlapsDeployed = true; // Parked at the beginning, so flaps are deployed by default
    private bool areFlapsDeploying = false;
    private bool areFlapsRetracting = false;

    void Start()
    {
        rightFlapOuterDeployedRotation = Quaternion.Euler(0f, 0f, foldAngle);
        rightFlapInnerDeployedRotation = Quaternion.Euler(0f, 0f, foldAngle);
        leftFlapOuterDeployedRotation = Quaternion.Euler(0f, 0f, -foldAngle);
        leftFlapInnerDeployedRotation = Quaternion.Euler(0f, 0f, -foldAngle);

        rightFlapOuter.localRotation = rightFlapOuterDeployedRotation;
        rightFlapInner.localRotation = rightFlapInnerDeployedRotation;
        leftFlapOuter.localRotation = leftFlapOuterDeployedRotation;
        leftFlapInner.localRotation = leftFlapInnerDeployedRotation;
    }

    void Update()
    {
        if (!areFlapsDeployed && areFlapsDeploying)
        {
            DeployFlaps();
        }
        else if(areFlapsDeployed && areFlapsRetracting)
        {
            RetractFlaps();
        }
    }

    public void DeployFlaps()
    {
        rightFlapOuter.localRotation = Quaternion.RotateTowards(rightFlapOuter.localRotation, rightFlapOuterDeployedRotation, foldSpeed * Time.deltaTime);
        rightFlapInner.localRotation = Quaternion.RotateTowards(rightFlapInner.localRotation, rightFlapInnerDeployedRotation, foldSpeed * Time.deltaTime);
        leftFlapOuter.localRotation = Quaternion.RotateTowards(leftFlapOuter.localRotation, leftFlapOuterDeployedRotation, foldSpeed * Time.deltaTime);
        leftFlapInner.localRotation = Quaternion.RotateTowards(leftFlapInner.localRotation, leftFlapInnerDeployedRotation, foldSpeed * Time.deltaTime);


        if (Quaternion.Angle(rightFlapOuter.localRotation, rightFlapOuterDeployedRotation) < 0.1f 
            && Quaternion.Angle(leftFlapOuter.localRotation, leftFlapOuterDeployedRotation) < 0.1f
            && Quaternion.Angle(rightFlapInner.localRotation, rightFlapInnerDeployedRotation) < 0.1f
            && Quaternion.Angle(leftFlapInner.localRotation, leftFlapInnerDeployedRotation) < 0.1f)
        {
            rightFlapOuter.localRotation = rightFlapOuterDeployedRotation;
            rightFlapInner.localRotation = rightFlapInnerDeployedRotation;
            leftFlapOuter.localRotation = leftFlapOuterDeployedRotation;
            leftFlapInner.localRotation = leftFlapInnerDeployedRotation;

            areFlapsDeploying = false;
            areFlapsDeployed = true;
        }
    }

    public void RetractFlaps()
    {
        rightFlapOuter.localRotation = Quaternion.RotateTowards(rightFlapOuter.localRotation, Quaternion.identity, foldSpeed * Time.deltaTime);
        rightFlapInner.localRotation = Quaternion.RotateTowards(rightFlapInner.localRotation, Quaternion.identity, foldSpeed * Time.deltaTime);
        leftFlapOuter.localRotation = Quaternion.RotateTowards(leftFlapOuter.localRotation, Quaternion.identity, foldSpeed * Time.deltaTime);
        leftFlapInner.localRotation = Quaternion.RotateTowards(leftFlapInner.localRotation, Quaternion.identity, foldSpeed * Time.deltaTime);


        if (Quaternion.Angle(rightFlapOuter.localRotation, Quaternion.identity) < 0.1f
            && Quaternion.Angle(leftFlapOuter.localRotation, Quaternion.identity) < 0.1f
            && Quaternion.Angle(rightFlapInner.localRotation, Quaternion.identity) < 0.1f
            && Quaternion.Angle(leftFlapInner.localRotation, Quaternion.identity) < 0.1f)
        {
            rightFlapOuter.localRotation = Quaternion.identity;
            rightFlapInner.localRotation = Quaternion.identity;
            leftFlapOuter.localRotation = Quaternion.identity;
            leftFlapInner.localRotation = Quaternion.identity;

            areFlapsRetracting = false;
            areFlapsDeployed = false;
        }
    }

    public void StartDeployingFlaps()
    {
        if (!areFlapsDeployed && !areFlapsDeploying)
        {
            areFlapsDeploying = true;
            areFlapsRetracting = false;
        }
    }

    public void StartRetractingFlaps()
    {
        if (areFlapsDeployed && !areFlapsRetracting)
        {
            areFlapsRetracting = true;
            areFlapsDeploying = false;
        }
    }
}
