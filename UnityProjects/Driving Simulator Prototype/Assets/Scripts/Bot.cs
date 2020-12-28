using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [Header("Y-up vector (in meters per second)")]
    public Vector3 vector = new Vector3(0, 0, 10);

    [System.Serializable]
    public struct ActivateModel
    {
        public GameObject target;
        public bool active;
    }
    
    public List<ActivateModel> modelsToActivate;

    private void Start()
    {
        modelsToActivate.ForEach((model) => model.target.SetActive(model.active));
    }

    private void Update()
    {
        transform.Translate( vector * Time.deltaTime);
    }
}
