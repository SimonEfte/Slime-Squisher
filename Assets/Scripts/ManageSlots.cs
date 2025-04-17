using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManageSlots : MonoBehaviour
{
    public GameObject[] upgradeIcons, iconsParent;
    public GameObject[] slots;
    public GameObject[] slotLevelText;

    public static int slotsAviable;
    public static int upgradeSlotsTaken;

    public Achivements achScript;

    private void Start()
    {
        StartCoroutine(SetsSlots());
    }

    public void SetSlotsPressPlay()
    {
        slotsAviable = 4 + MetaProgressionUpgrades.slotIncrease;
        for (int i = 0; i < slotsAviable; i++)
        {
            slots[i].SetActive(true);
        }
    }

    IEnumerator SetsSlots()
    {
        yield return new WaitForSeconds(0.1f);
        slotsAviable = 4 + MetaProgressionUpgrades.slotIncrease;

        yield return new WaitForSeconds(0.5f);
        SetSlotsPressPlay();
    }

    public void SetSlotAlpha(int upgradeNumber, float alpha)
    {
        Image image = slots[upgradeNumber].GetComponent<Image>();

        Color color = image.color;
        color.a = alpha; 
        image.color = color;
    }

    public void SetIconAndText(int upgradeNumber)
    {
        upgradeIcons[upgradeNumber].transform.SetParent(iconsParent[upgradeSlotsTaken].transform);
        upgradeIcons[upgradeNumber].SetActive(true);
        upgradeIcons[upgradeNumber].transform.localPosition = new Vector3(0,0, 0);
        upgradeIcons[upgradeNumber].transform.localRotation = Quaternion.Euler(0, 0, 0);
        slotLevelText[upgradeSlotsTaken].SetActive(true);

        if(upgradeSlotsTaken == 3) { achScript.TriggerACH("slot4"); Achivements.achievedFourClickUpgrades = true; }
        if (upgradeSlotsTaken == 4) { achScript.TriggerACH("slot5"); Achivements.achievedFiveClickUpgrades = true; }
        if (upgradeSlotsTaken == 5) { achScript.TriggerACH("slot6"); Achivements.achievedSixClickUpgrades = true; }
        if (upgradeSlotsTaken == 6) { achScript.TriggerACH("slot7"); Achivements.achievedSevenClickUpgrades = true; }
        if (upgradeSlotsTaken == 7) { achScript.TriggerACH("slot8"); Achivements.achievedEightClickUpgrades = true; }
    }

    public void SetLevelText(int upgradeNumber, int level)
    {
        TextMeshProUGUI levelText = slotLevelText[upgradeNumber].GetComponent<TextMeshProUGUI>();
        levelText.text = $"{LocalizationSCRIPT.lvl} {level}";
    }

    public void ResetSlots()
    {
        for (int i = 0; i < 8; i++)
        {
            SetSlotAlpha(i, 0.8f);
            slotLevelText[i].SetActive(false);
            upgradeIcons[i].SetActive(false);
        }

        for (int i = 0; i < upgradeIcons.Length; i++)
        {
            upgradeIcons[i].SetActive(false);
        }
    }

    public static int[] storeSlotLevel = new int[8];

    public void ChangeAllTexts()
    {
        if(upgradeSlotsTaken <= 0)
        {
            return;
        } 

        for (int i = 0; i < upgradeSlotsTaken; i++)
        {
            slotLevelText[i].GetComponent<TextMeshProUGUI>().text = LocalizationSCRIPT.lvl + " " +  storeSlotLevel[i];
        }
    }

}
