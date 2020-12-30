using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizePrefab : MonoBehaviour
{
    [Header("Location of prefabs")] public List<GameObject> targetGameObjects;
    private readonly List<GameObject> _listOfPrefabs = new List<GameObject>();
    
    private void Start()
    {
        if (targetGameObjects == null) return;

        foreach (GameObject targetGameObject in targetGameObjects)
        {
            foreach (Transform child in targetGameObject.transform)
            {
                _listOfPrefabs.Add(child.gameObject);
            }
        }

        var index = Random.Range(0, _listOfPrefabs.Count - 1);

        for (var i = 0; i < _listOfPrefabs.Count; i++)
        {
            _listOfPrefabs[i].SetActive(i == index);
        }
    }
}
