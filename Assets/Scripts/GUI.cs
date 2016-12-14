using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

    private static GUI instance;
    public static GUI Instance { get { return instance; } }

    public GameObject PlayersPointsPanel;
    public Text PlayerPoints;
    public Text RoundText;
    public Text pointsToWinText;

    private Snake[] players;
    private List<Text> texts = new List<Text>();

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        RoundText.text = "<i>ROUND</i>  " + GameManager.roundNumber;
        pointsToWinText.text = "<size=130>" + GameManager.pointsToWin + "</size>       Points To Win";

        players = FindObjectsOfType<Snake>();  
                     
        foreach (Snake player in players)
        {
            Text text = Instantiate(PlayerPoints, Vector3.zero, Quaternion.identity, PlayersPointsPanel.transform);
            text.transform.localScale = new Vector3(1, 1, 1);
            text.text = "<i>" + player.PlayerName + "</i>     " + player.GetComponentInParent<Player>()._points + "Pts";
            texts.Add(text);
        }

        StartCoroutine(UpdatePlayersPoints());
    }

    public void ChangeRoundText(int roundNumber)
    {
        RoundText.text = "<i>ROUND</i>  " + roundNumber;
    }
	
    public IEnumerator UpdatePlayersPoints()
    {
        yield return new WaitForSeconds(0.00000000000000000001f);

        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].text = "<i>" + players[i].PlayerName + "</i>     " + players[i].GetComponentInParent<Player>()._points + "Pts";
        }
    }

}
