using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private playerMovement mover;
    private wepnStats wepnStats;
    private Vector3 atkLoc;
    public GameObject wepn;
    public float atkTime;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponentInParent(typeof(playerMovement)) as playerMovement;
        wepnStats = wepn.GetComponent(typeof(wepnStats)) as wepnStats;
    }

    // Update is called once per frame
    void Update()
    {
        wepn.transform.localPosition = mover.playerDir * wepnStats.atkDistance;

        //atkObject.transform.rotation =
        if (Input.GetMouseButtonDown(0))
        {
            wepn.SetActive(true);
            atkTime = wepnStats.atkLength;
        }
        if (atkTime <= 0)
        {
            wepn.SetActive(false);
        }
        atkTime -= Time.deltaTime;
    }

}
