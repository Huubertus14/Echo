using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SubType
{
    BasicSub,
    HeavySub,
    MachineGunSub
}

public enum bulletTypes
{
    Torpedo,
    SmallBullet,
    HeavyBullet
}

public static class SubValues
{
    private static Dictionary<SubType, SubSettings> submarines = new Dictionary<SubType, SubSettings>
   {
       {SubType.BasicSub, new SubSettings(100, 0.8f,10, 1.2f, 0.91f) },
       {SubType.HeavySub, new SubSettings(250, 1.2f,5.5f, 3.6f, 0.99f) },
       {SubType.MachineGunSub, new SubSettings(75, 0.5f,13, 0.4f, 0.85f) }
   };

    public static SubSettings GetValues(SubType _sub)
    {
        if (submarines.TryGetValue(_sub, out SubSettings value))
        {
            return value;
        }
        Debug.LogError("Sub type does not exist in dict");
        return new SubSettings(99, 99, 99, 99, 0);
    }
}
