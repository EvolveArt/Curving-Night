using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public string GameSceneName;

    [Header("Size Panel")]
    public Text mapSize;
    public Slider sizeSlider;
    private int sizeIndex;
    public static Vector2 wallScale = new Vector2(1,1);

    [Header("Players Panel")]
    public Text playersNumber;
    public Slider playersSlider;
    private int numberOfPlayers = 2;

    [Header("Points To Win")]
    public Text PointsToWin;
    public Slider pointsToWinSlider;
    private int pointsToWin = 25;

    public void PointsToWinSlider()
    {
        pointsToWin = (int)pointsToWinSlider.value;
        PointsToWin.text = pointsToWin.ToString();
    }

    public void PlayersSlider()
    {
        numberOfPlayers = (int)playersSlider.value;
        playersNumber.text = numberOfPlayers.ToString();
    }

    public void SizeSlider()
    {
        sizeIndex = (int)sizeSlider.value;

        switch (sizeIndex)
        {
            case 1:
                mapSize.text = "SMALL";
                RandomSpawn.mapHeight = 3.7f;
                RandomSpawn.mapWidth = 3.7f;
                wallScale = new Vector2(1f, 1f);
                break;
            case 2:
                mapSize.text = "MEDIUM";
                RandomSpawn.mapHeight = 5.1f;
                RandomSpawn.mapWidth = 5.1f;
                wallScale = new Vector2(1.4f, 1.4f);
                break;
            case 3:
                mapSize.text = "LARGE";
                RandomSpawn.mapHeight = 6.7f;
                RandomSpawn.mapWidth = 6.7f;
                wallScale = new Vector2(1.8f, 1.8f);
                break;
            default:
                mapSize.text = "NO SIZE";
                RandomSpawn.mapHeight = 6.7f;
                RandomSpawn.mapWidth = 6.7f;
                wallScale = new Vector2(1.8f, 1.8f);
                break;
        }

    }

    public void PlayGame()
    {
        GameManager.pointsToWin = pointsToWin; 
        RandomSpawn.players = numberOfPlayers;
        SceneManager.LoadScene( GameSceneName );
    }
}
