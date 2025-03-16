using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFlashMechanics : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SetBack());
    }

    IEnumerator SetBack()
    {
        yield return new WaitForSeconds(1);
        ObjectPool.instance.ReturnShootFlashToPool(gameObject);
    }
}
