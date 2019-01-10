using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class gridGenerator : MonoBehaviour
{
    public int size = 1;
    public int minX = -25;
    public int maxX = 25;
    public int minZ = -25;
    public int maxZ = 25;
    public GameObject player;
    public List<GameObject> characters;
    public Vector3[] charLocations;
    Collider[] results;


    void Start()
    {
        InvokeRepeating("locateGrid", 0, 1);
        characters[0] = player;
    }

    //TODO: make a maximum distance from the player that this can go to avoid infinite loops
    //TODO: could remove checks inside of known area
    void locateGrid()
    {
        //check a box of 3 squares around each character, add characters in those areas to the list of
        //current characters
        Vector3 boxSize = new Vector3(size * 3, size * 3, size * 3);
        bool searching = true;
        while (searching)
        {
            foreach (GameObject character in characters)
            {
                Physics.OverlapBoxNonAlloc(transform.position, boxSize, results);
                if (results.Length != 0)
                {
                    foreach (Collider newChar in results)
                    {
                        characters.Add(newChar.gameObject);
                    }
                    //need to re-search the list if new chars added
                    break;
                }
            }
            //if finished searching all chars with no new ones, end check
            searching = false;
        }
        Debug.Log("balls");
        //charLocations. (player.transform.position);
        this.transform.position = charLocations[1];
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        //position -= transform.position;

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
        Gizmos.color = Color.yellow;
        for (float x = minX; x < maxX; x += size)
        {
            for (float z = minZ; z < maxZ; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.3f);
            }

        }
    }



    void Update()
    {

    }
}