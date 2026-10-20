using UnityEngine;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;

public class FlapControls : MonoBehaviour
{

    // Right outer flap pivot located on folding part of the right wing
    [SerializeField] private Transform rightFlapOuterPivot;
    [SerializeField] private Transform rightFlapOuterRetractedTransform;
    [SerializeField] private Transform rightFlapOuterDeployedTransform;


    // Left outer flap pivot located on folding part of the left wing
    [SerializeField] private Transform leftFlapOuterPivot;
    [SerializeField] private Transform leftFlapOuterRetractedTransform;
    [SerializeField] private Transform leftFlapOuterDeployedTransform;


    [SerializeField] private Transform rightAileron;
    [SerializeField] private Transform leftAileron;

    [SerializeField] private float foldSpeed = 10f;
    [SerializeField] private float foldAngle = 50f;

    private bool areFlapsDeployed = true; // Parked at the beginning, so flaps are deployed by default
    private bool areFlapsDeploying = false;
    private bool areFlapsRetracting = false;

    void Start()
    {
        rightFlapOuterPivot.rotation = rightFlapOuterDeployedTransform.rotation; // Set initial rotation to deployed position
        leftFlapOuterPivot.rotation = leftFlapOuterDeployedTransform.rotation; // Set initial rotation to deployed position

        //StartRetractingFlaps();
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
        rightFlapOuterPivot.localRotation = Quaternion.RotateTowards(rightFlapOuterPivot.localRotation, rightFlapOuterDeployedTransform.localRotation, foldSpeed * Time.deltaTime);
        leftFlapOuterPivot.localRotation = Quaternion.RotateTowards(leftFlapOuterPivot.localRotation, leftFlapOuterDeployedTransform.localRotation, foldSpeed * Time.deltaTime);


        if (Quaternion.Angle(rightFlapOuterPivot.localRotation, rightFlapOuterDeployedTransform.localRotation) < 0.1f 
            && Quaternion.Angle(leftFlapOuterPivot.localRotation, leftFlapOuterDeployedTransform.localRotation) < 0.1f)
        {
            rightFlapOuterPivot.localRotation = rightFlapOuterDeployedTransform.localRotation;
            leftFlapOuterPivot.localRotation = leftFlapOuterDeployedTransform.localRotation;

            areFlapsDeploying = false;
            areFlapsDeployed = true;
        }
    }

    
    public void RetractFlaps()
    {
        rightFlapOuterPivot.localRotation = Quaternion.RotateTowards(rightFlapOuterPivot.localRotation, rightFlapOuterRetractedTransform.localRotation, foldSpeed * Time.deltaTime);
        leftFlapOuterPivot.localRotation = Quaternion.RotateTowards(leftFlapOuterPivot.localRotation, leftFlapOuterRetractedTransform.localRotation, foldSpeed * Time.deltaTime);


        if (Quaternion.Angle(rightFlapOuterPivot.localRotation, rightFlapOuterRetractedTransform.localRotation) < 0.1f
            && Quaternion.Angle(leftFlapOuterPivot.localRotation, leftFlapOuterRetractedTransform.localRotation) < 0.1f)
        {
            rightFlapOuterPivot.localRotation = rightFlapOuterRetractedTransform.localRotation;
            leftFlapOuterPivot.localRotation = leftFlapOuterRetractedTransform.localRotation;

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
