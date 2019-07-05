using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private playerMovement mover;
    private wepnStats wepnStats;
    public GameObject wepn1;
    public GameObject wepn2;
    public List<wepnAttacks> equippedWepns;
    private bool isAttacking = false;
    private atkStats currAtk;
    private int combo = 0;
    private float atkSpeed = 1;
    private Coroutine atkRoutine;
    public int maxWepnsEquipped = 2;
    public gameCamera cam;
    private Dictionary<string, atkStats> atkTable = new Dictionary<string, atkStats>();

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponentInParent(typeof(playerMovement)) as playerMovement;
        equipWepn(wepn1);
        equipWepn(wepn2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // if (!isAttacking)
            // {
            //     atkRoutine = StartCoroutine(Attack("melee", "atk1"));
            //     Debug.Log(1);
            // }
            // else if (isAttacking && currAtk.getAtkName() == "atk1" && combo > 0)
            // {
            //     combo -= 1;
            //     StopCoroutine(atkRoutine);
            //     atkRoutine = StartCoroutine(Attack("melee", "atk2"));
            //     Debug.Log(2);
            // }
            // else if (isAttacking && currAtk.getAtkName() == "atk2" && combo > 0)
            // {
            //     combo -= 1;
            //     StopCoroutine(atkRoutine);
            //     atkRoutine = StartCoroutine(Attack("melee", "atk3"));
            //     Debug.Log(3);
            // }
            currAtk = equippedWepns[0].chooseAtk();
            if (atkRoutine != null)
            {
                StopCoroutine(atkRoutine);
            }
            atkRoutine = StartCoroutine(Attack(currAtk, equippedWepns[0]));
        }
        if (Input.GetMouseButtonUp(1))
        {
            currAtk = equippedWepns[1].chooseAtk();
            if (atkRoutine != null)
            {
                StopCoroutine(atkRoutine);
            }
            atkRoutine = StartCoroutine(Attack(currAtk, equippedWepns[1]));
        }
    }

    public bool getAtkStatus()
    {
        return isAttacking;
    }

    public IEnumerator Attack(atkStats atk, wepnAttacks wepn)
    {
        mover.pauseDashing();
        isAttacking = true;
        if (currAtk.nudge)
        {
            mover.nudgePlayer(currAtk.nudgeAmt);
        }
        mover.setMovement(currAtk.moveSpeedWhileAtking);
        wepn.gameObject.transform.rotation = Quaternion.LookRotation(mover.getPlayerDir());
        wepn.gameObject.transform.position = mover.getPlayerDir() * currAtk.atkDistance + mover.transform.position;

        yield return new WaitForSeconds(currAtk.hitDelay);

        StartCoroutine(AttackZone(currAtk.atkObject, wepn));
        if (currAtk.shake)
        {
            cam.ToggleShake(currAtk.shakeAmt);
        }

        yield return new WaitForSeconds((currAtk.atkLength - currAtk.hitDelay) * 1 / 2 * (1 / atkSpeed));

        mover.setMovement(mover.defaultMoveSpeed);
        mover.resumeDashing();
        combo += 1;

        yield return new WaitForSeconds((currAtk.atkLength - currAtk.hitDelay) * 1 / 2 * (1 / atkSpeed));

        isAttacking = false;
        combo -= 1;
    }

    public IEnumerator AttackZone(GameObject atkObject, wepnAttacks wepn)
    {
        GameObject currAtkObj = Instantiate(atkObject, wepn.gameObject.transform.position, wepn.gameObject.transform.rotation);
        currAtkObj.SetActive(true);
        currAtkObj.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.01f);
        Destroy(currAtkObj);
    }

    //returns true if the weapon was successfully equipped
    private bool equipWepn(GameObject wepn)
    {
        if (equippedWepns.Count < maxWepnsEquipped)
        {
            equippedWepns.Add(wepn.GetComponent<wepnAttacks>());
            return true;
        }
        return false;
    }

    private void unequipWepn(GameObject wepn)
    {
        equippedWepns.Remove(wepn.GetComponent<wepnAttacks>());
    }

}
