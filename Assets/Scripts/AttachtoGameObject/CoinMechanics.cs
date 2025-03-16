using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CoinMechanics : MonoBehaviour
{
    Rigidbody2D coinRigidbody;
    public Animation coinAnim;
    public GameObject coinsText;
    public TextMeshProUGUI coinsTextObject;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        coinAnim = gameObject.GetComponent<Animation>();
        coinRigidbody = gameObject.GetComponent<Rigidbody2D>();
        coinsText = GameObject.Find("PointsThisGameText");
        coinsTextObject = coinsText.GetComponent<TextMeshProUGUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);

        roundEnd = false;
        hoverCoin = false;
        coinCompletelySpawned = false;

        coinRigidbody.constraints = RigidbodyConstraints2D.None;

        Vector2 randomForce = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1.2f, 1.8f)) * 0.85f;
        coinRigidbody.AddForce(randomForce, ForceMode2D.Impulse);

        StartCoroutine(Wait());
    }

    public bool roundEnd;
    private void Update()
    {
        if(SpawnSlimes.isWaveCompleted == true && roundEnd == false)
        {
            roundEnd = true; PlussCoin();
        }
    }

    public bool coinCompletelySpawned;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.74f);
        coinCompletelySpawned = true;
        coinRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public bool hoverCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinCompletelySpawned == true)
        {
            if (collision.gameObject.layer == 10 && hoverCoin == false && roundEnd == false)
            {
                hoverCoin = true;
                PlussCoin();
            }
        }
    }

    public void PlussCoin()
    {
        PickUpgrade.coinsThisRound += 1;
        coinAnim.Play();
        TextMeshProUGUI pluss1Text = ObjectPool.instance.GetTextFromPool();
        Vector3 offset = new Vector3(0, 0, 0);
        pluss1Text.transform.position = gameObject.transform.position;
        StartCoroutine(AnimOff());
    }

    IEnumerator AnimOff()
    {
        float duration = 0.3f;
        float elapsedTime = 0f; 
        float speed = 1f; 

        while (elapsedTime < duration)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        ObjectPool.instance.ReturnCoinFromPool(gameObject);
    }
}
