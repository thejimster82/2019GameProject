using UnityEngine;
using System.Collections;

public class LerpFollowPlayer : MonoBehaviour
{

    public float interpVelocity = 0.05f;
    public GameObject target;
    public Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            if (target.GetComponent<playerState>().inCombat) //movement in combat
            {

            }
            else
            { //movement out of combat
                transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, interpVelocity);
            }
        }

    }
}