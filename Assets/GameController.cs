using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject enemy;
    public Vector3 spawnValues;
    public Vector3 enemySpawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private bool wonGame;
    private int score;

    void Start ()
    {
        gameOver = false;
		wonGame = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves ());
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
			
			if(score >= 50) {
				Vector3 spawnPosition = new Vector3 (enemySpawnValues.x, enemySpawnValues.y, enemySpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
				Instantiate (enemy, spawnPosition, spawnRotation);
			}

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }

			if (wonGame)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

	 public void WinGame ()
    {
        gameOverText.text = "You won!";
        wonGame = true;
    }
}