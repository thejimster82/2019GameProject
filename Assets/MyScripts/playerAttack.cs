using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerAttack : MonoBehaviour
{
    private CharacterController mover;
    public GameObject attackObject;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponentInParent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackObject.transform.position = new Vector3(mover.velocity.x/mover.velocity.magnitude,0,mover.velocity.z/mover.velocity.magnitude);
        }
    }
    
}
