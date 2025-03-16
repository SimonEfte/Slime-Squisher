using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooMechanics : MonoBehaviour
{
    public Animation anim;
    public bool isParent, isChild, isSpike;

    public Transform[] goo;
    public Transform spike;

    private SpriteRenderer spriteRenderer;

    public bool isGreenGoo, isBlueGoo, isOrangeGoo, isRedGoo, isPurpleGoo;

    private void Awake()
    {
        if (isParent) 
        { 
            goo[0] = transform.Find("goo1");
            goo[1] = transform.Find("goo2");
            goo[2] = transform.Find("goo3");

            spike = transform.Find("spike");
        }
        else
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        anim = gameObject.GetComponent<Animation>();
    }

    private void OnEnable()
    {
        if (isParent) 
        {
            if(PickUpgrade.choseSpikes == true)
            {
                spike.gameObject.SetActive(true);
            }
            else
            {
                spike.gameObject.SetActive(false);
            }

            goo[0].gameObject.SetActive(false);
            goo[1].gameObject.SetActive(false);
            goo[2].gameObject.SetActive(false);

            int random = Random.Range(0, 3);
            goo[random].gameObject.SetActive(true);
            StartCoroutine(SetGooBack(true));
        }
        else 
        {
            if(isSpike == false)
            {
                anim.Play("goo");
            }
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
            StartCoroutine(SetGooBack(false)); 
        }
    }

    IEnumerator SetGooBack(bool returnToObjectPool)
    {
        yield return new WaitForSeconds(15);
        if(returnToObjectPool == false) { anim.Play("gooFadeOut"); }
        yield return new WaitForSeconds(1.5f);
        if (returnToObjectPool == false) { gameObject.SetActive(false); }
        else 
        {
            if (isGreenGoo) { ObjectPool.instance.ReturnGooFromPool(gameObject); }
            else if (isBlueGoo) { ObjectPool.instance.ReturnBlueGooToPool(gameObject); }
            else if (isOrangeGoo) { ObjectPool.instance.ReturnOrangeGooToPool(gameObject); }
            else if (isRedGoo) { ObjectPool.instance.ReturnRedGooToPool(gameObject); }
            else if (isPurpleGoo) { ObjectPool.instance.ReturnPurpleGooToPool(gameObject); }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
