using UnityEngine;
using System.Collections;

public class TTTManager : MonoBehaviour
{
    public GameObject redPen;
    public GameObject bluePen;
    public Vector3[] boardPositions = new Vector3[9];
    
    private char[,] board = new char[3, 3]; // Game state
    private GameObject redPenInstance;
    public AdversaryController adversaryController;
    private bool isPlayerTurn = false;
    private int turnCount = 0;

    private bool playerIsDrawing = false;

    void Start()
    {
        StartCoroutine(InitializeGame());
    }

    IEnumerator InitializeGame()
    {
        yield return new WaitForSeconds(1f); // Small delay before starting
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = ' ';

        // Computer starts first and places 'X' in the center
        board[1, 1] = 'X';
        yield return StartCoroutine(adversaryController.DrawXAtPosition(boardPositions[4]));
        isPlayerTurn = true;
    }

    void Update()
    {
        if (isPlayerTurn)
        {
            if (!playerIsDrawing && bluePen.GetComponent<Draw>().isWriting) {
                playerIsDrawing = true;
                
            } else if (playerIsDrawing && !bluePen.GetComponent<Draw>().isWriting) {
                playerIsDrawing = false;
                Vector3 nearestPos = GetNearestBoardPosition(bluePen.transform.position);
                int index = System.Array.IndexOf(boardPositions, nearestPos);
                int x = index / 3;
                int y = index % 3;
                DrawAt(x, y, true);
                isPlayerTurn = false;
                turnCount++;
                if (turnCount >= 4)
                {
                    StartCoroutine(adversaryController.ChasePlayer());
                }
                else
                {
                    StartCoroutine(AdversaryTurn());
                }
            }
        }
    }

    Vector3 GetNearestBoardPosition(Vector3 penPosition)
    {
        float minDist = float.MaxValue;
        Vector3 nearestPos = boardPositions[0];
        foreach (Vector3 pos in boardPositions)
        {
            float dist = Vector3.Distance(penPosition, pos);
            if (dist < minDist)
            {
                minDist = dist;
                nearestPos = pos;
            }
        }
        return nearestPos;
    }

    void DrawAt(int x, int y, bool isPlayer = false)
    {
        if (board[x, y] == ' ') {
            board[x, y] = isPlayer ? 'O' : 'X';
            if (!isPlayer) {
                StartCoroutine(adversaryController.DrawXAtPosition(boardPositions[x * 3 + y]));
            }
        }
    }

    IEnumerator AdversaryTurn()
    {
        yield return new WaitForSeconds(1.5f);
        Vector3 pos = GetEmptyPosition();
        int index = System.Array.IndexOf(boardPositions, pos);
        int x = index / 3;
        int y = index % 3;
        DrawAt(x, y, false);
        turnCount++;
        yield return new WaitForSeconds(1f);
        StartCoroutine(adversaryController.RestPen());
        isPlayerTurn = true;
    }

    Vector3 GetEmptyPosition()
    {
        foreach (Vector3 pos in boardPositions)
        {
            int index = System.Array.IndexOf(boardPositions, pos);
            int x = index / 3;
            int y = index % 3;
            if (board[x, y] == ' ')
                return pos;
        }
        return Vector3.zero;
    }
}