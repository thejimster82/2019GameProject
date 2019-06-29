using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour
{

    public float interpVelocity = 0.05f;
    public GameObject target;
    public Vector3 offset;
    private Camera cam;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
        cam = GetComponent<Camera> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, interpVelocity);
        }
    }
    //TODO: make enemy go to closest point instead of last point
    //TODO: make enemy not go right up to person when they have weapon
    //TODO: make walls change transparency when they are in front of / around you
    //TODO: make attack objects affect player
    //TODO: make player attacks affect enemy
    public void ToggleShake(float shakeTime){
		//this.shakeTimer.StartTimer (shakeTime);
		//myslf.shakeActive = toggleValue;
		//if (!toggleValue) {
		//	myslf.targetCamera.transform.localPosition=myslf.camLocalPos;
		//}
	}

    public void zoomIn(){

    }

    public void zoomOut(){

    }
}