using UnityEngine;
using System.Collections;

public struct PositionIndexes
{
    public int x;
    public int y;
    public int z;

    public PositionIndexes(int X, int Y, int Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}
public struct GridCell
{
    public bool isEmpty;

    public void SpawnCube(int x, int y, int z)
    {
        GameObject CellFill = GameObject.CreatePrimitive(PrimitiveType.Cube);
        isEmpty = false;
        CellFill.transform.position = new Vector3(x,y,z);
        CellFill.gameObject.GetComponent<Renderer>().material.color = ConfigurationManager.currColor;
    }
}

  
public class GridManager : MonoBehaviour {

    public static PositionIndexes startingPos;
    public static PositionIndexes currentPos;
    
    static GridCell[,,] grid;
	// Use this for initialization
	void Start () {
        startingPos = new PositionIndexes(150, 0, 150);
        currentPos = new PositionIndexes(150, 0, 150);
        grid = new GridCell[300,300,300];
        createGrid();
	}
	
    void createGrid()
    {
        for (int i = 0; i<300; i++)
        {
            for (int j = 0; j<300; j++)
            {
                for (int k = 0; k < 300; k++)
                {
                    grid[i, j, k] = new GridCell();
                    grid[i, j, k].isEmpty = true;
                }
            }
        }
    }
    public static Vector3 getPosition(int i, int j, int k)
    {
        return new Vector3(i,j, k);
    }
    
    public static void paintCell(int i, int j, int k)
    {
        filler();
        if (grid[i, j, k].isEmpty)
            grid[i, j, k].SpawnCube(i,j,k);
    }
    public static IEnumerator filler()
    {
            yield return new WaitForEndOfFrame();
    }
    public static void floodFill(int i, int j, int k)
    {
        if (!grid[i, j, k].isEmpty)
            return;
        filler();
        grid[i, j, k].SpawnCube(i,j,k);
        if (grid[i - 1, j, k].isEmpty)
            floodFill(i - 1, j, k);
        if (grid[i + 1, j, k].isEmpty)
            floodFill(i + 1, j, k);
        if (grid[i, j, k + 1].isEmpty)
            floodFill(i, j, k + 1);
        if (grid[i, j, k - 1].isEmpty)
            floodFill(i, j, k - 1);
    }
}
