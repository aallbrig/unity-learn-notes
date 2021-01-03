using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("List of prefabs 'targets' to periodically spawn")]
    [SerializeField]
    private List<GameObject> targets;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Spawn rate in seconds")]
    [SerializeField]
    private float spawnRate = 1;
    private int _score;

    public void UpdateScore(int score)
    {
        _score += score;
    }

    private void RenderScoreText()
    {
        scoreText.text = "Score: " + _score;
    }

    private IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(targets[Random.Range(0, targets.Count)]);
        }
    }
    
    private void Start()
    {
        _score = 0;

        StartCoroutine(SpawnTarget());
    }

    private void Update()
    {
        RenderScoreText();
    }
}
