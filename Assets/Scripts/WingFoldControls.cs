using UnityEngine;

public class WingFoldControls : MonoBehaviour
{
    private StartUpProcedure startUpProcedure;
    [SerializeField] private Transform rightWing;
    [SerializeField] private Transform leftWing;
    [SerializeField] private float foldSpeed = 15f;
    [SerializeField] private float foldAngle = 95f;
    public bool isFolding = false;
    public bool isUnfolding = false;

    public bool isFolded = true;

    private Quaternion rightWingFoldRotation;
    private Quaternion leftWingFoldRotation;

    void Start()
    {
        startUpProcedure = GetComponent<StartUpProcedure>();

        rightWingFoldRotation = Quaternion.Euler(0f, 0f, foldAngle);
        leftWingFoldRotation = Quaternion.Euler(0f, 0f, -foldAngle);

        rightWing.localRotation = rightWingFoldRotation;
        leftWing.localRotation = leftWingFoldRotation;
        //StartUnfoldingWings();
    }

    void Update()
    {
        if (isFolding && !isFolded)
            FoldWings();
        else if (isUnfolding && isFolded)
            UnfoldWings();
    }

    private void FoldWings()
    {
        rightWing.localRotation = Quaternion.RotateTowards(rightWing.localRotation, rightWingFoldRotation, foldSpeed * Time.deltaTime);
        leftWing.localRotation = Quaternion.RotateTowards(leftWing.localRotation, leftWingFoldRotation, foldSpeed * Time.deltaTime);

        if (Quaternion.Angle(rightWing.localRotation, rightWingFoldRotation) < 0.1f && Quaternion.Angle(leftWing.localRotation, leftWingFoldRotation) < 0.1f)
        {
            rightWing.localRotation = rightWingFoldRotation;
            leftWing.localRotation = leftWingFoldRotation;

            isFolding = false;
            isFolded = true;
        }
    }

    private void UnfoldWings()
    {
        rightWing.localRotation = Quaternion.RotateTowards(rightWing.localRotation, Quaternion.identity, foldSpeed * Time.deltaTime);
        leftWing.localRotation = Quaternion.RotateTowards(leftWing.localRotation, Quaternion.identity, foldSpeed * Time.deltaTime);

        if (Quaternion.Angle(rightWing.localRotation, Quaternion.identity) < 0.1f && Quaternion.Angle(leftWing.localRotation, Quaternion.identity) < 0.1f)
        {
            rightWing.localRotation = Quaternion.identity;
            leftWing.localRotation = Quaternion.identity;

            isUnfolding = false;
            isFolded = false;
            startUpProcedure.CloseCanopy();
        }
    }

    public void StartFoldingWings()
    {
        isFolding = true;
        isUnfolding = false;
    }

    public void StartUnfoldingWings()
    {
        isFolding = false;
        isUnfolding = true;
    }
}
