using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveMechanics : MonoBehaviour, IDataPersistence
{
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
        punchyClicksPrice = 10;
        cloverPrice = 25;
        decoyPrice = 25;
        frenzyPrice = 50;
        antiSlimeBulletPrice = 50;

        ActiveVariables();

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
        if(MetaProgressionUpgrades.activeTier == 2)
        {
            deathToSlimes_WaveRecharge = 3;
            deathToSlimes_killAmount = 12;

            sharpClicksTimeInterval = 0.1f;
            sharpClicksTimer = 9;
            sharpClicks_WaveRecharge = 2;

            cloverTimer = 8;
            clover_waveRecharge = 2;

            decoyWaveHealth = 5;
            decoy_waveRecharge = 1;

            projectileFrencyTime = 3;
            projectileFrencyProjectiles = 45;
            projectileFrency_waveRecharge = 1;

            antiSlimeBulletCount = 35;
            antiBulletDeathChance = 25;
            antiSlime_waveRecharge = 2;
            antiSlimeDamage = 22;
        }
        else if (MetaProgressionUpgrades.activeTier == 1)
        {
            deathToSlimes_WaveRecharge = 3;
            deathToSlimes_killAmount = 9;

            sharpClicksTimeInterval = 0.1f;
            sharpClicksTimer = 7;
            sharpClicks_WaveRecharge = 2;

            cloverTimer = 6;
            clover_waveRecharge = 2;

            decoyWaveHealth = 4;
            decoy_waveRecharge = 1;

            projectileFrencyTime = 2;
            projectileFrencyProjectiles = 40;
            projectileFrency_waveRecharge = 1;

            antiSlimeBulletCount = 30;
            antiBulletDeathChance = 23;
            antiSlime_waveRecharge = 2;
            antiSlimeDamage = 17;
        }
        else
        {
            deathToSlimes_WaveRecharge = 3;
            deathToSlimes_killAmount = 7;

            sharpClicksTimeInterval = 0.1f;
            sharpClicksTimer = 5;
            sharpClicks_WaveRecharge = 2;

            cloverTimer = 4;
            clover_waveRecharge = 2;

            decoyWaveHealth = 3;
            decoy_waveRecharge = 1;

            projectileFrencyTime = 2;
            projectileFrencyProjectiles = 35;
            projectileFrency_waveRecharge = 1;

            antiSlimeBulletCount = 25;
            antiBulletDeathChance = 20;
            antiSlime_waveRecharge = 2;
            antiSlimeDamage = 15;
        }
    }
    #endregion

    bool playSound;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);

        if (choseDeathToSlimes == true) { SelectActive(1); }
        if (chosePunchyClicks == true) { SelectActive(2); }
        if (choseClover == true) { SelectActive(3); }
        if (choseDecoy == true) { SelectActive(4); }
        if (choseProjectileFrenzy == true) { SelectActive(5); }
        if (choseAntiSlime == true) { SelectActive(6); }

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
                return;
            }

            if (isPunchyClicksUnlcoked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = sharpClicks.transform.position;
            chosePunchyClicks = true;
            activeNameText.text = LocalizationSCRIPT.punchyClicks + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.punchyClicks_des;
            sharpClicksIcon.SetActive(true);
        }
        if (active == 3)
        {
            if (isCloverUnlocked == false && MetaProgressionUpgrades.totalCoins >= cloverPrice && DemoScript.isDemo == false)
            {
                lockedClover.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= cloverPrice;
                isCloverUnlocked = true;
                activePriceText.SetActive(false);
                return;
            }

            if (isCloverUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = clover.transform.position;
            choseClover = true;
            activeNameText.text = LocalizationSCRIPT.clover + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.clover_des;
            cloverIcon.SetActive(true);
        }
        if (active == 4)
        {
            if (isDecoyUnlocked == false && MetaProgressionUpgrades.totalCoins >= decoyPrice && DemoScript.isDemo == false)
            {
                lockedDecoy.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= decoyPrice;
                isDecoyUnlocked = true;
                activePriceText.SetActive(false);
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
                return;
            }

            if (isProjectileFrenzyUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = projectileFrency.transform.position;
            choseProjectileFrenzy = true;
            activeNameText.text = LocalizationSCRIPT.frency + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.frency_des;
            frenzyIcon.SetActive(true);
        }
        if (active == 6)
        {
            if (isAntiSlimeBulletsUnlocked == false && MetaProgressionUpgrades.totalCoins >= antiSlimeBulletPrice && DemoScript.isDemo == false)
            {
                lockedAntiSlimeBullets.SetActive(false); audioManager.Play("Purchase"); MetaProgressionUpgrades.totalCoins -= antiSlimeBulletPrice;
                isAntiSlimeBulletsUnlocked = true;
                activePriceText.SetActive(false);
                return;
            }

            if (isAntiSlimeBulletsUnlocked == false && justChangeStuff == false) { audioManager.Play("Error"); return; }
            SetActiveOff(); if (playSound == true && justChangeStuff == false) { audioManager.Play("Select"); }
            selectedActiveIcon.transform.position = antiSlimeBullets.transform.position;
            choseAntiSlime = true;
            activeNameText.text = LocalizationSCRIPT.antiSlime + " " + LocalizationSCRIPT.SELECTED;
            acitveDesText.text = LocalizationSCRIPT.antiSlime_des;
            antiIcon.SetActive(true);
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

    #region update
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && MainMenu.isInMainMenu == false && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false && PickUpgrade.isInChooseUpgrade == false)
        {
            if(choseDeathToSlimes == true && usedDeathToSlimes == false && isDeathToSlimesCooldown == false)
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

    public void UseClover()
    {
        cloverChanceAdd = 100;
        StartCoroutine(CloverTimer());
        StartCoroutine(ActiveTimerText(cloverTimer));
    }

    IEnumerator CloverTimer()
    {
        yield return new WaitForSeconds(cloverTimer);
        cloverChanceAdd = 0;
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

    public void ShootRandomProjetile()
    {
        overlappingScript.PlaySound(6, 0, false);

        Vector2 pos = cursorColliderObject.transform.position;
        CursorMechanics.kunaiHitPos = cursorColliderObject.transform.position;
        int random = Random.Range(1,13);

        if (random == 1) { cursorMechanicsScript.SelectRandomTargetObject(1); cursorMechanicsScript.ShootPaperClip(pos); }
        if (random == 2) { cursorMechanicsScript.SelectRandomTargetObject(4); }
        if (random == 3) { cursorMechanicsScript.ShootPoisonDart(pos); }
        if (random == 4) { SlimeMechanics.boulderStartPos = pos; cursorMechanicsScript.SelectRandomActiveSlime(4); } //Boulder
        if (random == 5)
        {
            GameObject bouncy = ObjectPool.instance.GetBouncyBallFromPool();
            bouncy.transform.position = pos;
        }
        if (random == 6) { pos = cursorColliderObject.transform.position; cursorMechanicsScript.ShootThorn(pos, false); }
        if (random == 7) { nonClickUpgradeScript.StapleShoot(pos); }
        if (random == 8) { nonClickUpgradeScript.ShootTheLaser(pos); }
        if (random == 9) { cursorMechanicsScript.ShootKatana(); }
        if (random == 10) { nonClickUpgradeScript.NailShoot(pos); }
        if (random == 11) { cursorMechanicsScript.ShootLog(pos); }
        if (random == 12) { cursorMechanicsScript.ShootSawBlades(pos, false); }
    }


    void ShootProjectile()
    {
        GameObject projectile = ObjectPool.instance.GetFrenzyProjectileFromPool();
        GameObject shadow = ObjectPool.instance.GetShadowFromPool();
        shadow.transform.localScale = new Vector2(0.6f, 0.6f);

        projectile.transform.position = cursorColliderObject.transform.position;
        shadow.transform.position = new Vector2(cursorColliderObject.transform.position.x, cursorColliderObject.transform.position.y - 0.2f);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = shadow.GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.velocity = randomDirection * 8;
        rb2.velocity = randomDirection * 8;
    }
    #endregion

    #region Use antiSlime
    public GameObject strawberry;

    public void UseAntiSlime()
    {
        float angleStep = 360f / 30; // Spread evenly in a circle
        Vector3 spawnPosition = strawberry.transform.position;

        for (int i = 0; i < 30; i++)
        {
            GameObject antiSlimeBullet = ObjectPool.instance.GetAntiSlimeBulletFromPool();

            if (antiSlimeBullet != null)
            {
                antiSlimeBullet.transform.position = spawnPosition;

                float angle = i * angleStep * Mathf.Deg2Rad; 
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

                Rigidbody2D rb = antiSlimeBullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float bulletSpeed = 5f; 
                    rb.velocity = direction * bulletSpeed;
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

        ActiveCooldown(true);
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
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
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
    #endregion
}

