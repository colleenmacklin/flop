using UnityEngine;
using System.Collections;

public class TTTPenController : MonoBehaviour
{
    public Transform restPositionObj; //  empty GameObject for rest position
    private mouse_control mouseControl;
    private Draw draw;
    public float moveSpeed = 2.0f;

    private bool isActive = false;
    private Plane plane = new Plane(Vector3.back, 0); //  for raycasting (borrowed from mouse_control script)

    private void Awake()
    {
        mouseControl = GetComponent<mouse_control>();
        draw = GetComponent<Draw>();
    }

    public void ActivatePen()
    {
        Debug.Log("ActivatePen: " + gameObject.name);
        isActive = true;
        mouseControl.enabled = true;
        draw.isActive = true;
    }

    public void DeactivatePen()
    {
        Debug.Log("DeactivatePen: " + gameObject.name);
        isActive = false;
        mouseControl.enabled = false;
        draw.isActive = false;
    }

    public void MoveToMouseAndActivate()
    {
        StartCoroutine(CoMoveToMouseAndActivate());
    }

    public void MoveToRestPositionAndDeactivate()
    {
        StartCoroutine(CoMoveToRestPositionAndDeactivate());
    }

    private IEnumerator CoMoveToMouseAndActivate()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 elbowPosition = CalculateElbowPosition(startPosition, targetPosition);

        // move to elbow first, then to mouse, so pen's don't 'crash'
        yield return MoveToPosition(elbowPosition);
        yield return MoveToPosition(GetMouseWorldPosition()); // Use updated mouse position

        ActivatePen();
    }

    private IEnumerator CoMoveToRestPositionAndDeactivate()
    {
        yield return MoveToPosition(restPositionObj.position);
        DeactivatePen();
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private Vector3 CalculateElbowPosition(Vector3 start, Vector3 target)
    {
        Vector3 direction = (target - start).normalized;
        float magnitude = Vector3.Distance(start, target);
        float elbowDistance = 0.5f * magnitude; // halfway thru the line
        // use cross product to find a perpendicular direction 
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;
        // apply the perpendicular offset
        Vector3 elbowOffset = perpendicular * 2.0f; // hardcoded.. :P
        return start + (direction * elbowDistance) + elbowOffset;
    }


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }
}
