using UnityEngine;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;

public class FlapControls : MonoBehaviour
{

    [Header("OUTER FLAP")] 
    // Right outer flap pivot located on folding part of the right wing
    [SerializeField] private Transform rightFlapOuterPivot;
    [SerializeField] private Transform rightFlapOuterRetractedTransform;
    [SerializeField] private Transform rightFlapOuterDeployedTransform;

    // Left outer flap pivot located on folding part of the left wing
    [SerializeField] private Transform leftFlapOuterPivot;
    [SerializeField] private Transform leftFlapOuterRetractedTransform;
    [SerializeField] private Transform leftFlapOuterDeployedTransform;



    [Header("MIDDLE FLAP")]
    // Right middle flap pivot located on folding part of the right wing
    [SerializeField] private Transform rightFlapMiddlePivot;
    [SerializeField] private Transform rightFlapMiddleRetractedTransform;
    [SerializeField] private Transform rightFlapMiddleDeployedTransform;


    // Left middle flap pivot located on folding part of the left wing
    [SerializeField] private Transform leftFlapMiddlePivot;
    [SerializeField] private Transform leftFlapMiddleRetractedTransform;
    [SerializeField] private Transform leftFlapMiddleDeployedTransform;

    [Header("INNER FLAP")]
    // Right inner flap pivot located on folding part of the right wing
    [SerializeField] private Transform rightFlapInnerPivot;
    [SerializeField] private Transform rightFlapInnerRetractedTransform;
    [SerializeField] private Transform rightFlapInnerDeployedTransform;


    // Left inner flap pivot located on folding part of the left wing
    [SerializeField] private Transform leftFlapInnerPivot;
    [SerializeField] private Transform leftFlapInnerRetractedTransform;
    [SerializeField] private Transform leftFlapInnerDeployedTransform;


    [Header("AILERONS")]
    [SerializeField] private Transform rightAileron;
    [SerializeField] private Transform leftAileron;

    [Header("")]
    [SerializeField] private float foldSpeed = 10f;
    [SerializeField] private float foldAngle = 50f;

    private bool areFlapsDeployed = true; // Parked at the beginning, so flaps are deployed by default
    private bool areFlapsDeploying = false;
    private bool areFlapsRetracting = false;

    // TODO: **************** Mid ve inner kısımları da hallet ve deploy & retract fonskiyonlarının sonundaki if kontrolü içine tüm pivotlar icin kontrol koy.

    void Start()
    {
        // Outer
        rightFlapOuterPivot.localRotation = rightFlapOuterDeployedTransform.localRotation; // Set initial rotation to deployed position
        leftFlapOuterPivot.localRotation = leftFlapOuterDeployedTransform.localRotation; // Set initial rotation to deployed position

        // Middle
        rightFlapMiddlePivot.localRotation = rightFlapMiddleDeployedTransform.localRotation; // Set initial rotation to deployed position
        leftFlapMiddlePivot.localRotation = leftFlapMiddleDeployedTransform.localRotation; // Set initial rotation to deployed position

        // Inner
        rightFlapInnerPivot.localRotation = rightFlapInnerDeployedTransform.localRotation; // Set initial rotation to deployed position
        leftFlapInnerPivot.localRotation = leftFlapInnerDeployedTransform.localRotation; // Set initial rotation to deployed position
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
        // Outer 
        rightFlapOuterPivot.localRotation = Quaternion.RotateTowards(rightFlapOuterPivot.localRotation, rightFlapOuterDeployedTransform.localRotation, foldSpeed * Time.deltaTime);
        leftFlapOuterPivot.localRotation = Quaternion.RotateTowards(leftFlapOuterPivot.localRotation, leftFlapOuterDeployedTransform.localRotation, foldSpeed * Time.deltaTime);

        // Middle

        // Inner

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
        // Outer
        rightFlapOuterPivot.localRotation = Quaternion.RotateTowards(rightFlapOuterPivot.localRotation, rightFlapOuterRetractedTransform.localRotation, foldSpeed * Time.deltaTime);
        leftFlapOuterPivot.localRotation = Quaternion.RotateTowards(leftFlapOuterPivot.localRotation, leftFlapOuterRetractedTransform.localRotation, foldSpeed * Time.deltaTime);

        // Middle

        // Inner

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
