using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    Snake[] snakes = new Snake[0];
    int snakesAlive = 0;
    Snake Winner = new Snake();

    float roundOverTime = 3f;
    bool roundOver = false;
    bool gameFinished = false;

    public Text winText;
    public GameObject winSpace;

    public static int pointsToWin = 25;
    private int playerDeadRank;

    [HideInInspector]
    public int numberOfPlayers;

    public static int roundNumber = 1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerDeadRank = 0;

        snakes = FindObjectsOfType(typeof(Snake)) as Snake[];

        foreach (Snake snake in snakes) {
            if (!snake.isDead)
                snakesAlive++;
        }

        numberOfPlayers = snakesAlive;

        Debug.Log(numberOfPlayers + " joueur(s)");
    }

    void Update()
    {
        if (snakesAlive <= 1 &&  !roundOver)
        {
            foreach (Snake snake in snakes)
            {
                if (!snake.isDead)
                    snake.GetComponentInParent<Player>().AddPoints(numberOfPlayers);
            }

            StartCoroutine(EndRound());
        }
    }

    public void KillPlayer (GameObject go)
    {

        Debug.Log("Player Killed");
        Tail tail = go.GetComponentInParent<Tail>();
        Snake snake = go.GetComponent<Snake>();
        Player player = go.GetComponentInParent<Player>();

        StartCoroutine(GUI.Instance.UpdatePlayersPoints());

        if (snake.GetComponent<Snake>() != null && !tail.invicible)
        {
            playerDeadRank++;
            snakesAlive--; 
            if(!roundOver)
                player.AddPoints(playerDeadRank);
            snake.isDead = true;
        }


    }

    IEnumerator  EndRound()
    {
        Debug.Log("Round Over !");
        roundOver = true;


        foreach (Snake snake in snakes)
        {

            if (!snake.isDead)
                Winner = snake;

            Player player = snake.GetComponentInParent<Player>();

            bool end = player.Win();
            if (end)
                gameFinished = true;
        }

        if(!gameFinished)
        {
            DisplayEndRound(Winner);
            yield return new WaitForSeconds(roundOverTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            roundNumber++;
            Debug.Log("Starting Round " + roundNumber); //TODO : Animation on Screen
            GUI.Instance.ChangeRoundText(roundNumber);
        }

        
        yield return false;
    }

    void DisplayEndRound(Snake winner)
    {
        winText.text = winner.PlayerName + " wins !";
        winSpace.SetActive(true);

        Debug.Log(winner.PlayerName + " wins !");
    }

    public void EndTheGame(Player winner)
    {
        // Afficher Ecran Game Over (DONE)
        Debug.Log("Game finished !" + winner.name);
        GameOver.Instance.DisplayGameOver(winner.GetComponentInChildren<Snake>());
        StartCoroutine( GUI.Instance.UpdatePlayersPoints());
    }

}
