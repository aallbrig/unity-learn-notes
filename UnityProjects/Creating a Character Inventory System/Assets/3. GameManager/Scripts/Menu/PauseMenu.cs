using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button ResumeButton;
    public Button QuitButton;
    public Button RestartButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(HandleResumeClick);
        QuitButton.onClick.AddListener(HandleQuitClick);
        RestartButton.onClick.AddListener(HandleRestartClick);
    }

    private void HandleResumeClick()
    {
        GameManager.Instance.TogglePause();
    }

    private void HandleQuitClick()
    {
        GameManager.Instance.QuitGame();
    }

    private void HandleRestartClick()
    {
        GameManager.Instance.RestartGame();
    }
}
