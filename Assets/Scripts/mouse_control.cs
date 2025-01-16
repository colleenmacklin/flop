using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_control : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public float moveSpeed = 0.1f;
    public new Rigidbody rigidbody;
    public GameObject pen;
    private Vector3 _starting_position;
    Plane plane = new Plane(Vector3.back, 0);


    void Start()
    {
        //_starting_position = Camera.main.ScreenToWorldPoint(rigidbody.position);
        _starting_position = pen.transform.position;

    }

    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        rigidbody.position = worldPosition;
        //TODO: this needs to be fixed so the position isn't tied to the camera
        //rigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        //this one works ok...
        //rigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _starting_position.z));


        //debugging
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(mousePosition);
        }
        */
    }
}