using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public bool isKnife;

    public bool isChainBall, isBlade;

    public float rotateSpeed;

    private void Awake()
    {
        if (isKnife) { rotateSpeed = 140f; }

    }
    private void OnEnable()
    {
        if(isChainBall == true || isBlade == true) { rotateSpeed = PickUpgrade.chainBallSpeed; }
     
        StartCoroutine(Rotate(rotateSpeed));
    }

    private void Update()
    {
        if(isChainBall == true || isBlade == true)
        {
            rotateSpeed = PickUpgrade.chainBallSpeed;
        }

        gameObject.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private IEnumerator Rotate(float speed)
    {
        while (true)
        {

            yield return null;
        }
    }
}
