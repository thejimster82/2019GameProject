using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private CharacterController mover;
    public GameObject atkObject;
    private Vector3 atkLoc;
    public float atkDistance;
    public float atkTime;
    public float atkLength;
    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponentInParent(typeof(CharacterController)) as CharacterController;
        atkObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        atkObject.transform.localPosition = mover.velocity.normalized * atkDistance;
        //atkObject.transform.rotation =
        if (Input.GetMouseButtonDown(0))
        {
            atkObject.SetActive(true);
            atkTime = atkLength;
        }
        if (atkTime <= 0)
        {
            atkObject.SetActive(false);
        }
        atkTime -= Time.deltaTime;
    }

}
