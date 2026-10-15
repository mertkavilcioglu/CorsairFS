using UnityEngine;

public class StartUpProcedure : MonoBehaviour
{
    private Propeller propeller;
    private WingFoldControls wingFoldControls;
    private CanopyMovement canopyMovement;
    private bool isStartUpProcedureActive = false;

    void Start()
    {
        propeller = GetComponent<Propeller>();
        wingFoldControls = GetComponent<WingFoldControls>();
        canopyMovement = GetComponent<CanopyMovement>();

        InitializeStartUpProcedure();  
    }

    void Update()
    {

    }

    public void InitializeStartUpProcedure()
    {
        propeller.StartEngine();
        isStartUpProcedureActive = true;
    }

    public void UnfoldWings()
    {
        wingFoldControls.StartUnfoldingWings();
    }

    public void CloseCanopy()
    {
        canopyMovement.StartClosingCanopy();
    }

    public void SetIsStartUpProcedureActive(bool isActive)
    {
        isStartUpProcedureActive = isActive;
    }
}
