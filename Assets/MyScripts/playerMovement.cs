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
    private Vector3 playerDir;
    private bool movementEnabled = true;
    private bool rotationEnabled = true;
    private bool dashingEnabled = true;
    void Awake()
    {
        //inDevice = InputManager.ActiveDevice;
        ctrlr = GetComponentInParent(typeof(CharacterController)) as CharacterController;
        atker = GetComponentInParent(typeof(playerAttack)) as playerAttack;
        ctrlr.detectCollisions = false;
    }
    void Update()
    {
        //TODO: change these to be event-based
        if (movementEnabled == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                yMove = moveSpeed;
                playerDir = ctrlr.velocity.normalized;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                yMove = -moveSpeed;
                playerDir = ctrlr.velocity.normalized;
            }
            else
            {
                yMove = 0.0f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                xMove = moveSpeed;
                playerDir = ctrlr.velocity.normalized;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                xMove = -moveSpeed;
                playerDir = ctrlr.velocity.normalized;
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

        xProportion = 1f;
        yProportion = 1f;
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
            if (dashTime <= 0 && dashingEnabled)
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
    public Vector3 getPlayerDir()
    {
        return playerDir;
    }
    public void pauseRotation()
    {
        rotationEnabled = false;
    }
    public void resumeRotation()
    {
        rotationEnabled = true;
    }
    public void pauseDashing()
    {
        dashingEnabled = false;
    }
    public void resumeDashing()
    {
        dashingEnabled = true;
    }
    public void nudgePlayer(float amt)
    {
        //TODO: make nudging actually perform the correct movement
        Vector3 newMovement = Vector3.Lerp(ctrlr.velocity, ctrlr.velocity * amt, 0.1f);
        ctrlr.SimpleMove(ctrlr.velocity * amt);
    }
}
