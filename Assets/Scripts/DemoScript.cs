using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public static bool isDemo;
    public static bool isLocalizationDone;

    private void Awake()
    {
        isLocalizationDone = true;

        isDemo = false;
        if(isDemo == true)
        {

        }
    }
}
