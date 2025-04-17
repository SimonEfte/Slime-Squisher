using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using TMPro;

public class LocalizationSCRIPT : MonoBehaviour
{
    public static int languageSelected;

    public GameObject englishFlag, germanFlag, japaneseFlag, frenchFlag, spanishFlag, chineseFlag, koreanFlag, russianFlag, polishFlag, portugeseFlag;
    public AudioManager audioManager;

    private void Awake()
    {
       
    }

    private void Start()
    {
        if (DemoScript.isLocalizationDone == true)
        {
            languageSelected = PlayerPrefs.GetInt("language");

            if (!PlayerPrefs.HasKey("language"))
            {
                CultureInfo userCulture = CultureInfo.CurrentCulture;
                RegionInfo region = new RegionInfo(userCulture.Name);

                // Use the region name to set the language
                string regionName = region.Name;

                SetLanguageByRegion(regionName);
            }
            else
            {
                SelectLanguage(false);
            }
        }
        else
        {
            languageSelected = 1;
            englishFlag.GetComponent<Button>().onClick.Invoke();
        }
    }

    #region Check country
    void SetLanguageByRegion(string regionName)
    {
        switch (regionName)
        {
            case "US":
            case "CA":
                EnglishLanguage();
                break;
            case "RU":
                RussianLanguage();
                break;
            case "JP":
                JapaneseLanguage();
                break;
            case "KR":
                KoreanLanguage();
                break;
            case "CN":
                ChineseLanguage();
                break;
            case "DE":
                GermanLanguage();
                break;
            case "FR":
                FrenchLanguage();
                break;
            case "ES":
                SpanishLanguage();
                break;
            case "PT":
            case "BR":  // For Brazil
                PortugeseLanguage();
                break;
            case "PL":
                PolishLanguage();
                break;

            default:
                // Set a default language if none of the cases match
                EnglishLanguage();
                break;
        }
    }
    #endregion

    #region select language
    public void SelectLanguage(bool change)
    {
        englishFlag.SetActive(false); germanFlag.SetActive(false); japaneseFlag.SetActive(false);
        frenchFlag.SetActive(false); spanishFlag.SetActive(false); chineseFlag.SetActive(false);
        koreanFlag.SetActive(false); russianFlag.SetActive(false); polishFlag.SetActive(false);
        portugeseFlag.SetActive(false);

        if(change == true) { languageSelected += 1; audioManager.Play("Ui_click1"); }
     
        if(languageSelected == 2) { GermanLanguage(); germanFlag.SetActive(true); }
        if (languageSelected == 3) { JapaneseLanguage(); japaneseFlag.SetActive(true); }
        if (languageSelected == 4) { FrenchLanguage(); frenchFlag.SetActive(true); }
        if (languageSelected == 5) { SpanishLanguage(); spanishFlag.SetActive(true); }
        if (languageSelected == 6) { ChineseLanguage(); chineseFlag.SetActive(true); }
        if (languageSelected == 7) { KoreanLanguage(); koreanFlag.SetActive(true); }
        if (languageSelected == 8) { RussianLanguage(); russianFlag.SetActive(true); }
        if (languageSelected == 9) { PolishLanguage(); polishFlag.SetActive(true); }
        if (languageSelected == 10) { PortugeseLanguage(); portugeseFlag.SetActive(true); }
        if (languageSelected == 11) { EnglishLanguage(); languageSelected = 1; englishFlag.SetActive(true); }

        if(languageSelected == 1) { EnglishLanguage(); englishFlag.SetActive(true); }

        PlayerPrefs.SetInt("language", languageSelected);
    }
    #endregion

    //Misc
    public static string wave, crit, close, back, reserCurrentRun, backToMainMenu, notAviableInDemo, SELECTED, level, lvl, youReachedWave, fullscreen, ON, OFF, background, reward;

    //Gamemodes strings
    public static string easyDescription, normalDescription, hardDescription, bulletHellDescription, flahsDescription, fragileDescription, narrowDescription, rampageDescription;

    //Active strings
    public static string deathToSlimes, deathToSlimes_des, punchyClicks, punchyClicks_des, clover, clover_des, decoy, decoy_des, frency, frency_des, antiSlime, antiSlime_des;

    //New strings
    public static string available, max, easy, normal, hard, bullethell, flash, fragile, rampage, waveReached, waveToBeat, timeReached, timeToReach, price, sec;

    //Shop names
    public static string selectAnUpgrade, coinDropChance, startClickDamage, startHealth, critChanceAndIncrease, clickCooldown, healHalfAHeart, waves, moreUpgradeSlots, rerolls, invincibilityAfterDamage, moreUpgradEChoises, onSlimeClickDeathChance, allDamageIncrease, betterActive, tier, slowerSlimes, slowerBullets;

    public GameObject tutLeftArrow, tutRightArrow;

    #region English
    public void EnglishLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(100, 0);
        tutRightArrow.transform.localPosition = new Vector2(-100, 0);

        languageSelected = 1; PlayerPrefs.SetInt("language", languageSelected);
        englishFlag.GetComponent<Button>().onClick.Invoke();

        wave = "wave";
        crit = "CRIT!"; 
        close = "close"; 
        back = "back";
        reserCurrentRun = "would you like to reset your current run?"; 
        backToMainMenu = "back to the main menu? this will end your current run";
        notAviableInDemo = "not avaiable in the demo"; 
        SELECTED = "(SELECTED)";
        level = "level"; 
        lvl = "lvl"; 
        fullscreen = "fullscreen";
        ON = "(ON)";
        OFF = "(OFF)";
        background = "background";

        // Gamemodes strings
        easyDescription = $"The standard gamemode: Easy difficulty\nSurvive {SelectGameMode.easy_waveToReach} waves"; 
        normalDescription = $"The standard gamemode: Normal difficulty. Some slimes are faster. Slime bullets travel faster.\nSurvive {SelectGameMode.normal_waveToReach} waves"; 
        hardDescription = $"The standard gamemode: Hard difficulty. All slimes are faster, shooting slimes shoot more often and the strawberry takes 1 full heart when damaged.\nSurvive {SelectGameMode.hard_waveToReach} waves"; 
        bulletHellDescription = $"Bullet hell challenge: Only shooting slimes will spawn. Bullets travel at a slower pace. Upgrades that block bullet have a higher chance to appear.\nSurvive {SelectGameMode.bullethell_waveToReach} waves";
        flahsDescription = $"Flash challenge: Only fast slimes will spawn. Upgrades that slows slimes down have a higher chance of appearing.\nSurvive {SelectGameMode.flash_waveToReach} waves"; 

        //The fragile desc needs to change on all languages
        fragileDescription = $"Fragile challenge: The strawberry dies in 1 hit. The health upgrade will not appear.\nSurvive {SelectGameMode.fragile_waveToReach} waves"; 

        narrowDescription = $"Narrow challenge: The game area is smaller.\nSurvive {SelectGameMode.narrow_waveToReach} waves"; 
        rampageDescription = $"Rampage challenge: There is only 1 long wave. Upgrades will appear every 20 seconds but the game will not be paused (slime bullets will stop).\nSurvive {SelectGameMode.rampage_MinuteToReach} minutes";

        // Active strings
        deathToSlimes = "DEATH TO SLIMES"; 
        deathToSlimes_des = $"instantly kills {ActiveMechanics.deathToSlimes_killAmount} random slimes. {ActiveMechanics.deathToSlimes_WaveRecharge} wave recharge";
        punchyClicks = "PUNCHY CLICKS"; 
        punchyClicks_des = $"all clicks are critical clicks and click cooldown time is reduced to {ActiveMechanics.sharpClicksTimeInterval} second for {ActiveMechanics.sharpClicksTimer} seconds. {ActiveMechanics.sharpClicks_WaveRecharge} wave recharge";
        clover = "CLOVER";
        clover_des = $"all \"on slime click\" and \"on slime death\" upgrades have a 50% chance to trigger for the next {ActiveMechanics.cloverTimer} seconds. {ActiveMechanics.clover_waveRecharge} wave recharge";
        decoy = "STRAWBERRY DECOY"; 
        decoy_des = $"place down a strawberry decoy. all slimes will target the decoy. the decoy lasts {ActiveMechanics.decoyWaveHealth} waves unless it dies by taking damage once. {ActiveMechanics.decoy_waveRecharge} wave recharge";
        frency = "PROJECTILE FRENCY"; 
        frency_des = $"shoot {ActiveMechanics.projectileFrencyProjectiles} random projectiles from the cursor for {ActiveMechanics.projectileFrencyTime} seconds. {ActiveMechanics.projectileFrency_waveRecharge} wave recharge"; 

        if(MobileScript.isMobile == true)
        {
            frency_des = $"shoot {ActiveMechanics.projectileFrencyProjectiles} random projectiles from the strawberry for {ActiveMechanics.projectileFrencyTime} seconds. {ActiveMechanics.projectileFrency_waveRecharge} wave recharge";
        }

        antiSlime = "ANTI SLIME BULLETS";
        antiSlime_des = $"shoot {ActiveMechanics.antiSlimeBulletCount} anti slime bullets from the strawberry. each bullet deals {ActiveMechanics.antiSlimeDamage} damage and has a {ActiveMechanics.antiBulletDeathChance}% chance of insta killing an enemy (except for bosses). {ActiveMechanics.antiSlime_waveRecharge} wave recharge ";

        //new text for full game
        reward = "Reward: ";
        available = "Available: ";
        selectAnUpgrade = "Select an upgrade";
        coinDropChance = "Gold coin drop chance";
        startClickDamage = "Start click damage";
        startHealth = "Start health";
        critChanceAndIncrease = "Crit chance and crit increase";
        clickCooldown = "Start click cooldown";
        healHalfAHeart = "Heal half a heart every";
        waves = "waves";
        moreUpgradeSlots = "More upgrade slots";
        rerolls = "Rerolls";
        invincibilityAfterDamage = "Invincibility after damaged";
        moreUpgradEChoises = "More upgrade choises";
        onSlimeClickDeathChance = "On slime click/death trigger chance";
        allDamageIncrease = "All damage increase";
        betterActive = "Better active";
        tier = "Tier";
        slowerSlimes = "Slower slimes";
        slowerBullets = "Slower slime bullets";
        max = "MAX";
        sec = "sec";
        easy = "easy";
        normal = "normal";
        hard = "hard";
        bullethell = "bullethell";
        flash = "flash";
        fragile = "fragile";
        rampage = "rampage";
        waveReached = "wave reached:";
        waveToBeat = "wave to beat:";
        timeReached = "time reached:";
        timeToReach = "time to reach:";
        price = "Price: ";

        ChangeText();
        englishFlag.SetActive(true);
    }
    #endregion

    #region German
    public void GermanLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(100, 0);
        tutRightArrow.transform.localPosition = new Vector2(-100, 0);

        languageSelected = 2; PlayerPrefs.SetInt("language", languageSelected);
        germanFlag.GetComponent<Button>().onClick.Invoke();

        wave = "welle ";
        crit = "KRIT!";
        close = "Schließen";
        back = "Zurück";
        reserCurrentRun = "Möchtest du deinen aktuellen Run zurücksetzen?";
        backToMainMenu = "Zurück zum Hauptmenü? Dies wird deinen aktuellen Run beenden.";
        notAviableInDemo = "In der Demo nicht verfügbar";
        SELECTED = "(AUSGEWÄHLT)";
        level = "level";
        lvl = "lvl";
        fullscreen = "Vollbild";
        ON = "(an)";
        OFF = "(aus)";
        background = "Hintergrund";

        // Gamemodes strings
        easyDescription = $"Der Standard-Spielmodus: Leichter Schwierigkeitsgrad\nÜberlebe {SelectGameMode.easy_waveToReach} Wellen";
        normalDescription = $"Der Standard-Spielmodus: Normaler Schwierigkeitsgrad. Manche Slimes sind schneller. Slime-Projektile fliegen schneller.\nnÜberlebe {SelectGameMode.normal_waveToReach} Wellen";
        hardDescription = $"Der Standard-Spielmodus: Hoher Schwierigkeitsgrad. Alle Slimes sind schneller, schießende Slimes schießen häufiger und die Erdbeere verliert 1 ganzes Herz, wenn sie Schaden nimmt.\nÜberlebe {SelectGameMode.hard_waveToReach} Wellen";
        bulletHellDescription = $"Bullet-Hell-Herausforderung: Es erscheinen nur schießende Slimes. Projektile bewegen sich langsamer. Upgrades, die Projektile blocken, haben eine höhere Erscheinungswahrscheinlichkeit.\nÜberlebe {SelectGameMode.bullethell_waveToReach} Wellen";
        flahsDescription = $"Blitz-Herausforderung: Es erscheinen nur schnelle Slimes. Upgrades, die Slimes verlangsamen, haben eine höhere Erscheinungswahrscheinlichkeit.\nÜberlebe {SelectGameMode.flash_waveToReach} Wellen";
        fragileDescription = $"Zerbrechlichkeits-Herausforderung: Die Erdbeere stirbt bei 1 Treffer. Das Gesundheits-Upgrade wird nicht erscheinen.\nÜberlebe {SelectGameMode.fragile_waveToReach} Wellen";
        narrowDescription = $"Enge Herausforderung: Der Spielbereich ist kleiner.\nÜberlebe {SelectGameMode.narrow_waveToReach} Wellen";
        rampageDescription = $"Randale-Herausforderung: Es gibt nur 1 lange Welle. Alle 20 Sekunden erscheinen Upgrades, aber das Spiel wird nicht angehalten (Slime-Projektile hören auf).\nÜberlebe {SelectGameMode.rampage_MinuteToReach} Minuten.";

        // Active strings
        deathToSlimes = "TOD DEN SCHLEIMEN";
        deathToSlimes_des = $"Tötet sofort {ActiveMechanics.deathToSlimes_killAmount} zufällige Slimes. {ActiveMechanics.deathToSlimes_WaveRecharge}-Wellen-Abklingzeit.";
        punchyClicks = "SCHLAGFERTIGE KLICKS";
        punchyClicks_des = $"Alle Klicks sind kritische Klicks und die Klick-Abklingzeit wird für {ActiveMechanics.sharpClicksTimeInterval} Sekunden auf {ActiveMechanics.sharpClicksTimer} Sekunden reduziert. {ActiveMechanics.sharpClicks_WaveRecharge}-Wellen-Abklingzeit.";
        clover = "KLEEBLATT";
        clover_des = $"alle Upgrades „bei Slime-Klick“ und „bei Slime-Tod“ haben eine 50%ige Chance, in den nächsten {ActiveMechanics.cloverTimer} Sekunden ausgelöst zu werden. {ActiveMechanics.clover_waveRecharge}-Wellen-Abklingzeit.";
        decoy = "ERDBEEREN-KÖDER";
        decoy_des = $"Lege einen Erdbeeren-Köder aus. Alle Slimes greifen den Köder an. Der Köder hält {ActiveMechanics.decoyWaveHealth} Wellen lang, es sei denn, er nimmt einmal Schaden und stirbt. {ActiveMechanics.decoy_waveRecharge}-Wellen-Abklingzeit.";
        frency = "PROJEKTIL-WAHNSINN";
        frency_des = $"Feuert {ActiveMechanics.projectileFrencyTime} Sekunden lang {ActiveMechanics.projectileFrencyProjectiles} zufällige Projektile vom Cursor ab. {ActiveMechanics.projectileFrency_waveRecharge}-Wellen-Abklingzeit.";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"Feuert {ActiveMechanics.projectileFrencyTime} Sekunden lang {ActiveMechanics.projectileFrencyProjectiles} zufällige Projektile aus der Erdbeere ab. {ActiveMechanics.projectileFrency_waveRecharge}-Wellen-Abklingzeit.";
        }

        antiSlime = "ANTI-SCHLEIM-PROJEKTILE";
        antiSlime_des = $"Feuert {ActiveMechanics.antiSlimeBulletCount} Anti-Slime-Projektile von der Erdbeere ab. Jedes Projektil verursacht {ActiveMechanics.antiSlimeDamage} Schaden und hat eine Chance von {ActiveMechanics.antiBulletDeathChance}% , einen Gegner (außer Bosse) sofort zu töten. {ActiveMechanics.antiSlime_waveRecharge}-Wellen-Abklingzeit.";

        //new text for full game
        reward = "Belohnung: ";
        available = "Verfügbar: ";
        selectAnUpgrade = "Wähle ein Upgrade";
        coinDropChance = "Chance, Goldmünzen fallen zu lassen";
        startClickDamage = "Start-Klickschaden";
        startHealth = "Start-Gesundheit";
        critChanceAndIncrease = "Kritische Chance und kritische Erhöhung";
        clickCooldown = "Start-Klick-Abklingzeit";
        healHalfAHeart = "Heilt jedes Mal ein halbes Herz";
        waves = "Wellen";
        moreUpgradeSlots = "Mehr Upgrade-Slots";
        rerolls = "Rerolls";
        invincibilityAfterDamage = "Unbesiegbarkeit nach Schaden";
        moreUpgradEChoises = "Mehr Upgrade-Möglichkeiten";
        onSlimeClickDeathChance = "Bei Slime-Klick/Todes-Auslöser-Chance";
        allDamageIncrease = "Erhöhung des gesamten Schadens";
        betterActive = "Besser Aktiv";
        tier = "Stufe";
        slowerSlimes = "Langsamere Slimes";
        slowerBullets = "Langsamere Slime-Geschosse";
        max = "MAX";
        sec = "s";
        easy = "Leicht";
        normal = "normal";
        hard = "Schwer";
        bullethell = "bullethell";
        flash = "flash";
        fragile = "Zerbrechlich";
        rampage = "Amoklauf";
        waveReached = "Welle erreicht:";
        waveToBeat = "Bevorstehende Wellen:";
        timeReached = "Zeit erreicht:";
        timeToReach = "Zielzeit:";
        price = "Preis: ";

        ChangeText();
        germanFlag.SetActive(true);
    }
    #endregion

    #region Japanese
    public void JapaneseLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(156, 0);
        tutRightArrow.transform.localPosition = new Vector2(-156, 0);

        languageSelected = 3; PlayerPrefs.SetInt("language", languageSelected);
        japaneseFlag.GetComponent<Button>().onClick.Invoke();

        wave = "ウェーブ";
        crit = "クリティカル";
        close = "閉じる";
        back = "戻る";
        reserCurrentRun = "現在のランをリセットしますか？";
        backToMainMenu = "メインメニューに戻りますか？現在のランが終了します。";
        notAviableInDemo = "デモでは使用できません";
        SELECTED = "(選択)";
        level = "レベル";
        lvl = "レベル";
        fullscreen = "フルスクリーン";
        ON = "(オン)";
        OFF = "(オフ)";
        background = "バックグラウンド";

        // Gamemodes strings
        easyDescription = $"スタンダードゲームモード: 難易度はイージー\n{SelectGameMode.easy_waveToReach}ウェーブを生き残る";
        normalDescription = $"スタンダードゲームモード：難易度はノーマル。一部のスライムの移動速度が上昇し、スライムの弾丸も高速化する。\n {SelectGameMode.normal_waveToReach}ウェーブを生き残る";
        hardDescription = $"スタンダードゲームモード：難易度はハード。全てのスライムの移動速度が上昇し、シューティングスライムの攻撃頻度も増加する。いちごがダメージを受けると、ハートを1つ失う。\n{SelectGameMode.hard_waveToReach}ウェーブを生き残る";
        bulletHellDescription = $"バレットヘルチャレンジ：シューティングスライムのみが出現。弾の速度が低下し、弾を防ぐアップグレードの出現確率が上昇する。\n {SelectGameMode.bullethell_waveToReach}ウェーブを生き延びよう。";
        flahsDescription = $"フラッシュチャレンジ：高速スライムのみが出現。スライムの速度を遅くするアップグレードの出現確率が上昇する。\n {SelectGameMode.flash_waveToReach}ウェーブを生き延びよう。";
        fragileDescription = $"フラジャイルチャレンジ:イチゴは1発で死亡！ 体力アップグレードはなし。\n{SelectGameMode.fragile_waveToReach}ウェーブ生き延びろ！";
        narrowDescription = $"ナロウチャレンジ：ゲームエリアが狭まる。\n{SelectGameMode.narrow_waveToReach} ウェーブを生き延びよう。";
        rampageDescription = $"ランペイジチャレンジ：1つの長いウェーブのみが続く。アップグレードは20秒ごとに出現するが、ゲームは一時停止しない（スライムの弾は停止する）。\n{SelectGameMode.rampage_MinuteToReach}分間生き延びよう";

        // Active strings
        deathToSlimes = "スライムの死";
        deathToSlimes_des = $"ランダムなスライム{ActiveMechanics.deathToSlimes_killAmount}匹を即死させる。{ActiveMechanics.deathToSlimes_WaveRecharge}ウェーブごとにリチャージ。";
        punchyClicks = "パンチクリック";
        punchyClicks_des = $"全てのクリックがクリティカルクリックとなり、クリックのリチャージ時間が3秒間{ActiveMechanics.sharpClicksTimeInterval}秒に短縮される。 {ActiveMechanics.sharpClicksTimer} {ActiveMechanics.sharpClicks_WaveRecharge}ウェーブごとにリチャージ。";
        clover = "クローバー";
        clover_des = $"「スライムクリック時」「スライム撃破時」発動系アップグレードが、次の{ActiveMechanics.cloverTimer}の確率で発動。{ActiveMechanics.clover_waveRecharge}ウェーブごとにリチャージ。";
        decoy = "ストロベリーデコイ";
        decoy_des = $"全てのスライムはデコイを狙う。デコイは1回ダメージを受けると、死なない限り{ActiveMechanics.decoyWaveHealth}ウェーブ続く。{ActiveMechanics.decoy_waveRecharge}ウェーブごとにリチャージ。";
        frency = "弾幕フィーバー";
        frency_des = $"{ActiveMechanics.projectileFrencyTime}秒間、カーソルからランダムに、弾を {ActiveMechanics.projectileFrencyProjectiles}発発射する。{ActiveMechanics.projectileFrency_waveRecharge}ウェーブごとにリチャージ。";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"{ActiveMechanics.projectileFrencyTime}秒間、いちごからランダムに、弾を {ActiveMechanics.projectileFrencyProjectiles}発発射する。{ActiveMechanics.projectileFrency_waveRecharge}ウェーブごとにリチャージ。";
        }

        antiSlime = "対スライム弾";
        antiSlime_des = $"いちごから{ActiveMechanics.antiSlimeBulletCount}発の対スライム弾を発射する。各弾は {ActiveMechanics.antiSlimeDamage}ダメージを与え、{ActiveMechanics.antiBulletDeathChance}% の確率で敵を即死させる（ボスを除く）。{ActiveMechanics.antiSlime_waveRecharge} ウェーブごとにリチャージ。";

        //new text for full game
        reward = "報酬： ";
        available = "所持金: ";
        selectAnUpgrade = "アップグレードを選択";
        coinDropChance = "ゴールドコインのドロップ率";
        startClickDamage = "初期クリックダメージ";
        startHealth = "初期HP";
        critChanceAndIncrease = "クリティカル率 & クリティカル倍率";
        clickCooldown = "初期クリッククールダウン";
        healHalfAHeart = "ごとにハートを半分回復:";
        waves = "ウェーブ";
        moreUpgradeSlots = "アップグレードスロット増加";
        rerolls = "リロール";
        invincibilityAfterDamage = "被ダメージ後の無敵時間";
        moreUpgradEChoises = "アップグレードの選択肢増加";
        onSlimeClickDeathChance = "スライムクリック/撃破時の発動チャンス";
        allDamageIncrease = "全ダメージ増加";
        betterActive = "アクティブスキル強化";
        tier = "レベル";
        slowerSlimes = "スライムの移動速度低下";
        slowerBullets = "スライムの弾速低下";
        max = "最大強化";
        sec = "秒";
        easy = "イージー";
        normal = "ノーマル";
        hard = "ハード";
        bullethell = "砲弾";
        flash = "フラッシュ";
        fragile = "フラジャイル";
        rampage = "ランページ";
        waveReached = "到達ウェーブ：";
        waveToBeat = "目標ウェーブ：";
        timeReached = "到達タイム：";
        timeToReach = "目標タイム：";
        price = "価格：";

        ChangeText();
        japaneseFlag.SetActive(true);
    }
    #endregion

    #region French
    public void FrenchLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(90, 0);
        tutRightArrow.transform.localPosition = new Vector2(-90, 0);

        languageSelected = 4; PlayerPrefs.SetInt("language", languageSelected);
        frenchFlag.GetComponent<Button>().onClick.Invoke();

        wave = "vague";
        crit = "CRITIQUE !";
        close = "fermer";
        back = "retour";
        reserCurrentRun = "souhaitez-vous réinitialiser votre partie en cours ?";
        backToMainMenu = "retourner au menu principal ? cela mettra fin à votre partie en cours";
        notAviableInDemo = "non disponible dans la version démo";
        SELECTED = "(SELECTIONNÉ)";
        level = "niveau";
        lvl = "niveau";
        fullscreen = "plein écran";
        ON = "(on)";
        OFF = "(off)";
        background = "arrière-plan";

        // Gamemodes strings
        easyDescription = $"Mode de jeu standard : difficulté facile\nSurvivez à {SelectGameMode.easy_waveToReach} vagues";
        normalDescription = $"Mode de jeu standard : difficulté normale. Certains slimes sont plus rapides. Les balles slime voyagent plus vite\nSurvivez à {SelectGameMode.normal_waveToReach} vagues";
        hardDescription = $"Mode de jeu standard : difficulté élevée. Tous les slimes sont plus rapides, les slimes tireurs tirent plus souvent et la fraise prend un cœur entier lorsqu'elle est touchée.\nSurvivez à {SelectGameMode.hard_waveToReach} vagues";
        bulletHellDescription = $"Défi Bullet Hell : Seuls les tireurs slimes apparaîtront. Les balles voyagent plus lentement. Les améliorations qui bloquent les balles ont plus de chances d'apparaître.\nSurvivez à {SelectGameMode.bullethell_waveToReach} vagues";
        flahsDescription = $"Défi Flash : Seuls les slimes rapides apparaîtront. Les améliorations qui ralentissent les slimes ont plus de chances d'apparaître.\nSurvivez à {SelectGameMode.flash_waveToReach}vagues";
        fragileDescription = $"Défi Fragile : La fraise meurt en un seul coup. L’amélioration de santé ne sera pas disponible.\nSurvivez à {SelectGameMode.fragile_waveToReach} vagues";
        narrowDescription = $"Défi Étroit : La zone de jeu est réduite.\nSurvivez à {SelectGameMode.narrow_waveToReach} vagues";
        rampageDescription = $"Défi Déchaînement : Il n'y a qu'une seule longue vague. Les améliorations apparaîtront toutes les 20 secondes, mais le jeu ne sera pas mis en pause (les balles slime continueront de se déplacer)\nSurvivez pendant {SelectGameMode.rampage_MinuteToReach} minutes.";

        // Active strings
        deathToSlimes = "MORT AUX SLIMES";
        deathToSlimes_des = $"élimine instantanément {ActiveMechanics.deathToSlimes_killAmount} slimes aléatoires. recharge après {ActiveMechanics.deathToSlimes_WaveRecharge} vagues";
        punchyClicks = "PUNCHY CLICKS";
        punchyClicks_des = $"tous les clics deviennent des clics critiques et le temps de recharge des clics est réduit à {ActiveMechanics.sharpClicksTimeInterval} seconde pendant {ActiveMechanics.sharpClicksTimer} secondes. recharge après {ActiveMechanics.sharpClicks_WaveRecharge} vagues";
        clover = "TRÈFLE";
        clover_des = $"Toutes les améliorations \"au clic sur un slime\" et \"à la mort d’un slime\" ont 50 % de chance de se déclencher pendant {ActiveMechanics.cloverTimer} secondes. Recharge : {ActiveMechanics.clover_waveRecharge} vagues.";
        decoy = "APPÂT FRAISE";
        decoy_des = $"placez un appât fraise. tous les slimes cibleront l'appât. l'appât reste actif pendant {ActiveMechanics.decoyWaveHealth} vagues, sauf s'il meurt après avoir pris un coup. recharge après {ActiveMechanics.decoy_waveRecharge} vagues";
        frency = "FRÉQUENCE DES PROJECTILES";
        frency_des = $"tirez {ActiveMechanics.projectileFrencyProjectiles} projectiles aléatoires avec le curseur pendant {ActiveMechanics.projectileFrencyTime} secondes. recharge après {ActiveMechanics.projectileFrency_waveRecharge} vagues";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"Tirez {ActiveMechanics.projectileFrencyProjectiles} projectiles aléatoires depuis la fraise pendant {ActiveMechanics.projectileFrencyTime} secondes. Recharge après {ActiveMechanics.projectileFrency_waveRecharge} vagues";
        }

        antiSlime = "BALLES ANTI-SLIME";
        antiSlime_des = $"tirez {ActiveMechanics.antiSlimeBulletCount} balles anti-slime depuis la fraise. chaque balle inflige {ActiveMechanics.antiSlimeDamage} dégâts et a {ActiveMechanics.antiBulletDeathChance}% de chances d'éliminer instantanément un ennemi (à l'exception des boss). recharge après {ActiveMechanics.antiSlime_waveRecharge} vagues";

        //new text for full game
        reward = "Récompense : ";
        available = "Disponible : ";
        selectAnUpgrade = "Sélectionnez une amélioration";
        coinDropChance = "Chance de faire tomber une pièce d’or";
        startClickDamage = "Dégâts par clics de départ";
        startHealth = "Santé de départ";
        critChanceAndIncrease = "Chance de coup critique et bonus critique";
        clickCooldown = "Temps de recharge par clic de départ";
        healHalfAHeart = "Régénère un demi-coeur toutes les ";
        waves = "vagues";
        moreUpgradeSlots = "Plus d'emplacements pour les améliorations";
        rerolls = "Relances";
        invincibilityAfterDamage = "Invincibilité après avoir été blessé";
        moreUpgradEChoises = "Plus de choix d’améliorations";
        onSlimeClickDeathChance = "Chance de déclenchement à la mort/au clic sur un slime";
        allDamageIncrease = "Augmentation de tous les dégâts";
        betterActive = "Amélioration ";
        tier = "Niveau";
        slowerSlimes = "Slimes plus lents";
        slowerBullets = "Projectiles de slimes plus lents";
        max = "MAX";
        sec = "sec";
        easy = "facile";
        normal = "normal";
        hard = "difficile";
        bullethell = "enfer de balles ";
        flash = "flash";
        fragile = "fragile";
        rampage = "carnage";
        waveReached = "Vague atteinte : ";
        waveToBeat = "Vague à battre : ";
        timeReached = "Temps atteint : ";
        timeToReach = "Temps à battre : ";
        price = "Récompense : ";

        ChangeText();
        frenchFlag.SetActive(true);
    }
    #endregion

    #region Spanish
    public void SpanishLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(90, 0);
        tutRightArrow.transform.localPosition = new Vector2(-90, 0);

        languageSelected = 5; PlayerPrefs.SetInt("language", languageSelected);
        spanishFlag.GetComponent<Button>().onClick.Invoke();

        wave = "ola";
        crit = "CRIT!";
        close = "cerrar";
        back = "atrás";
        reserCurrentRun = "¿Quieres reiniciar tu partida actual?";
        backToMainMenu = "¿Volver al menú principal? Esto terminará tu partida actual";
        notAviableInDemo = "no disponible en la demo";
        SELECTED = "(SELECCIONADO)";
        level = "nivel";
        lvl = "nivel";
        fullscreen = "pantalla completa";
        ON = "(on)";
        OFF = "(off)";
        background = "fondo";

        // Gamemodes strings
        easyDescription = $"El modo de juego estándar: Dificultad fácil\nSobrevive {SelectGameMode.easy_waveToReach} oleadas";
        normalDescription = $"Modo de juego estándar: Dificultad normal. Algunos slimes son más rápidos. Las balas de los slimes viajan más rápido\nSobrevive {SelectGameMode.normal_waveToReach} oleadas";
        hardDescription = $"Modo de juego estándar: Dificultad difícil. Todos los slimes son más rápidos, los slimes que disparan lo hacen con más frecuencia y la fresa pierde 1 corazón completo al recibir daño.\nSobrevive {SelectGameMode.hard_waveToReach} oleadas";
        bulletHellDescription = $"Desafío infierno de balas: Solo aparecerán slimes que disparan. Las balas viajan más lento. Las mejoras que bloquean balas tienen más probabilidad de aparecer.\nSobrevive {SelectGameMode.bullethell_waveToReach} oleadas";
        flahsDescription = $"Desafío flash: Solo aparecerán slimes rápidos. Las mejoras que ralentizan slimes tienen más probabilidad de aparecer.\nSobrevive {SelectGameMode.flash_waveToReach} oleadas";
        fragileDescription = $"Desafío frágil: La fresa muere de un golpe. La mejora de salud no aparecerá.\nSobrevive {SelectGameMode.fragile_waveToReach} oleadas";
        narrowDescription = $"Desafío estrecho: El área de juego es más pequeña.\nSobrevive {SelectGameMode.narrow_waveToReach} oleadas";
        rampageDescription = $"Desafío frenesí: Solo hay 1 oleada larga. Las mejoras aparecerán cada 20 segundos pero el juego no se pausará (las balas de slime se detendrán).\nSobrevive {SelectGameMode.rampage_MinuteToReach} minutos.";

        // Active strings
        deathToSlimes = "MUERTE A LOS SLIMES";
        deathToSlimes_des = $"mata instantáneamente a {ActiveMechanics.deathToSlimes_killAmount} slimes aleatorios.{ActiveMechanics.deathToSlimes_WaveRecharge}  oleadas de recarga";
        punchyClicks = "CLICKS POTENTES";
        punchyClicks_des = $"todos los clicks son críticos y el tiempo de recarga se reduce a {ActiveMechanics.sharpClicksTimeInterval} segundos durante {ActiveMechanics.sharpClicksTimer} segundos. {ActiveMechanics.sharpClicks_WaveRecharge} oleadas de recarga";
        clover = "TRÉBOL";
        clover_des = $"todas las mejoras \"al hacer click en slime\" y \"al matar slime\" siempre se activan durante los próximos {ActiveMechanics.cloverTimer} segundos. {ActiveMechanics.clover_waveRecharge} oleadas de recarga";
        decoy = "SEÑUELO DE FRESA";
        decoy_des = $"todas las mejoras \"al hacer click en slime\" y \"al matar slime\" tienen 50% de probabilidad de activarse durante los próximos {ActiveMechanics.decoyWaveHealth} segundos {ActiveMechanics.decoy_waveRecharge} oleadas de recarga";
        frency = "FRENESÍ DE PROYECTILES";
        frency_des = $"dispara {ActiveMechanics.projectileFrencyProjectiles} proyectiles aleatorios desde el cursor durante {ActiveMechanics.projectileFrencyTime} segundos. {ActiveMechanics.projectileFrency_waveRecharge} oleadas de recarga";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"Dispara {ActiveMechanics.projectileFrencyProjectiles} proyectiles aleatorios desde la fresa durante {ActiveMechanics.projectileFrencyTime} segundos. {ActiveMechanics.projectileFrency_waveRecharge} oleadas de recarga";
        }

        antiSlime = "BALAS ANTI-SLIME";
        antiSlime_des = $"dispara {ActiveMechanics.antiSlimeBulletCount} balas anti-slime desde la fresa. cada bala hace {ActiveMechanics.antiSlimeDamage} de daño y tiene {ActiveMechanics.antiBulletDeathChance}% de probabilidad de matar instantáneamente a un enemigo (excepto jefes). {ActiveMechanics.antiSlime_waveRecharge} oleadas de recarga";

        //new text for full game
        reward = "Recompensa: ";
        available = "Disponible: ";
        selectAnUpgrade = "Selecciona una mejora";
        coinDropChance = "Probabilidad de obtener monedas de oro";
        startClickDamage = "Daño inicial de click";
        startHealth = "Salud inicial";
        critChanceAndIncrease = "Probabilidad de crítico y aumento crítico";
        clickCooldown = "Tiempo de recarga inicial de click";
        healHalfAHeart = "Cura medio corazón cada";
        waves = "oleadas";
        moreUpgradeSlots = "Más espacios de mejora";
        rerolls = "Rerolls";
        invincibilityAfterDamage = "Invencibilidad después de ser dañado";
        moreUpgradEChoises = "Más opciones de mejora";
        onSlimeClickDeathChance = "Probabilidad de activación al hacer click/matar slime";
        allDamageIncrease = "Aumento de todo el daño";
        betterActive = "Activa mejorada";
        tier = "Nivel";
        slowerSlimes = "Slimes más lentos";
        slowerBullets = "Balas de slime más lentas";
        max = "MAX";
        sec = "s";
        easy = "fácil";
        normal = "normal";
        hard = "difícil";
        bullethell = "bullethell";
        flash = "flash";
        fragile = "frágil";
        rampage = "frenesí";
        waveReached = "oleada alcanzada:";
        waveToBeat = "oleada a superar:";
        timeReached = "tiempo alcanzado:";
        timeToReach = "tiempo a alcanzar:";
        price = "Precio: ";

        ChangeText();
        spanishFlag.SetActive(true);
    }
    #endregion

    #region Chinese
    public void ChineseLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(60, 0);
        tutRightArrow.transform.localPosition = new Vector2(-60, 0);

        languageSelected = 6; PlayerPrefs.SetInt("language", languageSelected);
        chineseFlag.GetComponent<Button>().onClick.Invoke();

        wave = "第-波攻击";
        crit = "暴击";
        close = "关闭";
        back = "返回";
        reserCurrentRun = "是否要重置当前回合？";
        backToMainMenu = "回到主菜单吗？这会结束你当前的回合";
        notAviableInDemo = "演示版本中不能玩";
        SELECTED = "(选中)";
        level = "级";
        lvl = "级";
        fullscreen = "全屏 ";
        ON = "(开启)";
        OFF = "(关闭)";
        background = "背景";

        // Gamemodes strings
        easyDescription = $"标准游戏模式：难易程度 - 简单\n挺过{SelectGameMode.easy_waveToReach}波攻击";
        normalDescription = $"标准游戏模式：难易程度 -普通。有些史莱姆更快。史莱姆子弹速度更快.\n挺过{SelectGameMode.normal_waveToReach}波攻击";
        hardDescription = $"标准游戏模式：难易程度 -困难。所有的史莱姆都更快，射击史莱姆的次数更多，草莓受伤时要消耗1个满格生命值。\n挺过{SelectGameMode.hard_waveToReach}波攻击";
        bulletHellDescription = $"子弹地狱挑战：只有射击史莱姆才可以生成。子弹飞射速度更慢。有更高几率出现能阻挡子弹的升级道具。\n挺过 {SelectGameMode.bullethell_waveToReach}波攻击";
        flahsDescription = $"快闪挑战: 只有快速史莱姆才可以生成。有更高几率出现能减缓史莱姆速度的升级道具。\n挺过{SelectGameMode.flash_waveToReach}波攻击";
        fragileDescription = $"脆弱挑战：草莓在1次攻击后阵亡。生命值升级将不会出现。\n挺过{SelectGameMode.fragile_waveToReach}波攻击";
        narrowDescription = $"狭窄挑战: 游戏区域更小。\n挺过{SelectGameMode.narrow_waveToReach}波攻击";
        rampageDescription = $"横冲直撞挑战: 只有1波长攻击。每20秒将出现一次升级，但游戏不会暂停（史莱姆子弹会停止射击）。\n挺过{SelectGameMode.rampage_MinuteToReach}分钟。";

        // Active strings
        deathToSlimes = "击杀史莱姆";
        deathToSlimes_des = $"瞬间随机击杀{ActiveMechanics.deathToSlimes_killAmount}个史莱姆。每{ActiveMechanics.deathToSlimes_WaveRecharge}波可充能使用一次。";
        punchyClicks = "强力点击";
        punchyClicks_des = $"所有点击均具有暴击效果，充能时间缩短至{ActiveMechanics.sharpClicksTimeInterval}秒，持续{ActiveMechanics.sharpClicksTimer}秒。每 {ActiveMechanics.sharpClicks_WaveRecharge}波可充能使用一次。";
        clover = "道具";
        clover_des = $"有50%的几率在接下来的{ActiveMechanics.cloverTimer}秒内触发所有“点击史莱姆时”和“史莱姆死亡时”升级。每{ActiveMechanics.clover_waveRecharge}波可充能使用一次。";
        decoy = "草莓诱饵";
        decoy_des = $"放下一个草莓诱饵。所有的史莱姆都会瞄准诱饵。诱饵持续{ActiveMechanics.decoyWaveHealth}波，除非它受到一次伤害而阵亡。每{ActiveMechanics.decoy_waveRecharge}波可充能使用一次。";
        frency = "疯狂炮弹";
        frency_des = $"从光标处随机发射{ActiveMechanics.projectileFrencyProjectiles}枚炮弹，持续{ActiveMechanics.projectileFrencyTime}秒。每 {ActiveMechanics.projectileFrency_waveRecharge}波可充能使用一次。";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"从草莓中随机发射{ActiveMechanics.projectileFrencyProjectiles}枚炮弹，持续{ActiveMechanics.projectileFrencyTime}秒。每 {ActiveMechanics.projectileFrency_waveRecharge}波可充能使用一次。";
        }

        antiSlime = "对抗史莱姆子弹";
        antiSlime_des = $"从草莓中射出{ActiveMechanics.antiSlimeBulletCount}颗对抗史莱姆子弹。每颗子弹造成{ActiveMechanics.antiSlimeDamage}点伤害，有{ActiveMechanics.antiBulletDeathChance}%的几率瞬间击杀一名敌人（boss除外）。每{ActiveMechanics.antiSlime_waveRecharge}波可充能使用";

        //new text for full game
        reward = "奖励：";
        available = "可用: ";
        selectAnUpgrade = "选择一次升级";
        coinDropChance = "金币掉落几率";
        startClickDamage = "初始点击伤害";
        startHealth = "初始生命值";
        critChanceAndIncrease = "暴击几率和暴击加成";
        clickCooldown = "初始点击冷却";
        healHalfAHeart = "治愈半颗心：";
        waves = "波";
        moreUpgradeSlots = "更多升级槽";
        rerolls = "重新选择";
        invincibilityAfterDamage = "受伤后获得无敌状态";
        moreUpgradEChoises = "更多升级选择";
        onSlimeClickDeathChance = "点击史莱姆时/史莱姆死亡时触发几率";
        allDamageIncrease = "所有伤害加成";
        betterActive = "更优主动能力";
        tier = "等级";
        slowerSlimes = "较慢的史莱姆";
        slowerBullets = "较慢的史莱姆子弹";
        max = "最大值";
        sec = "秒";
        easy = "简单";
        normal = "正常";
        hard = "困难";
        bullethell = "弹幕";
        flash = "闪光";
        fragile = "脆弱";
        rampage = "狂暴";
        waveReached = "已完成波数：";
        waveToBeat = "待战斗波数：";
        timeReached = "已完成时间：";
        timeToReach = "待完成时间：";
        price = "价格： ";

        ChangeText();
        chineseFlag.SetActive(true);
    }
    #endregion

    #region Korean
    public void KoreanLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(90, 0);
        tutRightArrow.transform.localPosition = new Vector2(-90, 0);

        languageSelected = 7; PlayerPrefs.SetInt("language", languageSelected);
        koreanFlag.GetComponent<Button>().onClick.Invoke();

        wave = "웨이브";
        crit = "크리티컬";
        close = "닫기";
        back = "뒤로";
        reserCurrentRun = "현재 진행 중인 게임을 초기화하시겠습니까?";
        backToMainMenu = "메인 메뉴로 돌아가시겠습니까? 현재 게임이 종료됩니다.";
        notAviableInDemo = "데모 버전에서는 사용할 수 없습니다.";
        SELECTED = "(선택됨)";
        level = "레벨";
        lvl = "레벨";
        fullscreen = "전체 화면";
        ON = "(켜짐)";
        OFF = "(꺼짐)";
        background = "배경";

        // Gamemodes strings
        easyDescription = $"일반 게임 모드: 쉬움 난이도\n{SelectGameMode.easy_waveToReach}웨이브 동안 생존하세요.";
        normalDescription = $"일반 게임 모드: 보통 난이도. 일부 슬라임이 더 빠릅니다. 슬라임 총알의 속도가 더 빠릅니다.\n {SelectGameMode.normal_waveToReach}웨이브 동안 생존하세요.";
        hardDescription = $"일반 게임 모드: 어려움 난이도. 모든 슬라임이 더 빠르고, 발사 슬라임의 공격 빈도가 증가하며, 딸기가 피해를 입으면 체력이 1칸 감소합니다.\n{SelectGameMode.hard_waveToReach}웨이브 동안 생존하세요.";
        bulletHellDescription = $"탄환 지옥 챌린지: 발사 슬라임만 생성됩니다. 탄환 속도가 느립니다. 탄환을 막는 업그레이드가 나올 확률이 더 높습니다.\n {SelectGameMode.bullethell_waveToReach}웨이브 동안 생존하세요.";
        flahsDescription = $"속도광 챌린지: 빠른 슬라임만 생성됩니다. 슬라임 속도를 느리게 하는 업그레이드가 나올 확률이 더 높습니다.\n {SelectGameMode.flash_waveToReach}웨이브 동안 ";
        fragileDescription = $"허약 챌린지: 딸기는 한 방에 죽습니다. 체력 업그레이드는 나타나지 않습니다.\n{SelectGameMode.fragile_waveToReach}웨이브 동안 생존하세요.";
        narrowDescription = $"좁은 공간 챌린지: 게임 영역이 더 작습니다. \n{SelectGameMode.narrow_waveToReach}웨이브 동안 생존하세요.";
        rampageDescription = $"광란 챌린지: 긴 웨이브가 단 1만 있습니다. 20초마다 업그레이드가 나타나지만 게임이 일시 정지되지 않습니다 (슬라임 총알은 멈춥니다).\n{SelectGameMode.rampage_MinuteToReach}분 동안 생존하세요";

        // Active strings
        deathToSlimes = "슬라임 처형";
        deathToSlimes_des = $"{ActiveMechanics.deathToSlimes_killAmount}마리의 무작위 슬라임을 즉시 처치합니다.{ActiveMechanics.deathToSlimes_WaveRecharge} 웨이브 재충전";
        punchyClicks = "강력한 클릭";
        punchyClicks_des = $"모든 클릭이 치명타 클릭이 되고, 클릭 재충전 시간이 {ActiveMechanics.sharpClicksTimeInterval}초 동안{ActiveMechanics.sharpClicksTimer} 초로 감소합니다. {ActiveMechanics.sharpClicks_WaveRecharge}웨이브 재충전";
        clover = "클로버";
        clover_des = $"모든 \"슬라임 클릭 시\" 및 \"슬라임 처치 시\" 업그레이드가 다음 {ActiveMechanics.cloverTimer}초 동안 50% 확률로 발동합니다.{ActiveMechanics.clover_waveRecharge} 웨이브 재충전.";
        decoy = "딸기 미끼";
        decoy_des = $"딸기 미끼를 설치합니다. 모든 슬라임이 미끼를 목표로 합니다. 미끼는 피해를 입어 한 번 죽지 않는 한{ActiveMechanics.decoyWaveHealth}웨이브 동안 지속됩니다.{ActiveMechanics.decoy_waveRecharge}웨이브 재충전";
        frency = "투사체 광란";
        frency_des = $"{ActiveMechanics.projectileFrencyTime}초 동안 커서에서 {ActiveMechanics.projectileFrencyProjectiles}개의 무작위 투사체를 발사합니다. {ActiveMechanics.projectileFrency_waveRecharge}웨이브 재충전";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"{ActiveMechanics.projectileFrencyTime}초 동안 딸기에서 {ActiveMechanics.projectileFrencyProjectiles}개의 무작위 투사체를 발사합니다. {ActiveMechanics.projectileFrency_waveRecharge}웨이브 재충전";
        }

        antiSlime = "대 슬라임 탄환";
        antiSlime_des = $"딸기에서 {ActiveMechanics.antiSlimeBulletCount}개의 대 슬라임 탄환을 발사합니다. 각 탄환은 {ActiveMechanics.antiSlimeDamage}의 피해를 입히고 적을 즉사시킬 확률이 {ActiveMechanics.antiBulletDeathChance}%입니다 (보스 제외). {ActiveMechanics.antiSlime_waveRecharge}웨이브 재충전";

        //new text for full game
        reward = "보상: ";
        available = "보유 중: ";
        selectAnUpgrade = "업그레이드 선택";
        coinDropChance = "금화 드랍 확률";
        startClickDamage = "시작 클릭 데미지";
        startHealth = "시작 체력";
        critChanceAndIncrease = "치명타 확률 및 증가량";
        clickCooldown = "시작 클릭 쿨타임";
        healHalfAHeart = "하트 반 칸 회복";
        waves = "웨이브마다";
        moreUpgradeSlots = "업그레이드 슬롯 추가";
        rerolls = "다시 굴리기";
        invincibilityAfterDamage = "피해 후 무적";
        moreUpgradEChoises = "업그레이드 선택지 추가";
        onSlimeClickDeathChance = "슬라임 클릭/처치 시 발동 확률";
        allDamageIncrease = "전체 데미지 증가";
        betterActive = "활성 효과 강화";
        tier = "단계";
        slowerSlimes = "슬라임 이동 속도 감소";
        slowerBullets = "슬라임 탄환 속도 감소";
        max = "최대치";
        sec = "초";
        easy = "쉬움";
        normal = "보통";
        hard = "어려움";
        bullethell = "탄막 지옥";
        flash = "번개";
        fragile = "허약";
        rampage = "광란";
        waveReached = "도달한 웨이브:";
        waveToBeat = "돌파해야 할 웨이브:";
        timeReached = "도달한 시간:";
        timeToReach = "돌파해야 할 시간:";
        price = "가격: ";

        ChangeText();
        koreanFlag.SetActive(true);
    }
    #endregion

    #region Russian
    public void RussianLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(135, 0);
        tutRightArrow.transform.localPosition = new Vector2(-135, 0);

        languageSelected = 8; PlayerPrefs.SetInt("language", languageSelected);
        russianFlag.GetComponent<Button>().onClick.Invoke();

        wave = "волна";
        crit = "КРИТ!";
        close = "закрыть";
        back = "назад";
        reserCurrentRun = "сбросить текущий раунд?";
        backToMainMenu = "вернуться в главное меню? это завершит текущий раунд";
        notAviableInDemo = "недоступно в демоверсии";
        SELECTED = "(ВЫБРАНО)";
        level = "уровень";
        lvl = "уровень";
        fullscreen = "во весь экран";
        ON = "(вкл.)";
        OFF = "(выкл.)";
        background = "фон";

        // Gamemodes strings
        easyDescription = $"TСтандартный режим: легкий уровень\nПережить {SelectGameMode.easy_waveToReach} волн";
        normalDescription = $"Стандартный режим: средний уровень. Некоторые слизни быстрее. Пули летят быстрее\nПережить {SelectGameMode.normal_waveToReach} волн";
        hardDescription = $"Стандартный режим: тяжелый уровень. Все слизни быстрые, слизни стреляют чаще, повреждение клубники = 1 полное сердце.\nПережить {SelectGameMode.hard_waveToReach} волн";
        bulletHellDescription = $"Испытание «Ад из пуль»: только стреляющие слизни. Пули летят медленнее. Улучшения, блокирующие пули, появляются чаще.\nПережить {SelectGameMode.bullethell_waveToReach}  волн";
        flahsDescription = $"Испытание «Флэш»: только быстрые слизни. Улучшения, замедляющие слизней, появляются чаще.\nПережить{SelectGameMode.flash_waveToReach} волн";
        fragileDescription = $"Хрупкость: клубника гибнет от 1 удара. Улучшений здоровья нет.\nПережить {SelectGameMode.fragile_waveToReach} волн";
        narrowDescription = $"Испытание «Узость»: игровая зона меньше.\nПережить {SelectGameMode.narrow_waveToReach} волн";
        rampageDescription = $"Испытание «Ярость»: всего 1 долгая волна. Улучшения появляются каждые 20 сек., но игра не ставится на паузу (пули не летят).\nВыдержать {SelectGameMode.rampage_MinuteToReach} мин.";

        // Active strings
        deathToSlimes = "СМЕРТЬ СЛИЗНЯМ";
        deathToSlimes_des = $"мгновенно убивает {ActiveMechanics.deathToSlimes_killAmount} случайных слизней. Откат в{ActiveMechanics.deathToSlimes_WaveRecharge} волны";
        punchyClicks = "УДАРНЫЕ КЛИКИ";
        punchyClicks_des = $"все клики критичны, время отката клика сокращено до {ActiveMechanics.sharpClicksTimeInterval}сек. (на {ActiveMechanics.sharpClicksTimer}сек.). Откат в {ActiveMechanics.sharpClicks_WaveRecharge} волны";
        clover = "КЛЕВЕР";
        clover_des = $"все улучшения «при клике на слизь» и «при смерти слизи» имеют 50% шанс активации следующие {ActiveMechanics.cloverTimer} сек. {ActiveMechanics.clover_waveRecharge} волны на откат";
        decoy = "КЛУБНИЧНАЯ ПРИМАНКА";
        decoy_des = $"поместите клубничную приманку. все слизни будут нацелены на приманку. приманка продержится {ActiveMechanics.decoyWaveHealth} волны, если не погибнет, получив урон. Откат в {ActiveMechanics.decoy_waveRecharge} волн";
        frency = "ЯРОСТЬ СНАРЯДОВ";
        frency_des = $"{ActiveMechanics.projectileFrencyProjectiles} случайных снарядов из курсора за {ActiveMechanics.projectileFrencyTime} сек. Откат в {ActiveMechanics.projectileFrency_waveRecharge} волны";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"{ActiveMechanics.projectileFrencyProjectiles} случайных снарядов из клубники за {ActiveMechanics.projectileFrencyTime} сек. Откат в {ActiveMechanics.projectileFrency_waveRecharge} волны";
        }

        antiSlime = "АНТИ-СЛИЗНЕВЫЕ ПУЛИ";
        antiSlime_des = $"{ActiveMechanics.antiSlimeBulletCount} анти-слизневых пуль из клубники. каждая пуля наносит {ActiveMechanics.antiSlimeDamage} ед. урона и имеет {ActiveMechanics.antiBulletDeathChance}% шанс мгновенно убить врага (кроме боссов). Откат в {ActiveMechanics.antiSlime_waveRecharge}  волны";

        //new text for full game
        reward = "Награда: ";
        available = "Доступно: ";
        selectAnUpgrade = "Выбрать улучшение";
        coinDropChance = "Шанс выпадения золотой монеты";
        startClickDamage = "Начальный урон от клика";
        startHealth = "Начальное здоровье";
        critChanceAndIncrease = "Шанс крита и повышение крита";
        clickCooldown = "Начальный откат клика";
        healHalfAHeart = "Исцеление половины сердца каждые";
        waves = "волны";
        moreUpgradeSlots = "Больше слотов для улучшений";
        rerolls = "Перетасовки";
        invincibilityAfterDamage = "Неуязвимость после урона";
        moreUpgradEChoises = "Больше вариантов улучшения";
        onSlimeClickDeathChance = "При клике на слизь/шанс активации смерти";
        allDamageIncrease = "Усиление всего урона";
        betterActive = "Лучше быть активным";
        tier = "Уровень";
        slowerSlimes = "Замедление слизи";
        slowerBullets = "Замедление пуль слизи";
        max = "МАКС.";
        sec = "с";
        easy = "легкий";
        normal = "средний";
        hard = "тяжелый";
        bullethell = "ад из пуль";
        flash = "вспышка";
        fragile = "хрупкость";
        rampage = "буйство";
        waveReached = "отбито волн:";
        waveToBeat = "осталось волн:";
        timeReached = "время выполнения:";
        timeToReach = "рекорд времени:";
        price = "Цена: ";

        ChangeText();
        russianFlag.SetActive(true);
    }
    #endregion

    #region Polish
    public void PolishLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(120, 0);
        tutRightArrow.transform.localPosition = new Vector2(-120, 0);

        languageSelected = 9; PlayerPrefs.SetInt("language", languageSelected);
        polishFlag.GetComponent<Button>().onClick.Invoke();

        wave = "fala";
        crit = "KRYT!";
        close = "zamknij";
        back = "wstecz";
        reserCurrentRun = "Czy chcesz zresetować bieżącą grę?";
        backToMainMenu = "wrócić do menu głównego? Spowoduje to zakończenie bieżącej gry.";
        notAviableInDemo = "niedostępne w wersji demo";
        SELECTED = "(WYBRANO)";
        level = "poziom";
        lvl = "poz. ";
        fullscreen = "pełny ekran";
        ON = "(wł.)";
        OFF = "(wył.)";
        background = "tło";

        // Gamemodes strings
        easyDescription = $"Standardowy tryb gry: Łatwy poziom trudności\nPrzetrwaj {SelectGameMode.easy_waveToReach} fal";
        normalDescription = $"Standardowy tryb gry: Normalny poziom trudności. Niektóre slimy są szybsze. Pociski slimów poruszają się szybciej\nPrzetrwaj {SelectGameMode.normal_waveToReach} fal";
        hardDescription = $"Standardowy tryb gry: Trudny poziom trudności. Wszystkie slimy są szybsze, strzelające slimy strzelają częściej, a truskawka otrzymuje 1 pełne serce obrażeń po uszkodzeniu.\nPrzetrwaj {SelectGameMode.hard_waveToReach} fal";
        bulletHellDescription = $"Wyzwanie z piekła rodem: Odradzają się tylko strzelające slimy. Pociski poruszają się wolniej. Ulepszenia blokujące pociski pojawiają się częściej.\nPrzetrwaj {SelectGameMode.bullethell_waveToReach} fal";
        flahsDescription = $"Wyzwanie błyskawiczne: Odradzają się tylko szybkie slimy. Ulepszenia spowalniające slimy pojawiają się częściej.\nPrzetrwaj {SelectGameMode.flash_waveToReach} fal";
        fragileDescription = $"Kruche wyzwanie: Truskawka ginie po 1 trafieniu. Brak ulepszeń zdrowia.\nPrzetrwaj {SelectGameMode.fragile_waveToReach} fal";
        narrowDescription = $"Mniejsze wyzwanie: obszar gry jest mniejszy.\nPrzetrwaj {SelectGameMode.narrow_waveToReach} fal";
        rampageDescription = $"Szał: Jest tylko 1 długa fala. Ulepszenia będą pojawiać się co 20 sekund, ale gra nie zostanie wstrzymana (pociski nie zatrzymują się).\nPrzetrwaj {SelectGameMode.rampage_MinuteToReach} minut.";

        // Active strings
        deathToSlimes = "ŚMIERĆ SLIMOM";
        deathToSlimes_des = $"natychmiast zabija {ActiveMechanics.deathToSlimes_killAmount} losowych slimów. {ActiveMechanics.deathToSlimes_WaveRecharge} fale ładowania";
        punchyClicks = "KRYTYCZNE KLIKNIĘCIA";
        punchyClicks_des = $"wszystkie kliknięcia są kliknięciami krytycznymi, a czas ładowania kliknięć zostaje skrócony do {ActiveMechanics.sharpClicksTimeInterval} sekundy na {ActiveMechanics.sharpClicksTimer} sekundy. {ActiveMechanics.sharpClicks_WaveRecharge} fale ładowania";
        clover = "KONICZYNA";
        clover_des = $"wszystkie ulepszenia „po kliknięciu slime” i „po śmierci slime” mają 50% szans na aktywację przez następne {ActiveMechanics.cloverTimer} sekundy. {ActiveMechanics.clover_waveRecharge} fale ładowania";
        decoy = "WABIK TRUSKAWKOWY";
        decoy_des = $"umieść wabik truskawkowy. wszystkie slimy będą celować w wabik. wabik wytrzyma {ActiveMechanics.decoyWaveHealth} fale, chyba że zginie, otrzymując obrażenia. {ActiveMechanics.decoy_waveRecharge} fal";
        frency = "SZAŁ POCISKÓW";
        frency_des = $"wystrzeliwuje {ActiveMechanics.projectileFrencyProjectiles} losowych pocisków z kursora przez {ActiveMechanics.projectileFrencyTime} sekundy. {ActiveMechanics.projectileFrency_waveRecharge} fale ładowania";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"wystrzeliwuje {ActiveMechanics.projectileFrencyProjectiles} losowych pocisków z truskawki przez {ActiveMechanics.projectileFrencyTime} sekundy. {ActiveMechanics.projectileFrency_waveRecharge} fale ładowania";
        }

        antiSlime = "POCISKI PRZECIW SLIMOM";
        antiSlime_des = $"wystrzel z truskawki {ActiveMechanics.antiSlimeBulletCount} pocisków przeciw slimom. każdy pocisk zadaje {ActiveMechanics.antiSlimeDamage} obrażeń i ma {ActiveMechanics.antiBulletDeathChance}% szans na natychmiastowe zabicie wroga (z wyjątkiem bossów). {ActiveMechanics.antiSlime_waveRecharge} fale ładowania";

        //new text for full game
        reward = "Nagroda: ";
        available = "Dostępne: ";
        selectAnUpgrade = "Wybierz ulepszenie";
        coinDropChance = "Szansa na drop złotej monety";
        startClickDamage = "Początkowe obrażenia na kliknięcie";
        startHealth = "Początkowy poziom zdrowia";
        critChanceAndIncrease = "Szansa na trafienie krytyczne i wzrost trafień krytycznych";
        clickCooldown = "Czas odnowienia przycisku Start";
        healHalfAHeart = "Leczenie pół serca co";
        waves = "fale";
        moreUpgradeSlots = "Więcej miejsc na ulepszenia";
        rerolls = "Przerzuty";
        invincibilityAfterDamage = "Nieśmiertelność po otrzymaniu obrażeń";
        moreUpgradEChoises = "Więcej opcji ulepszeń";
        onSlimeClickDeathChance = "Szansa na aktywację po kliknięciu/śmierci slime";
        allDamageIncrease = "Zwiększenie wszystkich obrażeń";
        betterActive = "Lepsze aktywne";
        tier = "Poziom";
        slowerSlimes = "Wolniejsze slimy";
        slowerBullets = "Wolniejsze pociski slimów";
        max = "MAKS";
        sec = "s";
        easy = "łatwy";
        normal = "normalny";
        hard = "trudny";
        bullethell = "z piekła rodem";
        flash = "błyskawiczny";
        fragile = "niebezpieczny";
        rampage = "szał";
        waveReached = "osiągnięta fala:";
        waveToBeat = "fala do pobicia:";
        timeReached = "osiągnięty czas:";
        timeToReach = "czas do pobicia:";
        price = "Cena: ";

        ChangeText();
        polishFlag.SetActive(true);
    }
    #endregion

    #region Portugese
    public void PortugeseLanguage()
    {
        tutLeftArrow.transform.localPosition = new Vector2(93, 0);
        tutRightArrow.transform.localPosition = new Vector2(-93, 0);

        languageSelected = 10; PlayerPrefs.SetInt("language", languageSelected);
        portugeseFlag.GetComponent<Button>().onClick.Invoke();

        wave = "onda";
        crit = "CRIT!";
        close = "fechar";
        back = "voltar";
        reserCurrentRun = "gostaria de reiniciar seu round atual?";
        backToMainMenu = "voltar ao menu principal? isso encerrará seu round atual";
        notAviableInDemo = "não disponível na demonstração";
        SELECTED = "(SELECIONADO)";
        level = "Nível";
        lvl = "nível";
        fullscreen = "Tela Cheia";
        ON = "(ligado)";
        OFF = "(desligado)";
        background = "plano de fundo";

        // Gamemodes strings
        easyDescription = $"O modo de jogo padrão: Dificuldade fácil\nSobreviver a {SelectGameMode.easy_waveToReach} ondas";
        normalDescription = $"O modo de jogo padrão: Dificuldade normal. Algumas gosmas são mais rápidas. As balas de gosma viajam mais rápido\nSobreviver a {SelectGameMode.normal_waveToReach} ondas";
        hardDescription = $"O modo de jogo padrão: Dificuldade difícil. Todas as gosmas são mais rápidas, as gosmas atiradoras disparam com mais frequência e o morango recebe 1 coração inteiro quando danificado.\nSobreviver a {SelectGameMode.hard_waveToReach} ondas";
        bulletHellDescription = $"Desafio do Inferno de Balas: Somente as gosmas atiradoras aparecerão. As balas viajam em um ritmo mais lento. Os aprimoramentos que bloqueiam balas têm maior chance de aparecer.\nSobreviva a {SelectGameMode.bullethell_waveToReach} ondas";
        flahsDescription = $"Desafio do Flash: Apenas gosmas rápidas aparecerão. Os aprimoramentos que reduzem a velocidade das gosmas têm uma chance maior de aparecer.\nSobreviver a {SelectGameMode.flash_waveToReach} ondas";
        fragileDescription = $"Desafio frágil: O morango morre em 1 golpe. A melhoria de saúde não aparecerá.\nSobreviver a {SelectGameMode.fragile_waveToReach} ondas";
        narrowDescription = $"Desafio Estreito: A área do jogo é menor.\nSobreviva a {SelectGameMode.narrow_waveToReach} ondas";
        rampageDescription = $"Desafio da Fúria: Há apenas 1 onda longa. Os aprimoramentos aparecerão a cada 20 segundos, mas o jogo não será pausado (as balas de gosma serão interrompidas).\nSobreviva {SelectGameMode.rampage_MinuteToReach} minutos.";

        // Active strings
        deathToSlimes = "MORTE PARA AS GOSMAS";
        deathToSlimes_des = $"mata instantaneamente {ActiveMechanics.deathToSlimes_killAmount} gosmas aleatórias. Recarga de {ActiveMechanics.deathToSlimes_WaveRecharge} ondas";
        punchyClicks = "CLIQUES DE PANCADARIA";
        punchyClicks_des = $"todos os cliques são cliques críticos e o tempo de recarga do clique é reduzido para {ActiveMechanics.sharpClicksTimeInterval} segundo por {ActiveMechanics.sharpClicksTimer} segundos. Recarga de {ActiveMechanics.sharpClicks_WaveRecharge} ondas";
        clover = "TREVO";
        clover_des = $"todos os aprimoramentos “ao clicar na gosma” e “ao morrer da gosma” têm 50% de chance de serem ativados nos próximos {ActiveMechanics.cloverTimer} segundos. Recarga de {ActiveMechanics.clover_waveRecharge} ondas";
        decoy = "CHAMARIZ DE MORANGO";
        decoy_des = $"Coloque um chamariz de morango. Todas as gosmas terão o chamariz como alvo. O chamariz dura {ActiveMechanics.decoyWaveHealth} ondas, a menos que morra ao receber dano uma vez. {ActiveMechanics.decoy_waveRecharge} ondas de recarga";
        frency = "PROJÉTIL FRENCY";
        frency_des = $"dispara {ActiveMechanics.projectileFrencyProjectiles} projéteis aleatórios do cursor por {ActiveMechanics.projectileFrencyTime} segundos. ondas de {ActiveMechanics.projectileFrency_waveRecharge} recarga";

        if (MobileScript.isMobile == true)
        {
            frency_des = $"Dispara {ActiveMechanics.projectileFrencyProjectiles} projéteis aleatórios do morango por {ActiveMechanics.projectileFrencyTime} segundos. Ondas de {ActiveMechanics.projectileFrency_waveRecharge} recarga";
        }

        antiSlime = "BALAS ANTI-GOSMAS";
        antiSlime_des = $"dispare {ActiveMechanics.antiSlimeBulletCount} balas anti-gosma do morango. Cada bala causa {ActiveMechanics.antiSlimeDamage} de dano e tem {ActiveMechanics.antiBulletDeathChance}% de chance de matar um inimigo instantaneamente (exceto chefes). Recarga de {ActiveMechanics.antiSlime_waveRecharge} ondas";

        //new text for full game
        reward = "Recompensa: ";
        available = "Disponível: ";
        selectAnUpgrade = "Selecione uma atualização";
        coinDropChance = "Chance de queda de moedas de ouro";
        startClickDamage = "Dano do clique inicial";
        startHealth = "Vida inicial";
        critChanceAndIncrease = "Chance de crítico e aumento de crítico";
        clickCooldown = "Início do tempo de recarga do clique";
        healHalfAHeart = "Cura meio coração a cada";
        waves = "ondas";
        moreUpgradeSlots = "Mais espaços de upgrade";
        rerolls = "Rolar novamente";
        invincibilityAfterDamage = "Invencibilidade após sofrer dano";
        moreUpgradEChoises = "Mais opções de aprimoramento";
        onSlimeClickDeathChance = "Chance de ativação no clique/morte do slime";
        allDamageIncrease = "Aumento de todos os danos";
        betterActive = "Melhor ativo";
        tier = "Nível ";
        slowerSlimes = "Gosmas mais lentas";
        slowerBullets = "Balas de Gosmas mais lentas";
        max = "MAX";
        sec = "s";
        easy = "fácil";
        normal = "normal";
        hard = "difícil";
        bullethell = "inferno de balas";
        flash = "flash";
        fragile = "frágil";
        rampage = "fúria";
        waveReached = "onda alcançada:";
        waveToBeat = "onda a ser vencida:";
        timeReached = "tempo alcançado:";
        timeToReach = "tempo para alcançar:";
        price = "Preço: ";

        ChangeText();
        portugeseFlag.SetActive(true);
    }
    #endregion


    #region Upgrade hover texts
    public static string upgradeHoverName, upgradeHoverDesc;

    public void SetUpgradeHoverText(string upgradeName)
    {
        hoverName = upgradeName;

        //Full game new texts
        #region dice
        if (upgradeName == "dice")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "reroll";
                upgradeHoverDesc = $"reroll your upgrades. {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound} left";

            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Würfeln";
                upgradeHoverDesc = $"Würfle neue Upgrades. {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound} übrig";
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "リロール";
                upgradeHoverDesc = $"アップグレードをリロール（残り{MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound}回）";
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "Relance ";
                upgradeHoverDesc = $"Relancez vos améliorations. Il vous reste {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound} relances.";
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "reroll";
                upgradeHoverDesc = $"cambia tus mejoras. quedan {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound}";
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "重新选择";
                upgradeHoverDesc = $"重新选择你的升级。剩余{MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound}次";
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "다시 굴리기";
                upgradeHoverDesc = $"업그레이드 다시 굴리기. 남은 횟수: {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound}";
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "перетасовать";
                upgradeHoverDesc = $"перетасовки улучшений. {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound} попытки";
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "przerzuć";
                upgradeHoverDesc = $"Przerzuć swoje ulepszenia. Pozostało: {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound}";
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "rolar novamente";
                upgradeHoverDesc = $"role novamente seus aprimoramentos. {MetaProgressionUpgrades.rerolls - PickUpgrade.rerollsThisRound} restantes";
            }
        }
        #endregion

        #region ladybug
        if (upgradeName == "allChance")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "ladybug";
                upgradeHoverDesc = $"increases the chance for all \"on slime click\" and \"on slime death\" upgrades to trigger";
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Marienkäfer";
                upgradeHoverDesc = $"Erhöht die Chance, dass alle „Bei Slime-Klick“ und „Bei Slime-Tod“ Upgrades ausgelöst werden";
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "てんとう虫";
                upgradeHoverDesc = $"「スライムクリック時」「スライム撃破時」発動系アップグレードの発動確率増加";
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "coccinelle";
                upgradeHoverDesc = $"augmente la chance de déclenchement de toutes les améliorations \"au kill slime\" et \"à la mort du slime\"";
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "mariquita";
                upgradeHoverDesc = $"aumenta la probabilidad de que se activen todas las mejoras \"al hacer click en slime\" y \"al matar slime\"";
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "瓢虫";
                upgradeHoverDesc = $"增加所有“点击史莱姆时”和“史莱姆死亡时”升级的几率来触发";
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "무당벌레";
                upgradeHoverDesc = $"모든 \"슬라임 클릭 시\" 및 \"슬라임 처치 시\" 업그레이드 발동 확률 증가";
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "божья коровка";
                upgradeHoverDesc = $"повышает шанс активации всех улучшений «при клике на слизь» и «при смерти слизи»";
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "biedronka";
                upgradeHoverDesc = $"zwiększa szansę na uruchomienie wszystkich ulepszeń „po kliknięciu slime” i „po śmierci slime”.";
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "joaninha";
                upgradeHoverDesc = $"aumenta a chance de todas as atualizações “ao clicar na gosma” e “ao morrer da gosma” serem ativadas";
            }
        }
        #endregion

        #region knife orbital
        if (upgradeName == "knifeOrbital")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "STABBY KNIFE";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"The strawberry will hold a knife and stab slimes every 3 seconds when they get close to the strawberry, dealing {PickUpgrade.knifeStabDamage} damage";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the knife stab damage from {PickUpgrade.knifeStabDamage} to {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "STICHIGES MESSER";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"Die Erdbeere hält ein Messer in der Hand und sticht alle 3 Sekunden auf Slimes ein, wenn sie sich der Erdbeere nähern, was  {PickUpgrade.knifeStabDamage} Schaden verursacht.";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden des Messerstichs von {PickUpgrade.knifeStabDamage} auf {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "スタビーナイフ";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"イチゴがナイフを持ち、3秒ごとに近づいたスライムを刺して{PickUpgrade.knifeStabDamage}ダメージを与える。";
                }
                else
                {
                    upgradeHoverDesc = $"ナイフのダメージが{PickUpgrade.knifeStabDamage}から{PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "STABBY KNIFE";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"La fraise tiendra un couteau et poignardera les slimes toutes les 3 secondes lorsqu’ils s’en approcheront, infligeant {PickUpgrade.knifeStabDamage} dégâts";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts de poignard de {PickUpgrade.knifeStabDamage} à {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "CUCHILLO APUÑALADOR";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"La fresa sostendrá un cuchillo y apuñalará a los slimes cada 3 segundos cuando se acerquen a ella, haciendo {PickUpgrade.knifeStabDamage} de daño";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de apuñalada del cuchillo de {PickUpgrade.knifeStabDamage} a {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "刺刀";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"当史莱姆靠近草莓时，草莓会握着刺刀每3秒刺杀史莱姆一次，造成{PickUpgrade.knifeStabDamage}点伤害";
                }
                else
                {
                    upgradeHoverDesc = $"刺刀的伤害从{PickUpgrade.knifeStabDamage}点提高到{PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "날카로운 칼";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"딸기가 칼을 들고 3초마다 근처 슬라임을 찌릅니다. 데미지{PickUpgrade.knifeStabDamage}";
                }
                else
                {
                    upgradeHoverDesc = $"칼 찌르기 데미지를 {PickUpgrade.knifeStabDamage} 에서{PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "КОЛЮЩИЙ НОЖ";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"Клубника держит нож, пронзая слизь при приближении каждые 3 сек., нанося {PickUpgrade.knifeStabDamage}ед. урона";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от удара ножом с {PickUpgrade.knifeStabDamage} до {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "OSTRY NÓŻ";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"Truskawka będzie trzymać nóż i dźgać slimy co 3 sekundy, zadając {PickUpgrade.knifeStabDamage} obrażeń.";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od ciosów nożem z {PickUpgrade.knifeStabDamage} do {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "FACA DE ESFAQUEAMENTO";

                if (PickUpgrade.choseKnifeOrbital == false)
                {
                    upgradeHoverDesc = $"O morango segura uma faca e apunhala as gosmas a cada 3 segundos quando elas se aproximam do morango, causando {PickUpgrade.knifeStabDamage} de dano";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano da faca de {PickUpgrade.knifeStabDamage} para {PickUpgrade.knifeStabDamage + PickUpgrade.knifeStabDamageIncrease}";
                }
            }
        }
        #endregion

        #region goo spike
        if (upgradeName == "Spike")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "goo spikes";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"when a slime is squished, the slime goo will spawn a small spike. the spike deals {PickUpgrade.spikeDamage} damage and lasts until the goo disappears";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the spike damage from {PickUpgrade.spikeDamage} to {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Schmodderstacheln";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"Wenn ein Slime zerquetscht wird, erzeugt der Schmodder einen kleinen Stachel. Der Stachel verursacht {PickUpgrade.spikeDamage} Schaden und hält an, bis der Schmodder verschwindet.";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden des Stachels von {PickUpgrade.spikeDamage} auf {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "スライムスパイク";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"スライムを潰すと、スライムの粘液から小さなトゲが発生。トゲは{PickUpgrade.spikeDamage}ダメージを与え、粘液が消えるまで持続する。";
                }
                else
                {
                    upgradeHoverDesc = $"トゲのダメージが{PickUpgrade.spikeDamage}から{PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "PICS DE GOO";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"Lorsqu’un slime est écrasé, la gelée qu’il laisse peut faire apparaître un petit pic. Ce pic inflige {PickUpgrade.spikeDamage} dégâts et reste en place jusqu’à la disparition de la gelée";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts du pic augmentés de {PickUpgrade.spikeDamage} à {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "picos de viscosidad";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"cuando un slime es aplastado, la viscosidad del slime generará un pequeño pico. el pico hace {PickUpgrade.spikeDamage} de daño y dura hasta que la viscosidad desaparece";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del pico de {PickUpgrade.spikeDamage} a {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "黏液尖刺";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"当史莱姆被粉碎掉时，史莱姆黏液会产生一个小尖刺。尖刺造成{PickUpgrade.spikeDamage}点伤害并持续到黏液消失为止";
                }
                else
                {
                    upgradeHoverDesc = $"尖刺的伤害从{PickUpgrade.spikeDamage}点提高到{PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "끈적한 가시";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"슬라임이 터질 때 끈적한 가시가 생성됩니다. 가시는 {PickUpgrade.spikeDamage} 데미지를 주며 끈적이가 사라질 때까지 유지됩니다.";
                }
                else
                {
                    upgradeHoverDesc = $"가시 데미지를 {PickUpgrade.spikeDamage}에서 {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}으로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "липкие шипы";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"после раздавливания, из слизи появится малый шип. Шип наносит {PickUpgrade.spikeDamage} ед. урона, исчезая вместе со слизью.";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от шипов с {PickUpgrade.spikeDamage} до {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "kolec z mazi";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"Kiedy slime zostanie zgnieciony, z jego pozostałości odrodzi się mały kolec. Kolec zadaje {PickUpgrade.spikeDamage} obrażenia i trwa do momentu zniknięcia maź.";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od kolców z {PickUpgrade.spikeDamage} do {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease} pkt.";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "gosma espinhosa";

                if (PickUpgrade.choseSpikes == false)
                {
                    upgradeHoverDesc = $"Quando uma gosma é esmagada, a gosma viscosa gera um espinho pequeno. O espinho causa {PickUpgrade.spikeDamage} de dano e dura até que a gosma desapareça.";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do espinho de {PickUpgrade.spikeDamage} para {PickUpgrade.spikeDamage + PickUpgrade.spikeDamageIncrease}";
                }
            }
        }
        #endregion

        #region Big laser 
        if (upgradeName == "BigLaser")
        {
            float laserTimer = PickUpgrade.bigLaserTimer;
            float laserTimerUpgrade = PickUpgrade.bigLaserTimer - PickUpgrade.bigLaserTimerDecrease;

            if (languageSelected == 1) //English
            {
                upgradeHoverName = "big Laser gun";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: A big laser gun will spin around the strawberry and shoot a big laser every {laserTimer.ToString("F0")} seconds. the laser deals {PickUpgrade.bigLaserDamage} damage and delets enemy bullets";
                }
                else
                {
                    upgradeHoverDesc = $"decrease the laser shoot time from {laserTimer.ToString("F1")} sec to {laserTimerUpgrade.ToString("F1")} sec and increases the big laser damage from {PickUpgrade.bigLaserDamage} to {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "große Laserkanone";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Eine große Laserkanone dreht sich um die Erdbeere und schießt alle {laserTimer.ToString("F0")} Sekunden einen großen Laser ab. Der Laser verursacht {PickUpgrade.bigLaserDamage} Schaden und zerstört sogar gegnerische Projektile";
                }
                else
                {
                    upgradeHoverDesc = $"Verringert die Laserschusszeit von {laserTimer.ToString("F1")} Sekunden auf {laserTimerUpgrade.ToString("F1")} Sekunden und erhöht den Schaden des großen Lasers von {PickUpgrade.bigLaserDamage} auf {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ビッグレーザーガン";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"イチゴの周囲を回る大型レーザー砲が{laserTimer.ToString("F0")}秒ごとにレーザーを発射。レーザーは{PickUpgrade.bigLaserDamage}ダメージを与え、敵の弾を消す。";
                }
                else
                {
                    upgradeHoverDesc = $"レーザーの発射間隔が{laserTimer.ToString("F1")}秒から{laserTimerUpgrade.ToString("F1")}秒に短縮され、ダメージが{PickUpgrade.bigLaserDamage}から{PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "MEGA LASER";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Satellite fraise : Un gros laser tournera autour de la fraise et tirera un laser toutes les {laserTimer.ToString("F0")} secondes. Il inflige {PickUpgrade.bigLaserDamage} dégâts et détruit les projectiles ennemis";
                }
                else
                {
                    upgradeHoverDesc = $"Délai réduit à {laserTimerUpgrade.ToString("F1")} sec, dégâts augmentés à {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "pistola láser grande";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: Una pistola láser grande girará alrededor de la fresa y disparará un gran láser cada {laserTimer.ToString("F0")} segundos. el láser hace {PickUpgrade.bigLaserDamage} de daño y elimina las balas";
                }
                else
                {
                    upgradeHoverDesc = $"reduce el tiempo de disparo del láser de {laserTimer.ToString("F1")} seg a {laserTimerUpgrade.ToString("F1")} seg y aumenta el daño del láser grande de {PickUpgrade.bigLaserDamage} a {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "大型激光枪";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"草莓轨道：一把大型激光枪会围绕草莓旋转，每{laserTimer.ToString("F0")}秒发射一次大束激光。激光造成{PickUpgrade.bigLaserDamage}点伤害并清除敌人的子弹";
                }
                else
                {
                    upgradeHoverDesc = $"激光发射时间从{laserTimer.ToString("F1")}秒减少到{laserTimerUpgrade.ToString("F1")}秒，大束激光伤害从{PickUpgrade.bigLaserDamage}秒增加到{PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}秒";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "거대 레이저 건";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: 거대한 레이저 건이 딸기 주변을 돌며 {laserTimer.ToString("F0")}초마다 레이저를 발사합니다. 레이저는 {PickUpgrade.bigLaserDamage} 데미지를 주며 적 탄환을 삭제합니다.";
                }
                else
                {
                    upgradeHoverDesc = $"레이저 발사 시간을 {laserTimer.ToString("F1")} 초에서 {laserTimerUpgrade.ToString("F1")} 초로 줄이고 거대 레이저 데미지를 {PickUpgrade.bigLaserDamage}에서 {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}으로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "большая лазерная пушка";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Орбита клубники: большая лазерная пушка вращается вокруг клубники, стреляя мощным лазером каждые {laserTimer.ToString("F0")} сек. лазер наносит {PickUpgrade.bigLaserDamage} ед. урона, уничтожая вражеские пули";
                }
                else
                {
                    upgradeHoverDesc = $"сокращает время выстрела лазера с {laserTimer.ToString("F1")} до {laserTimerUpgrade.ToString("F1")} сек., повышая урон мощного лазера с {PickUpgrade.bigLaserDamage} до {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "duży pistolet laserowy";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: Duże działo laserowe będzie obracać się wokół truskawki i strzelać co {laserTimer.ToString("F0")} sekund. Laser zadaje {PickUpgrade.bigLaserDamage} obrażeń i usuwa pociski wroga.";
                }
                else
                {
                    upgradeHoverDesc = $"skrócenie czasu strzału laserowego z {laserTimer.ToString("F1")} s do {laserTimerUpgrade.ToString("F1")} s i zwiększenie obrażeń zadawanych przez duży laser z {PickUpgrade.bigLaserDamage} do {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "arma laser grande";

                if (PickUpgrade.choseBigLaser == false)
                {
                    upgradeHoverDesc = $"Orbital de morango: Uma grande arma laser girará em torno do morango e disparará um grande laser a cada {laserTimer.ToString("F0")}  segundos. O laser causa {PickUpgrade.bigLaserDamage} de dano e elimina as balas inimigas";
                }
                else
                {
                    upgradeHoverDesc = $"Diminui o tempo de disparo do laser de {laserTimer.ToString("F1")} segundos para {laserTimerUpgrade.ToString("F1")} segundos e aumenta o dano do laser grande de {PickUpgrade.bigLaserDamage} para {PickUpgrade.bigLaserDamage + PickUpgrade.bigLaserDamageIncrease}";
                }
            }
        }
        #endregion

        #region meteor
        if (upgradeName == "Meteor")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "anvil";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"on slime click: chance to spawn an anvil that falls from the sky and damages slimes in an big area, dealing {PickUpgrade.meteorDamage} damage. targets shooting and big slimes. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the anvil damage from {PickUpgrade.meteorDamage} to {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Amboss";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, einen Amboss zu erzeugen, der vom Himmel fällt und Slimes in einem großen Gebiet beschädigt und {PickUpgrade.meteorDamage} Schaden verursacht. Zielt auf schießende und große Slimes. Verbraucht 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden des Ambosses von {PickUpgrade.meteorDamage} auf  {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "アンビル";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 一定確率で空から金床（アンビル）が降り、大範囲のスライムに{PickUpgrade.meteorDamage}ダメージを与える。射撃スライムと大型スライムを優先して狙う。アップグレードスロットを1つ使用！";
                }
                else
                {
                    upgradeHoverDesc = $"アンビルのダメージが{PickUpgrade.meteorDamage}から{PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "ENCLUME";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"Au clic par slime : chance de faire tomber une enclume du ciel infligeant {PickUpgrade.meteorDamage} dégâts sur une grande zone. Cible les slimes tireurs et les slimes géants. Utilise 1 emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.meteorDamage} à {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "yunque";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"al hacer click en un slime: probabilidad de generar un yunque que cae del cielo y daña a los slimes en un área grande, haciendo {PickUpgrade.meteorDamage} de daño. apunta a slimes que disparan y slimes grandes. ¡usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del yunque de {PickUpgrade.meteorDamage} a {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "铁砧";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率引发一个从天而降的铁砧，对大片区域内的史莱姆造成{PickUpgrade.meteorDamage}点伤害。瞄准射击，可伤害大个史莱姆。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"铁砧伤害从{PickUpgrade.meteorDamage}点提高到{PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "모루";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 하늘에서 모루가 떨어져 넓은 범위의 슬라임에게 {PickUpgrade.meteorDamage}데미지를 줄 확률이 있습니다. 사격 슬라임 및 거대 슬라임을 대상으로 합니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"모루 데미지를 {PickUpgrade.meteorDamage}에서 {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "наковальня";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"при клике на слизь: шанс вызвать наковальню, которая падает с неба и повреждает слизь на площади, нанося {PickUpgrade.meteorDamage} ед. урона. стрельба по целям и большой слизи. использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от наковальни с {PickUpgrade.meteorDamage} до {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "kowadło";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"Po kliknięciu slime: szansa na przywołanie kowadła, które spada z nieba i zadaje {PickUpgrade.meteorDamage} obrażeń slimom na dużym obszarze. celuje w strzelające i duże slimy. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zadawane przez kowadło z {PickUpgrade.meteorDamage} do {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease} pkt.";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "bigorna";

                if (PickUpgrade.choseMeteor == false)
                {
                    upgradeHoverDesc = $"ao clicar na gosma: chance de gerar uma bigorna que cai do céu e causa dano às gosmas em uma grande área, causando {PickUpgrade.meteorDamage} de dano. visa gosmas grandes e que atiram. usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano da bigorna de {PickUpgrade.meteorDamage} para {PickUpgrade.meteorDamage + PickUpgrade.meteorDamageIncrease}";
                }
            }
        }
        #endregion

        #region kuni
        if (upgradeName == "Kunai")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "Kunai";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"On slime death: chance to shoot 2 kunai that targets random slimes, dealing {PickUpgrade.kunaiDamage} damage. There is also a {PickUpgrade.kunaiInstaKill}% chance to instantly kill the slime (except for bosses). uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the kunai damage from {PickUpgrade.kunaiDamage} to {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease} and insta kill chance from {PickUpgrade.kunaiInstaKill}% to {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Kunai";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Tod: Chance, 2 Kunai zu schießen, die auf zufällige Slimes zielen und {PickUpgrade.kunaiDamage} Schaden verursachen. Es besteht auch eine Chance von {PickUpgrade.kunaiInstaKill}%, den Slime sofort zu töten (außer bei Bossen). Verbraucht 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Kunai-Schaden von {PickUpgrade.kunaiDamage} auf {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease}  und die Insta-Kill-Chance von {PickUpgrade.kunaiInstaKill}% auf {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "クナイ";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"スライム撃破時： 一定確率でランダムなスライムを狙うクナイを2本発射し、{PickUpgrade.kunaiDamage}ダメージを与える。また、 {PickUpgrade.kunaiInstaKill}%の確率で即死（ボスには無効）。アップグレードスロットを1つ使用！";
                }
                else
                {
                    upgradeHoverDesc = $"クナイのダメージが{PickUpgrade.kunaiDamage}から{PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease}に増加し、即死確率が{PickUpgrade.kunaiInstaKill}%から{PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%に上昇";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "Kunai";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"À la mort d’un slime : chance de lancer 2 kunais ciblant des slimes aléatoires, infligeant {PickUpgrade.kunaiDamage} dégâts. {PickUpgrade.kunaiInstaKill} % de chance d’élimination instantanée (hors boss). Utilise 1 emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés à {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease}, chance de kill instant à {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease} %";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "Kunai";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"Al morir un slime: probabilidad de disparar 2 kunais que apuntan a slimes aleatorios, haciendo {PickUpgrade.kunaiDamage} de daño. También hay un {PickUpgrade.kunaiInstaKill}% de probabilidad de matar instantáneamente al slime (excepto jefes). ¡usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del kunai de {PickUpgrade.kunaiDamage} a {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease} y la probabilidad de matar instantáneamente de {PickUpgrade.kunaiInstaKill}% a {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "苦无";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"击杀史莱姆：有几率射出2个瞄准随机史莱姆的苦无，造成{PickUpgrade.kunaiDamage}点伤害。也有{PickUpgrade.kunaiInstaKill}%的几率瞬间击杀史莱姆（史莱姆首领除外）。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"苦无伤害从{PickUpgrade.kunaiDamage}点提高到{PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease}点，瞬间击杀几率从{PickUpgrade.kunaiInstaKill}%提高到{PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "쿠나이";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"슬라임 처치 시: 랜덤 슬라임을 대상으로 {PickUpgrade.kunaiDamage} 데미지를 주는 쿠나이 2개를 발사할 확률이 있습니다. {PickUpgrade.kunaiInstaKill}% 확률로 슬라임을 즉시 처치합니다(보스 제외). 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"쿠나이 데미지를 {PickUpgrade.kunaiDamage}에서 {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease}로, 즉시 처치 확률을 {PickUpgrade.kunaiInstaKill}%에서 {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "Кунай";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"При смерти слизи: шанс выпустить 2 куная, которые поражают случайную слизь, нанося {PickUpgrade.kunaiDamage} ед. урона. Также есть {PickUpgrade.kunaiInstaKill}% шанс мгновенно убить слизь (кроме боссов). Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от куная с {PickUpgrade.kunaiDamage} до {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease} ед., а шанс на мгновенное убийство — с {PickUpgrade.kunaiInstaKill}% до {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "Kunai";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"Po śmierci slime: szansa na wystrzelenie 2 kunai, które celują w losowe slimy, zadając {PickUpgrade.kunaiDamage} obrażenia. {PickUpgrade.kunaiInstaKill}% szans na natychmiastowe zabicie slimów (z wyjątkiem bossów). wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zadawane przez kunai z {PickUpgrade.kunaiDamage} do {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease} i szansę na natychmiastowe zabicie z {PickUpgrade.kunaiInstaKill}% do {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Kunai";

                if (PickUpgrade.choseKunai == false)
                {
                    upgradeHoverDesc = $"Na morte da gosma: chance de disparar 2 kunai que têm como alvo gosmas aleatórias, causando {PickUpgrade.kunaiDamage} de dano. Há também {PickUpgrade.kunaiInstaKill}% de chance de matar instantaneamente a gosma (exceto os chefes). usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano da kunai de {PickUpgrade.kunaiDamage} para {PickUpgrade.kunaiDamage + PickUpgrade.kunaiDamageIncrease} e a chance de morte instantânea de {PickUpgrade.kunaiInstaKill}% para {PickUpgrade.kunaiInstaKill + PickUpgrade.kunaiIntaKillIncrease}%";
                }
            }
        }
        #endregion

        #region spiky shield
        if (upgradeName == "SpikyShield")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "Spiky shield";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: a small spiky shield will spin around the strawberry, blocking bullets and dealing {PickUpgrade.spikyShieldDamage} damage to slimes.";
                }
                else
                {
                    upgradeHoverDesc = $"increases the spiky shield size and increases the damage from {PickUpgrade.spikyShieldDamage} to {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Stchelschild";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Ein kleiner stacheliger Schild dreht sich um die Erdbeere, blockiert Kugeln und fügt Slimes {PickUpgrade.spikyShieldDamage} Schaden zu.";
                }
                else
                {
                    upgradeHoverDesc = $"Vergrößert den Stachelschild und erhöht den Schaden von {PickUpgrade.spikyShieldDamage} auf {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "スパイキーシールド";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"小さなトゲのシールドがストロベリーの周りを回転し、弾を防ぎ、スライムに{PickUpgrade.spikyShieldDamage}ダメージを与える。";
                }
                else
                {
                    upgradeHoverDesc = $"シールドのサイズが拡大し、ダメージが{PickUpgrade.spikyShieldDamage}から{PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "BOUCLIER PIQUANT";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Satellite fraise : un petit bouclier piquant tourne autour de la fraise, bloque les projectiles et inflige {PickUpgrade.spikyShieldDamage} dégâts aux slimes";
                }
                else
                {
                    upgradeHoverDesc = $"Taille et dégâts augmentés de {PickUpgrade.spikyShieldDamage} à {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "Escudo con picos";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: un pequeño escudo con picos girará alrededor de la fresa, bloqueando balas y haciendo {PickUpgrade.spikyShieldDamage} de daño a los slimes";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta el tamaño del escudo con picos y aumenta el daño de {PickUpgrade.spikyShieldDamage} a {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "尖刺盾";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"草莓轨道：小尖刺盾会围绕草莓旋转，阻挡子弹并对史莱姆造成{PickUpgrade.spikyShieldDamage}点伤害";
                }
                else
                {
                    upgradeHoverDesc = $"增加尖刺盾的尺寸，尖刺盾伤害从{PickUpgrade.spikyShieldDamage}点提高到{PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "뾰족한 방패";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: 작은 뾰족한 방패가 딸기 주변을 돌며 탄환을 막고 슬라임에게 {PickUpgrade.spikyShieldDamage} 데미지를 줍니다.";
                }
                else
                {
                    upgradeHoverDesc = $"뾰족한 방패 크기를 늘리고 데미지를 {PickUpgrade.spikyShieldDamage}에서 {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "Шипастый щит";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Орбита клубники: малый шипастый щит вращается вокруг клубники, блокируя пули и нанося {PickUpgrade.spikyShieldDamage} ед. урона слизи";
                }
                else
                {
                    upgradeHoverDesc = $"увеличивает размер шипастого щита и повышает урон с {PickUpgrade.spikyShieldDamage} до {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "Kolczasta tarcza";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: mała kolczasta tarcza będzie obracać się wokół truskawki, blokując pociski i zadając {PickUpgrade.spikyShieldDamage} obrażenia slimom.";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa rozmiar kolczastej tarczy i zwiększa obrażenia z {PickUpgrade.spikyShieldDamage} do {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Escudo espinhoso";

                if (PickUpgrade.choseSpikyShield == false)
                {
                    upgradeHoverDesc = $"Orbital de Morango: um pequeno escudo pontiagudo girará em torno do morango, bloqueando balas e causando {PickUpgrade.spikyShieldDamage}  de dano a gosmas";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta o tamanho do escudo espinhoso e aumenta o dano de {PickUpgrade.spikyShieldDamage} para {PickUpgrade.spikyShieldDamage + PickUpgrade.spikyShieldDamageIncrease}";
                }
            }
        }
        #endregion

        #region friendly bullets
        if (upgradeName == "FriendlyBullets")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "friendly bullets";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"shooting slimes have a chance to shoot a friendly bullet. friendly bullets will target a random slime, dealing {PickUpgrade.friendlyBulletsDamage} damage";
                }
                else
                {
                    upgradeHoverDesc = $"increase the friendly bullets damage from {PickUpgrade.friendlyBulletsDamage} to {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Freundliche Geschosse";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"Schießende Slimes haben die Chance, eine eigenes Geschoss abzuschießen. Ein eigenes Geschoss zielt auf einen zufälligen Slime und verursacht {PickUpgrade.friendlyBulletsDamage} Schaden.";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden der eigenen Geschosse von {PickUpgrade.friendlyBulletsDamage} auf {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "フレンドリーブレット";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"射撃スライムが一定確率で「味方弾」を発射。 味方弾はランダムなスライムを狙い、{PickUpgrade.friendlyBulletsDamage}ダメージを与える。";
                }
                else
                {
                    upgradeHoverDesc = $"味方弾のダメージが{PickUpgrade.friendlyBulletsDamage}から{PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "BALLES AMIES";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"Les slimes tireurs ont une chance de tirer une balle amie : elle cible un slime ennemi aléatoire et inflige{PickUpgrade.friendlyBulletsDamage} dégâts";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.friendlyBulletsDamage} à {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "balas amistosas";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"los slimes que disparan tienen probabilidad de disparar una bala amistosa. las balas amistosas apuntarán a un slime aleatorio, haciendo {PickUpgrade.friendlyBulletsDamage} de daño";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta el daño de las balas amistosas de {PickUpgrade.friendlyBulletsDamage} a {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "友军子弹";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"射击史莱姆有几率射出友军子弹。友军子弹会随机瞄准一个史莱姆，造成{PickUpgrade.friendlyBulletsDamage}点伤害";
                }
                else
                {
                    upgradeHoverDesc = $"友军子弹伤害从{PickUpgrade.friendlyBulletsDamage}点提高到{PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "아군 탄환";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"슬라임 사격 시 아군 탄환을 발사할 확률이 있습니다. 아군 탄환은 랜덤 슬라임을 대상으로 {PickUpgrade.friendlyBulletsDamage} 데미지를 줍니다.";
                }
                else
                {
                    upgradeHoverDesc = $"아군 탄환 데미지를 {PickUpgrade.friendlyBulletsDamage}에서 {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "дружественные пули";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"у стреляющей слизи есть шанс на дружественные пули, которые поражают случайную слизь, нанося {PickUpgrade.friendlyBulletsDamage} ед. урона";
                }
                else
                {
                    upgradeHoverDesc = $"повышает урон дружественных пуль с {PickUpgrade.friendlyBulletsDamage} до {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "przyjazne kule";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"strzelające slimy mają szansę wystrzelić przyjazny pocisk. Takie pociski lecą w losowego slima, zadając mu {PickUpgrade.friendlyBulletsDamage} obrażeń.";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zadawane przez przyjazne pociski z {PickUpgrade.friendlyBulletsDamage} do {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "balas amigáveis";

                if (PickUpgrade.choseFriendlyBullets == false)
                {
                    upgradeHoverDesc = $"as gosmas que atiram têm a chance de disparar uma bala amiga. as balas amigas terão como alvo uma gosma aleatória, causando {PickUpgrade.friendlyBulletsDamage} de dano";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta o dano das balas amigas de {PickUpgrade.friendlyBulletsDamage} para {PickUpgrade.friendlyBulletsDamage + PickUpgrade.friendlyBulletsIncrease}";
                }
            }
        }
        #endregion'

        #region saw blade
        if (upgradeName == "SawBlade")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "saw blades";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"On slime click: Chance to shoot 1-3 spinning saw blades. Each saw blade moves across the screen from one side to the other, piercing all slimes and deals {PickUpgrade.sawBladeDamage} damage. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"increase the saw blade damage from {PickUpgrade.sawBladeDamage} to {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Sägeblätter";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, 1-3 sich drehende Sägeblätter abzuschießen. Jedes Sägeblatt bewegt sich über den Bildschirm von einer Seite zur anderen, durchbohrt alle Schleime und verursacht {PickUpgrade.sawBladeDamage} Schaden. verbraucht 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöhung des Sägeblattschadens von {PickUpgrade.sawBladeDamage} auf {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ソーブレード";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 一定確率で1～3枚の回転ノコギリを発射。ノコギリは画面を横切り、貫通しながらスライムに{PickUpgrade.sawBladeDamage}ダメージを与える。アップグレードスロットを1つ使用！";
                }
                else
                {
                    upgradeHoverDesc = $"ノコギリのダメージが{PickUpgrade.sawBladeDamage}から{PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "LAMES SCIES";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"Au clic sur un slime : chance de tirer 1 à 3 lames circulaires qui traversent l'écran en transperçant tous les slimes et infligeant {PickUpgrade.sawBladeDamage} dégâts. Utilise 1 emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.sawBladeDamage} à {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "cuchillas de sierra";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"Al hacer click en un slime: Probabilidad de disparar 1-3 cuchillas de sierra giratorias. Cada cuchilla se mueve a través de la pantalla de un lado a otro, atravesando todos los slimes y hace {PickUpgrade.sawBladeDamage} de daño. ¡usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta el daño de la cuchilla de sierra de {PickUpgrade.sawBladeDamage} a {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "锯片";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率射出1-3个旋转锯片。每个锯片从屏幕一侧锯动到另一侧，割掉所有史莱姆并造成{PickUpgrade.sawBladeDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"锯片伤害从{PickUpgrade.sawBladeDamage}点提高到{PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "톱날";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 1-3개의 회전 톱날을 발사할 확률이 있습니다. 각 톱날은 화면 한쪽에서 다른 쪽으로 이동하며 모든 슬라임을 관통하고 {PickUpgrade.sawBladeDamage}데미지를 줍니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"톱날 데미지를 {PickUpgrade.sawBladeDamage}에서 {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}로 증가.";
                }

            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "дисковые пилы";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"При клике на слизь: шанс выпустить 1-3 вращающиеся дисковые пилы. Каждая пила движется по экрану из стороны в сторону, пронзая всю слизь и нанося {PickUpgrade.sawBladeDamage} ед. урона. использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"повышает урон дисковой пилы с {PickUpgrade.sawBladeDamage} до {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "ostrza pił";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"Po kliknięciu slime: Szansa na wystrzelenie 1-3 wirujących ostrzy piły. Każde ostrze piły przesuwa się po ekranie z jednej strony na drugą, przebijając wszystkie slimy i zadając {PickUpgrade.sawBladeDamage} obrażenia. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"zwiększenie obrażeń pił z {PickUpgrade.sawBladeDamage} do {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "lâminas de serra";

                if (PickUpgrade.choseSawBlade == false)
                {
                    upgradeHoverDesc = $"Ao clicar na gosma: Chance de disparar de 1 a 3 lâminas de serra giratórias. Cada lâmina de serra se move pela tela de um lado para o outro, perfurando todas as gosmas e causando {PickUpgrade.sawBladeDamage} de dano. usa 1 slot de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta o dano da lâmina de serra de {PickUpgrade.sawBladeDamage} para {PickUpgrade.sawBladeDamage + PickUpgrade.sawBladeDamageIncrease}";
                }
            }
        }
        #endregion

        #region katana
        if (upgradeName == "Katana")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "katana";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"On slime click: chance to spawn a katana that slashes in a circle and shoots in a random direction, dealing {PickUpgrade.katanaDamage} damage. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"increase the katana damage from {PickUpgrade.katanaDamage} to {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "katana";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, ein Katana zu erzeugen, das in einem Kreis aufschlitzt und in eine zufällige Richtung schießt und {PickUpgrade.katanaDamage} Schaden verursacht. Verbraucht 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöhung des Katanaschadens von {PickUpgrade.katanaDamage} auf {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "刀";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 一定確率で刀を召喚し、円形に斬撃を放ちつつランダムな方向に飛ぶ。{PickUpgrade.katanaDamage}ダメージを与える。アップグレードスロットを1つ使用！";
                }
                else
                {
                    upgradeHoverDesc = $"刀のダメージが{PickUpgrade.katanaDamage}から{PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "katana";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"Au clic sur un slime : chance de faire apparaître un katana qui tranche en cercle et tire dans une direction aléatoire, infligeant {PickUpgrade.katanaDamage} dégâts. Utilise 1 emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.katanaDamage} à {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "katana";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"Al hacer click en un slime: probabilidad de generar una katana que golpea en círculo y dispara en una dirección aleatoria, haciendo {PickUpgrade.katanaDamage} de daño. ¡usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta el daño de la katana de {PickUpgrade.katanaDamage} a {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "武士刀";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率生产一把武士刀，武士刀会划出一个圆圈并向随机方向射出，造成{PickUpgrade.katanaDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"武士刀伤害从{PickUpgrade.katanaDamage}点提高到{PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "카타나";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 원형으로 베고 랜덤 방향으로 발사하는 카타나를 생성할 확률이 있습니다. 데미지 {PickUpgrade.katanaDamage}. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"카타나 데미지를 {PickUpgrade.katanaDamage}에서 {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "катана";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"При клике на слизь: шанс вызвать катану, которая рубит по кругу, стреляя в случайном направлении и нанося {PickUpgrade.katanaDamage} ед. урона. использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"повышает урон катаны с {PickUpgrade.katanaDamage} до {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "katana";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"Po kliknięciu slime: szansa na stworzenie katany, która tnie i strzela w losowym kierunku, zadając {PickUpgrade.katanaDamage} obrażeń. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"zwiększenie obrażeń zadawanych kataną z {PickUpgrade.katanaDamage} do {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "katana";

                if (PickUpgrade.choseKatana == false)
                {
                    upgradeHoverDesc = $"Ao clicar na gosma: chance de gerar uma katana que corta em círculo e dispara em uma direção aleatória, causando {PickUpgrade.katanaDamage} de dano. usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta o dano da katana de {PickUpgrade.katanaDamage} para {PickUpgrade.katanaDamage + PickUpgrade.katanaDamageIncrease}";
                }
            }
        }
        #endregion

        #region chained blade
        if (upgradeName == "chainBlade")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "chained blade";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: A blade attached to a chain will spin around the strawberry, dealing {PickUpgrade.bladeBleedDamage.ToString("F1")} bleed damage for 3 seconds. the blade has a {PickUpgrade.bladeInstaKillChance.ToString("F0")}% chance to instantly kill the slime (except for bosses)";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the chained blade bleed damage from {PickUpgrade.bladeBleedDamage.ToString("F1")} to {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")} and the insta kill chance from {PickUpgrade.bladeInstaKillChance.ToString("F0")}% to {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%. also increases the spin speed";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Verkettete Klinge";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Eine Klinge, die an einer Kette befestigt ist, dreht sich um die Erdbeere und verursacht 3 Sekunden lang {PickUpgrade.bladeBleedDamage.ToString("F1")} Blutungsschaden. Die Klinge hat eine Chance von {PickUpgrade.bladeInstaKillChance.ToString("F0")}%, den Slime sofort zu töten (außer bei Bossen).";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Blutungsschaden der verketteten Klinge von {PickUpgrade.bladeBleedDamage.ToString("F1")} auf {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")} und die Insta-Kill-Chance von {PickUpgrade.bladeInstaKillChance.ToString("F0")}% auf {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%. Erhöht auch die Schleudergeschwindigkeit";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "チェインブレード";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"イチゴの周囲を回る鎖付きの刃が、スライムに3秒間{PickUpgrade.bladeBleedDamage.ToString("F1")}ダメージの出血効果を与える。 {PickUpgrade.bladeInstaKillChance.ToString("F0")}%の確率で即死（ボスには無効）。";
                }
                else
                {
                    upgradeHoverDesc = $"出血ダメージが{PickUpgrade.bladeBleedDamage.ToString("F1")}から{(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")}に増加、即死確率が{PickUpgrade.bladeInstaKillChance.ToString("F0")}%から{(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%に増加、回転速度が上昇";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "LAME ENCHAÎNÉE";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Satellite fraise : Une lame attachée à une chaîne tourne autour de la fraise, infligeant {PickUpgrade.bladeBleedDamage.ToString("F1")} dégâts de saignement pendant 3 secondes. Elle a {PickUpgrade.bladeInstaKillChance.ToString("F0")} % de chance de tuer instantanément un slime (hors boss)";
                }
                else
                {
                    upgradeHoverDesc = $"Saignement augmenté de {PickUpgrade.bladeBleedDamage.ToString("F1")} à {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")}, chance de kill instantané à {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")} %, vitesse de rotation améliorée";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "hoja encadenada";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: Una hoja atada a una cadena girará alrededor de la fresa, causando {PickUpgrade.bladeBleedDamage.ToString("F1")} de daño por sangrado durante 3 segundos. la hoja tiene un {PickUpgrade.bladeInstaKillChance.ToString("F0")}% de probabilidad de matar instantáneamente al slime (excepto jefes)";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño por sangrado de la hoja encadenada de {PickUpgrade.bladeBleedDamage.ToString("F1")} a {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")} y la probabilidad de matar instantáneamente de {PickUpgrade.bladeInstaKillChance.ToString("F0")}% a {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%. también aumenta la velocidad de giro";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "链式刀";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"草莓轨道：附在链条上的刀片会围绕草莓旋转，造成{PickUpgrade.bladeBleedDamage.ToString("F1")}点失血伤害并持续3秒。刀片有{PickUpgrade.bladeInstaKillChance.ToString("F0")}%的几率瞬间击杀史莱姆（史莱姆首领除外）";
                }
                else
                {
                    upgradeHoverDesc = $"链式刀的失血伤害从{PickUpgrade.bladeBleedDamage.ToString("F1")}点提高到{(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")}点，瞬间击杀几率从{PickUpgrade.bladeInstaKillChance.ToString("F0")}%提高到{(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%。旋转速度也会提高";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "사슬낫";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: 사슬에 연결된 낫이 딸기 주변을 돌며 3초 동안{PickUpgrade.bladeBleedDamage.ToString("F1")} 출혈 데미지를 줍니다. 낫은 {PickUpgrade.bladeInstaKillChance.ToString("F0")}% 확률로 슬라임을 즉시 처치합니다(보스 제외).";
                }
                else
                {
                    upgradeHoverDesc = $"사슬낫 출혈 데미지를 {PickUpgrade.bladeBleedDamage.ToString("F1")}에서 {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")}으로, 즉시 처치 확률을{PickUpgrade.bladeInstaKillChance.ToString("F0")}%에서 {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%로 증가합니다. 회전 속도도 증가합니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "лезвие на цепи";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Орбита клубники: лезвие на цепи вращается вокруг клубники, нанося {PickUpgrade.bladeBleedDamage.ToString("F1")} ед. урона от кровотечения за 3 сек. у лезвия есть {PickUpgrade.bladeInstaKillChance.ToString("F0")}% шанс мгновенно убить слизь (кроме боссов)";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон лезвия на цепи от кровотечения с {PickUpgrade.bladeBleedDamage.ToString("F1")} до {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")} ед., а шанс на мгновенное убийство — с {PickUpgrade.bladeInstaKillChance.ToString("F0")}% до {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%. Также повышает скорость вращения";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "ostrze łańcuchowe";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: Ostrze łańcuchowe będzie obracać się wokół truskawki, zadając {PickUpgrade.bladeBleedDamage.ToString("F1")} obrażenia od krwawienia przez 3 sekundy. Ostrze ma {PickUpgrade.bladeInstaKillChance.ToString("F0")}% szans na natychmiastowe zabicie slime (z wyjątkiem bossów).";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od krwawienia zadawane przez ostrze łańcuchowe z {PickUpgrade.bladeBleedDamage.ToString("F1")} do {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")} oraz szansę na natychmiastowe zabójstwo z {PickUpgrade.bladeInstaKillChance.ToString("F0")}% do {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%. zwiększa również prędkość wirowania";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Lâmina acorrentada";

                if (PickUpgrade.choseBlade == false)
                {
                    upgradeHoverDesc = $"Orbital de morango: Uma lâmina presa a uma corrente girará em torno do morango, causando {PickUpgrade.bladeBleedDamage.ToString("F1")} de dano de sangramento por 3 segundos. A lâmina tem {PickUpgrade.bladeInstaKillChance.ToString("F0")}% de chance de matar instantaneamente a gosma (exceto para chefes)";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano de sangramento da lâmina acorrentada de {PickUpgrade.bladeBleedDamage.ToString("F1")} para {(PickUpgrade.bladeBleedDamage + PickUpgrade.bladeBleedDamageIncrease).ToString("F1")} e a chance de matar instantaneamente de {PickUpgrade.bladeInstaKillChance.ToString("F0")}% para {(PickUpgrade.bladeInstaKillChance + PickUpgrade.bladeInstaKillChanceIncrease).ToString("F0")}%. Também aumenta a velocidade de giro";
                }
            }
        }
        #endregion

        #region nail gun
        if (upgradeName == "nailGun")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "nail gun";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: shoots a nail every {PickUpgrade.nailGunTimer} second, dealing {PickUpgrade.nailGunBleedDamage} bleed damage for 3 seconds. slimes hit by a nail will reduce their movement speed by {PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the nail bleed damage from {PickUpgrade.nailGunBleedDamage} to {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}, decreases nail gun shoot time from {PickUpgrade.nailGunTimer.ToString("F2")} sec to {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} sec and increases the speed reduction from {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% to {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Nagelpistole";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: schießt alle {PickUpgrade.nailGunTimer} Sekunde einen Nagel ab, der 3 Sekunden lang {PickUpgrade.nailGunBleedDamage} Blutungsschaden verursacht. Von einem Nagel getroffene Slimes reduzieren ihre Bewegungsgeschwindigkeit um 30%. {PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden durch Nagelblutung von {PickUpgrade.nailGunBleedDamage} auf {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}, erringert die Schusszeit der Nagelpistole von {PickUpgrade.nailGunTimer.ToString("F2")} Sekunde auf  {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} Sekunden und erhöht die Geschwindigkeitsreduzierung von {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% auf {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ネイルガン";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"イチゴの周囲を回る武器 で、{PickUpgrade.nailGunTimer}秒ごとに釘を発射し、3秒間{PickUpgrade.nailGunBleedDamage}ダメージの出血効果を与える。釘が当たったスライムの移動速度を{PickUpgrade.nailGunMovementSpeed}%低下させる。";
                }
                else
                {
                    upgradeHoverDesc = $"出血ダメージが{PickUpgrade.nailGunBleedDamage}から{PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}に増加、発射間隔が{PickUpgrade.nailGunTimer.ToString("F2")}秒から{(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")}秒に短縮、移動速度低下率が{PickUpgrade.nailGunMovementSpeed.ToString("F0")}%から{(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "PISTOLET À CLOUS";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Satellite fraise : tire un clou toutes les {PickUpgrade.nailGunTimer} seconde, inflige {PickUpgrade.nailGunBleedDamage} dégâts de saignement pendant 3 secondes. Les slimes touchés sont ralentis de {PickUpgrade.nailGunMovementSpeed} %";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.nailGunBleedDamage} à {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}, cadence de {PickUpgrade.nailGunTimer.ToString("F2")} sec à {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} sec, ralentissement de {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% à {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "pistola de clavos";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: dispara un clavo cada {PickUpgrade.nailGunTimer} segundo, causando {PickUpgrade.nailGunBleedDamage} de daño por sangrado durante 3 segundos. los slimes golpeados por un clavo reducirán su velocidad de movimiento en un {PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño por sangrado del clavo de {PickUpgrade.nailGunBleedDamage} a {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}, reduce el tiempo de disparo de la pistola de clavos de {PickUpgrade.nailGunTimer.ToString("F2")} seg a {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} seg y aumenta la reducción de velocidad de {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% a {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "射钉枪";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"草莓轨道：每{PickUpgrade.nailGunTimer}秒射出一颗钉子，造成{PickUpgrade.nailGunBleedDamage}点失血伤害并持续3秒。被钉子击中后史莱姆的移动速度会降低{PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"射钉枪失血伤害从{PickUpgrade.nailGunBleedDamage}点提高到{PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}点，射钉枪射击时间从{PickUpgrade.nailGunTimer.ToString("F2")}秒缩短到{(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")}秒，减速从{PickUpgrade.nailGunMovementSpeed.ToString("F0")}%提高到{(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "네일건";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: {PickUpgrade.nailGunTimer}초마다 못을 발사하며 3초 동안{PickUpgrade.nailGunBleedDamage} 출혈 데미지를 줍니다. 못에 맞은 슬라임은 이동 속도가 {PickUpgrade.nailGunMovementSpeed}% 감소합니다.";
                }
                else
                {
                    upgradeHoverDesc = $"못 출혈 데미지를 {PickUpgrade.nailGunBleedDamage}에서{PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}으로 증가, 네일건 발사 시간을 {PickUpgrade.nailGunTimer.ToString("F2")}초에서 {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")}초로 줄이고 속도 감소를 {PickUpgrade.nailGunMovementSpeed.ToString("F0")}%에서{(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "гвоздомет";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Орбита клубники: стреляет гвоздем через {PickUpgrade.nailGunTimer} сек., нанося {PickUpgrade.nailGunBleedDamage} ед. урона от кровотечения за 3 сек. урон от гвоздя понижает скорость слизи на {PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон гвоздя от кровотечения с {PickUpgrade.nailGunBleedDamage} до {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease} ед., сокращая время выстрела с {PickUpgrade.nailGunTimer.ToString("F2")} до {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} сек. и улучшая снижение скорости с {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% до {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "pistolet na gwoździe";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: wystrzeliwuje gwóźdź co {PickUpgrade.nailGunTimer} sekundę, zadając {PickUpgrade.nailGunBleedDamage} obrażenia od krwawienia przez 3 sekundy. slimy trafione gwoździem zmniejszają swoją prędkość ruchu o {PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od krwawienia z gwodźi z {PickUpgrade.nailGunBleedDamage} do {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}, skraca czas wystrzału z pistoletu na gwoździe z {PickUpgrade.nailGunTimer.ToString("F2")}  sek. do  {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} sek. i zwiększa redukcję prędkości z {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% do {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Arma de pregos";

                if (PickUpgrade.choseNailGun == false)
                {
                    upgradeHoverDesc = $"Orbital de morango: dispara um prego a cada {PickUpgrade.nailGunTimer} segundo, causando {PickUpgrade.nailGunBleedDamage} de dano de sangramento por 3 segundos. As gosmas atingidas por um prego reduzirão sua velocidade de movimento em {PickUpgrade.nailGunMovementSpeed}%";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano de sangramento do prego de {PickUpgrade.nailGunBleedDamage} para {PickUpgrade.nailGunBleedDamage + PickUpgrade.nailGunBleedDamageIncrease}, diminui o tempo de disparo da pistola de pregos de {PickUpgrade.nailGunTimer.ToString("F2")} segundo para {(PickUpgrade.nailGunTimer - PickUpgrade.nailGunTimerDecrease).ToString("F2")} segundo e aumenta a redução de velocidade de {PickUpgrade.nailGunMovementSpeed.ToString("F0")}% para {(PickUpgrade.nailGunMovementSpeed + PickUpgrade.nailGunMovementSpeedDecrease).ToString("F0")}%";
                }
            }
        }
        #endregion

        #region bear trap
        if (upgradeName == "BearTrap")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "bear trap";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"on slime death: chance to drop a bear trap that deals {PickUpgrade.bearTrapDamage} damage and stuns the slime that walks over it for {PickUpgrade.bearTrapStunTimer} seconds. the bear trap only dissapears after a slime walks over it. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"increases the bear trap damage from {PickUpgrade.bearTrapDamage} to {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease} and the stun time from {PickUpgrade.bearTrapStunTimer} sec to {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} sec";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Bärenfalle";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Tod: Chance, eine Bärenfalle zu werfen, die {PickUpgrade.bearTrapDamage} Schaden verursacht und den Slime, der darüber läuft,  {PickUpgrade.bearTrapStunTimer} ekunden lang betäubt. Die Bärenfalle verschwindet erst, wenn ein Slime darüber läuft. Verbraucht 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden der Bärenfalle von {PickUpgrade.bearTrapDamage} auf {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease}  und die Betäubungszeit von {PickUpgrade.bearTrapStunTimer} Sekunden auf {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} Sekunden";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ベアトラップ";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"スライム撃破時： 一定確率でベアトラップを設置。スライムが踏むと{PickUpgrade.bearTrapDamage}ダメージを与え、{PickUpgrade.bearTrapStunTimer} 秒間スタンさせる。スライムが踏むまで消えない。アップグレードスロットを1つ使用！";
                }
                else
                {
                    upgradeHoverDesc = $"ダメージが{PickUpgrade.bearTrapDamage}から{PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease}に増加、スタン時間が{PickUpgrade.bearTrapStunTimer}秒から{PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease}秒に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "PIÈGE À OURS";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"À la mort d’un slime : chance de laisser un piège à ours qui inflige {PickUpgrade.bearTrapDamage} dégâts et étourdit le slime pendant {PickUpgrade.bearTrapStunTimer} secondes. Le piège reste jusqu'à être activé. Utilise 1 emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.bearTrapDamage} à {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease} , étourdissement rallongés de {PickUpgrade.bearTrapStunTimer} à {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} sec";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "trampa para osos";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"al morir un slime: probabilidad de dejar caer una trampa para osos que hace {PickUpgrade.bearTrapDamage} de daño y aturde al slime que pasa sobre ella durante {PickUpgrade.bearTrapStunTimer} segundos. la trampa para osos solo desaparece después de que un slime pase sobre ella. ¡usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta el daño de la trampa para osos de {PickUpgrade.bearTrapDamage} a {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease} y el tiempo de aturdimiento de {PickUpgrade.bearTrapStunTimer} seg a {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} seg";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "捕熊器";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"击杀史莱姆：有几率投掷一个捕熊器，可造成{PickUpgrade.bearTrapDamage}点伤害并使走过的史莱姆昏迷{PickUpgrade.bearTrapStunTimer}秒。史莱姆从捕熊器上面走过后捕熊器才会消失。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"捕熊器伤害从{PickUpgrade.bearTrapDamage}点提高到{PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease}点，昏迷时间从{PickUpgrade.bearTrapStunTimer}秒提高到{PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease}秒";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "베어 트랩";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"슬라임 처치 시: {PickUpgrade.bearTrapDamage}데미지를 주고 밟은 슬라임을 {PickUpgrade.bearTrapStunTimer}초 동안 기절시키는 베어 트랩을 떨어뜨릴 확률이 있습니다. 베어 트랩은 슬라임이 밟을 때만 사라집니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"베어 트랩 데미지를 {PickUpgrade.bearTrapDamage}에서{PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease}로, 기절 시간을{PickUpgrade.bearTrapStunTimer}초에서 {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease}초로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "медвежий капкан";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"при смерти слизи: шанс поставить медвежий капкан, нанося {PickUpgrade.bearTrapDamage} ед. урона и оглушая слизь при контакте на {PickUpgrade.bearTrapStunTimer} сек. медвежий капкан исчезнет после контакта со слизью. использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"повышает урон медвежьего капкана с {PickUpgrade.bearTrapDamage} до {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease} ед., а время оглушения — с {PickUpgrade.bearTrapStunTimer} до {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} сек.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "pułapka na niedźwiedzie";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"Po śmierci slime: szansa na upuszczenie pułapki na niedźwiedzie, która zadaje {PickUpgrade.bearTrapDamage} dobrażeń i ogłusza slime, który nad nią przejdzie, na {PickUpgrade.bearTrapStunTimer} sekundy. pułapka na niedźwiedzie znika dopiero po przejściu nad nią slima. wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia pułapki na niedźwiedzie z {PickUpgrade.bearTrapDamage} do {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease} i czas ogłuszenia z {PickUpgrade.bearTrapStunTimer} do {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} sek";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "armadilha para ursos";

                if (PickUpgrade.choseBearTrap == false)
                {
                    upgradeHoverDesc = $"na morte da gosma: chance de soltar uma armadilha de urso que causa {PickUpgrade.bearTrapDamage} de dano e atordoa a gosma que passar por cima dela por {PickUpgrade.bearTrapStunTimer} segundos. a armadilha de urso só desaparece depois que uma gosma passa por cima dela. usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta o dano da armadilha de urso de {PickUpgrade.bearTrapDamage} para {PickUpgrade.bearTrapDamage + PickUpgrade.bearTrapDamageIncrease} e o tempo de atordoamento de {PickUpgrade.bearTrapStunTimer} segundos para {PickUpgrade.bearTrapStunTimer + PickUpgrade.bearTrapStunTimerIncrease} segundos";
                }
            }
        }
        #endregion

        #region Spiky log
        if (upgradeName == "Log")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "spiky log";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"on slime click: chance to spawn a large spiky log that moves in a random direction. the log pierces all slimes and deals {PickUpgrade.logDamage} damage. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"increases the log damage from {PickUpgrade.logDamage} to {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Stacheliger Holzklotz";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, einen großen stacheligen Holzklotz zu erzeugen, der sich in eine zufällige Richtung bewegt. Der Klotz durchbohrt alle Slimes und verursacht {PickUpgrade.logDamage} Schaden. Verbraucht 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Klotzschaden von {PickUpgrade.logDamage} auf {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "スパイキーログ";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 一定確率で大型のトゲ付き丸太を召喚。丸太はランダムな方向に転がり、スライムを貫通しながら{PickUpgrade.logDamage}ダメージを与える。アップグレードスロットを1つ使用！";
                }
                else
                {
                    upgradeHoverDesc = $"丸太のダメージが{PickUpgrade.logDamage}から{PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "TRONC PIQUANT";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"Au clic sur un slime : chance de faire apparaître un gros tronc piquant qui se déplace dans une direction aléatoire, transperce tous les slimes et inflige {PickUpgrade.logDamage} dégâts. Utilise 1 emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Dégâts augmentés de {PickUpgrade.logDamage} à {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "tronco con picos";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"al hacer click en un slime: probabilidad de generar un gran tronco con picos que se mueve en una dirección aleatoria. el tronco atraviesa todos los slimes y hace {PickUpgrade.logDamage} de daño. ¡usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta el daño del tronco de {PickUpgrade.logDamage} a {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "带刺原木";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率生成一个朝随机方向移动的带刺大圆木。圆木会刺穿所有史莱姆并造成{PickUpgrade.logDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"圆木伤害从{PickUpgrade.logDamage}点提高到{PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}点";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "뾰족한 통나무";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 랜덤 방향으로 이동하는 큰 뾰족한 통나무를 생성할 확률이 있습니다. 통나무는 모든 슬라임을 관통하고 {PickUpgrade.logDamage} 데미지를 줍니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"통나무 데미지를 {PickUpgrade.logDamage}에서{PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}로 증가.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "шипастое бревно";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"при клике на слизь: шанс вызвать большое шипастое бревно, катящееся в случайном направлении. бревно пронзает всю слизь, нанося {PickUpgrade.logDamage}ед. урона. использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"повышает урон от бревна с {PickUpgrade.logDamage} до {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease} ед.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "kolczasta kłoda";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"po kliknięciu slime: szansa na pojawienie się dużej kolczastej kłody, która porusza się w losowym kierunku. Kłoda przebija wszystkie slimy i zadaje {PickUpgrade.logDamage} obrażenia. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"zwiększa obrażenia kłody z {PickUpgrade.logDamage} do {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "tronco espinhoso";

                if (PickUpgrade.choseLog == false)
                {
                    upgradeHoverDesc = $"ao clicar na gosma: chance de gerar um grande tronco espinhoso que se move em uma direção aleatória. o tronco perfura todas as gosmas e causa {PickUpgrade.logDamage} de dano. usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"aumenta o dano do tronco de {PickUpgrade.logDamage} para {PickUpgrade.logDamage + PickUpgrade.logDamageIncrease}";
                }
            }
        }
        #endregion

        #region leg
        if (upgradeName == "Leg")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "strawberry legs";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"the strawberry will get a pair of legs. legs have a chance to kick slime bullets, which will either delete the bullet or kick it back at slimes, dealing {PickUpgrade.kickedBulletDamage} damage. there is also a small chance to negate any damaged dealt to the strawberry";
                }
                else
                {
                    upgradeHoverDesc = $"increases the kicked bullet damage from {PickUpgrade.kickedBulletDamage} to {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Erdbeerbeine";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"Die erdbeere bekommt ein paar Beine. Die Beine haben eine Chance, Slime-Geschosse zu treten, die entweder das Geschoss zerstören oder es auf die Slimes zurückwerfen, was {PickUpgrade.kickedBulletDamage} Schaden verursacht. Es gibt auch eine geringe Chance, dass Schaden an der Erdbeere negiert wird.";
                }
                else
                {
                    upgradeHoverDesc = $"erhöht den Schaden durch getretene Kugeln von {PickUpgrade.kickedBulletDamage} auf {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ストロベリーレッグス";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"イチゴに脚が生える。脚はスライムの弾を蹴ることがあり、弾を消すか、スライムに跳ね返して{PickUpgrade.kickedBulletDamage}ダメージを与える。さらに、稀にイチゴがダメージを無効化することがある。";
                }
                else
                {
                    upgradeHoverDesc = $"蹴られた弾丸のダメージを{PickUpgrade.kickedBulletDamage}から{PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "JAMBES DE FRAISE";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"La fraise obtient une paire de jambes. Elles ont une chance de donner un coup de pied aux projectiles slimes, ce qui peut les détruire ou les renvoyer, infligeant {PickUpgrade.kickedBulletDamage} dégâts. Petite chance également de bloquer complètement un dégât";
                }
                else
                {
                    upgradeHoverDesc = $"augmente les dégâts des balles frappées de {PickUpgrade.kickedBulletDamage} à {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "patas de fresa";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"la fresa obtendrá un par de patas. las patas tienen probabilidad de patear las balas de slime, lo que eliminará la bala o la pateará de vuelta a los slimes, haciendo {PickUpgrade.kickedBulletDamage} de daño. también hay una pequeña probabilidad de anular cualquier daño causado a la fresa";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de la bala pateada de {PickUpgrade.kickedBulletDamage} a {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "草莓腿";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"草莓会得到一双腿。草莓腿有几率踢到史莱姆子弹，从而消除子弹或把子弹踢回史莱姆，造成{PickUpgrade.kickedBulletDamage}点伤害。草莓腿也有小几率帮助草莓避开伤害";
                }
                else
                {
                    upgradeHoverDesc = $"增加踢出的子弹伤害从{PickUpgrade.kickedBulletDamage}到{PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }

            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "딸기 다리";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"딸기에게 다리가 생깁니다. 다리는 슬라임 탄환을 걷어찰 확률이 있으며, 탄환을 삭제하거나 슬라임에게 되돌려 차서 {PickUpgrade.kickedBulletDamage} 데미지를 줍니다. 딸기가 받는 모든 데미지를 무효화할 작은 확률도 있습니다.";
                }
                else
                {
                    upgradeHoverDesc = $"발로 차는 총알의 데미지를 {PickUpgrade.kickedBulletDamage}에서 {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "клубничные ноги";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"клубника получит пару ног. у ног есть шанс пнуть пулю слизи, уничтожив или отбив ее в слизь, нанося {PickUpgrade.kickedBulletDamage}  ед. урона. также есть малый шанс свести на нет любой урон, наносимый клубнике";
                }
                else
                {
                    upgradeHoverDesc = $"увеличивает урон от пуль, отброшенных ногой, с {PickUpgrade.kickedBulletDamage} до {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "truskawkowe nogi";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"truskawka otrzyma parę nóg. nogi mają szansę kopnąć pociski, które albo je usuną, albo kopną ją z powrotem w slime, zadając {PickUpgrade.kickedBulletDamage} obrażeń. Jest również niewielka szansa na zanegowanie wszelkich obrażeń zadanych truskawce.";
                }
                else
                {
                    upgradeHoverDesc = $"zwiększa obrażenia od kopniętej kuli z {PickUpgrade.kickedBulletDamage} do {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "pernas de morango";

                if (PickUpgrade.choseLegs == false)
                {
                    upgradeHoverDesc = $"o morango receberá um par de pernas. as pernas têm a chance de chutar balas de gosma, que apagarão a bala ou a chutarão de volta para as gosmas, causando {PickUpgrade.kickedBulletDamage} de dano. há também uma pequena chance de anular qualquer dano causado ao morango";
                }
                else
                {
                    upgradeHoverDesc = $"Aumento do dano de balas chutadas de {PickUpgrade.kickedBulletDamage} para {PickUpgrade.kickedBulletDamage + PickUpgrade.kickedBulletDamageIncrease}";
                }
            }
        }
        #endregion

        //Already translated
        #region cooldown
        if (upgradeName == "cooldown")
        {
            float totalClickCooldown = PickUpgrade.clickCooldown - PickUpgrade.clickCooldownDecrease;
            float currentClickCoodown = PickUpgrade.clickCooldown;

            if (languageSelected == 1) //English
            { 
                upgradeHoverName = "click cooldown";
                upgradeHoverDesc = $"Decreases the click cooldown time from {currentClickCoodown.ToString("F3")} sec to {(totalClickCooldown).ToString("F3")} sec";
            }
            else if (languageSelected == 2) //German
            { 
                upgradeHoverName = "Klick-Abklingzeit";
                upgradeHoverDesc = $"Verringert die Klick-Abklingzeit von {currentClickCoodown.ToString("F3")}. auf {(totalClickCooldown).ToString("F3")} Sek.";
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "クリック・クールダウン";
                upgradeHoverDesc = $"クリックのクールダウン時間を {currentClickCoodown.ToString("F3")}秒から{(totalClickCooldown).ToString("F3")}秒に短縮";
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "temps de recharge des clics";
                upgradeHoverDesc = $"réduit le temps de recharge des clics de {currentClickCoodown.ToString("F3")} seconde à {(totalClickCooldown).ToString("F3")} seconde";
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "tiempo de recarga de click";
                upgradeHoverDesc = $"Reduce el tiempo de recarga del click de {currentClickCoodown.ToString("F3")} seg a {(totalClickCooldown).ToString("F3")} seg";
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "点击冷却";
                upgradeHoverDesc = $"点击冷却时间从{currentClickCoodown.ToString("F3")}秒缩短至{(totalClickCooldown).ToString("F3")}秒";
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "클릭 쿨다운";
                upgradeHoverDesc = $"클릭 쿨다운 시간을 {currentClickCoodown.ToString("F3")} 초에서 {(totalClickCooldown).ToString("F3")} 초로 감소시킵니다.";
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "откат клика";
                upgradeHoverDesc = $"Уменьшает время отката клика с {currentClickCoodown.ToString("F3")} сек. до {(totalClickCooldown).ToString("F3")} сек.";
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "czas odnowienia kliknięcia";
                upgradeHoverDesc = $"Skraca czas odnowienia kliknięcia z {currentClickCoodown.ToString("F3")} sek. do {(totalClickCooldown).ToString("F3")} sek.";
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Recarga de clique";
                upgradeHoverDesc = $"Diminui o tempo de recarga do clique de {currentClickCoodown.ToString("F3")} seg. para {(totalClickCooldown).ToString("F3")} seg.";
            }
        }
        #endregion

        #region click damage
        if (upgradeName == "clickDamage")
        {
            float totalClickDamage = MetaProgressionUpgrades.clickDamageIncrease + PickUpgrade.clickDamage + PickUpgrade.clickDamageIncrease;
            float currentClickDamage = MetaProgressionUpgrades.clickDamageIncrease + PickUpgrade.clickDamage;

            if (languageSelected == 1) //English
            {
                upgradeHoverName = "click damage";
                upgradeHoverDesc = $"Increases the click damage from {currentClickDamage} to {totalClickDamage}";
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Klick-Schaden";
                upgradeHoverDesc = $"Erhöht den Klick-Schaden von {currentClickDamage} auf {totalClickDamage}";
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "クリックダメージ";
                upgradeHoverDesc = $"クリックダメージを{currentClickDamage}から{totalClickDamage}に増加";
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "dégâts des clics";
                upgradeHoverDesc = $"augmente les dégâts des clics de {currentClickDamage} à {totalClickDamage}";
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "daño de click";
                upgradeHoverDesc = $"Aumenta el daño del click de {currentClickDamage} a {totalClickDamage}";
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "点击伤害";
                upgradeHoverDesc = $"点击伤害从{currentClickDamage}提高到{totalClickDamage}";
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "클릭 데미지";
                upgradeHoverDesc = $"클릭 데미지를 {currentClickDamage} 에서 {totalClickDamage}로 증가시킵니다.";
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "урон клика";
                upgradeHoverDesc = $"Повышает урон клика с {currentClickDamage} до {totalClickDamage}";
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "obrażenia kliknięcia";
                upgradeHoverDesc = $"Zwiększa obrażenia od kliknięcia z {currentClickDamage} do {totalClickDamage}";
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Dano do clique";
                upgradeHoverDesc = $"Aumenta o dano do clique de {currentClickDamage} para {totalClickDamage}";
            }
        }
        #endregion

        #region critDamage
        if (upgradeName == "critDamage")
        {
            float currentCritChance = PickUpgrade.critChance + MetaProgressionUpgrades.critChanceIncrease;
            float totalCritChance = PickUpgrade.critChance + MetaProgressionUpgrades.critChanceIncrease + PickUpgrade.critChanceIncrease;

            float currentCritIncrease = (PickUpgrade.critIncrease + MetaProgressionUpgrades.critIncreaseIncrease) * 100;
            float totalCritIncrease = (PickUpgrade.critIncrease + MetaProgressionUpgrades.critIncreaseIncrease + PickUpgrade.critIncreaseIncrease) * 100;

            if (languageSelected == 1) //English
            {
                upgradeHoverName = "crit damage";
                upgradeHoverDesc = $"Increases the crit chance from {currentCritChance}% to {totalCritChance}%. And crit damage increase from {currentCritIncrease.ToString("F0")}% to {totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Krit-Schaden";
                upgradeHoverDesc = $"Erhöht die Krit-Chance von {currentCritChance}% auf {totalCritChance}% und den Krit-Schaden von{currentCritIncrease.ToString("F0")}% auf  {totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "クリティカルダメージ";
                upgradeHoverDesc = $"クリティカル率を{currentCritChance}%から{totalCritChance}%に増加し、クリティカルダメージが{currentCritIncrease.ToString("F0")}%から{totalCritIncrease.ToString("F0")}%に増加する。";
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "dégâts critiques";
                upgradeHoverDesc = $"Augmente la chance de critique de {currentCritChance}% à {totalCritChance}%. Et augmente les dégâts critiques de {currentCritIncrease.ToString("F0")}% à {totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "daño crítico";
                upgradeHoverDesc = $"Aumenta la probabilidad de crítico de {currentCritChance}% a {totalCritChance}%. Y el daño crítico aumenta de {currentCritIncrease.ToString("F0")}% a {totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "暴击伤害";
                upgradeHoverDesc = $"将暴击率从{currentCritChance}%提高到{totalCritChance}%。暴击伤害从{currentCritIncrease.ToString("F0")}%提高到{totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "크리티컬 데미지";
                upgradeHoverDesc = $"크리티컬 확률을 {currentCritChance}% 에서 {totalCritChance}%로, 크리티컬 데미지를 {currentCritIncrease.ToString("F0")}% 에서 {totalCritIncrease.ToString("F0")}%로 증가시킵니다.";
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "критический урон";
                upgradeHoverDesc = $"Повышает шанс крита с {currentCritChance}% до {totalCritChance}%. Урон крита повышается с {currentCritIncrease}% до {totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "obrażenia krytyczne";
                upgradeHoverDesc = $"Zwiększa szansę na trafienie krytyczne z {currentCritChance}% do {totalCritChance}%. Zwiększa obrażenia krytyczne z {currentCritIncrease.ToString("F0")}% do {totalCritIncrease.ToString("F0")}%";
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Dano crítico";
                upgradeHoverDesc = $"Aumenta a chance de crítico de {currentCritChance}% para {totalCritChance}%. E o dano crítico aumenta de {currentCritIncrease.ToString("F0")}% para {totalCritIncrease.ToString("F0")}%";
            }
        }
        #endregion

        #region health
        if (upgradeName == "health")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "strawberry health";
                upgradeHoverDesc = $"Increases the health of the strawberry by half a heart";
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Erdbeeren-Gesundheit";
                upgradeHoverDesc = $"Erhöht die Gesundheit der Erdbeere um ein halbes Herz";
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "いちごの体力";
                upgradeHoverDesc = $"いちごの体力がハート半分分増加する";
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "santé de la fraise";
                upgradeHoverDesc = $"Augmente la santé de la fraise d'un demi-cœur.";
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "salud de la fresa";
                upgradeHoverDesc = $"Aumenta la salud de la fresa en medio corazón";
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "草莓生命值";
                upgradeHoverDesc = $"草莓生命值增加一半";
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "딸기 체력";
                upgradeHoverDesc = $"딸기의 체력을 반 칸 증가시킵니다.";
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "здоровье клубники";
                upgradeHoverDesc = $"Повышает здоровье клубники на полсердца";
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "zdrowie truskawek";
                upgradeHoverDesc = $"Zwiększa zdrowie truskawki o połowę serca";
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Saúde do morango";
                upgradeHoverDesc = $"Aumenta a saúde do morango em meio coração";
            }
        }
        #endregion

        #region all damage
        if (upgradeName == "allDamage")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "damage increase";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"increases all damage dealt by {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"increases all damage dealt from {PickUpgrade.totalIncreaseDamage}% to {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Schadenssteigerung";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"Erhöht allen verursachten Schaden um {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"Erhöht allen verursachten Schaden von {PickUpgrade.totalIncreaseDamage}% auf {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ダメージ増加";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"全ての与ダメージが{PickUpgrade.displayTotalDamageIncrease}%増加"; }
                else
                {
                    upgradeHoverDesc = $"全ての与ダメージを{PickUpgrade.totalIncreaseDamage}%から{PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "augmentation des dégâts";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"augmente tous les dégâts infligés de {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"augmente tous les dégâts infligés de {PickUpgrade.totalIncreaseDamage}% à {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "aumento de daño";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"aumenta todo el daño causado en un {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"aumenta todo el daño causado de {PickUpgrade.totalIncreaseDamage}% a {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "伤害值增加";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"使所有伤害值提高{PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"使所有伤害值从{PickUpgrade.totalIncreaseDamage}%提高到{PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "데미지 증가";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"모든 데미지를 {PickUpgrade.displayTotalDamageIncrease}% 증가시킵니다."; }
                else
                {
                    upgradeHoverDesc = $"모든 데미지를 {PickUpgrade.totalIncreaseDamage}% 에서  {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "повышение урона";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"повышает весь наносимый урон на {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"повышает весь наносимый урон с {PickUpgrade.totalIncreaseDamage}% до {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "zwiększenie obrażeń";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"Zwiększa wszystkie zadawane obrażenia o {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"Zwiększa wszystkie zadawane obrażenia z {PickUpgrade.totalIncreaseDamage}% do {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "aumento de dano";
                if (PickUpgrade.increaseAllDamageLevel == 0) { upgradeHoverDesc = $"aumenta todo o dano causado em {PickUpgrade.displayTotalDamageIncrease}%"; }
                else
                {
                    upgradeHoverDesc = $"aumenta todo o dano causado de {PickUpgrade.totalIncreaseDamage}% para {PickUpgrade.totalIncreaseDamage + PickUpgrade.totalIncreaseDamageIncrease}%";
                }
            }
        }
        #endregion

        #region cursor slash
        if (upgradeName == "cursorSlash")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "CURSOR SLASH";
                if(MobileScript.isMobile == true) { upgradeHoverName = "FINGER SLASH"; }

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Move the cursor over a slime to slash. Each slash deals {PickUpgrade.cursorSlashDamage} damage. Uses 1 upgrade slot!";

                    if (MobileScript.isMobile == true) { upgradeHoverDesc = $"Drag the finger over a slime to slash. Each slash deals {PickUpgrade.cursorSlashDamage} damage. Uses 1 upgrade slot!"; }
                }
                else
                {
                    upgradeHoverDesc = $"Increases the cursor slash damage from {PickUpgrade.cursorSlashDamage} to {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";

                    if (MobileScript.isMobile == true) { upgradeHoverDesc = $"Increases the slash damage from {PickUpgrade.cursorSlashDamage} to {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}"; }
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "CURSOR-HIEB";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Bewege den Cursor über einen Slime, um zuzuschlagen. Jeder Hieb verursacht {PickUpgrade.cursorSlashDamage} Schaden. Benutzt 1 Upgrade-Slot!!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Cursor-Hieb-Schaden von {PickUpgrade.cursorSlashDamage} auf {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "カーソルスラッシュ";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"カーソルをスライムの上に移動させて斬る。{PickUpgrade.cursorSlashDamage}回斬るごとに1ダメージを与える。アップグレードスロットを1使用する！";
                }
                else
                {
                    upgradeHoverDesc = $"カーソルスラッシュのダメージが{PickUpgrade.cursorSlashDamage}から{PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}へ増加。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "COUP DE CURSEUR";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Déplacez le curseur sur un slime pour l'entailler. Chaque coup inflige {PickUpgrade.cursorSlashDamage} dégât. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts du coup de curseur de {PickUpgrade.cursorSlashDamage} à {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "CORTE DEL CURSOR";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Mueve el cursor sobre un slime para cortar. Cada corte hace {PickUpgrade.cursorSlashDamage} de daño. ¡Usa 1 espacio de";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del corte del cursor de {PickUpgrade.cursorSlashDamage} a {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "光标斩击";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"将光标移到史莱姆上进行斩击。每次斩击造成{PickUpgrade.cursorSlashDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"光标斩击的伤害值从{PickUpgrade.cursorSlashDamage}提高到{PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "커서 슬래시";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"커서를 슬라임 위로 이동시켜 베어버립니다. 각 슬래시는 {PickUpgrade.cursorSlashDamage} 의 데미지를 입힙니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"커서 슬래시 데미지를 {PickUpgrade.cursorSlashDamage} 에서 {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "РУБКА КУРСОРОМ";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Наведите курсор на слизня, чтобы ударить. Каждый удар наносит {PickUpgrade.cursorSlashDamage} ед. урона. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"커서 슬래시 데미지를 {PickUpgrade.cursorSlashDamage}에서 {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "OSTRZE KURSORA";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Przesuń kursor na slime, aby wykonać cięcie. Każde cięcie zadaje {PickUpgrade.cursorSlashDamage} obrażenie. Wykorzystuje 1 slot na ulepszenia!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od cięcia kursorem z {PickUpgrade.cursorSlashDamage} do {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "CORTE COM O CURSOR";

                if (PickUpgrade.choseCursorSlash == false)
                {
                    upgradeHoverDesc = $"Mova o cursor sobre uma gosma para cortá-la. Cada corte causa {PickUpgrade.cursorSlashDamage} de dano. Usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do corte do cursor de {PickUpgrade.cursorSlashDamage} para {PickUpgrade.cursorSlashDamage + PickUpgrade.cursorSlashDamageIncrease}";
                }
            }
        }
        #endregion

        #region paper plane
        if (upgradeName == "paperShot")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "paper plane";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"On slime death: Chance to shoot paper plane at a random slime, dealing {PickUpgrade.paperShotDamage} damage. Uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the paper shot damage from {PickUpgrade.paperShotDamage} to {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "Papierflieger";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Tod: Chance, einen Papierflieger auf einen zufälligen Slime zu schießen, der {PickUpgrade.paperShotDamage} Schaden verursacht. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden des Papierfliegers von {PickUpgrade.paperShotDamage} auf  {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "紙飛行機";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"スライム死亡時： スライムにランダムに紙飛行機を発射し、{PickUpgrade.paperShotDamage}ダメージを与える。アップグレードスロットを1つ使用する！";
                }
                else
                {
                    upgradeHoverDesc = $"紙飛行機のダメージが{PickUpgrade.paperShotDamage}から{PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "avion en papier";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"À la mort du slime : Chance de tirer un avion en papier sur un slime aléatoire, infligeant {PickUpgrade.paperShotDamage} dégâts. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts du tir de papier de {PickUpgrade.paperShotDamage} à {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "avión de papel";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"Al morir un slime: Probabilidad de disparar un avión de papel a un slime aleatorio, haciendo {PickUpgrade.paperShotDamage} de daño. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del disparo de papel de {PickUpgrade.paperShotDamage} a {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "纸飞机";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率向随机一个史莱姆发射纸飞机，造成{PickUpgrade.paperShotDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"纸飞机的伤害值从{PickUpgrade.paperShotDamage}提高到{PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "종이 비행기";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"슬라임 죽음 시: 무작위 슬라임에게 종이 비행기를 발사하여 {PickUpgrade.paperShotDamage}의 데미지를 입힐 확률이 있습니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"종이 비행기 데미지를 {PickUpgrade.paperShotDamage}에서 {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "бумажный самолетик";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"При смерти слизня: шанс выпустить бумажный самолетик в случайного слизня, нанося {PickUpgrade.paperShotDamage} ед. урона. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от бумаги с {PickUpgrade.paperShotDamage} до {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "papierowy samolot";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"Po śmierci slime: Szansa na wystrzelenie papierowego samolotu w losowego slime, zadając {PickUpgrade.paperShotDamage} obrażeń. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od papierowych strzałów z {PickUpgrade.paperShotDamage} do {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "avião de papel";

                if (PickUpgrade.chosePaperShot == false)
                {
                    upgradeHoverDesc = $"Na morte da gosma: Chance de atirar um avião de papel em uma gosma aleatória, causando {PickUpgrade.paperShotDamage} de dano. Usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do tiro de papel de {PickUpgrade.paperShotDamage} para {PickUpgrade.paperShotDamage + PickUpgrade.paperShotDamageIncrease}";
                }
            }
        }
        #endregion

        #region arrow rain
        if (upgradeName == "arrowRain")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "ARROW RAIN";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"On slime click: Chance to fire 4-7 arrows that fall from the sky, targets random slimes and deals {PickUpgrade.arrowRainDamage} damage per arrow. Uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the arrow damage from {PickUpgrade.arrowRainDamage} to {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "PFEILREGEN";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, 4-7 Pfeile vom Himmel regnen zu lassen, die zufällige Slimes treffen und jeweils {PickUpgrade.arrowRainDamage} Schaden verursachen. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Pfeilschaden von {PickUpgrade.arrowRainDamage} auf {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "弓矢の雨";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 空から降ってくる矢を4～7本放つ。ランダムなスライムを攻撃対象とし、1本につき{PickUpgrade.arrowRainDamage} ダメージを与える。アップグレードスロットを1つ使用する！";
                }
                else
                {
                    upgradeHoverDesc = $"矢のダメージを{PickUpgrade.arrowRainDamage}から{PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}に増加";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "PLUIE DE FLÈCHES ";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"Par clic sur slime : Chance de tirer 4 à 7 flèches qui tombent du ciel, ciblent des slimes aléatoires et infligent {PickUpgrade.arrowRainDamage} dégâts par flèche. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the arrow damage from {PickUpgrade.arrowRainDamage} to {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "LLUVIA DE FLECHAS";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"Al hacer click en un slime: Probabilidad de disparar 4-7 flechas que caen del cielo, apuntan a slimes aleatorios y hacen {PickUpgrade.arrowRainDamage} de daño por flecha. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de las flechas de {PickUpgrade.arrowRainDamage} a {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "箭雨";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率发射4-7支从天而降的箭，每支箭随机瞄准史莱姆，射中史莱姆可造成{PickUpgrade.arrowRainDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"箭的伤害值从{PickUpgrade.arrowRainDamage}提高到{PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "화살비";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 하늘에서 4-7개의 화살이 떨어져 무작위 슬라임을 공격하고 화살당 {PickUpgrade.arrowRainDamage}의 데미지를 입힐 확률이 있습니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"화살 데미지를 {PickUpgrade.arrowRainDamage}에서 {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "ДОЖДЬ ИЗ СТРЕЛ";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"При клике на слизня: шанс выпустить 4-7 стрел, которые падают с неба на случайных слизней и наносят {PickUpgrade.arrowRainDamage} ед. урона на стрелу. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от стрелы с {PickUpgrade.arrowRainDamage} до {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "DESZCZ STRZAŁ";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"Po kliknięciu slime: Szansa na wystrzelenie 4-7 strzał, które spadną z nieba, w losowe slimy i zadadzą {PickUpgrade.arrowRainDamage} obrażenia na strzałę. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od strzał z {PickUpgrade.arrowRainDamage} do {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "CHUVA DE FLECHAS";

                if (PickUpgrade.choseArrowRain == false)
                {
                    upgradeHoverDesc = $"Ao clicar na gosma: Chance de disparar de 4 a 7 flechas que caem do céu, atingem gosmas aleatórias e causam {PickUpgrade.arrowRainDamage} de dano por flecha. Usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano da flecha de {PickUpgrade.arrowRainDamage} para {PickUpgrade.arrowRainDamage + PickUpgrade.arrowRainDamageIncrease}";
                }
            }
        }
        #endregion

        #region scythe
        if (upgradeName == "scythe")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "scythe";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"On slime click: Chance to spawn a spinning scythe that lasts for 3 seconds, dealing {PickUpgrade.scytheDamage} damage per second. Uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the scythe dps from {PickUpgrade.scytheDamage} to {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "SENSE";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, eine rotierende Sense zu beschwören, die 3 Sekunden lang anhält und pro Sekunde {PickUpgrade.scytheDamage} Schaden verursacht. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den DPS der Sense von {PickUpgrade.scytheDamage} auf {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "大鎌";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 一定の確率で3秒間回転する鎌を発生させ、毎秒{PickUpgrade.scytheDamage}ダメージを与える。アップグレードスロットを1つ使用する！";
                }
                else
                {
                    upgradeHoverDesc = $"鎌のDPSが{PickUpgrade.scytheDamage}から{PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "faucheuse";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"Par clic sur slime : Chance de faire apparaître une faucheuse tournoyante qui dure 3 secondes, infligeant {PickUpgrade.scytheDamage} dégâts par seconde. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts par seconde de la faucheuse de {PickUpgrade.scytheDamage} à {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "guadaña";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"Al hacer click en un slime: Probabilidad de generar una guadaña giratoria que dura 3 segundos, haciendo {PickUpgrade.scytheDamage} de daño por segundo. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el dps de la guadaña de {PickUpgrade.scytheDamage} a {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "镰刀";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率生成一把持续3秒的旋转镰刀，每秒造成{PickUpgrade.scytheDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"镰刀的每秒伤害值从{PickUpgrade.scytheDamage}提高到{PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "낫";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 3초 동안 지속되는 회전하는 낫을 생성하여 초당 {PickUpgrade.scytheDamage} 의 데미지를 입힐 확률이 있습니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"낫의 초당 데미지를 {PickUpgrade.scytheDamage}에서 {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}으로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "коса";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"При клике на слизня: шанс вызвать вращающуюся косу на 3 сек., нанося {PickUpgrade.scytheDamage} ед. урона в сек. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон косы с {PickUpgrade.scytheDamage} до {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "kosa";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"Po kliknięciu slime: Szansa na przywołanie wirującej kosy, która działa przez 3 sekundy, zadając {PickUpgrade.scytheDamage} pkt. obrażeń na sekundę. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia na sekundę kosy z {PickUpgrade.scytheDamage} do {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "foice";

                if (PickUpgrade.choseScythe == false)
                {
                    upgradeHoverDesc = $"Ao clicar na gosma: Chance de gerar uma foice giratória que dura 3 segundos, causando {PickUpgrade.scytheDamage} de dano por segundo. Usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dps da foice de {PickUpgrade.scytheDamage} para {PickUpgrade.scytheDamage + PickUpgrade.scytheDamageIncrease}";
                }
            }
        }
        #endregion

        #region laser gun
        if (upgradeName == "laserGun")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "LASER gun";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: A laser gun spin around the strawberry and shoot a laser every {PickUpgrade.laserGunTime.ToString("F2")} seconds, dealing {PickUpgrade.laserGunDamage} damage to slimes. The laser pierces and deletes enemy bullets. {PickUpgrade.laser2XChance}% chance to shoot twice";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the laser damage from {PickUpgrade.laserGunDamage} to {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. Decreases the laser shoot time from {PickUpgrade.laserGunTime.ToString("F2")} to {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}. increases the shoot twice chance from {PickUpgrade.laser2XChance}% to {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "LASERWAFFE";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Eine Laserwaffe dreht sich um die Erdbeere und feuert alle {PickUpgrade.laserGunTime.ToString("F2")} Sekunden einen Laser ab, der {PickUpgrade.laserGunDamage} Schaden an Slimes verursacht. Der Laser durchdringt und löscht gegnerische Projektile. {PickUpgrade.laser2XChance}% Chance, zweimal zu schießen.";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Laserschaden von {PickUpgrade.laserGunDamage} auf {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. verringert die Abschusszeit von {PickUpgrade.laserGunTime.ToString("F2")} Sek. auf  {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")} Sek. und erhöht die Chance, zweimal zu schießen, von{PickUpgrade.laser2XChance}% auf {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "レーザー銃";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"いちごの軌道： レーザーガンがいちごの周りを回転し、{PickUpgrade.laserGunTime.ToString("F2")}秒ごとにレーザーを放ち、スライムに{PickUpgrade.laserGunDamage}ダメージを与える。レーザーは敵の弾丸を貫通する。{PickUpgrade.laser2XChance}%の確率で2回撃てる。";
                }
                else
                {
                    upgradeHoverDesc = $"レーザーダメージが{PickUpgrade.laserGunDamage}から{PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. に増加。レーザーの発射時間を{PickUpgrade.laserGunTime.ToString("F2")}秒から{(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}秒に短縮し、2回発射する確率を{PickUpgrade.laser2XChance}%から{PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "pistolet laser";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Orbital de la fraise : Un pistolet laser tourne autour de la fraise et tire un laser toutes les {PickUpgrade.laserGunTime.ToString("F2")} secondes, infligeant {PickUpgrade.laserGunDamage} dégâts aux slimes. Le laser perfore et supprime les balles ennemies {PickUpgrade.laser2XChance}% de chance de tirer deux ";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts du laser de {PickUpgrade.laserGunDamage} à {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. Réduit le temps de tir du laser de {PickUpgrade.laserGunTime.ToString("F2")} seconde à {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")} seconde. Augmente la chance de tirer deux fois de {PickUpgrade.laser2XChance}% à {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "pistola láser";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: Una pistola láser gira alrededor de la fresa y dispara un láser cada {PickUpgrade.laserGunTime.ToString("F2")} segundos, haciendo {PickUpgrade.laserGunDamage} de daño a los slimes. El láser atraviesa y elimina las balas enemigas. {PickUpgrade.laser2XChance}% de probabilidad de disparar dos veces";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del láser de {PickUpgrade.laserGunDamage} a {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. Reduce el tiempo de disparo del láser de {PickUpgrade.laserGunTime.ToString("F2")} seg a {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")} seg. aumenta la probabilidad de disparar dos veces de {PickUpgrade.laser2XChance}% a {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "激光枪";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"草莓轨道: 激光枪会围绕草莓旋转，每{PickUpgrade.laserGunTime.ToString("F2")}秒发射一次激光，对史莱姆造成 {PickUpgrade.laserGunDamage}点伤害。激光可以击穿并消灭敌人的子弹。有{PickUpgrade.laser2XChance}%的几率射击两次";
                }
                else
                {
                    upgradeHoverDesc = $"激光的伤害值从{PickUpgrade.laserGunDamage}提高到{PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. 。激光射击时间从{PickUpgrade.laserGunTime.ToString("F2")}秒缩短至{(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}秒，两次射击的几率从{PickUpgrade.laser2XChance}%增加到{PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "레이저 총";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: 레이저 총이 딸기 주위를 회전하며 {PickUpgrade.laserGunTime.ToString("F2")} 초마다 레이저를 발사하여 슬라임에게 {PickUpgrade.laserGunDamage}의 데미지를 입힙니다. 레이저는 관통하며 적의 총알을 삭제합니다. {PickUpgrade.laser2XChance}%확률로 두 번 발사합니다.";
                }
                else
                {
                    upgradeHoverDesc = $"레이저 데미지를 {PickUpgrade.laserGunDamage}에서 {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. 로 증가시킵니다. 레이저 발사 시간을 {PickUpgrade.laserGunTime.ToString("F2")}초에서{(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}. 초로 감소시킵니다. 두 번 발사 확률을 {PickUpgrade.laser2XChance}% 에서 {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "лазерная пушка";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Вокруг клубники: лазерная пушка вращается вокруг клубники и стреляет лазером каждые {PickUpgrade.laserGunTime.ToString("F2")} сек., нанося {PickUpgrade.laserGunDamage} ед. урона слизням. Лазер пронзает и сбивает вражеские пули. {PickUpgrade.laser2XChance}% шанс выстрелить дважды";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон лазера с {PickUpgrade.laserGunDamage} до {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. Сокращает время выстрела с {PickUpgrade.laserGunTime.ToString("F2")} сек. до  {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}.  сек. Повышает шанс двойного выстрела с {PickUpgrade.laser2XChance}% до {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "pistolet laserowy";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: Działo laserowe obraca się wokół truskawki i wystrzeliwuje laser co {PickUpgrade.laserGunTime.ToString("F2")} sekundy, zadając {PickUpgrade.laserGunDamage} obrażeń slimom. Laser przebija i usuwa pociski wroga. {PickUpgrade.laser2XChance}% szans na dwukrotny strzał";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od lasera z {PickUpgrade.laserGunDamage} do {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}pkt. Skraca czas strzału lasera z {PickUpgrade.laserGunTime.ToString("F2")} sek. do {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}. sek. Zwiększa szansę na dwukrotny strzał z {PickUpgrade.laser2XChance}% do {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Arma laser";

                if (PickUpgrade.choseLaserGun == false)
                {
                    upgradeHoverDesc = $"Orbital de morango: Uma arma laser gira em torno do morango e dispara um laser a cada {PickUpgrade.laserGunTime.ToString("F2")} segundo, causando {PickUpgrade.laserGunDamage} de dano a gosmas. O laser perfura e elimina as balas inimigas. {PickUpgrade.laser2XChance}% de chance de disparar duas vezes";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do laser de {PickUpgrade.laserGunDamage} para {PickUpgrade.laserGunDamage + PickUpgrade.laserGunDamageIncrease}. Diminui o tempo de disparo do laser de {PickUpgrade.laserGunTime.ToString("F2")} s para {(PickUpgrade.laserGunTime - PickUpgrade.laserGunTimeDecrease).ToString("F2")}. s. Aumenta a chance de disparar duas vezes de {PickUpgrade.laser2XChance}% para {PickUpgrade.laser2XChance + PickUpgrade.laser2XChanceIncrease}%";
                }
            }
        }
        #endregion

        #region sword
        if (upgradeName == "sword")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "sword";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"On slime click: Chance to spawn a sword that slashes random slimes 7 times, dealing {PickUpgrade.swordDamage} damage each slash. Uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the sword slash damage from {PickUpgrade.swordDamage} to {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "SCHWERT";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, ein Schwert zu beschwören, das 7-mal zufällige Slimes schlägt und pro Schlag {PickUpgrade.swordDamage} Schaden verursacht. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schwertschlag-Schaden von {PickUpgrade.swordDamage} auf {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ソード";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： ランダムなスライムを7回斬りつけ、1回につき{PickUpgrade.swordDamage}ダメージを与える。アップグレードスロットを1つ使用する！";
                }
                else
                {
                    upgradeHoverDesc = $"ソードの斬撃ダメージが{PickUpgrade.swordDamage}から{PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "épée";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"Par clic sur slime : Chance de faire apparaître une épée qui tranche 7 slimes aléatoires, infligeant {PickUpgrade.swordDamage} dégâts à chaque coup. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts des coups d'épée de {PickUpgrade.swordDamage} à {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "espada";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"Al hacer click en un slime: Probabilidad de generar una espada que golpea a slimes aleatorios 7 veces, haciendo {PickUpgrade.swordDamage} de daño por golpe. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del golpe de espada de {PickUpgrade.swordDamage} a {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "利剑";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率生成一把利剑，可随机斩击史莱姆7次，每次斩击造成{PickUpgrade.swordDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"利剑斩击的伤害值从{PickUpgrade.swordDamage}提高到{PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "검";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 무작위 슬라임을 7번 베는 검을 생성하여 각 베기마다 {PickUpgrade.swordDamage}의 데미지를 입힐 확률이 있습니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"검 슬래시 데미지를 {PickUpgrade.swordDamage} 에서 {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "меч";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"При клике на слизня: шанс вызвать меч, который рубит случайных слизней 7 раз, нанося {PickUpgrade.swordDamage} ед. урона за удар. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от удара мечом с {PickUpgrade.swordDamage} до {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "miecz";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"Po kliknięciu slime: Szansa na przywołanie miecza, który tnie losowe slimy 7 razy, zadając {PickUpgrade.swordDamage} obrażenia za każde cięcie. Wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od cięcia mieczem z {PickUpgrade.swordDamage} do {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Espada";

                if (PickUpgrade.choseSword == false)
                {
                    upgradeHoverDesc = $"Ao clicar na gosma: Chance de gerar uma espada que corta gosmas aleatórias 7 vezes, causando {PickUpgrade.swordDamage} de dano a cada corte. Usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do corte da espada de {PickUpgrade.swordDamage} para {PickUpgrade.swordDamage + PickUpgrade.swordDamageIncrease}";
                }
            }
        }
        #endregion

        #region poison dart
        if (upgradeName == "poisonDart")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "poison dart";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"On slime death: Chance to shoot a poison dart that deals {PickUpgrade.poisonDartDamage} damage + {PickUpgrade.poisonDamage} poison damage for 4 seconds. The poison dart targets the slime that is furthest away from the strawberry. Uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the poison dart damage from {PickUpgrade.poisonDartDamage} to {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} and the poison damage from {PickUpgrade.poisonDamage} to {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "GIFTPFEIL";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Kill: Chance, einen Giftpfeil abzuschießen, der {PickUpgrade.poisonDartDamage} Schaden + {PickUpgrade.poisonDamage} Giftschaden für 4 Sekunden verursacht. Der Giftpfeil zielt auf den Slime, der am weitesten von der Erdbeere entfernt ist. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Giftpfeil-Schaden von {PickUpgrade.poisonDartDamage} auf {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} und den Giftschaden von {PickUpgrade.poisonDamage} auf {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "毒のダーツ";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"スライムを倒すと 4秒間、確率で{PickUpgrade.poisonDartDamage}ダメージ{PickUpgrade.poisonDamage}毒を与える。毒のダーツはいちごから離れたスライムを狙う。アップグレードスロットを1つ使用する！";
                }
                else
                {
                    upgradeHoverDesc = $"毒のダメージのダメージが{PickUpgrade.poisonDartDamage}から{PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease}に、ポーションダメージが{PickUpgrade.poisonDamage}から{PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}に増加します。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "fléchette empoisonnée";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"À la mort du slime : Chance de tirer une fléchette empoisonnée qui inflige {PickUpgrade.poisonDartDamage} dégâts + {PickUpgrade.poisonDamage} dégât de poison pendant 4 secondes. La fléchette empoisonnée cible le slime le plus éloigné de la fraise. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts de la fléchette empoisonnée de {PickUpgrade.poisonDartDamage} à {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} et les dégâts de poison de {PickUpgrade.poisonDamage} à {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "dardo venenoso";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"Al matar un slime: Probabilidad de disparar un dardo venenoso que hace {PickUpgrade.poisonDartDamage} de daño + {PickUpgrade.poisonDamage} de daño de veneno durante 4 segundos. El dardo venenoso apunta al slime más alejado de la fresa. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño del dardo venenoso de {PickUpgrade.poisonDartDamage} a {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} y el daño de veneno de {PickUpgrade.poisonDamage} a {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "毒镖";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"击杀史莱姆：有几率射出一支毒镖，可造成{PickUpgrade.poisonDartDamage}点伤害+{PickUpgrade.poisonDamage} 点毒药伤害，持续4秒。毒镖的目标是离草莓较远的史莱姆。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"毒镖的伤害值从{PickUpgrade.poisonDartDamage}提高到{PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} ，毒药伤害从{PickUpgrade.poisonDamage}提高到{PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "독침";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"슬라임 죽음 시: 4초 동안 {PickUpgrade.poisonDartDamage} 의 데미지 + {PickUpgrade.poisonDamage} 의 독 데미지를 입히는 독침을 발사할 확률이 있습니다. 독침은 딸기에서 가장 멀리 있는 슬라임을 목표로 합니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"독침 데미지를 {PickUpgrade.poisonDartDamage}에서 {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} 로, 독 데미지를 {PickUpgrade.poisonDamage}에서  {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "отравленный дротик";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"При смерти слизня: шанс выстрелить отравленным дротиком, который наносит {PickUpgrade.poisonDartDamage} ед. урона + {PickUpgrade.poisonDamage} ед. урона ядом на 4 сек. Отравленный дротик нацелен на слизня макс. удаленного от клубники. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от отравленного дротика с {PickUpgrade.poisonDartDamage} до {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} и урон от яда с {PickUpgrade.poisonDamage} до {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "trująca strzałka";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"Po zabiciu slime: Szansa na wystrzelenie zatrutej strzałki, która zadaje {PickUpgrade.poisonDartDamage} obrażeń + {PickUpgrade.poisonDamage}  obrażenie od trucizny przez 4 sek. Strzałka z trucizną celuje w slime, który znajduje się najdalej od truskawki. Zużywa 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od trujących strzałek z{PickUpgrade.poisonDartDamage} do {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} oraz obrażenia od zatrucia z {PickUpgrade.poisonDamage} do {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Dardo venenoso";

                if (PickUpgrade.chosePoisonDart == false)
                {
                    upgradeHoverDesc = $"Ao matar uma gosma: Chance de disparar um dardo venenoso que causa {PickUpgrade.poisonDartDamage} de dano + {PickUpgrade.poisonDamage} de dano venenoso por 4 segundos. O dardo venenoso tem como alvo a gosma que estiver mais distante do morango. Usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do dardo venenoso de {PickUpgrade.poisonDartDamage} para {PickUpgrade.poisonDartDamage + PickUpgrade.poisonDartDamageIncrease} e o dano de veneno de {PickUpgrade.poisonDamage} para {PickUpgrade.poisonDamage + PickUpgrade.poisonDamageIncrease}";
                }
            }
        }
        #endregion

        #region strawberryShield
        if (upgradeName == "strawberryShield")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "strawberry shield";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: A shield will spin around the strawberry, blocking incoming slime bullets";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the size of the shield";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "ERDBEEREN-SCHILD";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Ein Schild rotiert um die Erdbeere und blockiert ankommende Slime-Projektile";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht die Größe des Schildes";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "いちごシールド";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"いちごの軌道： いちごの周りをシールドが回転し、飛んでくるスライム弾を防ぐ。";
                }
                else
                {
                    upgradeHoverDesc = $"シールドのサイズが大きくなる";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "bouclier fraise";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Orbital de la fraise : Un bouclier tourne autour de la fraise, bloquant les balles slime entrantes.";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente la taille du bouclier";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "escudo de fresa";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: Un escudo girará alrededor de la fresa, bloqueando las balas de slime entrantes";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el tamaño del escudo";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "草莓护盾";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"草莓轨道: 护盾会围绕草莓旋转，挡住射来的史莱姆子弹";
                }
                else
                {
                    upgradeHoverDesc = $"增加护盾的大小";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "딸기 방패";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: 방패가 딸기 주위를 회전하며 날아오는 슬라임 총알을 막습니다.";
                }
                else
                {
                    upgradeHoverDesc = $"방패의 크기를 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "клубничный щит";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Вокруг клубники: щит будет вращаться вокруг клубники, блокируя подлетающие пули";
                }
                else
                {
                    upgradeHoverDesc = $"Увеличивает размер щита";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "truskawkowa tarcza";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: Wokół truskawki obraca się tarcza, blokując nadlatujące pociski";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa rozmiar osłony";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Escudo de morango";

                if (PickUpgrade.choseStrawberryShield == false)
                {
                    upgradeHoverDesc = $"Morango orbital: Um escudo girará em torno do morango, bloqueando as balas de gosma que chegam";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o tamanho do escudo";
                }
            }
        }
        #endregion

        #region chained ball
        if (upgradeName == "chainBall")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "chained cannonball";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Strawberry orbital: A cannonball attached to a chain will spin around the strawberry, dealing {PickUpgrade.chainBallDamage} damage to slimes";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the chained cannonball damage from {PickUpgrade.chainBallDamage} to {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}. also increases the spin speed";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "ANGEKETTETE KANONENKUGEL";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Eine an einer Kette befestigte Kanonenkugel rotiert um die Erdbeere und verursacht {PickUpgrade.chainBallDamage} Schaden an Slimes";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden der Ketten-Kanonenkugel von {PickUpgrade.chainBallDamage} auf {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease} und erhöht zudem die Rotationsgeschwindigkeit";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "チェーンキャノンボール";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"いちごの軌道： チェーンにつながれたキャノンボールがいちごの周りを回転し、スライムに{PickUpgrade.chainBallDamage}ダメージを与える。";
                }
                else
                {
                    upgradeHoverDesc = $"チェーンキャノンボールのダメージが{PickUpgrade.chainBallDamage}から{PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}に増加。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "canon enchaîné";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Orbital de la fraise : Un boulet de canon attaché à une chaîne tournera autour de la fraise, infligeant {PickUpgrade.chainBallDamage} dégâts aux slimes";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts du boulet de canon enchaîné de {PickUpgrade.chainBallDamage} à {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}. Augmente également la vitesse de rotation";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "bola de cañón encadenada";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Orbital de la fresa: Una bola de cañón atada a una cadena girará alrededor de la fresa, haciendo {PickUpgrade.chainBallDamage} de daño a los slimes";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de la bola de cañón encadenada de {PickUpgrade.chainBallDamage} a {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}. también aumenta la velocidad de giro";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "链式炮弹";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"草莓轨道: 链式炮弹会绕着草莓旋转，对史莱姆造成{PickUpgrade.chainBallDamage}点伤害";
                }
                else
                {
                    upgradeHoverDesc = $"链式炮弹的伤害值从{PickUpgrade.chainBallDamage}提高到{PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}，同时提高旋转速度";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "쇠사슬 포탄";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: 쇠사슬에 연결된 포탄이 딸기 주위를 회전하며 슬라임에게 {PickUpgrade.chainBallDamage}의 데미지를 입힙니다.";
                }
                else
                {
                    upgradeHoverDesc = $"쇠사슬 포탄 데미지를{PickUpgrade.chainBallDamage}에서 {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}로 증가시키고 회전 속도도 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "ядро на цепи";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Вокруг клубники: ядро на цепи будет вращаться вокруг клубники, нанося {PickUpgrade.chainBallDamage} ед. урона слизням";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от ядра на цепи с {PickUpgrade.chainBallDamage} до {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}. Также увеличивает скорость вращения";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "kula na łańcuchu";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: Kula armatnia przymocowana do łańcucha będzie wirować wokół truskawki, zadając {PickUpgrade.chainBallDamage} obrażenia slimom";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zadawane przez kulę armatnią z {PickUpgrade.chainBallDamage} do {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}. Zwiększa również prędkość wirowania.";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Bola de canhão acorrentadal";

                if (PickUpgrade.choseChainBall == false)
                {
                    upgradeHoverDesc = $"Orbital do morango: Uma bala de canhão presa a uma corrente girará em torno do morango, causando {PickUpgrade.chainBallDamage} de dano às gosmas.";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano da bala de canhão em cadeia de {PickUpgrade.chainBallDamage} para {PickUpgrade.chainBallDamage + PickUpgrade.chainBallDamageIncrease}. Também aumenta a velocidade de giro";
                }
            }
        }
        #endregion

        #region Thorn
        if (upgradeName == "Thorn")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "Vine Thorns";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"On slime death: Chance to shoot 4 vine thorns, each dealing {PickUpgrade.thornDamage} damage. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the thorn damage from {PickUpgrade.thornDamage} to {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "RANKENDORNE";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Tod: Chance 4 Rankendorne abzuschießen, die jeweils {PickUpgrade.thornDamage} Schaden verursachen. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Dornschaden von {PickUpgrade.thornDamage} auf {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ツタのトゲ";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"スライム死亡時：4 本の蔓の棘を発射するチャンスがあり、それぞれ{PickUpgrade.thornDamage}のダメージを与えます。アップグレード スロットを 1 つ使用します。";
                }
                else
                {
                    upgradeHoverDesc = $"トゲのダメージが{PickUpgrade.thornDamage}から{PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "Épines de Vigne";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"À la mort du slime : Chance de tirer 4 épines de vigne, chacune infligeant {PickUpgrade.thornDamage} dégâts. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts des épines de {PickUpgrade.thornDamage} à {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "Espinas de Enredadera";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"Al morir un slime: Probabilidad de disparar 4 espinas de enredadera, cada una haciendo {PickUpgrade.thornDamage} de daño. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de las espinas de {PickUpgrade.thornDamage} a {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "藤刺";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"击杀史莱姆：有几率射出4根藤刺，每根造成{PickUpgrade.thornDamage}点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"藤刺的伤害值从{PickUpgrade.thornDamage}提高到{PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "덩굴 가시";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"슬라임 죽음 시: 각각{PickUpgrade.thornDamage} 의 데미지를 입히는 4개의 덩굴 가시를 발사할 확률이 있습니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"가시 데미지를 {PickUpgrade.thornDamage} 에서 {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "Шипы лозы";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"При смерти слизня: шанс выстрелить 4 шипами лозы, каждый из которых наносит {PickUpgrade.thornDamage} ед. урона. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от шипов с {PickUpgrade.thornDamage} до {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "Ciernie winorośli";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"Po śmierci slime: Szansa na wystrzelenie 4 kolców winorośli, z których każdy zadaje {PickUpgrade.thornDamage} obrażenia. wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia od cierni z {PickUpgrade.thornDamage} do {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Espinhos de Videira";

                if (PickUpgrade.choseThorn == false)
                {
                    upgradeHoverDesc = $"Na morte da gosma: Chance de disparar 4 espinhos de videira, cada um causando {PickUpgrade.thornDamage} de dano. usa 1 slot de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do espinho de {PickUpgrade.thornDamage} para {PickUpgrade.thornDamage + PickUpgrade.thornDamageIncrease}";
                }
            }
        }
        #endregion

        #region Boulder
        if (upgradeName == "Boulder")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "rolling boulder";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"on slime click: Chance to shoot a rolling boulder that pierces all slimes, dealing {PickUpgrade.boulderDamage} damage. the boulder targets the slime that is furthest away from the slime click position. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the boulder damage from {PickUpgrade.boulderDamage} to {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "ROLLENDER FELS";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Klick: Chance, einen rollenden Fels abzuschießen, der alle Slimes durchdringt und {PickUpgrade.boulderDamage} Schaden verursacht. Der Fels zielt auf den Slime, der am weitesten vom Klickpunkt entfernt ist. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Fels-Schaden von {PickUpgrade.boulderDamage} auf {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ローリングストーン";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"スライムクリック時： 全てのスライムを貫通し、{PickUpgrade.boulderDamage}ダメージを与えるローリングストーンを放つチャンス。スライムクリック位置から最も遠いスライムをターゲットにする！";
                }
                else
                {
                    upgradeHoverDesc = $"ストーンのダメージが{PickUpgrade.boulderDamage}から{PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "pierre roulante";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"par clic sur slime : Chance de tirer un rocher roulent qui perfore tous les slimes, infligeant {PickUpgrade.boulderDamage} dégâts. Le rocher cible le slime le plus éloigné de la position du clic. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts du rocher de {PickUpgrade.boulderDamage} à {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "roca rodante";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"al hacer click en un slime: Probabilidad de disparar una roca rodante que atraviesa todos los slimes, haciendo {PickUpgrade.boulderDamage} de daño. la roca apunta al slime más alejado de la posición del click. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de la roca de {PickUpgrade.boulderDamage} a {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "滚动巨石";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"点击史莱姆：有几率射出一块滚动巨石，击穿所有史莱姆并造成{PickUpgrade.boulderDamage}点伤害。巨石的目标是离点击史莱姆位置最远的史莱姆。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"巨石的伤害值从{PickUpgrade.boulderDamage}提高到{PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "굴러가는 바위";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"슬라임 클릭 시: 모든 슬라임을 관통하는 굴러가는 바위를 발사하여 {PickUpgrade.boulderDamage}의 데미지를 입힐 확률이 있습니다. 바위는 슬라임 클릭 위치에서 가장 멀리 있는 슬라임을 목표로 합니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"바위 데미지를 {PickUpgrade.boulderDamage} 에서 {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "катящийся валун";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"при клике на слизня: шанс выстрелить катящимся валуном, который пробивает всех слизней, нанося {PickUpgrade.boulderDamage} ед. урона. валун нацелен на слизня макс. удаленного от места клика на слизня. использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от валуна с {PickUpgrade.boulderDamage} до {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "toczący się głaz";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"po kliknięciu slime: Szansa na wystrzelenie toczącego się głazu, który przebija wszystkie slimy, zadając {PickUpgrade.boulderDamage} obrażenia. głaz celuje w slime znajdującego się najdalej od kliknięcia slime. wykorzystuje 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zadawane przez głazy z {PickUpgrade.boulderDamage} do {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "Pedregulho rolante";

                if (PickUpgrade.choseBoulder == false)
                {
                    upgradeHoverDesc = $"ao clicar na gosma: Chance de disparar uma pedra rolante que perfura todas as gosmas, causando {PickUpgrade.boulderDamage} de dano. A pedra tem como alvo a gosma que está mais distante da posição de clique da gosma. usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do pedregulho de {PickUpgrade.boulderDamage} para {PickUpgrade.boulderDamage + PickUpgrade.boulderDamageIncrease}";
                }
            }
        }
        #endregion

        #region Bouncy ball
        if (upgradeName == "BouncyBall")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "bouncy ball";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"on slime death: chance to shoot a bouncy ball that targets a random slime and bounces 1 time with the chance of bouncing 3 times, dealing {PickUpgrade.bouncyBallDamage} damage. uses 1 upgrade slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the bouncy ball damage from {PickUpgrade.bouncyBallDamage} to {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "SPRINGENDER BALL";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"Bei Slime-Tod: Chance, einen springenden Ball abzuschießen, der einen zufälligen Slime anvisiert, einmal abprallt (mit der Chance, bis zu 3-mal abzuprallen) und {PickUpgrade.bouncyBallDamage} Schaden verursacht. Benutzt 1 Upgrade-Slot!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Schaden des springenden Balls von {PickUpgrade.bouncyBallDamage} auf {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "バウンドボール";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"スライム死亡時：ランダムなスライムをターゲットにした弾むボールを放ち、1回バウンドし、一定の確率で3回バウンドし、{PickUpgrade.bouncyBallDamage}ダメージを与える！";
                }
                else
                {
                    upgradeHoverDesc = $"バウンドボールのダメージが{PickUpgrade.bouncyBallDamage}から{PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}に増加する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "balle rebondissante";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"À la mort du slime : Chance de tirer une balle rebondissante qui cible un slime aléatoire et rebondit 1 fois avec la possibilité de rebondir 3 fois, infligeant {PickUpgrade.bouncyBallDamage} dégâts. Utilise un emplacement d'amélioration !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts de la balle rebondissante de {PickUpgrade.bouncyBallDamage} à {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "pelota rebotadora";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"al morir un slime: probabilidad de disparar una pelota rebotadora que apunta a un slime aleatorio y rebota 1 vez con la probabilidad de rebotar 3 veces, haciendo {PickUpgrade.bouncyBallDamage} de daño. ¡Usa 1 espacio de mejora!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de la pelota rebotadora de {PickUpgrade.bouncyBallDamage} a {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "弹球";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"击杀史莱姆：有几率射出一个弹球。这个弹球随机瞄准一个史莱姆，弹跳1次（有几率弹跳3次）并造成{PickUpgrade.bouncyBallDamage} 点伤害。使用1个升级槽！";
                }
                else
                {
                    upgradeHoverDesc = $"弹球的伤害值从{PickUpgrade.bouncyBallDamage}提高到{PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "통통볼";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"슬라임 죽음 시: 무작위 슬라임을 목표로 하고 1번 튕기며 3번 튕길 확률이 있는 통통볼을 발사하여 {PickUpgrade.bouncyBallDamage} 의 데미지를 입힐 확률이 있습니다. 업그레이드 슬롯 1개 사용!";
                }
                else
                {
                    upgradeHoverDesc = $"통통볼 데미지를 {PickUpgrade.bouncyBallDamage}에서 {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "прыгучий мяч";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"при смерти слизня: шанс выстрелить прыгучим мячом, который нацелен на случайного слизня и отскакивает 1 раз, с шансом на отскок {PickUpgrade.bouncyBallDamage} раза, нанося 4 ед. урона. Использует 1 слот улучшения!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон от прыгучего мяча с {PickUpgrade.bouncyBallDamage} до {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "dmuchana piłka";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"przy śmierci slime: szansa na wystrzelenie odbijającej się kuli, która celuje w losowego slime i odbija się 1 raz z szansą na odbicie się 3 razy, zadając {PickUpgrade.bouncyBallDamage} obrażenia. zużywa 1 slot na ulepszenie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zadawane przez odbijającą się piłkę z {PickUpgrade.bouncyBallDamage} do {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "bola saltitante";

                if (PickUpgrade.choseBouncyBall == false)
                {
                    upgradeHoverDesc = $"na morte da gosma: chance de atirar uma bola saltitante que tem como alvo uma gosma aleatória e quica 1 vez com a chance de quicar 3 vezes, causando {PickUpgrade.bouncyBallDamage}  de dano. usa 1 espaço de aprimoramento!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano da bola saltitante de {PickUpgrade.bouncyBallDamage} para {PickUpgrade.bouncyBallDamage + PickUpgrade.bouncyBallDamageIncrease}";
                }
            }
        }
        #endregion

        #region stapler
        if (upgradeName == "Stapler")
        {
            if (languageSelected == 1) //English
            {
                upgradeHoverName = "stapler";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"strawberry orbital: shoots a staple every {PickUpgrade.staplerTimer} second, dealing {PickUpgrade.staplerDamage} damage and stops the hit slime for {PickUpgrade.staplerStunTine} seconds. clicking on a slime also has a chance to staple the slime!";
                }
                else
                {
                    upgradeHoverDesc = $"Increases the stapler damage from {PickUpgrade.staplerDamage} to {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, decreases stapler shoot time from {PickUpgrade.staplerTimer.ToString("F2")} sec to {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")} sec and stapler stun time from {PickUpgrade.staplerStunTine} sec to {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} sec";
                }
            }
            else if (languageSelected == 2) //German
            {
                upgradeHoverName = "TACKER";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"Erdbeer-Orbital: Feuert jede {PickUpgrade.staplerTimer} Sekunde eine Tacker-Klammer ab, die  {PickUpgrade.staplerDamage} Schaden verursacht und den Slime {PickUpgrade.staplerStunTine} Sekunden lang stoppt. Das Anklicken eines Slimes hat ebenfalls eine Chance, den Slime zu tackern!";
                }
                else
                {
                    upgradeHoverDesc = $"Erhöht den Tacker-Schaden von {PickUpgrade.staplerDamage} auf {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, verringert die Tacker-Schussfrequenz von {PickUpgrade.staplerTimer.ToString("F2")} Sek. auf {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")} Sek. und erhöht die Betäubungszeit von {PickUpgrade.staplerStunTine} Sek. auf {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} Sek.";
                }
            }
            else if (languageSelected == 3) //Japanese
            {
                upgradeHoverName = "ホチキス";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"いちご軌道：{PickUpgrade.staplerTimer}秒毎にホッチキスを発射し、{PickUpgrade.staplerDamage}ダメージを与え、スライムを{PickUpgrade.staplerStunTine}秒間停止させる！";
                }
                else
                {
                    upgradeHoverDesc = $"ホッチキスのダメージを{PickUpgrade.staplerDamage}から{PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}に、ホッチキスの発射時間を{PickUpgrade.staplerTimer.ToString("F2")}秒から{(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")}秒に、ホッチキスの気絶時間を{PickUpgrade.staplerStunTine}秒から{PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease}秒に変更する。";
                }
            }
            else if (languageSelected == 4) //French
            {
                upgradeHoverName = "agrafeuse";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"Orbital de la fraise : Tire une agrafe toutes les {PickUpgrade.staplerTimer} seconde, infligeant {PickUpgrade.staplerDamage} égâts et immobilisant le slime pendant {PickUpgrade.staplerStunTine} secondes. Cliquer sur un slime a également une chance de l'agrafer !";
                }
                else
                {
                    upgradeHoverDesc = $"Augmente les dégâts de l'agrafeuse de {PickUpgrade.staplerDamage} à {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, réduit le temps de tir de l'agrafeuse de {PickUpgrade.staplerTimer.ToString("F2")} seconde à {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")} seconde et augmente le temps d'étourdissement de l'agrafeuse de {PickUpgrade.staplerStunTine} secondes à {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} secondes";
                }
            }
            else if (languageSelected == 5) //Spanish
            {
                upgradeHoverName = "grapadora";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"orbital de la fresa: dispara una grapa cada {PickUpgrade.staplerTimer.ToString("F2")} segundo, haciendo {PickUpgrade.staplerDamage} de daño y detiene al slime durante {PickUpgrade.staplerStunTine} segundos. ¡hacer click en un slime también tiene probabilidad de graparlo!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta el daño de la grapadora de {PickUpgrade.staplerDamage} a {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, reduce el tiempo de disparo de la grapadora de {PickUpgrade.staplerTimer.ToString("F2")} seg a {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")} seg y el tiempo de aturdimiento de {PickUpgrade.staplerStunTine} seg a {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} seg";
                }
            }
            else if (languageSelected == 6) //Chinese
            {
                upgradeHoverName = "锚钉发射器";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"草莓轨道：{PickUpgrade.staplerTimer}秒发射一枚锚钉，造成{PickUpgrade.staplerDamage}点伤害并让史莱姆停下 {PickUpgrade.staplerStunTine}秒。点击史莱姆也有几率钉住史莱姆！";
                }
                else
                {
                    upgradeHoverDesc = $"锚钉发射器的伤害从{PickUpgrade.staplerDamage}提高到{PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, 锚钉发射器射击时间从{PickUpgrade.staplerTimer.ToString("F2")}秒减少到{(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")}秒，锚钉发射器眩晕时间从{PickUpgrade.staplerStunTine}秒提高到{PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease}秒";
                }
            }
            else if (languageSelected == 7) //Korean
            {
                upgradeHoverName = "스테이플러";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"딸기 궤도: {PickUpgrade.staplerTimer}초마다 스테이플을 발사하여 {PickUpgrade.staplerDamage} 의 데미지를 입히고 슬라임을 {PickUpgrade.staplerStunTine}초 동안 멈추게 합니다. 슬라임을 클릭하면 슬라임을 스테이플로 고정할 확률도 있습니다!";
                }
                else
                {
                    upgradeHoverDesc = $"스테이플러 데미지를 {PickUpgrade.staplerDamage}에서 {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, 로 증가시키고, 스테이플러 발사 시간을 {PickUpgrade.staplerTimer.ToString("F2")}초에서 {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")}초로 감소시키며, 스테이플러 기절 시간을 {PickUpgrade.staplerStunTine}초에서  {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} 초로 증가시킵니다.";
                }
            }
            else if (languageSelected == 8) //Russian
            {
                upgradeHoverName = "степлер";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"вокруг клубники: стреляет скобой {PickUpgrade.staplerTimer} раз в секунду, нанося {PickUpgrade.staplerDamage} ед. урона и останавливая слизня на {PickUpgrade.staplerStunTine} сек. клик на слизня также может остановить его!";
                }
                else
                {
                    upgradeHoverDesc = $"Повышает урон степлера с {PickUpgrade.staplerDamage} до {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, изменяет время выстрела с {PickUpgrade.staplerTimer.ToString("F2")} до {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")} сек., а время оглушения — с {PickUpgrade.staplerStunTine} до {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} сек.";
                }
            }
            else if (languageSelected == 9) //Polish
            {
                upgradeHoverName = "zszywacz";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"Orbituje wokół truskawki: wystrzeliwuje zszywkę co {PickUpgrade.staplerTimer} sekundę, zadając {PickUpgrade.staplerDamage} obrażenia i zatrzymując slime n {PickUpgrade.staplerStunTine} sekundy. Kliknięcie slime ma również szansę na jego zszycie!";
                }
                else
                {
                    upgradeHoverDesc = $"Zwiększa obrażenia zszywacza z {PickUpgrade.staplerDamage} do {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, skraca czas wystrzału zszywacza z {PickUpgrade.staplerTimer.ToString("F2")} sek. do {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")}  sek. i czas ogłuszenia zszywacza z {PickUpgrade.staplerStunTine} sek. do {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} sek.";
                }
            }
            else if (languageSelected == 10) //Portugese
            {
                upgradeHoverName = "grampeador";

                if (PickUpgrade.choseStapler == false)
                {
                    upgradeHoverDesc = $"morango orbital: dispara um grampo a cada {PickUpgrade.staplerTimer} segundo, causando {PickUpgrade.staplerDamage} de dano e parando a gosma por {PickUpgrade.staplerStunTine} egundos. clicar em uma gosma também tem a chance de grampear a gosma!";
                }
                else
                {
                    upgradeHoverDesc = $"Aumenta o dano do grampeador de {PickUpgrade.staplerDamage} para {PickUpgrade.staplerDamage + PickUpgrade.staplerDamageIncrease}, diminui o tempo de disparo do grampeador de {PickUpgrade.staplerTimer.ToString("F2")}seg. para {(PickUpgrade.staplerTimer - PickUpgrade.staplerTimerDecrease).ToString("F2")} seg. e o tempo de atordoamento do grampeador de {PickUpgrade.staplerStunTine} seg. para {PickUpgrade.staplerStunTine + PickUpgrade.staplerStunTimeIncrease} seg.";
                }
            }
        }
        #endregion
    }
    #endregion

    #region Strings that change
    public void ChangingStrings(int number)
    {
        if (languageSelected == 1) //English
        {
            youReachedWave = $"you reached wave {number}!";
        }
        else if (languageSelected == 2) //German
        {
            youReachedWave = $"Du hast Welle {number} erreicht!";
        }
        else if (languageSelected == 3) //Japanese
        {
            youReachedWave = $"ウェーブ{number}に到達!";
        }
        else if (languageSelected == 4) //French
        {
            youReachedWave = $"vous avez atteint la vague {number}!";
        }
        else if (languageSelected == 5) //Spanish
        {
            youReachedWave = $"¡has llegado a la oleada {number}!";
        }
        else if (languageSelected == 6) //Chinese
        {
            youReachedWave = $"你已挺过{number}波攻击！";
        }
        else if (languageSelected == 7) //Korean
        {
            youReachedWave = $"{number} 웨이브에 도달했습니다!";
        }
        else if (languageSelected == 8) //Russian
        {
            youReachedWave = $"идет {number}-я волна!";
        }
        else if (languageSelected == 9) //Polish
        {
            youReachedWave = $"dotarłeś do fali {number}!";
        }
        else if (languageSelected == 10) //Portugese
        {
            youReachedWave = $"você alcançou a onda {number}!";
        }
    }
    #endregion

    public TextMeshProUGUI fullscreenText, backgroundText, backText;

    public TextMeshProUGUI upgradeNameText, upgradeLevelText, upgradeDesText;

    public static string hoverName;

    public ManageSlots slotsScript;

    public void ChangeText()
    {
        englishFlag.SetActive(false); germanFlag.SetActive(false); japaneseFlag.SetActive(false);
        frenchFlag.SetActive(false); spanishFlag.SetActive(false); chineseFlag.SetActive(false);
        koreanFlag.SetActive(false); russianFlag.SetActive(false); polishFlag.SetActive(false);
        portugeseFlag.SetActive(false);

        slotsScript.ChangeAllTexts();

        if (Settings.saveFullsScreen == 1) { fullscreenText.text = fullscreen + "" + OFF; }
        if (Settings.saveFullsScreen == 0) { fullscreenText.text = fullscreen + "" + ON; }

        backgroundText.text = background + "(" + MainMenu.backgroundNumber +")";
        if(MainMenu.isInMainMenu == true)
        {
            backText.text = back;
        }
        else { backText.text = close; }

        if(PickUpgrade.isInChooseUpgrade == true)
        {
            SetUpgradeHoverText(hoverName);

            upgradeNameText.text = upgradeHoverName;
            upgradeLevelText.text = $"{level} {HoverUpgrades.currentHoverLevel + 1}";
            upgradeDesText.text = upgradeHoverDesc;
        }
    }
}
