using UnityEngine;
using System.Collections;

public class CommandList : MonoBehaviour
{
    /// <summary>
    /// Class to store the commands that can be accessed by both buttons and the input parser
    /// Currently receives the player gameObject through public declaration
    /// </summary>

    GameObject obj;

    enum Direction { N, NE, E, SE, S, SW, W, NW };
    Direction movementDir;

    void Start()
    {
        movementDir = Direction.N;
        ToHome();
    }

    public void MoveForward(int val)
    {
        if (val != 0)
        {
            obj = GameObject.FindGameObjectWithTag("Player");
            var dest = GridManager.getPosition(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z);
            //var dest = obj.transform.forward * (val);
            //if N// do other sides depending on the enum
            switch (movementDir)
            {
                case Direction.N:
                    if (GridManager.currentPos.z + val >= ConfigurationManager.size)
                        break; //insert more error handling plz
                    dest = GridManager.getPosition(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z + val);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if(ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z + i);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z + i);
                        }
                    }
                    GridManager.currentPos.z += val;
                    break;
                case Direction.NE:
                    if (GridManager.currentPos.x + val >= ConfigurationManager.size || GridManager.currentPos.z + val >= ConfigurationManager.size)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x + val, GridManager.currentPos.y, GridManager.currentPos.z + val);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover) { 
                            for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x + i, GridManager.currentPos.y, GridManager.currentPos.z + i);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x + i, GridManager.currentPos.y, GridManager.currentPos.z + i);
                        }
                    }
                    GridManager.currentPos.x += val;
                    GridManager.currentPos.z += val;
                    break;
                case Direction.E:
                    if (GridManager.currentPos.x + val >= ConfigurationManager.size)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x + val, GridManager.currentPos.y, GridManager.currentPos.z);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x + i, GridManager.currentPos.y, GridManager.currentPos.z);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x + i, GridManager.currentPos.y, GridManager.currentPos.z);

                        }
                    }
                    GridManager.currentPos.x += val;
                    break;
                case Direction.SE:
                    if (GridManager.currentPos.x + val >= ConfigurationManager.size || GridManager.currentPos.z - val < 0)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x + val, GridManager.currentPos.y , GridManager.currentPos.z - val);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x + i, GridManager.currentPos.y, GridManager.currentPos.z - i);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x + i, GridManager.currentPos.y, GridManager.currentPos.z - i);
                        }
                    }
                    GridManager.currentPos.x += val;
                    GridManager.currentPos.z -= val;
                    break;
                case Direction.S:
                    if (GridManager.currentPos.z - val < 0)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z - val);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z - i);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z - i);
                        }
                    }
                    GridManager.currentPos.z -= val;
                    break;
                case Direction.SW:
                    if (GridManager.currentPos.x - val < 0 || GridManager.currentPos.z - val < 0)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x - val, GridManager.currentPos.y, GridManager.currentPos.z - val);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x - i, GridManager.currentPos.y, GridManager.currentPos.z - i);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x - i, GridManager.currentPos.y, GridManager.currentPos.z - i);                    
                        }
                    }
                    GridManager.currentPos.x -= val;
                    GridManager.currentPos.z -= val;
                    break;
                case Direction.W:
                    if (GridManager.currentPos.x - val < 0)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x - val, GridManager.currentPos.y, GridManager.currentPos.z);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x - i, GridManager.currentPos.y, GridManager.currentPos.z);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x - i, GridManager.currentPos.y, GridManager.currentPos.z);
                        }
                    }
                    GridManager.currentPos.x -= val;
                    break;
                case Direction.NW:
                    if (GridManager.currentPos.x - val < 0 || GridManager.currentPos.z + val >= ConfigurationManager.size)
                        break;
                    dest = GridManager.getPosition(GridManager.currentPos.x - val, GridManager.currentPos.y, GridManager.currentPos.z + val);
                    if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
                    {
                        for (int i = 0; i < val; i++)
                        {
                            if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                                GridManager.paintCell(GridManager.currentPos.x - i, GridManager.currentPos.y, GridManager.currentPos.z + i);
                            else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                                GridManager.deleteCell(GridManager.currentPos.x - i, GridManager.currentPos.y, GridManager.currentPos.z + i);
                        }
                    }
                    GridManager.currentPos.x -= val;
                    GridManager.currentPos.z += val;
                    break;
                default:
                    //dest = GridManager.getPosition(GridManager.currentPos.x, GridManager.currentPos.y);
                    Debug.Log("something is not right with me");
                    break;
            }
            obj.transform.position = dest;

        }
    }

    public void MoveBackwards(int val)
    {
        if (val != 0)
        {
            
            ReverseDirection(ref movementDir);
            MoveForward(val);
            ReverseDirection(ref movementDir);
        }
    }

    public void RotateLeft(int val)
    {
        if (val != 0)
        {
            obj = GameObject.FindGameObjectWithTag("Player");
            for (int i = 0; i < val; i++)
            {
                obj.transform.Rotate(new Vector3(0, -45, 0));
                IterateDirectionLeft(ref movementDir);
            }

        }
    }

    public void RotateRight(int val)
    {
        if (val != 0)
        {
            obj = GameObject.FindGameObjectWithTag("Player");
            for (int i = 0; i < val; i++)
            {
                obj.transform.Rotate(new Vector3(0, 45, 0));
                IterateDirectionRight(ref movementDir);
            }
        }
    }

    public void MoveUp(int val)
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        var dest = GridManager.getPosition(GridManager.currentPos.x, GridManager.currentPos.y + val, GridManager.currentPos.z);
        if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
        {
            for (int i = 0; i < val; i++)
            {
                if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                    GridManager.paintCell(GridManager.currentPos.x, GridManager.currentPos.y + i, GridManager.currentPos.z);
                else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                    GridManager.deleteCell(GridManager.currentPos.x, GridManager.currentPos.y + i, GridManager.currentPos.z);
            }
        }
        GridManager.currentPos.y += val;
        obj.transform.position = dest;
    }

    public void MoveDown(int val)
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        var dest = GridManager.getPosition(GridManager.currentPos.x, GridManager.currentPos.y - val, GridManager.currentPos.z);
        if (ConfigurationManager.paintMode != ConfigurationManager.PaintMode.Hover)
        {
            for (int i = 0; i < val; i++)
            {
                if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Paint)
                    GridManager.paintCell(GridManager.currentPos.x, GridManager.currentPos.y - i, GridManager.currentPos.z);
                else if (ConfigurationManager.paintMode == ConfigurationManager.PaintMode.Erase)
                    GridManager.deleteCell(GridManager.currentPos.x, GridManager.currentPos.y - i, GridManager.currentPos.z);
            }
        }
        GridManager.currentPos.y -= val;
        obj.transform.position = dest;
    }

    //===Returns to the starting position and rotation received in Start()===//
    public void ToHome()
    {

        obj = GameObject.FindGameObjectWithTag("Player");
        obj.transform.position = GridManager.getPosition(GridManager.startingPos.x, GridManager.startingPos.y, GridManager.startingPos.z);
        GridManager.currentPos = GridManager.startingPos;
        obj.transform.rotation = new Quaternion(0, 0, 0, 0);
        movementDir = Direction.N;
    }
    public void DeleteCube()
    {
        GridManager.deleteCell(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z);
    }
    public void FillInside(string var)
    {
        var temp = ConfigurationManager.currColor;
        ChangeColor(var);
        GridManager.floodFill(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z);
        ChangeColor(temp.ToString());
    }
    public void DeleteObject()
    {
        GridManager.floodDelete(GridManager.currentPos.x, GridManager.currentPos.y, GridManager.currentPos.z);
    }

    public void ChangeColor(string var)
    {
        switch (var)
        {
            case "juoda":
            case "black":
                ConfigurationManager.currColor = Color.black;
                break;
            case "zalia":
            case "žalia":
            case "green":
                ConfigurationManager.currColor = Color.green;
                break;
            case "white":
            case "balta":
                ConfigurationManager.currColor = Color.white;
                break;
            case "melyna":
            case "mėlyna":
            case "blue":
                ConfigurationManager.currColor = Color.blue;
                break;
            case "raudona":
            case "red":
                ConfigurationManager.currColor = Color.red;
                break;
            case "geltona":
            case "yellow":
                ConfigurationManager.currColor = Color.yellow;
                break;
            case "pilka":
            case "grey":
            case "gray":
                ConfigurationManager.currColor = Color.grey;
                break;
            case "zydra":
            case "cyan":
            case "žydra":
                ConfigurationManager.currColor = Color.cyan;
                break;
            case "rozine":
            case "rožinė":
            case "magenta":
                ConfigurationManager.currColor = Color.magenta;
                break;
            default:
                ConfigurationManager.currColor = ConfigurationManager.defaultColor;
                break;
        }
    }
    Direction ReverseDirection(ref Direction dir)
    {
        if (dir == Direction.N)
            dir = Direction.S;
        else if (dir == Direction.S)
            dir = Direction.N;
        else if (dir == Direction.E)
            dir = Direction.W;
        else if (dir == Direction.W)
            dir = Direction.E;
        else if (dir == Direction.NE)
            dir = Direction.SW;
        else if (dir == Direction.SW)
            dir = Direction.NE;
        else if (dir == Direction.SE)
            dir = Direction.NW;
        else if (dir == Direction.NW)
            dir = Direction.SE;
        return dir;
    }
    Direction IterateDirectionRight(ref Direction dir)
    {
        if (dir == Direction.N)
            dir = Direction.NE;
        else if (dir == Direction.NE)
            dir = Direction.E;
        else if (dir == Direction.E)
            dir = Direction.SE;
        else if (dir == Direction.SE)
            dir = Direction.S;
        else if (dir == Direction.S)
            dir = Direction.SW;
        else if (dir == Direction.SW)
            dir = Direction.W;
        else if (dir == Direction.W)
            dir = Direction.NW;
        else if (dir == Direction.NW)
            dir = Direction.N;
        return dir;
    }
    Direction IterateDirectionLeft(ref Direction dir)
    {
        if (dir == Direction.N)
            dir = Direction.NW;
        else if (dir == Direction.NW)
            dir = Direction.W;
        else if (dir == Direction.W)
            dir = Direction.SW;
        else if (dir == Direction.SW)
            dir = Direction.S;
        else if (dir == Direction.S)
            dir = Direction.SE;
        else if (dir == Direction.SE)
            dir = Direction.E;
        else if (dir == Direction.E)
            dir = Direction.NE;
        else if (dir == Direction.NE)
            dir = Direction.N;
        return dir;
    }
}