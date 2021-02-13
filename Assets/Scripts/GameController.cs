using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Piece
    {
        Empty = 0, Yellow = 1, Red = 2
    }

    public GameObject pieceRed;
    public GameObject pieceYellow;
    public Board board;

    GameObject currentPiece;

    bool allowMousePress = true;
    //bool gameOver = false;
    bool isPlayerTurn = false;
    bool isDroping = false;

    int col;
    Vector3 startPos;
    Vector3 endPos;

    void Start()
    {
        currentPiece = SpawnPiece();
    }

    GameObject SpawnPiece()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject newPiece = Instantiate(
            isPlayerTurn ? pieceYellow : pieceRed,
            new Vector3(Mathf.Clamp(spawnPos.x, 0, board.GetColums() - 1), 1, 1),
            Quaternion.identity) as GameObject;

        return newPiece;
    }

    void GetPos(int row, int col)
    {
        //col = (int)currentPiece.transform.position.x;
        startPos = currentPiece.transform.position;
        endPos = new Vector3();

        startPos = new Vector3(col, startPos.y, startPos.z);
        endPos = new Vector3(col, (row) * (-1), startPos.z);
    }

    int GetColorTurn()
    {
        int color = (int)Piece.Empty;
        color = isPlyaerTurn ? (int)Piece.Yellow : (int)Piece.Red;
        return color;
    }

    void Preparing(GameObject tempPiece)
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, 1f, 1f);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        tempPiece.transform.position = new Vector3(
                                    Mathf.Clamp(mouse.x, 0, board.GetColums() - 1), // limit the piece position when the mouse 
                                    1, 1);                                          // move out of the board space.
    }

    void UpdateBoard(int row, int col, int color)
    {
        board.map[row, col] = color;
        //Debug.Log("Row: " + row + " Col: " + col + " value: " + board.map[row, col]);
    }
}
