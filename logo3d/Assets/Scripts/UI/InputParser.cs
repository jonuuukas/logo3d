using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputParser : CommandList {

	//Gets the line from the Terminal in GameView, passes it to parse and switch statement does
	//Dirty work
	public void CmdExecutor(string line){
		
		string[] cmdList;
		Parser (line, out cmdList);

		for (int i = 0; i < cmdList.Length ; i+=1) {
			var cmd = cmdList [i];
			var temp = 0.0f;
			//Using this to try and get the string as float needed for most commands
			try {
				float.TryParse (cmdList [i + 1], out temp);	
			} catch (System.Exception ex) {
				Debug.Log ("no parameter given " + ex);
			}
			switch (cmd)
			{
			case "pirmyn":
			case "pn":
				MoveForward (temp);
				i++;
				break;
			case "atgal":
			case "al":
				MoveBackwards (temp);
				i++;
				break;
			case "kairen":
			case "kn":
				RotateLeft (temp);
				i++;
				break;
			case "desinen":
			case "dn":
				RotateRight (temp);
				i++;
				break;
			case "namo":
				ToHome ();
				break;
			case "kartok":
			case "kartot":
				for (int j = 0; j < temp; j++)
					CmdExecutor (cmdList [i + 2]);
				i += 2;
				break;
			default:
				Debug.Log ("komanda nerasta");
				break;
			}
		}
	}
	public void Parser(string line, out string[] cmdList){
		//using list so the size of array could be dynamic
		List<string> cmds = new List<string> ();
		//Let's do this if there is no cycle in the simple way by splitting spaces
		if (!line.Contains ("[")) {
			var temp = line.Split (' ');
			for (int i = 0; i < temp.Length; i++)
				cmds.Add (temp [i]);
		//If there are brackets, it means there is a cycle
		//So let's find out where it starts and ends
		//What happens before and after
		//And merge that stuff into a list that will be used to be made into out string[]
		} else {
			var counter = 0;
			var startCycleIndex = 0;
			var endCycleIndex = 0;
			for (int i = 0; i < line.ToCharArray().Length; i++) {
				if (line.ToCharArray () [i] == ('[')) {
					if (counter == 0)
						startCycleIndex = i;
					counter++;
				}
				if (line.ToCharArray () [i] == (']')) {
					if (counter == 1)
						endCycleIndex = i;
					counter--;
				}
			}
			//Just to be clear we rename stuff
			var tmpBefore = line.Substring (0, startCycleIndex-1).Split (' ');
			var tmpCycle = line.Substring (startCycleIndex + 1, endCycleIndex - startCycleIndex - 1);
			//Here we add stuff to the list
			for (int i = 0; i < tmpBefore.Length; i++) {
				cmds.Add(tmpBefore[i]);
			}
			cmds.Add (tmpCycle);
			//If there is something after the cycle we add this so need checking
			if(endCycleIndex + 1 != line.Length){
				var tmpAfter = line.Substring (endCycleIndex + 2, line.Length - endCycleIndex - 2).Split (' ');
				for (int i = 0; i < tmpAfter.Length; i++) {
					cmds.Add (tmpAfter[i]);
				}
			}
		}
		//Finishing up
		cmdList = cmds.ToArray (); 
	}
}