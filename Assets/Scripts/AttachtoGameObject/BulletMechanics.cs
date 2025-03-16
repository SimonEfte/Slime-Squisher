using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    public OverlappingSounds overlappingSound;
    public GameObject overlappingObject;

    public bool isBullet, isParticle;

    public Transform redBulletIcon, greenBulletIcon;

    public static bool legsKicked;
    public static Vector2 bulletPos;

    public Rigidbody2D rigidbody2d;
    public Collider2D bulletCollider;

    bool bulletKicked;

    private void Awake()
    {
        if(isBullet == true)
        {
            bulletCollider = gameObject.GetComponent<Collider2D>();

            rigidbody2d = gameObject.GetComponent<Rigidbody2D>();

            overlappingObject = GameObject.Find("OverlappingSounds");
            overlappingSound = overlappingObject.GetComponent<OverlappingSounds>();

            redBulletIcon = transform.Find("bulletIcon");
            greenBulletIcon = transform.Find("friendlyBulletIcon");
        }
    }

    bool stopped;
    Vector2 currentPos = new Vector2(0, 0);

    private void Update()
    {
        if(SelectGameMode.choseRampage == true && PickUpgrade.isInChooseUpgrade == true)
        {
            if (stopped == false)
            {
                currentPos = gameObject.transform.position;
            }
            gameObject.transform.position = currentPos;
            stopped = true;
        }
        else
        {
            stopped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBullet == true)
        {
            if (bulletKicked == true || isFriendlyBullet == true)
            {
                if (collision.gameObject.layer == 7)
                {
                    ObjectPool.instance.ReturnEnemyBulletFromPool(gameObject);
                }
            }

            if (isFriendlyBullet == false)
            {
                if (collision.gameObject.layer == 9)
                {
                    if (MainMenu.isInTut == false)
                    {
                        SpawnParticle();
                    }
                    ObjectPool.instance.ReturnEnemyBulletFromPool(gameObject);
                }
                if (collision.gameObject.layer == 11)
                {
                    if(SelectGameMode.choseRampage == true && PickUpgrade.isInChooseUpgrade == true) { }
                    else
                    {
                        ObjectPool.instance.ReturnEnemyBulletFromPool(gameObject);
                        SpawnParticle();
                    }
                }
                if (PickUpgrade.choseLegs)
                {
                    if (collision.gameObject.layer == 0)
                    {
                        if(collision.gameObject.name == "Legs")
                        {
                            bulletPos = gameObject.transform.localPosition;
                            int randomStuff = Random.Range(0,100);
                            if(randomStuff < 10) 
                            {
                                SpawnParticle();
                                ObjectPool.instance.ReturnEnemyBulletFromPool(gameObject);
                                legsKicked = true; 
                            }
                            else if (randomStuff > 87)
                            {
                                //Kicked
                                gameObject.tag = "EnemyBulletKicked";
                                if(gameObject.activeInHierarchy == true)
                                {
                                    StartCoroutine(SetBulletBack(5));
                                }

                                bulletKicked = true;
                                redBulletIcon.gameObject.SetActive(false);
                                greenBulletIcon.gameObject.SetActive(true);
                                gameObject.layer = 8;
                                isFriendlyBullet = true;

                                legsKicked = true;

                                Vector2 shootDirection;
                                if (bulletPos.x < 0)
                                {
                                    float angle = Random.Range(135f, 225f);
                                    shootDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                                }
                                else
                                {
                                    float angle = Random.Range(-45f, 45f);
                                    shootDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                                }

                                if (rigidbody2d != null)
                                {
                                    float force = 5f; 
                                    rigidbody2d.velocity = shootDirection * force;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //If friendly bullet, do something.
            }
        }
    }

    public void SpawnParticle()
    {
        GameObject particle = ObjectPool.instance.GetBulletHitParticleFromPool();
        particle.transform.localPosition = gameObject.transform.localPosition;
    }

    bool isFriendlyBullet;

    private void OnEnable()
    {
        stopped = false;
        if (isParticle)
        {
            StartCoroutine(SetParticleBack());
        }
        else
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        bulletKicked = false;

        if (gameObject.tag == "EnemyBullet")
        {
            bulletCollider.enabled = true;
            redBulletIcon.gameObject.SetActive(true);
            greenBulletIcon.gameObject.SetActive(false);
            isFriendlyBullet = false;
        }
        else if (gameObject.tag == "FriendlyBullet")
        {
            StartCoroutine(SetCollider()); bulletCollider.enabled = false;
            StartCoroutine(SetBulletBack(4));
            redBulletIcon.gameObject.SetActive(false);
            greenBulletIcon.gameObject.SetActive(true);
            isFriendlyBullet = true;
        }
    }

    IEnumerator SetCollider()
    {
        yield return new WaitForSeconds(1.85f);
        bulletCollider.enabled = true;
    }

    IEnumerator SetBulletBack(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        ObjectPool.instance.ReturnEnemyBulletFromPool(gameObject);
    }

    IEnumerator SetParticleBack()
    {
        yield return new WaitForSeconds(1);
        ObjectPool.instance.ReturnBulletHitParticleToPool(gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
