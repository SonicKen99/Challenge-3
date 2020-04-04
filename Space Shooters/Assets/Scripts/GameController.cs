using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text WinText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        WinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene("Space Shooter Challenge");
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                /* bool flag = (Random.value > 0.5f);
                  if (flag)
                  {
                  
                  spawnValues.z (16 or -16)

                  }
                 */
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                //GameObject clone = 
                    Instantiate(hazard, spawnPosition, spawnRotation);
                //ReverseDirection(clone);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'K' for Restart";
                restart = true;
                break;
            }
        }
    }

    /*void ReverseDirection(GameObject clone)
    {
        clone.transform.rotation.y = 0;
        clone.GetComponent<Mover> ().speed = 5;
    }
    */
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            WinText.text = "You Win! Game Created By Kendrick Rivera.";
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        gameOver = true;
    }
}
