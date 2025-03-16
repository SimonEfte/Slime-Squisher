using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StrawberryMechanics : MonoBehaviour, IDataPersistence
{
    public Image strawberryCooldown;
    public static float hitCooldownTimer;
    public static bool isDamageFullHeart;

    public static int strawberryHealth;

    public static int startHealth;

    public static bool isHalfHeart;
    public static int slimesCurrentlyColliding;

    public AudioManager audioManager;

    public static int currentMaxHealth;

    public Animation leftLegAnim, rightLegAnim;

    private void Start()
    {
        startHealth = 2;

        strawberryHealth = startHealth + MetaProgressionUpgrades.startHealth;
        
        if(SelectGameMode.choseFragile == true)
        {
            startHealth = 0;
            strawberryHealth = 0;
            isHalfHeart = true;
        }

        if (SelectGameMode.choseHard == true)
        {
            isDamageFullHeart = true;
        }
        else
        {
            isDamageFullHeart = false;
        }
    }

    private void Update()
    {
        if(BulletMechanics.legsKicked == true)
        {
            if (BulletMechanics.bulletPos.x < 0)
            {
                leftLegAnim.Play("LeftLegKick");
            }
            else
            {
                rightLegAnim.Play("RightLegKick");
            }

            BulletMechanics.legsKicked = false;
        }

        //Reset the cooldown if the cooldown is active while a wave is completed.

        if (strawberryHealth == 0 && isHalfHeart == false)
        {
            isInDeathFrame = true;
        }

        if (slimesCurrentlyColliding > 0 && hitStrawberry == false)
        {
            DealDamageToStraweberry();
        }

        if (isInDeathFrame == true)
        {

        }
    }

    public static int slimesColliding_w_strawberry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            DealDamageToStraweberry();
        }
    }

    public GameObject strawberrySmile, strawberryFrown;

    #region deal damage
    public static bool isDeath;

    public void DealDamageToStraweberry()
    {
        if(PickUpgrade.choseLegs == true && hitStrawberry == false)
        {
            int randomNegate = Random.Range(0,100);
            if(randomNegate < 15)
            {
                hitStrawberry = true;
                cooldownCoroutine = StartCoroutine(StrawberryCooldown());
                StartCoroutine(MinusHeartTextAnim(false, true));
                return;
            }
        }

        if (hitStrawberry == false)
        {
            strawberrySmile.SetActive(false);
            strawberryFrown.SetActive(true);

            if (isDeath == true) { return; }

            if (isHalfHeart == true && isDamageFullHeart == false)
            {
                halfHeart.SetActive(false);
                isHalfHeart = false;
            }
            else if (isHalfHeart == false && isDamageFullHeart == false)
            {
                isHalfHeart = true;
                strawberryHealth -= 1;
                halfHeart.SetActive(true);
                hearts[strawberryHealth].SetActive(false);
            }
            else if (isDamageFullHeart == true)
            {
                if (strawberryHealth > 0 && isHalfHeart == true)
                {
                    halfHeart.SetActive(true);
                    isHalfHeart = true;
                    strawberryHealth -= 1;
                }
                else if (strawberryHealth > 0 && isHalfHeart == false)
                {
                    strawberryHealth -= 1;
                }
                else if (strawberryHealth == 0 && isHalfHeart == true)
                {
                    halfHeart.SetActive(false);
                    isHalfHeart = false;
                }

                hearts[strawberryHealth].SetActive(false);
            }

            if (strawberryHealth == 0 && isHalfHeart == false)
            {
                isInDeathFrame = true;
                audioManager.Play("StrawberryDamaged");
                isDeath = true;
                Death();
            }
            else
            {
                audioManager.Play("StrawberryDamaged");
                hitStrawberry = true;
                cooldownCoroutine = StartCoroutine(StrawberryCooldown());
                if(isDamageFullHeart == false) { StartCoroutine(MinusHeartTextAnim(true, false)); }
                else { StartCoroutine(MinusHeartTextAnim(false, false)); }
            }
        }
    }
    #endregion

    #region set health
    public GameObject halfHeart;
    public GameObject[] hearts;

    public void SetHealth()
    {
        currentMaxHealth = strawberryHealth;

        if (SelectGameMode.choseFragile == true)
        {
            isHalfHeart = true;
            currentMaxHealth = 0;
            strawberryHealth = 0;
        }

        if(SelectGameMode.choseHard == true)
        {
            isDamageFullHeart = true;
        }
        else
        {
            isDamageFullHeart = false;
        }

        if (isHalfHeart == true) { halfHeart.SetActive(true); }
        else { halfHeart.SetActive(false); }
        
        for (int i = 0; i < strawberryHealth; i++)
        {
            hearts[i].SetActive(true);
        }
    }
    #endregion

    #region cooldown
    public static bool hitStrawberry;
    public Coroutine cooldownCoroutine;

    IEnumerator StrawberryCooldown()
    {
        float duration = hitCooldownTimer + MetaProgressionUpgrades.damagedCooldownIcrease;
        float timerDuration = hitCooldownTimer + MetaProgressionUpgrades.damagedCooldownIcrease;
        float elapsedTime = 0f;

        strawberryCooldown.fillAmount = 1;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            strawberryCooldown.fillAmount = 1f - (elapsedTime / duration);
            timerDuration -= Time.deltaTime;
            yield return null;
        }

        strawberrySmile.SetActive(true);
        strawberryFrown.SetActive(false);

        hitStrawberry = false;
        strawberryCooldown.fillAmount = 0;
    }
    #endregion

    #region Heal 
    public static int waveToHeal;

    public void Heal()
    {
        if(SelectGameMode.choseFragile == true)
        {
            return;
        }

        waveToHeal += 1;
        if(waveToHeal == MetaProgressionUpgrades.healEveryWave)
        {
            waveToHeal = 0;
        }
        else
        {
            return;
        }

        if (strawberryHealth == currentMaxHealth)
        {
            //If the HP is max, do not heal
        }
        else
        {
            if(isDamageFullHeart == true)
            {
                strawberryHealth += 1;
            }
            else
            {
                if (isHalfHeart == true) { isHalfHeart = false; halfHeart.SetActive(false); strawberryHealth += 1; }
                else { halfHeart.SetActive(true); isHalfHeart = true; }
            }

            for (int i = 0; i < strawberryHealth; i++)
            {
                hearts[i].SetActive(true);
            }
        }
    }
    #endregion

    #region Minus heart
    public TextMeshProUGUI minusHealthText, negatedText;
    public Image halftHeart, fullHeart;

    IEnumerator MinusHeartTextAnim(bool halfHeart, bool negate)
    {
        TextMeshProUGUI texToSpawn = null;

        Vector2 posSpawn = new Vector2(0,0);

        if (negate == true)
        {
            posSpawn = new Vector2(0, 120);
            texToSpawn = negatedText;
        }
        else
        {
            texToSpawn = minusHealthText;
            posSpawn = new Vector2(-19, 120);
        }

        texToSpawn.gameObject.SetActive(true);

        texToSpawn.transform.localPosition = posSpawn;

        if (halfHeart == true) { halftHeart.gameObject.SetActive(true); fullHeart.gameObject.SetActive(false); }
        else { halftHeart.gameObject.SetActive(false); fullHeart.gameObject.SetActive(true); }

        if(negate == true)
        {
            halftHeart.gameObject.SetActive(false); fullHeart.gameObject.SetActive(false);
        }

        float duration = 1.1f;
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;
        float speed = 1.2f;

        Color initialColor = texToSpawn.color;
        Color initialHalfHeartColor = halftHeart.color;
        Color initialFullHeartColor = fullHeart.color;

        Image activeHeart = isHalfHeart ? halftHeart : fullHeart;

        while (elapsedTime < duration)
        {
            texToSpawn.transform.position += Vector3.up * speed * Time.deltaTime;

            if (elapsedTime > duration - fadeDuration)
            {
                float fadeElapsed = elapsedTime - (duration - fadeDuration);
                float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);

                texToSpawn.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

                activeHeart.color = new Color(activeHeart.color.r, activeHeart.color.g, activeHeart.color.b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        halftHeart.color = new Color(initialHalfHeartColor.r, initialHalfHeartColor.g, initialHalfHeartColor.b, 1f);
        fullHeart.color = new Color(initialFullHeartColor.r, initialFullHeartColor.g, initialFullHeartColor.b, 1f);
        texToSpawn.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        halftHeart.gameObject.SetActive(false);
        fullHeart.gameObject.SetActive(false);
        texToSpawn.gameObject.SetActive(false);
    }
    #endregion

    #region reset strawberry
    public void ResetStrawberry(bool setHealthOff)
    {
        if(setHealthOff == true)
        {
            halfHeart.SetActive(false);
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(false);
            }
        }
        else
        {
            strawberryHealth = startHealth + MetaProgressionUpgrades.startHealth;
            SetHealth();
        }

        isDeath = false;
        hitStrawberry = false;

        strawberrySmile.SetActive(true);
        strawberryFrown.SetActive(false);

        if(DemoScript.isDemo == true && isHalfHeart == true)
        {
            isHalfHeart = false;
            halfHeart.SetActive(false);
        }

        strawberryCooldown.fillAmount = 0;
        if(cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = null;
        }
    }
    #endregion

    #region Death
    public GameObject deathFrame;
    public GameObject demoFrame, fullGameFrame;
    public static bool isInDeathFrame;
    public Animation wishlistText;

    public GameObject blockFrame;
    public TextMeshProUGUI deathWaveText;

    public LocalizationSCRIPT locScript;

    public GameObject resetRunBtn, mainMenuBtn;

    public void Death()
    {
        SpawnSlimes.isRampagePlaying = false;

        locScript.ChangingStrings(SpawnSlimes.slimeWave);

        deathWaveText.text = $"{LocalizationSCRIPT.youReachedWave}";

        if (DemoScript.isDemo == true)
        {
            StartCoroutine(DeathScreen("Easy"));
        }
        else
        {
            if(SelectGameMode.choseEasy == true) { StartCoroutine(DeathScreen("Easy")); }
            else if (SelectGameMode.choseNormal == true) { StartCoroutine(DeathScreen("Normal")); }
            else if (SelectGameMode.choseHard == true) { StartCoroutine(DeathScreen("Hard")); }
            else if (SelectGameMode.choseBullethell == true) { StartCoroutine(DeathScreen("Bullethell")); }
            else if (SelectGameMode.choseFlash == true) { StartCoroutine(DeathScreen("Flash")); }
            else if (SelectGameMode.choseFragile == true) { StartCoroutine(DeathScreen("Fragile")); }
            else if (SelectGameMode.choseNarrow == true) { StartCoroutine(DeathScreen("Narrow")); }
            else if (SelectGameMode.choseRampage == true) { StartCoroutine(DeathScreen("Rampage")); }
        }
    }

    IEnumerator DeathScreen(string gameMode)
    {
        yield return new WaitForSeconds(0.1f);

        audioManager.Play("GameOver");

        if (DemoScript.isDemo == false)
        {
            StartCoroutine(DeathFramePopUp());
            demoFrame.SetActive(false);
            fullGameFrame.SetActive(true);
        }
        else
        {
            demoFrame.SetActive(true);
            fullGameFrame.SetActive(false);
        }

        deathFrame.SetActive(true);
        deathFrame.GetComponent<Animation>().Play();

        if (gameMode == "Easy")
        {
            if (DemoScript.isDemo == true)
            {
                wishlistText.gameObject.SetActive(true);
                wishlistText.Play("WishlistAnim");
            }
            else
            {
                wishlistText.gameObject.SetActive(false);
            }
        }
    }

    public TextMeshProUGUI waveReached, waveReachedNumber, waveToReach, waveToReachNumber, coinCollected, coinsCollectedAmount, totalCoins, totalCoinsAmount;

    IEnumerator DeathFramePopUp()
    {
        resetRunBtn.SetActive(false); mainMenuBtn.SetActive(false);
        waveReached.gameObject.SetActive(false); waveReachedNumber.gameObject.SetActive(false);
        waveToReach.gameObject.SetActive(false); waveToReachNumber.gameObject.SetActive(false);
        coinCollected.gameObject.SetActive(false); coinsCollectedAmount.gameObject.SetActive(false);
        totalCoins.gameObject.SetActive(false); totalCoinsAmount.gameObject.SetActive(false);

        coinsCollectedAmount.text = "+" + PickUpgrade.coinsThisRound;

        MetaProgressionUpgrades.totalCoins += PickUpgrade.coinsThisRound;

        totalCoinsAmount.text = "" + MetaProgressionUpgrades.totalCoins;

        waveReachedNumber.text = "" + SpawnSlimes.slimeWave;
        if(SelectGameMode.choseEasy == true) { waveToReachNumber.text = "" + SelectGameMode.easy_waveToReach; }
        if (SelectGameMode.choseNormal == true) { waveToReachNumber.text = "" + SelectGameMode.normal_waveToReach; }
        if (SelectGameMode.choseHard == true) { waveToReachNumber.text = "" + SelectGameMode.hard_waveToReach; }
        if (SelectGameMode.choseBullethell == true) { waveToReachNumber.text = "" + SelectGameMode.bullethell_waveToReach; }
        if (SelectGameMode.choseFlash == true) { waveToReachNumber.text = "" + SelectGameMode.flash_waveToReach; }
        if (SelectGameMode.choseFragile == true) { waveToReachNumber.text = "" + SelectGameMode.fragile_waveToReach; }
        if (SelectGameMode.choseNarrow == true) { waveToReachNumber.text = "" + SelectGameMode.narrow_waveToReach; }

        if (SelectGameMode.choseRampage == true) 
        {
            waveReached.text = "time reached:";
            waveToReach.text = "time to reach:";
            waveToReachNumber.text = "" + SelectGameMode.rampage_MinuteToReach + ":00";

            int minutes = Mathf.FloorToInt(SpawnSlimes.waveTime / 60);
            int seconds = Mathf.FloorToInt(SpawnSlimes.waveTime % 60);
            waveReachedNumber.text = $"{minutes:00}:{seconds:00}";
        }
        else
        {
            waveReached.text = "wave reached:";
            waveToReach.text = "wave to reach:";
        }

        yield return new WaitForSeconds(0.65f);
        waveReached.gameObject.SetActive(true); waveReachedNumber.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        waveToReach.gameObject.SetActive(true); waveToReachNumber.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        coinCollected.gameObject.SetActive(true); coinsCollectedAmount.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        totalCoins.gameObject.SetActive(true); totalCoinsAmount.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        resetRunBtn.SetActive(true); mainMenuBtn.SetActive(true); audioManager.Play("Pop");
    }

    public void PlayAgainBtn()
    {
        PickUpgrade.coinsThisRound = 0;

        blockFrame.SetActive(true);
        deathFrame.SetActive(false);
        MainMenu.clickedResetRun = true;
    }

    public void MainMenuBtn()
    {
        PickUpgrade.coinsThisRound = 0;

        blockFrame.SetActive(true);
        deathFrame.SetActive(false);
        MainMenu.clickedResetRun = false;
    }
    #endregion

    #region Load Data
    public void LoadData(GameData data)
    {
        strawberryHealth = data.strawberryHealth;
        isHalfHeart = data.isHalfHeart;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.strawberryHealth = strawberryHealth;
        data.isHalfHeart = isHalfHeart;
    }
    #endregion
}
