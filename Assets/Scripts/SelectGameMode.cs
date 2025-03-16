using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectGameMode : MonoBehaviour, IDataPersistence
{
    public AudioManager audioManager;

    public static int easy_waveToReach, normal_waveToReach, hard_waveToReach, bullethell_waveToReach, flash_waveToReach, fragile_waveToReach, narrow_waveToReach, rampage_MinuteToReach;

    public static bool easyCompleted, normalCompleted, hardCompleted, bullethellCompleted, flashCompleted, fragileCompleted, narrowCompleted, rampageCompleted;

    public static int easyReward, normalReward, hardReward, bullethellReward, fragileReward, flashReward, narrowReward, rampageReward;

    private void Awake()
    {
        easyReward = 20;
        normalReward = 40;
        hardReward = 100;

        bullethellReward = 35;
        flashReward = 30;
        fragileReward = 50;
        narrowReward = 25;
        rampageReward = 25;

        easy_waveToReach = 25;
        normal_waveToReach = 30;
        hard_waveToReach = 35;

        bullethell_waveToReach = 20;
        flash_waveToReach = 20;
        fragile_waveToReach = 15;
        narrow_waveToReach = 20;
        rampage_MinuteToReach = 10;
    }

    public GameObject normalLocked, hardLocked, bullethellLocked, flashLocked, fragileLocked, narrowLocked, rampageLocked;

    private void Start()
    {
        if(DemoScript.isDemo == true)
        {
            isNormalUnlocked = false;
            isHardUnlocked = false;
            isBullethellUnlocked = false;
            isFlashunlocked = false;
            isFragileUnlocked = false;
            isNarrowUnlocked = false;
            isRampageUnlocked = false;

            choseEasy = true;
            choseNormal = false;
            choseHard = false;
            choseBullethell = false;
            choseFlash = false;
            choseFragile = false;
            choseNarrow = false;
            choseRampage = false;
        }

        if (DemoScript.isDemo == false)
        {

        }

        playSound = false;
        StartCoroutine(Wait());
    }

    bool playSound;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);

        if (DemoScript.isDemo == false)
        {
            isNormalUnlocked = true;
            isHardUnlocked = true;
            isBullethellUnlocked = true;
            isFlashunlocked = true;
            isFragileUnlocked = true;
            isNarrowUnlocked = true;
            isRampageUnlocked = true;

            normalLocked.SetActive(false);
            hardLocked.SetActive(false);
            bullethellLocked.SetActive(false);
            flashLocked.SetActive(false);
            fragileLocked.SetActive(false);
            narrowLocked.SetActive(false);
            rampageLocked.SetActive(false);
        }

        if (choseEasy == true) { SelectTheGamemode(1); }
        if (choseNormal == true) { SelectTheGamemode(2); }
        if (choseHard == true) { SelectTheGamemode(3); }
        if (choseBullethell == true) { SelectTheGamemode(4); }
        if (choseFlash == true) { SelectTheGamemode(5); }
        if (choseFragile == true) { SelectTheGamemode(6); }
        if (choseNarrow == true) { SelectTheGamemode(7); }
        if (choseRampage == true) { SelectTheGamemode(8); }

        rewardText.gameObject.SetActive(false);

        playSound = true;
    }

    public void SetAllOff()
    {
        choseEasy = false;
        choseNormal = false;
        choseHard = false;
        choseBullethell = false;
        choseFlash = false;
        choseFragile = false;
        choseNarrow = false;
        choseRampage = false;
    }

    public static bool choseEasy, choseNormal, choseHard, choseBullethell, choseFlash, choseFragile, choseNarrow, choseRampage;
    public static bool isNormalUnlocked, isHardUnlocked, isBullethellUnlocked, isFlashunlocked, isFragileUnlocked, isNarrowUnlocked, isRampageUnlocked;

    public Transform easy, hard, normal, bullethell, flash, fragile, narrow, rampage;
    public Transform selectedIcon;

    public TextMeshProUGUI gamemodeDesText, rewardText;

    public static bool justSetStuff;


    #region Select gamemode
    public void SelectTheGamemode(int gamemode)
    {
        if (gamemode == 1) 
        {
            SetAllOff();
            selectedIcon.transform.position = easy.transform.position;
            choseEasy = true;
            gamemodeDesText.text = LocalizationSCRIPT.easyDescription + " " + LocalizationSCRIPT.SELECTED;
            if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            if(DemoScript.isDemo == false && easyCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + easyReward; }
        }
        if (gamemode == 2) 
        {
            if (isNormalUnlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = hard.transform.position;
            choseNormal = true;
            gamemodeDesText.text = LocalizationSCRIPT.normalDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && normalCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + normalReward; }
        }
        if (gamemode == 3)
        {
            if (isHardUnlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = normal.transform.position;
            choseHard = true;
            gamemodeDesText.text = LocalizationSCRIPT.hardDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && hardCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + hardReward; }
        }
        if (gamemode == 4) 
        {
            if (isBullethellUnlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = bullethell.transform.position;
            choseBullethell = true;
            gamemodeDesText.text = LocalizationSCRIPT.bulletHellDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && bullethellCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + bullethellReward; }
        }
        if (gamemode == 5) 
        {
            if (isFlashunlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = flash.transform.position;
            choseFlash = true;
            gamemodeDesText.text = LocalizationSCRIPT.flahsDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && flashCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + flashReward; }
        }
        if (gamemode == 6) 
        {
            if (isFragileUnlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = fragile.transform.position;
            choseFragile = true;
            gamemodeDesText.text = LocalizationSCRIPT.fragileDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && fragileCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + fragileReward; }
        }
        if (gamemode == 7)
        {
            if (isNarrowUnlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = narrow.transform.position;
            choseNarrow = true;
            gamemodeDesText.text = LocalizationSCRIPT.narrowDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && narrowCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + narrowReward; }
        }
        if (gamemode == 8) 
        {
            if (isRampageUnlocked == false && justSetStuff == false) { audioManager.Play("Error"); return; }
            SetAllOff(); if (playSound == true && justSetStuff == false) { audioManager.Play("Select"); }
            selectedIcon.transform.position = rampage.transform.position;
            choseRampage = true;
            gamemodeDesText.text = LocalizationSCRIPT.rampageDescription + " " + LocalizationSCRIPT.SELECTED;
            if (DemoScript.isDemo == false && rampageCompleted == false) { rewardText.gameObject.SetActive(true); rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + rampageReward; }
        }

        justSetStuff = false;
    }
    #endregion



    #region Load Data
    public void LoadData(GameData data)
    {
        choseEasy = data.choseEasy;
        choseNormal = data.choseNormal;
        choseHard = data.choseHard;
        choseBullethell = data.choseBullethell;
        choseFlash = data.choseFlash;
        choseFragile = data.choseFragile;
        choseNarrow = data.choseNarrow;
        choseRampage = data.choseRampage;

        isNormalUnlocked = data.isNormalUnlocked;
        isHardUnlocked = data.isHardUnlocked;
        isBullethellUnlocked = data.isBullethellUnlocked;
        isFlashunlocked = data.isFlashunlocked;
        isFragileUnlocked = data.isFragileUnlocked;
        isNarrowUnlocked = data.isNarrowUnlocked;
        isRampageUnlocked = data.isRampageUnlocked;

        easyCompleted = data.easyCompleted;
        normalCompleted = data.normalCompleted;
        hardCompleted = data.hardCompleted;
        bullethellCompleted = data.bullethellCompleted;
        flashCompleted = data.flashCompleted;
        fragileCompleted = data.fragileCompleted;
        narrowCompleted = data.narrowCompleted;
        rampageCompleted = data.rampageCompleted;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.choseEasy = choseEasy;
        data.choseNormal = choseNormal;
        data.choseHard = choseHard;
        data.choseBullethell = choseBullethell;
        data.choseFlash = choseFlash;
        data.choseFragile = choseFragile;
        data.choseNarrow = choseNarrow;
        data.choseRampage = choseRampage;

        data.isNormalUnlocked = isNormalUnlocked;
        data.isHardUnlocked = isHardUnlocked;
        data.isBullethellUnlocked = isBullethellUnlocked;
        data.isFlashunlocked = isFlashunlocked;
        data.isFragileUnlocked = isFragileUnlocked;
        data.isNarrowUnlocked = isNarrowUnlocked;
        data.isRampageUnlocked = isRampageUnlocked;

        data.easyCompleted = easyCompleted;
        data.normalCompleted = normalCompleted;
        data.hardCompleted = hardCompleted;
        data.bullethellCompleted = bullethellCompleted;
        data.flashCompleted = flashCompleted;
        data.fragileCompleted = fragileCompleted;
        data.narrowCompleted = narrowCompleted;
        data.rampageCompleted = rampageCompleted;
    }
    #endregion
}
