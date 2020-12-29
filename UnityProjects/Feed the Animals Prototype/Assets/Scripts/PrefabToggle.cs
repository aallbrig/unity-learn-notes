using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabToggle : MonoBehaviour
{
    [Serializable]
    public struct ToggleableGameObject
    {
        public GameObject prefab;
        public bool isActive;
    }

    public List<ToggleableGameObject> listOfPrefabs;

    private void Start()
    {
        listOfPrefabs.ForEach((toggleable) => toggleable.prefab.SetActive(toggleable.isActive));
    }
}
