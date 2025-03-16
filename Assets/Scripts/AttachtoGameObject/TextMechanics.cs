using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextMechanics : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool isCoinText;
    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        if(gameObject.tag == "CoinText") { isCoinText = true; }
        else { isCoinText = false; rigidbody2d = gameObject.GetComponent<Rigidbody2D>(); }
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if(isCoinText == true) { StartCoroutine(TextAnim()); }
        else
        {
            Vector2 randomForce = new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(1.2f, 1.8f)) * 1.1f;
            rigidbody2d.AddForce(randomForce, ForceMode2D.Impulse);
            StartCoroutine(FadeTextOut());
        }
    }

    IEnumerator FadeTextOut()
    {
        yield return new WaitForSeconds(0.05f);
        Color initialColor = text.color;
        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1);

        yield return new WaitForSeconds(0.3f);

        float duration = 0.55f;
        float fadeDuration = 0.3f;
        float elapsedTime = 0f;
        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        while (elapsedTime < duration)
        {
            float fadeElapsed = elapsedTime - (duration - fadeDuration);
            float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        ObjectPool.instance.ReturnDamageTextFromPool(text);
    }

    #region coin text anim
    IEnumerator TextAnim()
    {
        float duration = 0.55f;
        float fadeDuration = 0.3f;
        float elapsedTime = 0f;
        float speed = 2f;

        Color initialColor = text.color;

        while (elapsedTime < duration)
        {
            // Move the text upwards
            text.transform.position += Vector3.up * speed * Time.deltaTime;

            // Start fading out during the last 0.3 seconds
            if (elapsedTime > duration - fadeDuration)
            {
                float fadeElapsed = elapsedTime - (duration - fadeDuration);
                float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
                text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
        ObjectPool.instance.ReturnTextFromPool(text);
    }
    #endregion

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
