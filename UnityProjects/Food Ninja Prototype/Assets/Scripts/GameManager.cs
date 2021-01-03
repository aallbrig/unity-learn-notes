using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver;

    [Header("List of prefabs 'targets' to periodically spawn")]
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameHud;

    [Header("Spawn rate in seconds")]
    [SerializeField]
    private float spawnRate = 1;
    private int _score;

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;

        gameOverScreen.SetActive(false);
        titleScreen.SetActive(false);
        gameHud.SetActive(true);
    
        gameOver = false;
        _score = 0;

        StartCoroutine(SpawnTarget());
    }

    public void UpdateScore(int score)
    {
        _score += score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    
        StopCoroutine(SpawnTarget());
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void RenderScoreText()
    {
        scoreText.text = "Score: " + _score;
    }

    private IEnumerator SpawnTarget()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(targets[Random.Range(0, targets.Count)]);
        }
    }

    private void Update()
    {
        RenderScoreText();
    }
}
