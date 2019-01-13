using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using world;

public class playerMovement : MonoBehaviour
{
    public CharacterController mover;
    public worldLocation combatLocation;
    public gridGenerator grid;
    void Start()
    {
        //pull charcontroller from parent object
        mover = GetComponentInParent(typeof(CharacterController)) as CharacterController;
        //usually use charcontroller for collisions but I just use it
        //because it is easier to move things around via simpleMove()
        mover.detectCollisions = false;
        grid = GameObject.Find("grid_Manager").GetComponent<gridGenerator>();
    }

    void Update()
    {
        if (GetComponent<playerState>().inCombat)
        {//combat-based movement
            combatLocation = grid.transformToGridIndices(transform.position);
            //Debug.Log(combatLocation.x + " " + combatLocation.z);
        }
        else
        { //free movement
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

    public void moveToGrid()
    {
        transform.position = grid.transformToGridspace(transform.position);
    }
}
