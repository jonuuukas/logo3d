using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct CustomCommand
{
    public string name;
    public string action;
    public bool hasParam;
    public string paramName;

    public CustomCommand(string Name, string Action, bool Param, string ParamName)
    {
        name = Name;
        action = Action;
        hasParam = Param;
        if (hasParam)
            paramName = ParamName;
        else
            paramName = "null";
    }
}

public class InputParser : CommandList {

    //Gets the line from the Terminal in GameView, passes it to parse and switch statement does
    //Dirty work

    List<CustomCommand> extraCmds = new List<CustomCommand>();
	public void CmdExecutor(string line){
		
		string[] cmdList;
		Parser (line, out cmdList);
        string[] argList;
        string[] listedCmdList;

		for (int i = 0; i < cmdList.Length ; i+=1) {
			var cmd = cmdList [i];
            Debug.Log(cmd + cmdList[i+1]);
			var temp = 0.0f;
			//Using this to try and get the string as float needed for most commands
			try {
                if (!cmdList[i + 1].Contains(","))
                {
                    float.TryParse(cmdList[i + 1], out temp);
                }
                else
                {
                    argList = cmdList[i + 1].Split(',');
                    listedCmdList = new string[argList.Length];
                    for(int j = 0; j < argList.Length; j++)
                    {
                        for(int k = 0; k < cmdList.Length; k++)
                        {
                            if (k != i + 1)
                                listedCmdList[j] += cmdList[k] + " ";
                            else {

                                listedCmdList[j] += argList[j] + " ";
                            }
                        }
                    }
                    for (int j = 0; j < listedCmdList.Length; j++)
                    {
                        CmdExecutor(listedCmdList[j].Trim());
                    }
                    break;
                }

			} catch (System.Exception ex) {
                if (cmdList[i + 1].Contains(","))
                {
                    argList = cmdList[i + 1].Split(',');
                }
				Debug.Log ("no parameter given " + ex);
			}
			switch (cmd)
			{
			case "pirmyn":
			case "pn":
				MoveForward (Mathf.FloorToInt(temp));
				i++;
				break;
			case "atgal":
			case "al":
				MoveBackwards (Mathf.FloorToInt(temp));
				i++;
				break;
			case "kairen":
			case "kn":
				RotateLeft (Mathf.FloorToInt(temp));
				i++;
				break;
			case "desinen":
			case "dn":
				RotateRight (Mathf.FloorToInt(temp));
				i++;
				break;
            case "aukstyn":
            case "an":
            case "up":
                MoveUp(Mathf.FloorToInt(temp));
                i++;
                break;
            case "zemyn":
            case "zn":
            case "down":
                MoveDown(Mathf.FloorToInt(temp));
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
            case "spalva":
                    ChangeColor(cmdList[i+1]);
                    i++;
                    break;
            case "spalvink":
                    FillInside(cmdList [i + 1]);
                    i++;
                    break;
            case "trintukas":
            case "eraser":
                    ConfigurationManager.paintMode = ConfigurationManager.PaintMode.Erase;
                    break;
            case "piestukas":
            case "pieštukas":
            case "pen":
                    ConfigurationManager.paintMode = ConfigurationManager.PaintMode.Paint;
                    break;
            case "hover":
            case "sklandyk":
                    ConfigurationManager.paintMode = ConfigurationManager.PaintMode.Hover;
                    break;
            case "delete":
            case "trink":
                    DeleteObject();
                    break;
            case "tai":
                    if (CheckEndingOfFunction(line) && !CheckForParameter(cmdList[i+2]))
                    {
                        int j = 2;
                        string actionTemp = "";
                        while (cmdList[i+j] != "taškas" || cmdList[i + j] != "taskas" || cmdList[i + j] != "dot")
                        {
                            if (cmdList[i + j] == "taskas" || cmdList[i + j] == "dot" || cmdList[i + j] == "taškas")
                                break;
                            if (cmdList[i + j] == "kartok" || cmdList[i + j] == "kartot" || cmdList[i + j] == "repeat")
                                cmdList[i + j + 2] = "[" + cmdList[i + j + 2] + "]";
                            actionTemp +=  cmdList[i + j] + " ";
                            Debug.Log(cmdList[i + j]);
                            j++;
                        }   
                        AddNewCommand(cmdList[i + 1], actionTemp);
                        Debug.Log(cmdList[i + 1] + " is Name");
                        Debug.Log(actionTemp + " is the action");
                        i += j;
                    }
                    else if (CheckEndingOfFunction(line) && CheckForParameter(cmdList[i + 2]))
                    {
                        int j = 3;
                        string actionTemp = "";
                        while (cmdList[i + j] != "taškas" || cmdList[i + j] != "taskas" || cmdList[i + j] != "dot")
                        {
                            if (cmdList[i + j] == "taskas" || cmdList[i + j] == "dot" || cmdList[i + j] == "taškas")
                                break;
                            if (cmdList[i + j] == "kartok" || cmdList[i + j] == "kartot" || cmdList[i + j] == "repeat")
                                cmdList[i + j + 2] = "[" + cmdList[i + j + 2] + "]";
                            actionTemp += cmdList[i + j] + " ";
                            Debug.Log(cmdList[i + j]);
                            j++;
                        }
                        AddNewCommand(cmdList[i + 1], actionTemp, cmdList[i + 2]);
                        i += j;
                    }
                    break;
            default:
                for (int j = 0; j<extraCmds.Count; j++)
                    {
                        if (extraCmds[j].name == cmd && !extraCmds[j].hasParam)
                        {
                            CmdExecutor(extraCmds[j].action);
                            break;
                        }
                        else if (extraCmds[j].name == cmd && extraCmds[j].hasParam)
                        {
                            CmdExecutor(extraCmds[j].action.Replace(extraCmds[j].paramName, cmdList[i + 1]));
                            i++;
                            break;
                        }
                    }
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

    void AddNewCommand(string name, string action)
    {
        CustomCommand temp = new CustomCommand(name, action, false, "");
        extraCmds.Add(temp);
    }
    void AddNewCommand(string name, string action, string paramName)
    {
        extraCmds.Add(new CustomCommand(name, action, true, paramName));
    }
    bool CheckForParameter(string var)
    {
        Debug.Log(var);
        if (var[0] == ':')
            return true;
        else
            return false;
    }
    bool CheckEndingOfFunction(string var)
    {
        if (var.Contains("taškas") || var.Contains("taskas") || var.Contains("dot"))
            return true;
        else
            return false;
    }
}