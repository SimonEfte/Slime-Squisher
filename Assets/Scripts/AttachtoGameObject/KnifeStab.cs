using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStab : MonoBehaviour
{
    public CursorMechanics cursorMechanicsScript;

    public static bool isAnySlimesClose;

    public Vector2 originalPos;
    public Transform handPos;

    private void OnEnable()
    {
        knifeCollider.enabled = false;
        originalPos = new Vector2(-44, -20);
        gameObject.transform.localPosition = originalPos;
        gameObject.transform.localScale = new Vector2(0.56f, 0.56f);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 130);

        StartCoroutine(StabKnife());
    }

    IEnumerator StabKnife()
    {
        float stabWait = 0f;
        float stabTime = 3f;

        while (true)
        {
            while (stabWait < stabTime)
            {
                stabWait += Time.deltaTime;
                yield return null;
            }

            stabWait = 0;
            StabTheSlime();
        }
    }

    public void StabTheSlime()
    {
        isAnySlimesClose = false;
        cursorMechanicsScript.SelectRandomActiveSlime(2);

        if (isAnySlimesClose == true) { StartCoroutine(MoveKnife()); }
    }

    public Transform canvasParent, strawberryParent;

    public float extraRotation, moveBackRoatation;

    public Collider2D knifeCollider;

    IEnumerator MoveKnife()
    {
        knifeCollider.enabled = true;

        gameObject.transform.SetParent(canvasParent);

        Vector2 startPos = gameObject.transform.position;
        Vector2 targetPos = CursorMechanics.knifeHitPos;

        float speed = 17f; 
        float distanceToTarget = Vector2.Distance(startPos, targetPos);
        float moveDuration = distanceToTarget / speed; 

        float targetAngle = Mathf.Atan2(targetPos.y - startPos.y, targetPos.x - startPos.x) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(0, 0, targetAngle + extraRotation);

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration); // Ensure t stays in [0, 1]
            gameObject.transform.position = Vector2.Lerp(startPos, targetPos, t);

            yield return null;
        }

        gameObject.transform.position = targetPos;

        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration); // Ensure t stays in [0, 1]
            gameObject.transform.position = Vector2.Lerp(targetPos, handPos.transform.position, t);

            yield return null;
        }

        gameObject.transform.SetParent(strawberryParent);
        gameObject.transform.localPosition = originalPos;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 130);

        knifeCollider.enabled = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
