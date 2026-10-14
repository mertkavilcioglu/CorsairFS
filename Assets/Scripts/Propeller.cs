using UnityEngine;

public class Propeller : MonoBehaviour
{

    //Throttle bilgisini al, pervane dönme hızını ona göre ayarla ayrıca uçağa itkiyi de bu script içerisinde uygula

    [SerializeField] private Transform propeller;
    private bool isEngineOn = false;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        
    }

    public void EngineStart()
    {
        // throttle döndürmeye basla;

        isEngineOn = true;
    }
}
