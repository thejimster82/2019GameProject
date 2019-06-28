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
    private int combo = 0;
    private float atkSpeed = 1;
    private Coroutine currRoutine;
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
                currRoutine = StartCoroutine(Attack("melee", "atk1"));
                Debug.Log(1);
            }
            else if (isAttacking && currAtk.getAtkName() == "atk1" && combo > 0)
            {
                combo -= 1;
                StopCoroutine(currRoutine);
                currRoutine = StartCoroutine(Attack("melee", "atk2"));
                Debug.Log(2);
            }
            else if (isAttacking && currAtk.getAtkName() == "atk2" && combo > 0)
            {
                combo -= 1;
                StopCoroutine(currRoutine);
                currRoutine = StartCoroutine(Attack("melee", "atk3"));
                Debug.Log(3);
            }
            // else
            // {
            //     //restart loop
            //     combo -= 1;
            //     StopCoroutine(currRoutine);
            //     currRoutine = StartCoroutine(Attack("melee", "atk1"));
            //     Debug.Log(1);
            // }

        }
    }

    public bool getAtkStatus()
    {
        return isAttacking;
    }

    public IEnumerator Attack(string type, string atkName)
    {
        mover.pauseDashing();
        isAttacking = true;
        currAtk = atkTable[atkName];
        mover.nudgePlayer(currAtk.nudgeAmt);
        mover.setMovement(currAtk.moveSpeedWhileAtking);
        equippedWepn.transform.rotation = Quaternion.LookRotation(mover.getPlayerDir());
        equippedWepn.transform.position = mover.getPlayerDir() * currAtk.atkDistance + mover.transform.position;

        yield return new WaitForSeconds(currAtk.hitDelay);

        StartCoroutine(AttackZone(currAtk.atkObject));

        yield return new WaitForSeconds((currAtk.atkLength - currAtk.hitDelay) * 1 / 2 * (1 / atkSpeed));

        mover.setMovement(mover.defaultMoveSpeed);
        mover.resumeDashing();
        combo += 1;

        yield return new WaitForSeconds((currAtk.atkLength - currAtk.hitDelay) * 1 / 2 * (1 / atkSpeed));

        isAttacking = false;
        combo -= 1;
    }

    public IEnumerator AttackZone(GameObject atkObject)
    {
        GameObject currAtkObj = Instantiate(atkObject, equippedWepn.transform.position, equippedWepn.transform.rotation);
        currAtkObj.SetActive(true);
        currAtkObj.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.01f);
        Destroy(currAtkObj);

    }

    private void equipWepn(GameObject wepn)
    {
        wepnStats = wepn.GetComponent(typeof(wepnStats)) as wepnStats;
        atkTable = (wepn.GetComponent(typeof(wepnAttacks)) as wepnAttacks).getAtks();
        equippedWepn = wepn;
    }

}
