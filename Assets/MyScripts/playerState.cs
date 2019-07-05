using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using InControl;

public class playerState : MonoBehaviour
{
    public int health;
    public bool inCombat = false;

    public playerMovement playerMover;

    void Start()
    {
        playerMover = GetComponent<playerMovement>();
        // var InputDevice = InputManager.ActiveDevice;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
