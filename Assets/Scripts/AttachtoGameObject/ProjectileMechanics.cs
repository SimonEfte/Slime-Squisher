using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMechanics : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(corotuine != null) { StopCoroutine(corotuine); }
            ObjectPool.instance.ReturnPaperClipFromPool(gameObject);
        }
    }

    public Coroutine corotuine;
    private void OnEnable()
    {
        corotuine =  StartCoroutine(SetBack());
    }

    IEnumerator SetBack()
    {
        yield return new WaitForSeconds(3);
        ObjectPool.instance.ReturnPaperClipFromPool(gameObject);
    }
}
