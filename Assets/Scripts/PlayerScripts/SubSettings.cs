using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classes with the values of all submarine settings that can be changed
/// </summary>
[Serializable]

public class SubBaseSettings
{

    public float health;
    public float pingInterval;
    public float resistence;

    public float basePingBeginSpeed;
    public float basePingLifeTime;

    public SubBaseSettings(float _hp, float _pingInterval, float _waterResistance, float _basePingSpeed, float _basePingLife)
    {
        health = _hp;
        pingInterval = _pingInterval;
        resistence = _waterResistance;
        basePingBeginSpeed = _basePingSpeed;
        basePingLifeTime = _basePingLife;
    }
}



public class SubEnineSettings
{

    public float acceleration;
    public float maxVelocity;
    public SubEnineSettings(float _acc, float _maxSpeed)
    {
        acceleration = _acc;
        maxVelocity = _maxSpeed;
    }
}

public class SubCannonSettings
{

    public float shootInterval;
    public SubCannonSettings(float _shootInterval)
    {
        shootInterval = _shootInterval;
    }
}


