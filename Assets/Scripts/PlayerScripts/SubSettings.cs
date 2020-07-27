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
    public SubSettings(string subName, int _hp, float _pingInterval, float _move, float _shootInt, float _resistence, float _basePingSpeed, float _basePingLifeTime, float _maxVelocity, Submarine _marine)
    {
        health = _hp;
        pingInterval = _pingInterval;
        movementSpeed = _move;
        shootInterval = _shootInt;
        resistence = _resistence;

        this.basePingBeginSpeed = _basePingSpeed;
        this.basePingLifeTime = _basePingLifeTime;
        this.maxVelocity = _maxVelocity;
        this.subName = subName;
        subMarine = _marine;
    }

    public string subName;

    public Submarine subMarine;

    public float health;
    public float pingInterval;
    public float movementSpeed;
    public float shootInterval;
    public float resistence;
    public float maxVelocity;

    public float basePingBeginSpeed;
    public float basePingLifeTime;
    
}

public enum Submarine
{
    basicSub,
    HeavySub,
    LightSub
}