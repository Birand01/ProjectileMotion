using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private Vector3 mouseOffset;
    [SerializeField] private float mouseZCoor;
    [SerializeField] private float ylimit = -0.31f;
    [SerializeField] private float xlimit = 47.0f;
    [SerializeField] private float zlimit = 22.0f;

    
    private void Update()
    {
        ObjectMovement();
    }

    private void ObjectMovement()
    {
        if (transform.position.y < ylimit)
        {
            transform.position = new Vector3(transform.position.x, ylimit, transform.position.z);
        }
        if (transform.position.x > xlimit)
        {
            transform.position = new Vector3(xlimit, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xlimit)
        {
            transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -zlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zlimit);
        }
        if (transform.position.z > zlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zlimit);
        }
    }
    
   
    private void OnMouseDown()
    {
     
            mouseZCoor = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mouseOffset = gameObject.transform.position - GetMouseWorldPosition();
    }
    private Vector3 GetMouseWorldPosition()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mouseZCoor;


        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mouseOffset;
    }
}
