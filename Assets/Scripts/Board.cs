using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    const int COLUMS = 7;
    const int ROWS = 6;

    enum Piece
    {
        Empty = 0, Yellow = 1, Red = 2
    }
    
    public int[,] map;
    public GameObject pieceRed;
    public GameObject pieceYellow;

    GameObject gameObjectField;
    GameObject currentPiece = null;
    bool mouseButtonPressed;

    float startTime;

    Vector3 startPos;
    Vector3 endPos;
    bool allowMousePress = true;
    bool isPlayerTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        CreateBoard();
             
        if (currentPiece == null) Debug.Log("GameObject is null");
    }


    void CreateBoard()
    {
        //Generate the board with 42 empty pieces
        map = new int[ROWS, COLUMS];
        for (int col = 0; col < COLUMS; col++)
        {
            for (int row = 0; row < ROWS; row++)
            {
                map[row, col] = (int)Piece.Empty;
            }
        }

        // center camera
        Camera.main.transform.position = new Vector3(
            (COLUMS - 1) / 2.0f, -((ROWS - 3) / 2.0f), Camera.main.transform.position.z);
    }

    public bool IsValidLocation(int col)
    {
        return (map[0, col] == 0);
    }

    public int GetNextOpenRow(int col)
    {
        for (int row = ROWS - 1; row >= 0; row--)
        {
            if (map[row, col] == 0)
                return row;
        }
        return 0;
    }

    public int GetColums() { return COLUMS; }
    public int GetRows() { return ROWS; }
}
