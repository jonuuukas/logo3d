using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TerminalController : MonoBehaviour {
	
	public InputField terminal;
	public int commCount = 10; //sets the memorized command for up arrow count

	CircularBuffer lastComm; //class found in Utils folder



	// Use this for initialization
	void Start () {
		lastComm = new CircularBuffer(commCount);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			getLastCommand ();
		}
		if (Input.GetKeyDown (KeyCode.Return))
			sendInput ();
	}

	public void sendInput(){
		var temp = terminal.text; // clearing and using shorter var for convenience
		terminal.text = "";
		terminal.ActivateInputField ();
		if (temp != "") {
			lastComm.Push (temp);
		}
	}
	void getLastCommand(){
		terminal.text = lastComm.Peek ();
	}
}

