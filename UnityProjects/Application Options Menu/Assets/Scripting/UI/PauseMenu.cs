using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button quit;
    
    private void Start()
    {
        resume.onClick.AddListener(GameManager.Instance.TogglePauseGame);
        restart.onClick.AddListener(GameManager.Instance.RestartGame);
        quit.onClick.AddListener(GameManager.Instance.QuitGame);
    }
}
