using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_control : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Vector3 _starting_position;
    public Vector3 mouse_offset;


    public float moveSpeed = 0.1f;
    public Rigidbody _rigidbody;
    public GameObject pen;

    Plane plane = new Plane(Vector3.back, 0);

    public void SetCursorVisible(bool value) {
        Cursor.visible = value;
    }

    void Start()
    {
        //_starting_position = Camera.main.ScreenToWorldPoint(rigidbody.position);
        _starting_position = pen.transform.position;
        //Set Cursor to not be visible
        Cursor.visible = false;


    }

    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        GetComponent<Rigidbody>().position = worldPosition+mouse_offset;
    }

    //Vector3 mousePos = Input.mousePosition;

    //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

    //transform.position = worldPos;

}