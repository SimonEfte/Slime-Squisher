using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMechanics : MonoBehaviour
{
    public GameObject clickOBject, clickCollider;
    public Image clickCooldown;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the screen position to world position
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(
            mouseScreenPosition.x,
            mouseScreenPosition.y,
            mainCamera.nearClipPlane // Or a fixed distance from the camera
        ));

        clickOBject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            if(cursorCooldown == null)
            {
                cursorCooldown = StartCoroutine(CursorCooldown());
                StartCoroutine(SetColliderOff());
            }
        }
    }

    IEnumerator SetColliderOff()
    {
        yield return new WaitForSeconds(0.07f);
        clickCollider.SetActive(false);
    }

    public Coroutine cursorCooldown;

    IEnumerator CursorCooldown()
    {
        float duration = PickUpgrade.clickCooldown;
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
    }
}
