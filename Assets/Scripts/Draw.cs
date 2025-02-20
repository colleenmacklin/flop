using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draw : MonoBehaviour
{
    
    public Camera m_camera;
    public GameObject brush;
    public GameObject pen_tip;
    [SerializeField] private List<GameObject> brushMarks;

    public bool forceDraw = false;

    LineRenderer currentLineRenderer;

    public Vector3 lastPos;
    public Vector3 penPos;

    public bool isActive;
    public bool isWriting = false;

    private void OnEnable()
    {
        Actions.clearScreen += DestroyBrushmarks;

    }

    private void OnDisable()
    {
        Actions.clearScreen -= DestroyBrushmarks;

    }


    private void LateUpdate()
    {
        if (isActive)
            Drawing();
    }

    void Drawing()
    {
        if (Input.GetMouseButtonDown(0) || forceDraw && !isWriting)
        {
            CreateBrush();
            isWriting = true;
            //onDrawing?.Invoke(this.gameObject);
            //add sound trigger
        }

        else if (Input.GetMouseButton(0) || forceDraw && isWriting)
         {
            PointToPenPos();
            //add sound trigger
        }
        else
        {
            currentLineRenderer = null;
            isWriting = false;
        }
    }

    void CreateBrush()
    {

        penPos = pen_tip.transform.position;
        GameObject brushInstance = Instantiate(brush);
        brushMarks.Add(brushInstance);
        brushInstance.transform.position = penPos;

        //Debug.Log("penPos: " + penPos + ", " + "brushPos: " + brushInstance.transform.position);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

    }

    void DestroyBrushmarks()
    {
        foreach (GameObject b in brushMarks)
        {
            Destroy(b);
        }
    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
        //Debug.Log("adding a point: " + "penPos: " + penPos + ", " + "point: " + pointPos);

    }

    void PointToPenPos()
    {
        penPos = pen_tip.transform.position;
        //Debug.Log("pen tip position when drawing: " + penPos);

        if (lastPos != penPos)
        {
            AddAPoint(penPos);
            lastPos = penPos;
        }
    }


    
}


