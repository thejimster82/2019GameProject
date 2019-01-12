using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class gridGenerator : MonoBehaviour
{
    public int size = 4;
    public int minX = -25;
    public int maxX = 25;
    public int minZ = -25;
    public int maxZ = 25;
    public int distance = 20;
    public GameObject player;
    public List<GameObject> characters;
    public Collider[] results = new Collider[20];
    public bool searching = true;
    public bool inCombat;

    public void Start()
    {
        //could add multiple players for local coop instead
        characters.Add(player);
        //TODO: find a good time interval to run this (very fast for testing currently)
        InvokeRepeating("locateGrid", 0, 0.01f);
    }

    //TODO: make a maximum distance from the player that this can go to avoid infinite loops
    //TODO: could remove checks inside of known area
    void locateGrid()
    {
        //use this to reset grid size each round of checks
        maxX = 25;
        maxZ = 25;
        //locates grid at player when not in combat, takes other characters into account when in combat
        if (inCombat)
        {
            this.transform.position = GetNearestPointOnGrid(player.transform.position);
            //check a box of 3 squares around each character, add characters in those areas to the list of
            //current characters
            searching = true;
            while (searching)
            {
                foreach (GameObject character in characters)
                {
                    Vector3 boxSize = new Vector3(size * distance, size * distance, size * distance);
                    Collider[] hitColliders = Physics.OverlapBox(transform.position, boxSize);
                    Debug.Log("results" + results);
                    foreach (Collider newChar in hitColliders)
                    {
                        if (!characters.Contains(newChar.gameObject))
                        {
                            characters.Add(newChar.gameObject);
                        }
                    }
                    //need to re-search the list if new chars added
                    break;
                }
                //if finished searching all chars with no new ones, end check
                searching = false;
            }
            foreach (GameObject item in characters)
            {
                //if characters are out of grid bounds, resize grid
                //make grid bounds a multiple of size and add extra to edges
                if (Mathf.Abs(player.transform.position.x - item.transform.position.x) > maxX)
                {
                    maxX = Mathf.RoundToInt(Mathf.Abs(player.transform.position.x - item.transform.position.x));
                    maxX = Mathf.RoundToInt(maxX / size) * size + 8 * size;

                }
                if (Mathf.Abs(player.transform.position.z - item.transform.position.z) > maxZ)
                {
                    maxZ = Mathf.RoundToInt(Mathf.Abs(player.transform.position.z - item.transform.position.z));
                    maxZ = Mathf.RoundToInt(maxZ / size) * size + 8 * size;
                }
            }
        }
        else
        {
            //clear char checking array
            characters.Clear();
            characters.Add(player);
            this.transform.position = GetNearestPointOnGrid(player.transform.position);
        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxSize = new Vector3(size * distance, size * distance, size * distance);
        for (float x = -maxX; x < maxX; x += size)
        {
            for (float z = -maxZ; z < maxZ; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z)) + transform.position;
                Gizmos.DrawSphere(point, 0.3f);
            }

        }
        //Gizmos.DrawCube(transform.position, boxSize);
    }

    void Update()
    {
    }
}