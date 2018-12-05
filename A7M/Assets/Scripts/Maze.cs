using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour
{

    public struct CellPos
    {
        public int x;
        public int y;
    };
    public struct Cell
    {
        public bool visited;
        public int x;
        public int y;
        public bool north;
        public bool east;
        public bool west;
        public bool south;
    };

    public int lengthx;
    public int lengthy;
    public int prob;
    public GameObject wall;
    public float spawnSpeed;
    public bool isMapCreated = false;
    public bool deployRobot = false;
    int currentx;
    int currenty;
    int[,] positions;
    CellPos selectedNeighbour;
    CellPos[] nearbyCells;
    int n_pos;
    public Cell[,] map;
    int mapCells;

    int lastposx;
    int lastposy;

    int FindNeighbors(int valx, int valy)
    {
        int n_cells = 0;
        int valxo = valx - 1;
        int valxw = valx + 1;
        int valys = valy - 1;
        int valyn = valy + 1;

        if (valx > 0)
        {
            if (!map[valxo, currenty].visited)
            {
                nearbyCells[n_cells].x = valxo;
                nearbyCells[n_cells].y = valy;
                n_cells++;
            }
        }
        if (valx < (lengthx - 1))
        {
            if (!map[valxw, currenty].visited)
            {
                nearbyCells[n_cells].x = valxw;
                nearbyCells[n_cells].y = valy;
                n_cells++;
            }
        }
        if (valy > 0)
        {
            if (!map[valx, valys].visited)
            {
                nearbyCells[n_cells].x = valx;
                nearbyCells[n_cells].y = valys;
                n_cells++;
            }
        }
        if (valy < (lengthy - 1))
        {
            if (!map[valx, valyn].visited)
            {
                nearbyCells[n_cells].x = valx;
                nearbyCells[n_cells].y = valyn;
                n_cells++;
            }
        }
        return n_cells;
    }

    void SetMapNull()
    {
        for (int x = 0; x < lengthx + 1; x++)
        {
            for (int y = 0; y < lengthy + 1; y++)
            {
                map[x, y].x = x;
                map[x, y].y = y;
                map[x, y].visited = false;
                map[x, y].north = true;
                map[x, y].east = true;
                map[x, y].west = true;
                map[x, y].south = true;
            }
        }
    }

    CellPos chooseNeighbour(int n_neighboors)
    {
        int randomOne = Random.Range(0, n_neighboors);
        CellPos randomCell;
        randomCell = nearbyCells[randomOne];
        return randomCell;
    }

    void DeleteWalls(int nextCellx, int nextCelly)
    {
        if (nextCellx - currentx != 0)
        {
            if (nextCellx - currentx == 1)
            {
                map[currentx, currenty].east = false;
                map[currentx + 1, currenty].west = false;
            }
            else if (nextCellx - currentx == -1)
            {
                map[currentx, currenty].west = false;
                map[currentx - 1, currenty].east = false;
            }
        }
        else
        {
            if (nextCelly - currenty == 1)
            {
                map[currentx, currenty].north = false;
                map[currentx, currenty + 1].south = false;
            }
            else if (nextCelly - currenty == -1)
            {
                map[currentx, currenty].south = false;
                map[currentx, currenty - 1].north = false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        lengthx = PlayerPrefs.GetInt("x_value");
        lengthy = PlayerPrefs.GetInt("y_value");

        n_pos = 0;
        mapCells = 0;

        int pos_length = lengthx * lengthy;
        positions = new int[pos_length, 2];
        nearbyCells = new CellPos[4];
        map = new Cell[lengthx + 1, lengthy + 1];

        SetMapNull();

        currentx = Random.Range(0, lengthx);
        currenty = Random.Range(0, lengthy);

        map[currentx, currenty].visited = true;
        mapCells++;
        n_pos++;
        while (mapCells < ((lengthx) * (lengthy)))
        {
            int neighboors = FindNeighbors(currentx, currenty);
            if (neighboors > 0)
            {
                selectedNeighbour = chooseNeighbour(neighboors);
                DeleteWalls(selectedNeighbour.x, selectedNeighbour.y);
                positions[n_pos, 0] = currentx;
                positions[n_pos, 1] = currenty;
                n_pos++;
                currentx = selectedNeighbour.x;
                currenty = selectedNeighbour.y;
                map[currentx, currenty].visited = true;
                mapCells++;
            }
            else
            {
                n_pos--;
                currentx = positions[n_pos, 0];
                currenty = positions[n_pos, 1];
            }
        }
        //tolgo alcuni muri
        for (int i = 1; i < (lengthx - 1); i++)
        {
            for (int u = 1; u < (lengthy - 1); u++)
            {
                currentx = i;
                currenty = u;
                int delete = Random.Range(0, prob);
                if (delete == 0)
                {
                    if (map[currentx, currenty].north)
                    {
                        DeleteWalls(currentx, (currenty + 1));
                    }
                    else if (map[currentx, currenty].south)
                    {
                        DeleteWalls(currentx, (currenty - 1));
                    }
                    else if (map[currentx, currenty].east)
                    {
                        DeleteWalls((currentx + 1), currenty);
                    }
                    else if (map[currentx, currenty].west)
                    {
                        DeleteWalls((currentx - 1), currenty);
                    }
                }
            }
        }
        StartCoroutine(SpawnMap());
    }

    IEnumerator SpawnMap()
    {
        for (int i = 0; i < lengthx; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            for (int u = 0; u < lengthy; u++)
            {
                if (map[i, u].north)
                {
                    Instantiate(wall, new Vector3(i, u, -1), Quaternion.AngleAxis(0, Vector3.forward));
                }
                if (map[i, u].east)
                {
                    Instantiate(wall, new Vector3(i, u, -1), Quaternion.AngleAxis(-90, Vector3.forward));
                }
                if (map[i, u].west)
                {
                    Instantiate(wall, new Vector3(i, u, -1), Quaternion.AngleAxis(90, Vector3.forward));
                }
                if (map[i, u].south)
                {
                    Instantiate(wall, new Vector3(i, u, -1), Quaternion.AngleAxis(180, Vector3.forward));
                }
            }
        }
        isMapCreated = true;
        yield return new WaitForSeconds(1);
        deployRobot = true;
    }
}