using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMechanics : MonoBehaviour
{
    public GameObject clickOBject, clickCollider;
    public Image clickCooldown, clockCooldownMobile;
    public static bool isClickCooldown;

    private Camera mainCamera;

    public GameObject blockObject;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (MobileScript.isMobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began ||
                    touch.phase == TouchPhase.Moved ||
                    touch.phase == TouchPhase.Stationary)
                {
                    // Finger is touching the screen — enable blockObject
                    blockObject.SetActive(true);
                }
                else if (touch.phase == TouchPhase.Ended ||
                         touch.phase == TouchPhase.Canceled)
                {
                    // Finger lifted or touch interrupted — disable blockObject
                    blockObject.SetActive(false);
                }
            }
            else
            {
                // No touches — make sure it's inactive
                blockObject.SetActive(false);
            }
        }


        Vector3 worldPosition = Vector3.zero;

        if (MobileScript.isMobile == false)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Convert the screen position to world position
            worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
               mouseScreenPosition.x,
               mouseScreenPosition.y,
               mainCamera.nearClipPlane // Or a fixed distance from the camera
           ));
        }
        else
        {
            // Mobile input using touch
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchScreenPosition = touch.position;
                worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
                    touchScreenPosition.x,
                    touchScreenPosition.y,
                    mainCamera.nearClipPlane + 10f
                ));
            }
            else
            {
                return; // No touch input detected
            }
        }

        worldPosition.z = 0;

        clickOBject.transform.position = worldPosition;

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
        if(MobileScript.isMobile == true) { clockCooldownMobile.gameObject.SetActive(true); clockCooldownMobile.fillAmount = 1f; }
        else { clickCooldown.gameObject.SetActive(true); clickCooldown.fillAmount = 1f; }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            if (MobileScript.isMobile == true) { clockCooldownMobile.fillAmount = 1f - (elapsedTime / duration); }
            else { clickCooldown.fillAmount = 1f - (elapsedTime / duration); }
           
            yield return null; 
        }

        clockCooldownMobile.fillAmount = 0;
        clockCooldownMobile.gameObject.SetActive(false);

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
        clockCooldownMobile.gameObject.SetActive(false);
        isClickCooldown = false;
    }
}
