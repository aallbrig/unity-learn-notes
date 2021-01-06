using UnityEngine;

public class Counter : MonoBehaviour
{
    public delegate void TriggerAction();
    public static event TriggerAction OnObjectInBox;
    private void OnTriggerEnter(Collider other)
    {
        OnObjectInBox?.Invoke();
    }
}
