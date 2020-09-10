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


[Serializable]
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
[Serializable]
public class SubCannonSettings
{
    public float shootInterval;
    public float baseDamage;
    public float bounceForce;
    public SubCannonSettings(float _shootInterval, float _baseDamage, float _bounceForce)
    {
        shootInterval = _shootInterval;
        baseDamage = _baseDamage;
        bounceForce = _bounceForce;
    }
}


