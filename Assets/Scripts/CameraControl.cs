using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private float _panSpeed = 20.0f;

    [Header("Scroll Sensitivity Varibles")]
    [SerializeField] private float _scrollSpeed = 20.0f;
    [SerializeField] private float _minY = 20.0f;
    [SerializeField] private float _maxY = 120.0f;

    [Header("Rotation Varibles")]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;
    [SerializeField] private float _targetAngleX;
    [SerializeField] private float _targetAngleY;

    public Vector2 panLimit;

    
    void Update()
    {
        Vector3 pos = transform.position;

        // WASD Movement of Camera
        if (Input.GetKey("w"))
        {
            pos.z += _panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= _panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += _panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= _panSpeed * Time.deltaTime;
        }

        // Rotation ( Middle Mouse ) of Camera
        if (Input.GetMouseButton(2))
        {
            _targetAngleX += Input.GetAxis("Mouse X") * _sensitivityX * Time.deltaTime;
            _targetAngleY -= Input.GetAxis("Mouse Y") * _sensitivityY * Time.deltaTime;
        }

        //Zoom adjustment with using scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * _scrollSpeed * 100.0f * Time.deltaTime;

        // Constraints for WASD movement
        //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.y);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        //targetAngleY = Mathf.Clamp(targetAngleX, -360.0f, 360.0f); 

        transform.eulerAngles = new Vector3(_targetAngleY, _targetAngleX, 0); // Rotational Movement

        transform.position = pos; // Direct Movement
    }
}
