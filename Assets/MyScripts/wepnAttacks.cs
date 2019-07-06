using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wepnAttacks : MonoBehaviour
{
    private Dictionary<string, atkStats> atkTable = new Dictionary<string, atkStats>();
    public List<atkStats> atkStatsList = new List<atkStats>();
    public atkStats getAtk(string atkName)
    {
        return atkTable[atkName];
    }

    public Dictionary<string, atkStats> getAtks()
    {
        return atkTable;
    }

    void Start()
    {
        foreach (atkStats atk in atkStatsList)
        {
            atkTable.Add(atk.getAtkName(), atk);
        }
    }

    public Tuple<atkStats, combo> chooseAtk(combo cb, int wepNum)
    {
        if (cb.numAtks + 1 < atkStatsList.Count)
        {
            int newAtkNum = cb.numAtks + 1;
            cb.numAtks = newAtkNum;
            atkStats newAtk = atkStatsList[newAtkNum];
            cb.atks.Add(newAtk);
            cb.addMod(newAtk.mod);
            //atkStats newAtkModded = cb.applyMod(newAtk, cb.mods[cb.numAtks]);
            //return Tuple.Create(newAtkModded, cb);
            return Tuple.Create(newAtk, cb);
        }
        else
        {
            cb.numAtks = 0;
            cb.resetModifiers();
            cb.atks.Clear();
            cb.atks.Add(atkStatsList[0]);
            return Tuple.Create(atkStatsList[0], cb);
        }
    }
}


