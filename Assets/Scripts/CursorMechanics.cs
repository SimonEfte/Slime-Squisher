using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMechanics : MonoBehaviour
{
    public static List<GameObject> slimesArray = new List<GameObject>();

    public static void AddSlime(GameObject slime)
    {
        if (!slimesArray.Contains(slime))
        {
            slimesArray.Add(slime);
        }
    }

    #region Check random slime

    public void SelectRandomActiveSlime(int projectileType)
    {
        var activeSlimes = new List<GameObject>();
        foreach (var slime in slimesArray)
        {
            if (slime != null && slime.activeSelf)
            {
                activeSlimes.Add(slime);
            }
        }

        if (activeSlimes.Count > 0)
        {
            int randomIndex = Random.Range(0, activeSlimes.Count);
            GameObject selectedSlime = activeSlimes[randomIndex];

            if(projectileType == 1)
            {
                //Debug.Log($"Selected Slime: {selectedSlime.name}");
                paperClipHitPos = selectedSlime.transform.position;
            }
        }
        else
        {
            if (projectileType == 1)
            {
                int random = Random.Range(-900, 900);
                paperClipHitPos = new Vector2(random, random);
            }

            //Debug.Log("No active slimes found.");
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
           
        }
    }

    public static bool triggerPaperClip;
    public Vector2 paperClipHitPos;

    private void Update()
    {
        if (triggerPaperClip == true)
        {
            int random = Random.Range(1,2);
            if(random == 1)
            { 
                SelectRandomActiveSlime(1);
                ShootPaperClip();
            }
            triggerPaperClip = false;
        }
    }

    public void ShootPaperClip()
    {
        GameObject paperClip = ObjectPool.instance.GetPaperClipFromPool();
        paperClip.transform.position = SlimeMechanics.slimeSquishedPos;

        Vector2 direction = (paperClipHitPos - (Vector2)paperClip.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        paperClip.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = paperClip.GetComponent<Rigidbody2D>();
        float speed = 11f;
        rb.velocity = direction * speed;
    }
}
