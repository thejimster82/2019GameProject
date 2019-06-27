using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;
using static UnityEngine.Time;
public class playerMovement : MonoBehaviour
{
    public CharacterController ctrlr;
    public playerAttack atker;
    // private InputDevice inDevice;
    public float xMove = 0.0f;
    public float yMove = 0.0f;
    public float dashSpeed = 4.0f;
    public float maxRunSpeed = 20.0f;
    private float moveSpeed = 1.0f;
    public float defaultMoveSpeed = 1.0f;
    public float dashTime = 0.5f;
    public float maxTime = 0.5f;
    private float xProportion = 1.0f;
    private float yProportion = 1.0f;
    public Vector3 playerDir;
    private bool movementEnabled = true;
    void Awake()
    {
        //inDevice = InputManager.ActiveDevice;
        ctrlr = GetComponentInParent(typeof(CharacterController)) as CharacterController;
        atker = GetComponentInParent(typeof(playerAttack)) as playerAttack;
        ctrlr.detectCollisions = false;
    }
    void Update()
    {
        playerDir = ctrlr.velocity.normalized;
        xProportion = 1f;
        yProportion = 1f;
        //TODO: change these to be event-based
        if (movementEnabled == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                yMove = moveSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                yMove = -moveSpeed;
            }
            else
            {
                yMove = 0.0f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                xMove = moveSpeed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                xMove = -moveSpeed;
            }
            else
            {
                xMove = 0.0f;
            }
        }
        else
        {
            yMove = 0.0f;
            xMove = 0.0f;
        }


        if (yMove != 0)
        {
            xProportion = (float)Sin(Abs(xMove) / Abs(yMove));
        }
        if (xMove != 0)
        {
            yProportion = (float)Sin(Abs(yMove) / Abs(xMove));
        }
        float xMoveFloat = -50 * maxRunSpeed * xProportion * xMove;
        float yMoveFloat = -50 * maxRunSpeed * yProportion * yMove;
        Vector3 fullMove = Vector3.Lerp(ctrlr.velocity, new Vector3(xMoveFloat * Time.deltaTime, 0, yMoveFloat * Time.deltaTime), 0.1f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashTime <= 0 && !atker.getAtkStatus())
            {
                fullMove = new Vector3(-1 * xMove * dashSpeed * maxRunSpeed, 0, -1 * yMove * dashSpeed * maxRunSpeed);
                dashTime = maxTime;
                //atker.atkTime = 0;
            }
        }
        //TODO: utilize move instead of simplemove and fix slopes/gravity
        ctrlr.SimpleMove(fullMove);
        dashTime -= Time.deltaTime;
    }

    public void pauseMovement()
    {
        movementEnabled = false;
    }
    public void resumeMovement()
    {
        movementEnabled = true;
    }
    public void setMovement(float speed)
    {
        moveSpeed = speed;
    }
}
