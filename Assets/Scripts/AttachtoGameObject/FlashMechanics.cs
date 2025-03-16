using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashMechanics : MonoBehaviour
{
    private ParticleSystem flashParticle;

    private void Awake()
    {
        flashParticle = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        flashParticle.Play();
        StartCoroutine(CheckIfParticleFinished());
    }

    IEnumerator CheckIfParticleFinished()
    {
        yield return new WaitForSeconds(0.3f);
        ReturnTheFlash();
    }

    private void ReturnTheFlash()
    {
        ObjectPool.instance.ReturnShootFlashToPool(gameObject);
    }
}
