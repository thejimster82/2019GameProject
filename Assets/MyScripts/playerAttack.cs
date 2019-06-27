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
    private bool isAttacking = false;
    private string prevAtkName;
    public string atkName;
    private atkStats currentAtk;
    public Dictionary<string, atkStats> atkTable = new Dictionary<string, atkStats>();

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponentInParent(typeof(playerMovement)) as playerMovement;
        wepnStats = wepn.GetComponent(typeof(wepnStats)) as wepnStats;
        //TODO: make a wrapper for all weapon-related scripts so we can just call equipWepn() and get everything
        atkTable = (wepn.GetComponent(typeof(wepnAttacks)) as wepnAttacks).getAtks();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Attack("melee1"));
        }
    }

    public bool getAtkStatus()
    {
        return isAttacking;
    }

    public IEnumerator Attack(string atkName)
    {
        currentAtk = atkTable[atkName];
        isAttacking = true;
        mover.setMovement(currentAtk.moveSpeedWhileAtking);
        wepn.transform.localPosition = mover.playerDir * currentAtk.atkDistance;
        wepn.transform.rotation = Quaternion.LookRotation(mover.playerDir);

        yield return new WaitForSeconds(currentAtk.hitDelay);

        wepn.SetActive(true);

        yield return new WaitForSeconds(currentAtk.atkLength);

        mover.setMovement(mover.defaultMoveSpeed);
        wepn.SetActive(false);
        isAttacking = false;
    }

}
