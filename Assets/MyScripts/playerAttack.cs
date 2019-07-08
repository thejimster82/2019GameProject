using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class playerAttack : MonoBehaviour
{
    private playerMovement mover;
    /* #region   temporary*/
    public GameObject wepn1;
    public GameObject wepn2;
    /* #endregion */
    public List<wepnAttacks> equippedWepns = new List<wepnAttacks>();
    private bool isAttacking = false;
    private atkStats currAtk;
    private float atkSpeed = 1;
    private Coroutine atkRoutine;
    public int maxWepnsEquipped = 2;
    public gameCamera cam;
    public combo cb;

    // Start is called before the first frame update
    void Start()
    {
        cb = GetComponentInParent(typeof(combo)) as combo;
        mover = GetComponentInParent(typeof(playerMovement)) as playerMovement;
        for (int i = 0; i < maxWepnsEquipped; i++)
        {
            equippedWepns.Add(null);
        }
        equipWepn(wepn1, 0);
        equipWepn(wepn2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isAttacking = true;
                cb = equippedWepns[0].chooseAtk(cb, 0);
                currAtk = cb.atks.Last();
                if (atkRoutine != null)
                {
                    StopCoroutine(atkRoutine);
                }
                atkRoutine = StartCoroutine(Attack(currAtk, equippedWepns[0]));

            }
            if (Input.GetMouseButtonUp(1))
            {
                isAttacking = true;
                cb = equippedWepns[1].chooseAtk(cb, 1);
                currAtk = cb.atks.Last();
                if (atkRoutine != null)
                {
                    StopCoroutine(atkRoutine);
                }
                atkRoutine = StartCoroutine(Attack(currAtk, equippedWepns[1]));

            }
            //charge attack
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
    public IEnumerator Attack(atkStats atk, wepnAttacks wepn)
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

        yield return new WaitForSeconds((currAtk.atkLength - currAtk.hitDelay) * 1 / 2 * (1 / atkSpeed));
        mover.setMovement(mover.defaultMoveSpeed);
        mover.resumeDashing();
        isAttacking = false;
    }
    public IEnumerator AttackZone(GameObject atkObject, wepnAttacks wepnAtks)
    {
        GameObject currAtkObj = Instantiate(atkObject, wepnAtks.gameObject.transform.position, wepnAtks.gameObject.transform.rotation);
        currAtkObj.SetActive(true);
        currAtkObj.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.01f);
        Destroy(currAtkObj);
    }
    //returns true if the weapon was successfully equipped
    public int getNumWepnsEquipped()
    {
        return equippedWepns.Count;
    }

    public int getNumAtksForWepn(int wepn)
    {
        if (equippedWepns.ElementAtOrDefault(wepn) != null)
        {
            return equippedWepns[wepn].numSlots;
        }
        else
        {
            return 0;
        }
    }

    public bool equipWepn(GameObject wepnObj)
    {
        if (equippedWepns.Count < maxWepnsEquipped)
        {
            int wepn = equippedWepns.Count;
            return equipWepn(wepnObj, wepn);
        }
        return false;
    }

    public bool equipWepn(GameObject wepnObj, int wepn)
    {
        if (wepn < maxWepnsEquipped && equippedWepns[wepn] == null)
        {
            wepnAttacks wepnInfo = wepnObj.GetComponent<wepnAttacks>();
            equippedWepns[wepn] = wepnInfo;
            wepnInfo.atker = this;
            // for (int atk = 0; atk < wepnInfo.atkStatsList.Count; atk++)
            // {
            //     foreach (mod mod in wepnInfo.atkStatsList[atk].mods)
            //     {
            //         modInfo.equipMod(mod, wepn, atk);
            //     }
            // }
            cb.mods = cb.resetModifiers();
            cb.modEffects = cb.resetModifiers();
            return true;
        }
        return false;
    }

    public void unequipWepn(int wepn)
    {
        //remove mod applications to the unequipped weapon
        removeModApplicationsToWepn(wepn);
        equippedWepns[wepn].atker = null;
        equippedWepns.RemoveAt(wepn);
    }

    public void removeModApplicationsToAtk(int wepn, int slot)
    {
        for (int i = 0; i < equippedWepns.Count; i++)
        {
            if (i != wepn)
            {
                equippedWepns[i].removeModApplicationsToAtk(wepn, slot);
            }
        }
    }

    public void removeModApplicationsToWepn(int wepn)
    {
        for (int i = 0; i < getNumWepnsEquipped(); i++)
        {
            if (i != wepn)
            {
                equippedWepns[i].removeModApplicationsToWepn(wepn);
            }
        }
    }

}
