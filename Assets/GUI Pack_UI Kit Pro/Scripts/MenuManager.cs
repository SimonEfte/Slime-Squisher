using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


namespace GUIPack_CasualGame
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Main")]
        public GameObject screen1;
        [Header("MenuPopup")]
        public GameObject MenuPopup2;
        [Header("AvatarScreen")]
        public GameObject AvatarScreen3;

        [Header("AcquirePopup")]
        public GameObject AcquirePopup4;
        [Header("AchievementScreen")]
        public GameObject AchievementScreen5; 
        [Header("SkillScreen")]
        public GameObject SkillScreen6; 
        [Header("GuildScreen")]
        public GameObject GuildScreen7;
        [Header("ChangeMarkPopup")]
        public GameObject ChangeMarkPopup8;
        [Header("ContentScene")]
        public GameObject ContentScene9;
        [Header("TreasureScreen")]
        public GameObject TreasureScreen10; 
        [Header("ExchangeScreen")]
        public GameObject ExchangeScreen11; 
        [Header("CraftingScreen")]
        public GameObject CraftingScreen12; 
        [Header("MonsterCoreScreen")]
        public GameObject MonsterCoreScreen13; 
        [Header("BindingScreen")]
        public GameObject BindingScreen14; 
        [Header("DeathLogScreen")]
        public GameObject DeathLogScreen15; 
        [Header("SettingPopup")]
        public GameObject SettingPopup16; 
        [Header("LanguagePopup")]
        public GameObject LanguagePopup17;
        [Header("MailboxPopup")]
        public GameObject MailboxPopup18; 
        [Header("NoticePopup")]
        public GameObject NoticePopup19; 
        [Header("ShopScreen")]
        public GameObject ShopScreen20;

        [Header("GoldPopup_01")]
        public GameObject GoldPopup_01_21;
        [Header("GoldPopup_02")]
        public GameObject GoldPopup_02_22;
        [Header("GoldPopup_03")]
        public GameObject GoldPopup_03_23;
        [Header("GoldPopup_04")]
        public GameObject GoldPopup_04_24;
        [Header("GoldPopup_05")]
        public GameObject GoldPopup_05_25;
        [Header("GoldPopup_06")]
        public GameObject GoldPopup_06_26;
        [Header("GoldPopup_07")]
        public GameObject GoldPopup_07_27;

        [Header("GemPopup_01")]
        public GameObject GemPopup_01_28;
        [Header("GemPopup_02")]
        public GameObject GemPopup_02_29; 
        [Header("GemPopup_03")]
        public GameObject GemPopup_03_30;
        [Header("GemPopup_04")]
        public GameObject GemPopup_04_31;
        [Header("GemPopup_05")]
        public GameObject GemPopup_05_32;
        [Header("GemPopup_06")]
        public GameObject GemPopup_06_33; 
        [Header("GemPopup_07")]
        public GameObject GemPopup_07_34;

        [Header("MoneyPopup_01")]
        public GameObject MoneyPopup_01_35;
        [Header("MoneyPopup_02")]
        public GameObject MoneyPopup_02_36;
        [Header("MoneyPopup_03")]
        public GameObject MoneyPopup_03_37;
        [Header("MoneyPopup_04")]
        public GameObject MoneyPopup_04_38;
        [Header("MoneyPopup_05")]
        public GameObject MoneyPopup_05_39;
        [Header("MoneyPopup_06")]
        public GameObject MoneyPopup_06_40; 
        [Header("MoneyPopup_07")]
        public GameObject MoneyPopup_07_41;

        [Header("PackagePopup_01")]
        public GameObject PackagePopup_01_42;
        [Header("PackagePopup_02")]
        public GameObject PackagePopup_02_43; 
        [Header("PackagePopup_03")]
        public GameObject PackagePopup_03_44;

        [Header("InventoryScreen")]
        public GameObject InventoryScreen_45; 
        [Header("InfoPopup")]
        public GameObject InfoPopup_46; 
        [Header("InfoPopup_02")]
        public GameObject InfoPopup_02_47; 
        [Header("UpdatePopup")]
        public GameObject UpdatePopup_48; 
        [Header("PassInfoInfoPopup")]
        public GameObject PassInfoInfoPopup_49; 
        [Header("ReceiveAllPopup")]
        public GameObject ReceiveAllPopup_50; 
        [Header("PassScreen")]
        public GameObject PassScreen_51;
        [Header("RankingScreen")]
        public GameObject RankingScreen_52; 
        [Header("ProfileFramePopup")]
        public GameObject ProfileFramePopup_53; 
        [Header("FramePopup")]
        public GameObject FramePopup_54; 

        [Header("LevelMapScreen")]
        public GameObject LevelMapScreen_55; 
        [Header("WorldMapScreen")]
        public GameObject WorldMapScreen_56; 
        [Header("WorldMap_02_Screen")]
        public GameObject WorldMap_02_Screen_57;

        [Header("ChattingPopup")]
        public GameObject ChattingPopup_58; 

        [Header("ReceiveAllPopup_02")]
        public GameObject ReceiveAllPopup_02_59; 

        [Header("GameWinPopup")]
        public GameObject GameWinPopup_60;
        [Header("GameOverPopup")]
        public GameObject GameOverPopup_61; 

        [Header("ControllerScreen")]
        public GameObject ControllerScreen_62;

        [Header("Prefabs")]
        public GameObject Prefabs_63;


        public void DisableAll()
        {
            screen1.SetActive(false);
            MenuPopup2.SetActive(false);
            AvatarScreen3.SetActive(false);
            AchievementScreen5.SetActive(false);
            SkillScreen6.SetActive(false);
            GuildScreen7.SetActive(false);
            ContentScene9.SetActive(false);
            TreasureScreen10.SetActive(false);
            ExchangeScreen11.SetActive(false);
            CraftingScreen12.SetActive(false);
            MonsterCoreScreen13.SetActive(false); 
            BindingScreen14.SetActive(false);
            DeathLogScreen15.SetActive(false); 
            ShopScreen20.SetActive(false); 
            InventoryScreen_45.SetActive(false); 
            PassScreen_51.SetActive(false); 
            RankingScreen_52.SetActive(false); 
            LevelMapScreen_55.SetActive(false); 
            WorldMapScreen_56.SetActive(false); 
            WorldMap_02_Screen_57.SetActive(false); 
            ChattingPopup_58.SetActive(false); 
            ControllerScreen_62.SetActive(false);
            Prefabs_63.SetActive(false);


        }

        public void ShowScreen(int screenNo)
        {
            switch (screenNo)
            {
                case 1:
                    DisableAll();
                    screen1.SetActive(true);
                    break;
                case 2:
                    MenuPopup2.SetActive(true);
                    break;
                case 3:
                    DisableAll();
                    AvatarScreen3.SetActive(true);
                    break;
                case 4:
                    AcquirePopup4.SetActive(true);
                    break;
                case 5:
                    DisableAll();
                    AchievementScreen5.SetActive(true);
                    break;
                case 6:
                    DisableAll();
                    SkillScreen6.SetActive(true);
                    break;
                case 7:
                    DisableAll();
                    GuildScreen7.SetActive(true);
                    break;
                case 8:
                    ChangeMarkPopup8.SetActive(true);
                    break;
                case 9:
                    DisableAll();
                    ContentScene9.SetActive(true);

                    break;
                case 10:
                    DisableAll();
                    TreasureScreen10.SetActive(true);
                    break;
                case 11:
                    DisableAll();
                    ExchangeScreen11.SetActive(true);
                    break;
                case 12:
                    DisableAll();
                    CraftingScreen12.SetActive(true);
                    break;
                case 13:
                    DisableAll();
                    MonsterCoreScreen13.SetActive(true);
                    break;
                case 14:
                    DisableAll();
                    BindingScreen14.SetActive(true);
                    break;
                case 15:
                    DisableAll();
                    DeathLogScreen15.SetActive(true);
                    break;
                case 16:
                    SettingPopup16.SetActive(true);
                    break;
                case 17:
                    LanguagePopup17.SetActive(true);
                    break;
                case 18:
                    MailboxPopup18.SetActive(true);
                    break;
                case 19:
                    NoticePopup19.SetActive(true);
                    break;
                case 20:
                    DisableAll();
                    ShopScreen20.SetActive(true);
                    break;
                case 21:
                    GoldPopup_01_21.SetActive(true);
                    break;
                case 22:
                    GoldPopup_02_22.SetActive(true);
                    break;
                case 23:
                    GoldPopup_03_23.SetActive(true);
                    break;
                case 24:
                    GoldPopup_04_24.SetActive(true);
                    break;
                case 25:
                    GoldPopup_05_25.SetActive(true);
                    break;
                case 26:
                    GoldPopup_06_26.SetActive(true);
                    break;
                case 27:
                    GoldPopup_07_27.SetActive(true);
                    break;
                case 28:
                    GemPopup_01_28.SetActive(true);
                    break;
                case 29:
                    GemPopup_02_29.SetActive(true);
                    break;
                case 30:
                    GemPopup_03_30.SetActive(true);
                    break;
                case 31:
                    GemPopup_04_31.SetActive(true);
                    break;
                case 32:
                    GemPopup_05_32.SetActive(true);
                    break;
                case 33:
                    GemPopup_06_33.SetActive(true);
                    break;
                case 34:
                    GemPopup_07_34.SetActive(true);
                    break;
                case 35:
                    MoneyPopup_01_35.SetActive(true);
                    break;
                case 36:
                    MoneyPopup_02_36.SetActive(true);
                    break;
                case 37:
                    MoneyPopup_03_37.SetActive(true);
                    break;
                case 38:
                    MoneyPopup_04_38.SetActive(true);
                    break;
                case 39:
                    MoneyPopup_05_39.SetActive(true);
                    break;
                case 40:
                    MoneyPopup_06_40.SetActive(true);
                    break;
                case 41:
                    MoneyPopup_07_41.SetActive(true); 
                    break;
                case 42:
                    PackagePopup_01_42.SetActive(true);
                    break;
                case 43:
                    PackagePopup_02_43.SetActive(true);
                    break;
                case 44:
                    PackagePopup_03_44.SetActive(true); 
                    break;
                case 45:
                    DisableAll();
                    InventoryScreen_45.SetActive(true);
                    break;
                case 46:
                    InfoPopup_46.SetActive(true);
                    break;
                case 47:
                    InfoPopup_02_47.SetActive(true); 
                    break;
                case 48:
                    UpdatePopup_48.SetActive(true); 
                    break;
                case 49:
                    PassInfoInfoPopup_49.SetActive(true); 
                    break;
                case 50:
                    ReceiveAllPopup_50.SetActive(true); 
                    break;
                case 51:
                    DisableAll();
                    PassScreen_51.SetActive(true);
                    break;
                case 52:
                    DisableAll();
                    RankingScreen_52.SetActive(true); 
                    break;
                case 53:
                    ProfileFramePopup_53.SetActive(true); 
                    break;
                case 54:
                    FramePopup_54.SetActive(true); 
                    break;
                case 55:
                    DisableAll();
                    LevelMapScreen_55.SetActive(true); 
                    break;
                case 56:
                    DisableAll();
                    WorldMapScreen_56.SetActive(true); 
                    break;
                case 57:
                    DisableAll();
                    WorldMap_02_Screen_57.SetActive(true); 
                    break;
                case 58:
                    DisableAll();
                    ChattingPopup_58.SetActive(true); 
                    break;
                case 59:
                    ReceiveAllPopup_02_59.SetActive(true); 
                    break;
                case 60:
                    GameWinPopup_60.SetActive(true); 
                    break;
                case 61:
                    GameOverPopup_61.SetActive(true); 
                    break;
                case 62:
                    ControllerScreen_62.SetActive(true);
                    break;
                case 63:
                    DisableAll();
                    Prefabs_63.SetActive(true);
                    break;
            }
        }

        public void OnShowPopup(PanelHandler pop)
        {
            pop.Show();
        }

        public void OnHidePopup(PanelHandler pop)
        {
            pop.Hide();
        }

        public void Back()
        {

            if (MenuPopup2.activeSelf)
                StartCoroutine(waitForPopupToPlay(MenuPopup2));
            else
            {
                DisableAll();
                screen1.SetActive(true);
            }
        }

        public void BackPopup()
        {
            if (AcquirePopup4.activeSelf)
                StartCoroutine(waitForPopupToPlay(AcquirePopup4));
            else if (ChangeMarkPopup8.activeSelf)
                StartCoroutine(waitForPopupToPlay(ChangeMarkPopup8));
            else if (SettingPopup16.activeSelf)
                StartCoroutine(waitForPopupToPlay(SettingPopup16));
            else if (MailboxPopup18.activeSelf)
                StartCoroutine(waitForPopupToPlay(MailboxPopup18));
            else if (NoticePopup19.activeSelf)
                StartCoroutine(waitForPopupToPlay(NoticePopup19));
            else if (GoldPopup_01_21.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_01_21));
            else if (GoldPopup_02_22.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_02_22));
            else if (GoldPopup_03_23.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_03_23));
            else if (GoldPopup_04_24.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_04_24));
            else if (GoldPopup_05_25.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_05_25));
            else if (GoldPopup_06_26.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_06_26));
            else if (GoldPopup_07_27.activeSelf)
                StartCoroutine(waitForPopupToPlay(GoldPopup_07_27));
            else if (GemPopup_01_28.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_01_28));
            else if (GemPopup_02_29.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_02_29));
            else if (GemPopup_03_30.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_03_30));
            else if (GemPopup_04_31.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_04_31));
            else if (GemPopup_05_32.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_05_32));
            else if (GemPopup_06_33.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_06_33));
            else if (GemPopup_07_34.activeSelf)
                StartCoroutine(waitForPopupToPlay(GemPopup_07_34));
            else if (MoneyPopup_01_35.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_01_35));
            else if (MoneyPopup_02_36.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_02_36));
            else if (MoneyPopup_03_37.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_03_37));
            else if (MoneyPopup_04_38.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_04_38));
            else if (MoneyPopup_05_39.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_05_39));
            else if (MoneyPopup_06_40.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_06_40));
            else if (MoneyPopup_07_41.activeSelf)
                StartCoroutine(waitForPopupToPlay(MoneyPopup_07_41));
            else if (PackagePopup_01_42.activeSelf)
                StartCoroutine(waitForPopupToPlay(PackagePopup_01_42));
            else if (PackagePopup_02_43.activeSelf)
                StartCoroutine(waitForPopupToPlay(PackagePopup_02_43)); 
            else if (PackagePopup_03_44.activeSelf)
                StartCoroutine(waitForPopupToPlay(PackagePopup_03_44));
            else if (InfoPopup_46.activeSelf)
                StartCoroutine(waitForPopupToPlay(InfoPopup_46)); 
            else if (InfoPopup_02_47.activeSelf)
                StartCoroutine(waitForPopupToPlay(InfoPopup_02_47)); 
            else if (UpdatePopup_48.activeSelf)
                StartCoroutine(waitForPopupToPlay(UpdatePopup_48)); 
            else if (PassInfoInfoPopup_49.activeSelf)
                StartCoroutine(waitForPopupToPlay(PassInfoInfoPopup_49)); 
            else if (ReceiveAllPopup_50.activeSelf)
                StartCoroutine(waitForPopupToPlay(ReceiveAllPopup_50)); 
            else if (ProfileFramePopup_53.activeSelf)
                StartCoroutine(waitForPopupToPlay(ProfileFramePopup_53)); 
            else if (ReceiveAllPopup_02_59.activeSelf)
                StartCoroutine(waitForPopupToPlay(ReceiveAllPopup_02_59)); 
            else if (GameWinPopup_60.activeSelf)
                StartCoroutine(waitForPopupToPlay(GameWinPopup_60));
            else if (GameOverPopup_61.activeSelf)
                StartCoroutine(waitForPopupToPlay(GameOverPopup_61));

        }

        public void BackOnePopup(GameObject screen)
        {
            StartCoroutine(waitForPopupToPlay(screen));
        }

        IEnumerator waitForPopupToPlay(GameObject screen)
        {
            print("runned");
            yield return new WaitForSeconds(0.15f);
            
            screen.SetActive(false);

        }
    }
}
