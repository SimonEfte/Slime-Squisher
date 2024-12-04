using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StrawberryMechanics : MonoBehaviour
{
    public TextMeshProUGUI hitCooldownText;
    public Image strawberryCooldown;
    public static float hitCooldownTimer;
    public static bool isDamageFullHeart;

    public static int startHealth;
    public static bool isHalfHeart;
    public static int slimesCurrentlyColliding;

    private void Awake()
    {
        startHealth = 5;
        isDamageFullHeart = false;
        hitCooldownTimer = 3f;
        SetHealth();
    }

    private void Update()
    {
        //Reset the cooldown if the cooldown is active while a wave is completed.

        if(slimesCurrentlyColliding > 0 && hitStrawberry == false)
        {
            DealDamageToStraweberry();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            DealDamageToStraweberry();
        }
    }

    #region deal damage
    public static bool isDeath;

    public void DealDamageToStraweberry()
    {
        if (hitStrawberry == false)
        {
            if (isDeath == true) { return; }

            if (isHalfHeart == true && isDamageFullHeart == false)
            {
                halfHeart.SetActive(false);
                isHalfHeart = false;
            }
            else if (isHalfHeart == false && isDamageFullHeart == false)
            {
                isHalfHeart = true;
                startHealth -= 1;
                halfHeart.SetActive(true);
                hearts[startHealth].SetActive(false);
            }
            else if (isDamageFullHeart == true)
            {
                if (startHealth == 0 && isHalfHeart == true)
                {
                    halfHeart.SetActive(false);
                }
                else
                {
                    startHealth -= 1;
                }

                hearts[startHealth].SetActive(false);
            }

            if (startHealth == 0 && isHalfHeart == false)
            {
                isDeath = true;
            }
            else
            {
                hitStrawberry = true;
                StartCoroutine(StrawberryCooldown());
            }
        }

    }
    #endregion

    #region set health
    public GameObject halfHeart;
    public GameObject[] hearts;

    public void SetHealth()
    {
        if(isHalfHeart == true) { halfHeart.SetActive(true); }
        else { halfHeart.SetActive(false); }

        for (int i = 0; i < startHealth; i++)
        {
            hearts[i].SetActive(true);
        }
    }
    #endregion

    #region cooldown
    public static bool hitStrawberry;

    IEnumerator StrawberryCooldown()
    {
        float duration = hitCooldownTimer;
        float timerDuration = hitCooldownTimer;
        float elapsedTime = 0f;

        strawberryCooldown.fillAmount = 1;
        hitCooldownText.text = duration.ToString("F2");
        hitCooldownText.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            strawberryCooldown.fillAmount = 1f - (elapsedTime / duration);
            timerDuration -= Time.deltaTime;
            hitCooldownText.text = timerDuration.ToString("F2");
            yield return null;
        }

        hitCooldownText.gameObject.SetActive(false);
        hitStrawberry = false;
        strawberryCooldown.fillAmount = 0;
    }
    #endregion

}
