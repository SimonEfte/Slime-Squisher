using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMechanics : MonoBehaviour
{
    public GameObject clickOBject, clickCollider;
    public Image clickCooldown;
    public static bool isClickCooldown;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 mouseWorldPosition;

        // Convert the screen position to world position
         mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            mouseScreenPosition.x,
            mouseScreenPosition.y,
            mainCamera.nearClipPlane // Or a fixed distance from the camera
        ));

        mouseWorldPosition.z = 0;

        clickOBject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mouseWorldPosition.z);

        if (Input.GetMouseButtonDown(0))
        {
            if(PickUpgrade.isInChooseUpgrade == false && MainMenu.isInMainMenu == false && MainMenu.isPaused == false)
            {
                if (cursorCooldown == null)
                {
                    cursorCooldown = StartCoroutine(CursorCooldown());
                    StartCoroutine(SetColliderOff());
                }
            }
            if(MainMenu.isInTut == true)
            {
                if (cursorCooldown == null)
                {
                    cursorCooldown = StartCoroutine(CursorCooldown());
                    StartCoroutine(SetColliderOff());
                }
            }
        }

        if(MainMenu.isInTut == false && cursorCooldown != null && SpawnSlimes.isPlayingRun == false)
        {
            ResetClick();
        }
    }

    IEnumerator SetColliderOff()
    {
        yield return new WaitForSeconds(0.08f);
        clickCollider.SetActive(false);
    }

    public Coroutine cursorCooldown;
    public Texture2D clickCursor, clickCursorRed;

    IEnumerator CursorCooldown()
    {
        isClickCooldown = true;
        Cursor.SetCursor(clickCursorRed, Vector2.zero, CursorMode.Auto);

        float duration = 0;

        if (ActiveMechanics.punchyClicksIsUsed == true) { duration = ActiveMechanics.sharpClicksTimeInterval; }
        else { duration = PickUpgrade.clickCooldown; }
      
        float elapsedTime = 0f;

        clickCollider.SetActive(true);
        clickCooldown.gameObject.SetActive(true);

        clickCooldown.fillAmount = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; 
            clickCooldown.fillAmount = 1f - (elapsedTime / duration);
            yield return null; 
        }

        clickCooldown.fillAmount = 0;
        clickCooldown.gameObject.SetActive(false);
        cursorCooldown = null;

        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
        isClickCooldown = false;
    }

    public void ResetClick()
    {
        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
        if(cursorCooldown != null) { StopCoroutine(cursorCooldown); }
        cursorCooldown = null;
        PickUpgrade.isInChooseUpgrade = false;
        clickCooldown.gameObject.SetActive(false);
        isClickCooldown = false;
    }
}
