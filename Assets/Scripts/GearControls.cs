using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearControls : MonoBehaviour
{
    [Header("RIGHT GEAR")]
    [SerializeField] private GameObject rightGearDeployed;
    [SerializeField] private GameObject rightGearRetracted;

    [Header("LEFT GEAR")]   
    [SerializeField] private GameObject leftGearDeployed;
    [SerializeField] private GameObject leftGearRetracted;

    [Header("TAIL GEAR")]
    [SerializeField] private GameObject tailGearDeployed;
    [SerializeField] private GameObject tailGearRetracted;

    private List<GameObject> deployedGears = new List<GameObject>();
    private List<GameObject> retractedGears = new List<GameObject>();

    [SerializeField] private float deployDuration = 5f;
    [SerializeField] private float retractDuration = 5f;
    private bool isGearMoving = false;

    private bool isGearDeployed = true; // Gear is deployed at parking
    void Start()
    {
        deployedGears.Add(rightGearDeployed);
        deployedGears.Add(leftGearDeployed);
        deployedGears.Add(tailGearDeployed);

        retractedGears.Add(rightGearRetracted);
        retractedGears.Add(leftGearRetracted);
        retractedGears.Add(tailGearRetracted);

        DeployGear();
    }


    public void ToggleGear()
    {
        if (isGearMoving)
            return;

        if (isGearDeployed)
            StartCoroutine(RetractGearRoutine());
        else
            StartCoroutine(DeployGearRoutine());
    }

    public void DeployGear()
    {
        if (!isGearDeployed)
        {
            foreach (GameObject gear in deployedGears)
            {
                gear.SetActive(true);
            }

            foreach (GameObject gear in retractedGears)
            {
                gear.SetActive(false);
            }

            isGearDeployed = true;
        }
    }

    public void RetractGear()
    {
        if (isGearDeployed)
        {
            foreach (GameObject gear in deployedGears)
            {
                gear.SetActive(false);
            }

            foreach (GameObject gear in retractedGears)
            {
                gear.SetActive(true);
            }

            isGearDeployed = false;
        }
    }

    private IEnumerator DeployGearRoutine()
    {
        isGearMoving = true;

        yield return new WaitForSeconds(deployDuration);

        DeployGear();
        isGearMoving = false;
    }

    private IEnumerator RetractGearRoutine()
    {
        isGearMoving = true;

        yield return new WaitForSeconds(retractDuration);

        RetractGear();
        isGearMoving = false;
    }
}
