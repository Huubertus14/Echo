using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum SubBaseType
{
    Medium,
    Heavy,
    Light,
    None
}

public enum SubEngineType
{

    Light,
    Medium,
    Heavy,
    None,
}

public enum SubCannonType
{
    Torpedo,
    Minigun,
    Ram,
    None
}

public enum SubSpecialType
{
    None
}

public static class SubValues
{
    private static Dictionary<SubBaseType, SubBaseSettings> subBaseSettings = new Dictionary<SubBaseType, SubBaseSettings>
    {
        {SubBaseType.Medium, new SubBaseSettings(50, .9f, .95f, 22, 1.8f) },
        {SubBaseType.Heavy, new SubBaseSettings(90, 1.5f, .9f, 22, 2.5f) },
        {SubBaseType.Light, new SubBaseSettings(30, .6f, .99f, 22, 1.2f) }
    };

    private static Dictionary<SubEngineType, SubEnineSettings> subEngineSettings = new Dictionary<SubEngineType, SubEnineSettings>
    {
        { SubEngineType.Medium, new SubEnineSettings(10, 50)},
        { SubEngineType.Heavy, new SubEnineSettings(7, 65)},
        { SubEngineType.Light, new SubEnineSettings(20, 20)}
    };

    private static Dictionary<SubCannonType, SubCannonSettings> subCannonSettings = new Dictionary<SubCannonType, SubCannonSettings>
    {
        {SubCannonType.Minigun, new SubCannonSettings(0.05f, 3.2f, 0.05f) },
         {SubCannonType.Torpedo, new SubCannonSettings(2.4f, 15f, 1.5f) },
          {SubCannonType.Ram, new SubCannonSettings(0.0f, 28f, 5f) },
    };

    public static SubBaseSettings GetBaseSettings(SubBaseType _type)
    {
        if (subBaseSettings.TryGetValue(_type, out SubBaseSettings value))
        {
            return value;
        }
        Debug.LogError("Subbase values not found");
        return new SubBaseSettings(1, 100, 100, 100, 0);
    }

    public static SubEnineSettings GetEngineSettings(SubEngineType _type)
    {
        if (subEngineSettings.TryGetValue(_type, out SubEnineSettings value))
        {
            return value;
        }
        Debug.LogError("Subengine values not found");
        return new SubEnineSettings(0, 0);
    }

    public static SubCannonSettings GetCannonSettings(SubCannonType _type)
    {
        if (subCannonSettings.TryGetValue(_type, out SubCannonSettings value))
        {
            return value;
        }
        Debug.LogError("Subcannon values not found");
        return new SubCannonSettings(100, 0, 0);
    }


}
