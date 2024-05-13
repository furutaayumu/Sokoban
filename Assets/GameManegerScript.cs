using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject clearText;
    public GameObject clearObj;

    int[,] map;
    GameObject[,] field;
    GameObject obj;


// Start is called before the first frame update
void Start()
    {

        map = new int[,] {
        { 0,0,0,0,0 },
        { 0,3,1,3,0 },
        { 0,0,2,0,0 },
        { 0,2,3,2,0 },
        { 0,0,0,0,0 },
        };

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
                if (map[y,x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                        clearObj,
                        new Vector3(x, map.GetLength(0) - y, 0.1f),
                        Quaternion.identity
                        );
                }
            }
        }
        //PrintArray();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber(PlayerIndex, PlayerIndex + new Vector2Int(1,0));

            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber(PlayerIndex, PlayerIndex + new Vector2Int(-1, 0));

            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber(PlayerIndex, PlayerIndex + new Vector2Int(0, -1 ));

            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber(PlayerIndex, PlayerIndex + new Vector2Int(0, 1));

            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }
        //クリア
        if (IsCleard())
        {
            Debug.Log("Clear");
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

    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        if(moveTo.y<0||moveTo.y>= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x] = null;
        
        return true;
    }

    //クリア判定
    bool IsCleard()
    {
        List<Vector2Int> goals = new  List<Vector2Int>();

        for(int y = 0; y <map.GetLength(0); y++)
        {
            for(int x = 0;x<map.GetLength(1); x++)
            {
                if (map[y,x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }
        for(int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if(f == null || f.tag != "Box")
            {
                return false;
            }
        }
        return true;
    }
}

