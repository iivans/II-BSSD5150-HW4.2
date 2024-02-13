using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDragWithDynamics : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;
    public float maxSpeed = 10;
    Vector2 mouseForce;
    Vector3 lastPosition;

    bool isDragging = false; // Flag to track if dragging is active

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (selectedObject)
        {
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
            lastPosition = mousePosition;
        }

        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
                isDragging = true; // Set the flag to indicate that dragging has started
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.velocity = Vector2.zero;
            selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
            selectedObject = null;
            isDragging = false; // Reset the flag when dragging is stopped
        }
    }

    void FixedUpdate()
    {
        if (selectedObject && isDragging)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}
