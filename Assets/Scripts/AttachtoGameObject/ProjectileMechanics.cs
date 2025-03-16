using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMechanics : MonoBehaviour
{
    public bool isPaperClip, isLaser, isPoisonDart, isThorn, isStaple, isSkull, isShadow, isKunai, isAntiSlimeBullet, isFrenzy, isSawblade, isKatana, isNail, isLog;
    bool stapleHit, nailHit;

    private Rigidbody2D rb;
    private Collider2D collider2d;
    public GameObject projcetileParent;

    public Transform thorn1, thorn2, thorn3;
    public Transform laser, thorn, staple, poisonDart, paperPlane, kunai, arrow, friendlyBullet;
    public Transform katanaTrailRenderer;

    public Transform logAnimator;

    private void Awake()
    {
        if (isLog)
        {
            logAnimator = transform.Find("LogIcon");
        }

        if(isKatana == true)
        {
            katanaTrailRenderer = transform.Find("trailrenderer");
        }

        if(isStaple == true || isNail)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            collider2d = gameObject.GetComponent<Collider2D>();
            projcetileParent = GameObject.Find("ProjectilesParent");
        }

        if (isThorn)
        {
            thorn1 = transform.Find("thorn1");
            thorn2 = transform.Find("thorn2");
            thorn3 = transform.Find("thorn3");
        }

        if (isFrenzy)
        {
            laser = transform.Find("Laser");
            thorn = transform.Find("Thorn");
            staple = transform.Find("Staple");
            poisonDart = transform.Find("PoisonDart");
            paperPlane = transform.Find("PaperPlane");
            kunai = transform.Find("Kunai");
            arrow = transform.Find("Arrow");
            friendlyBullet = transform.Find("FriendlyBullet");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(corotuine != null && isSawblade == false && isKatana == false) { StopCoroutine(corotuine); }

            if (isPaperClip == true) { ObjectPool.instance.ReturnPaperClipFromPool(gameObject); }
            else if (isLaser == true) { ObjectPool.instance.ReturnLaserFromPool(gameObject); }
            else if (isPoisonDart == true) { ObjectPool.instance.ReturnPoisonDartFromPool(gameObject); }
            else if (isThorn == true) { ObjectPool.instance.ReturnThornFromPool(gameObject); }
            else if (isShadow == true) { ObjectPool.instance.ReturnShadowToPool(gameObject); }
            else if (isKunai == true) { ObjectPool.instance.ReturnKunaiToPool(gameObject); }
            else if (isAntiSlimeBullet == true) { ObjectPool.instance.ReturnAntiSlimeBulletToPool(gameObject); }
            else if (isFrenzy == true) { ObjectPool.instance.ReturnFrenzyProjectileToPool(gameObject); }
          

            if (isStaple == true)
            {
                gameObject.transform.SetParent(collision.gameObject.transform);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                collider2d.enabled = false;
                stapleHit = true; 
            }

            if (isNail == true)
            {
                gameObject.transform.SetParent(collision.gameObject.transform);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                collider2d.enabled = false;
                nailHit = true;
            }
        }
    }

    public Coroutine corotuine;

    private void OnEnable()
    {
        nailHit = false;
        if (isLog == true)
        {
            logAnimator.GetComponent<Animator>().SetTrigger("PlayLogAnim");
        }

        if (isKatana)
        {
            katanaTrailRenderer.GetComponent<TrailRenderer>().enabled = true;
        }

        if (isThorn)
        {
            thorn1.gameObject.SetActive(false); thorn2.gameObject.SetActive(false); thorn3.gameObject.SetActive(false);

            int random = Random.Range(1,4);
            if(random == 1) { thorn1.gameObject.SetActive(true); }
            if (random == 2) { thorn2.gameObject.SetActive(true); }
            if (random == 3) { thorn3.gameObject.SetActive(true); }
        }

        if (isFrenzy)
        {
            thorn.gameObject.SetActive(false); laser.gameObject.SetActive(false); staple.gameObject.SetActive(false);
            poisonDart.gameObject.SetActive(false); paperPlane.gameObject.SetActive(false); kunai.gameObject.SetActive(false);
            arrow.gameObject.SetActive(false); friendlyBullet.gameObject.SetActive(false);

            int random = Random.Range(1, 9);
            if (random == 1) { thorn.gameObject.SetActive(true); }
            if (random == 2) { laser.gameObject.SetActive(true); }
            if (random == 3) { staple.gameObject.SetActive(true); }
            if (random == 4) { poisonDart.gameObject.SetActive(true); }
            if (random == 5) { paperPlane.gameObject.SetActive(true); }
            if (random == 6) { kunai.gameObject.SetActive(true); }
            if (random == 7) { arrow.gameObject.SetActive(true); }
            if (random == 8) { friendlyBullet.gameObject.SetActive(true); }
        }

        if (isStaple == true || isNail)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            collider2d.enabled = true;
        }

        stapleHit = false;
        corotuine = StartCoroutine(SetBack());
    }

    IEnumerator SetBack()
    {
        if(isSkull == false)
        {
            if (isKatana)
            {
                yield return new WaitForSeconds(0.35f);
                katanaTrailRenderer.GetComponent<TrailRenderer>().enabled = false;
            }

            if (isSawblade == true) { yield return new WaitForSeconds(5);  }
            else if (isLog) { yield return new WaitForSeconds(6); }
            else { yield return new WaitForSeconds(3); }

            if (isPaperClip == true) { ObjectPool.instance.ReturnPaperClipFromPool(gameObject); }
            else if (isLaser == true) { ObjectPool.instance.ReturnLaserFromPool(gameObject); }
            else if (isPoisonDart == true) { ObjectPool.instance.ReturnPoisonDartFromPool(gameObject); }
            else if (isThorn == true) { ObjectPool.instance.ReturnThornFromPool(gameObject); }
            else if (isShadow == true) { ObjectPool.instance.ReturnShadowToPool(gameObject); }
            else if (isStaple == true)
            {
                if (stapleHit == false)
                {
                    ObjectPool.instance.ReturnStapleToPool(gameObject);
                }
            }
            else if (isNail == true)
            {
                if (nailHit == false)
                {
                    ObjectPool.instance.ReturnNailFromPool(gameObject);
                }
            }
            else if (isKunai == true) { ObjectPool.instance.ReturnKunaiToPool(gameObject); }
            else if (isAntiSlimeBullet == true) { ObjectPool.instance.ReturnAntiSlimeBulletToPool(gameObject); }
            else if (isFrenzy == true) { ObjectPool.instance.ReturnFrenzyProjectileToPool(gameObject); }
            else if (isSawblade == true) { ObjectPool.instance.ReturnSawbladeToPool(gameObject);  }
            else if (isKatana == true) { ObjectPool.instance.ReturnKatanaToPool(gameObject); }
            else if (isLog == true) { ObjectPool.instance.ReturnLogToPool(gameObject); }
        }
        else
        {
            yield return new WaitForSeconds(0.81f);
            ObjectPool.instance.ReturnSkullToPool(gameObject);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
