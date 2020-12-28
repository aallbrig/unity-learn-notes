using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [Header("Y-up vector (in meters per second)")]
    public Vector3 vector = new Vector3(0, 0, 10);
    
    [Header("Enable model (unique weight in kilograms)")]
    public GameObject armoredCar;
    public bool armoredCarActive = false;
    public GameObject bus;
    public bool busActive = false;
    public GameObject blueCar;
    public bool blueCarActive = false;
    public GameObject redCar;
    public bool redCarActive = true;
    public GameObject greenVan;
    public bool greenVanActive = false;

    private void Start()
    {
        armoredCar.SetActive(armoredCarActive);
        bus.SetActive(busActive);
        blueCar.SetActive(blueCarActive);
        redCar.SetActive(redCarActive);
        greenVan.SetActive(greenVanActive);
    }

    private void Update()
    {
        transform.Translate( vector * Time.deltaTime);
    }
}
