using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheCollider : MonoBehaviour
{
    public Collider2D collider2d;
    public Transform shadow;

    private void Awake()
    {
        shadow = transform.Find("Shadow");
        collider2d = gameObject.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        shadow.gameObject.SetActive(true);
        StartCoroutine(SetColliderOnAndOff());
        StartCoroutine(SetInactive());
    }

    IEnumerator SetColliderOnAndOff()
    {
        collider2d.enabled = false;

        float toggleInterval = 0.3333f; // Exact interval
        float nextToggleTime = Time.time;

        while (true)
        {
            // Wait until the next toggle time
            while (Time.time < nextToggleTime)
            {
                yield return null; // Wait for the next frame
            }

            collider2d.enabled = true;
            StartCoroutine(SetColliderOff());

            // Update the next toggle time
            nextToggleTime += toggleInterval;
        }
    }

    IEnumerator SetColliderOff()
    {
        yield return new WaitForSeconds(0.15f);
        collider2d.enabled = false;
    }

    IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(3);
        ObjectPool.instance.ReturnScyntheFromPool(gameObject);
    }

    private void OnDisable()
    {
        shadow.gameObject.SetActive(false);
        StopAllCoroutines();
    }

}
