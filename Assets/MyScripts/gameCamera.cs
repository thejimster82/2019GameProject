using UnityEngine;
using System.Collections;
using System.Linq;

public class gameCamera : MonoBehaviour
{

    public float interpVelocity = 0.05f;
    public GameObject target;
    public Vector3 offset;
    private Camera cam;
    private Vector3[] rayChecks = new Vector3[3];
    public float visionDistance = 2;
    public opacityController opacCtrlr;
    public cameraShake shaker;
    //private LayerMask sceneryMask = LayerMask.GetMask("Scenery");
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
        cam = GetComponent<Camera>();
        shaker = GetComponent<cameraShake>();
        rayChecks[0] = new Vector3(-3, 0, 3);
        rayChecks[1] = new Vector3(3, 0, 3);
        rayChecks[2] = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, interpVelocity);
            Vector3 dirToPlayer = (target.transform.position - transform.position).normalized;
            Collider[] transparentizeThese = Physics.OverlapCapsule(transform.position, target.transform.position, visionDistance);
            Collider[] opaquenThese = Physics.OverlapCapsule(transform.position, target.transform.position, visionDistance + 1);
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
            //Debug.DrawRay(transform.position, dirToPlayer);
        }
    }
    public void ToggleShake(float shakeTime)
    {
        shaker.enabled = true;
        shaker.AddShakeTime(0.5f);
    }

    public void zoomIn()
    {

    }

    public void zoomOut()
    {

    }

    public void moveCamera(Vector3 movement)
    {

    }
}