using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingetonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T sp;

    public static T SP
    {
        get
        {
            if (sp == null)
            {
                // Search for existing instance.
                sp = (T)FindObjectOfType(typeof(T));

                // Create new instance if one doesn't already exist.
                if (sp == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    sp = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return sp;
        }
    }

    protected virtual void Awake()
    {
        if (sp == null)
        {
            // Search for existing instance.
            sp = (T)FindObjectOfType(typeof(T));

            // Create new instance if one doesn't already exist.
            if (sp == null)
            {
                // Need to create a new GameObject to attach the singleton to.
                var singletonObject = new GameObject();
                sp = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).ToString() + " (Singleton)";

                // Make instance persistent.
                DontDestroyOnLoad(singletonObject);
            }
        }
    }
}


