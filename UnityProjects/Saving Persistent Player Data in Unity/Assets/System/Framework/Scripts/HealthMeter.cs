using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    public CharacterSaveData_SO characterData;
    public Text meter;

    void Awake()
    {
        InvokeRepeating("Refresh", 1, 1);
    }

    void Refresh()
    {
        if (characterData != null)
        {
            meter.text = characterData.CurrentHealth.ToString();
        }
    }
}
