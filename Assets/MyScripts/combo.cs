using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo : MonoBehaviour
{
    // Start is called before the first frame update
    public List<atkStats> atks = new List<atkStats>();
    public Dictionary<int, List<atkModifier>> mods = new Dictionary<int, List<atkModifier>>();
    public int numAtks = 5;
    void Start()
    {
        resetModifiers();
    }

    public void resetModifiers()
    {
        mods.Clear();
        for (int i = 0; i < numAtks; i++)
        {
            List<atkModifier> modifiers = new List<atkModifier>();
            mods[i] = modifiers;
        }
    }
    public void addMod(atkModifier mod)
    {
    }
    public atkStats applyMod(atkStats stats, List<atkModifier> mods)
    {
        return stats;
    }
}
