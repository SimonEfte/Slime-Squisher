using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlimeMechanics : MonoBehaviour
{
    public GameObject middleObject;
    public Animator animator;
    public float moveSpeed;
    public float slimeHealth;

    private Color whiteFlashColor = Color.white;
    private SpriteRenderer spriteRenderer;
    private Material material;

    public Collider2D slimeCollider;
    public Transform targetObject;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        middleObject = GameObject.Find("Strawberry");

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        slimeCollider = GetComponent<Collider2D>();
        targetObject = transform.Find("TargetObject");
    }

    private void OnEnable()
    {
        slimeCollider.enabled = true;
        slimeHealth = SpawnSlimes.slimeHealth;

        SetPos();
        targetObject.gameObject.SetActive(true);

        moveCoroutine = StartCoroutine(MoveTowardsTarget());
    }

    public void SetPos()
    {
        int randomPos = Random.Range(1, 5);
        int randomX = Random.Range(-900, 900);
        int randomY = Random.Range(580, -580);

        if (randomPos == 1) { gameObject.transform.localPosition = new Vector2(randomX, 582); }
        else if (randomPos == 2) { gameObject.transform.localPosition = new Vector2(randomX, -582); }
        else if (randomPos == 3) { gameObject.transform.localPosition = new Vector2(1030, randomY); }
        else if (randomPos == 4) { gameObject.transform.localPosition = new Vector2(-1030, -randomY); }

        if (gameObject.transform.localPosition.x < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (gameObject.transform.localPosition.x > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public Coroutine moveCoroutine;

    IEnumerator MoveTowardsTarget()
    {
        while (Vector3.Distance(transform.position, middleObject.transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                middleObject.transform.position,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }
    }

    #region collision 2d
    public bool isCollidingWithStrawberry;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PickUpgrade.haveCursorSlash == true)
        {
            if (collision.gameObject.layer == 10)
            {
                DamageSlime(PickUpgrade.clickDamage / 10);
            }
        }

        if (collision.gameObject.layer == 9)
        {
            isCollidingWithStrawberry = true;
            StrawberryMechanics.slimesCurrentlyColliding += 1;
            StopCoroutine(moveCoroutine);
        }

        if (collision.gameObject.layer == 6)
        {
            DamageSlime(PickUpgrade.clickDamage);
        }
        if (collision.gameObject.layer == 8)
        {
            DamageSlime(PickUpgrade.clickDamage / 1);
        }
    }
    #endregion

    #region damage slime
    public static Vector2 slimeSquishedPos;

    public void DamageSlime(float damage)
    {
        StartCoroutine(DamageWhiteFlash());
        slimeHealth -= damage;

        TextMeshProUGUI damageText = ObjectPool.instance.GetDamageTextFromPool();
        damageText.text = damage.ToString("F1");
        damageText.transform.localPosition = gameObject.transform.localPosition;

        if (slimeHealth <= 0)
        {
            targetObject.gameObject.SetActive(false);

            slimeSquishedPos = gameObject.transform.position;
            CursorMechanics.triggerPaperClip = true;

            slimeCollider.enabled = false;
            StopCoroutine(moveCoroutine);
            SpawnSlimes.slimesSquished += 1;
            if (SpawnSlimes.slimesSquished >= SpawnSlimes.slimesWaveSpawnCount)
            {
                SpawnSlimes.isWaveCompleted = true;
            }

            StartCoroutine(SquishTheSlime());
        }
    }
    #endregion

    #region damage white flash
    IEnumerator DamageWhiteFlash()
    {
        material.SetColor("_FlashColor", whiteFlashColor);
        material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.2f);
        //material.SetColor("_FlashColor", whiteFlashColor);
        material.SetFloat("_FlashAmount", 0);
    }
    #endregion

    #region slimes squished/killed
    IEnumerator SquishTheSlime()
    {
        int randomCoin = Random.Range(1,2);
        if(randomCoin == 1)
        {
            SpawnCoin();
        }

        GameObject goo = ObjectPool.instance.GetGooFromPool();
        goo.transform.localPosition = gameObject.transform.localPosition;

        animator.SetBool("IsSquished", true);
        yield return new WaitForSeconds(0.3f);
        if (isCollidingWithStrawberry == true) { StrawberryMechanics.slimesCurrentlyColliding -= 1; }

        ObjectPool.instance.ReturnSlime1FromPool(gameObject);
    }
    #endregion

    #region Spawn coin
    public void SpawnCoin()
    {
        GameObject coin = ObjectPool.instance.GetCoinFromPool();
        coin.transform.localPosition = gameObject.transform.localPosition;
    }
    #endregion

}
