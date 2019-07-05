using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class gameCamera : MonoBehaviour
{

    public float interpVelocity = 0.05f;
    public Vector3 target;
    public GameObject player;
    public Dictionary<int, GameObject> targetObjs = new Dictionary<int, GameObject>();
    public Vector3 offset;
    public float defaultSize;
    private Camera cam;
    public float visionDistance = 2;
    public opacityController opacCtrlr;
    public cameraShake shaker;

    void Start()
    {
        cam = GetComponent<Camera>();
        shaker = GetComponent<cameraShake>();

        targetObjs.Add(player.GetInstanceID(), player);
        target = FindCenterPoint(targetObjs);

        offset = transform.position - target;
        defaultSize = cam.orthographicSize;

        StartCoroutine(onScreenProcess());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        target = FindCenterPoint(targetObjs);
        transform.position = Vector3.Lerp(transform.position, target + offset, interpVelocity);
        Vector3 dirToTarget = (target - transform.position).normalized;
        Collider[] transparentizeThese = Physics.OverlapCapsule(transform.position, player.transform.position, visionDistance);
        Collider[] opaquenThese = Physics.OverlapCapsule(transform.position, player.transform.position, visionDistance + 1);
        foreach (Collider col in opaquenThese)
        {
            if (col.gameObject.tag == "Scenery")
            {
                if (transparentizeThese.Contains(col))
                {
                    opacCtrlr.Transparentize(col.transform.gameObject);
                }
                else
                {
                    opacCtrlr.Opaquen(col.transform.gameObject);
                }
            }
        }
        //Debug.DrawRay(transform.position, dirToTarget);
    }

    public IEnumerator onScreenProcess()
    {
        while (true)
        {
            if (!checkIfOnScreen(targetObjs))
            {
                zoomOut(5);
            }
            else
            {
                zoomIn();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ToggleShake(float shakeTime)
    {
        shaker.enabled = true;
        shaker.AddShakeTime(0.5f);
    }

    private Vector3 FindCenterPoint(Dictionary<int, GameObject> Objs)
    {
        if (Objs.Count == 0)
            return Vector3.zero;
        else if (Objs.Count == 1)
            return Objs.ElementAt(0).Value.transform.position;
        else
        {
            Vector3 bounds = new Vector3();
            foreach (GameObject obj in Objs.Values)
            {
                bounds += obj.transform.position;
            }
            return bounds / Objs.Count;
        }
    }

    private bool checkIfOnScreen(Dictionary<int, GameObject> Objs)
    {
        foreach (GameObject obj in Objs.Values)
        {
            Vector3 objLoc = cam.WorldToViewportPoint(obj.transform.position);
            float loBound = 0.15f;
            float hiBound = 0.85f;
            if (objLoc.x < loBound || objLoc.x > hiBound || objLoc.y < loBound || objLoc.y > hiBound || objLoc.z < 0)
            {
                return false;
            }
        }
        return true;
    }

    public void zoomIn()
    {
        if (Mathf.Abs(cam.orthographicSize - defaultSize) > 0.01)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, defaultSize, 0.01f);
        }
    }

    public void zoomOut(float amt)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize + amt, 0.03f);
    }

    public void moveCamera(Vector3 movement)
    {

    }
    public void addToTargets(GameObject obj)
    {
        if (!targetObjs.Values.Contains(obj))
        {
            targetObjs.Add(obj.GetInstanceID(), obj);
        }
    }
    public void rmFromTargets(GameObject obj)
    {
        targetObjs.Remove(obj.GetInstanceID());
        Debug.Log(targetObjs.Count);
    }
}