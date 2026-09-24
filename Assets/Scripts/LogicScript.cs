using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    // Game state flag
    public bool gameIsOver = false;

    // Audio clips
    public AudioClip flapClip;
    public AudioClip pointClip;
    public AudioClip hitClip;
    public AudioClip dieClip;
    public AudioClip swooshClip;

    /// Play sound from Main Camera
    public void PlayClip(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource camAudio = Camera.main?.GetComponent<AudioSource>();
        if (camAudio != null)
            camAudio.PlayOneShot(clip);
    }

    public void addScore(int scoreToAdd)
    {
        if (gameIsOver) return;   // stop scoring after death

        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void gameOver()
    {
        gameIsOver = true;        //  THIS IS THE KEY LINE
        gameOverScreen.SetActive(true);
    }

    public void restartGame()
    {
        gameIsOver = false;       // reset flag
        PlayClip(swooshClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
