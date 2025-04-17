using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achivements : MonoBehaviour, IDataPersistence
{
    public static bool achievedRegularEasy = false;
    public static bool achievedRegularNormal = false;
    public static bool achievedRegularHard = false;
    public static bool achievedBulletHell = false;
    public static bool achievedFlash = false;
    public static bool achievedFragile = false;
    public static bool achievedRampage = false;
    public static bool achievedAllGamemodes = false;
    public static bool achievedNoDamageRun = false;
    public static bool achievedPunchyClicks = false;
    public static bool achievedClover = false;
    public static bool achievedDecoy = false;
    public static bool achievedProjectileFrenzy = false;
    public static bool achievedAntiSlimeBullets = false;
    public static bool achievedCollect10Coins = false;
    public static bool achievedCollect100Coins = false;
    public static bool achievedCollect1000Coins = false;
    public static bool achievedMaxUpgrade = false;
    public static bool achievedAllUpgrades = false;
    public static bool achievedOneOfEachUpgrade = false;
    public static bool achievedAllGunOrbitals = false;
    public static bool achievedKickBullet = false;
    public static bool achievedFourClickUpgrades = false;
    public static bool achievedFiveClickUpgrades = false;
    public static bool achievedSixClickUpgrades = false;
    public static bool achievedSevenClickUpgrades = false;
    public static bool achievedEightClickUpgrades = false;
    public static bool achievedDiedOnce = false;

    public static int totalGoldCoinsCollected, totalShopUpgradesPurchased;

    private void Start()
    {
        StartCoroutine(CheckAch());
    }

    IEnumerator CheckAch()
    {
        yield return new WaitForSeconds(0.5f);

        if(MobileScript.isMobile == false)
        {
            if (achievedRegularEasy) { TriggerACH("complete_easy"); }
            if (achievedRegularNormal) { TriggerACH("complete_normal"); }
            if (achievedRegularHard) { TriggerACH("complete_hard"); }
            if (achievedBulletHell) { TriggerACH("complete_bullethell"); }
            if (achievedFlash) { TriggerACH("complete_flash"); }
            if (achievedFragile) { TriggerACH("complete_fragile"); }
            if (achievedRampage) { TriggerACH("complete_rampage"); }
            if (achievedAllGamemodes) { TriggerACH("complete_allGamemode"); }
            if (achievedNoDamageRun) { TriggerACH("noDamage"); }
            if (achievedPunchyClicks) { TriggerACH("purchase_punchyClicks"); }
            if (achievedClover) { TriggerACH("purchase_clover"); }
            if (achievedDecoy) { TriggerACH("purchase_decoy"); }
            if (achievedProjectileFrenzy) { TriggerACH("purchase_frenzy"); }
            if (achievedAntiSlimeBullets) { TriggerACH("purchase_antiSlime"); }
            if (achievedCollect10Coins) { TriggerACH("coins_10"); }
            if (achievedCollect100Coins) { TriggerACH("coins_100"); }
            if (achievedCollect1000Coins) { TriggerACH("coins_1000"); }
            if (achievedMaxUpgrade) { TriggerACH("shop_1max"); }
            if (achievedAllUpgrades) { TriggerACH("shop_100percent"); }
            if (achievedOneOfEachUpgrade) { TriggerACH("shop1ofeach"); }
            if (achievedAllGunOrbitals) { TriggerACH("allGuns"); }
            if (achievedKickBullet) { TriggerACH("kick"); }
            if (achievedFourClickUpgrades) { TriggerACH("slot4"); }
            if (achievedFiveClickUpgrades) { TriggerACH("slot5"); }
            if (achievedSixClickUpgrades) { TriggerACH("slot6"); }
            if (achievedSevenClickUpgrades) { TriggerACH("slot7"); }
            if (achievedEightClickUpgrades) { TriggerACH("slot8"); }
            if (achievedDiedOnce) { TriggerACH("dieOnce"); }
        }
        //ClearAllAch();
    }

    #region Trigger ach
    public void TriggerACH(string achNAME)
    {
        if (MobileScript.isMobile == true) { return; }
        if (SteamIntgr.noSteamInt == true) { return; }

        if (DemoScript.isDemo == false)
        {
            //var ach = new Steamworks.Data.Achievement(achNAME);
            //if (ach.State == false)
            //{
                //ach.Trigger();
            //}
        }
    }
    #endregion

    #region Clear all ach
    public void ClearAllAch()
    {
        AchClear("complete_easy");
        AchClear("complete_normal");
        AchClear("complete_hard");
        AchClear("complete_bullethell");
        AchClear("complete_flash");
        AchClear("complete_fragile");
        AchClear("complete_rampage");
        AchClear("complete_allGamemode");
        AchClear("noDamage");
        AchClear("purchase_punchyClicks");
        AchClear("purchase_clover");
        AchClear("purchase_decoy");
        AchClear("purchase_frenzy");
        AchClear("purchase_antiSlime");
        AchClear("coins_10");
        AchClear("coins_100");
        AchClear("coins_1000");
        AchClear("shop_1max");
        AchClear("shop_100percent");
        AchClear("shop1ofeach");
        AchClear("allGuns");
        AchClear("kick");
        AchClear("slot4");
        AchClear("slot5");
        AchClear("slot6");
        AchClear("slot7");
        AchClear("slot8");
        AchClear("dieOnce");
    }
    #endregion

    public void AchClear(string achNAME)
    {
        //var ach = new Steamworks.Data.Achievement(achNAME);
        //ach.Clear();
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        achievedRegularEasy = data.achievedRegularEasy;
        achievedRegularNormal = data.achievedRegularNormal;
        achievedRegularHard = data.achievedRegularHard;
        achievedBulletHell = data.achievedBulletHell;
        achievedFlash = data.achievedFlash;
        achievedFragile = data.achievedFragile;
        achievedRampage = data.achievedRampage;
        achievedAllGamemodes = data.achievedAllGamemodes;
        achievedNoDamageRun = data.achievedNoDamageRun;
        achievedPunchyClicks = data.achievedPunchyClicks;
        achievedClover = data.achievedClover;
        achievedDecoy = data.achievedDecoy;
        achievedProjectileFrenzy = data.achievedProjectileFrenzy;
        achievedAntiSlimeBullets = data.achievedAntiSlimeBullets;
        achievedCollect10Coins = data.achievedCollect10Coins;
        achievedCollect100Coins = data.achievedCollect100Coins;
        achievedCollect1000Coins = data.achievedCollect1000Coins;
        achievedMaxUpgrade = data.achievedMaxUpgrade;
        achievedAllUpgrades = data.achievedAllUpgrades;
        achievedOneOfEachUpgrade = data.achievedOneOfEachUpgrade;
        achievedAllGunOrbitals = data.achievedAllGunOrbitals;
        achievedKickBullet = data.achievedKickBullet;
        achievedFourClickUpgrades = data.achievedFourClickUpgrades;
        achievedFiveClickUpgrades = data.achievedFiveClickUpgrades;
        achievedSixClickUpgrades = data.achievedSixClickUpgrades;
        achievedSevenClickUpgrades = data.achievedSevenClickUpgrades;
        achievedEightClickUpgrades = data.achievedEightClickUpgrades;
        achievedDiedOnce = data.achievedDiedOnce;

        totalGoldCoinsCollected = data.totalGoldCoinsCollected;
        totalShopUpgradesPurchased = data.totalShopUpgradesPurchased;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.achievedRegularEasy = achievedRegularEasy;
        data.achievedRegularNormal = achievedRegularNormal;
        data.achievedRegularHard = achievedRegularHard;
        data.achievedBulletHell = achievedBulletHell;
        data.achievedFlash = achievedFlash;
        data.achievedFragile = achievedFragile;
        data.achievedRampage = achievedRampage;
        data.achievedAllGamemodes = achievedAllGamemodes;
        data.achievedNoDamageRun = achievedNoDamageRun;
        data.achievedPunchyClicks = achievedPunchyClicks;
        data.achievedClover = achievedClover;
        data.achievedDecoy = achievedDecoy;
        data.achievedProjectileFrenzy = achievedProjectileFrenzy;
        data.achievedAntiSlimeBullets = achievedAntiSlimeBullets;
        data.achievedCollect10Coins = achievedCollect10Coins;
        data.achievedCollect100Coins = achievedCollect100Coins;
        data.achievedCollect1000Coins = achievedCollect1000Coins;
        data.achievedMaxUpgrade = achievedMaxUpgrade;
        data.achievedAllUpgrades = achievedAllUpgrades;
        data.achievedOneOfEachUpgrade = achievedOneOfEachUpgrade;
        data.achievedAllGunOrbitals = achievedAllGunOrbitals;
        data.achievedKickBullet = achievedKickBullet;
        data.achievedFourClickUpgrades = achievedFourClickUpgrades;
        data.achievedFiveClickUpgrades = achievedFiveClickUpgrades;
        data.achievedSixClickUpgrades = achievedSixClickUpgrades;
        data.achievedSevenClickUpgrades = achievedSevenClickUpgrades;
        data.achievedEightClickUpgrades = achievedEightClickUpgrades;
        data.achievedDiedOnce = achievedDiedOnce;

        data.totalGoldCoinsCollected = totalGoldCoinsCollected;
        data.totalShopUpgradesPurchased = totalShopUpgradesPurchased;
    }
    #endregion

}
