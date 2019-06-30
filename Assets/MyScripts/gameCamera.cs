using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour
{

    public float interpVelocity = 0.05f;
    public GameObject target;
    public Vector3 offset;
    private Camera cam;
    private Vector3[] rayChecks = new Vector3[3];
    public opacityController opacCtrlr;
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
            foreach (Vector3 rayDir in rayChecks)
            {
                Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position + rayDir);
                Ray ray = cam.ScreenPointToRay(screenPos);
                RaycastHit hit = new RaycastHit();
                Physics.Raycast(ray.origin, ray.direction, out hit);
                if (hit.collider != null && hit.collider.tag == "Scenery")
                {
                    Debug.Log(hit.transform.name);
                    opacCtrlr.Transparentize(hit.transform.gameObject);
                    //TODO: change this when we switch to sprites
                }
                Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);
            }
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