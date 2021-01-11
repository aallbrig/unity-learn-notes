using UnityEngine;
using UnityEngine.UI;

public class DisableWhilePlaying : MonoBehaviour
{
    private Button _button;

    private void DisableButton() => _button.interactable = false;
    private void ReenableButton() => _button.interactable = true;

    private void Start()
    {
        _button = GetComponent<Button>();

        CommandManager.OnPlaying += DisableButton;
        CommandManager.OnPlayingComplete += ReenableButton;
    }
}
