using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScytheShadow : MonoBehaviour
{
    private Vector2[] positions = new Vector2[]
    {
        new Vector2(1.4f, -1.4f),
        new Vector2(5f, 1.4f),
        new Vector2(1f, 5f),
        new Vector2(-2.2f, 2f),
        new Vector2(1.4f, -1.4f)
    };

    public float speed = 5f; // Adjust speed as needed
    private Coroutine moveCoroutine;


    private void OnEnable()
    {
        transform.localPosition = positions[0];
        moveCoroutine = StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        while (true)
        {
            for (int i = 1; i < positions.Length; i++)
            {
                yield return StartCoroutine(MoveToPosition(positions[i]));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        while ((Vector2)transform.localPosition != target)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDisable()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }
}
