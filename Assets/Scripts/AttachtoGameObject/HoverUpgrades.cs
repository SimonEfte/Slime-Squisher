using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HoverUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverUpgradeObject;
    public GameObject upgradeName, upgradeDesc;
    public TextMeshProUGUI upgradeNameText, upgradeDescText;

    private void Awake()
    {
        hoverUpgradeObject = GameObject.Find("HoverIconUpgrade");
        upgradeName = GameObject.Find("UpgradeNameText");
        upgradeDesc = GameObject.Find("UpgradeDescriptionText");

        upgradeNameText = upgradeName.GetComponent<TextMeshProUGUI>();
        upgradeDescText = upgradeDesc.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverUpgradeObject.transform.localPosition = gameObject.transform.localPosition;
        hoverUpgradeObject.SetActive(true);

        if (gameObject.name == "Upgrade_Cooldown")
        {
            upgradeNameText.text = "CLICK COOLDOWN";
            upgradeDescText.text = "Decreases the click cooldown";
        }
        if (gameObject.name == "Upgrade_ClickDamage")
        {
            upgradeNameText.text = "CLICK DAMAGE";
            upgradeDescText.text = "Increases the click damage";
        }
        if (gameObject.name == "Upgrade_CursorSlash")
        {
            upgradeNameText.text = "CURSOR SLASH";
            upgradeDescText.text = "Move the cursor over a slime to slash. Each slash is equal to 20% of the cursor click damage";
        }
        if (gameObject.name == "Upgrade_CritDamage")
        {
            upgradeNameText.text = "CRIT DAMAGE";
            upgradeDescText.text = "10% chance to deal 2X damage";
        }
        if (gameObject.name == "Upgrade_HealthUpgrade")
        {
            upgradeNameText.text = "STRAWBERRY HEALTH";
            upgradeDescText.text = "Increases the health of the strawberry. ";
        }
        if (gameObject.name == "Upgrade_PaperShot")
        {
            upgradeNameText.text = "PAPER SHOT";
            upgradeDescText.text = "Every slime click has a chance to shoot a damage dealing paper at a random slime";
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
