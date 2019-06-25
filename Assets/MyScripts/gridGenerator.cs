// using UnityEngine;
// using System.Collections.Generic;
// using System.Linq;
// using world;

// public class gridGenerator : MonoBehaviour
// {
//     public int size = 4;
//     public int minX = -25;
//     public int maxX = 25;
//     public int minZ = -25;
//     public int maxZ = 25;
//     public int distance = 20;
//     public GameObject player;
//     public List<GameObject> characters;
//     public Collider[] results = new Collider[20];
//     public bool searching = true;
//     public bool inCombat;
//     public List<worldLocation> objectLocations = new List<worldLocation>();
//     public worldLocation playerLocation;
//     public LayerMask affectsCombat;


//     public void Start()
//     {
//         //set up layers which are checked for combat affecting grid locations
//         affectsCombat = LayerMask.GetMask("inWorld");
//         //could add multiple players for local coop instead
//         characters.Add(player);
//         //TODO: find a good time interval to run this (very fast for testing currently)
//         InvokeRepeating("locateGrid", 0, 0.01f);
//     }
//     //TODO: make a maximum distance from the player that this can go to avoid infinite loops
//     //TODO: could remove checks inside of known area
//     void locateGrid()
//     {
//         foreach (worldLocation location in objectLocations)
//         {
//             Debug.Log(location.x + " " + location.z);
//         }
//         //use this to reset grid size each round of checks
//         maxX = 25;
//         maxZ = 25;
//         //locates grid at player when not in combat, takes other characters into account when in combat
//         //check player's inCombat variable
//         //this should allow for multiple players
//         inCombat = player.GetComponent<playerState>().inCombat;
//         if (inCombat)
//         {
//             this.transform.position = GetNearestPointOnGrid(player.transform.position);
//             playerLocation = player.GetComponent<playerMovement>().combatLocation;
//             //check a box of 3 squares around each character, add characters in those areas to the list of
//             //current characters
//             searching = true;
//             while (searching)
//             {
//                 foreach (GameObject character in characters)
//                 {
//                     Vector3 boxSize = new Vector3(size * distance, size * distance, size * distance);
//                     Collider[] hitColliders = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity, affectsCombat);
//                     foreach (Collider newChar in hitColliders)
//                     {
//                         if (!characters.Contains(newChar.gameObject))
//                         {
//                             characters.Add(newChar.gameObject);
//                             objectLocations.Add(transformToGridIndices(newChar.gameObject.transform.position));
//                         }
//                     }
//                     //need to re-search the list if new chars added
//                     break;
//                 }
//                 //if finished searching all chars with no new ones, end check
//                 searching = false;
//             }
//             //adjusting maxX, maxZ 
//             foreach (GameObject item in characters)
//             {
//                 //if characters are out of grid bounds, resize grid
//                 //make grid bounds a multiple of size and add extra to edges
//                 if (Mathf.Abs(player.transform.position.x - item.transform.position.x) > maxX)
//                 {
//                     maxX = Mathf.RoundToInt(Mathf.Abs(player.transform.position.x - item.transform.position.x));
//                     maxX = Mathf.RoundToInt(maxX / size) * size + 20 * size;

//                 }
//                 if (Mathf.Abs(player.transform.position.z - item.transform.position.z) > maxZ)
//                 {
//                     maxZ = Mathf.RoundToInt(Mathf.Abs(player.transform.position.z - item.transform.position.z));
//                     maxZ = Mathf.RoundToInt(maxZ / size) * size + 20 * size;
//                 }
//             }
//             //TODO: possibly - create array relative to player for moving 
//         }
//         else
//         {//do every second while not in combat

//         }
//     }
//     public Vector3 GetNearestPointOnGrid(Vector3 position)
//     {
//         position -= transform.position;

//         int xCount = Mathf.RoundToInt(position.x / size);
//         int yCount = Mathf.RoundToInt(position.y / size);
//         int zCount = Mathf.RoundToInt(position.z / size);

//         Vector3 result = new Vector3(
//             (float)xCount * size,
//             (float)yCount * size,
//             (float)zCount * size);

//         result += transform.position;

//         return result;
//     }
//     public Vector3 transformToGridspace(Vector3 position)
//     {

//         int xCount = Mathf.RoundToInt(position.x / size);
//         int zCount = Mathf.RoundToInt(position.z / size);

//         Vector3 result = new Vector3(
//             (float)xCount * size,
//             position.y,
//             (float)zCount * size);

//         return result;
//     }
//     public worldLocation transformToGridIndices(Vector3 position)
//     {
//         worldLocation place = new worldLocation();
//         int xCount = Mathf.RoundToInt(position.x / size);
//         int zCount = Mathf.RoundToInt(position.z / size);
//         place.x = xCount;
//         place.z = zCount;
//         return place;
//     }

//     public Vector3 gridIndexToV3(worldLocation loc)
//     {
//         return new Vector3(loc.x, 0, loc.z);
//     }
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Vector3 boxSize = new Vector3(size * distance, size * distance, size * distance);
//         for (float x = -maxX; x < maxX; x += size)
//         {
//             for (float z = -maxZ; z < maxZ; z += size)
//             {
//                 var point = GetNearestPointOnGrid(new Vector3(x, 0f, z)) + transform.position;
//                 Gizmos.DrawSphere(point, 0.3f);
//             }

//         }
//         //Gizmos.DrawCube(transform.position, boxSize);
//     }

//     public bool isDirOpen(worldLocation loc, int dir)
//     {
//         //0 = down, 1 = right, 2 = up, 3 = left
//         if (dir == 0)
//         {
//             if (objectLocations.Contains(new worldLocation(loc.x - 1, loc.z)))
//             {
//                 return false;
//             }
//             else
//             {
//                 return true;
//             }
//         }
//         else if (dir == 1)
//         {
//             if (dir == 0)
//             {
//                 if (objectLocations.Contains(new worldLocation(loc.x, loc.z - 1)))
//                 {
//                     return false;
//                 }
//                 else
//                 {
//                     return true;
//                 }
//             }
//         }
//         else if (dir == 1)
//         {
//             if (dir == 0)
//             {
//                 if (objectLocations.Contains(new worldLocation(loc.x + 1, loc.z)))
//                 {
//                     return false;
//                 }
//                 else
//                 {
//                     return true;
//                 }
//             }
//         }
//         else
//         {
//             if (dir == 0)
//             {
//                 if (objectLocations.Contains(new worldLocation(loc.x, loc.z + 1)))
//                 {
//                     return false;
//                 }
//                 else
//                 {
//                     return true;
//                 }
//             }
//         }
//         return false;
//     }
// }