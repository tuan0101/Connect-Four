﻿using System.Collections;
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

    // Check the first row if available
    public bool IsValidLocation(int[,] tempBoard, int col)
    {
        return (tempBoard[0, col] == 0);
    }

    public List<int> GetValidLocations(int[,] tempBoard)
    {
        List<int> validLocations = new List<int>();
        for (int col = 0; col < COLUMNS; col++)
        {
            if (IsValidLocation(tempBoard, col)) validLocations.Add(col);
        }
        return validLocations;
    }

    public int GetNextOpenRow(int[,] tempBoard, int col)
    {
        for (int row = ROWS - 1; row >= 0; row--)
        {
            if (tempBoard[row, col] == 0)
                return row;
        }
        return 0;
    }

    // @map is tempBoard
    public bool WinCondition(int[,] map, int color)
    {
        // Check horizontal case for win
        for (int c = COLUMS - 1; c >= 3; c--)
            for (int r = ROWS - 1; r >= 3; r--)
            {
                if (map[r, c] == color && map[r, c - 1] == color
                    && map[r, c - 2] == color && map[r, c - 3] == color)
                    return true;
            }

        // Check vertical locations for win
        for (int c = COLUMS - 1; c >= 0; c--)
            for (int r = ROWS - 1; r >= 3; r--)
            {
                if (map[r, c] == color && map[r - 1, c] == color
                    && map[r - 2, c] == color && map[r - 3, c] == color)
                    return true;
            }

        //Check for negatively sloped diagnols 
        for (int c = COLUMS - 1; c >= 3; c--)
            for (int r = ROWS - 1; r >= 3; r--)
            {
                if (map[r, c] == color && map[r - 1, c - 1] == color
                    && map[r - 2, c - 2] == color && map[r - 3, c - 3] == color)
                    return true;
            }

        //Check for positively sloped diagnols 
        for (int c = 0; c < COLUMS - 3; c++)
            for (int r = ROWS - 1; r >= 3; r--)
            {
                if (map[r, c] == color && map[r - 1, c + 1] == color
                    && map[r - 2, c + 2] == color && map[r - 3, c + 3] == color)
                    return true;
            }
        return false;
    }

    public int GetColums() { return COLUMS; }
    public int GetRows() { return ROWS; }
}
