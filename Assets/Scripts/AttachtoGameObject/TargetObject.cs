using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public static int targetNumber;

    private void Awake()
    {
        if(gameObject.name.Contains("TargetObject"))
        {
            targetNumber += 1;
            CursorMechanics.AddSlimeTargetObject(gameObject);
            gameObject.name = "TargetObject" + targetNumber;
        }
        else
        {
            CursorMechanics.AddSlime(gameObject);
        }
    }
}
