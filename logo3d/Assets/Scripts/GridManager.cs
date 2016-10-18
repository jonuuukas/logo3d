using UnityEngine;

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
    public Vector3 position;
    public bool isEmpty;
    GameObject CellFill;

    public GridCell(float x, float y, float z)
    {
        position = new Vector3(x, y, z);
        isEmpty = true;
        CellFill = null;
    }
    public void SpawnCube()
    {
        CellFill = GameObject.CreatePrimitive(PrimitiveType.Cube);
        isEmpty = false;
        CellFill.transform.position = position;
        CellFill.gameObject.GetComponent<Renderer>().material.color = ConfigurationManager.currColor;
    }
}

  
public class GridManager : MonoBehaviour {

    public static PositionIndexes startingPos;
    public static PositionIndexes currentPos;
    
    static GridCell[,] grid;
	// Use this for initialization
	void Start () {
        startingPos = new PositionIndexes(300, 0, 300);
        currentPos = new PositionIndexes(300, 0, 300);
        grid = new GridCell[600,600];
        createGrid();
	}
	
    void createGrid()
    {
        for (int i = 0; i<600; i++)
        {
            for (int j = 0; j<600; j++)
            {
                grid[i, j] = new GridCell(i, 0, j);
            }
        }
    }
    public static Vector3 getPosition(int i, int j)
    {
        return grid[i, j].position;
    }
    
    public static void paintCell(int i, int j)
    {
        if (grid[i,j].isEmpty)
            grid[i, j].SpawnCube();
    }
}
