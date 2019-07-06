using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerAttack : MonoBehaviour
{
    private playerMovement mover;
    private wepnStats wepnStats;
    public GameObject wepn1;
    public GameObject wepn2;
    public List<wepnAttacks> equippedWepns;
    private bool isAttacking = false;
    private atkStats currAtk;
    private float atkSpeed = 1;
    private Coroutine atkRoutine;
    public int maxWepnsEquipped = 2;
    public gameCamera cam;
    public combo cb;
    public Tuple<atkStats, combo> atkInfo;
    private Dictionary<string, atkStats> atkTable = new Dictionary<string, atkStats>();

    // Start is called before the first frame update
    void Start()
    {
        cb = GetComponentInParent(typeof(combo)) as combo;
        mover = GetComponentInParent(typeof(playerMovement)) as playerMovement;
        equipWepn(wepn1);
        equipWepn(wepn2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isAttacking = true;
                atkInfo = equippedWepns[0].chooseAtk(cb, 0);
                currAtk = atkInfo.Item1;
                cb = atkInfo.Item2;
                if (atkRoutine != null)
                {
                    StopCoroutine(atkRoutine);
                }
                if (cb.mods.Count > cb.numAtks)
                {
                    atkRoutine = StartCoroutine(Attack(currAtk, cb.mods[cb.numAtks], equippedWepns[1]));
                }
                else
                {
                    atkRoutine = Attack(currAtk, equippedWepns[0]);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                isAttacking = true;
                atkInfo = equippedWepns[1].chooseAtk(cb, 1);
                currAtk = atkInfo.Item1;
                cb = atkInfo.Item2;
                if (atkRoutine != null)
                {
                    StopCoroutine(atkRoutine);
                }
                if (cb.mods.Count > cb.numAtks)
                {
                    atkRoutine = StartCoroutine(Attack(currAtk, cb.mods[cb.numAtks], equippedWepns[1]));
                }
                else
                {
                    atkRoutine = Attack(currAtk, equippedWepns[1]);
                }
            }
            // if (Input.GetMouseButton(0))
            // {
            //     isAttacking = true;
            // }
        }

    }

    public bool getAtkStatus()
    {
        return isAttacking;
    }

    public Coroutine Attack(atkStats atk, wepnAttacks wepn)
    {
        return StartCoroutine(Attack(currAtk, null, equippedWepns[1]));
    }
    public IEnumerator Attack(atkStats atk, List<atkModifier> mods, wepnAttacks wepn)
    {
        mover.pauseDashing();
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

        yield return new WaitForSeconds((currAtk.atkLength - currAtk.hitDelay) * (1 / atkSpeed));
        mover.setMovement(mover.defaultMoveSpeed);
        mover.resumeDashing();
        isAttacking = false;
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
