using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    int playersSpawned = 0;
    public static int players = 2;

    public static Transform wall;

    public static float mapHeight = 3.7f;
    public static float mapWidth = 3.7f;

    //public GameObject player1Prefab;
    //public GameObject player2Prefab;
    public GameObject[] playersPrefab;

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
    
    void SpawnPlayers()
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

        foreach (GameObject player in playersPrefab)
        {

            if (playersSpawned >= players)
                return;

            playersSpawned++;

            float x = Random.Range(-mapWidth + 0.1f, mapWidth - 0.1f);
            float y = Random.Range(-mapHeight + 0.1f, mapHeight - 0.1f);
            float zR = Random.Range(0f, 360f);

            Instantiate(player, new Vector3(x, y, 1), Quaternion.identity); /* Instantiate player */
            Snake snake = player.GetComponentInChildren<Snake>();
            snake.transform.Rotate(new Vector3(0, 0, zR));              
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
