using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wepnAttacks : MonoBehaviour
{
    public List<atkStats> atkStatsList = new List<atkStats>();
    public wepnStats wepnStats;
    public int numSlots;
    public playerAttack atker;
    private void Start()
    {
        wepnStats = GetComponentInParent(typeof(wepnStats)) as wepnStats;
    }
    public combo chooseAtk(combo cb, int wepNum)
    {
        if (cb.currAtk < atkStatsList.Count)
        {
            int newAtkNum = cb.currAtk;
            atkStats newAtk = atkStatsList[newAtkNum];
            cb.atks.Add(newAtk);
            cb.addMods(newAtkNum, wepNum);
            cb.applyMods(newAtkNum, wepNum);
            cb.currAtk += 1;
            return cb;
        }
        else
        {
            cb.resetModifiers();
            cb.atks.Clear();
            cb.atks.Add(atkStatsList[0]);
            cb.applyMods(0, wepNum);
            cb.currAtk = 1;
            return cb;
        }
    }

    public bool equipAtk(atkStats atk)
    {
        if (atkStatsList.Count < numSlots)
        {
            return equipAtk(atk, atkStatsList.Count);
        }
        return false;
    }

    public bool equipAtk(atkStats atk, int slot)
    {
        if (slot < numSlots)
        {
            atkStatsList.Insert(slot, atk);
            return true;
        }
        return false;
    }

    public void unequipAtk(int slot)
    {
        foreach (mod mod in atkStatsList[slot].mods)
        {
            mod.Applications.Clear();
        }
        Predicate<wepnAttacks> match = matchID;
        atker.removeModApplicationsToAtk(atker.equippedWepns.FindIndex(match), slot);
        atkStatsList.RemoveAt(slot);
    }

    public bool matchID(wepnAttacks wepn)
    {
        return wepn.GetInstanceID() == this.GetInstanceID();
    }

    public void removeModApplicationsToWepn(int wepn)
    {
        foreach (atkStats atk in atkStatsList)
        {
            atk.removeModApplicationsToWepn(wepn);
        }
    }

    public void removeModApplicationsToAtk(int wepn, int slot)
    {
        for (int i = 0; i < atkStatsList.Count; i++)
        {
            atkStatsList[i].removeModApplicationsToAtk(wepn, slot);
        }
    }
}


