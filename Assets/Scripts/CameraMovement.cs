using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float rotationX = 0f;
    private float sensitivityMouse = 100f;
    private float rotationXBorders = 60f;
    [HideInInspector]
    public Vector2 lookAxis;
    // Update is called once per frame
    void Update()
    {
        rotationX -= lookAxis.y * sensitivityMouse*Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -rotationXBorders, rotationXBorders);
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z); 
    }
}
