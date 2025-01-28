using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_mouse_control : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Vector3 _starting_position;
    public Vector3 mouse_offset;

    public float moveSpeed = 0.1f;
    public Rigidbody rigidbody;
    public GameObject pen;
    public Rigidbody hand;

    Plane plane = new Plane(Vector3.back, 0);
    public float forceAmount;

    public bool MouseActive;

    private void OnEnable()
    {
        Actions.onMouseActivate += activateMouse;
        Actions.onMouseDeactivate += deactivateMouse;
    }

    private void OnDisable()
    {
        Actions.onMouseActivate -= activateMouse;
        Actions.onMouseDeactivate -= deactivateMouse;
    }


    void Start()
    {
        _starting_position = pen.transform.position;
        MouseActive = false;
    }

    void Update()
    {
        if (MouseActive) { 
        //check to see if mouseinputs have started (intro scene is over)
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        rigidbody.position = worldPosition + mouse_offset;
        hand.position = worldPosition + mouse_offset;

        }
        //TODO: this needs to be fixed so the position isn't tied to the camera
        //rigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        //this one works ok...
        //rigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _starting_position.z));
    }

    private void activateMouse()
    {
          MouseActive = true;

    }

    private void deactivateMouse()
    {
        MouseActive = false;

    }


}