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
        SlotsSaves();
        MetaProgressionSaves();
        SpawnSlimesSaves();
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

    #region Slots saves

    public void SlotsSaves()
    {
    }
    #endregion

    #region Meta progression saves
    public int totalCoins;

    public int coinChance_PURCHASED, clickDamage_PURCHASED, healthIncrease_PURCHASED, crit_PURCHASED, clickCooldown_PURCHASED, regen_PURCHASED, slots_PURCHASED, reroll_PURCHASED, damageCooldown_PURCHASED, moreChoises_PURCHASED, onSlime_CD_PURCHASED, damageIncrease_PURCHASED, activeTier_PURCHASED, slowerSlimes_PURCHASED, slowerBullets_PURCHASED;

    public float goldChanceIncrease, damageIncrease, critChanceIncrease, critIncreaseIncrease, clickCooldownDecrease, damagedCooldownIcrease, slowerSlimes, slowerBullets;

    public int clickDamageIncrease, startHealth, healEveryWave, slotIncrease, rerolls, extraUpgradeChoises, onSlime_CD_ChanceIncrease, activeTier;

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
    }
    #endregion

    #region Slime wave saves
    public int slimeWave;

    public void SpawnSlimesSaves()
    {
        this.slimeWave = 0;
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
}
