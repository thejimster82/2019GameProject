using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private playerMovement mover;
    private wepnStats wepnStats;
    public GameObject wepn1;
    public GameObject equippedWepn;
    private bool isAttacking = false;
    private atkStats currAtk;
    private Dictionary<string, atkStats> atkTable = new Dictionary<string, atkStats>();

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponentInParent(typeof(playerMovement)) as playerMovement;
        equipWepn(wepn1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack("melee", "atk1"));
            }
            else if (isAttacking && currAtk.getAtkName() == "atk1")
            {
                StartCoroutine(Attack("melee", "atk2"));
            }
            else if (isAttacking && currAtk.getAtkName() == "atk2")
            {
                StartCoroutine(Attack("melee", "atk3"));
            }

        }
    }

    public bool getAtkStatus()
    {
        return isAttacking;
    }

    public IEnumerator Attack(string type, string atkName)
    {
        isAttacking = true;
        currAtk = atkTable[atkName];
        mover.setMovement(currAtk.moveSpeedWhileAtking);
        equippedWepn.transform.rotation = Quaternion.LookRotation(mover.getPlayerDir());
        equippedWepn.transform.position = mover.getPlayerDir() * currAtk.atkDistance + mover.transform.position;

        yield return new WaitForSeconds(currAtk.hitDelay);

        GameObject currAtkObj = Instantiate(currAtk.atkObject, equippedWepn.transform.position, equippedWepn.transform.rotation);
        currAtkObj.SetActive(true);
        currAtkObj.transform.parent = gameObject.transform;

        yield return new WaitForSeconds(currAtk.atkLength);

        mover.setMovement(mover.defaultMoveSpeed);
        Destroy(currAtkObj);
        isAttacking = false;
    }

    private void equipWepn(GameObject wepn)
    {
        wepnStats = wepn.GetComponent(typeof(wepnStats)) as wepnStats;
        atkTable = (wepn.GetComponent(typeof(wepnAttacks)) as wepnAttacks).getAtks();
        equippedWepn = wepn;
    }

}
