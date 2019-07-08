using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modSettings : MonoBehaviour
{
    public List<List<List<mod>>> mods = new List<List<List<mod>>>();
    public playerAttack plAtk;
    public void equipMod(mod mod, int wepn, int slot)
    {
        mods[wepn][slot].Add(mod);
    }

    public void unequipMod(mod mod, int wepn, int slot)
    {
        mods[wepn][slot].Remove(mod);
    }

    public void unequipModAtWepnIndex(int wepn)
    {
        mods.RemoveAt(wepn);
    }


    public void equipMods(List<List<List<mod>>> mods)
    {
        for (int wepn = 0; wepn < mods.Count; wepn++)
        {
            for (int slot = 0; slot < mods[wepn].Count; slot++)
            {
                for (int mod = 0; mod < mods[wepn][slot].Count; mod++)
                {
                    equipMod(mods[wepn][slot][mod], wepn, slot);
                }
            }
        }
    }

}
