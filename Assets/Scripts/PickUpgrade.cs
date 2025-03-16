using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpgrade : MonoBehaviour, IDataPersistence
{
    public static int coinsThisRound;
    public static int totalCoins;
    public TextMeshProUGUI coinThisRoundText;
    public TextMeshProUGUI currentWaveText;
    public NonClickUpgrades nonClickUpgradeScript;
    public SpawnSlimes spawnSlimesScript;
    public AudioManager audioManager;
    public MainMenu mainMenuScript;
    public CursorMechanics cursorScript;

    private void Awake()
    {
        SetDemoUpgradeStats();
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.5f);
        if(SelectGameMode.easyCompleted == true) { easyCrown.SetActive(true); }
        if (SelectGameMode.normalCompleted == true) { normalCrown.SetActive(true); }
        if (SelectGameMode.hardCompleted == true) { hardCrown.SetActive(true); }

        if (SelectGameMode.bullethellCompleted == true) { bullethellCrown.SetActive(true); }
        if (SelectGameMode.flashCompleted == true) { flashCrown.SetActive(true); }
        if (SelectGameMode.fragileCompleted == true) { fragileCrown.SetActive(true); }
        if (SelectGameMode.rampageCompleted == true) { rampageCrown.SetActive(true); }
    }

    #region Check bullets on screen
    public void CheckBullets()
    {
        GameObject[] Bullet = GameObject.FindGameObjectsWithTag("EnemyBulletKicked");
        foreach (GameObject bullet in Bullet)
        {
            if (bullet.activeSelf)
            {
                ObjectPool.instance.ReturnEnemyBulletFromPool(bullet);
            }
        }

        GameObject[] EnemyBullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject enemyBullet in EnemyBullet)
        {
            if (enemyBullet.activeSelf)
            {
                ObjectPool.instance.ReturnEnemyBulletFromPool(enemyBullet);
            }
        }
    }
    #endregion

    #region Set upgrade stats
    public void SetDemoUpgradeStats()
    {
        clickDamage = 10;
        clickDamageIncrease = 2;

        clickCooldown = 0.75f;
        clickCooldownDecrease = 0.091f;

        critChance = 0;
        critIncrease = 0;
        critChanceIncrease = 10;
        critIncreaseIncrease = 2;

        cursorSlashDamage = 1;
        cursorSlashDamageIncrease = 0.4f;

        paperShotDamage = 8;
        paperShotDamageIncrease = 2;

        arrowRainDamage = 4;
        arrowRainDamageIncrease = 1;

        knifeStabDamage = 6;
        knifeStabDamageIncrease = 2;

        scytheDamage = 5;
        scytheDamageIncrease = 1;

        laserGunDamage = 10;
        laserGunDamageIncrease = 2;
        laserGunTime = 1.1f;
        laserGunTimeDecrease = 0.2f;
        laser2XChance = 10;
        laser2XChanceIncrease = 4;

        swordDamage = 3;
        swordDamageIncrease = 1;

        poisonDartDamage = 6;
        poisonDartDamageIncrease = 2;
        poisonDamage = 1;
        poisonDamageIncrease = 0.2f;

        spikyShieldDamage = 3;
        spikyShieldDamageIncrease = 1;

        chainBallDamage = 4;
        chainBallDamageIncrease = 1;
        chainBallSpeed = -210;
        chainBallSpeedIncrease = -30;

        bladeInstaKillChance = 15;
        bladeInstaKillChanceIncrease = 3;
        bladeBleedDamage = 2f;
        bladeBleedDamageIncrease = 0.5f;

        thornDamage = 4;
        thornDamageIncrease = 2;

        bigLaserDamage = 5;
        bigLaserDamageIncrease = 1;
        bigLaserTimer = 5;
        bigLaserTimerDecrease = 0.4f;

        boulderDamage = 4;
        boulderDamageIncrease = 2;

        bouncyBallDamage = 5;
        bouncyBallDamageIncrease = 1;

        meteorDamage = 10;
        meteorDamageIncrease = 2;

        staplerDamage = 6;
        staplerDamageIncrease = 2;
        staplerStunTine = 1.5f;
        staplerStunTimeIncrease += 0.3f;
        staplerTimer = 0.9f;
        staplerTimerDecrease = 0.1f;

        kunaiDamage = 3;
        kunaiDamageIncrease = 1;
        kunaiInstaKill = 12;
        kunaiIntaKillIncrease = 4;

        friendlyBulletsDamage = 6;
        friendlyBulletsIncrease = 2;

        sawBladeDamage = 3;
        sawBladeDamageIncrease = 2;

        katanaDamage = 6;
        katanaDamageIncrease = 2;

        spikeDamage = 1;
        spikeDamageIncrease = 1;

        nailGunBleedDamage = 2;
        nailGunBleedDamageIncrease = 1f;
        nailGunTimer = 1;
        nailGunTimerDecrease = 0.1f;
        nailGunMovementSpeed = 35f;
        nailGunMovementSpeedDecrease = 5f;

        bearTrapDamage = 7;
        bearTrapDamageIncrease = 2;
        bearTrapStunTimer = 2;
        bearTrapStunTimerIncrease = 0.5f;

        logDamage = 3;
        logDamageIncrease = 2;

        kickedBulletDamage = 7;
        kickedBulletDamageIncrease = 2;

        displayTotalDamageIncrease = 5;

        totalChanceIncreaseLOW = 0f; 
        totalChanceIncreaseMID = 0f; 
        totalChanceIncreaseHIGH = 0f;
    }
    #endregion

    public bool spawnedRandomUpgrade;
    public TextMeshProUGUI upgradeNameText, upgradeDescText, upgradeLevelText;
    public GameObject darkUpgrade, hoverUpgradeObject, pick1UpgradeText;
    public static bool isInChooseUpgrade;
    public ActiveMechanics activeMechanicsScript;

    public GameObject decoy;
    public GameObject dice;

    #region Update
    private void Update()
    {
        if(SpawnSlimes.slimeWave == 0) { currentWaveText.text = $"{LocalizationSCRIPT.wave} 1"; }
        else { currentWaveText.text = $"{LocalizationSCRIPT.wave} " + SpawnSlimes.slimeWave; }

        coinThisRoundText.text = coinsThisRound.ToString("F0");

        if(SelectGameMode.choseRampage == true)
        {
            if (SpawnSlimes.isRampageStop == true)
            {
                SpawnSlimes.isRampageStop = false;

                if(isInChooseUpgrade == false)
                {
                    if (upgradePopUpCoroutine == null)
                    {
                        isInChooseUpgrade = true;
                        UpgradesPopUp();
                    }
                }
            }

            if (SpawnSlimes.waveTime >= (SelectGameMode.rampage_MinuteToReach * 60) && isInWonRunScene == false)
            {
                dice.SetActive(false);
                isInWonRunScene = true; WonRunPopUp("Rampage");
            }
        }
        else if (SpawnSlimes.slimesSquished >= SpawnSlimes.slimesWaveSpawnCount && SpawnSlimes.isInGame == true)
        {
            if(spawnedRandomUpgrade == false)
            {
                isInChooseUpgrade = true;

                CheckBullets();

                if (DemoScript.isDemo == true && SpawnSlimes.slimeWave == 25)
                {
                    isInWonRunScene = true;
                    WonRunPopUp("Easy");
                }
                else if (SelectGameMode.choseEasy && SpawnSlimes.slimeWave == SelectGameMode.easy_waveToReach)
                {
                    dice.SetActive(false);
                    //SpawnSlimes.slimeWave == SelectGameMode.easy_waveToReach 
                    //SpawnSlimes.slimeWave == 1
                    isInWonRunScene = true; WonRunPopUp("Easy"); 
                }
                else if (SelectGameMode.choseNormal && SpawnSlimes.slimeWave == SelectGameMode.normal_waveToReach)
                {
                    dice.SetActive(false);
                    isInWonRunScene = true; WonRunPopUp("Normal");
                }
                else if (SelectGameMode.choseHard && SpawnSlimes.slimeWave == SelectGameMode.hard_waveToReach)
                {
                    dice.SetActive(false);
                    isInWonRunScene = true; WonRunPopUp("Hard");
                }
                else if (SelectGameMode.choseBullethell && SpawnSlimes.slimeWave == SelectGameMode.bullethell_waveToReach)
                {
                    dice.SetActive(false);
                    isInWonRunScene = true; WonRunPopUp("Bullethell");
                }
                else if (SelectGameMode.choseFlash && SpawnSlimes.slimeWave == SelectGameMode.flash_waveToReach)
                {
                    dice.SetActive(false);
                    isInWonRunScene = true; WonRunPopUp("Flash");
                }
                else if (SelectGameMode.choseFragile && SpawnSlimes.slimeWave == SelectGameMode.fragile_waveToReach)
                {
                    dice.SetActive(false);
                    isInWonRunScene = true; WonRunPopUp("Fragile");
                }
                else if (SelectGameMode.choseNarrow && SpawnSlimes.slimeWave == SelectGameMode.narrow_waveToReach)
                {
                    dice.SetActive(false);
                    isInWonRunScene = true; WonRunPopUp("Narrow");
                }
                else
                {
                    if (upgradePopUpCoroutine == null) 
                    {
                        UpgradesPopUp();
                    }
                }

                spawnedRandomUpgrade = true;
            }
        }

        if(choseUpgrade == true)
        {
            strawberryScript.Heal();

            if (ActiveMechanics.isDecoyPlaced == true)
            {
                ActiveMechanics.decoyWavesAlive += 1;
                if(ActiveMechanics.decoyWavesAlive == ActiveMechanics.decoyWaveHealth)
                {
                    activeMechanicsScript.DecoyDeath(false);
                }
            }

            cursorScript.StopSword();
            if(SelectGameMode.choseRampage == false)
            {
                mainMenuScript.RemoveSomeProjectiles();
            }
            songVolumeCoroutine = StartCoroutine(SongVolume("MusicRun", false, 0.53f, 0.2f, 0.1f, false));
            activeMechanicsScript.ActiveCooldown(false);
            ChooseUpgrade(true);
        }

        if(StrawberryMechanics.isInDeathFrame == true || isInWonRunScene == true)
        {
            darkUpgrade.SetActive(false);
            pick1UpgradeText.SetActive(false);
            upgradeNameText.gameObject.SetActive(false);
            upgradeDescText.gameObject.SetActive(false);
            upgradeLevelText.gameObject.SetActive(false);
            SetAllUpgradesOff();
        }
    }

    public Coroutine upgradePopUpCoroutine;

    IEnumerator WaitForUpgradeScreen()
    {
        songVolumeCoroutine = StartCoroutine(SongVolume("MusicRun", true, 0.53f, 0.2f, 0.1f, false));

        yield return new WaitForSeconds(0.01f);

        yield return new WaitForSeconds(0.25f);

        if(MainMenu.isInMainMenu == false)
        {
            audioManager.Play("UpgradeSpawn");

            darkUpgrade.SetActive(true);
            pick1UpgradeText.SetActive(true);
            upgradeNameText.gameObject.SetActive(true);
            upgradeDescText.gameObject.SetActive(true);
            upgradeLevelText.gameObject.SetActive(true);
            upgradeNameText.text = ""; upgradeDescText.text = ""; upgradeLevelText.text = "";

            if (MetaProgressionUpgrades.rerolls > 0)
            {
                dice.SetActive(true);
                if(rerollsThisRound >= MetaProgressionUpgrades.rerolls)
                {
                    dice.SetActive(false);
                }
            }

            upgradesSpawned = 0;
            upgrade1Number = 0; upgrade2Number = 0; upgrade3Number = 0; upgrade4Number = 0; upgrade5Number = 0;

            SpawnRandomUpgrade();
            SpawnRandomUpgrade();
            SpawnRandomUpgrade();
            if (MetaProgressionUpgrades.extraUpgradeChoises > 0) { SpawnRandomUpgrade(); }
            if (MetaProgressionUpgrades.extraUpgradeChoises > 1) { SpawnRandomUpgrade(); }
        }

        upgradePopUpCoroutine = null;
    }
    #endregion


    #region Upgrades pop uo
    public void UpgradesPopUp()
    {
        if (MetaProgressionUpgrades.extraUpgradeChoises == 0)
        {
            pos1.transform.localPosition = new Vector2(-340, 172);
            pos2.transform.localPosition = new Vector2(0, 172);
            pos3.transform.localPosition = new Vector2(340, 172);
        }
        else if (MetaProgressionUpgrades.extraUpgradeChoises == 1)
        {
            pos1.transform.localPosition = new Vector2(-495, 172);
            pos2.transform.localPosition = new Vector2(-165, 172);
            pos3.transform.localPosition = new Vector2(165, 172);
            pos4.transform.localPosition = new Vector2(495, 172);
        }
        else if (MetaProgressionUpgrades.extraUpgradeChoises == 2)
        {
            pos1.transform.localPosition = new Vector2(600, 172);
            pos2.transform.localPosition = new Vector2(300, 172);
            pos3.transform.localPosition = new Vector2(0, 172);
            pos4.transform.localPosition = new Vector2(-300, 172);
            pos5.transform.localPosition = new Vector2(-600, 172);
        }
        upgradePopUpCoroutine = StartCoroutine(WaitForUpgradeScreen());
    }
    #endregion

    #region Song volume
    public static Coroutine songVolumeCoroutine;

    IEnumerator SongVolume(string name, bool turnDown, float startVolume, float endVolume, float fadeTime, bool stopMusic)
    {
        float start = turnDown ? startVolume : endVolume;
        float end = turnDown ? endVolume : startVolume;

        float time = 0;
        float fadeTotalTime = fadeTime;

        while (time < fadeTotalTime)
        {
            time += Time.unscaledDeltaTime;
            float newVolume = Mathf.Lerp(start, end, time / fadeTotalTime);
            audioManager.ChangeVolume(name, newVolume);

            yield return null;
        }

        audioManager.ChangeVolume(name, end);

        if(stopMusic == true)
        {
            audioManager.Stop(name);
        }
    }

    public void SondVoumeSet(string name, bool turnDown, float startVolume, float endVolume, float fadeTime, bool stopMusic)
    {
        songVolumeCoroutine = StartCoroutine(SongVolume(name, turnDown, startVolume, endVolume, fadeTime, stopMusic));
    }
    #endregion


    #region set all upgrades off
    public void SetAllUpgradesOff()
    {
        clickUpgrade.SetActive(false); 
        cooldownUpgrade.SetActive(false);
        critDamageUpgrade.SetActive(false);
        healthUpgrade.SetActive(false);

        cursorSlashUpgrade.SetActive(false);
        paperShotUpgrade.SetActive(false);
        arrowRainUpgrade.SetActive(false);
        knifeOrbitalUpgrade.SetActive(false);
        scyntheUpgrade.SetActive(false);
        laserUpgrade.SetActive(false);
        swordUpgrade.SetActive(false);
        poisonDartUpgrade.SetActive(false);
        strawberryShieldUpgrade.SetActive(false);
        chainBallUpgrade.SetActive(false);
        thornUpgrade.SetActive(false);
        bigLaserUpgrade.SetActive(false);
        boulderUpgrade.SetActive(false);
        bouncyBallUpgrade.SetActive(false);
        increaseAllDamageUpgrade.SetActive(false);
        increaseAllChanceUpgrade.SetActive(false);
        meteorUpgrade.SetActive(false);
        staplerUpgrade.SetActive(false);
        kunaiUpgrade.SetActive(false);
        spikyShieldUpgrade.SetActive(false);
        friendlyBulletsUpgrade.SetActive(false);
        katanaUpgrade.SetActive(false);
        sawBladeUpgrade.SetActive(false);
        spikeUpgrade.SetActive(false);
        bladeUpgrade.SetActive(false);
        nailGunUpgrade.SetActive(false);
        bearTrapUpgrade.SetActive(false);
        logUpgrade.SetActive(false);
        legsUpgrade.SetActive(false);

    }
    #endregion

    #region Spawn random upgrade
    public GameObject pos1, pos2, pos3, pos4, pos5;

    //Non slot taking upgrades
    public GameObject clickUpgrade, cooldownUpgrade, critDamageUpgrade, healthUpgrade, knifeOrbitalUpgrade, laserUpgrade, strawberryShieldUpgrade, bigLaserUpgrade, chainBallUpgrade, increaseAllDamageUpgrade, increaseAllChanceUpgrade, staplerUpgrade, spikyShieldUpgrade, friendlyBulletsUpgrade, spikeUpgrade, bladeUpgrade, nailGunUpgrade, legsUpgrade;

    //Slot taking upgrades
    public GameObject cursorSlashUpgrade, paperShotUpgrade, arrowRainUpgrade,scyntheUpgrade, swordUpgrade, poisonDartUpgrade, thornUpgrade, boulderUpgrade, bouncyBallUpgrade, meteorUpgrade, kunaiUpgrade, katanaUpgrade, sawBladeUpgrade, bearTrapUpgrade, logUpgrade;

    //11 total upgrades

    public int upgrade1Number, upgrade2Number, upgrade3Number, upgrade4Number, upgrade5Number;

    public GameObject[] upgradesPicked;

    public void SpawnRandomUpgrade()
    {
        //changs pos1-5 if upgradeChosenCount is greater than 3
        
        if(ManageSlots.slotsAviable != ManageSlots.upgradeSlotsTaken)
        {
            #region if all upgrades can appear
            int randomUpgrade;

            int extraChance = 0;
            if(SelectGameMode.choseBullethell == true || SelectGameMode.choseFlash == true) { extraChance = 2; }

            do
            {
                randomUpgrade = Random.Range(1, 34 + extraChance);
            } while (randomUpgrade == upgrade1Number || randomUpgrade == upgrade2Number || randomUpgrade == upgrade3Number || randomUpgrade == upgrade4Number);

            if(SelectGameMode.choseFragile == true)
            {
                if(randomUpgrade == 4) { Debug.Log("Health"); SpawnRandomUpgrade(); return; } //Health
            }

            if (upgradesSpawned == 0) { upgrade1Number = randomUpgrade; }
            if (upgradesSpawned == 1) { upgrade2Number = randomUpgrade; }
            if (upgradesSpawned == 2) { upgrade3Number = randomUpgrade; }
            if (upgradesSpawned == 3) { upgrade4Number = randomUpgrade; }
            if (upgradesSpawned == 4) { upgrade5Number = randomUpgrade; }

            //Non slot taking upgrades
            if (randomUpgrade == 1) { SetUpgradePos(clickUpgrade, true); }
            else if (randomUpgrade == 2) { SetUpgradePos(cooldownUpgrade, true); }
            else if (randomUpgrade == 3) { SetUpgradePos(critDamageUpgrade, true); } 
            else if (randomUpgrade == 4) { SetUpgradePos(healthUpgrade, true); } 
            else if (randomUpgrade == 5) { SetUpgradePos(increaseAllDamageUpgrade, false); }
            else if (randomUpgrade == 6) { SetUpgradePos(increaseAllChanceUpgrade, false); }

            //passive and on click/kill upgrades
            else if (randomUpgrade == 7) { SetUpgradePos(knifeOrbitalUpgrade, false); }
            else if (randomUpgrade == 8) { SetUpgradePos(laserUpgrade, true); }
            else if (randomUpgrade == 9) { SetUpgradePos(strawberryShieldUpgrade, true); }
            else if (randomUpgrade == 10) { SetUpgradePos(cursorSlashUpgrade, true); }
            else if (randomUpgrade == 11) { SetUpgradePos(paperShotUpgrade, true); } //On slime DEATH:
            else if (randomUpgrade == 12) { SetUpgradePos(arrowRainUpgrade, true); } //On slime CLICK:
            else if (randomUpgrade == 13) { SetUpgradePos(scyntheUpgrade, true); } //On slime CLICK:
            else if (randomUpgrade == 14) { SetUpgradePos(swordUpgrade, true); } //On slime CLICK:
            else if (randomUpgrade == 15) { SetUpgradePos(poisonDartUpgrade, true); } //On slime DEATH:
            else if (randomUpgrade == 16) { SetUpgradePos(chainBallUpgrade, true); }
            else if (randomUpgrade == 17) { SetUpgradePos(thornUpgrade, true); } //On slime DEATH:
            else if (randomUpgrade == 18) { SetUpgradePos(bigLaserUpgrade, false); }
            else if (randomUpgrade == 19) { SetUpgradePos(boulderUpgrade, true); } //On slime CLICK:
            else if (randomUpgrade == 20) { SetUpgradePos(bouncyBallUpgrade, false); } //On slime DEATH:
            else if (randomUpgrade == 21) { SetUpgradePos(meteorUpgrade, false); } //On slime CLICK:
            else if (randomUpgrade == 22) { SetUpgradePos(staplerUpgrade, true); }
            else if (randomUpgrade == 23) { SetUpgradePos(kunaiUpgrade, false); } //On slime DEATH:
            else if (randomUpgrade == 24) { SetUpgradePos(spikyShieldUpgrade, false); }
            else if (randomUpgrade == 25) { SetUpgradePos(friendlyBulletsUpgrade, false); }
            else if (randomUpgrade == 26) { SetUpgradePos(sawBladeUpgrade, false); } //On slime CLICK:
            else if (randomUpgrade == 27) { SetUpgradePos(katanaUpgrade, false); } //On slime CLICK:
            else if (randomUpgrade == 28) { SetUpgradePos(spikeUpgrade, false); }
            else if (randomUpgrade == 29) { SetUpgradePos(bladeUpgrade, false); }
            else if (randomUpgrade == 30) { SetUpgradePos(nailGunUpgrade, false); }
            else if (randomUpgrade == 31) { SetUpgradePos(bearTrapUpgrade, false); } //On slime DEATH:
            else if (randomUpgrade == 32) { SetUpgradePos(logUpgrade, false); } //On slime DEATH:
            else if (randomUpgrade == 33) { SetUpgradePos(legsUpgrade, false); }

            //When bullethell or fragile is chosen
            else if (randomUpgrade == 34 || randomUpgrade == 35) 
            {
                if (SelectGameMode.choseBullethell == true) 
                {
                    int randomBullethell = Random.Range(1, 6);
                    Debug.Log("Got bullet blocking " + randomBullethell);

                    if (randomBullethell == 1) { SetUpgradePos(strawberryShieldUpgrade, true); }
                    if (randomBullethell == 2) { SetUpgradePos(bigLaserUpgrade, false); }
                    if (randomBullethell == 3) { SetUpgradePos(spikyShieldUpgrade, false); }
                    if (randomBullethell == 4) { SetUpgradePos(laserUpgrade, true); }
                    if (randomBullethell == 5) { SetUpgradePos(legsUpgrade, false); }
                }

                if (SelectGameMode.choseFlash == true)
                {
                    int randomFragile = Random.Range(1, 8);
                    Debug.Log("Got fragile " + randomFragile);

                    if (randomFragile == 1) { SetUpgradePos(knifeOrbitalUpgrade, false); }
                    if (randomFragile == 2) { SetUpgradePos(chainBallUpgrade, true); }
                    if (randomFragile == 3) { SetUpgradePos(staplerUpgrade, true); }
                    if (randomFragile == 4) { SetUpgradePos(spikyShieldUpgrade, false); }
                    if (randomFragile == 5) { SetUpgradePos(nailGunUpgrade, false); }
                    if (randomFragile == 6) { SetUpgradePos(bearTrapUpgrade, false); }
                    if (randomFragile == 7) { SetUpgradePos(bladeUpgrade, false); }
                }
            }
            #endregion
        }
        else
        {
            #region if all slots are taken
            //Execute this code if all aviable slots have been taken
            int randomUpgrade;

            int extraChance = 0;
            if (SelectGameMode.choseBullethell == true || SelectGameMode.choseFlash == true) { extraChance = 2; }

            do
            {
                randomUpgrade = Random.Range(1 - extraChance, 19 + ManageSlots.slotsAviable);
            } while (randomUpgrade == upgrade1Number || randomUpgrade == upgrade2Number || randomUpgrade == upgrade3Number || randomUpgrade == upgrade4Number);

            if (SelectGameMode.choseFragile == true)
            {
                if (randomUpgrade == 4) { Debug.Log("Health"); SpawnRandomUpgrade(); return; } //Health
            }

            if (upgradesSpawned == 0) { upgrade1Number = randomUpgrade; }
            if (upgradesSpawned == 1) { upgrade2Number = randomUpgrade; }
            if (upgradesSpawned == 2) { upgrade3Number = randomUpgrade; }
            if (upgradesSpawned == 3) { upgrade4Number = randomUpgrade; }
            if (upgradesSpawned == 4) { upgrade5Number = randomUpgrade;  }

            if (randomUpgrade == 1) { SetUpgradePos(clickUpgrade, true); }
            else if(randomUpgrade == 2) { SetUpgradePos(cooldownUpgrade, true); }
            else if (randomUpgrade == 3) { SetUpgradePos(critDamageUpgrade, true); }
            else if (randomUpgrade == 4) { SetUpgradePos(healthUpgrade, true); }
            else if (randomUpgrade == 5) { SetUpgradePos(increaseAllDamageUpgrade, false); }
            else if (randomUpgrade == 6) { SetUpgradePos(increaseAllChanceUpgrade, false); }

            else if (randomUpgrade == 7) { SetUpgradePos(knifeOrbitalUpgrade, false); }
            else if (randomUpgrade == 8) { SetUpgradePos(laserUpgrade, true); }
            else if (randomUpgrade == 9) { SetUpgradePos(strawberryShieldUpgrade, true); }
            else if (randomUpgrade == 10) { SetUpgradePos(chainBallUpgrade, true); }
            else if (randomUpgrade == 11) { SetUpgradePos(bigLaserUpgrade, false); }
            else if (randomUpgrade == 12) { SetUpgradePos(staplerUpgrade, true); }
            else if (randomUpgrade == 13) { SetUpgradePos(spikyShieldUpgrade, false); }
            else if (randomUpgrade == 14) { SetUpgradePos(friendlyBulletsUpgrade, false); }
            else if (randomUpgrade == 15) { SetUpgradePos(spikeUpgrade, false); }
            else if (randomUpgrade == 16) { SetUpgradePos(bladeUpgrade, false); }
            else if (randomUpgrade == 17) { SetUpgradePos(nailGunUpgrade, false); }
            else if (randomUpgrade == 18) { SetUpgradePos(legsUpgrade, false); }

            else if (randomUpgrade == 19) { SetUpgradePos(upgradesPicked[0], true); }
            else if (randomUpgrade == 20) { SetUpgradePos(upgradesPicked[1], true); }
            else if (randomUpgrade == 21) { SetUpgradePos(upgradesPicked[2], true); }
            else if (randomUpgrade == 22) { SetUpgradePos(upgradesPicked[3], true); }
            else if (randomUpgrade == 23) { SetUpgradePos(upgradesPicked[4], true); }
            else if (randomUpgrade == 24) { SetUpgradePos(upgradesPicked[5], true); }
            else if (randomUpgrade == 25) { SetUpgradePos(upgradesPicked[6], true); }
            else if (randomUpgrade == 26) { SetUpgradePos(upgradesPicked[7], true); }

            else if (randomUpgrade == 0 || randomUpgrade == -1)
            {
                if (SelectGameMode.choseBullethell == true)
                {
                    int randomBullethell = Random.Range(1, 6);
                    Debug.Log("Got bullet blocking " + randomBullethell);

                    if (randomBullethell == 1) { SetUpgradePos(strawberryShieldUpgrade, true); }
                    if (randomBullethell == 2) { SetUpgradePos(bigLaserUpgrade, false); }
                    if (randomBullethell == 3) { SetUpgradePos(spikyShieldUpgrade, false); }
                    if (randomBullethell == 4) { SetUpgradePos(laserUpgrade, true); }
                    if (randomBullethell == 5) { SetUpgradePos(legsUpgrade, false); }
                }

                if (SelectGameMode.choseFlash == true)
                {
                    int randomFragile = Random.Range(1, 8);
                    Debug.Log("Got flash " + randomFragile);

                    if (randomFragile == 1) { SetUpgradePos(knifeOrbitalUpgrade, false); }
                    if (randomFragile == 2) { SetUpgradePos(chainBallUpgrade, true); }
                    if (randomFragile == 3) { SetUpgradePos(staplerUpgrade, true); }
                    if (randomFragile == 4) { SetUpgradePos(spikyShieldUpgrade, false); }
                    if (randomFragile == 5) { SetUpgradePos(nailGunUpgrade, false); }
                    if (randomFragile == 6) { SetUpgradePos(bearTrapUpgrade, false); }
                    if (randomFragile == 7) { SetUpgradePos(bladeUpgrade, false); }
                }
            }
            #endregion
        }
    }

    public int upgradesSpawned;

    public void SetUpgradePos(GameObject upgrade, bool isInDemo)
    {
        if(DemoScript.isDemo == true)
        {
            if(isInDemo == false) { SpawnRandomUpgrade(); return; }
        }

        upgradesSpawned += 1;

        if (StrawberryMechanics.isInDeathFrame == true || isInWonRunScene == true)
        {
            return;
        }
        upgrade.SetActive(true);

        if (upgradesSpawned == 1) { upgrade.transform.localPosition = pos1.transform.localPosition; }
        if (upgradesSpawned == 2) { upgrade.transform.localPosition = pos2.transform.localPosition; }
        if (upgradesSpawned == 3) { upgrade.transform.localPosition = pos3.transform.localPosition; }
        if (upgradesSpawned == 4) { upgrade.transform.localPosition = pos4.transform.localPosition; }
        if (upgradesSpawned == 5) { upgrade.transform.localPosition = pos5.transform.localPosition; }
    }
    #endregion

    #region Choose upgrade
    public void ChooseUpgrade(bool spawnWave)
    {
        dice.SetActive(false);

        SpawnSlimes.slimesSpawned = 0;
        SpawnSlimes.slimesSquished = 0;

        hoverUpgradeObject.SetActive(false);
        darkUpgrade.SetActive(false);
        pick1UpgradeText.SetActive(false);
        upgradeNameText.gameObject.SetActive(false);
        upgradeDescText.gameObject.SetActive(false);
        upgradeLevelText.gameObject.SetActive(false);
        choseUpgrade = false;

        SetAllUpgradesOff();

        SpawnSlimes.isWaveCompleted = false;
        SpawnSlimes.allSlimesSpawned = false;
       
        spawnedRandomUpgrade = false;

        if (spawnWave == true)
        {
            if(DemoScript.isDemo == true) { spawnSlimesScript.SetEasyWaves(); }
            else
            {
                if(SelectGameMode.choseEasy == true) { spawnSlimesScript.SetEasyWaves(); }
                if (SelectGameMode.choseNormal == true) { spawnSlimesScript.SetNormalWaves(); }
                if (SelectGameMode.choseHard == true) { spawnSlimesScript.SetHardWaves(); }

                if (SelectGameMode.choseBullethell == true) { spawnSlimesScript.SetBullethellWaves(); }
                if (SelectGameMode.choseFlash == true) { spawnSlimesScript.SetFlashWaves(); }
                if (SelectGameMode.choseFragile == true) { spawnSlimesScript.SetFragileWaves(); }
                //if (SelectGameMode.choseNarrow == true) { spawnSlimesScript.SetNarrowWaves(); }
                //if (SelectGameMode.choseRampage == true) { spawnSlimesScript.SetRampageWaves(); }
            }
        }
    }
    #endregion

    #region Reroll
    public static int rerollsThisRound;

    public void Reroll()
    {
        audioManager.Play("Ui_click1");
        rerollsThisRound += 1;
        if(rerollsThisRound >= MetaProgressionUpgrades.rerolls)
        {
            dice.SetActive(false);
            upgradeDescText.text = ""; upgradeLevelText.text = ""; upgradeNameText.text = "";
        }
        else
        {
            upgradeDescText.text = $"reroll your upgrades. {MetaProgressionUpgrades.rerolls - rerollsThisRound} left"; upgradeLevelText.text = "";
            upgradeNameText.text = "reroll";
        }

        SetAllUpgradesOff();
        upgradePopUpCoroutine = null;

        upgradesSpawned = 0;
        upgrade1Number = 0; upgrade2Number = 0; upgrade3Number = 0; upgrade4Number = 0; upgrade5Number = 0;

        SpawnRandomUpgrade();
        SpawnRandomUpgrade();
        SpawnRandomUpgrade();
        if (MetaProgressionUpgrades.extraUpgradeChoises > 0) { SpawnRandomUpgrade(); }
        if (MetaProgressionUpgrades.extraUpgradeChoises > 1) { SpawnRandomUpgrade(); }
    }
    #endregion

    public static bool choseUpgrade;

    public ManageSlots manageSlotsScript;
    public StrawberryMechanics strawberryScript;

    //Stat increasing upgrades
    #region Stronger clicks
    public static float clickDamage, clickDamageIncrease;
    public static int clickDamageLevel;

    public void Upgrade_StrongerClicks()
    {
        clickDamage += clickDamageIncrease;
        clickDamageLevel += 1;
        ThingsAllDo();
    }
    #endregion

    #region Critical clicks
    public static bool chosenCriticalClicks;
    public static int critChance, critChanceIncrease, critLevel;
    public static float critIncrease, critIncreaseIncrease;

    public void Upgrade_CriticalClicks()
    {
        critLevel += 1;
        ThingsAllDo();

        if (chosenCriticalClicks == false)
        {
            critChance = critChanceIncrease;
            critIncrease = critIncreaseIncrease;

            critChanceIncrease = 5;
            critIncreaseIncrease = 0.2f;
        }
        else
        {
            critChance += critChanceIncrease;
            critIncrease += critIncreaseIncrease;

            if(critChanceIncrease <= 1) { critChanceIncrease = 1; }
            else
            {
                critChanceIncrease -= 1;
            }

            if (critIncreaseIncrease <= 0.1) { critIncreaseIncrease = 0.2f; }
            else
            {
                critIncreaseIncrease -= 0.01f;
            }
        }
        chosenCriticalClicks = true;
    }
    #endregion

    #region Click cooldown
    public static float clickCooldown;
    public static float clickCooldownDecrease;
    public static int clickCooldownLevel;

    public void Upgrade_ClickCooldown()
    {
        ThingsAllDo();

        clickCooldown -= clickCooldownDecrease;

        if (clickCooldown <= 0.4)
        {
            clickCooldownDecrease = 0.025f;
        }
        else
        {
            clickCooldownDecrease = clickCooldown / 8;
        }

        if (clickCooldown < 0.1f) { clickCooldownDecrease = 0.01f; }
        if (clickCooldown < 0.05f) { clickCooldownDecrease = 0f; }

        clickCooldownLevel += 1;
    }
    #endregion

    #region Increase health
    public static int healthLevel;

    public void Upgrade_StrawberryHealth()
    {
        healthLevel += 1;
        ThingsAllDo();

        if (StrawberryMechanics.strawberryHealth < 10)
        {
            if(StrawberryMechanics.isHalfHeart == true)
            {
                StrawberryMechanics.strawberryHealth += 1;
                StrawberryMechanics.isHalfHeart = false;
            }
            else
            {
                StrawberryMechanics.isHalfHeart = true;
            }
           
        }

        strawberryScript.SetHealth();
    }
    #endregion

    #region Increase all damage
    public static float totalIncreaseDamage, totalIncreaseDamageIncrease;
    public static float displayTotalDamageIncrease;
    public static int increaseAllDamageLevel;
    public static bool choseDamageIncrease;

    public void Upgrade_IncreaseAllDamage()
    {
        if(choseDamageIncrease == false)
        {
            totalIncreaseDamage = 5;
            totalIncreaseDamageIncrease = 3;
            choseDamageIncrease = true;
        }
        else
        {
            totalIncreaseDamage += totalIncreaseDamageIncrease;
        }
      
        increaseAllDamageLevel += 1;
        
        ThingsAllDo();
    }
    #endregion

    #region Increase all upgrade chance
    public static float totalChanceIncreaseLOW, totalChanceIncreaseIncreaseLOW;
    public static float totalChanceIncreaseMID, totalChanceIncreaseIncreaseMID;
    public static float totalChanceIncreaseHIGH, totalChanceIncreaseIncreaseHIGH;

    public static int totalChanceIncreaseLevel;
    public static bool choseIncreaseAllChance;
    public GameObject ladyBugOnStrawberry;

    public void Upgrade_IncreaseAllChance()
    {
        if(choseIncreaseAllChance == false)
        {
            ladyBugOnStrawberry.SetActive(true);
            totalChanceIncreaseLOW = 0.5f; totalChanceIncreaseIncreaseLOW = 0.3f;
            totalChanceIncreaseMID = 1; totalChanceIncreaseIncreaseMID = 0.8f;
            totalChanceIncreaseHIGH = 1.5f; totalChanceIncreaseIncreaseHIGH = 1f;

             choseIncreaseAllChance = true;
        }
        else
        {
            totalChanceIncreaseLOW += totalChanceIncreaseIncreaseLOW;
            totalChanceIncreaseMID += totalChanceIncreaseIncreaseMID;
            totalChanceIncreaseHIGH += totalChanceIncreaseIncreaseHIGH;
        }

        totalChanceIncreaseLevel += 1;

        ThingsAllDo();
    }
    #endregion

    //Passive upgrades
    #region Knife orbital
    public static int knifeOrbitalLevel;
    public static bool choseKnifeOrbital;

    public static float knifeStabDamage, knifeStabDamageIncrease;

    public GameObject knifeOrbital;

    public void Upgrade_KnifeOrbital()
    {
        knifeOrbital.SetActive(true);

        ThingsAllDo();

        knifeOrbitalLevel += 1;

        if (choseKnifeOrbital == false) { }
        else
        {
            knifeStabDamage += knifeStabDamageIncrease;
        }

        choseKnifeOrbital = true;
    }
    #endregion

    #region chainBall
    public static int chainBallLevel;
    public static bool choseChainBall;

    public static float chainBallDamage, chainBallDamageIncrease, chainBallSpeed, chainBallSpeedIncrease;

    public GameObject chainBall;

    public void Upgrade_ChainBall()
    {
        chainBall.SetActive(true);

        ThingsAllDo();

        chainBallLevel += 1;

        if (choseChainBall == false) { }
        else
        {
            chainBallDamage += chainBallDamageIncrease;
            chainBallSpeed += chainBallSpeedIncrease;
        }

        choseChainBall = true;
    }
    #endregion

    #region chainedBlade
    public static int bladeLevel;
    public static bool choseBlade;

    public static float bladeInstaKillChance, bladeInstaKillChanceIncrease, bladeBleedDamage, bladeBleedDamageIncrease;

    public GameObject chainBlade;

    public void Upgrade_ChainBlade()
    {
        chainBlade.SetActive(true);

        ThingsAllDo();

        bladeLevel += 1;

        if (choseBlade == false) 
        {
            choseBlade = true;
        }
        else
        {
            bladeInstaKillChance += bladeInstaKillChanceIncrease;
            bladeBleedDamage += bladeBleedDamageIncrease;
            chainBallSpeed += chainBallSpeedIncrease;
        }

    }
    #endregion

    #region Laser gun orbital
    public static int laserGunLevel;
    public static bool choseLaserGun;

    public static float laserGunDamage, laserGunDamageIncrease;
    public static float laserGunTime, laserGunTimeDecrease;
    public static int laser2XChance, laser2XChanceIncrease;

    public GameObject laserPistol;

    public void Upgrade_LaserGun()
    {
        ThingsAllDo();
     
        if (choseLaserGun == true)
        {
            laserGunDamage += laserGunDamageIncrease;
            laser2XChance += laser2XChanceIncrease;

            laserGunTime -= laserGunTimeDecrease;
            laserGunTimeDecrease = laserGunTime / 9;
        }
        else
        {
            SetOrbitalPosition(laserPistol);
            laserPistol.SetActive(true);
            NonClickUpgrades.laserGunStartPos = laserPistol.transform.localPosition;
            NonClickUpgrades.startLaserGun = true;
        }

        choseLaserGun = true;
        laserGunLevel += 1;
    }
    #endregion

    #region shield
    public static int strawberryShieldLevel;
    public static bool choseStrawberryShield;

    public GameObject shield;
    public float shieldSize;
    public static float shieldSizeIncrease;

    public void Upgrade_StrawberryShield()
    {
        ThingsAllDo();

        shield.SetActive(true);
        if(choseStrawberryShield == true)
        {
            shield.transform.localScale = new Vector2(1.1f + shieldSizeIncrease, 1.1f + shieldSizeIncrease);
        }
        else
        {
            shield.transform.localScale = new Vector2(1.1f, 1.1f);
        }

        shieldSizeIncrease += 0.22f;

        choseStrawberryShield = true;
        strawberryShieldLevel += 1;
    }
    #endregion

    #region spiky shield
    public static int spikyShieldLevel;
    public static bool choseSpikyShield;

    public GameObject spikyShield;
    public float spikyShieldSize;
    public static float spikyShieldSizeIncrease;
    public static float spikyShieldDamage, spikyShieldDamageIncrease;

    public void Upgrade_SpikeShield()
    {
        ThingsAllDo();

        spikyShield.SetActive(true);
        if (choseSpikyShield == true)
        {
            spikyShield.transform.localScale = new Vector2(0.85f + spikyShieldSizeIncrease, 0.85f + spikyShieldSizeIncrease);
        }
        else
        {
            spikyShield.transform.localScale = new Vector2(0.85f, 0.85f);
        }

        spikyShieldSizeIncrease += 0.13f;

        choseSpikyShield = true;
        spikyShieldLevel += 1;
    }
    #endregion

    #region bigLaser
    public static int bigLaserLevel;
    public static bool choseBigLaser;
    public static int bigLaserDamage, bigLaserDamageIncrease;
    public static float bigLaserTimer, bigLaserTimerDecrease;

    public static bool startBigLaser;

    public GameObject bigLaser;

    public void Upgrade_BigLaser()
    {
        ThingsAllDo();

        if (choseBigLaser == true)
        {
            bigLaserDamage += bigLaserDamageIncrease;
            bigLaserTimer -= bigLaserTimerDecrease;
            bigLaserTimerDecrease = bigLaserTimer / 16;
        }
        else
        {
            SetOrbitalPosition(bigLaser);
            bigLaser.SetActive(true);
            NonClickUpgrades.bigLaserGunStartPos = bigLaser.transform.localPosition;
            startBigLaser = true;
            choseBigLaser = true;
        }

        bigLaserLevel += 1;
    }
    #endregion

    #region Stapler
    public static int staplerLevel;
    public static bool choseStapler;

    public static float staplerDamage, staplerDamageIncrease;
    public static float staplerTimer, staplerTimerDecrease;
    public static float staplerStunTine, staplerStunTimeIncrease;

    public static bool startStapler;

    public GameObject staplerGun;

    public void Upgrade_Stapler()
    {
        ThingsAllDo();

        if (choseStapler == false)
        {
            SetOrbitalPosition(staplerGun);
            staplerGun.SetActive(true);
            NonClickUpgrades.staplerGunStartPos = staplerGun.transform.localPosition;
            startStapler = true;
        }
        else
        {
            staplerDamage += staplerDamageIncrease;
            staplerStunTine += staplerStunTimeIncrease;

            staplerTimer -= staplerTimerDecrease;
            staplerTimerDecrease = staplerTimer / 9;
        }

        choseStapler = true;

        staplerLevel += 1;
    }
    #endregion

    #region Nail Gun
    public static int nailGunLevel;
    public static bool choseNailGun;

    public static float nailGunBleedDamage, nailGunBleedDamageIncrease;
    public static float nailGunTimer, nailGunTimerDecrease;
    public static float nailGunMovementSpeed, nailGunMovementSpeedDecrease;

    public static bool startNailGun;

    public GameObject nailGun;

    public void UpgradeNailGun()
    {
        ThingsAllDo();

        if (choseNailGun == false)
        {
            SetOrbitalPosition(nailGun);
            nailGun.SetActive(true);
            NonClickUpgrades.nailGunStartPos = nailGun.transform.localPosition;

            startNailGun = true;
            choseNailGun = true;
        }
        else
        {
            nailGunBleedDamage += nailGunBleedDamageIncrease;

            nailGunMovementSpeed += nailGunMovementSpeedDecrease;
            nailGunMovementSpeedDecrease *= 0.9f;

            nailGunTimer -= nailGunTimerDecrease;
            nailGunTimerDecrease = nailGunTimer / 9;
        }

        nailGunLevel += 1;
    }
    #endregion

    #region friendly bullets
    public static int friendlyBulletsLevel;
    public static bool choseFriendlyBullets;

    public static float friendlyBulletsDamage, friendlyBulletsIncrease;

    public void Upgrade_FriendlyBullets()
    {
        ThingsAllDo();

        if (choseFriendlyBullets == false)
        {
            choseFriendlyBullets = true;
        }
        else
        {
            friendlyBulletsDamage += friendlyBulletsIncrease;
        }

        friendlyBulletsLevel += 1;
    }
    #endregion
    
    #region Small spikes
    public static int spikesLevel;
    public static bool choseSpikes;

    public static float spikeDamage, spikeDamageIncrease;

    public void Upgrade_Spike()
    {
        ThingsAllDo();

        if (choseSpikes == false)
        {
            choseSpikes = true;
        }
        else
        {
            spikeDamage += spikeDamageIncrease;
        }

        spikesLevel += 1;
    }
    #endregion

    #region Legs
    public static int legsLevel;
    public static bool choseLegs;

    public static float kickedBulletDamage, kickedBulletDamageIncrease;

    public GameObject strawberryLegs;

    public void Upgrade_Legs()
    {
        ThingsAllDo();

        if (choseLegs == false)
        {
            choseLegs = true;
            strawberryLegs.SetActive(true);
        }
        else
        {
            kickedBulletDamage += kickedBulletDamageIncrease;
        }

        legsLevel += 1;
    }
    #endregion

    //Uses ability slots (is triggered by clicking, or moving the cursor)
    #region Cursor slash
    public static bool choseCursorSlash;
    public static int cursorSlashSlotPosition;
    public static int cursorSlashLevel;

    public static float cursorSlashDamage, cursorSlashDamageIncrease;
    public GameObject cursorSlash;

    public void Upgrade_CursorSlash()
    {
        ThingsAllDo();

        if (choseCursorSlash == false)
        {
            cursorSlash.GetComponent<TrailRenderer>().enabled = true;
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = cursorSlashUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(0);
            cursorSlashSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            cursorSlashDamage += cursorSlashDamageIncrease;
        }

        choseCursorSlash = true;

        choseUpgrade = true;

        cursorSlashLevel += 1;
        ManageSlots.storeSlotLevel[cursorSlashSlotPosition] = cursorSlashLevel;

        manageSlotsScript.SetLevelText(cursorSlashSlotPosition, cursorSlashLevel);
    }
    #endregion

    //On death
    #region PaperShot
    public static int paperShotLevel;
    public static int paperShotSlotPosition;
    public static bool chosePaperShot;

    public static float paperShotDamage, paperShotDamageIncrease;

    public void Upgrade_PaperShot()
    {
        ThingsAllDo();

        if (chosePaperShot == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = paperShotUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(1);
            paperShotSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            paperShotDamage += paperShotDamageIncrease;
        }

        chosePaperShot = true;
       
        paperShotLevel += 1;
        ManageSlots.storeSlotLevel[paperShotSlotPosition] = paperShotLevel;
        manageSlotsScript.SetLevelText(paperShotSlotPosition, paperShotLevel);
    }
    #endregion

    #region poison dart
    public static int poisonDartLevel;
    public static int poisonDartPosition;
    public static bool chosePoisonDart;

    public static int poisonDartDamage, poisonDartDamageIncrease;
    public static float poisonDamage, poisonDamageIncrease;

    public void Upgrade_PoisonDart()
    {
        ThingsAllDo();

        if (chosePoisonDart == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = poisonDartUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(5);
            poisonDartPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            poisonDamage += poisonDamageIncrease;
            poisonDartDamage += poisonDartDamageIncrease;
        }

        chosePoisonDart = true;

        poisonDartLevel += 1;
        ManageSlots.storeSlotLevel[poisonDartPosition] = poisonDartLevel;
        manageSlotsScript.SetLevelText(poisonDartPosition, poisonDartLevel);
    }
    #endregion

    #region thorn
    public static int thornLevel;
    public static int thornPosition;
    public static bool choseThorn;

    public static int thornDamage, thornDamageIncrease;

    public void UpgradeThorn()
    {
        ThingsAllDo();

        if (choseThorn == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = thornUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(6);
            thornPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            thornDamage += thornDamageIncrease;
        }

        choseThorn = true;

        thornLevel += 1;
        ManageSlots.storeSlotLevel[thornPosition] = thornLevel;
        manageSlotsScript.SetLevelText(thornPosition, thornLevel);
    }
    #endregion

    #region bouncy ball
    public static int bouncyBallLevel;
    public static int bouncyBallPosition;
    public static bool choseBouncyBall;

    public static int bouncyBallDamage, bouncyBallDamageIncrease;

    public void Upgrade_BouncyBall()
    {
        ThingsAllDo();

        if (choseBouncyBall == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = bouncyBallUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(8);
            bouncyBallPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            bouncyBallDamage += bouncyBallDamageIncrease;
        }

        choseBouncyBall = true;

        bouncyBallLevel += 1;
        ManageSlots.storeSlotLevel[bouncyBallPosition] = bouncyBallLevel;
        manageSlotsScript.SetLevelText(bouncyBallPosition, bouncyBallLevel);
    }
    #endregion

    #region Kunai
    public static int kunaiLevel;
    public static int kunaiSlotPosition;
    public static bool choseKunai;

    public static float kunaiDamage, kunaiDamageIncrease;
    public static float kunaiInstaKill, kunaiIntaKillIncrease;

    public void Upgrade_Kunai()
    {
        ThingsAllDo();

        if (choseKunai == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = kunaiUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(10);
            kunaiSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            kunaiDamage += kunaiDamageIncrease;
        }

        choseKunai = true;

        kunaiLevel += 1;
        ManageSlots.storeSlotLevel[kunaiSlotPosition] = kunaiLevel;
        manageSlotsScript.SetLevelText(kunaiSlotPosition, kunaiLevel);
    }
    #endregion

    #region Bear trap
    public static int bearTrapLevel;
    public static bool choseBearTrap;
    public static int bearTrapSlotPosition;

    public static float bearTrapDamage, bearTrapDamageIncrease, bearTrapStunTimer, bearTrapStunTimerIncrease;

    public void Upgrade_BearTrap()
    {
        ThingsAllDo();

        if (choseBearTrap == false)
        {
            choseBearTrap = true;

            upgradesPicked[ManageSlots.upgradeSlotsTaken] = bearTrapUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(13);
            bearTrapSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            bearTrapDamage += bearTrapDamageIncrease;
            bearTrapStunTimer += bearTrapStunTimerIncrease;
        }

        bearTrapLevel += 1;
        ManageSlots.storeSlotLevel[bearTrapSlotPosition] = bearTrapLevel;
        manageSlotsScript.SetLevelText(bearTrapSlotPosition, bearTrapLevel);
    }
    #endregion

    //On click
    #region Arrow Rain
    public static int arrowRainLevel;
    public static int arrowRainSlotPosition;
    public static bool choseArrowRain;

    public static int arrowRainDamage, arrowRainDamageIncrease;

    public void Upgrade_ArrowRain()
    {
        ThingsAllDo();

        choseUpgrade = true;

        if (choseArrowRain == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = arrowRainUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(2);
            arrowRainSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            arrowRainDamage += arrowRainDamageIncrease;
        }

        choseArrowRain = true;

        arrowRainLevel += 1;
        ManageSlots.storeSlotLevel[arrowRainSlotPosition] = arrowRainLevel;
        manageSlotsScript.SetLevelText(arrowRainSlotPosition, arrowRainLevel);
    }
    #endregion

    #region Scythe
    public static int scytheLevel;
    public static int scytheSlotPosition;
    public static bool choseScythe;

    public static float scytheDamage, scytheDamageIncrease;

    public void Upgrade_Scythe()
    {
        ThingsAllDo();

        if (choseScythe == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = scyntheUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(3);
            scytheSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            scytheDamage += scytheDamageIncrease;
        }

        choseScythe = true;

        scytheLevel += 1;
        ManageSlots.storeSlotLevel[scytheSlotPosition] = scytheLevel;
        manageSlotsScript.SetLevelText(scytheSlotPosition, scytheLevel);
    }
    #endregion

    #region Sword
    public static int swordLevel;
    public static int swordSlotPosition;
    public static bool choseSword;

    public static float swordDamage, swordDamageIncrease;

    public void UpgradeSword()
    {
        ThingsAllDo();

        if (choseSword == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = swordUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(4);
            swordSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            swordDamage += swordDamageIncrease;
        }

        choseSword = true;

        swordLevel += 1;
        ManageSlots.storeSlotLevel[swordSlotPosition] = swordLevel;
        manageSlotsScript.SetLevelText(swordSlotPosition, swordLevel);
    }
    #endregion

    #region Boulder
    public static int boulderLevel;
    public static int boulderSlotPosition;
    public static bool choseBoulder;

    public static float boulderDamage, boulderDamageIncrease;

    public void Upgrade_Boulder()
    {
        ThingsAllDo();

        if (choseBoulder == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = boulderUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(7);
            boulderSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            boulderDamage += boulderDamageIncrease;
        }

        choseBoulder = true;

        boulderLevel += 1;
        ManageSlots.storeSlotLevel[boulderSlotPosition] = boulderLevel;
        manageSlotsScript.SetLevelText(boulderSlotPosition, boulderLevel);
    }
    #endregion

    #region Meteor
    public static int meteorLevel;
    public static int meteorSlotPosition;
    public static bool choseMeteor;

    public static float meteorDamage, meteorDamageIncrease;

    public void Upgrade_Meteor()
    {
        ThingsAllDo();

        if (choseMeteor == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = meteorUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(9);
            meteorSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            meteorDamage += meteorDamageIncrease;
        }

        choseMeteor = true;

        meteorLevel += 1;
        ManageSlots.storeSlotLevel[meteorSlotPosition] = meteorLevel;
        manageSlotsScript.SetLevelText(meteorSlotPosition, meteorLevel);
    }
    #endregion

    #region SawBlade
    public static int sawBladeLevel;
    public static int sawBladeSlotPosition;
    public static bool choseSawBlade;

    public static float sawBladeDamage, sawBladeDamageIncrease;

    public void Upgrade_SawBlade()
    {
        ThingsAllDo();

        if (choseSawBlade == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = sawBladeUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(11);
            sawBladeSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            sawBladeDamage += sawBladeDamageIncrease;
        }

        choseSawBlade = true;

        sawBladeLevel += 1;
        ManageSlots.storeSlotLevel[sawBladeSlotPosition] = sawBladeLevel;
        manageSlotsScript.SetLevelText(sawBladeSlotPosition, sawBladeLevel);
    }
    #endregion

    #region Katana
    public static int katanaLevel;
    public static int katanaSlotPosition;
    public static bool choseKatana;

    public static float katanaDamage, katanaDamageIncrease;

    public void Upgrade_Katana()
    {
        ThingsAllDo();

        if (choseKatana == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = katanaUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(12); 
            katanaSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
        }
        else
        {
            katanaDamage += katanaDamageIncrease;
        }

        choseKatana = true;

        katanaLevel += 1;
        ManageSlots.storeSlotLevel[katanaSlotPosition] = katanaLevel;
        manageSlotsScript.SetLevelText(katanaSlotPosition, katanaLevel);
    }
    #endregion

    #region Spiky log
    public static int logLevel;
    public static int logSlotPosition;
    public static bool choseLog;

    public static float logDamage, logDamageIncrease;

    public void Upgrade_Log()
    {
        ThingsAllDo();

        if (choseLog == false)
        {
            upgradesPicked[ManageSlots.upgradeSlotsTaken] = logUpgrade;

            manageSlotsScript.SetSlotAlpha(ManageSlots.upgradeSlotsTaken, 1);
            manageSlotsScript.SetIconAndText(14);
            logSlotPosition = ManageSlots.upgradeSlotsTaken;
            ManageSlots.upgradeSlotsTaken += 1;
            choseLog = true;
        }
        else
        {
            logDamage += logDamageIncrease;
        }

        logLevel += 1;
        ManageSlots.storeSlotLevel[logSlotPosition] = logLevel;
        manageSlotsScript.SetLevelText(logSlotPosition, logLevel);
    }
    #endregion


    #region set orbital position
    public int totalShootingOrbitals, totalShieldOrbitals;

    public void SetOrbitalPosition(GameObject orbital)
    {
        totalShootingOrbitals += 1;
        if (totalShootingOrbitals == 1)
        {
            orbital.transform.localPosition = new Vector2(-123, 0);
            orbital.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (totalShootingOrbitals == 2)
        {
            orbital.transform.localPosition = new Vector2(123, 0);
            orbital.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (totalShootingOrbitals == 3)
        {
            orbital.transform.localPosition = new Vector2(0, 123);
            orbital.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (totalShootingOrbitals == 4)
        {
            orbital.transform.localPosition = new Vector2(0, -123);
            orbital.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
    #endregion

    public GameObject blockFrame;

    #region Run won
    public GameObject easyCrown, normalCrown, hardCrown, bullethellCrown, flashCrown, fragileCrown, rampageCrown;

    public GameObject wonRunFrame;
    public Animation wishlistText;
    public GameObject demoFrame, fullGameFrame;
    public static bool isInWonRunScene;

    public void WonRunPopUp(string gameMode)
    {
        if (StrawberryMechanics.isInDeathFrame == true)
        {
            return;
        }

        SpawnSlimes.isRampagePlaying = false;
        StartCoroutine(WaitForWin(gameMode));
    }

    IEnumerator WaitForWin(string gameMode)
    {
        yield return new WaitForSeconds(1);

        audioManager.Play("GameWin");

        if (DemoScript.isDemo == false)
        {
            StartCoroutine(SetWinTexts());
            demoFrame.SetActive(false);
            fullGameFrame.SetActive(true);
        }
        else
        {
            demoFrame.SetActive(true);
            fullGameFrame.SetActive(false);
        }

        wonRunFrame.SetActive(true);
        wonRunFrame.GetComponent<Animation>().Play();

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

    public TextMeshProUGUI coinCollected, coinsCollectedAmount, totalCoinsText, totalCoinsAmount;
    public TextMeshProUGUI gamemodeCompletedText, gamemodeCompletedName, completionRewardText, completionRewardAmount;

    public GameObject playAgainBtn, mainMenuBtn, exitGameBtn;

    IEnumerator SetWinTexts()
    {
        playAgainBtn.SetActive(false); mainMenuBtn.SetActive(false); exitGameBtn.SetActive(false);
        coinCollected.gameObject.SetActive(false); coinsCollectedAmount.gameObject.SetActive(false);
        totalCoinsAmount.gameObject.SetActive(false); totalCoinsText.gameObject.SetActive(false);
        gamemodeCompletedText.gameObject.SetActive(false); gamemodeCompletedName.gameObject.SetActive(false);
        completionRewardText.gameObject.SetActive(false); completionRewardAmount.gameObject.SetActive(false);

        coinsCollectedAmount.text = "+" + coinsThisRound;

        MetaProgressionUpgrades.totalCoins += coinsThisRound;

        if (SelectGameMode.choseEasy == true) { gamemodeCompletedName.text = "easy"; easyCrown.SetActive(true); }
        if (SelectGameMode.choseNormal == true) { gamemodeCompletedName.text = "normal"; normalCrown.SetActive(true); }
        if (SelectGameMode.choseHard == true) { gamemodeCompletedName.text = "hard"; hardCrown.SetActive(true); }
        if (SelectGameMode.choseBullethell == true) { gamemodeCompletedName.text = "bullethell"; bullethellCrown.SetActive(true); }
        if (SelectGameMode.choseFlash == true) { gamemodeCompletedName.text = "flash"; flashCrown.SetActive(true); }
        if (SelectGameMode.choseFragile == true) { gamemodeCompletedName.text = "fragile"; fragileCrown.SetActive(true); }
        if (SelectGameMode.choseRampage == true) { gamemodeCompletedName.text = "rampage"; rampageCrown.SetActive(true); }

        if (SelectGameMode.choseEasy == true && SelectGameMode.easyCompleted == false) 
        { 
            MetaProgressionUpgrades.totalCoins += SelectGameMode.easyReward; completionRewardAmount.text = "+" + SelectGameMode.easyReward;
        }
        else if (SelectGameMode.choseNormal == true && SelectGameMode.normalCompleted == false) 
        { 
            MetaProgressionUpgrades.totalCoins += SelectGameMode.normalReward; completionRewardAmount.text = "+" + SelectGameMode.normalReward;
        }
        else if (SelectGameMode.choseHard == true && SelectGameMode.hardCompleted == false)
        {
            MetaProgressionUpgrades.totalCoins += SelectGameMode.hardReward; completionRewardAmount.text = "+" + SelectGameMode.hardReward;
        }
        else if (SelectGameMode.choseBullethell == true && SelectGameMode.bullethellCompleted == false) 
        {
            MetaProgressionUpgrades.totalCoins += SelectGameMode.bullethellReward; completionRewardAmount.text = "+" + SelectGameMode.bullethellReward;
        }
        else if (SelectGameMode.choseFlash == true && SelectGameMode.flashCompleted == false) 
        { 
            MetaProgressionUpgrades.totalCoins += SelectGameMode.flashReward; completionRewardAmount.text = "+" + SelectGameMode.flashReward;
        }
        else if (SelectGameMode.choseFragile == true && SelectGameMode.fragileCompleted == false)
        {
            MetaProgressionUpgrades.totalCoins += SelectGameMode.fragileReward; completionRewardAmount.text = "+" + SelectGameMode.fragileReward;
        }
        else if (SelectGameMode.choseNarrow == true && SelectGameMode.narrowCompleted == false) 
        {
            MetaProgressionUpgrades.totalCoins += SelectGameMode.narrowReward; completionRewardAmount.text = "+" + SelectGameMode.narrowReward;
        }
        else if (SelectGameMode.choseRampage == true && SelectGameMode.rampageCompleted == false)
        {
            MetaProgressionUpgrades.totalCoins += SelectGameMode.rampageReward; completionRewardAmount.text = "+" + SelectGameMode.rampageReward;
        }
        else
        {
            completionRewardAmount.text = "+0";
        }

        totalCoinsAmount.text = "" + MetaProgressionUpgrades.totalCoins;

        yield return new WaitForSeconds(0.65f);
        gamemodeCompletedText.gameObject.SetActive(true); gamemodeCompletedName.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        completionRewardText.gameObject.SetActive(true); completionRewardAmount.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        coinCollected.gameObject.SetActive(true); coinsCollectedAmount.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        totalCoinsText.gameObject.SetActive(true); totalCoinsAmount.gameObject.SetActive(true); audioManager.Play("Pop");
        yield return new WaitForSeconds(0.15f);
        playAgainBtn.SetActive(true); mainMenuBtn.SetActive(true); exitGameBtn.SetActive(true); audioManager.Play("Pop");
    }
    
    public void PlayAgainBtn()
    {
        coinsThisRound = 0;

        blockFrame.SetActive(true);
        wonRunFrame.SetActive(false);
        MainMenu.clickedResetRun = true;
    }

    public void MainMenuBtn()
    {
        coinsThisRound = 0;

        blockFrame.SetActive(true);
        wonRunFrame.SetActive(false);
        MainMenu.clickedResetRun = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.35f);
        blockFrame.SetActive(false);
    }
    #endregion


    #region All upgrades reset
    public void ResetUpgrades()
    {
        //Remember to also reset the damage and everything else

        SetDemoUpgradeStats();

        #region set objects false
        knifeOrbital.SetActive(false);
        chainBall.SetActive(false);
        laserPistol.SetActive(false);
        shield.SetActive(false);
        bigLaser.SetActive(false);
        staplerGun.SetActive(false);
        strawberryLegs.SetActive(false);
        ladyBugOnStrawberry.SetActive(false);
        nailGun.SetActive(false);
        chainBlade.SetActive(false);
        spikyShield.SetActive(false);
        cursorSlash.GetComponent<TrailRenderer>().enabled = false;
        #endregion

        #region reset all levels
        shieldSizeIncrease = 0;

        clickDamageLevel = 0;
        critLevel = 0;
        clickCooldownLevel = 0;
        healthLevel = 0;
        increaseAllDamageLevel = 0;
        totalChanceIncreaseLevel = 0;
        knifeOrbitalLevel = 0;
        chainBallLevel = 0;
        laserGunLevel = 0;
        strawberryShieldLevel = 0;
        bigLaserLevel = 0;
        staplerLevel = 0;
        cursorSlashLevel = 0;
        paperShotLevel = 0;
        poisonDartLevel = 0;
        thornLevel = 0;
        bouncyBallLevel = 0;
        arrowRainLevel = 0;
        scytheLevel = 0;
        swordLevel = 0;
        boulderLevel = 0;
        meteorLevel = 0;
        spikyShieldLevel = 0;
        bladeLevel = 0;
        kunaiLevel = 0;
        friendlyBulletsLevel = 0;
        sawBladeLevel = 0;
        katanaLevel = 0;
        spikesLevel = 0;
        nailGunLevel = 0;
        bearTrapLevel = 0;
        legsLevel = 0;
        logLevel = 0;
        #endregion

        #region set all "chosen" false
        chosenCriticalClicks = false;
        choseKnifeOrbital = false;
        choseChainBall = false;
        choseLaserGun = false;
        choseStrawberryShield = false;
        choseBigLaser = false;
        choseStapler = false;
        choseCursorSlash = false;
        chosePaperShot = false;
        chosePoisonDart = false;
        choseThorn = false;
        choseBouncyBall = false;
        choseArrowRain = false;
        choseScythe = false;
        choseSword = false;
        choseBoulder = false;

        choseLegs = false;
        choseLog = false;
        choseBearTrap = false;
        choseNailGun = false;
        choseSpikes = false;
        choseKatana = false;
        choseSawBlade = false;
        choseFriendlyBullets = false;
        choseKunai = false;
        choseMeteor = false;
        choseBlade = false;
        choseSpikyShield = false;
        choseIncreaseAllChance = false;
        choseDamageIncrease = false;
        #endregion

        ManageSlots.upgradeSlotsTaken = 0;

        spawnedRandomUpgrade = false;
        isInChooseUpgrade = false;

        if(upgradePopUpCoroutine != null)
        {
            StopCoroutine(upgradePopUpCoroutine);
            upgradePopUpCoroutine = null;
        }

        ChooseUpgrade(false);
    }
    #endregion


    public void ThingsAllDo()
    {
        audioManager.Play("UpgradeChoose");
        choseUpgrade = true;
        isInChooseUpgrade = false;
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
      
    }
    #endregion
}
