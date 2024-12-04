using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooMechanics : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SetGooBack());
    }

    IEnumerator SetGooBack()
    {
        yield return new WaitForSeconds(10);
        ObjectPool.instance.ReturnGooFromPool(gameObject);
    }
}
