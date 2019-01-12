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

    public void Start()
    {
        InvokeRepeating("locateGrid", 0, 1);
        characters.Add(player);
    }

    //TODO: make a maximum distance from the player that this can go to avoid infinite loops
    //TODO: could remove checks inside of known area
    void locateGrid()
    {
        this.transform.position = player.transform.position;
        //check a box of 3 squares around each character, add characters in those areas to the list of
        //current characters
        bool searching = true;
        while (searching)
        {
            foreach (GameObject character in characters)
            {
                Vector3 boxSize = new Vector3(size * distance, size * distance, size * distance);
                Physics.OverlapBoxNonAlloc(transform.position, boxSize, results);
                Debug.Log(results);
                foreach (Collider newChar in results)
                {
                    Debug.Log(newChar.gameObject);
                    characters.Add(newChar.gameObject);
                }
                //need to re-search the list if new chars added
                break;
            }
            //if finished searching all chars with no new ones, end check
            searching = false;
        }
        foreach (GameObject item in characters)
        {
            if (Mathf.Abs(player.transform.position.x - item.transform.position.x) > maxX)
            {
                maxX = Mathf.RoundToInt(Mathf.Abs(player.transform.position.x - item.transform.position.x));
            }
            if (Mathf.Abs(player.transform.position.z - item.transform.position.z) > maxZ)
            {
                maxZ = Mathf.RoundToInt(Mathf.Abs(player.transform.position.z - item.transform.position.z));
            }
        }
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
        Gizmos.color = Color.red;
        Vector3 boxSize = new Vector3(size * distance, size * distance, size * distance);
        for (float x = -1 * maxX - 3 * size; x < maxX + 3 * size; x += size)
        {
            for (float z = -1 * maxZ - 3 * size; z < maxZ + 3 * size; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.3f);
            }

        }
        Gizmos.DrawCube(transform.position, boxSize);
    }



    void Update()
    {
    }
}