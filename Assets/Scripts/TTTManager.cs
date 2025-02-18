using UnityEngine;
using System.Collections;

public class TTTManager : MonoBehaviour
{
    public GameObject redPen;
    public GameObject bluePen;
    public Vector3[] boardPositions = new Vector3[9];

    public mouse_control mouseControl;
    
    private char[,] board = new char[3, 3]; // Game state
    public AdversaryController adversaryController;
    private bool isPlayerTurn = false;
    private int turnCount = 0;

    private bool playerIsDrawing = false;
    private bool shouldRecoverBluePenRotation = false;

    void Start()
    {
        // AddCollidersToBones(redPen);
        // AddCollidersToBones(bluePen);
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
                    StartCoroutine(FightScene());
                }
                else
                {
                    StartCoroutine(AdversaryTurn());
                }
            }
        }
    }

    IEnumerator RecoverPenRotationOverTime(GameObject pen, Quaternion targetRotation)
    {
        while (adversaryController.chargeCount < adversaryController.attackNumCharges)
        {
            pen.transform.rotation = Quaternion.Slerp(pen.transform.rotation, targetRotation, 0.1f);
            yield return null;
        }
    }

    IEnumerator FightScene() {
        yield return StartCoroutine(adversaryController.Scribble());
        SetBluePenDrawActive(false);
        AddCollidersToBones(redPen);
        AddCollidersToBones(bluePen);
        StartCoroutine(RecoverPenRotationOverTime(bluePen, Quaternion.identity));
        yield return StartCoroutine(adversaryController.ChasePlayer());
        mouseControl?.SetCursorVisible(true);
        StartCoroutine(WrestlingScene());
    }

    IEnumerator WrestlingScene() {
        bluePen.GetComponent<mouse_control>().enabled = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveBluePenToMiddleOfScreen());
        StartCoroutine(adversaryController.WrestleWithPlayer());
    }

    IEnumerator MoveBluePenToMiddleOfScreen() {
        Vector3 middle = new Vector3(0, 0, 0);
        while (true) {
            if (Vector3.Distance(bluePen.transform.position, middle) < 0.1f) {
                // move towards red pen
                while (Vector3.Distance(bluePen.transform.position, redPen.transform.position) > 0.1f) {
                    bluePen.transform.position = Vector3.Lerp(bluePen.transform.position, redPen.transform.position, 0.1f);
                    yield return null;
                }
            } else {
                bluePen.transform.position = Vector3.Lerp(bluePen.transform.position, middle, 0.02f);
            }
            yield return null;
        }
    }

    void SetBluePenDrawActive(bool active)
    {
        bluePen.GetComponent<Draw>().isActive = active;
        bluePen.GetComponent<Draw>().forceDraw = active;
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

    void AddCollidersToBones(GameObject pen)
    {
        foreach (Transform bone in pen.GetComponentsInChildren<Transform>())
        {
            if (bone != pen.transform) // Skip the root object
            {
                CapsuleCollider collider = bone.gameObject.AddComponent<CapsuleCollider>();
                collider.direction = 1; // Z-axis for best alignment with bones
                collider.radius = 0.0025f;
                collider.height = 0.005f;
            }
        }
    }
}