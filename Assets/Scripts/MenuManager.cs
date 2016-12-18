using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private int numberOfPlayers = 2;
    public Transform panel;
    public GameObject playerPanel;
    public string GameSceneName = "Curve Night";

    private List<GameObject> playerPanels = new List<GameObject>();
    public static List<Color32> playerColors = new List<Color32>();
    public static List<KeyCode> leftPlayerCommands = new List<KeyCode>();
    public static List<KeyCode> rightPlayerCommands = new List<KeyCode>();


    void Start()
    {
        numberOfPlayers = RandomSpawn.players;
        SpawnPanels();
    }

    void SpawnPanels()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject go = Instantiate(playerPanel, panel);
            go.transform.localScale = new Vector3(1f, 1f, 1f);

            Text playerName = go.transform.GetChild(1).GetComponent<Text>();
            playerName.text = "Player " + ( i+1 ).ToString();
            playerPanels.Add(go);

            Commands cmd = go.GetComponentInChildren<Commands>();
            switch (i)
            {
                case 0:
                    cmd.leftCommand = KeyCode.LeftArrow;
                    cmd.rightCommand = KeyCode.RightArrow;
                    break;
                case 1:
                    cmd.leftCommand = KeyCode.A;
                    cmd.rightCommand = KeyCode.E;
                    break;
                case 2:
                    cmd.leftCommand = KeyCode.KeypadDivide;
                    cmd.rightCommand = KeyCode.KeypadMinus;
                    break;
                case 3:
                    cmd.leftCommand = KeyCode.Mouse0;
                    cmd.rightCommand = KeyCode.Mouse1;
                    break;

                default:
                    cmd.leftCommand = KeyCode.LeftArrow;
                    cmd.rightCommand = KeyCode.RightArrow;
                    break;
            }
        }
    }

    public void StartGame()
    {
        // Apply color for each player
        for (int i = 0; i < playerPanels.Count; i++)
        {
            ColorPicker colorPicker = playerPanels[i].GetComponentInChildren<ColorPicker>();
            playerColors.Add(colorPicker.playerColor);

            //Change commands for each player
            Commands commands = playerPanels[i].GetComponentInChildren<Commands>();
            leftPlayerCommands.Add(commands.leftCommand);
            rightPlayerCommands.Add(commands.rightCommand);
        }

        SceneManager.LoadScene( GameSceneName );
    }
}
