using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizePrefab : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject activeGameObject;
    private List<GameObject> _prefabs;

    private static List<GameObject> PopulateListOfPrefabs(IEnumerable<GameObject> targets)
    {
        var allTargetTransforms = targets.SelectMany(targetGameObject => targetGameObject.transform.Cast<Transform>());
        return allTargetTransforms.Select(child => child.gameObject).ToList();
    }

    private void Start()
    {
        _prefabs = PopulateListOfPrefabs(targets);
        var randomIndex = Random.Range(0, _prefabs.Count);

        for (var i = 0; i < _prefabs.Count; i++)
        {
            var prefab = _prefabs[i];
            prefab.gameObject.SetActive(i == randomIndex);

            if (i == randomIndex)
                activeGameObject = prefab;
        }
    }
}
