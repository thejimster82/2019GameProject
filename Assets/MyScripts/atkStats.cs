using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkStats : MonoBehaviour
{
    public float atkDistance;
    public float atkLength;
    public float hitDelay;
    public float moveSpeedWhileAtking;
    public float nudgeAmt;
    public GameObject atkObject;
    public string getAtkName()
    {
        return gameObject.name;
    }
}

