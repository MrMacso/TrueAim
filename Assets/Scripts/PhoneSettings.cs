using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        
    }
}
