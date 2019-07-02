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
    //private LayerMask sceneryMask = LayerMask.GetMask("Scenery");
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
        cam = GetComponent<Camera>();
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
    //TODO: make enemy not go right up to person when they have weapon
    //TODO: make walls change transparency when they are in front of / around you
    //TODO: make attack objects affect player
    //TODO: make player attacks affect enemy
    public void ToggleShake(float shakeTime)
    {
        //this.shakeTimer.StartTimer (shakeTime);
        //myslf.shakeActive = toggleValue;
        //if (!toggleValue) {
        //	myslf.targetCamera.transform.localPosition=myslf.camLocalPos;
        //}
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