using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public GameData()
    {
        GamemodeSaves();
        ActiveSaves();
        MetaProgressionSaves();
        AchievementsSaves();
    }

    #region Gamemode saves
    public bool choseEasy, choseNormal, choseHard, choseBullethell, choseFlash, choseFragile, choseNarrow, choseRampage;
    public bool isNormalUnlocked, isHardUnlocked, isBullethellUnlocked, isFlashunlocked, isFragileUnlocked, isNarrowUnlocked, isRampageUnlocked;
    public bool easyCompleted, normalCompleted, hardCompleted, bullethellCompleted, flashCompleted, fragileCompleted, narrowCompleted, rampageCompleted;

    public void GamemodeSaves()
    {
        this.isNormalUnlocked = false;
        this.isHardUnlocked = false;
        this.isBullethellUnlocked = false;
        this.isFlashunlocked = false;
        this.isFragileUnlocked = false;
        this.isNarrowUnlocked = false;
        this.isRampageUnlocked = false;

        this.choseEasy = true;
        this.choseNormal = false;
        this.choseHard = false;
        this.choseBullethell = false;
        this.choseFlash = false;
        this.choseFragile = false;
        this.choseNarrow = false;
        this.choseRampage = false;

        this.easyCompleted = false;
        this.normalCompleted = false;
        this.hardCompleted = false;
        this.bullethellCompleted = false;
        this.flashCompleted = false;
        this.fragileCompleted = false;
        this.narrowCompleted = false;
        this.rampageCompleted = false;
    }
    #endregion

    #region Active saves
    public bool choseDeathToSlimes, choseSharpClicks, choseClover, choseDecoy, choseProjectileFrenzy, choseAntiSlime;
    public bool isPunchyClicksUnlcoked, isCloverUnlocked, isDecoyUnlocked, isProjectileFrenzyUnlocked, isAntiSlimeBulletsUnlocked;

    public void ActiveSaves()
    {
        this.choseDeathToSlimes = true;
        this.choseSharpClicks = false;
        this.choseClover = false;
        this.choseDecoy = false;
        this.choseProjectileFrenzy = false;
        this.choseAntiSlime = false;

        this.isPunchyClicksUnlcoked = false;
        this.isCloverUnlocked = false;
        this.isDecoyUnlocked = false;
        this.isProjectileFrenzyUnlocked = false;
        this.isAntiSlimeBulletsUnlocked = false;
    }
    #endregion

    #region Meta progression saves
    public int totalCoins;

    public int coinChance_PURCHASED, clickDamage_PURCHASED, healthIncrease_PURCHASED, crit_PURCHASED, clickCooldown_PURCHASED, regen_PURCHASED, slots_PURCHASED, reroll_PURCHASED, damageCooldown_PURCHASED, moreChoises_PURCHASED, onSlime_CD_PURCHASED, damageIncrease_PURCHASED, activeTier_PURCHASED, slowerSlimes_PURCHASED, slowerBullets_PURCHASED;

    public float goldChanceIncrease, damageIncrease, critChanceIncrease, critIncreaseIncrease, clickCooldownDecrease, damagedCooldownIcrease, slowerSlimes, slowerBullets;

    public int clickDamageIncrease, startHealth, healEveryWave, slotIncrease, rerolls, extraUpgradeChoises, onSlime_CD_ChanceIncrease, activeTier;

    public int clickDamageIncrease_price, startHealth_price, healEveryWave_price, slotIncrease_price, rerolls_price, extraUpgradeChoises_price, onSlime_CD_ChanceIncrease_price, activeTier_price, goldChanceIncrease_price, damageIncrease_price, critPrice, clickCooldownDecrease_price, damagedCooldownIcrease_price, slowerSlimes_price, slowerBullets_price;

    public void MetaProgressionSaves()
    {
        totalCoins = 0;

        coinChance_PURCHASED = 0;
        clickDamage_PURCHASED = 0;
        healthIncrease_PURCHASED = 0;
        crit_PURCHASED = 0;
        clickCooldown_PURCHASED = 0;
        regen_PURCHASED = 0;
        slots_PURCHASED = 0;
        reroll_PURCHASED = 0;
        damageCooldown_PURCHASED = 0;
        moreChoises_PURCHASED = 0;
        onSlime_CD_PURCHASED = 0;
        damageIncrease_PURCHASED = 0;
        activeTier_PURCHASED = 0;
        slowerSlimes_PURCHASED = 0;
        slowerBullets_PURCHASED = 0;

        clickDamageIncrease = 0;
        startHealth = 0;
        healEveryWave = 0;
        slotIncrease = 0;
        rerolls = 0;
        extraUpgradeChoises = 0;
        onSlime_CD_ChanceIncrease = 0;
        activeTier = 0;

        goldChanceIncrease = 0f;
        damageIncrease = 0f;
        critChanceIncrease = 0f;
        critIncreaseIncrease = 0f;
        clickCooldownDecrease = 0f;
        damagedCooldownIcrease = 0f;
        slowerSlimes = 0f;
        slowerBullets = 0f;

        goldChanceIncrease_price = 8;
        clickDamageIncrease_price = 8;
        startHealth_price = 10;
        critPrice = 10;
        clickCooldownDecrease_price = 20;
        healEveryWave_price = 15;
        slotIncrease_price = 15;
        rerolls_price = 20;
        damagedCooldownIcrease_price = 12;
        extraUpgradeChoises_price = 35;
        onSlime_CD_ChanceIncrease_price = 30;
        damageIncrease_price = 25;
        activeTier_price = 50;
        slowerSlimes_price = 15;
        slowerBullets_price = 20;
    }
    #endregion

    #region straberry health
    public int strawberryHealth;
    public bool isHalfHeart;

    public void StrawberryHealthSaves()
    {
        this.isHalfHeart = false;
        this.strawberryHealth = 0;
    }
    #endregion

    #region ach saves
    public bool achievedRegularEasy;
    public bool achievedRegularNormal;
    public bool achievedRegularHard;
    public bool achievedBulletHell;
    public bool achievedFlash;
    public bool achievedFragile;
    public bool achievedRampage;
    public bool achievedAllGamemodes;
    public bool achievedNoDamageRun;
    public bool achievedPunchyClicks;
    public bool achievedClover;
    public bool achievedDecoy;
    public bool achievedProjectileFrenzy;
    public bool achievedAntiSlimeBullets;
    public bool achievedCollect10Coins;
    public bool achievedCollect100Coins;
    public bool achievedCollect1000Coins;
    public bool achievedMaxUpgrade;
    public bool achievedAllUpgrades;
    public bool achievedOneOfEachUpgrade;
    public bool achievedAllGunOrbitals;
    public bool achievedKickBullet;
    public bool achievedFourClickUpgrades;
    public bool achievedFiveClickUpgrades;
    public bool achievedSixClickUpgrades;
    public bool achievedSevenClickUpgrades;
    public bool achievedEightClickUpgrades;
    public bool achievedDiedOnce;

    public int totalGoldCoinsCollected, totalShopUpgradesPurchased;

    public void AchievementsSaves()
    {
        totalGoldCoinsCollected = 0;
        totalShopUpgradesPurchased = 0;

        achievedRegularEasy = false;
        achievedRegularNormal = false;
        achievedRegularHard = false;
        achievedBulletHell = false;
        achievedFlash = false;
        achievedFragile = false;
        achievedRampage = false;
        achievedAllGamemodes = false;
        achievedNoDamageRun = false;
        achievedPunchyClicks = false;
        achievedClover = false;
        achievedDecoy = false;
        achievedProjectileFrenzy = false;
        achievedAntiSlimeBullets = false;
        achievedCollect10Coins = false;
        achievedCollect100Coins = false;
        achievedCollect1000Coins = false;
        achievedMaxUpgrade = false;
        achievedAllUpgrades = false;
        achievedOneOfEachUpgrade = false;
        achievedAllGunOrbitals = false;
        achievedKickBullet = false;
        achievedFourClickUpgrades = false;
        achievedFiveClickUpgrades = false;
        achievedSixClickUpgrades = false;
        achievedSevenClickUpgrades = false;
        achievedEightClickUpgrades = false;
        achievedDiedOnce = false;
    }
    #endregion


}
