using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizePrefab : MonoBehaviour
{
    [Header("Location of prefabs")] public List<GameObject> targetGameObjects;
    private readonly List<GameObject> _listOfPrefabs = new List<GameObject>();
    private GameObject _activePrefab;

    public GameObject GetActivePrefab()
    {
        return _activePrefab;
    }
    private void Start()
    {
        if (targetGameObjects == null) return;

        foreach (var child in targetGameObjects.SelectMany(targetGameObject => targetGameObject.transform.Cast<Transform>()))
          _listOfPrefabs.Add(child.gameObject);

        var randomIndex = Random.Range(0, _listOfPrefabs.Count - 1);

        // Hide all objects except for the randomly active one.
        for (var i = 0; i < _listOfPrefabs.Count; i++)
            _listOfPrefabs[i].SetActive(i == randomIndex);

        _activePrefab = _listOfPrefabs[randomIndex];
    }
}
