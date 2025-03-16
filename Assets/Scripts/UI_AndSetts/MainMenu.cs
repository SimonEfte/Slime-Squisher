using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool isTesting;
    public static bool isInMainMenu;

    public StrawberryMechanics strawberryScript;
    public PickUpgrade pickUpgradeScript;
    public ClickMechanics clickMechanicsScript;
    public NonClickUpgrades nonClickUpgradesScript;
    public ActiveMechanics activeScript;
    public ManageSlots manageSlotScript;

    public AudioManager audioManager;

    public TextMeshProUGUI backExtiText, mainMenuOrResetText;

    public GameObject blockFrame;

    public static float mainMenuMusicStartVolume, playMusicStartVolume;

    public SelectGameMode selectGamemodeScript;

    public GameObject mainMenuWishlistBtn;

    private void Awake()
    {
        rewardText.SetActive(false);
        mainMenuMusicStartVolume = 0.6f;
        playMusicStartVolume = 0.67f;

        isTesting = true;
        SetMainMenu();

        backgroundNumber = PlayerPrefs.GetInt("backgroundSave");

        if (!PlayerPrefs.HasKey("backgroundSave"))
        {
            background2.SetActive(true);
            backgroundNumber = 1;
        }
        else
        {
            ChangeBackground(false);
        }

        if (!PlayerPrefs.HasKey("lookedTut"))
        {
            playBtn.interactable = false;
            tutArrow1.SetActive(true); tutArrow2.SetActive(true);
        }
        else
        {
            playBtn.interactable = true;
            tutArrow1.SetActive(false); tutArrow2.SetActive(false);
        }

        //StartCoroutine(ResetPlayerPRefs());
    }

    IEnumerator ResetPlayerPRefs()
    {
        yield return new WaitForSeconds(2);
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isInMainMenu == false)
        {
            if (openSettingsCoroutine == null)
            {
                if(PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
                {
                    openSettingsCoroutine = StartCoroutine(OpenSettings(false, false));
                }
            }
        }

        if(DemoScript.isDemo == false)
        {
            totalCoins_inShopScene.text = MetaProgressionUpgrades.totalCoins.ToString();
        }
    }

    public GameObject waveText, moneyText, slots, strawberry, topLeftActive, mainMenuAllTexts;
    public GameObject mainMenuTexts, gameLogo;
    public GameObject selectGamemodeText, easyOption, normalOption, hardOption;
    public GameObject chall_Bullethel, chall_Flash, chall_fragile, chall_Cascace, challengeInfoText, selectGameModeBackBtn, selectGameModePlayBtn, challengesText, selectGameModeScreen, rewardText;

    public GameObject easy2, normal2, hard2;
    public GameObject bullethell2, flash2, fragile2, rampage2;

    public GameObject shopScreenAll, selectActiveText, selectedActiveTextInfo, selectActiveName, deathToSlimes, sharpClicks, lucky, decoy, projectileFrency, antiSlimeBullets, shopBtn, backBtnShopsCreen, playGameShopsCreen;

    public GameObject allBasicGameModesParent, shopLockedInDemo;

    public TextMeshProUGUI totalCoins_inShopScene;

    public GameObject selectedGamemode, selectedActive;
    public GameObject bottomRightQuit;

    #region Set main menu - Start
    public void SetMainMenu()
    {
        blockFrame.SetActive(false);

        if (isTesting == false)
        {
            isInMainMenu = false;
            mainMenuAllTexts.SetActive(false);
            gameLogo.SetActive(false);
            mainMenuTexts.SetActive(false);

            waveText.SetActive(true);
            if (DemoScript.isDemo == false)
            {
                moneyText.SetActive(true);
            }
            slots.SetActive(true);
            strawberry.SetActive(true);
            topLeftActive.SetActive(true);
        }
        else
        {
            settingsDark.SetActive(false);
            isInMainMenu = true;
            mainMenuAllTexts.SetActive(true);
            gameLogo.SetActive(true);
            mainMenuTexts.SetActive(true);
            gameLogo.GetComponent<Animation>().Play("LogoAnim");

            waveText.SetActive(false);
            moneyText.SetActive(false);
            slots.SetActive(false);
            strawberry.SetActive(false);
            topLeftActive.SetActive(false);

            SetSelectGameModeStuffOff("", false);
            SetShopSCreenStuffInactive("", false);
        }

        if(DemoScript.isDemo == true)
        {
            mainMenuWishlistBtn.SetActive(true);
            totalCoins_inShopScene.gameObject.SetActive(false);
        }
        else
        {
            mainMenuWishlistBtn.SetActive(false);
        }

        StartCoroutine(WaitForText());
    }

    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(1);
        backgroundText.text = $"{LocalizationSCRIPT.background}(" + backgroundNumber + ")";
    }
    #endregion

    #region set select game mode stuff inactive
    public void SetSelectGameModeStuffOff(string doNotSetInactive, bool active)
    {
        if(active == false)
        {
            if (doNotSetInactive == "selectGameModeText") { selectGamemodeText.SetActive(true); }
            else { selectGamemodeText.SetActive(false); }
            easyOption.SetActive(false);
            normalOption.SetActive(false);
            hardOption.SetActive(false);
            chall_Bullethel.SetActive(false);
            chall_Flash.SetActive(false);
            chall_fragile.SetActive(false);
            chall_Cascace.SetActive(false);
            challengeInfoText.SetActive(false);
            selectGameModeBackBtn.SetActive(false);
            selectGameModePlayBtn.SetActive(false);
            challengesText.SetActive(false);
            rewardText.SetActive(false);
        }
        else
        {
            audioManager.Play("MenuAppear");

            easyOption.SetActive(true);
            easyOption.GetComponent<Animation>().Play("UiSpawnIn");

            selectGameModeBackBtn.SetActive(true);
            selectGameModePlayBtn.SetActive(true);
            challengeInfoText.SetActive(true);

            normalOption.SetActive(true);
            normalOption.GetComponent<Animation>().Play("UiSpawnIn");
            hardOption.SetActive(true);
            hardOption.GetComponent<Animation>().Play("UiSpawnIn");
            chall_Bullethel.SetActive(true);
            chall_Bullethel.GetComponent<Animation>().Play("UiSpawnIn");
            chall_Flash.SetActive(true);
            chall_Flash.GetComponent<Animation>().Play("UiSpawnIn");
            chall_fragile.SetActive(true);
            chall_fragile.GetComponent<Animation>().Play("UiSpawnIn");
            chall_Cascace.SetActive(true);
            chall_Cascace.GetComponent<Animation>().Play("UiSpawnIn");

            if(DemoScript.isDemo == false) { rewardText.SetActive(true); }
           
            challengesText.SetActive(true);
            StartCoroutine(GameModeMovement());
        }
    }

    IEnumerator GameModeMovement()
    {
        easy2.transform.localPosition = new Vector2(0,0);
        normal2.transform.localPosition = new Vector2(0, 0);
        hard2.transform.localPosition = new Vector2(0, 0);
        bullethell2.transform.localPosition = new Vector2(0, 0);
        flash2.transform.localPosition = new Vector2(0, 0);
        fragile2.transform.localPosition = new Vector2(0, 0);
        rampage2.transform.localPosition = new Vector2(0, 0);

        yield return new WaitForSeconds(0.167f);
        easy2.GetComponent<Animation>().Play("UiMovement3");
        yield return new WaitForSeconds(0.07f);
        normal2.GetComponent<Animation>().Play("UiMovement3");
        yield return new WaitForSeconds(0.07f);
        hard2.GetComponent<Animation>().Play("UiMovement3");
        yield return new WaitForSeconds(0.07f);
        bullethell2.GetComponent<Animation>().Play("UiMovement3");
        yield return new WaitForSeconds(0.07f);
        flash2.GetComponent<Animation>().Play("UiMovement3");
        yield return new WaitForSeconds(0.07f);
        fragile2.GetComponent<Animation>().Play("UiMovement3");
        yield return new WaitForSeconds(0.07f);
        rampage2.GetComponent<Animation>().Play("UiMovement3");
    }
    #endregion

    #region set shop mode stuff inactive
    public void SetShopSCreenStuffInactive(string doNotSetInactive, bool active)
    {
        if(active == false)
        {
            shopScreenAll.SetActive(false);
            deathToSlimes.SetActive(false);
            sharpClicks.SetActive(false);
            lucky.SetActive(false);
            decoy.SetActive(false);
            projectileFrency.SetActive(false);
            antiSlimeBullets.SetActive(false);
            shopBtn.SetActive(false);
            backBtnShopsCreen.SetActive(false);
            playGameShopsCreen.SetActive(false);
            selectedActiveTextInfo.SetActive(false);
            selectActiveName.SetActive(false);
        }
        else
        {
            if(DemoScript.isDemo == true) { shopLockedInDemo.SetActive(true); }
            else { shopLockedInDemo.SetActive(false); }

            selectActiveText.SetActive(true);

            if(DemoScript.isDemo == true) { totalCoins_inShopScene.gameObject.SetActive(false); }
            else { totalCoins_inShopScene.gameObject.SetActive(true); }

            audioManager.Play("MenuAppear");

            deathToSlimes.SetActive(true); deathToSlimes.transform.localPosition = new Vector2(-675, 209);
            deathToSlimes.GetComponent<Animation>().Play("UiSpawnIn");
            sharpClicks.SetActive(true); sharpClicks.transform.localPosition = new Vector2(-405, 209);
            sharpClicks.GetComponent<Animation>().Play("UiSpawnIn");
            lucky.SetActive(true); lucky.transform.localPosition = new Vector2(-135, 209);
            lucky.GetComponent<Animation>().Play("UiSpawnIn");
            decoy.SetActive(true); decoy.transform.localPosition = new Vector2(135, 209);
            decoy.GetComponent<Animation>().Play("UiSpawnIn");
            projectileFrency.SetActive(true); projectileFrency.transform.localPosition = new Vector2(405, 209);
            projectileFrency.GetComponent<Animation>().Play("UiSpawnIn");
            antiSlimeBullets.SetActive(true); antiSlimeBullets.transform.localPosition = new Vector2(675, 209);
            antiSlimeBullets.GetComponent<Animation>().Play("UiSpawnIn");
            StartCoroutine(ActiveMovement());

            shopBtn.SetActive(true);
            backBtnShopsCreen.SetActive(true);
            playGameShopsCreen.SetActive(true);
            selectedActiveTextInfo.SetActive(true);
            selectActiveName.SetActive(true);
        }
    }

    IEnumerator ActiveMovement()
    {
        yield return new WaitForSeconds(0.16f);
        deathToSlimes.GetComponent<Animation>().Play("UiMovement");
        yield return new WaitForSeconds(0.07f);
        sharpClicks.GetComponent<Animation>().Play("UiMovement");
        yield return new WaitForSeconds(0.07f);
        lucky.GetComponent<Animation>().Play("UiMovement");
        yield return new WaitForSeconds(0.07f);
        decoy.GetComponent<Animation>().Play("UiMovement");
        yield return new WaitForSeconds(0.07f);
        projectileFrency.GetComponent<Animation>().Play("UiMovement");
        yield return new WaitForSeconds(0.07f);
        antiSlimeBullets.GetComponent<Animation>().Play("UiMovement");
    }
    #endregion

    //-- switching between main menus
    #region play game main menu
    public void PlayGameMainMenu()
    {
        if(DemoScript.isDemo == true)
        {
            mainMenuWishlistBtn.SetActive(false);
        }

        blockFrame.SetActive(true);
        UiClickSound();
        Vector2 starPosTexts = new Vector2(0, -36);
        Vector2 endPosTexts = new Vector2(0, -750);

        Vector2 starPosLogo = new Vector2(0, 228);
        Vector2 endPosLogo = new Vector2(0, 710);

        StartCoroutine(FadeInOrOut(false, mainMenuTexts, starPosTexts, endPosTexts));
        StartCoroutine(FadeInOrOut(false, gameLogo, starPosLogo, endPosLogo));

        StartCoroutine(SetObjectActiveOrInactive(false, mainMenuTexts, 0.33f));
        //StartCoroutine(SetObjectActiveOrInactive(false, gameLogo, 0.33f));

        StartCoroutine(FadeInSelectGameMode());
    }

    bool playedTextAnim;

    IEnumerator FadeInSelectGameMode()
    {
        selectGameModeScreen.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        selectGamemodeText.SetActive(true);

        Vector2 startPos = new Vector2(0, 616);
        Vector2 endPos = new Vector2(0, 404);

        StartCoroutine(FadeInOrOut(false, selectGamemodeText, startPos, endPos));

        yield return new WaitForSeconds(0.33f);
        if(playedTextAnim == false)
        {
            selectGamemodeText.GetComponent<Animation>().Play("MovementAnim");
            playedTextAnim = true;
        }

        SelectGameMode.justSetStuff = true;
        if (DemoScript.isDemo == true) { selectGamemodeScript.SelectTheGamemode(1); }
        else
        {
            if (SelectGameMode.choseEasy == true) { selectGamemodeScript.SelectTheGamemode(1); }
            if (SelectGameMode.choseNormal == true) { selectGamemodeScript.SelectTheGamemode(2); }
            if (SelectGameMode.choseHard == true) { selectGamemodeScript.SelectTheGamemode(3); }
            if (SelectGameMode.choseBullethell == true) { selectGamemodeScript.SelectTheGamemode(4); }
            if (SelectGameMode.choseFlash == true) { selectGamemodeScript.SelectTheGamemode(5); }
            if (SelectGameMode.choseFragile == true) { selectGamemodeScript.SelectTheGamemode(6); }
            if (SelectGameMode.choseNarrow == true) { selectGamemodeScript.SelectTheGamemode(7); }
            if (SelectGameMode.choseRampage == true) { selectGamemodeScript.SelectTheGamemode(8); }
        }

        SetSelectGameModeStuffOff("", true);
        selectedGamemode.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        blockFrame.SetActive(false);
    }
    #endregion

    #region next and back button in select gamemode
    bool selectFirst;

    public void PlayGame_AfterGameModeSelected(bool pressedPlay)
    {
        if(pressedPlay == false)
        {
            if (DemoScript.isDemo == true)
            {
                mainMenuWishlistBtn.SetActive(true);
            }

            blockFrame.SetActive(true);
            UiClickSound();
            Vector2 startPos = new Vector2(0, 404);
            Vector2 endPos = new Vector2(0, 616);

            StartCoroutine(FadeInOrOut(false, selectGamemodeText, startPos, endPos));

            StartCoroutine(SetObjectActiveOrInactive(true, mainMenuTexts, 0.33f));
            //StartCoroutine(SetObjectActiveOrInactive(true, gameLogo, 0.33f));
            mainMenuTexts.GetComponent<Animation>().Play("MainMenuIn");

            Vector2 starPosLogo = new Vector2(0, 228);
            Vector2 endPosLogo = new Vector2(0, 710);

            StartCoroutine(FadeInOrOut(false, gameLogo, endPosLogo, starPosLogo));

            StartCoroutine(SetObjectActiveOrInactive(false, selectGamemodeText, 0.33f));
            selectedGamemode.SetActive(false);
            StartCoroutine(SetBlockOff());
            StartCoroutine(SetOBjecsOff());
        }
        else
        {
            ActiveMechanics.justChangeStuff = true;
            if (DemoScript.isDemo == true) { activeScript.SelectActive(1); }
            else
            {
                if(ActiveMechanics.choseDeathToSlimes == true) { activeScript.SelectActive(1); }
                if (ActiveMechanics.chosePunchyClicks == true) { activeScript.SelectActive(2); }
                if (ActiveMechanics.choseClover == true) { activeScript.SelectActive(3); }
                if (ActiveMechanics.choseDecoy == true) { activeScript.SelectActive(4); }
                if (ActiveMechanics.choseProjectileFrenzy == true) { activeScript.SelectActive(5); }
                if (ActiveMechanics.choseAntiSlime == true) { activeScript.SelectActive(6); }
            }

            blockFrame.SetActive(true);
            UiClickSound();
            shopScreenAll.SetActive(true);
            selectActiveText.SetActive(true);

            if(selectFirst == false)
            {
                selectActiveText.GetComponent<Animation>().Play("MovementAnim");
                selectFirst = true;
            }

            Vector2 startPos1 = new Vector2(0, 602);
            Vector2 endPos1 = new Vector2(0, 410);

            StartCoroutine(FadeInOrOut(false, selectActiveText, startPos1, endPos1));

            Vector2 startPos = new Vector2(0, 404);
            Vector2 endPos = new Vector2(0, 616);

            StartCoroutine(FadeInOrOut(false, selectGamemodeText, startPos, endPos));
           
            StartCoroutine(FadeInShopScreen());
        }
    }

    IEnumerator FadeInShopScreen()
    {
        yield return new WaitForSeconds(0.33f);
        SetSelectGameModeStuffOff("selectGameModeText", false);
        selectedGamemode.SetActive(false);
        selectedActive.SetActive(true);
        blockFrame.SetActive(false);
        SetShopSCreenStuffInactive("", true);
    }

    IEnumerator SetOBjecsOff()
    {
        yield return new WaitForSeconds(0.33f);
    }
    #endregion

    #region back and play game inside shop screen
    public Texture2D clickCursor, clickCursorRed;
    public Animation strawberryHealthMovement;

    public void PlayGame_OnShopScreen(bool pressedPlay)
    {
        if (pressedPlay == false)
        {
            UiClickSound();
            Vector2 startPos1 = new Vector2(0, 410);
            Vector2 endPos1 = new Vector2(0, 590);

            StartCoroutine(FadeInOrOut(false, selectActiveText, startPos1, endPos1));

            Vector2 startPos = new Vector2(0, 404);
            Vector2 endPos = new Vector2(0, 616);

            StartCoroutine(FadeInOrOut(false, selectGamemodeText, endPos, startPos));

            blockFrame.SetActive(true);
            StartCoroutine(SetShopStuffOff(false));
        }
        else
        {
            PickUpgrade.clickCooldown = 0.75f - MetaProgressionUpgrades.clickCooldownDecrease;
            if(PickUpgrade.clickCooldown < 0.5f) { PickUpgrade.clickCooldownDecrease = 0.075f; }
            else { PickUpgrade.clickCooldownDecrease = 0.091f; }
            

            manageSlotScript.SetSlotsPressPlay();

            StrawberryMechanics.strawberryHealth = StrawberryMechanics.startHealth;

            strawberry.SetActive(true);
            strawberryHealthMovement.gameObject.SetActive(true);
            strawberryHealthMovement.Play();
            StrawberryMechanics.startHealth = 2;
            StrawberryMechanics.strawberryHealth = StrawberryMechanics.startHealth + MetaProgressionUpgrades.startHealth;

            if(SelectGameMode.choseFragile == true)
            {
                StrawberryMechanics.startHealth = 0;
                StrawberryMechanics.strawberryHealth = 0;
            }

            strawberryScript.SetHealth();

            UiClickSound();

            Vector2 startPos1 = new Vector2(0, 410);
            Vector2 endPos1 = new Vector2(0, 590);

            blockFrame.SetActive(true);
            StartCoroutine(FadeInOrOut(false, selectActiveText, startPos1, endPos1));

            StartCoroutine(SetShopStuffOff(true));
            StartCoroutine(SetGameUI());

            pickUpgradeScript.SondVoumeSet("MainMenuMusic", true, mainMenuMusicStartVolume, 0f, 1.4f, true);
            pickUpgradeScript.SondVoumeSet("MusicRun", false, playMusicStartVolume, 0, 1.4f, false);
            audioManager.Play("MusicRun");

            activeScript.ActiveCooldown(true);
        }
    }

    IEnumerator SetShopStuffOff(bool onlyShop)
    {
        yield return new WaitForSeconds(0.33f);
        //selectGamemodeText.GetComponent<Animation>().Play("MovementAnim");

        if (onlyShop == true) 
        {
            SetShopSCreenStuffInactive("", false); 
            selectedActive.SetActive(false);
            selectedGamemode.SetActive(false);
        }
        else
        {
            selectedGamemode.SetActive(true);
            selectedActive.SetActive(false);
            SetShopSCreenStuffInactive("", false);
            SetSelectGameModeStuffOff("", true);
        }

        StrawberryMechanics.isInDeathFrame = false;
        PickUpgrade.isInWonRunScene = false;

        blockFrame.SetActive(false);
        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
    }

    public SpawnSlimes spawnSlimesScript;
    public GameObject waveAndGoldText;
    
    IEnumerator SetGameUI()
    {
        yield return new WaitForSeconds(0.33f);

        waveAndGoldText.SetActive(true);
        waveAndGoldText.GetComponent<Animation>().Play("WaveAndCoinIn");

        waveText.SetActive(true);
        if(DemoScript.isDemo == false)
        {
            moneyText.SetActive(true);
        }

        topLeftActive.SetActive(true);
        topLeftActive.GetComponent<Animation>().Play("TopLeftIn");

        slots.SetActive(true);
        slots.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(0.33f);

        if(DemoScript.isDemo == true) { spawnSlimesScript.StartEasyGameModeWave(); }
        else
        {
            if (SelectGameMode.choseEasy == true) { spawnSlimesScript.StartEasyGameModeWave(); }
            if (SelectGameMode.choseNormal == true) { spawnSlimesScript.StartNormalGameModeWave(); }
            if (SelectGameMode.choseHard == true) { spawnSlimesScript.StartHardGameModeWave(); }

            if (SelectGameMode.choseBullethell == true) { spawnSlimesScript.StartBullethellGameModeWave(); }
            if (SelectGameMode.choseFlash == true) { spawnSlimesScript.StartFlashGameModeWave(); }
            if (SelectGameMode.choseFragile == true) { spawnSlimesScript.StartFragileGameModeWave(); }
            if (SelectGameMode.choseRampage == true) { spawnSlimesScript.StartRampageGameModeWave(); }
        }

        StartCoroutine(UIMovement());

        yield return new WaitForSeconds(0.1f);
        isInMainMenu = false;
    }

    IEnumerator UIMovement()
    {
        yield return new WaitForSeconds(Random.Range(0.35f, 0.5f));
        topLeftActive.GetComponent<Animation>().Play("TopLeftMovement");
        waveAndGoldText.GetComponent<Animation>().Play("TopLeftMovement");
    }
    #endregion
    //--


    #region fade in or out
    IEnumerator FadeInOrOut(bool fadeIn, GameObject objectToMove, Vector2 startPos, Vector2 endPos)
    {
        float duration = 0.33f; 
        float elapsedTime = 0f;

        Vector2 initialPos = fadeIn ? endPos : startPos;
        Vector2 targetPos = fadeIn ? startPos : endPos;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            objectToMove.transform.localPosition = Vector2.Lerp(initialPos, targetPos, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        objectToMove.transform.localPosition = targetPos;
    }
    #endregion


    #region Set object inactive or active
    IEnumerator SetObjectActiveOrInactive(bool setObjectActive, GameObject objectToSet, float timer)
    {
        if (setObjectActive == true)
        {
            objectToSet.SetActive(true);
        }
        else
        {
            if (objectToSet.name == "selectGameMode")
            {
                SetSelectGameModeStuffOff("selectGameModeText", false);
            }
        }

        yield return new WaitForSeconds(timer);

       

        if (setObjectActive == false)
        {
            if (objectToSet.name == "selectGameMode")
            {
            }
            else
            {
                objectToSet.SetActive(false);
            }
        }
        else
        {
            if (objectToSet.name == "LogoText") 
            { 
                //gameLogo.GetComponent<Animation>().Play("LogoAnim");
            }
        }
    }
    #endregion

    #region open shop
    public GameObject antiSlimeShop, shopDark;
    public bool isShopOpen;

    public void OpenShop()
    {
        if(DemoScript.isDemo == true) { audioManager.Play("Error"); return; }

        UiClickSound();

        if (isShopOpen == false)
        {
            isShopOpen = true;
            shopDark.SetActive(true);
            shopDark.GetComponent<Animation>().Play("shopDarkIn");

            antiSlimeShop.SetActive(true);
            antiSlimeShop.GetComponent<Animation>().Play("shopIn");
        }
        else
        {
            isShopOpen = false;
            antiSlimeShop.GetComponent<Animation>().Play("shopOut");
            shopDark.GetComponent<Animation>().Play("shopDarkOut");
            StartCoroutine(SetShopInactive());
        }
    }
     
    IEnumerator SetShopInactive()
    {
        yield return new WaitForSeconds(0.33f);
        antiSlimeShop.SetActive(false);
        shopDark.SetActive(false);
    }
    #endregion

    #region settings 
    public TrailRenderer slashRenderer;
    public static bool isPaused;

    public void SettingsMainMenu(bool playAnim)
    {
        if(openSettingsCoroutine == null)
        {
            blockFrame.SetActive(true);
            openSettingsCoroutine = StartCoroutine(OpenSettings(playAnim, true));
        }
    }

    public void SettingsClickBack()
    {
        if (isInMainMenu == true)
        {
            if (openSettingsCoroutine == null)
            {
                blockFrame.SetActive(true);
                openSettingsCoroutine = StartCoroutine(OpenSettings(true, false));
            }
        }
    }

    public void CloseSettings_InGame()
    {
        if(isInMainMenu == false)
        {
          
            openSettingsCoroutine = StartCoroutine(OpenSettings(false, false));
        }
    }

    public Coroutine openSettingsCoroutine;
    public Animation settings;
    public GameObject settingsDark;

    public GameObject resetRunText, mainMenuText, socials, music, sounds, resolution, fullscreen, language, background;


    IEnumerator OpenSettings(bool playAnim, bool fadeIn)
    {
        if(DemoScript.isLocalizationDone == false)
        {
            language.SetActive(false);
        }

        if(playAnim == true)
        {
            bottomRightQuit.SetActive(false);
            if (fadeIn == true)
            {
                UiClickSound();
                socials.transform.localPosition = new Vector2(0, 313);
                music.transform.localPosition = new Vector2(0, 210);
                sounds.transform.localPosition = new Vector2(0, 100);

                resolution.transform.localPosition = new Vector2(0, -27);
                fullscreen.transform.localPosition = new Vector2(0, -170);
                language.transform.localPosition = new Vector2(0, -265);
                background.transform.localPosition = new Vector2(0, -360);
                backExtiText.transform.localPosition = new Vector2(0, -455);

                if (DemoScript.isLocalizationDone == false)
                {
                    socials.transform.localPosition = new Vector2(0, 263);
                    music.transform.localPosition = new Vector2(0, 160);
                    sounds.transform.localPosition = new Vector2(0, 50);

                    resolution.transform.localPosition = new Vector2(0, -77);
                    fullscreen.transform.localPosition = new Vector2(0, -220);
                    background.transform.localPosition = new Vector2(0, -318);
                    backExtiText.transform.localPosition = new Vector2(0, -415);
                }

                resetRunText.SetActive(false); mainMenuText.SetActive(false);

                backExtiText.text = $"{LocalizationSCRIPT.back}";
                mainMenuTexts.GetComponent<Animation>().Play("MainMenuOut");

                Vector2 starPosLogo = new Vector2(0, 228);
                Vector2 endPosLogo = new Vector2(0, 810);

                StartCoroutine(FadeInOrOut(false, gameLogo, starPosLogo, endPosLogo));

                StartCoroutine(SetObjectActiveOrInactive(false, mainMenuTexts, 0.33f));
                //StartCoroutine(SetObjectActiveOrInactive(false, gameLogo, 0.33f));

                yield return new WaitForSeconds(0.4f);
                settings.Play("settingsIn");
                settings.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.217f);
                blockFrame.SetActive(false);
            }
            else
            {
                UiClickSound();
                settings.Play("settingsOut");
                yield return new WaitForSeconds(0.4f);
                settings.gameObject.SetActive(false);
                mainMenuAllTexts.SetActive(true);
                mainMenuTexts.SetActive(true);
                mainMenuTexts.GetComponent<Animation>().Play("MainMenuIn");

                Vector2 starPosLogo = new Vector2(0, 228);
                Vector2 endPosLogo = new Vector2(0, 710);

                StartCoroutine(FadeInOrOut(false, gameLogo, endPosLogo, starPosLogo));
                yield return new WaitForSeconds(0.217f);
                blockFrame.SetActive(false);
            }
        }
        else
        {
            bottomRightQuit.SetActive(true);
            settings.gameObject.transform.localPosition = new Vector2(0,44);

            if (settings.gameObject.activeInHierarchy == true) 
            {
                isPaused = false;
                resetYesOrNo.SetActive(false);

                if (ClickMechanics.isClickCooldown == false)
                {
                    Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(clickCursorRed, Vector2.zero, CursorMode.Auto);
                }

                settings.gameObject.SetActive(false); settingsDark.SetActive(false); Time.timeScale = 1;

                if (PickUpgrade.choseCursorSlash)
                {
                    slashRenderer.enabled = true;
                }
            }
            else
            {
                isPaused = true;
                if (PickUpgrade.choseCursorSlash)
                {
                    slashRenderer.enabled = false;
                }

                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                settings.gameObject.SetActive(true); settingsDark.SetActive(true); Time.timeScale = 0; 
            }

            backExtiText.text = $"{LocalizationSCRIPT.close}";

            socials.transform.localPosition = new Vector2(0, 420);
            music.transform.localPosition = new Vector2(0, 321);
            sounds.transform.localPosition = new Vector2(0, 211);
            resolution.transform.localPosition = new Vector2(0, 94);
            fullscreen.transform.localPosition = new Vector2(0, -35);
            language.transform.localPosition = new Vector2(0, -136);
            background.transform.localPosition = new Vector2(0, -235);
            resetRunText.transform.localPosition = new Vector2(0, -329);
            mainMenuText.transform.localPosition = new Vector2(0, -423);
            backExtiText.transform.localPosition = new Vector2(0, -518);
            resetRunText.SetActive(true); mainMenuText.SetActive(true);

            if (DemoScript.isLocalizationDone == false)
            {
                socials.transform.localPosition = new Vector2(0, 369);
                music.transform.localPosition = new Vector2(0, 270);
                sounds.transform.localPosition = new Vector2(0, 160);
                resolution.transform.localPosition = new Vector2(0, 43);
                fullscreen.transform.localPosition = new Vector2(0, -86);
                language.transform.localPosition = new Vector2(0, -136);
                background.transform.localPosition = new Vector2(0, -181);
                resetRunText.transform.localPosition = new Vector2(0, -275);
                mainMenuText.transform.localPosition = new Vector2(0, -369);
                backExtiText.transform.localPosition = new Vector2(0, -464);
            }
        }

        openSettingsCoroutine = null;
    }
    #endregion

    #region Credits
    public void CreditsMainMenu()
    {

    }
    #endregion

    #region quit game
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region tutotial
    public GameObject tutorialFrame;
    public GameObject greenSlimeTut, shootingSlimeTut;
    public bool openTut;
    public static bool isInTut;

    public void OpenTurotial()
    {
        UiClickSound();
        if (openTut == false)
        {
            LookedAtTurotial();
            isInTut = true;
            openTut = true;
            blockFrame.SetActive(true);
            greenSlimeTut.SetActive(true);
            shootingSlimeTut.SetActive(true);

            tutorialFrame.SetActive(true);

            tutorialFrame.GetComponent<Animation>().Play("MainMenuIn");
            mainMenuTexts.GetComponent<Animation>().Play("MainMenuOut");

            Vector2 starPosLogo = new Vector2(0, 228);
            Vector2 endPosLogo = new Vector2(0, 810);

            StartCoroutine(FadeInOrOut(false, gameLogo, starPosLogo, endPosLogo));

            StartCoroutine(BlockOff());
        }
        else
        {
            GameObject[] EnemyBullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach (GameObject enemyBullet in EnemyBullet)
            {
                if (enemyBullet.activeSelf)
                {
                    ObjectPool.instance.ReturnEnemyBulletFromPool(enemyBullet);
                }
            }

            isInTut = false;
            openTut = false;
            blockFrame.SetActive(true);

            tutorialFrame.GetComponent<Animation>().Play("MainMenuOut");
            mainMenuTexts.GetComponent<Animation>().Play("MainMenuIn");

            Vector2 starPosLogo = new Vector2(0, 810);
            Vector2 endPosLogo = new Vector2(0, 228);

            StartCoroutine(FadeInOrOut(false, gameLogo, starPosLogo, endPosLogo));

            StartCoroutine(SetFalse());
            StartCoroutine(BlockOff());
        }
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(0.35f);
        greenSlimeTut.SetActive(false);
        shootingSlimeTut.SetActive(false);

        tutorialFrame.SetActive(false);
    }
    IEnumerator BlockOff()
    {
        yield return new WaitForSeconds(0.35f);
        blockFrame.SetActive(false);
    }
    #endregion


    #region Reset current run or go to main menu
    public static bool clickedResetRun;

    public void ResetCurrentRun()
    {
        clickedResetRun = true;
        resetYesOrNo.SetActive(true);
        mainMenuOrResetText.text = $"{LocalizationSCRIPT.reserCurrentRun}";
    }

    public GameObject resetYesOrNo;

    public void GoToMainMenu()
    {
        clickedResetRun = false;
        resetYesOrNo.SetActive(true);
        mainMenuOrResetText.text = $"{LocalizationSCRIPT.backToMainMenu}";
    }

    public void ActuallyResetOrGoToMainMenu()
    {
        UiClickSound();
        SpawnSlimes.waveTime = 0;

        SpawnSlimes.isRampageDone = false;
        SpawnSlimes.isInGame = false;

        CheckObjectsOnScreen();
        spawnSlimesScript.ResetCurrentRun();
        activeScript.ResetActiveAbility();
        manageSlotScript.ResetSlots();

        if (clickedResetRun == true) 
        {
            strawberryScript.ResetStrawberry(false);

            if(SelectGameMode.choseEasy == true || DemoScript.isDemo == true)
            {
                spawnSlimesScript.StartEasyGameModeWave();
            }
            else if (SelectGameMode.choseNormal == true) { spawnSlimesScript.StartNormalGameModeWave(); }
            else if (SelectGameMode.choseHard == true) { spawnSlimesScript.StartHardGameModeWave(); }
            else if (SelectGameMode.choseBullethell == true) { spawnSlimesScript.StartBullethellGameModeWave(); }
            else if (SelectGameMode.choseFlash == true) { spawnSlimesScript.StartFlashGameModeWave(); }
            else if (SelectGameMode.choseFragile == true) { spawnSlimesScript.StartFragileGameModeWave(); }
            else if (SelectGameMode.choseRampage == true) { spawnSlimesScript.StartRampageGameModeWave(); }

            if (clickedResetRun == true)
            {
                SpawnSlimes.isInGame = true;
            }
        }
        else
        {
            if (DemoScript.isDemo == true)
            {
                mainMenuWishlistBtn.SetActive(true);
            }

            SpawnSlimes.isPlayingRun = false;
            strawberryHealthMovement.gameObject.SetActive(false);
            strawberryScript.ResetStrawberry(true);

            //go to main menu
            strawberry.SetActive(false);
            topLeftActive.SetActive(false);
            slots.SetActive(false);
            waveAndGoldText.gameObject.SetActive(false);
            waveText.gameObject.SetActive(false);

            Vector2 starPosTexts = new Vector2(0, -36);
            Vector2 endPosTexts = new Vector2(0, -750);

            Vector2 starPosLogo = new Vector2(0, 228);
            Vector2 endPosLogo = new Vector2(0, 710);

            mainMenuTexts.SetActive(true);
            StartCoroutine(FadeInOrOut(true, mainMenuTexts, starPosTexts, endPosTexts));
            StartCoroutine(FadeInOrOut(true, gameLogo, starPosLogo, endPosLogo));
            isInMainMenu = true;

            pickUpgradeScript.SondVoumeSet("MainMenuMusic", false, mainMenuMusicStartVolume, 0f, 0.5f, false);
            pickUpgradeScript.SondVoumeSet("MusicRun", true, playMusicStartVolume, 0, 0.5f, true);
            audioManager.Play("MainMenuMusic");
        }

        pickUpgradeScript.ResetUpgrades();
        clickMechanicsScript.ResetClick();
        nonClickUpgradesScript.Reset();

        if(clickedResetRun == false)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if(PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
        {
            openSettingsCoroutine = StartCoroutine(OpenSettings(false, false));
        }

        if(clickedResetRun == true)
        {
            StrawberryMechanics.isInDeathFrame = false;
            PickUpgrade.isInWonRunScene = false;
        }

        PickUpgrade.coinsThisRound = 0;

        resetYesOrNo.SetActive(false);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.55f);
        blockFrame.SetActive(false);
    }

    public void DoNotResetOrMainMenu()
    {
        UiClickSound();
        resetYesOrNo.SetActive(false);
    }
    #endregion


    #region Remove and set objects to object pool
    public void CheckObjectsOnScreen()
    {
        GameObject[] Slimes = GameObject.FindGameObjectsWithTag("Slime");
        foreach (GameObject slime in Slimes)
        {
            if (slime.activeSelf)
            {
                if(slime.name.Contains("Green Basic")) { ObjectPool.instance.ReturnSlime1FromPool(slime); }
                else if (slime.name.Contains("RegularBlue")) { ObjectPool.instance.ReturnRegularBlueToPool(slime); }
                else if (slime.name.Contains("RegularYellow")) { ObjectPool.instance.ReturnRegularYellowToPool(slime); }
                else if (slime.name.Contains("RegularRed")) { ObjectPool.instance.ReturnRegularRedToPool(slime); }
                else if (slime.name.Contains("RegularPurple")) { ObjectPool.instance.ReturnRegularPurpleToPool(slime); }

                else if (slime.name.Contains("FastGreen")) { ObjectPool.instance.ReturnFastGreenToPool(slime); }
                else if (slime.name.Contains("FastBlue")) { ObjectPool.instance.ReturnFastBlueToPool(slime); }
                else if (slime.name.Contains("FastYellow")) { ObjectPool.instance.ReturnFastYellowToPool(slime); }
                else if (slime.name.Contains("FastRed")) { ObjectPool.instance.ReturnFastRedToPool(slime); }
                else if (slime.name.Contains("FastPurple")) { ObjectPool.instance.ReturnFastPurpleToPool(slime); }

                else if (slime.name.Contains("ShootingGreen")) { ObjectPool.instance.ReturnShootingGreenToPool(slime); }
                else if (slime.name.Contains("Blue Shooting")) { ObjectPool.instance.ReturnBlueShootingFromPool(slime); }
                else if (slime.name.Contains("ShootingYellow")) { ObjectPool.instance.ReturnShootingYellowToPool(slime); }
                else if (slime.name.Contains("ShootingRed")) { ObjectPool.instance.ReturnShootingRedToPool(slime); }
                else if (slime.name.Contains("ShootingPurple")) { ObjectPool.instance.ReturnShootingPurpleToPool(slime); }

                else if (slime.name.Contains("BigGreen")) { ObjectPool.instance.ReturnBigGreenToPool(slime); }
                else if (slime.name.Contains("BigBlue")) { ObjectPool.instance.ReturnBigBlueToPool(slime); }
                else if (slime.name.Contains("BigYellow")) { ObjectPool.instance.ReturnRedBigToPool(slime); }
                else if (slime.name.Contains("BigRed")) { ObjectPool.instance.ReturnBigYellowToPool(slime); }
                else if (slime.name.Contains("BigPurple")) { ObjectPool.instance.ReturnBigPurpleToPool(slime); }
            }
        }

        GameObject[] Goo = GameObject.FindGameObjectsWithTag("Goo");
        foreach (GameObject goo in Goo)
        {
            if (goo.activeSelf)
            {
                ObjectPool.instance.ReturnGooFromPool(goo);
            }
        }

        GameObject[] BlueGoo = GameObject.FindGameObjectsWithTag("BlueGoo");
        foreach (GameObject goo in BlueGoo)
        {
            if (goo.activeSelf)
            {
                ObjectPool.instance.ReturnBlueGooToPool(goo);
            }
        }

        GameObject[] Orangegoo = GameObject.FindGameObjectsWithTag("OrangeGoo");
        foreach (GameObject goo in Orangegoo)
        {
            if (goo.activeSelf)
            {
                ObjectPool.instance.ReturnOrangeGooToPool(goo);
            }
        }

        GameObject[] RedGoo = GameObject.FindGameObjectsWithTag("RedGoo");
        foreach (GameObject goo in RedGoo)
        {
            if (goo.activeSelf)
            {
                ObjectPool.instance.ReturnRedGooToPool(goo);
            }
        }

        GameObject[] PurpleGoo = GameObject.FindGameObjectsWithTag("PurpleGoo");
        foreach (GameObject goo in PurpleGoo)
        {
            if (goo.activeSelf)
            {
                ObjectPool.instance.ReturnPurpleGooToPool(goo);
            }
        }

        if (PickUpgrade.choseArrowRain == true)
        {
            GameObject[] Arrow = GameObject.FindGameObjectsWithTag("Arrow");
            foreach (GameObject arrow in Arrow)
            {
                if (arrow.activeSelf)
                {
                    ObjectPool.instance.ReturnArrowFrompool(arrow);
                }
            }
        }

        if (PickUpgrade.choseBouncyBall == true)
        {
            GameObject[] BouncyBall = GameObject.FindGameObjectsWithTag("BouncyBall");
            foreach (GameObject bouncyBall in BouncyBall)
            {
                if (bouncyBall.activeSelf)
                {
                    ObjectPool.instance.ReturnBouncyBallFromPool(bouncyBall);
                }
            }
        }

        GameObject[] DamageText = GameObject.FindGameObjectsWithTag("DamageText");
        foreach (GameObject damageText in DamageText)
        {
            if (damageText.activeSelf)
            {
                TextMeshProUGUI tmpComponent = damageText.GetComponent<TextMeshProUGUI>();
                ObjectPool.instance.ReturnDamageTextFromPool(tmpComponent);
            }
        }

      

        if(PickUpgrade.choseLaserGun == true)
        {
            GameObject[] Laser = GameObject.FindGameObjectsWithTag("LaserGun");
            foreach (GameObject laser in Laser)
            {
                if (laser.activeSelf)
                {
                    ObjectPool.instance.ReturnLaserFromPool(laser);
                }
            }
        }

        if (PickUpgrade.choseMeteor == true)
        {
            GameObject[] Meteor = GameObject.FindGameObjectsWithTag("Meteor");
            foreach (GameObject meteor in Meteor)
            {
                if (meteor.activeSelf)
                {
                    ObjectPool.instance.ReturnMeteorToPool(meteor);
                }
            }
        }

        if (PickUpgrade.chosePaperShot == true)
        {
            GameObject[] Paper = GameObject.FindGameObjectsWithTag("PaperClip");
            foreach (GameObject paper in Paper)
            {
                if (paper.activeSelf)
                {
                    ObjectPool.instance.ReturnPaperClipFromPool(paper);
                }
            }
        }

        if (PickUpgrade.chosePoisonDart == true)
        {
            GameObject[] PosionDart = GameObject.FindGameObjectsWithTag("PoisonDart");
            foreach (GameObject posionDart in PosionDart)
            {
                if (posionDart.activeSelf)
                {
                    ObjectPool.instance.ReturnPoisonDartFromPool(posionDart);
                }
            }
        }

        GameObject[] Coin = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in Coin)
        {
            if (coin.activeSelf)
            {
                ObjectPool.instance.ReturnCoinFromPool(coin);
            }
        }

        GameObject[] Flash = GameObject.FindGameObjectsWithTag("Flash");
        foreach (GameObject flash in Flash)
        {
            if (flash.activeSelf)
            {
                ObjectPool.instance.ReturnShootFlashToPool(flash);
            }
        }

        GameObject[] Skull = GameObject.FindGameObjectsWithTag("Skull");
        foreach (GameObject skull in Skull)
        {
            if (skull.activeSelf)
            {
                ObjectPool.instance.ReturnSkullToPool(skull);
            }
        }

        if (PickUpgrade.choseStapler == true)
        {
            GameObject[] Staple = GameObject.FindGameObjectsWithTag("Staple");
            foreach (GameObject staple in Staple)
            {
                if (staple.activeSelf)
                {
                    ObjectPool.instance.ReturnStapleToPool(staple);
                }
            }
        }

        if (PickUpgrade.choseSword == true)
        {
            GameObject[] SwordParent = GameObject.FindGameObjectsWithTag("SwordParent");
            foreach (GameObject swordParent in SwordParent)
            {
                if (swordParent.activeSelf)
                {
                    ObjectPool.instance.ReturnSwordToPool(swordParent);
                }
            }
        }

        if (PickUpgrade.choseThorn == true)
        {
            GameObject[] Thorn = GameObject.FindGameObjectsWithTag("Thorn");
            foreach (GameObject thorn in Thorn)
            {
                if (thorn.activeSelf)
                {
                    ObjectPool.instance.ReturnThornFromPool(thorn);
                }
            }
        }

        if (PickUpgrade.choseNailGun == true)
        {
            GameObject[] Nail = GameObject.FindGameObjectsWithTag("Nail");
            foreach (GameObject nail in Nail)
            {
                if (nail.activeSelf)
                {
                    ObjectPool.instance.ReturnNailFromPool(nail);
                }
            }
        }

        if (PickUpgrade.choseKunai == true)
        {
            GameObject[] Kunai = GameObject.FindGameObjectsWithTag("Kunai");
            foreach (GameObject kunai in Kunai)
            {
                if (kunai.activeSelf)
                {
                    ObjectPool.instance.ReturnKunaiToPool(kunai);
                }
            }
        }

        if (PickUpgrade.choseBearTrap == true)
        {
            GameObject[] BearTrap = GameObject.FindGameObjectsWithTag("THEBearTrap");
            foreach (GameObject bearTrap in BearTrap)
            {
                if (bearTrap.activeSelf)
                {
                    ObjectPool.instance.ReturnBearTrapToPool(bearTrap);
                }
            }
        }

        if (ActiveMechanics.choseAntiSlime == true)
        {
            GameObject[] AntiSlime = GameObject.FindGameObjectsWithTag("AntiSlimeBullet");
            foreach (GameObject antiSlime in AntiSlime)
            {
                if (antiSlime.activeSelf)
                {
                    ObjectPool.instance.ReturnAntiSlimeBulletToPool(antiSlime);
                }
            }
        }

        RemoveSomeProjectiles();
    }

    public void RemoveSomeProjectiles()
    {
        if (PickUpgrade.choseScythe == true)
        {
            GameObject[] Scythe = GameObject.FindGameObjectsWithTag("Scythe");
            foreach (GameObject scythe in Scythe)
            {
                if (scythe.activeSelf)
                {
                    ObjectPool.instance.ReturnScyntheFromPool(scythe);
                }
            }
        }

        if (PickUpgrade.choseBoulder == true)
        {
            GameObject[] Boulder = GameObject.FindGameObjectsWithTag("Boulder");
            foreach (GameObject boulder in Boulder)
            {
                if (boulder.activeSelf)
                {
                    ObjectPool.instance.ReturnBoulderFromPool(boulder);
                }
            }
        }

        if (PickUpgrade.choseLog == true)
        {
            GameObject[] Log = GameObject.FindGameObjectsWithTag("Log");
            foreach (GameObject log in Log)
            {
                if (log.activeSelf)
                {
                    ObjectPool.instance.ReturnLogToPool(log);
                }
            }
        }

        if (PickUpgrade.choseSawBlade == true)
        {
            GameObject[] SawBlade = GameObject.FindGameObjectsWithTag("SawBlade");
            foreach (GameObject sawBlade in SawBlade)
            {
                if (sawBlade.activeSelf)
                {
                    ObjectPool.instance.ReturnSawbladeToPool(sawBlade);
                }
            }
        }

        if (PickUpgrade.choseKatana == true)
        {
            GameObject[] Katana = GameObject.FindGameObjectsWithTag("Katana");
            foreach (GameObject katana in Katana)
            {
                if (katana.activeSelf)
                {
                    ObjectPool.instance.ReturnKatanaToPool(katana);
                }
            }
        }

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


    #region socials
    public void OpenSocials(int social)
    {
        if(social == 1) { Application.OpenURL("https://discord.gg/qrBGyWkCgJ"); }
        if (social == 2) { Application.OpenURL("https://www.twitch.tv/simon_ef"); }
        if (social == 3) { Application.OpenURL("https://www.youtube.com/@EagleEyeGames/featured"); }
        if (social == 4) { Application.OpenURL("https://store.steampowered.com/curator/43674917"); }
        if (social == 5) { Application.OpenURL("https://store.steampowered.com/app/3449900/Slime_Squisher/"); }
    }
    #endregion

    #region change background
    public static int backgroundNumber;
    public GameObject background1, background2, background3, background4;
    public TextMeshProUGUI backgroundText;

    public void ChangeBackground(bool changeBackground)
    {
        background1.SetActive(false); background2.SetActive(false); background3.SetActive(false); background4.SetActive(false);

        if(changeBackground == true) { backgroundNumber += 1; UiClickSound(); }

        bool testing = true;

        if(DemoScript.isDemo == true && testing == false)
        {
            if (backgroundNumber == 1) { background2.SetActive(true); }
            if (backgroundNumber == 2) { background1.SetActive(true); }
            if (backgroundNumber == 3) { background2.SetActive(true); backgroundNumber = 1; }
        }
        else
        {
            if (backgroundNumber == 1) { background2.SetActive(true); }

            if (backgroundNumber == 2) { background1.SetActive(true); }
            else if (backgroundNumber == 3) { background3.SetActive(true); }
            else if (backgroundNumber == 4) { background4.SetActive(true); }
            else if (backgroundNumber == 5) { backgroundNumber = 1; background2.SetActive(true); }
        }
      
        backgroundText.text = $"{LocalizationSCRIPT.background}(" + backgroundNumber + ")";

        PlayerPrefs.SetInt("backgroundSave", backgroundNumber);
    }
    #endregion

    public int lookedTut;
    public Button playBtn;
    public GameObject tutArrow1, tutArrow2;
    public void LookedAtTurotial()
    {
        tutArrow1.SetActive(false); tutArrow2.SetActive(false);
        playBtn.interactable = true;
        lookedTut = 1;
        PlayerPrefs.SetInt("lookedTut", lookedTut);
    }

    IEnumerator SetBlockOff()
    {
        yield return new WaitForSeconds(0.33f);
        blockFrame.SetActive(false);
    }

    public void UiClickSound()
    {
        audioManager.Play("Ui_click1");
    }
}
