using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    const int EMPTY = 0, PLAYER_PIECE = 1, AI_PIECE = 2;
    const int MAX = 999999, MIN = -999999;
    const int WINDOW_LENGTH = 4;

    public GameObject pieceRed;
    public GameObject pieceYellow;
    public Board board;

    GameObject currentPiece;

    bool allowMousePress = true;
    bool gameOver = false;
    bool isPlayerTurn = true, isAITurn = false;
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

    GameObject SpawnPiece()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject newPiece = Instantiate(
            isPlayerTurn ? pieceYellow : pieceRed,
            new Vector3(Mathf.Clamp(spawnPos.x, 0, board.GetColumns() - 1), 1, 1),
            Quaternion.identity) as GameObject;

        return newPiece;
    }

    void Preparing(GameObject tempPiece)
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, 1f, 1f);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        tempPiece.transform.position = new Vector3(
                                    Mathf.Clamp(mouse.x, 0, board.GetColums() - 1), // limit the piece position when the mouse 
                                    1, 1);                                          // move out of the board space.
    }

    void UpdateBoard(int[,] board, int row, int col, int color)
    {
        board[row, col] = color;
        //Debug.Log("Row: " + row + " Col: " + col + " value: " + board.map[row, col]);
    }

    IEnumerator DropPieceAnimation(GameObject tempPiece)
    {
        float t = 0;
        int speed = 3;
        currentPiece = SpawnPiece();
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            tempPiece.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;

        }
        //activating the preparing() function
        isDroping = false;
        isAITurn = !isAITurn; // toggle AI each move
    }

    IEnumerator DropPieceAtColumn(int col, float delay)
    {
        yield return new WaitForSeconds(delay);
        int row = board.GetNextOpenRow(col);
        GetPos(row, col);

        StartCoroutine(DropPieceAnimation(currentPiece));
        UpdateBoard(row, col, color);
        if (board.WinCondition(board.map, piece))
        {
            print("Color " + piece + " win!!");
            gameOver = true;
        }
        if (board.GetValidLocations(board.map).Count == 0)
        {
            print("A Draw Game!");
            gameOver = true;
        } 
    }

    bool IsTerminalNode(int[,] tempBoard)
    {
        return (board.WinCondition(tempBoard, PLAYER_PIECE) || board.WinCondition(tempBoard, AI_PIECE)
                || board.GetValidLocations(tempBoard).Count == 0);
    }

    // A helper function to evaluate a score of a window of 4 spaces
    int EvaluateWindow(List<int> window, int piece)
    {
        int score = 0;
        int oppPice = piece == PLAYER_PIECE ? AI_PIECE : PLAYER_PIECE;
        // how many same color pieces appears within this window
        int countPiece = window.Where(p => p == piece).Count();
        int countEmpty = window.Where(p => p == EMPTY).Count();
        int countOppPiece = window.Where(p => p == oppPice).Count();

        if (countPiece == 4) { score += 1000; Debug.Log("countPiece   " + countPiece); }
        else if (countPiece == 3 && countEmpty == 1) { score += 5; }
        else if (countPiece == 2 && countEmpty == 2) score += 2;

        if (countOppPiece == 3 && countEmpty == 1) score -= 40;
        if (countOppPiece == 2 && countEmpty == 2) score -= 10;
        return score;
    }
}
