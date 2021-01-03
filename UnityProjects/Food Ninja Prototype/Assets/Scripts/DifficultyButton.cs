using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty = 1;
    private Button _button;
    private GameManager _gameManager;

    private void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
