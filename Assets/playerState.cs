using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerState : MonoBehaviour
{
    public int health;
    public bool inCombat = false;

    public playerMovement playerMover;

    public gridGenerator grid;
    // Start is called before the first frame update
    void Start()
    {
        playerMover = GetComponent<playerMovement>();
        grid = GameObject.Find("grid_Manager").GetComponent<gridGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        var InputDevice = InputManager.ActiveDevice;
        if (InputDevice.Action1 && inCombat == false)
        {
            inCombat = true;
            //move player onto the grid
            playerMover.moveToGrid();

        }
        if (InputDevice.Action2 && inCombat == true)
        {
            inCombat = false;
            grid.characters.Clear();
            grid.characters.Add(grid.player);
            grid.objectLocations.Clear();
        }
    }
}
