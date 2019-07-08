using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class atkStats : MonoBehaviour
{
    public float atkDistance;
    public float atkLength;
    public float hitDelay;
    public float moveSpeedWhileAtking;
    public bool nudge;
    public float nudgeAmt;
    public GameObject atkObject;
    public Animation atkAnim;
    public bool shake;
    public float shakeAmt;
    //TODO: move mods to the modSettings instead, also need to change where mods come from in wepnAttacks/combo
    public List<mod> mods = new List<mod>();
    public string getAtkName()
    {
        return gameObject.name;
    }
    public void removeModApplicationsToWepn(int wepn)
    {
        foreach (mod mod in mods)
        {
            foreach (Tuple<int, int> app in mod.Applications)
            {
                if (app.Item1 == wepn)
                {
                    mod.removeAppliedTo(app);
                }
            }
        }
    }

    public void removeModApplicationsToAtk(int wepn, int slot)
    {
        foreach (mod mod in mods)
        {
            mod.removeAppliedTo(wepn, slot);
        }
    }
}

