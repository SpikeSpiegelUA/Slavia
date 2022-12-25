using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public FixedJoystick moveJoystick;
    public FixedJoystick lookJoystick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = GetComponent<PlayerController>();
        var cameraMovement = GetComponentInChildren<CameraMovement>();
        playerController.runAxis = moveJoystick.Direction;
        playerController.lookAxis = lookJoystick.Direction;
        cameraMovement.lookAxis = lookJoystick.Direction;
    }
}
