using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resume;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _quit;

    private void Start()
    {
        _resume.onClick.AddListener(GameManager.Instance.TogglePauseGame);
        _restart.onClick.AddListener(GameManager.Instance.RestartGame);
        _quit.onClick.AddListener(GameManager.Instance.QuitGame);
    }
}
