using UnityEngine;
using UnityEngine.UI;

public class DisableOnRewind : MonoBehaviour
{
    private Button _btn;

    private void Start()
    {
        _btn = GetComponent<Button>();

        CommandManager.OnRewindStart += () => _btn.interactable = false;
        CommandManager.OnRewindComplete += () => _btn.interactable = true;
    }
}
