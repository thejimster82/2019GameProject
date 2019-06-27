﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkStats : MonoBehaviour
{
    public float atkDistance;
    public float atkLength;
    public float hitDelay;
    public float moveSpeedWhileAtking;
    public GameObject atkObject;
    public string atkName;

    void Awake()
    {
        atkName = gameObject.name;
    }

    public string getAtkName()
    {
        return atkName;
    }
}
