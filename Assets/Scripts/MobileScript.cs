using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileScript : MonoBehaviour
{
    public static bool isMobile;
    public static bool isGooglePlay, isAppStore;

    public GameObject clickCollider, blockCollider;

    private void Awake()
    {
        isMobile = true;

        if (isMobile == true)
        {
            Application.targetFrameRate = 60;

            clickCollider.transform.localScale = new Vector2(2.8f, 2.8f);
            blockCollider.transform.localScale = new Vector2(1.4f, 1.4f);
        }
    }
}
