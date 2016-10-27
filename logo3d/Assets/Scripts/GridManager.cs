using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

public class GridManager : MonoBehaviour {

    public static PositionIndexes startingPos;
    public static PositionIndexes currentPos;

    static BitArray new_grid;

    // Use this for initialization
    void Start() {
        //div by 2 so it would be in the middle
        startingPos = new PositionIndexes(ConfigurationManager.size/2, 0, ConfigurationManager.size/2);
        currentPos = new PositionIndexes(ConfigurationManager.size/2, 0, ConfigurationManager.size/2);

        //size*size*size is pow 3 and also method sets all the bits to be empty = 0
        new_grid = new BitArray((int)Mathf.Pow(ConfigurationManager.size,3));
        new_grid.SetAll(false);

    }

    public static Vector3 getPosition(int i, int j, int k)
    {
        return new Vector3(i, j, k);
    }
    //if true the grid part is not empty
    public static bool getBit(int i, int j, int k)
    {
        //f.e: i * 300 * 300 + j * 300 + k
        return new_grid[i * (int)Mathf.Pow(ConfigurationManager.size,2) + j * ConfigurationManager.size + k];
    }

    public static void inverseBit(int i, int j, int k)
    {
        new_grid[i * (int)Mathf.Pow(ConfigurationManager.size, 2) + j * ConfigurationManager.size + k] = !new_grid[i * (int)Mathf.Pow(ConfigurationManager.size, 2) + j * ConfigurationManager.size + k];
    }
    //used for command list to check if empty or not
    public static void paintCell(int i, int j, int k)
    {
        if (!getBit(i, j, k))
            SpawnCube(i, j, k);
    }
    public static void deleteCell(int i, int j, int k)
    {
        if (getBit(i, j, k))
            DeleteCube(i, j, k);
    }
    public static void SpawnCube(int x, int y, int z)
    {
        GameObject CellFill = GameObject.CreatePrimitive(PrimitiveType.Cube);
        inverseBit(x, y, z);
        CellFill.transform.position = new Vector3(x, y, z);
        CellFill.gameObject.GetComponent<Renderer>().material.color = ConfigurationManager.currColor;
    }
    public static void DeleteCube(int x, int y, int z)
    {
        Collider[] coll;
        if ((coll = Physics.OverlapSphere(getPosition(x,y,z), 0.1f)).Length >= 1)
        {
            foreach (var collider in coll)
            {
                //cause collider might pick up ground and player
                if (collider.name == "Cube" || collider.tag == "Spawn")
                    Destroy(collider.gameObject);
            }
            inverseBit(x, y, z);
        }
    }
    public static void floodFill(int i, int j, int k)
    {
        if (getBit(i, j, k))
            return;
        SpawnCube(i, j, k);
        if (!getBit(i - 1, j, k))
            floodFill(i - 1, j, k);
        if (!getBit(i + 1, j, k))
            floodFill(i + 1, j, k);
        if (!getBit(i, j, k + 1))
            floodFill(i, j, k + 1);
        if (!getBit(i, j, k - 1))
            floodFill(i, j, k - 1);
    }
    public static void floodDelete(int i, int j, int k)
    {
        if (!getBit(i, j, k))
            return;
        DeleteCube(i, j, k);
        if (getBit(i - 1, j, k))
            floodDelete(i - 1, j, k);
        if (getBit(i + 1, j, k))
            floodDelete(i + 1, j, k);
        if (getBit(i, j, k + 1))
            floodDelete(i, j, k + 1);
        if (getBit(i, j, k - 1))
            floodDelete(i, j, k - 1);
    }
    public static void QfloodFill(int i, int j, int k)
    {
        Queue <Vector3> Q = new Queue <Vector3>();
        if (getBit(i, j, k))
            return;
        Q.Enqueue(getPosition(i, j, k));
        
        while (Q.Count > 0)
        {
            Vector3 curr = Q.Dequeue();

            int w = (int) curr.x;
            int e = (int) curr.x;
            int y = (int) curr.z;

                while ((w > 0) && getBit(w-1,j, y)==false)
                {
                    w--;
                }

                while ((e < ConfigurationManager.size) && getBit(e+1,j, y)==false)
                {
                    e++;
                }

                for (int x = w; x <= e; x++)
                {

                    SpawnCube(x,j,y);

                    if ((y > 0) && !getBit(x,j,y-1))
                    {
                        Q.Enqueue(getPosition(x,j,y - 1));
                    }

                    if ((y < ConfigurationManager.size) && !getBit(x, j, y + 1))
                    {
                        Q.Enqueue(getPosition(x, j, y + 1));
                    }
                }

            }
        }
}
