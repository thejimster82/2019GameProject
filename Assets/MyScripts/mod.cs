using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class mod : MonoBehaviour
{
    //mods all inherit from this
    public List<int> wepnApply;
    public List<int> slotApply;
    public List<Tuple<int, int>> Applications;
    public atkStats applyTo(atkStats stats)
    {
        return stats;
    }
    public void setAppliedTo(int wepn, int slot)
    {
        Tuple<int, int> app = Tuple.Create(wepn, slot);
        Applications.Add(app);
    }
    public void removeAppliedTo(Tuple<int, int> app)
    {
        Applications.Remove(app);
    }
    public void removeAppliedTo(int wepn, int slot)
    {
        Tuple<int, int> app = Tuple.Create(wepn, slot);
        Applications.Remove(app);
    }
}
