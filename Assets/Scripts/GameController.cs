using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    public AudioClip musicClipBackground;
    public AudioClip musicClipWin;
    public AudioClip musicClipLoss;
    public AudioSource musicSource;

    public int score;

    private bool gameOver;
    private bool restart;
    

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        musicSource.clip = musicClipBackground;
        musicSource.Play();
        musicSource.loop = true;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene("Space_Shooter_Tutorial");
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
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Tab' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 150)
        {
            gameOverText.text = "You win! Game created by Jamie LaRose";
            gameOver = true;
            restart = true;
            musicSource.clip = musicClipWin;
            musicSource.Play();
            musicSource.loop = true;
            
        }
    }

    public void GameOver()
    {
        gameOverText.text = "You Lose! Game created by Jamie LaRose";
        gameOver = true;
        musicSource.clip = musicClipLoss;
        musicSource.Play();

    }
}