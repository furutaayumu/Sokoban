using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{
    public GameObject playerPrefab;

    int[,] map = {
        {0,0,0,0,0},
        {1,0,0,0,0 },
        {0,0,0,0,0 },
        };
    GameObject[,] field;
    GameObject obj;


// Start is called before the first frame update
void Start()
    {
    string debugTXT = "";

     field = new GameObject
[
    map.GetLength(0),
    map.GetLength(1)
];

        //GameObject instance = Instantiate(
        //    plyerPrefab,
        //    new Vector3(0, 0, 0),
        //    Quaternion.identity);


        for (int y = 0;y<map.GetLength(0);y++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y,x] == 1)
                {
                    field[y,x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0.0f),
                        Quaternion.identity);
                }
            }
            debugTXT += "\n";
        }
        Debug.Log(debugTXT);
        //PrintArray();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber(tag,PlayerIndex, PlayerIndex);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }

    }
Vector2Int GetPlayerIndex()
    {for(int y = 0;y<field.GetLength(0);y++)
        {
            for(int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y,x] == null) { continue; }
                if (field[y,x].tag == "Player") { return new Vector2Int(x, y); }
            }       
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        if(moveTo.y<0||moveTo.y>= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        //if (field[moveTo.y,moveTo.x] != null && field[moveTo.y ,moveTo.x].tag == "Box")
        //{
        //    Vector2Int velocity = moveTo - moveFrom;
        //    bool success = MoveNumber(tag, moveTo, moveTo + velocity);
        //    if (!success) { return false; }
        //}
        field[moveFrom.y,moveFrom.x].transform.position = new Vector3(moveTo.x,field.GetLength(0)- moveTo.y,0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }
}

