using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBallMechanics : MonoBehaviour
{
    public Transform ballIcon;
    private Rigidbody2D rb;
    public CursorMechanics cursorMechanicsScript;
    public GameObject cmScript;

    public Animation bounceAnim;

    private void Awake()
    {
        cmScript = GameObject.Find("ClickObjectFollowCursor");
        cursorMechanicsScript = cmScript.GetComponent<CursorMechanics>();

        ballIcon = transform.Find("Ball_icon");
        rb = GetComponent<Rigidbody2D>();

        bounceAnim = ballIcon.gameObject.GetComponent<Animation>();
    }

    private void OnEnable()
    {
        timesBounced = 0;
        StartCoroutine(AdjustRotationBasedOnDirection());
        StartCoroutine(SetBallOff());

        ShootBouncyBall();

        bounceAnim.Play();
    }

    public void ShootBouncyBall()
    {
        if(timesBounced < 1) { cursorMechanicsScript.SelectRandomTargetObject(3); }

        Vector2 direction;

        if (timesBounced < 1) { direction = (CursorMechanics.bouncyBallTarget - CursorMechanics.bouncyBallStartPos).normalized; }
        else { direction = (CursorMechanics.bouncyBallTarget - bouncePos).normalized; }

        float speed = 6.25f;
        rb.velocity = direction * speed;
    }

    int timesBounced;
    Vector2 bouncePos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            timesBounced += 1;
            if (timesBounced > 3)
            {
                ObjectPool.instance.ReturnBouncyBallFromPool(gameObject);
            }
            else
            {
                float randomX = Random.Range(-1000f, 1000f);
                float randomY = Random.Range(-1000f, 1000f);
                bouncePos = new Vector2(randomX, randomY);
                ShootBouncyBall();
            }
        }
    }

    private IEnumerator AdjustRotationBasedOnDirection()
    {
        while (true)
        {
            float rotateSpeed;

            if (rb.velocity.x > 0)
            {
                rotateSpeed = -65;
            }
            else if (rb.velocity.x < 0)
            {
                rotateSpeed = 65;
            }
            else
            {
                rotateSpeed = 65;
            }

            ballIcon.gameObject.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SetBallOff()
    {
        yield return new WaitForSeconds(8);
        ObjectPool.instance.ReturnBouncyBallFromPool(gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
