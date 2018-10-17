using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject asteroid;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;


	// Use this for initialization
	void Start () {
        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        BeginGame();
	}
	
	// Update is called once per frame
	void Update () {
        // Quit if player presses escape
        if (Input.GetKey("escape"))
            Application.Quit();
	}

    void BeginGame() {
        score = 0;
        lives = 3;
        wave = 1;

        //prepare the hud
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HISCORE:" + hiscore;
        livesText.text = "LIVES:" + lives;
        waveText.text = "WAVE:" + wave;

        SpawnAsteroids();
    }

    void SpawnAsteroids() {

        int i = 0; 
        DestroyExistingAsteroids();

        //decide how many asteroids to spawn
        //if any asteroids are left, substract them
        asteroidsRemaining = (wave * increaseEachWave);

        while (i < asteroidsRemaining) {
            // spawn an asteroid
            Instantiate(asteroid,
                new Vector3(Random.Range(-9.0f, 9.0f),
                            Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
        }
        waveText.text = "WAVE:" + wave;
    }

    public void IncrementScore() {
        score++;

        scoreText.text = "SCORE:" + score;

        if (score > hiscore) {
            hiscore = score;
            hiscoreText.text = "HISCORE:" + hiscore;

            //save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        if (asteroidsRemaining < 1) {
            wave++;
            SpawnAsteroids();
        }
    }

    public void DecrementLives() {
        lives--;
        livesText.text = "LIVES:" + lives;

        if (lives < 1)
            BeginGame();
    }

    public void DecrementAsteroids() {
        asteroidsRemaining--;
    }

    public void SplitAsteroid() {
        asteroidsRemaining += 2;
    }

    void DestroyExistingAsteroids() {

        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("Large Asteroid");
        foreach (GameObject current in asteroids)
            GameObject.Destroy(current);

        GameObject[] asteroids2 =
            GameObject.FindGameObjectsWithTag("Small Asteroid");
        foreach (GameObject current in asteroids2)
            GameObject.Destroy(current);
    }

}
