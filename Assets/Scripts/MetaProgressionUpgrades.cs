using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MetaProgressionUpgrades : MonoBehaviour, IDataPersistence
{
    public static int upgradeChooseCount;

    public static int totalCoins;

    public TextMeshProUGUI totalCoinsTexT;

    public AudioManager audioManager;

    public static int clickDamageIncrease_price, startHealth_price, healEveryWave_price, slotIncrease_price, rerolls_price, extraUpgradeChoises_price, onSlime_CD_ChanceIncrease_price, activeTier_price, goldChanceIncrease_price, damageIncrease_price, critPrice, clickCooldownDecrease_price, damagedCooldownIcrease_price, slowerSlimes_price, slowerBullets_price;

    public static int coinChance_MAX, clickDamage_MAX, healthIncrease_MAX, crit_MAX, clickCooldown_MAX, regen_MAX, slots_MAX, reroll_MAX, damageCooldown_MAX, moreChoises_MAX, onSlime_CD_MAX, damageIncrease_MAX, activeTier_MAX, slowerSlimes_MAX, slowerBullets_MAX;

    public static int coinChance_PURCHASED, clickDamage_PURCHASED, healthIncrease_PURCHASED, crit_PURCHASED, clickCooldown_PURCHASED, regen_PURCHASED, slots_PURCHASED, reroll_PURCHASED, damageCooldown_PURCHASED, moreChoises_PURCHASED, onSlime_CD_PURCHASED, damageIncrease_PURCHASED, activeTier_PURCHASED, slowerSlimes_PURCHASED, slowerBullets_PURCHASED;

    public static int goldStartDropChance;

    private void Awake()
    {
        StrawberryMechanics.hitCooldownTimer = 3f;

        coinChance_MAX = 5;
        clickDamage_MAX = 5;
        healthIncrease_MAX = 3;
        crit_MAX = 5;
        clickCooldown_MAX = 5;
        regen_MAX = 4;
        slots_MAX = 4;
        reroll_MAX = 3;
        damageCooldown_MAX = 5;
        moreChoises_MAX = 2;
        onSlime_CD_MAX = 5;
        damageIncrease_MAX = 5;
        activeTier_MAX = 2;
        slowerSlimes_MAX = 5;
        slowerBullets_MAX = 5;

        goldStartDropChance = 2;

        goldChanceIncrease_price = 5;
        clickDamageIncrease_price = 5;
        startHealth_price = 7;
        critPrice = 12;
        clickCooldownDecrease_price = 15;
        healEveryWave_price = 20;
        slotIncrease_price = 30;
        rerolls_price = 30;
        damagedCooldownIcrease_price = 20;
        extraUpgradeChoises_price = 50;
        onSlime_CD_ChanceIncrease_price = 35;
        damageIncrease_price = 35;
        activeTier_price = 75;
        slowerSlimes_price = 20;
        slowerBullets_price = 20;

        totalCoins = 0;

        upgradeChooseCount = 3;
    }

    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        upgradeName.text = "Select an upgrade";
        upgradeStats.text = "";
        upgradePrice.text = "";
        priceGoldCoinObject.SetActive(false);

        totalCoins = 341;
        SetUpgradeMaxNumbers();
    }

    #region Update
    public TextMeshProUGUI goldAvailableText;

    private void Update()
    {
        goldAvailableText.text = "Available: <color=yellow>" + totalCoins;

        totalCoinsTexT.text = totalCoins.ToString("F0");
    }
    #endregion

    public static float goldChanceIncrease, damageIncrease, critChanceIncrease, critIncreaseIncrease, clickCooldownDecrease, damagedCooldownIcrease, slowerSlimes, slowerBullets;

    public static int clickDamageIncrease, startHealth, healEveryWave, slotIncrease, rerolls, extraUpgradeChoises, onSlime_CD_ChanceIncrease, activeTier;

    #region Select and purchase
    public static int upgradeSelected;

    public GameObject upgradeSelectedIcon;

    public GameObject[] upgrades;

    public TextMeshProUGUI upgradeName, upgradeStats, upgradePrice;

    public ActiveMechanics activeScript;
    public LocalizationSCRIPT locScript;

    public void SelectUpgrade(int selected)
    {
        upgradeSelectedIcon.SetActive(true);
        audioManager.Play("Select");
        upgradeSelected = selected;
        upgradeSelectedIcon.transform.localPosition = upgrades[selected].transform.localPosition;

        SetTexts(selected);
    }

    public GameObject priceGoldCoinObject;

    public void SetTexts(int selected)
    {
        #region Gold drop chance
        if (selected == 0)
        {
            upgradeName.text = "Gold coin drop chance";

            if (coinChance_PURCHASED == coinChance_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{goldStartDropChance + goldChanceIncrease}%";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{goldChanceIncrease_price}";
                upgradeStats.text = $"<color=green>{goldStartDropChance + goldChanceIncrease}% -> {goldStartDropChance + goldChanceIncrease + 1f}%";
            }
        }
        #endregion 

        #region Click damage increase
        if (selected == 1)
        {
            upgradeName.text = "Start click damage";

            if (clickDamage_PURCHASED == clickDamage_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{10 + clickDamageIncrease}";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{clickDamageIncrease_price}";
                upgradeStats.text = $"<color=green>{10 + clickDamageIncrease} -> {10 + clickDamageIncrease + 1}";
            }
        }
        #endregion 

        #region Start health
        if (selected == 2)
        {
            upgradeName.text = "Start health";

            if (healthIncrease_PURCHASED == healthIncrease_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{2 + startHealth}";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{startHealth_price}";
                upgradeStats.text = $"<color=green>{2 + startHealth} -> {2 + startHealth + 1}";
            }
        }
        #endregion //Done

        #region Crit
        if (selected == 3)
        {
            upgradeName.text = "Crit chance and crit increase";

            if (crit_PURCHASED == crit_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{0 + critChanceIncrease}% & {(0 + critIncreaseIncrease) * 100}%";

            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{critPrice}";
                upgradeStats.text = $"<color=green>{0 + critChanceIncrease}% -> {0 + critChanceIncrease + 2}% & {(0 + critIncreaseIncrease) * 100}% -> {((0 + critIncreaseIncrease + 0.5) * 100).ToString("F0")}%";
            }

            if (crit_PURCHASED == 0)
            {
                upgradeStats.text = $"<color=green>{0 + critChanceIncrease}% -> {0 + critChanceIncrease + 2}% & {(0 + critIncreaseIncrease) * 100}% -> {(0 + 1.1f) * 100}%";
            }
        }
        #endregion //Done

        #region Click cooldown
        if (selected == 4)
        {
            upgradeName.text = "Start click cooldown";

            if (clickCooldown_PURCHASED == clickCooldown_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{PickUpgrade.clickCooldown - clickCooldownDecrease} sec";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{clickCooldownDecrease_price}";
                upgradeStats.text = $"<color=green>{PickUpgrade.clickCooldown - clickCooldownDecrease} sec -> {PickUpgrade.clickCooldown - clickCooldownDecrease - 0.07f} sec";
            }
        }
        #endregion //Done

        #region Heal every wave
        if (selected == 5)
        {
            upgradeName.text = "Heal half a heart every";
            
            if (regen_PURCHASED == regen_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{healEveryWave} waves";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{healEveryWave_price}";
                upgradeStats.text = $"<color=green>{0 + healEveryWave} waves -> {0 + healEveryWave - 1} waves";
            }

            if (healEveryWave == 0)
            {
                upgradeStats.text = $"<color=green>{0} waves -> {7} waves";
            }
        }
        #endregion

        #region More upgrade slots
        if (selected == 6)
        {
            upgradeName.text = "More upgrde slots";

            if (slots_PURCHASED == slots_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{slotIncrease + ManageSlots.slotsAviable}";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{slotIncrease_price}";
                upgradeStats.text = $"<color=green>{slotIncrease + ManageSlots.slotsAviable} -> {slotIncrease + ManageSlots.slotsAviable + 1}";
            }
        }
        #endregion

        #region Rerolls
        if (selected == 7)
        {
            upgradeName.text = "Rerolls";

            if (reroll_PURCHASED == reroll_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{rerolls}";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{rerolls_price}";
                upgradeStats.text = $"<color=green>{0 + rerolls} -> {rerolls + 3}";
            }
        }
        #endregion

        #region Damage cooldown
        if (selected == 8)
        {
            upgradeName.text = "Invincibility after damaged";

            if (damageCooldown_PURCHASED == damageCooldown_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{StrawberryMechanics.hitCooldownTimer + damagedCooldownIcrease} sec";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{damagedCooldownIcrease_price}";
                upgradeStats.text = $"<color=green>{StrawberryMechanics.hitCooldownTimer + damagedCooldownIcrease} sec -> {StrawberryMechanics.hitCooldownTimer + damagedCooldownIcrease + 0.5f} sec";
            }
        }
        #endregion

        #region Upgrade choises
        if (selected == 9)
        {
            upgradeName.text = "More upgrade choises";

            if (moreChoises_PURCHASED == moreChoises_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{3 + extraUpgradeChoises}";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{extraUpgradeChoises_price}";
                upgradeStats.text = $"<color=green>{3 + extraUpgradeChoises} -> {3 + extraUpgradeChoises + 1}";
            }
        }
        #endregion

        #region On slime click/death chance increase
        if (selected == 10)
        {
            upgradeName.text = "On slime click/death trigger chance";

            if (onSlime_CD_PURCHASED == onSlime_CD_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{onSlime_CD_ChanceIncrease}%";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{onSlime_CD_ChanceIncrease_price}";
                upgradeStats.text = $"<color=green>{onSlime_CD_ChanceIncrease}% -> {onSlime_CD_ChanceIncrease + 1}%";
            }
        }
        #endregion

        #region Overall damage
        if (selected == 11)
        {
            upgradeName.text = "all damage increase";

            if (damageIncrease_PURCHASED == damageIncrease_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{damageIncrease}%";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{damageIncrease_price}";
                upgradeStats.text = $"<color=green>{damageIncrease}% -> {damageIncrease + 3}%";
            }
        }
        #endregion

        #region Active tier
        if (selected == 12)
        {
            upgradeName.text = "better active";

            if (activeTier_PURCHASED == activeTier_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>Tier {activeTier + 1}";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{activeTier_price}";
                upgradeStats.text = $"<color=green>Tier {activeTier + 1} -> Tier {activeTier + 1 + 1}";
            }
        }
        #endregion

        #region Slower slimes
        if (selected == 13)
        {
            upgradeName.text = "slower slimes";

            if (slowerSlimes_PURCHASED == slowerSlimes_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{slowerSlimes}%";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{slowerSlimes_price}";
                upgradeStats.text = $"<color=green>{slowerSlimes}% -> {slowerSlimes + 4}%";
            }
        }
        #endregion

        #region Slower slime bullets
        if (selected == 14)
        {
            upgradeName.text = "slower slime bullets";

            if (slowerBullets_PURCHASED == slowerBullets_MAX)
            {
                priceGoldCoinObject.SetActive(false);
                upgradePrice.text = "<color=red>MAX";
                upgradeStats.text = $"<color=green>{slowerBullets}%";
            }
            else
            {
                priceGoldCoinObject.SetActive(true);
                upgradePrice.text = $"Price: <color=yellow>{slowerBullets_price}";
                upgradeStats.text = $"<color=green>{slowerBullets}% -> {slowerBullets + 5}%";
            }
        }
        #endregion
    }

    public void BuyUpgrade()
    {
        #region Gold drop chance
        if (upgradeSelected == 0 && totalCoins >= goldChanceIncrease_price) 
        {
            if(coinChance_PURCHASED < coinChance_MAX)
            {
                coinChance_PURCHASED += 1;
                PurchaseSound();
                goldChanceIncrease += 1f;
                totalCoins -= goldChanceIncrease_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Click damage increase
        else if(upgradeSelected == 1 && totalCoins >= clickDamageIncrease_price)
        {
            if (clickDamage_PURCHASED < clickDamage_MAX)
            {
                clickDamage_PURCHASED += 1;
                PurchaseSound();
                clickDamageIncrease += 1;
                totalCoins -= clickDamageIncrease_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Start health
        else if (upgradeSelected == 2 && totalCoins >= startHealth_price)
        {
            if (healthIncrease_PURCHASED < healthIncrease_MAX)
            {
                healthIncrease_PURCHASED += 1;
                PurchaseSound();
                startHealth += 1;
                totalCoins -= startHealth_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Crit
        else if (upgradeSelected == 3 && totalCoins >= critPrice)
        {
            if (crit_PURCHASED < crit_MAX)
            {
                if(crit_PURCHASED == 0)
                {
                    critChanceIncrease += 2;
                    critIncreaseIncrease += 1.1f;
                }
                else
                {
                    critChanceIncrease += 2;
                    critIncreaseIncrease += 0.5f;
                }
                crit_PURCHASED += 1;
                PurchaseSound();
               
                totalCoins -= critPrice;
            }
            else { PlayError(); }
        }
        #endregion

        #region Click cooldown
        else if (upgradeSelected == 4 && totalCoins >= clickCooldownDecrease_price)
        {
            if (clickCooldown_PURCHASED < clickCooldown_MAX)
            {
                clickCooldown_PURCHASED += 1;
                PurchaseSound();
                clickCooldownDecrease += 0.07f;

                totalCoins -= clickCooldownDecrease_price;
            }
            else { PlayError(); }
        }
       
        #endregion

        #region Heal every wave
        else if (upgradeSelected == 5 && totalCoins >= healEveryWave_price)
        {
            if (regen_PURCHASED < regen_MAX)
            {
                regen_PURCHASED += 1;
                PurchaseSound();
                if (healEveryWave == 0) { healEveryWave = 7; }
                else
                {
                    healEveryWave -= 1;
                }

                totalCoins -= healEveryWave_price;
            }
            else { PlayError(); }
        }
     
        #endregion

        #region More upgrade slots
        else if (upgradeSelected == 6 && totalCoins >= slotIncrease_price)
        {
            if (slots_PURCHASED < slots_MAX)
            {
                slots_PURCHASED += 1;
                PurchaseSound();
                slotIncrease += 1;

                totalCoins -= slotIncrease_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Rerolls
        else if (upgradeSelected == 7 && totalCoins >= rerolls_price)
        {
            if (reroll_PURCHASED < reroll_MAX)
            {
                reroll_PURCHASED += 1;
                PurchaseSound();
                rerolls += 3;

                totalCoins -= rerolls_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Damage cooldown
        else if (upgradeSelected == 8 && totalCoins >= damagedCooldownIcrease_price)
        {
            if (damageCooldown_PURCHASED < damageCooldown_MAX)
            {
                damageCooldown_PURCHASED += 1;
                PurchaseSound();
                damagedCooldownIcrease += 0.5f;

                totalCoins -= damagedCooldownIcrease_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Upgrade choises
        else if (upgradeSelected == 9 && totalCoins >= extraUpgradeChoises_price)
        {
            if (moreChoises_PURCHASED < moreChoises_MAX)
            {
                moreChoises_PURCHASED += 1;
                PurchaseSound();
                extraUpgradeChoises += 1;

                totalCoins -= extraUpgradeChoises_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region On slime click/death chance increase
        else if (upgradeSelected == 10 && totalCoins >= onSlime_CD_ChanceIncrease_price)
        {
            if (onSlime_CD_PURCHASED < onSlime_CD_MAX)
            {
                onSlime_CD_PURCHASED += 1;
                PurchaseSound();
                onSlime_CD_ChanceIncrease += 1;

                totalCoins -= onSlime_CD_ChanceIncrease_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Overall damage
        else if (upgradeSelected == 11 && totalCoins >= damageIncrease_price)
        {
            if (damageIncrease_PURCHASED < damageIncrease_MAX)
            {
                damageIncrease_PURCHASED += 1;
                PurchaseSound();
                damageIncrease += 3;

                totalCoins -= damageIncrease_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Active tier
        else if (upgradeSelected == 12 && totalCoins >= activeTier_price)
        {
            if (activeTier_PURCHASED < activeTier_MAX)
            {
                activeTier_PURCHASED += 1;
                PurchaseSound();
                activeTier += 1;

                activeScript.ActiveVariables();

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

                totalCoins -= activeTier_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Slower slimes
        else if (upgradeSelected == 13 && totalCoins >= slowerSlimes_price)
        {
            if (slowerSlimes_PURCHASED < slowerSlimes_MAX)
            {
                slowerSlimes_PURCHASED += 1;
                PurchaseSound();
                slowerSlimes += 4;

                totalCoins -= slowerSlimes_price;
            }
            else { PlayError(); }
        }
        #endregion

        #region Slower slime bullets
        else if (upgradeSelected == 14 && totalCoins >= slowerBullets_price)
        {
            if (slowerBullets_PURCHASED < slowerBullets_MAX)
            {
                slowerBullets_PURCHASED += 1;
                PurchaseSound();
                slowerBullets += 5;

                totalCoins -= slowerBullets_price;
            }
            else { PlayError(); }
        }
        #endregion

        else
        {
            PlayError();
        }

        SetTexts(upgradeSelected);

        SetUpgradeMaxNumbers();
    }

    public TextMeshProUGUI coinChance_MAX_text, clickDamage_MAX_text, healthIncrease_MAX_text, crit_MAX_text, clickCooldown_MAX_text, regen_MAX_text, slots_MAX_text, reroll_MAX_text, damageCooldown_MAX_text, moreChoises_MAX_text, onSlime_CD_MAX_text, damageIncrease_MAX_text, activeTier_MAX_text, slowerSlimes_MAX_text, slowerBullets_MAX_text;
    public void SetUpgradeMaxNumbers()
    {
        coinChance_MAX_text.text = coinChance_PURCHASED + "/" + coinChance_MAX;
        clickDamage_MAX_text.text = clickDamage_PURCHASED + "/" + clickDamage_MAX;
        healthIncrease_MAX_text.text = healthIncrease_PURCHASED + "/" + healthIncrease_MAX;
        crit_MAX_text.text = crit_PURCHASED + "/" + crit_MAX;
        clickCooldown_MAX_text.text = clickCooldown_PURCHASED + "/" + clickCooldown_MAX;
        regen_MAX_text.text = regen_PURCHASED + "/" + regen_MAX;
        slots_MAX_text.text = slots_PURCHASED + "/" + slots_MAX;
        reroll_MAX_text.text = reroll_PURCHASED + "/" + reroll_MAX;
        damageCooldown_MAX_text.text = damageCooldown_PURCHASED + "/" + damageCooldown_MAX;
        moreChoises_MAX_text.text = moreChoises_PURCHASED + "/" + moreChoises_MAX;
        onSlime_CD_MAX_text.text = onSlime_CD_PURCHASED + "/" + onSlime_CD_MAX;
        damageIncrease_MAX_text.text = damageIncrease_PURCHASED + "/" + damageIncrease_MAX;
        activeTier_MAX_text.text = activeTier_PURCHASED + "/" + activeTier_MAX;
        slowerSlimes_MAX_text.text = slowerSlimes_PURCHASED + "/" + slowerSlimes_MAX;
        slowerBullets_MAX_text.text = slowerBullets_PURCHASED + "/" + slowerBullets_MAX;

        if (coinChance_PURCHASED == coinChance_MAX) { coinChance_MAX_text.color = Color.green; }
        if (clickDamage_PURCHASED == clickDamage_MAX) { clickDamage_MAX_text.color = Color.green; }
        if (healthIncrease_PURCHASED == healthIncrease_MAX) { healthIncrease_MAX_text.color = Color.green; }
        if (crit_PURCHASED == crit_MAX) { crit_MAX_text.color = Color.green; }
        if (clickCooldown_PURCHASED == clickCooldown_MAX) { clickCooldown_MAX_text.color = Color.green; }
        if (regen_PURCHASED == regen_MAX) { regen_MAX_text.color = Color.green; }
        if (slots_PURCHASED == slots_MAX) { slots_MAX_text.color = Color.green; }
        if (reroll_PURCHASED == reroll_MAX) { reroll_MAX_text.color = Color.green; }
        if (damageCooldown_PURCHASED == damageCooldown_MAX) { damageCooldown_MAX_text.color = Color.green; }
        if (moreChoises_PURCHASED == moreChoises_MAX) { moreChoises_MAX_text.color = Color.green; }
        if (onSlime_CD_PURCHASED == onSlime_CD_MAX) { onSlime_CD_MAX_text.color = Color.green; }
        if (damageIncrease_PURCHASED == damageIncrease_MAX) { damageIncrease_MAX_text.color = Color.green; }
        if (activeTier_PURCHASED == activeTier_MAX) { activeTier_MAX_text.color = Color.green; }
        if (slowerSlimes_PURCHASED == slowerSlimes_MAX) { slowerSlimes_MAX_text.color = Color.green; }
        if (slowerBullets_PURCHASED == slowerBullets_MAX) { slowerBullets_MAX_text.color = Color.green; }
    }
    #endregion

    public void PlayError()
    {
        audioManager.Play("Error");
    }

    public void PurchaseSound()
    {
        audioManager.Play("Purchase");
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        totalCoins = data.totalCoins;

        coinChance_PURCHASED = data.coinChance_PURCHASED;
        clickDamage_PURCHASED = data.clickDamage_PURCHASED;
        healthIncrease_PURCHASED = data.healthIncrease_PURCHASED;
        crit_PURCHASED = data.crit_PURCHASED;
        clickCooldown_PURCHASED = data.clickCooldown_PURCHASED;
        regen_PURCHASED = data.regen_PURCHASED;
        slots_PURCHASED = data.slots_PURCHASED;
        reroll_PURCHASED = data.reroll_PURCHASED;
        damageCooldown_PURCHASED = data.damageCooldown_PURCHASED;
        moreChoises_PURCHASED = data.moreChoises_PURCHASED;
        onSlime_CD_PURCHASED = data.onSlime_CD_PURCHASED;
        damageIncrease_PURCHASED = data.damageIncrease_PURCHASED;
        activeTier_PURCHASED = data.activeTier_PURCHASED;
        slowerSlimes_PURCHASED = data.slowerSlimes_PURCHASED;
        slowerBullets_PURCHASED = data.slowerBullets_PURCHASED;

        clickDamageIncrease = data.clickDamageIncrease;
        startHealth = data.startHealth;
        healEveryWave = data.healEveryWave;
        slotIncrease = data.slotIncrease;
        rerolls = data.rerolls;
        extraUpgradeChoises = data.extraUpgradeChoises;
        onSlime_CD_ChanceIncrease = data.onSlime_CD_ChanceIncrease;
        activeTier = data.activeTier;

        goldChanceIncrease = data.goldChanceIncrease;
        damageIncrease = data.damageIncrease;
        critChanceIncrease = data.critChanceIncrease;
        critIncreaseIncrease = data.critIncreaseIncrease;
        clickCooldownDecrease = data.clickCooldownDecrease;
        damagedCooldownIcrease = data.damagedCooldownIcrease;
        slowerSlimes = data.slowerSlimes;
        slowerBullets = data.slowerBullets;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.totalCoins = totalCoins;

        data.coinChance_PURCHASED = coinChance_PURCHASED;
        data.clickDamage_PURCHASED = clickDamage_PURCHASED;
        data.healthIncrease_PURCHASED = healthIncrease_PURCHASED;
        data.crit_PURCHASED = crit_PURCHASED;
        data.clickCooldown_PURCHASED = clickCooldown_PURCHASED;
        data.regen_PURCHASED = regen_PURCHASED;
        data.slots_PURCHASED = slots_PURCHASED;
        data.reroll_PURCHASED = reroll_PURCHASED;
        data.damageCooldown_PURCHASED = damageCooldown_PURCHASED;
        data.moreChoises_PURCHASED = moreChoises_PURCHASED;
        data.onSlime_CD_PURCHASED = onSlime_CD_PURCHASED;
        data.damageIncrease_PURCHASED = damageIncrease_PURCHASED;
        data.activeTier_PURCHASED = activeTier_PURCHASED;
        data.slowerSlimes_PURCHASED = slowerSlimes_PURCHASED;
        data.slowerBullets_PURCHASED = slowerBullets_PURCHASED;

        data.clickDamageIncrease = clickDamageIncrease;
        data.startHealth = startHealth;
        data.healEveryWave = healEveryWave;
        data.slotIncrease = slotIncrease;
        data.rerolls = rerolls;
        data.extraUpgradeChoises = extraUpgradeChoises;
        data.onSlime_CD_ChanceIncrease = onSlime_CD_ChanceIncrease;
        data.activeTier = activeTier;

        data.goldChanceIncrease = goldChanceIncrease;
        data.damageIncrease = damageIncrease;
        data.critChanceIncrease = critChanceIncrease;
        data.critIncreaseIncrease = critIncreaseIncrease;
        data.clickCooldownDecrease = clickCooldownDecrease;
        data.damagedCooldownIcrease = damagedCooldownIcrease;
        data.slowerSlimes = slowerSlimes;
        data.slowerBullets = slowerBullets;
    }
    #endregion
}
