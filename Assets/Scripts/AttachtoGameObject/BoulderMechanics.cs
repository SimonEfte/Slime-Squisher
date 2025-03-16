using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMechanics : MonoBehaviour
{
    public Transform rockIcon;
    private Rigidbody2D rb;

    private void Awake()
    {
        rockIcon = transform.Find("RockIcon");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(AdjustRotationBasedOnDirection());
        StartCoroutine(SetRockOff());
    }

    private IEnumerator AdjustRotationBasedOnDirection()
    {
        while (true)
        {
            float rotateSpeed;

            if (rb.velocity.x > 0) 
            {
                rotateSpeed = -130;
            }
            else if (rb.velocity.x < 0)
            {
                rotateSpeed = 130;
            }
            else
            {
                rotateSpeed = 130;
            }

            rockIcon.gameObject.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SetRockOff()
    {
        yield return new WaitForSeconds(6);
        ObjectPool.instance.ReturnBoulderFromPool(gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
