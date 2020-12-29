using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

public class PrefabToggle : MonoBehaviour
{
    [Header("Index is 0 based")] public int activeIndex;

    [Header("Optionally, randomize active prefab")]
    public bool randomizeActivePrefab;
    [Header("Location of prefabs")] public GameObject targetGameObject;
    private readonly List<GameObject> _listOfPrefabs = new List<GameObject>();

    private void SetIsActiveIndex(int index)
    {
        var maxListIndex = _listOfPrefabs.Count;

        if (index > maxListIndex)
        {
            activeIndex = maxListIndex;
        } else if (index < 0)
        {
            activeIndex = 0;
        }
        else
        {
            activeIndex = index;
        }

        for (var i = 0; i < maxListIndex; i++)
        {
            _listOfPrefabs[i].SetActive(i == activeIndex);
        }
    }

    private void Start()
    {
        if (targetGameObject == null) return;

        foreach (Transform child in targetGameObject.transform)
        {
            _listOfPrefabs.Add(child.gameObject);
        }

        SetIsActiveIndex(randomizeActivePrefab ? new Random().Next(0, _listOfPrefabs.Count -1) : activeIndex);
    }
}
