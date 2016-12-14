using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {

    public static List<int> playerPoints = new List<int>();

    private void Start()
    {
        while (playerPoints.Count < GameManager.Instance.numberOfPlayers)
        {
            playerPoints.Add(0);
        }
    }

    public static void ResetPoints()
    {
        playerPoints.Clear();
    }
}
