using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int playerID;

    private Player[] players;

    public int _points;
    private int pointsWon;

    void Start()
    {
        players = FindObjectsOfType(typeof(Player)) as Player[];

        AssignPoints();

    }

    public void AddPoints(int points)
    {
        pointsWon = points;
        // TODO : Afficher sur le GUI
        Points.playerPoints[playerID] += pointsWon;

        AssignPoints();
    }

    public bool Win()
    {
        foreach (Player p in players)
        {
            if (p != this)
            {
                if (_points >= (p._points + 2))
                {
                    if (_points >= GameManager.Instance.pointsToWin)
                    {
                        GameManager.Instance.EndTheGame(this);
                        return true;
                    }
                }
            }
        }

        AssignPoints();

        return false;   
    }    

    void AssignPoints()
    {
        _points = Points.playerPoints[playerID];
    }
}
