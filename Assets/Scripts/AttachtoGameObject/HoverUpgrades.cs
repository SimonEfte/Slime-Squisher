using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HoverUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isUpgrade;

    public GameObject hoverUpgradeObject;
    public GameObject upgradeName, upgradeDesc;
    public TextMeshProUGUI upgradeNameText, upgradeDescText, levelText, rewardText;

    public bool isSelectGameMode, isSelectGameModeChallenges, isSelectActive, isActiveIcon;

    public GameObject hoverSelectGameMode;
    public TextMeshProUGUI selectedGameModeText;
    public LocalizationSCRIPT locScript;

    public Animation upgradeAnim;

    public TextMeshProUGUI activeName, activeDesctiption;

    public GameObject hoverGameModeAndActive;

    public AudioManager audioManager;
    public GameObject audioGameobject;

    public Button btn;

    public static int currentHoverLevel;

    public TextMeshProUGUI activePriceText;

    public bool isHoveringEasy, isHoveringNormal, isHoveringHard, isHoveringBullethell, isHoveringFlash, isHoveringFragile, isHoveringNarrow, isHoveringRampage;

    public bool isDice;

    private void Awake()
    {
        if (isUpgrade) { btn = gameObject.GetComponent<Button>(); }

        audioGameobject = GameObject.Find("AudioManager");
        audioManager = audioGameobject.GetComponent<AudioManager>();

        upgradeAnim = gameObject.GetComponent<Animation>();

        //hoverUpgradeObject = GameObject.Find("HoverIconUpgrade");

        if(isUpgrade == true)
        {
            upgradeName = GameObject.Find("UpgradeNameText");
            upgradeDesc = GameObject.Find("UpgradeDescriptionText");

            upgradeNameText = upgradeName.GetComponent<TextMeshProUGUI>();
            upgradeDescText = upgradeDesc.GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        if (isUpgrade)
        {
            btn.enabled = false;
            StartCoroutine(WaitForBtn());
        }
    }

    IEnumerator WaitForBtn()
    {
        yield return new WaitForSeconds(0.4f);
        btn.enabled = true;
    }

    #region check reward text
    public void CheckRewardText()
    {
        if(isSelectActive == true || isActiveIcon == true) { return; }
        if (DemoScript.isDemo == false)
        {
            bool isCompleted = false;

            if (isHoveringEasy == true)
            {
                if(SelectGameMode.easyCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.easyReward;
            }
            else if (isHoveringNormal == true)
            {
                if (SelectGameMode.normalCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.normalReward;
            }
            else if (isHoveringHard == true)
            {
                if (SelectGameMode.hardCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.hardReward;
            }
            else if (isHoveringBullethell == true)
            {
                if (SelectGameMode.bullethellCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.bullethellReward;
            }
            else if (isHoveringFlash == true)
            {
                if (SelectGameMode.flashCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.flashReward;
            }
            else if (isHoveringFragile == true && SelectGameMode.fragileCompleted == false)
            {
                if (SelectGameMode.fragileCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.fragileReward;
            }
            else if (isHoveringRampage == true && SelectGameMode.rampageCompleted == false)
            {
                if (SelectGameMode.rampageCompleted == false) { isCompleted = false; rewardText.gameObject.SetActive(true); }
                else { isCompleted = true; rewardText.gameObject.SetActive(false); }

                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.rampageReward;
            }

            if(isCompleted == true)
            {
                selectedGameModeText.gameObject.transform.localPosition = new Vector2(0, -312);
                rewardText.gameObject.SetActive(false);
            }
            else
            {
                selectedGameModeText.gameObject.transform.localPosition = new Vector2(0, -208);
                rewardText.gameObject.transform.localPosition = new Vector2(4, -306);
            }
        }
    }
    #endregion

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.Play("HoverUI_1");

        if (isSelectGameMode == true || isSelectGameModeChallenges == true || isSelectActive == true || isActiveIcon == true)
        {
            CheckRewardText();

            if (isActiveIcon == true)
            {
                if (upgradeAnim.isPlaying)
                {
                    upgradeAnim.Stop();
                }
                upgradeAnim.Play("IconHoverAnim");
            }

            #region Select game mode hover
           
            if (gameObject.name == "Normal")
            {
                selectedGameModeText.text = LocalizationSCRIPT.normalDescription;
            }
            if (gameObject.name == "Hard")
            {
                selectedGameModeText.text = LocalizationSCRIPT.hardDescription;
            }

            if (gameObject.name == "Chall_Bullethell")
            {
                selectedGameModeText.text = LocalizationSCRIPT.bulletHellDescription;
            }
            if (gameObject.name == "Chall_Flash")
            {
                selectedGameModeText.text = LocalizationSCRIPT.flahsDescription;
            }
            if (gameObject.name == "Chall_Fragile")
            {
                selectedGameModeText.text = LocalizationSCRIPT.fragileDescription;
            }
            if (gameObject.name == "Chall_Narrow")
            {
                selectedGameModeText.text = LocalizationSCRIPT.narrowDescription;
            }
            if (gameObject.name == "Chall_Cascade")
            {
                selectedGameModeText.text = LocalizationSCRIPT.rampageDescription;
            }

            if(DemoScript.isDemo == true && (isSelectGameMode == true || isSelectGameModeChallenges == true))
            {
                selectedGameModeText.text = LocalizationSCRIPT.notAviableInDemo;
            }

            if (gameObject.name == "Easy")
            {
                selectedGameModeText.text = LocalizationSCRIPT.easyDescription;
            }
            #endregion

            #region Select active
            if (gameObject.name == "DeathToSlimes")
            {
                activeName.text = LocalizationSCRIPT.deathToSlimes;
                activeDesctiption.text = LocalizationSCRIPT.deathToSlimes_des;
            }
            if (gameObject.name == "SharpClicks")
            {
                activeName.text = LocalizationSCRIPT.punchyClicks;
                activeDesctiption.text = LocalizationSCRIPT.punchyClicks_des;
                if(ActiveMechanics.isPunchyClicksUnlcoked == false && DemoScript.isDemo == false)
                {
                    activePriceText.text = "<color=yellow>" + ActiveMechanics.punchyClicksPrice.ToString("F0");
                    activePriceText.gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y + 0.82f);
                    activePriceText.gameObject.SetActive(true);
                }
            }
            if (gameObject.name == "Clover")
            {
                activeName.text = LocalizationSCRIPT.clover;
                activeDesctiption.text = LocalizationSCRIPT.clover_des;
                if (ActiveMechanics.isCloverUnlocked == false && DemoScript.isDemo == false)
                {
                    activePriceText.text = "<color=yellow>" + ActiveMechanics.cloverPrice.ToString("F0");
                    activePriceText.gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y + 0.82f);
                    activePriceText.gameObject.SetActive(true);

                }
            }
            if (gameObject.name == "Decoy")
            {
                activeName.text = LocalizationSCRIPT.decoy;
                activeDesctiption.text = LocalizationSCRIPT.decoy_des;
                if (ActiveMechanics.isDecoyUnlocked == false && DemoScript.isDemo == false)
                {
                    activePriceText.text = "<color=yellow>" + ActiveMechanics.decoyPrice.ToString("F0");
                    activePriceText.gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y + 0.82f);
                    activePriceText.gameObject.SetActive(true);
                }
            }
            if (gameObject.name == "ProjectileFrency")
            {
                activeName.text = LocalizationSCRIPT.frency;
                activeDesctiption.text = LocalizationSCRIPT.frency_des;
                if (ActiveMechanics.isProjectileFrenzyUnlocked == false && DemoScript.isDemo == false)
                {
                    activePriceText.text = "<color=yellow>" + ActiveMechanics.frenzyPrice.ToString("F0");
                    activePriceText.gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y + 0.82f);
                    activePriceText.gameObject.SetActive(true);
                }
            }
            if (gameObject.name == "AntiSlimeBullets")
            {
                activeName.text = LocalizationSCRIPT.antiSlime;
                activeDesctiption.text = LocalizationSCRIPT.antiSlime_des;
                if (ActiveMechanics.isAntiSlimeBulletsUnlocked == false && DemoScript.isDemo == false)
                {
                    activePriceText.text = "<color=yellow>" + ActiveMechanics.antiSlimeBulletPrice.ToString("F0");
                    activePriceText.gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y + 0.82f);
                    activePriceText.gameObject.SetActive(true);
                }
            }

            if (DemoScript.isDemo == true && isSelectActive && gameObject.name != "DeathToSlimes")
            {
                activeDesctiption.text = LocalizationSCRIPT.notAviableInDemo;
            }

            #endregion

            #region active and game mode animations
            hoverGameModeAndActive.transform.position = gameObject.transform.position;
            hoverGameModeAndActive.SetActive(true);

            if (isSelectActive == true)
            {
                Animation hoverAnim = activeName.GetComponent<Animation>();
                if (hoverAnim.isPlaying)
                {
                    hoverAnim.Stop();
                }
                hoverAnim.Play("IconHoverAnim2");

                Animation hoverAnim2 = activeDesctiption.GetComponent<Animation>();
                if (hoverAnim2.isPlaying)
                {
                    hoverAnim2.Stop();
                }
                hoverAnim2.Play("IconHoverAnim2");
            }

            if(isSelectGameMode == true || isSelectGameModeChallenges == true)
            {
                Animation hoverAnim2 = selectedGameModeText.GetComponent<Animation>();
                if (hoverAnim2.isPlaying)
                {
                    hoverAnim2.Stop();
                }
                hoverAnim2.Play("IconHoverAnim2");

                if(DemoScript.isDemo == false)
                {
                    Animation hoverAnim3 = rewardText.GetComponent<Animation>();
                    if (hoverAnim3.isPlaying)
                    {
                        hoverAnim3.Stop();
                    }
                    hoverAnim3.Play("IconHoverAnim2");
                }

                if (isSelectGameMode == true)
                {
                    if (upgradeAnim.isPlaying)
                    {
                        upgradeAnim.Stop();
                    }
                    upgradeAnim.Play("IconHoverAnim3");
                }

                if (isSelectGameModeChallenges == true)
                {
                    if (upgradeAnim.isPlaying)
                    {
                        upgradeAnim.Stop();
                    }
                    upgradeAnim.Play("IconHoverAnim3");
                }
            }
            #endregion
        }
        else
        {
            if(isDice == false)
            {
                hoverUpgradeObject.transform.localPosition = gameObject.transform.localPosition;
                hoverUpgradeObject.SetActive(true);
            }

            #region ANimation
            if (upgradeAnim.isPlaying)
            {
                upgradeAnim.Stop();
            }

            upgradeAnim.Play("IconHoverAnim");

            if(isDice == false)
            {
                Animation hoverAnim = hoverUpgradeObject.GetComponent<Animation>();
                if (hoverAnim.isPlaying)
                {
                    hoverAnim.Stop();
                }
                hoverAnim.Play("IconHoverAnim");
            }

            Animation nameTextAnim = upgradeNameText.GetComponent<Animation>();
            if (nameTextAnim.isPlaying)
            {
                nameTextAnim.Stop();
            }
            nameTextAnim.Play("IconHoverAnim2");

            Animation descTextAnim = upgradeDescText.GetComponent<Animation>();
            if (descTextAnim.isPlaying)
            {
                descTextAnim.Stop();
            }
            descTextAnim.Play("IconHoverAnim2");

            Animation levelTextAnim = levelText.GetComponent<Animation>();
            if (levelTextAnim.isPlaying)
            {
                levelTextAnim.Stop();
            }
            levelTextAnim.Play("IconHoverAnim2");
            #endregion


            #region Dice
            if (gameObject.name == "Dice" && MetaProgressionUpgrades.rerolls > 0)
            {
                locScript.SetUpgradeHoverText("dice");
                levelText.gameObject.SetActive(false);
            }
            else
            {
                levelText.gameObject.SetActive(true);
            }
            #endregion


            #region cooldown
            if (gameObject.name == "Upgrade_Cooldown")
            {
                locScript.SetUpgradeHoverText("cooldown");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.clickCooldownLevel + 1}";
                currentHoverLevel = PickUpgrade.clickCooldownLevel;
            }
            #endregion

            #region click damage
            else if (gameObject.name == "Upgrade_ClickDamage")
            {
                locScript.SetUpgradeHoverText("clickDamage");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.clickDamageLevel + 1}";
                currentHoverLevel = PickUpgrade.clickDamageLevel;
            }
            #endregion

            #region crit damage
            else if (gameObject.name == "Upgrade_CritDamage")
            {
                locScript.SetUpgradeHoverText("critDamage");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.critLevel + 1}";
                currentHoverLevel = PickUpgrade.critLevel;
            }
            #endregion

            #region health upgrade
            else if (gameObject.name == "Upgrade_HealthUpgrade")
            {
                locScript.SetUpgradeHoverText("health");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.healthLevel + 1}";
                currentHoverLevel = PickUpgrade.healthLevel;
            }
            #endregion

            #region all damage
            else if (gameObject.name == "Upgrade_IncreaseAllDamage")
            {
                locScript.SetUpgradeHoverText("allDamage");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.increaseAllDamageLevel + 1}";
                currentHoverLevel = PickUpgrade.increaseAllDamageLevel;
            }
            #endregion

            #region all chance
            else if (gameObject.name == "Upgrade_IncreaseAllChance")
            {
                locScript.SetUpgradeHoverText("allChance");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.totalChanceIncreaseLevel + 1}";
                currentHoverLevel = PickUpgrade.totalChanceIncreaseLevel;
            }
            #endregion

            #region cursor slash
            else if (gameObject.name == "Upgrade_CursorSlash")
            {
                locScript.SetUpgradeHoverText("cursorSlash");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.cursorSlashLevel + 1}";
                currentHoverLevel = PickUpgrade.cursorSlashLevel;
            }
            #endregion

            #region paper shot
            else if (gameObject.name == "Upgrade_PaperShot")
            {
                locScript.SetUpgradeHoverText("paperShot");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.paperShotLevel + 1}";
                currentHoverLevel = PickUpgrade.paperShotLevel;
            }
            #endregion

            #region arrow rain
            else if (gameObject.name == "Upgrade_ArrowRain")
            {
                locScript.SetUpgradeHoverText("arrowRain");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.arrowRainLevel + 1}";
                currentHoverLevel = PickUpgrade.arrowRainLevel;
            }
            #endregion

            #region knife orbital
            else if (gameObject.name == "Upgrade_KnifeOrbital")
            {
                locScript.SetUpgradeHoverText("knifeOrbital");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.knifeOrbitalLevel + 1}";
                currentHoverLevel = PickUpgrade.knifeOrbitalLevel;
            }
            #endregion

            #region scynthe
            else if (gameObject.name == "Upgrade_Scythe")
            {
                locScript.SetUpgradeHoverText("scythe");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.scytheLevel + 1}";
                currentHoverLevel = PickUpgrade.scytheLevel;
            }
            #endregion

            #region laser gun
            else if (gameObject.name == "Upgrade_LaserGun")
            {
                locScript.SetUpgradeHoverText("laserGun");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.laserGunLevel + 1}";
                currentHoverLevel = PickUpgrade.laserGunLevel;
            }
            #endregion

            #region sword
            else if (gameObject.name == "Upgrade_Sword")
            {
                locScript.SetUpgradeHoverText("sword");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.swordLevel + 1}";
                currentHoverLevel = PickUpgrade.swordLevel;
            }
            #endregion

            #region poison dart
            else if (gameObject.name == "Upgrade_PoisonDart")
            {
                locScript.SetUpgradeHoverText("poisonDart");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.poisonDartLevel + 1}";
                currentHoverLevel = PickUpgrade.poisonDartLevel;
            }
            #endregion
            
            #region strawberry shield
            else if (gameObject.name == "Upgrade_StrawberryShield")
            {
                locScript.SetUpgradeHoverText("strawberryShield");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.strawberryShieldLevel + 1}";
                currentHoverLevel = PickUpgrade.strawberryShieldLevel;
            }
            #endregion

            #region chain ball
            else if (gameObject.name == "Upgrade_ChainBall")
            {
                locScript.SetUpgradeHoverText("chainBall");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.chainBallLevel + 1}";
                currentHoverLevel = PickUpgrade.chainBallLevel;
            }
            #endregion

            #region thorn
            else if (gameObject.name == "Upgrade_Thorn")
            {
                locScript.SetUpgradeHoverText("Thorn");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.thornLevel + 1}";
                currentHoverLevel = PickUpgrade.thornLevel;
            }
            #endregion

            #region big laser
            else if (gameObject.name == "Upgrade_BigLaser")
            {
                locScript.SetUpgradeHoverText("BigLaser");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.bigLaserLevel + 1}";
                currentHoverLevel = PickUpgrade.bigLaserLevel;
            }
            #endregion

            #region boulder
            else if (gameObject.name == "Upgrade_Boulder")
            {
                locScript.SetUpgradeHoverText("Boulder");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.boulderLevel + 1}";
                currentHoverLevel = PickUpgrade.boulderLevel;
            }
            #endregion

            #region bouncyBall
            else if (gameObject.name == "Upgrade_BouncyBall")
            {
                locScript.SetUpgradeHoverText("BouncyBall");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.bouncyBallLevel + 1}";
                currentHoverLevel = PickUpgrade.bouncyBallLevel;
            }
            #endregion

            #region meteor
            else if (gameObject.name == "Upgrade_Meteor")
            {
                locScript.SetUpgradeHoverText("Meteor");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.meteorLevel + 1}";
                currentHoverLevel = PickUpgrade.meteorLevel;
            }
            #endregion

            #region stapler
            else if (gameObject.name == "Upgrade_Stapler")
            {
                locScript.SetUpgradeHoverText("Stapler");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.staplerLevel + 1}";
                currentHoverLevel = PickUpgrade.staplerLevel;
            }
            #endregion

            #region kunai
            else if (gameObject.name == "Upgrade_Kunai")
            {
                locScript.SetUpgradeHoverText("Kunai");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.kunaiLevel + 1}";
                currentHoverLevel = PickUpgrade.kunaiLevel;
            }
            #endregion

            #region spiky shield
            else if (gameObject.name == "Upgrade_SpikyShield")
            {
                locScript.SetUpgradeHoverText("SpikyShield");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.spikyShieldLevel + 1}";
                currentHoverLevel = PickUpgrade.spikyShieldLevel;
            }
            #endregion

            #region friendly bullets
            else if (gameObject.name == "Upgrade_FriendlyBullets")
            {
                locScript.SetUpgradeHoverText("FriendlyBullets");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.friendlyBulletsLevel + 1}";
                currentHoverLevel = PickUpgrade.friendlyBulletsLevel;
            }
            #endregion

            #region saw blade
            else if (gameObject.name == "Upgrade_Sawblade")
            {
                locScript.SetUpgradeHoverText("SawBlade");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.sawBladeLevel + 1}";
                currentHoverLevel = PickUpgrade.sawBladeLevel;
            }
            #endregion

            #region Katana
            else if (gameObject.name == "Upgrade_Katana")
            {
                locScript.SetUpgradeHoverText("Katana");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.katanaLevel + 1}";
                currentHoverLevel = PickUpgrade.katanaLevel;
            }
            #endregion

            #region Spike
            else if (gameObject.name == "Upgrade_Spike")
            {
                locScript.SetUpgradeHoverText("Spike");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.spikesLevel + 1}";
                currentHoverLevel = PickUpgrade.spikesLevel;
            }
            #endregion

            #region chain blade
            else if (gameObject.name == "Upgrade_ChainBlade")
            {
                locScript.SetUpgradeHoverText("chainBlade");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.bladeLevel + 1}";
                currentHoverLevel = PickUpgrade.bladeLevel;
            }
            #endregion

            #region nail gun
            else if (gameObject.name == "Upgrade_NailGun")
            {
                locScript.SetUpgradeHoverText("nailGun");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.nailGunLevel + 1}";
                currentHoverLevel = PickUpgrade.nailGunLevel;
            }
            #endregion

            #region bear trap
            else if (gameObject.name == "Upgrade_BearTrap")
            {
                locScript.SetUpgradeHoverText("BearTrap");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.bearTrapLevel + 1}";
                currentHoverLevel = PickUpgrade.bearTrapLevel;
            }
            #endregion

            #region spiky log
            else if (gameObject.name == "Upgrade_Log")
            {
                locScript.SetUpgradeHoverText("Log");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.logLevel + 1}";
                currentHoverLevel = PickUpgrade.logLevel;
            }
            #endregion

            #region legs
            else if (gameObject.name == "Upgrade_Legs")
            {
                locScript.SetUpgradeHoverText("Leg");
                levelText.text = $"{LocalizationSCRIPT.level} {PickUpgrade.legsLevel + 1}";
                currentHoverLevel = PickUpgrade.legsLevel;
            }
            #endregion

            //LocalizationSCRIPT.hoverName = gameObject.name;

            upgradeNameText.text = LocalizationSCRIPT.upgradeHoverName;
            upgradeDescText.text = LocalizationSCRIPT.upgradeHoverDesc;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelectGameMode == true || isSelectGameModeChallenges == true || isSelectActive == true || isActiveIcon == true)
        {
            hoverGameModeAndActive.SetActive(false);
        }

        if(isSelectGameMode || isSelectGameModeChallenges)
        {
            CheckRewardText();

            if (SelectGameMode.choseEasy == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.easyDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.easyReward;
            }
            if (SelectGameMode.choseNormal == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.normalDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.normalReward;
            }
            if (SelectGameMode.choseHard == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.hardDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.hardReward;
            }
            if (SelectGameMode.choseBullethell == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.bulletHellDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.bullethellReward;
            }
            if (SelectGameMode.choseFlash == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.flahsDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.flashReward;
            }
            if (SelectGameMode.choseFragile == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.fragileDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.fragileReward;
            }
            if (SelectGameMode.choseNarrow == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.narrowDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.narrowReward;
            }
            if (SelectGameMode.choseRampage == true)
            {
                selectedGameModeText.text = $"{LocalizationSCRIPT.rampageDescription} {LocalizationSCRIPT.SELECTED}";
                rewardText.text = LocalizationSCRIPT.reward + "<color=yellow>" + SelectGameMode.rampageReward;
            }
        }

        if (isSelectActive)
        {
            activePriceText.gameObject.SetActive(false);
            if (ActiveMechanics.choseDeathToSlimes == true)
            {
                activeName.text = $"{LocalizationSCRIPT.deathToSlimes} {LocalizationSCRIPT.SELECTED}";
                activeDesctiption.text = $"{LocalizationSCRIPT.deathToSlimes_des}";
            }
            if (ActiveMechanics.chosePunchyClicks == true)
            {
                activeName.text = $"{LocalizationSCRIPT.punchyClicks} {LocalizationSCRIPT.SELECTED}";
                activeDesctiption.text = $"{LocalizationSCRIPT.punchyClicks_des}";
            }
            if (ActiveMechanics.choseClover == true)
            {
                activeName.text = $"{LocalizationSCRIPT.clover} {LocalizationSCRIPT.SELECTED}";
                activeDesctiption.text = $"{LocalizationSCRIPT.clover_des}";
            }
            if (ActiveMechanics.choseDecoy == true)
            {
                activeName.text = $"{LocalizationSCRIPT.decoy} {LocalizationSCRIPT.SELECTED}";
                activeDesctiption.text = $"{LocalizationSCRIPT.decoy_des}";
            }
            if (ActiveMechanics.choseProjectileFrenzy == true)
            {
                activeName.text = $"{LocalizationSCRIPT.frency} {LocalizationSCRIPT.SELECTED}";
                activeDesctiption.text = $"{LocalizationSCRIPT.frency_des}";
            }
            if (ActiveMechanics.choseAntiSlime == true)
            {
                activeName.text = $"{LocalizationSCRIPT.antiSlime} {LocalizationSCRIPT.SELECTED}";
                activeDesctiption.text = $"{LocalizationSCRIPT.antiSlime_des}";
            }
        }
    }
}
