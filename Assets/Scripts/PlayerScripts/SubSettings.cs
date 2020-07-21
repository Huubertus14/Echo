using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classes with the values of all submarine settings that can be changed
/// </summary>
[Serializable]
public class SubSettings
{
    public SubSettings(string subName, int _hp, float _pingInterval, float _move, float shootInt, float _resistence, float basePingSpeed, float basePingLifeTime, float maxVelocity)
    {
        health = _hp;
        pingInterval = _pingInterval;
        movementSpeed = _move;
        shootInterval = shootInt;
        resistence = _resistence;

        this.basePingBeginSpeed = basePingSpeed;
        this.basePingLifeTime = basePingLifeTime;
        this.maxVelocity = maxVelocity;
        this.subName = subName;
    }

    public string subName;

    public float health;
    public float pingInterval;
    public float movementSpeed;
    public float shootInterval;
    public float resistence;
    public float maxVelocity;

    public float basePingBeginSpeed;
    public float basePingLifeTime;
    
}
