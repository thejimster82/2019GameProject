using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class combo : MonoBehaviour
{
    // Start is called before the first frame update
    public List<atkStats> atks = new List<atkStats>();
    public List<List<List<mod>>> mods = new List<List<List<mod>>>();
    public List<List<List<mod>>> modEffects = new List<List<List<mod>>>();
    public playerAttack atker;
    public int currAtk = 0;
    void Start()
    {
        atker = GetComponentInParent(typeof(playerAttack)) as playerAttack;
    }

    public List<List<List<mod>>> resetModifiers()
    {
        List<List<List<mod>>> mods = new List<List<List<mod>>>();

        for (int wepn = 0; wepn < atker.getNumWepnsEquipped(); wepn++)
        {
            List<List<mod>> modsPerWepn = new List<List<mod>>();
            mods.Add(modsPerWepn);

            for (int slot = 0; slot < atker.getNumAtksForWepn(wepn); slot++)
            {
                List<mod> modifiers = new List<mod>();
                mods[wepn].Add(modifiers);
            }
        }
        return mods;
    }
    //add mods from current attack to list
    public void addMods(int currAtk, int currWep)
    {
        //add mods to their locations
        mods[currWep][currAtk].AddRange(atks[currAtk].mods);
        //add mod effects to correct places
        foreach (mod mod in mods[currWep][currAtk])
        {
            foreach (Tuple<int, int> App in mod.Applications)
            {
                modEffects[App.Item1][App.Item2].Add(mod);
            }
        }
    }
    //apply mods from previous attacks to the current attack
    public atkStats applyMods(int currAtk, int currWep)
    {
        foreach (mod mod in modEffects[currWep][currAtk])
        {
            atks[currAtk] = mod.applyTo(atks[currAtk]);
        }
        return atks[currAtk];
    }

}
