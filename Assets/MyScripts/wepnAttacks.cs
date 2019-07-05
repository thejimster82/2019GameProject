using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public atkStats chooseAtk()
    {
        return atkStatsList[0];
    }
}


