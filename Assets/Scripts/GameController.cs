using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Piece
    {
        Empty = 0, Yellow = 1, Red = 2
    }

    //public GameObject boardObject;
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
}
