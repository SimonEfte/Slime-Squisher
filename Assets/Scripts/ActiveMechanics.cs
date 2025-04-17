using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveMechanics : MonoBehaviour, IDataPersistence
{
    public LocalizationSCRIPT locScript;

    public Achivements achScript;

    public AudioManager audioManager;

    //Death to slimes
    public static bool usedDeathToSlimes, isDeathToSlimesCooldown;
    public CursorMechanics cursorMechanicsScript;
    public static int deathToSlimes_killAmount, deathToSlimes_slimesKilled;
    public static int deathToSlimes_checked;
    public static int deathToSlimes_WaveRecharge, deathToSlime_WavesCharged;

    //Sharp clicks
    public static bool usedPunchyClicks, isPunchyClicksCooldown;
    public static int sharpClicksTimer, sharpClicks_WaveRecharge, sharpClicks_WaveRecharged;
    public static float sharpClicksTimeInterval;

    //clover
    public static bool usedClover, isCloverCooldown;
    public static int cloverTimer, clover_waveRecharge, clover_waveRecharged;

    //decoy
    public static bool usedDecoy, isDecoyCooldown;
    public static bool isDecoyPlaced, isDecoyDestroyed;
    public static int decoyWaveHealth;
    public static int decoy_waveRecharge, decoy_WavesCharged, decoyWavesAlive;

    //frency
    public static bool usedProjcetileFrency, isFrenzyCooldown;
    public static int  projectileFrency_waveRecharge, projectileFrency_waveRecharged;
    public static float projectileFrencyTime, projectileFrencyProjectiles;


    //anti slime bullets
    public static bool usedAntiSlimeBullet, isAntiSlimeBulletCooldown;
    public static int antiSlimeBulletCount, antiSlime_waveRecharge, antiSlime_waveRecharged, antiSlimeDamage;
    public static float antiBulletDeathChance;

    public static int punchyClicksPrice, cloverPrice, decoyPrice, frenzyPrice, antiSlimeBulletPrice;


    #region Awake
    private void Awake()
    {
        punchyClicksPrice = 25;
        cloverPrice = 35;
        decoyPrice = 50;
        frenzyPrice = 50;
        antiSlimeBulletPrice = 60;

        if (DemoScript.isDemo == true)
        {
            choseDeathToSlimes = true;
            isDeathToSlimesCooldown = true;
        }

        playSound = false;

        StartCoroutine(Wait());
    }
    #endregion

    #region Set active variables
    public void ActiveVariables()
    {
        if (MetaProgressionUpgrades.activeTier == 2)
        {
            deathToSlimes_WaveRecharge = 2;
            deathToSlimes_killAmount = 9;

            sharpClicksTimeInterval = 0.1f;
            sharpClicksTimer = 6;
            sharpClicks_WaveRecharge = 2;

            cloverTimer = 6;
            clover_waveRecharge = 3;

            decoyWaveHealth = 3;
            decoy_waveRecharge = 4;

            projectileFrencyTime = 3;
            projectileFrencyProjectiles = 50;
            projectileFrency_waveRecharge = 3;

            antiSlimeBulletCount = 35;
            antiBulletDeathChance = 30;
            antiSlime_waveRecharge = 2;
            antiSlimeDamage = 20;
        }
        else if (MetaProgressionUpgrades.activeTier == 1)
        {
            deathToSlimes_WaveRecharge = 3;
            deathToSlimes_killAmount = 9;

            sharpClicksTimeInterval = 0.1f;
            sharpClicksTimer = 5;
            sharpClicks_WaveRecharge = 3;

            cloverTimer = 5;
            clover_waveRecharge = 4;

            decoyWaveHealth = 2;
            decoy_waveRecharge = 5;

            projectileFrencyTime = 2;
            projectileFrencyProjectiles = 40;
            projectileFrency_waveRecharge = 3;

            antiSlimeBulletCount = 31;
            antiBulletDeathChance = 25;
            antiSlime_waveRecharge = 2;
            antiSlimeDamage = 17;
        }
        else
        {
            deathToSlimes_WaveRecharge = 3;
            deathToSlimes_killAmount = 7;

            sharpClicksTimeInterval = 0.1f;
            sharpClicksTimer = 4;
            sharpClicks_WaveRecharge = 3;

            cloverTimer = 4;
            clover_waveRecharge = 4;

            decoyWaveHealth = 2;
            decoy_waveRecharge = 5;

            projectileFrencyTime = 2;
            projectileFrencyProjectiles = 35;
            projectileFrency_waveRecharge = 3;

            antiSlimeBulletCount = 26;
            antiBulletDeathChance = 20;
            antiSlime_waveRecharge = 2;
            antiSlimeDamage = 15;
        }

        if (LocalizationSCRIPT.languageSelected == 1) //English
        {
            locScript.EnglishLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 2) //German
        {
            locScript.GermanLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 3) //Japanese
        {
            locScript.JapaneseLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 4) //French
        {
            locScript.FrenchLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 5) //Spanish
        {
            locScript.SpanishLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 6) //Chinese
        {
            locScript.ChineseLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 7) //Korean
        {
            locScript.KoreanLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 8) //Russian
        {
            locScript.RussianLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 9) //Polish
        {
            locScript.PolishLanguage();
        }
        else if (LocalizationSCRIPT.languageSelected == 10) //Portugese
        {
            locScript.PortugeseLanguage();
        }
    }
    #endregion

    bool playSound;

    public GameObject punchyClicksPriceText, cloverPriceText, frenzyPriceText, antiPriceText;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        ActiveVariables();

        if (isPunchyClicksUnlcoked == true) { lockedSharpClicks.SetActive(false); }
        if (isCloverUnlocked == true) { lockedClover.SetActive(false); }
        if (isDecoyUnlocked == true) { lockedDecoy.SetActive(false); }
        if (isProjectileFrenzyUnlocked == true) { lockedFrenzy.SetActive(false); }
        if (isAntiSlimeBulletsUnlocked == true) { lockedAntiSlimeBullets.SetActive(false); }

        if (choseDeathToSlimes == true) { SelectActive(1); }
        if (chosePunchyClicks == true) { SelectActive(2); }
        if (choseClover == true) { SelectActive(3); }
        if (choseDecoy == true) { SelectActive(4); }
        if (choseProjectileFrenzy == true) { SelectActive(5); }
        if (choseAntiSlime == true) { SelectActive(6); }

        if(MobileScript.isMobile == true)
        {
            punchyClicksPriceText.GetComponent<TextMeshProUGUI>().text = "<color=yellow>" + punchyClicksPrice.ToString("F0");
            cloverPriceText.GetComponent<TextMeshProUGUI>().text = "<color=yellow>" + cloverPrice.ToString("F0");
            frenzyPriceText.GetComponent<TextMeshProUGUI>().text = "<color=yellow>" + frenzyPrice.ToString("F0");
            antiPriceText.GetComponent<TextMeshProUGUI>().text = "<color=yellow>" + antiSlimeBulletPrice.ToString("F0");

            if (isPunchyClicksUnlcoked == false) { punchyClicksPriceText.gameObject.SetActive(true); }
            if (isCloverUnlocked == false) { cloverPriceText.gameObject.SetActive(true); }
            if (isProjectileFrenzyUnlocked == false) { frenzyPriceText.gameObject.SetActive(true); }
            if (isAntiSlimeBulletsUnlocked == false) { antiPriceText.gameObject.SetActive(true); }
        }

        playSound = true;
    }

    #region select active and purchase
    public static bool choseDeathToSlimes, chosePunchyClicks, choseClover, choseDecoy, choseProjectileFrenzy, choseAntiSlime;
    public static bool isPunchyClicksUnlcoked, isCloverUnlocked, isDecoyUnlocked, isProjectileFrenzyUnlocked, isAntiSlimeBulletsUnlocked;

    public Transform deathToSlimes, sharpClicks, clover, decoy, projectileFrency, antiSlimeBullets;
    public Transform selectedActiveIcon;

    public TextMeshProUGUI acitveDesText, activeNameText;
    public static bool justChangeStuff;

    public GameObject lockedSharpClicks, lockedClover, lockedDecoy, lockedFrenzy, lockedAntiSlimeBullets;
    public GameObject deathToSlimesIcon, sharpClicksIcon, cloverIcon, decoyIcon, frenzyIcon, antiIcon;
    public GameObject activePriceText;

    public void SelectActive(int active)
    {
        if (active == 1)
        {
            SetActiveOff();
            selectedActiveIcon.transform.position = deathToSlimes.transform.position;
            choseDeathToSlimes = true;
            if(playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            activeNameText.text = LocalizationSCRIPT.deathToSlimes + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.deathToSlimes_des;
            deathToSlimesIcon.SetActive(true);
        }
        if (active == 2)
        {
            if(isPunchyClicksUnlcoked == false && MetaProgressionUpgrades.totalCoins >= punchyClicksPrice && DemoScript.isDemo == false)
            {
                lockedSharpClicks.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= punchyClicksPrice;
                isPunchyClicksUnlcoked = true;
                activePriceText.SetActive(false);
                Achivements.achievedPunchyClicks = true;
                achScript.TriggerACH("purchase_punchyClicks");
                return;
            }

            if (isPunchyClicksUnlcoked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = sharpClicks.transform.position;
            chosePunchyClicks = true;
            activeNameText.text = LocalizationSCRIPT.punchyClicks + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.punchyClicks_des;
            sharpClicksIcon.SetActive(true);

            if(MobileScript.isMobile == true) { punchyClicksPriceText.SetActive(false); }
        }
        if (active == 3)
        {
            if (isCloverUnlocked == false && MetaProgressionUpgrades.totalCoins >= cloverPrice && DemoScript.isDemo == false)
            {
                lockedClover.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= cloverPrice;
                isCloverUnlocked = true;
                activePriceText.SetActive(false);
                activePriceText.SetActive(false);
                Achivements.achievedClover = true;
                achScript.TriggerACH("purchase_clover");
                return;
            }

            if (isCloverUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = clover.transform.position;
            choseClover = true;
            activeNameText.text = LocalizationSCRIPT.clover + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.clover_des;
            cloverIcon.SetActive(true);

            if (MobileScript.isMobile == true) { cloverPriceText.SetActive(false); }
        }
        if (active == 4)
        {
            if (isDecoyUnlocked == false && MetaProgressionUpgrades.totalCoins >= decoyPrice && DemoScript.isDemo == false)
            {
                lockedDecoy.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= decoyPrice;
                isDecoyUnlocked = true;
                activePriceText.SetActive(false);
                Achivements.achievedDecoy = true;
                achScript.TriggerACH("purchase_decoy");
                return;
            }

            if (isDecoyUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = decoy.transform.position;
            choseDecoy = true;
            activeNameText.text = LocalizationSCRIPT.decoy + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.decoy_des;
            decoyIcon.SetActive(true);

          
        }
        if (active == 5)
        {
            if (isProjectileFrenzyUnlocked == false && MetaProgressionUpgrades.totalCoins >= frenzyPrice && DemoScript.isDemo == false)
            {
                lockedFrenzy.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= frenzyPrice;
                isProjectileFrenzyUnlocked = true;
                activePriceText.SetActive(false);
                Achivements.achievedProjectileFrenzy = true;
                achScript.TriggerACH("purchase_frenzy");
                return;
            }

            if (isProjectileFrenzyUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = projectileFrency.transform.position;
            choseProjectileFrenzy = true;
            activeNameText.text = LocalizationSCRIPT.frency + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.frency_des;
            frenzyIcon.SetActive(true);

            if (MobileScript.isMobile == true) { frenzyPriceText.SetActive(false); }
        }
        if (active == 6)
        {
            if (isAntiSlimeBulletsUnlocked == false && MetaProgressionUpgrades.totalCoins >= antiSlimeBulletPrice && DemoScript.isDemo == false)
            {
                lockedAntiSlimeBullets.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= antiSlimeBulletPrice;
                isAntiSlimeBulletsUnlocked = true;
                activePriceText.SetActive(false);
                Achivements.achievedAntiSlimeBullets = true;
                achScript.TriggerACH("purchase_antiSlime");
                return;
            }

            if (isAntiSlimeBulletsUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = antiSlimeBullets.transform.position;
            choseAntiSlime = true;
            activeNameText.text = LocalizationSCRIPT.antiSlime + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.antiSlime_des;
            antiIcon.SetActive(true);

            if (MobileScript.isMobile == true) { antiPriceText.SetActive(false); }
        }

        justChangeStuff = false;
    }

    public void SetActiveOff()
    {
        choseDeathToSlimes = false;
        chosePunchyClicks = false;
        choseClover = false;
        choseDecoy = false;
        choseProjectileFrenzy = false;
        choseAntiSlime = false;

        deathToSlimesIcon.SetActive(false);
        sharpClicksIcon.SetActive(false);
        cloverIcon.SetActive(false);
        decoyIcon.SetActive(false);
        frenzyIcon.SetActive(false);
        antiIcon.SetActive(false);
    }
    #endregion

    #region update and mobile click active button
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && MainMenu.isInMainMenu == false && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false && PickUpgrade.isInChooseUpgrade == false && MobileScript.isMobile == false)
        {
            UseActive();
        }
    }

    public void UseActive()
    {
        if (choseDeathToSlimes == true && usedDeathToSlimes == false && isDeathToSlimesCooldown == false)
        {
            audioManager.Play("ActiveClick");

            deathToSlimes_slimesKilled = 0;
            deathToSlime_WavesCharged = 0;
            cursorMechanicsScript.SelectRandomActiveSlime(0);
            usedDeathToSlimes = true;
            ActiveCooldown(true);
        }
        if (chosePunchyClicks == true && usedPunchyClicks == false && isPunchyClicksCooldown == false)
        {
            audioManager.Play("ActiveClick");
            UsePunchyClicks();

            sharpClicks_WaveRecharged = 0;
            usedPunchyClicks = true;
            ActiveCooldown(true);
        }
        if (choseClover == true && usedClover == false && isCloverCooldown == false)
        {
            audioManager.Play("ActiveClick");
            UseClover();

            clover_waveRecharged = 0;
            usedClover = true;
            ActiveCooldown(true);
        }
        if (choseDecoy == true && usedDecoy == false && isDecoyCooldown == false)
        {
            Decoy();

            audioManager.Play("ActiveClick");

            decoy_WavesCharged = 0;
            usedDecoy = true;
            ActiveCooldown(true);
        }
        if (choseProjectileFrenzy == true && usedProjcetileFrency == false && isFrenzyCooldown == false)
        {
            audioManager.Play("ActiveClick");
            UseFrenzy();

            projectileFrency_waveRecharged = 0;
            usedProjcetileFrency = true;
            ActiveCooldown(true);
        }
        if (choseAntiSlime == true && usedAntiSlimeBullet == false && isAntiSlimeBulletCooldown == false)
        {
            audioManager.Play("ActiveClick");
            UseAntiSlime();

            antiSlime_waveRecharged = 0;
            usedAntiSlimeBullet = true;
            ActiveCooldown(true);
        }
    }
    #endregion

    #region Use punchy clicks
    public static bool punchyClicksIsUsed;

    public void UsePunchyClicks()
    {
        punchyClicksIsUsed = true;
        StartCoroutine(PunchyClicksTimer());
        StartCoroutine(ActiveTimerText(sharpClicksTimer));
    }

    IEnumerator PunchyClicksTimer()
    {
        yield return new WaitForSeconds(sharpClicksTimer);
        punchyClicksIsUsed = false;
    }
    #endregion

    #region UseClover
    public static bool cloverIsInUse;
    public static int cloverChanceAdd;

    public static bool isCloverInUse;

    public void UseClover()
    {
        cloverChanceAdd = 50;
        StartCoroutine(CloverTimer());
        StartCoroutine(ActiveTimerText(cloverTimer));
    }

    IEnumerator CloverTimer()
    {
        isCloverInUse = true;
        yield return new WaitForSeconds(cloverTimer);
        cloverChanceAdd = 0;
        isCloverInUse = false;
    }
    #endregion

    #region Use frenzy
    public GameObject cursorColliderObject;

    public void UseFrenzy()
    {
        isFrenzyInUse = true;
        StartCoroutine(ShootFrenzyProjectile());
        StartCoroutine(ActiveTimerText(projectileFrencyTime));
    }

    IEnumerator ShootFrenzyProjectile()
    {
        float interval = projectileFrencyTime / projectileFrencyProjectiles;

        for (int i = 0; i < projectileFrencyProjectiles; i++)
        {
            ShootRandomProjetile();
            yield return new WaitForSeconds(interval);
        }

        isFrenzyInUse = false;
    }

    public NonClickUpgrades nonClickUpgradeScript;
    public OverlappingSounds overlappingScript;

    public static bool isFrenzyInUse;
    public static Vector2 frenzyStartPos;

    public void ShootRandomProjetile()
    {
        overlappingScript.PlaySound(6, 0, false);

        Vector2 pos = cursorColliderObject.transform.position;
        CursorMechanics.kunaiStartPos = pos;

        CursorMechanics.kunaiHitPos = cursorColliderObject.transform.position;

        if(MobileScript.isMobile == true)
        {
            pos = new Vector2(0,0);
        }

        int random = Random.Range(1,10);

        if (random == 1) { cursorMechanicsScript.SelectRandomTargetObject(1); cursorMechanicsScript.ShootPaperClip(pos); }
        if (random == 2)
        { 
            cursorMechanicsScript.SelectRandomTargetObject(4);
        }
        if (random == 3) { cursorMechanicsScript.ShootPoisonDart(pos); }
        if (random == 4) 
        { 
            SlimeMechanics.boulderStartPos = pos; cursorMechanicsScript.SelectRandomActiveSlime(4); 
        } //Boulder
        if (random == 5)
        {
            Vector2 randomTarget = new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000));
            cursorMechanicsScript.ShootBouncyBall(pos, randomTarget);
        }
        if (random == 6) { pos = cursorColliderObject.transform.position; cursorMechanicsScript.ShootThorn(pos, false); }
        if (random == 7) { cursorMechanicsScript.ShootKatana(); }
        if (random == 8) { cursorMechanicsScript.ShootLog(pos); }
        if (random == 9) { cursorMechanicsScript.ShootSawBlades(pos, false); }
    }
    #endregion

    #region Use antiSlime
    public GameObject strawberry;

    public void UseAntiSlime()
    {
        float angleStep = 360f / antiSlimeBulletCount; // Spread evenly in a circle
        Vector3 spawnPosition = strawberry.transform.position;

        for (int i = 0; i < antiSlimeBulletCount; i++)
        {
            GameObject antiSlimeBullet = ObjectPool.instance.GetAntiSlimeBulletFromPool();
            GameObject shadow = ObjectPool.instance.GetShadowFromPool();

            if (antiSlimeBullet != null)
            {
                antiSlimeBullet.transform.position = spawnPosition;

                shadow.transform.position = new Vector2(spawnPosition.x, spawnPosition.y - 0.27f);

                shadow.transform.localScale = new Vector2(0.87f, 0.87f);

                float angle = i * angleStep * Mathf.Deg2Rad; 
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

                Rigidbody2D rb = antiSlimeBullet.GetComponent<Rigidbody2D>();
                Rigidbody2D rbShadow = shadow.GetComponent<Rigidbody2D>();

                float bulletSpeed = 5f;

                if (rb != null)
                {
                    rb.velocity = direction * bulletSpeed;
                }

                if (rbShadow != null)
                {
                    rbShadow.velocity = direction * bulletSpeed;
                }
            }
        }
    }
    #endregion

    #region Decoy
    public GameObject decoyObject, decoyParent, decoyTest;

    public void Decoy()
    {
        decoyWavesAlive = 0;
        isDecoyPlaced = true;
        decoyObject.SetActive(true); decoyParent.SetActive(true);
        Vector3 cursorScreenPos = Input.mousePosition;
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(cursorScreenPos.x, cursorScreenPos.y, Camera.main.nearClipPlane));

        // Set decoy position
        decoyParent.transform.position = cursorWorldPos;
        decoyTest.transform.localPosition = new Vector3(decoyParent.transform.localPosition.x, decoyParent.transform.localPosition.y, 0);
    }

    public Animation deathAnim;

    public void DecoyDeath(bool playSound)
    {
        if(playSound == true) { audioManager.Play("StrawberryDamaged"); }

        isDecoyPlaced = false;
        deathAnim.Play("DecoyDeath");
        StartCoroutine(DecoyDeathWait());
    }

    IEnumerator DecoyDeathWait()
    {
        yield return new WaitForSeconds(0.65f);
        decoyParent.SetActive(false);
    }
    #endregion

    #region cooldown
    public GameObject skullDark, punchyDark, cloverDark, decoyDark, frenzyDark, antiDark;
    public TextMeshProUGUI activeCooldownText;

    public void ActiveCooldown(bool used)
    {
        if(used == true)
        {
            #region used active
            activeCooldownText.gameObject.SetActive(true);

            if (choseDeathToSlimes == true)
            {
                isDeathToSlimesCooldown = true;
                skullDark.SetActive(true);
                skullDark.GetComponent<Image>().fillAmount = 1;
                activeCooldownText.text = deathToSlimes_WaveRecharge.ToString();
            }
            if (chosePunchyClicks == true)
            {
                isPunchyClicksCooldown = true;
                punchyDark.SetActive(true);
                punchyDark.GetComponent<Image>().fillAmount = 1;
                activeCooldownText.text = sharpClicks_WaveRecharge.ToString();
            }
            if (choseClover == true)
            {
                isCloverCooldown = true;
                cloverDark.SetActive(true);
                cloverDark.GetComponent<Image>().fillAmount = 1;
                activeCooldownText.text = clover_waveRecharge.ToString();
            }
            if (choseDecoy == true)
            {
                isDecoyCooldown = true;
                decoyDark.SetActive(true);
                decoyDark.GetComponent<Image>().fillAmount = 1;
                activeCooldownText.text = decoy_waveRecharge.ToString();
            }
            if (choseProjectileFrenzy == true)
            {
                isFrenzyCooldown = true;
                frenzyDark.SetActive(true);
                frenzyDark.GetComponent<Image>().fillAmount = 1;
                activeCooldownText.text = projectileFrency_waveRecharge.ToString();
            }
            if (choseAntiSlime == true)
            {
                isAntiSlimeBulletCooldown = true;
                antiDark.SetActive(true);
                antiDark.GetComponent<Image>().fillAmount = 1;
                activeCooldownText.text = antiSlime_waveRecharge.ToString();
            }
            #endregion
        }
        else
        {
            #region recharging
            if (choseDeathToSlimes == true)
            {
                deathToSlime_WavesCharged += 1;
                if(deathToSlime_WavesCharged == deathToSlimes_WaveRecharge)
                {
                    isDeathToSlimesCooldown = false;
                    skullDark.SetActive(false); 
                    activeCooldownText.gameObject.SetActive(false);
                    deathToSlime_WavesCharged = 0;
                    usedDeathToSlimes = false;
                }
                else
                {
                    skullDark.GetComponent<Image>().fillAmount = 1f - ((float)deathToSlime_WavesCharged / (float)deathToSlimes_WaveRecharge);
                    activeCooldownText.text = (deathToSlimes_WaveRecharge - deathToSlime_WavesCharged).ToString();
                }
            }

            if (chosePunchyClicks == true)
            {
                sharpClicks_WaveRecharged += 1;
                if (sharpClicks_WaveRecharged == sharpClicks_WaveRecharge)
                {
                    isPunchyClicksCooldown = false;
                    punchyDark.SetActive(false);
                    activeCooldownText.gameObject.SetActive(false);
                    sharpClicks_WaveRecharged = 0;
                    usedPunchyClicks = false;
                }
                else
                {
                    punchyDark.GetComponent<Image>().fillAmount = 1f - ((float)sharpClicks_WaveRecharged / (float)sharpClicks_WaveRecharge);
                    activeCooldownText.text = (sharpClicks_WaveRecharge - sharpClicks_WaveRecharged).ToString();
                }
            }

            if (choseClover == true)
            {
                clover_waveRecharged += 1;
                if (clover_waveRecharged == clover_waveRecharge)
                {
                    isCloverCooldown = false;
                    cloverDark.SetActive(false);
                    activeCooldownText.gameObject.SetActive(false);
                    clover_waveRecharged = 0;
                    usedClover = false;
                }
                else
                {
                    cloverDark.GetComponent<Image>().fillAmount = 1f - ((float)clover_waveRecharged / (float)clover_waveRecharge);
                    activeCooldownText.text = (clover_waveRecharge - clover_waveRecharged).ToString();
                }
            }

            if (choseDecoy == true)
            {
                decoy_WavesCharged += 1;
                if (decoy_WavesCharged == decoy_waveRecharge)
                {
                    isDecoyCooldown = false;
                    decoyDark.SetActive(false);
                    activeCooldownText.gameObject.SetActive(false);
                    decoy_WavesCharged = 0;
                    usedDecoy = false;
                }
                else
                {
                    decoyDark.GetComponent<Image>().fillAmount = 1f - ((float)decoy_WavesCharged / (float)decoy_waveRecharge);
                    activeCooldownText.text = (decoy_waveRecharge - decoy_WavesCharged).ToString();
                }
            }

            if (choseProjectileFrenzy == true)
            {
                projectileFrency_waveRecharged += 1;
                if (projectileFrency_waveRecharged == projectileFrency_waveRecharge)
                {
                    isFrenzyCooldown = false;
                    frenzyDark.SetActive(false);
                    activeCooldownText.gameObject.SetActive(false);
                    projectileFrency_waveRecharged = 0;
                    usedProjcetileFrency = false;
                }
                else
                {
                    frenzyDark.GetComponent<Image>().fillAmount = 1f - ((float)projectileFrency_waveRecharged / (float)projectileFrency_waveRecharge);
                    activeCooldownText.text = (projectileFrency_waveRecharge - projectileFrency_waveRecharged).ToString();
                }
            }

            if (choseAntiSlime == true)
            {
                antiSlime_waveRecharged += 1;
                if (antiSlime_waveRecharged == antiSlime_waveRecharge)
                {
                    isAntiSlimeBulletCooldown = false;
                    antiDark.SetActive(false);
                    activeCooldownText.gameObject.SetActive(false);
                    antiSlime_waveRecharged = 0;
                    usedAntiSlimeBullet = false;
                }
                else
                {
                    antiDark.GetComponent<Image>().fillAmount = 1f - ((float)antiSlime_waveRecharged / (float)antiSlime_waveRecharge);
                    activeCooldownText.text = (antiSlime_waveRecharge - antiSlime_waveRecharged).ToString();
                }
            }
            #endregion
        }
    }
    #endregion

    #region Reset
    public void ResetActiveAbility()
    {
        skullDark.SetActive(false);
        punchyDark.SetActive(false);
        cloverDark.SetActive(false);
        decoyDark.SetActive(false);
        frenzyDark.SetActive(false);
        antiDark.SetActive(false);

        activeCooldownText.gameObject.SetActive(false);

        deathToSlime_WavesCharged = 0;
        sharpClicks_WaveRecharged = 0;
        clover_waveRecharged = 0;
        decoy_WavesCharged = 0;
        projectileFrency_waveRecharged = 0;
        antiSlime_waveRecharged = 0;

        usedDeathToSlimes = false;
        usedPunchyClicks = false;
        usedClover = false;
        usedDecoy = false;
        usedProjcetileFrency = false;
        usedAntiSlimeBullet = false;

        isCloverInUse = false;

        ActiveCooldown(true);

        activeTimerText.gameObject.SetActive(false);
    }
    #endregion

    #region Active cooldown timer text
    public TextMeshProUGUI activeTimerText;
    IEnumerator ActiveTimerText(float timer)
    {
        activeTimerText.gameObject.SetActive(true);
        activeTimerText.text = timer.ToString("F2");

        float zeroTime = 0;

        while (zeroTime < timer)
        {
            zeroTime += Time.deltaTime;
            activeTimerText.text = (timer - zeroTime).ToString("F2");
            yield return null;
        }

        activeTimerText.gameObject.SetActive(false);
    }
    #endregion

    #region Load Data
    public void LoadData(GameData data)
    {
        if(DemoScript.isDemo == false)
        {
            choseDeathToSlimes = data.choseDeathToSlimes;
            chosePunchyClicks = data.choseSharpClicks;
            choseClover = data.choseClover;
            choseDecoy = data.choseDecoy;
            choseProjectileFrenzy = data.choseProjectileFrenzy;
            choseAntiSlime = data.choseAntiSlime;
            isPunchyClicksUnlcoked = data.isPunchyClicksUnlcoked;
            isCloverUnlocked = data.isCloverUnlocked;
            isDecoyUnlocked = data.isDecoyUnlocked;
            isProjectileFrenzyUnlocked = data.isProjectileFrenzyUnlocked;
            isAntiSlimeBulletsUnlocked = data.isAntiSlimeBulletsUnlocked;
        }
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        if (DemoScript.isDemo == false)
        {
            data.choseDeathToSlimes = choseDeathToSlimes;
            data.choseSharpClicks = chosePunchyClicks;
            data.choseClover = choseClover;
            data.choseDecoy = choseDecoy;
            data.choseProjectileFrenzy = choseProjectileFrenzy;
            data.choseAntiSlime = choseAntiSlime;
            data.isPunchyClicksUnlcoked = isPunchyClicksUnlcoked;
            data.isCloverUnlocked = isCloverUnlocked;
            data.isDecoyUnlocked = isDecoyUnlocked;
            data.isProjectileFrenzyUnlocked = isProjectileFrenzyUnlocked;
            data.isAntiSlimeBulletsUnlocked = isAntiSlimeBulletsUnlocked;
        }
    }
    #endregion
}

