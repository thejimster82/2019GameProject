using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerMovement : MonoBehaviour
{
    public CharacterController mover;

    void Start()
    {
        //pull charcontroller from parent object
        mover = GetComponentInParent(typeof(CharacterController)) as CharacterController;
        //usually use charcontroller for collisions but I just use it
        //because it is easier to move things around via simpleMove()
        mover.detectCollisions = false;
    }

    void Update()
    {
        //pick active controller from InControl
        var inputDevice = InputManager.ActiveDevice;
        //set up floats from left axis of controller
        float xMove = -1000.0f * Time.deltaTime * inputDevice.LeftStick.X;
        float zMove = -1000.0f * Time.deltaTime * inputDevice.LeftStick.Y;
        //use Lerp to interpolate movement for smoothness
        Vector3 fullMove = Vector3.Lerp(mover.velocity, new Vector3(xMove, 0, zMove), 0.1f);
        mover.SimpleMove(fullMove);
    }
}
