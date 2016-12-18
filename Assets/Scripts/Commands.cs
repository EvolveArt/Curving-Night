using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commands : MonoBehaviour {

    public KeyCode leftCommand;
    public KeyCode rightCommand;
    private float detectingTime = 5f;
    private bool lDetecting;
    private bool rDetecting;

    KeyCode keyPressed;

    public Text lcmdText;
    public Text rcmdText;
    
    void Start()
    {
        lDetecting = false;
        rDetecting = false;
    }

    void Update()
    {
        if(lDetecting)
        {
            DetectKey(0);
        }

        if(rDetecting)
        {
            DetectKey(1);
        }

        lcmdText.text = leftCommand.ToString();
        rcmdText.text = rightCommand.ToString();
    }

    public void DetectLeftCommand()
    {
        lDetecting = true;
        Debug.Log("Waiting for a left key..");
    }

    public void DetectRightCommand()
    {
        rDetecting = true;
        Debug.Log("Waiting for a right key..");
    }

    void DetectKey(int command)
    {
        foreach (KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vkey))
            {
                keyPressed = vkey;
                if (command == 0)
                {
                    leftCommand = keyPressed;
                    lDetecting = false;
                }
                else
                {
                    rightCommand = keyPressed;
                    rDetecting = false;
                }
                Debug.Log("Key detected");
            }
        }
    }
}
