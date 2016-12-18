using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    int playersSpawned = 0;
    public static int players = 2;

    public static Transform wall;

    public static float mapHeight = 3.7f;
    public static float mapWidth = 3.7f;

    //public GameObject player1Prefab;
    //public GameObject player2Prefab;
    public GameObject playerPrefab;

    public GameObject[] powerups;

    private void Start()
    {
        SpawnPlayers();

        SpawnPowerup();

        wall = GameObject.Find("Wall").transform;
        wall.localScale = Menu.wallScale;
    }

    private void Update()
    {
        int a = Random.Range(0, 1000);
        if (a > 998)
            SpawnPowerup();
    }
    
    public void SpawnPlayers()
    {
        //for (int i = 0; i < players; i++)
        //{
        //    float x = Random.Range(-mapWidth, mapWidth);
        //    float y = Random.Range(-mapHeight, mapHeight);
        //    float zR = Random.Range(0f, 360f);
        //    switch (i)
        //    {
        //        case 0:
        //            playerToSpawn = player1Prefab;
        //            break;
        //        case 1:
        //            playerToSpawn = player2Prefab;
        //            break;
        //        default:
        //            playerToSpawn = player1Prefab;
        //            break;
        //    }
        //    Instantiate(playerToSpawn, new Vector3(x, y, 1), Quaternion.identity);
        //    Snake snake = playerToSpawn.GetComponentInChildren<Snake>();
        //    snake.transform.Rotate(new Vector3(0, 0, zR));
        //}

        for (int i = 0; i < players; i++)
        {
            //if (playersSpawned >= players)
                //return;

            //playersSpawned++;

            float x = Random.Range(-mapWidth + 0.1f, mapWidth - 0.1f);
            float y = Random.Range(-mapHeight + 0.1f, mapHeight - 0.1f);
            float zR = Random.Range(0f, 360f);

            GameObject go = Instantiate(playerPrefab, new Vector3(x, y, 1), Quaternion.identity); /* Instantiate player */
            Snake snake = go.GetComponentInChildren<Snake>();
            snake.transform.Rotate(new Vector3(0, 0, zR));

            //Change each player color
            LineRenderer lineColor = go.GetComponentInChildren<LineRenderer>();
            SpriteRenderer headColor = go.GetComponentInChildren<SpriteRenderer>();
            lineColor.startColor = MenuManager.playerColors[i];
            lineColor.endColor = MenuManager.playerColors[i];
            headColor.color = MenuManager.playerColors[i];

            //Change each player commands
            snake.leftCMD = MenuManager.leftPlayerCommands[i];
            snake.rightCMD = MenuManager.rightPlayerCommands[i];

            //Change each player name
            snake.PlayerName = "Player " + (i+1).ToString();

            //Change each player ID
            go.GetComponentInParent<Player>().playerID = i;
        }

    }

    private void SpawnPowerup()
    {
        for (int i = 0; i < Random.Range(1f, 3f); i++)
        {
            float x = Random.Range(-mapWidth, mapWidth);
            float y = Random.Range(-mapHeight, mapHeight);
            Instantiate(powerups[Random.Range(0, powerups.Length)], new Vector3(x, y, 1), Quaternion.identity);
        }
    }

}
