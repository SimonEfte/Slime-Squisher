using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMechanics : MonoBehaviour
{
    public bool isArrow;
    public Transform arrowShadow;
    public float shadowMoveSpeed;

    private void Awake()
    {
        if (isArrow)
        {
            arrowShadow = transform.Find("ArrowShadow");
        }
    }

    private void OnEnable()
    {
        if (isArrow == true)
        {
            arrowShadow.transform.localPosition = new Vector2(-23, -380);
            StartCoroutine(MoveShadow(arrowShadow.gameObject));
        }
    }

    IEnumerator MoveShadow(GameObject shadow)
    {
        Vector2 moveToPos = new Vector2(-23, 44);

        while ((Vector2)shadow.transform.localPosition != moveToPos)
        {
                shadow.transform.localPosition = Vector2.MoveTowards(
                shadow.transform.localPosition,
                moveToPos,
                shadowMoveSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}
