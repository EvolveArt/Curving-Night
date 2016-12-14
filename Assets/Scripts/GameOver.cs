using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    private static GameOver instance;
    public static GameOver Instance { get { return instance; } }

    public GameObject GameOverUI;
    public Text resultText;
    
    private void Awake()
    {
        instance = this;
    }

    public void Restart()
    {
        Points.ResetPoints();
        GameManager.roundNumber = 0;

        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }

    public void Menu()
    {
        Points.ResetPoints();
        GameManager.roundNumber = 0;

        SceneManager.LoadScene( "Menu" );

        if (Time.timeScale < 1)
            Time.timeScale = 1;
    }

    public void DisplayGameOver(Snake winner)
    {
        GameOverUI.SetActive(true);
        resultText.text = winner.PlayerName + " has won !";
    }
}
