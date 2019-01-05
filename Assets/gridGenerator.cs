using UnityEngine;

public class gridGenerator : MonoBehaviour
{
    public float size = 1f;
    public int minX = -25;
    public int maxX = 25;
    public int minZ = -25;
    public int maxZ = 25;


    void start()
    {

        InvokeRepeating("locateGrid", 0f, 1.0f);
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

    public void locateGrid()
    {
        Vector3 test1 = new Vector3(0, 0, 0);
        Vector3 test2 = new Vector3(10, 0, 10);
        Vector3[] charLocations = { test1, test2 };
        this.transform.position = charLocations[1];
    }
}