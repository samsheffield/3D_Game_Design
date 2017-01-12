using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;           // Current score
    public int goal = 8;        // Score to reach to complete game
    public Text scoreText, winText, loseText;   // UI Text GameObjects
    public AudioSource backgroundMusic, gameStateSounds; // Two AudioSource components. One for background sounds (not currently implemented), one for state changes like winning or losing
    public AudioClip winClip, loseClip; // AudioClips for win and lose state

    private bool gameOver;      // Has the game been completed?
    private float gameOverDelay = 5f;

    void Start()
    {
        score = 0;
        gameOver = false;
    }

    // Method used by other scripts to increase score and handle win/lose conditions
    public void Score(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString("00"); // "00" Formats the score to two digits when displayed

        // Only allow winning if the goal is achieved and the game has not already ended (if you've lost, for instance!)
        if(score >= goal && !gameOver)
        {
            Win();
        }
    }

    // Method used to handle the win state
    private void Win()
    {
        // End the game if it hasn't already been ended. This ensures that the sounds and coroutines are not run multiple times
        if (!gameOver)
        {
            gameOver = true;
            winText.gameObject.SetActive(true);         // Enable the currently disabled win text.
            gameStateSounds.PlayOneShot(winClip);
            StartCoroutine(GameOver());
        }
        
    }

    // Method used to handle the lose state. Called by other scripts.
    public void Lose()
    {
        // End the game if it hasn't already been ended. This ensures that the sounds and coroutines are not run multiple times
        if (!gameOver)
        {
            gameOver = true;
            loseText.gameObject.SetActive(true);         // Enable the currently disabled win text.
            gameStateSounds.PlayOneShot(loseClip);
            StartCoroutine(GameOver());
        }

    }

    // Handles a brief delay before restarting level
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);     // Delay before resetting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Restart current level. Be sure to add: using UnityEngine.SceneManagement; at top
    }

}
