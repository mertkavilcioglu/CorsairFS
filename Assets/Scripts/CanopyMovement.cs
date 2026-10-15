using UnityEngine;

public class CanopyMovement : MonoBehaviour
{
    private StartUpProcedure startUpProcedure;
    [SerializeField] private Transform canopy;  
    [SerializeField] private Transform closedCanopy;
    [SerializeField] private Transform openCanopy;
    private float canopyMoveSpeed = 0.2f;
    public bool isCanopyOpen = true;
    private bool isCanopyClosing = false;
    private bool isCanopyOpening = false;

    void Start()
    {
        startUpProcedure = GetComponent<StartUpProcedure>();

    }

    
    void Update()
    {
        if(isCanopyClosing)
        {
            CloseCanopy();
        }
    }

    public void StartClosingCanopy()
    {
        if(!isCanopyClosing && isCanopyOpen)
        {
            isCanopyClosing = true;
            isCanopyOpening = false;
        }
    }

    public void StartOpeningCanopy()
    {
        if (!isCanopyOpening && !isCanopyOpen)
        {
            isCanopyOpening = true;
            isCanopyClosing = false;
        }
    }

    public void CloseCanopy()
    {
        if (isCanopyClosing)
        {
            canopy.position = Vector3.MoveTowards(canopy.position, closedCanopy.position, canopyMoveSpeed * Time.deltaTime);
            canopy.rotation = Quaternion.RotateTowards(canopy.rotation, closedCanopy.rotation, canopyMoveSpeed * 10f * Time.deltaTime);

            if (Vector3.Distance(canopy.position, closedCanopy.position) < 0.001f &&
                Quaternion.Angle(canopy.rotation, closedCanopy.rotation) < 0.1f)
            {
                canopy.position = closedCanopy.position;
                canopy.rotation = closedCanopy.rotation;

                isCanopyClosing = false;
                isCanopyOpen = false;
            }
        }
    }
}
