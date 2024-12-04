using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpgrade : MonoBehaviour
{
    public static int coinsThisRound;
    public static int totalCoins;
    public TextMeshProUGUI coinThisRoundText;
    public TextMeshProUGUI currentWaveText;

    private void Awake()
    {
        clickDamage = 1;
        clickCooldown = 0.5f;
    }

    public bool spawnedRandomUpgrade;
    public TextMeshProUGUI upgradeNameText, upgradeDescText;
    public GameObject darkUpgrade, hoverUpgradeObject;

    private void Update()
    {
        coinThisRoundText.text = coinsThisRound.ToString("F0");

        if (SpawnSlimes.slimesSquished >= SpawnSlimes.slimesWaveSpawnCount)
        {
            if(spawnedRandomUpgrade == false)
            {
                hoverUpgradeObject.SetActive(true);
                darkUpgrade.SetActive(true);
                upgradeNameText.gameObject.SetActive(true);
                upgradeDescText.gameObject.SetActive(true);
                upgradeNameText.text = ""; upgradeDescText.text = "";

                upgradesSpawned = 0;
                upgrade1Number = 0; upgrade2Number = 0; upgrade3Number = 0;

                SpawnRandomUpgrade();
                SpawnRandomUpgrade();
                SpawnRandomUpgrade();
                spawnedRandomUpgrade = true;
            }
        }

        if(choseUpgrade == true)
        {
            ChooseUpgrade();
        }
    }

    #region Spawn random upgrade
    public GameObject pos1, pos2, pos3;
    public GameObject clickUpgrade, cooldownUpgrade, cursorSlashUpgrade, critDamageUpgrade, healthUpgrade, paperShotUpgrade;
    public int upgrade1Number, upgrade2Number, upgrade3Number;

    public void SpawnRandomUpgrade()
    {
        int randomUpgrade;

        do
        {
            randomUpgrade = Random.Range(1, 7);
        } while (randomUpgrade == upgrade1Number || randomUpgrade == upgrade2Number);

        if (upgradesSpawned == 0) { upgrade1Number = randomUpgrade; }
        if (upgradesSpawned == 1) { upgrade2Number = randomUpgrade; }
        if (upgradesSpawned == 2) { upgrade3Number = randomUpgrade; }

        if (randomUpgrade == 1) { SetUpgradePos(clickUpgrade); }
        if (randomUpgrade == 2) { SetUpgradePos(cooldownUpgrade); }
        if (randomUpgrade == 3) { SetUpgradePos(cursorSlashUpgrade); }
        if (randomUpgrade == 4) { SetUpgradePos(critDamageUpgrade); }
        if (randomUpgrade == 5) { SetUpgradePos(healthUpgrade); }
        if (randomUpgrade == 6) { SetUpgradePos(paperShotUpgrade); }
    }

    public int upgradesSpawned;

    public void SetUpgradePos(GameObject upgrade)
    {
        upgradesSpawned += 1;

        upgrade.SetActive(true);

        if (upgradesSpawned == 1) { upgrade.transform.localPosition = pos1.transform.localPosition; }
        if (upgradesSpawned == 2) { upgrade.transform.localPosition = pos2.transform.localPosition; }
        if (upgradesSpawned == 3) { upgrade.transform.localPosition = pos3.transform.localPosition; }
    }
    #endregion

    #region Choose upgrade
    public void ChooseUpgrade()
    {
        if(SpawnSlimes.slimeWave == 1) 
        {
            SpawnSlimes.slimeSpeed += 0.1f;
            SpawnSlimes.slimeSpawnTime -= 0.1f;
        }
        if (SpawnSlimes.slimeWave == 2)
        {
            SpawnSlimes.slimeHealth += 0.5f;
        }
        if (SpawnSlimes.slimeWave == 3)
        {
            SpawnSlimes.slimeSpeed += 0.3f;
            SpawnSlimes.slimeSpawnTime -= 0.2f;
        }
        if (SpawnSlimes.slimeWave == 4)
        {
            SpawnSlimes.slimeHealth += 0.5f;
            SpawnSlimes.slimeSpawnTime -= 0.3f;
        }
        if (SpawnSlimes.slimeWave > 4)
        {
            SpawnSlimes.slimeSpeed += 0.05f;
            SpawnSlimes.slimeSpawnTime -= 0.05f;
        }

        SpawnSlimes.slimeWave += 1;
        currentWaveText.text = "Wave " + SpawnSlimes.slimeWave;

        SpawnSlimes.slimesWaveSpawnCount += 4;

        SpawnSlimes.slimesSpawned = 0;
        SpawnSlimes.slimesSquished = 0;

        hoverUpgradeObject.SetActive(false);
        darkUpgrade.SetActive(false);
        upgradeNameText.gameObject.SetActive(false);
        upgradeDescText.gameObject.SetActive(false);
        choseUpgrade = false;

        clickUpgrade.SetActive(false); cooldownUpgrade.SetActive(false); cursorSlashUpgrade.SetActive(false);
        critDamageUpgrade.SetActive(false); healthUpgrade.SetActive(false); paperShotUpgrade.SetActive(false);

        SpawnSlimes.isWaveCompleted = false;
        SpawnSlimes.allSlimesSpawned = false;
       
        spawnedRandomUpgrade = false;
    }
    #endregion

    public static bool choseUpgrade;

    #region Stronger clicks
    public static float clickDamage;
    public void Upgrade_StrongerClicks()
    {
        clickDamage += 0.5f;
        choseUpgrade = true;
    }
    #endregion

    #region Click cooldown
    public static float clickCooldown;

    public void Upgrade_ClickCooldown()
    {
        choseUpgrade = true;
        clickCooldown -= 0.05f;
        if(clickCooldown < 0.1f) { clickCooldown = 0.05f; }
    }
    #endregion

    #region Cursor slash
    public static bool haveCursorSlash;
    public void Upgrade_CursorSlash()
    {
        haveCursorSlash = true;
        choseUpgrade = true;
    }
    #endregion

    #region Crit damage
    public void UpgradeCritDamage()
    {
        choseUpgrade = true;
    }
    #endregion

    #region Health upgrade
    public void Upgrade_StrawberryHealth()
    {
        choseUpgrade = true;
    }
    #endregion

    #region PaperShot
    public void Upgrade_PaperShot()
    {
        choseUpgrade = true;
    }
    #endregion

}
