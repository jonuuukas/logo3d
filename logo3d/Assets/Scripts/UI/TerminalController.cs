﻿using UnityEngine;
using UnityEngine.UI;

public class TerminalController : MonoBehaviour {

    
	
	public InputField terminal;
    public InputField multiTerminal;

	public int commCount = 10; //sets the memorized command for up arrow count

    public Text history1;
    public Text history2;
    public Text history3;
    public Text history4;
    public Text history5;

	public static CircularBuffer lastComm; //class found in Utils folder

    InputParser parser;

	//static string temp;

	// Use this for initialization
	void Start () {
		lastComm = new CircularBuffer(commCount);
		parser = new InputParser ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			terminal.text = lastComm.Peek ("up");
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			terminal.text = lastComm.Peek ("down");
		}
		if (Input.GetKeyDown (KeyCode.Return) && terminal.isFocused)
			sendInput ();
	}

	public void sendInput(){
		var temp = terminal.text; // clearing and using shorter var for convenience
		terminal.text = "";
		parser.CmdExecutor (temp);
		terminal.ActivateInputField ();
		if (temp != "") {
			lastComm.Push (temp);
		}
        setHistory();
    }
    public void sendMultiInput()
    {
        var temp = multiTerminal.text.Split('\n');
        multiTerminal.text = "";
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] != "")
            {
                parser.CmdExecutor(temp[i]);
                lastComm.Push(temp[i]);
            }
        }
        setHistory();
    }
    public void setHistory()
    {
        var temp = lastComm.GetLastFive();
        history1.text = temp[4];
        history2.text = temp[3];
        history3.text = temp[2];
        history4.text = temp[1];
        history5.text = temp[0];
    }

}

