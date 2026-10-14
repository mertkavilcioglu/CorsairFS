using UnityEngine;

public class WingFoldControls : MonoBehaviour
{
    [SerializeField] private Transform rightWing;
    [SerializeField] private Transform leftWing;
    [SerializeField] private float foldSpeed = 15f;
    [SerializeField] private float foldAngle = 95f;
    private bool isFolding;
    private bool isUnfolding;

    private Quaternion rightWingFoldRotation;
    private Quaternion leftWingFoldRotation;

    void Start()
    {
        rightWingFoldRotation = Quaternion.Euler(0f, 0f, foldAngle);
        leftWingFoldRotation = Quaternion.Euler(0f, 0f, -foldAngle);

        rightWing.localRotation = rightWingFoldRotation;
        leftWing.localRotation = leftWingFoldRotation;
        //StartUnfoldingWings();
    }

    void Update()
    {
        if (isFolding)
            FoldWings();
        else if (isUnfolding)
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
