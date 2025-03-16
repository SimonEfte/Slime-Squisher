using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapMechanics : MonoBehaviour
{
    public Rigidbody2D trapRigidbody;
    public Collider2D trapCollider;

    public Transform trap1, trap2, trap3, trap4;

    public OverlappingSounds overLappingScript;

    private void Awake()
    {
        GameObject audioTransform = GameObject.Find("OverlappingSounds");
        overLappingScript = audioTransform.GetComponent<OverlappingSounds>();

        trap1 = transform.Find("trap1");
        trap2 = transform.Find("trap2");
        trap3 = transform.Find("trap3");
        trap4 = transform.Find("trap4");
        
        trapRigidbody = gameObject.GetComponent<Rigidbody2D>();
        trapCollider = gameObject.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        trap1.gameObject.SetActive(true);
        trap2.gameObject.SetActive(false);
        trap3.gameObject.SetActive(false);
        trap4.gameObject.SetActive(false);

        slimeWalkedOver = false;
        trapLanded = false;
        trapCollider.enabled = false;
        trapRigidbody.constraints = RigidbodyConstraints2D.None;

        Vector2 randomForce = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1.2f, 1.8f)) * 0.8f;
        trapRigidbody.AddForce(randomForce, ForceMode2D.Impulse);
        StartCoroutine(TrapLand());
    }

    public bool trapLanded, slimeWalkedOver;

    IEnumerator TrapLand()
    {
        yield return new WaitForSeconds(0.7f);
        trapLanded = true;
        trapRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        trapCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trapLanded == true && slimeWalkedOver == false)
        {
            if (collision.gameObject.layer == 7)
            {
                slimeWalkedOver = true;
                StartCoroutine(BearTrapAnim());
            }
        }
    }
    
    IEnumerator BearTrapAnim()
    {
        overLappingScript.PlaySound(7, 0, false);
        yield return new WaitForSeconds(0.08f);

        trap1.gameObject.SetActive(false);
        trap2.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.025f);
        trap2.gameObject.SetActive(false);
        trap3.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.025f);
        trap3.gameObject.SetActive(false);
        trap4.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.07f);

        ObjectPool.instance.ReturnBearTrapToPool(gameObject);
    }
}
