using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private GameSceneController gameSceneController;
	#region Field Declarations

	[Header("UI Components")]
    [Space]
	public Text scoreText;
    public StatusText statusText;
    public Button restartButton;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;

    #endregion

    #region Startup

    private void Awake()
    {
        statusText.gameObject.SetActive(false);
    }

    #endregion

    #region Public methods

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D5");
    }

    public void ShowStatus(string newStatus)
    {
        statusText.gameObject.SetActive(true);
        StartCoroutine(statusText.ChangeStatus(newStatus));
    }

    public void HideShip(int imageIndex)
    {
        shipImages[imageIndex].gameObject.SetActive(false);
    }

    public void ResetShips()
    {
        foreach (Image ship in shipImages)
            ship.gameObject.SetActive(true);
    }

    #endregion

    private void Start()
    {
        gameSceneController = FindObjectOfType<GameSceneController>();
        gameSceneController.ScoreUpdatedOnKill += GameSceneControllerOnScoreUpdatedOnKill;
    }

    private void GameSceneControllerOnScoreUpdatedOnKill(int points)
    {
        UpdateScore(points);
    }
}
