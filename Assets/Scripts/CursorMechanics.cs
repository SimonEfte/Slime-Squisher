using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorMechanics : MonoBehaviour
{
    public static List<GameObject> shootingSlimeArray = new List<GameObject>();
    public static List<GameObject> bigSlimeArray = new List<GameObject>();

    public static List<GameObject> slimesArray = new List<GameObject>();
    public static List<GameObject> slimeTargetObjectArray = new List<GameObject>();
    public Transform middleObject;

    public static Vector2 bouncyBallTarget;

    public AudioManager audioManager;

    public OverlappingSounds overlappingScript;

    public static Vector2 friendlyBulletTarget;

    #region add slime array
    public static void AddSlime(GameObject slime)
    {
        if (!slimesArray.Contains(slime))
        {
            slimesArray.Add(slime);
        }
    }
    #endregion

    #region add target object array
    public static void AddSlimeTargetObject(GameObject slime)
    {
        if (!slimeTargetObjectArray.Contains(slime))
        {
            slimeTargetObjectArray.Add(slime);
        }
    }
    #endregion

    #region add shooting array
    public static void AddShootingSlime(GameObject slime)
    {
        if (!shootingSlimeArray.Contains(slime))
        {
            shootingSlimeArray.Add(slime);
        }
    }
    #endregion

    #region add big array
    public static void AddBigSlime(GameObject slime)
    {
        if (!bigSlimeArray.Contains(slime))
        {
            bigSlimeArray.Add(slime);
        }
    }
    #endregion


    //Check random slimes
    #region Check random slime
    public static int totalSlimesActive;

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

        totalSlimesActive = activeSlimes.Count;

        if (activeSlimes.Count > 0)
        {
            int randomIndex = Random.Range(0, activeSlimes.Count);
            GameObject selectedSlime = activeSlimes[randomIndex];
          
            if (projectileType == 2)
            {
                selectedSlime = activeSlimes.OrderBy(slime => Vector3.Distance(slime.transform.position, middleObject.position)).FirstOrDefault();
                float slimeDistance = Vector3.Distance(selectedSlime.transform.position, middleObject.position);

                float maxAllowedDistance = 2.4f;

                if (slimeDistance <= maxAllowedDistance)
                {
                    KnifeStab.isAnySlimesClose = true;
                    knifeHitPos = selectedSlime.transform.position;
                }
                else
                {
                    KnifeStab.isAnySlimesClose = false;
                }
            }
            else if (projectileType == 3)
            {
                arrowHitPos = selectedSlime.transform.localPosition;
                StartCoroutine(ArrowRain(arrowHitPos));
            }
            else if (projectileType == 4)
            {
                Vector2 startPos = SlimeMechanics.boulderStartPos;

                selectedSlime = activeSlimes.OrderByDescending(slime => Vector3.Distance(slime.transform.position, startPos)).First();

                if(activeSlimes.Count < 2) 
                {
                    int random = Random.Range(-900, 900);
                    boulderTargetPosition = new Vector2(random, random);
                }
                else
                {
                    boulderTargetPosition = selectedSlime.transform.position;
                }

                ShootBoulder();
            }
            else if (projectileType == 5)
            {
                swordHitPos = selectedSlime.transform.localPosition;
            }
            else if (projectileType == 6)
            {
                meteorTargetPos = selectedSlime.transform.localPosition;
                SpawnMeteor(meteorTargetPos);
            }
        }
        else
        {
            //Debug.Log("No active slimes found.");
        }
    }

    public int GetSlimeIndex(GameObject slime)
    {
        if (slime == null)
        {
            Debug.LogError("Slime is null. Cannot find index.");
            return -1;
        }

        int index = slimesArray.IndexOf(slime);
        if (index == -1)
        {
            Debug.LogWarning("The provided slime is not in the list.");
        }
        return index;
    }
    #endregion

    #region Check random targetOBject
    public void SelectRandomTargetObject(int projectileType)
    {
        var activeSlimes = new List<GameObject>();
        foreach (var slime in slimeTargetObjectArray)
        {
            if (slime != null && slime.activeSelf)
            {
                activeSlimes.Add(slime);
            }
        }

        if (activeSlimes.Count > 0)
        {
            GameObject selectedSlime;

            if (projectileType == 2 && activeSlimes.Count > 1)
            {
                //Find the slime furthest away from middleObject
                selectedSlime = activeSlimes.OrderByDescending(slime => Vector3.Distance(slime.transform.position, middleObject.position)).First();
            }
            else
            {
                //Randomly selects if there is only 1 slime
                int randomIndex = Random.Range(0, activeSlimes.Count);
                selectedSlime = activeSlimes[randomIndex];
            }

            if (projectileType == 1)
            {
                paperClipHitPos = selectedSlime.transform.position;
            }
            else if (projectileType == 2)
            {
                poisonDartHitPos = selectedSlime.transform.position;
            }
            else if (projectileType == 3)
            {
                if (activeSlimes.Count < 1)
                {
                    int random = Random.Range(-900, 900);
                    bouncyBallTarget = new Vector2(random, random);
                }
                else
                {
                    bouncyBallTarget = selectedSlime.transform.position;
                }
            }
            if (projectileType == 4)
            {
                if(ActiveMechanics.isFrenzyInUse == false)
                {
                    kunaiHitPos = selectedSlime.transform.position;
                }
                ShootKunai(kunaiHitPos); 
            }

            if (projectileType == 5)
            {
                friendlyBulletTarget = selectedSlime.transform.position;
            }
        }
        else
        {
            int randomx = Random.Range(-900, 900);
            int randomy = Random.Range(-900, 900);
            paperClipHitPos = new Vector2(randomx, randomy);
            //Debug.Log("No active slimes found.");
        }
    }

    public int GetTargetObjextIndex(GameObject slime)
    {
        if (slime == null)
        {
            Debug.LogError("Slime is null. Cannot find index.");
            return -1;
        }

        int index = slimeTargetObjectArray.IndexOf(slime);
        if (index == -1)
        {
            Debug.LogWarning("The provided slime is not in the list.");
        }
        return index;
    }
    #endregion

    #region Check random SHOOTING slime
    private int totalShootingSlimes;

    public void SelectRandomShootingSlime(int projectileType, bool onlyGetCount)
    {
        var activeSlimes = new List<GameObject>();
        foreach (var slime in shootingSlimeArray)
        {
            if (slime != null && slime.activeSelf)
            {
                activeSlimes.Add(slime);
            }
        }

        totalShootingSlimes = activeSlimes.Count;

        if(onlyGetCount == false)
        {
            if (activeSlimes.Count > 0)
            {
                int randomIndex = Random.Range(0, activeSlimes.Count);
                GameObject selectedSlime = activeSlimes[randomIndex];

                if (projectileType == 1)
                {
                    meteorTargetPos = selectedSlime.transform.localPosition;
                    SpawnMeteor(meteorTargetPos);
                }
            }
            else
            {
                //Debug.Log("No active slimes found.");
            }
        }
    }

    public int GetShootingSlimeIndex(GameObject slime)
    {
        if (slime == null)
        {
            Debug.LogError("Slime is null. Cannot find index.");
            return -1;
        }

        int index = shootingSlimeArray.IndexOf(slime);
        if (index == -1)
        {
            Debug.LogWarning("The provided slime is not in the list.");
        }
        return index;
    }
    #endregion

    #region Check random BIG slime
    private int totalBigSlimes;
    public void SelectRandomBigSlime(int projectileType, bool onlyGetCount)
    {
        var activeSlimes = new List<GameObject>();
        foreach (var slime in bigSlimeArray)
        {
            if (slime != null && slime.activeSelf)
            {
                activeSlimes.Add(slime);
            }
        }

        totalBigSlimes = activeSlimes.Count;

        if (onlyGetCount == false)
        {
            if (activeSlimes.Count > 0)
            {
                int randomIndex = Random.Range(0, activeSlimes.Count);
                GameObject selectedSlime = activeSlimes[randomIndex];

                if (projectileType == 1)
                {
                    meteorTargetPos = selectedSlime.transform.localPosition;
                    SpawnMeteor(meteorTargetPos);
                }
            }
            else
            {
                //Debug.Log("No active slimes found.");
            }
        }
    }

    public int GetBigSlimeIndex(GameObject slime)
    {
        if (slime == null)
        {
            Debug.LogError("Slime is null. Cannot find index.");
            return -1;
        }

        int index = bigSlimeArray.IndexOf(slime);
        if (index == -1)
        {
            Debug.LogWarning("The provided slime is not in the list.");
        }
        return index;
    }
    #endregion

    #region Update
    public static bool triggerPaperClip, triggerPoisonDart;
    public static Vector2 paperClipHitPos, knifeHitPos, arrowHitPos, bouncyBallStartPos, kunaiStartPos, kunaiHitPos;

    private void Update()
    {
        if (triggerPaperClip == true)
        {
            SelectRandomTargetObject(1);
            ShootPaperClip(SlimeMechanics.slimeSquishedPos);
            triggerPaperClip = false;
        }

        if (SlimeMechanics.triggerArrowRain == true)
        {
            ChooseArrows();
            SlimeMechanics.triggerArrowRain = false;
        }

        if (SlimeMechanics.triggerScythe == true)
        {
            ShootScynthe();
            SlimeMechanics.triggerScythe = false;
        }

        if (SlimeMechanics.triggerSword == true)
        {
            if(swordSlashCoroutine == null) { swordSlashCoroutine =  StartCoroutine(SwordSlash()); }
            if(swordOffCoroutine == null) { swordOffCoroutine = StartCoroutine(SetSwordOnAnOff()); }
           
            SlimeMechanics.triggerSword = false;
        }

        if (SlimeMechanics.triggerBoulder == true)
        {
            SelectRandomActiveSlime(4);
            SlimeMechanics.triggerBoulder = false;
        }

        if (SlimeMechanics.triggerMeteor == true)
        {
            int random = Random.Range(1, 3);
            SelectRandomShootingSlime(1, true);
            SelectRandomBigSlime(1, true);

            if (totalShootingSlimes > 0 && totalBigSlimes > 0)
            {
                if (random == 1) { SelectRandomShootingSlime(1, false); }
                else { SelectRandomBigSlime(1, false); }
            }
            else if (totalShootingSlimes > 0 && totalBigSlimes == 0)
            {
                SelectRandomShootingSlime(1, false);
            }
            else if (totalShootingSlimes == 0 && totalBigSlimes > 0)
            {
                SelectRandomBigSlime(1, false);
            }
            else
            {
                SelectRandomActiveSlime(6);
            }
            SlimeMechanics.triggerMeteor = false;
        }

        if (SlimeMechanics.triggerKunai == true)
        {
            SelectRandomTargetObject(4);
            SelectRandomTargetObject(4);

            SlimeMechanics.triggerKunai = false;
        }

        if(SlimeMechanics.triggerSawblade == true)
        {
            Vector2 pos = new Vector2(0,0);
            ShootSawBlades(pos, true);
            SlimeMechanics.triggerSawblade = false;
        }

        if (SlimeMechanics.triggerKatana == true)
        {
            ShootKatana();
            SlimeMechanics.triggerKatana = false;
        }

        if (SlimeMechanics.triggerLog == true)
        {
            SlimeMechanics.triggerLog = false;
        }
    }
    #endregion

    IEnumerator RandomTarget()
    {
        yield return new WaitForSeconds(0.6f);
    }

    //Slime kill chance
    #region Shoot paper clip
    public void ShootPaperClip(Vector2 pos)
    {
        GameObject paperClip = ObjectPool.instance.GetPaperClipFromPool();
        GameObject shadow = ObjectPool.instance.GetShadowFromPool();
        shadow.transform.localScale = new Vector2(0.6f, 0.6f);

        ShootPaperClipDirection(paperClip, false, pos); ShootPaperClipDirection(shadow, true, pos);
    }

    public void ShootPaperClipDirection(GameObject objecToShoot, bool isShadow, Vector2 pos)
    {
        objecToShoot.transform.position = pos;

        Vector2 direction = (paperClipHitPos - (Vector2)objecToShoot.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(isShadow == false) { objecToShoot.transform.rotation = Quaternion.Euler(0, 0, angle); }
        else
        {
            objecToShoot.transform.position = new Vector2(pos.x, pos.y - 0.19f);
        }

        Rigidbody2D rb = objecToShoot.GetComponent<Rigidbody2D>();
        float speed = 11f;
        rb.velocity = direction * speed;
    }
    #endregion

    #region Shoot kunai
    public void ShootKunai(Vector2 hitPos)
    {
        GameObject kunai = ObjectPool.instance.GetKunaiFromPool();
        GameObject shadow = ObjectPool.instance.GetShadowFromPool();
        shadow.transform.localScale = new Vector2(0.6f, 0.6f);

        ShootKunaiDirection(kunai, false, hitPos); ShootKunaiDirection(shadow, true, hitPos);
    }

    public void ShootKunaiDirection(GameObject objecToShoot, bool isShadow, Vector2 hitPos)
    {
        objecToShoot.transform.position = kunaiStartPos;

        Vector2 direction = (hitPos - (Vector2)objecToShoot.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (isShadow == false) { objecToShoot.transform.rotation = Quaternion.Euler(0, 0, angle); }
        else
        {
            objecToShoot.transform.position = new Vector2(kunaiStartPos.x, kunaiStartPos.y - 0.19f);
        }

        Rigidbody2D rb = objecToShoot.GetComponent<Rigidbody2D>();
        float speed = 10f;
        rb.velocity = direction * speed;
    }
    #endregion

    #region Shoot poison dart 
    public Vector2 poisonDartHitPos;

    //Should target the enemy that is furthers away
    public void ShootPoisonDart(Vector2 pos)
    {
        SelectRandomTargetObject(2);

        GameObject poisonDart = ObjectPool.instance.GetPoisonDartFromPool();
        GameObject shadow = ObjectPool.instance.GetShadowFromPool();
        shadow.transform.localScale = new Vector2(0.55f, 0.55f);

        ShootPosionDartDirection(poisonDart, false, pos);
        ShootPosionDartDirection(shadow, true, pos);
    }

    public void ShootPosionDartDirection(GameObject objecToShoot, bool isShadow, Vector2 pos)
    {
        objecToShoot.transform.position = pos;

        Vector2 direction = (poisonDartHitPos - (Vector2)objecToShoot.transform.position).normalized;

        if (isShadow == true)
        {
            objecToShoot.transform.position = new Vector2(pos.x, pos.y - 0.2f);
        }
        else
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            objecToShoot.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        Rigidbody2D rb = objecToShoot.GetComponent<Rigidbody2D>();
        float speed = 9.5f;
        rb.velocity = direction * speed;
    }
    #endregion

    #region shoot thorn
    public void ShootThorn(Vector2 pos, bool shoot4)
    {
        int thornAmount = 4;

        if(shoot4 == false)
        {
            thornAmount = 1;
        }

        for (int i = 0; i < thornAmount; i++)
        {
            GameObject thorn = ObjectPool.instance.GetThornFromPool();

            if (ActiveMechanics.isFrenzyInUse) { thorn.transform.position = pos; }
            else { thorn.transform.localPosition = pos; }
        
            GameObject shadow = ObjectPool.instance.GetShadowFromPool();

            if (ActiveMechanics.isFrenzyInUse) { shadow.transform.position = new Vector2(pos.x, pos.y - 0.15f); }
            else { shadow.transform.localPosition = new Vector2(pos.x, pos.y - 15); }

            shadow.transform.localScale = new Vector2(0.4f, 0.4f);

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            thorn.transform.rotation = Quaternion.Euler(0, 0, angle);

            thorn.GetComponent<Rigidbody2D>().velocity = direction * 9f;
            shadow.GetComponent<Rigidbody2D>().velocity = direction * 9f;
        }
    }
    #endregion

    //Slime click chance
    #region Arrow rain
    public void ChooseArrows()
    {
        int random = Random.Range(4, 8);
        for (int i = 0; i < random; i++)
        {
            SelectRandomActiveSlime(3);
        }
    }

    public Transform arrowOriginalParent, slimeCanvas;

    IEnumerator ArrowRain(Vector2 slimePos)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));

        if(PickUpgrade.isInChooseUpgrade == true) { yield return null; }

        if(totalSlimesActive < 1)
        {
            yield return null;
        }

        Vector2 slimeHitPos = new Vector2(slimePos.x, slimePos.y);

        GameObject arrow = ObjectPool.instance.GetArrowFromPool();

        Collider2D collider = arrow.GetComponent<Collider2D>();
        collider.enabled = false;

        arrow.transform.SetParent(slimeCanvas);

        Vector2 startPos = new Vector2(slimePos.x - 1100, slimePos.y + 1100);
        arrow.transform.localPosition = startPos;

        float speed = 1850f; 

        while ((Vector2)arrow.transform.localPosition != slimeHitPos)
        {
            arrow.transform.localPosition = Vector2.MoveTowards(
                arrow.transform.localPosition,
                slimeHitPos,
                speed * Time.deltaTime
            );

            yield return null;
        }

        collider.enabled = true;

        //overlappingScript.PlaySound(3);

        yield return new WaitForSeconds(0.05f);
        arrow.transform.SetParent(arrowOriginalParent);

        ObjectPool.instance.ReturnArrowFrompool(arrow);
    }
    #endregion

    #region Shoot scynthe
    public void ShootScynthe()
    {
        GameObject scynthe = ObjectPool.instance.GetScyntheFromPool();
        scynthe.transform.Rotate(0, 0, 0);
        scynthe.transform.position = SlimeMechanics.scytheStartPos;

        overlappingScript.PlaySound(4, 0, false);

        int random = Random.Range(-900, 900);
        Vector2 randomPos = new Vector2(random, random);

        Vector2 direction = (randomPos - SlimeMechanics.scytheStartPos).normalized;

        Rigidbody2D rb = scynthe.GetComponent<Rigidbody2D>();
        float speed = 0.75f;
        rb.velocity = direction * speed;
    }
    #endregion

    #region Sword slashes
    public Coroutine swordOffCoroutine, swordSlashCoroutine;
    public Vector2 swordHitPos;

    public void StopSword()
    {
        if (PickUpgrade.choseSword == true)
        {
            if (swordSlashCoroutine != null) 
            {
                StopCoroutine(swordSlashCoroutine);
                swordOffCoroutine = null;
                swordSlashCoroutine = null;
            }
        }
    }

    IEnumerator SetSwordOnAnOff()
    {
        yield return new WaitForSeconds(2.5f);

        if(PickUpgrade.choseSword == true)
        {
            if(swordSlashCoroutine != null) { StopCoroutine(swordSlashCoroutine); }
           
        }

        swordOffCoroutine = null;
        swordSlashCoroutine = null;
    }

    IEnumerator SwordSlash()
    {
        float toggleInterval = .41f; 
        float nextToggleTime = Time.time;

        while (true)
        {
            while (Time.time < nextToggleTime)
            {
                yield return null; 
            }

            SelectRandomActiveSlime(5);
            if(totalSlimesActive > 0)
            {
                StartCoroutine(SlashTheSword(swordHitPos));
            }

            nextToggleTime += toggleInterval;
        }
    }

    //The trail renderer also moves when the cursor moves.

    IEnumerator SlashTheSword(Vector2 pos)
    {
        GameObject sword = ObjectPool.instance.GetSwordFromPool();

        audioManager.Play("SwordSlash");

        Vector2 offset = Vector2.zero;

        int randomQuadrant = Random.Range(1, 5);

        switch (randomQuadrant)
        {
            case 1: // Top Right
                offset = new Vector2(120, 30);
                sword.transform.localRotation = Quaternion.Euler(0, 0, 25);
                break;
            case 2: // Top Left
                offset = new Vector2(-120, 30);
                sword.transform.localRotation = Quaternion.Euler(0, 0, -25);
                break;
            case 3: // Bottom Left
                offset = new Vector2(-120, -120);
                sword.transform.localRotation = Quaternion.Euler(0, 0, 25);
                break;
            case 4: // Bottom Right
                offset = new Vector2(120, -120);
                sword.transform.localRotation = Quaternion.Euler(0, 0, -25);
                break;
        }

        sword.transform.localPosition = pos + offset;
        Vector2 startpos = sword.transform.localPosition;

        // Determine movement direction based on spawn quadrant
        Vector2 targetDirection = Vector2.zero;
        switch (randomQuadrant)
        {
            case 1: // Top Right moves to Bottom Left
                offset = new Vector2(-120, -120);
                targetDirection = pos + offset;
                break;
            case 2: // Top Left moves to Bottom Right
                offset = new Vector2(120, -120);
                targetDirection = pos + offset;
                break;
            case 3: // Bottom Left moves to Top Right
                offset = new Vector2(120, 30);
                targetDirection = pos + offset;
                break;
            case 4: // Bottom Right moves to Top Left
                offset = new Vector2(-120, 30);
                targetDirection = pos + offset;
                break;
        }

        float rotationSpeed = 0;
        if(randomQuadrant == 1) { rotationSpeed = Random.Range(140, 160); }
        if (randomQuadrant == 2) { rotationSpeed = Random.Range(-140, -160); }
        if (randomQuadrant == 3) { rotationSpeed = Random.Range(-140, -160); }
        if (randomQuadrant == 4) { rotationSpeed = Random.Range(140, 160); }

        float elapsedTime = 0;
        float time = 0.37f; // Duration of the slash

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / time);
            sword.transform.localPosition = Vector3.Lerp(startpos, targetDirection, t);

            // Move the sword in the direction of the target
            //sword.transform.localPosition += (Vector3)(targetDirection * moveSpeed * Time.deltaTime);

            //Rotate the sword
            sword.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        ObjectPool.instance.ReturnSwordToPool(sword);
    }
    #endregion

    #region Shoot boulder
    public Vector2 boulderTargetPosition;

    public void ShootBoulder()
    {
        audioManager.Play("boulder1");

        GameObject boulder = ObjectPool.instance.GetBoulderFromPool();
        boulder.transform.position = SlimeMechanics.boulderStartPos;

        Vector2 direction = (boulderTargetPosition - SlimeMechanics.boulderStartPos).normalized;

        Rigidbody2D rb = boulder.GetComponent<Rigidbody2D>();
        float speed = 3.7f;
        rb.velocity = direction * speed;
    }
    #endregion

    #region spawn meteor
    public Vector2 meteorTargetPos;

    public void SpawnMeteor(Vector2 pos)
    {
        GameObject meteor = ObjectPool.instance.GetMeteorFromPool();
        meteor.transform.localPosition = pos;
    }
    #endregion

    #region Shoot saw blades
    public void ShootSawBlades(Vector2 pos, bool randomSpawn)
    {
        float randomSpeed = Random.Range(5, 7);

        if (randomSpawn == true)
        {
            int random = Random.Range(1, 4);

            for (int i = 0; i < random; i++)
            {
                GameObject sawBlade = ObjectPool.instance.GetSawbladeFromPool();

                int spawnXposPluss = 1050, spawnXposMinus = -1050;
                int spawnYposPluss = 600, spawnYposMinus = -600;

                int randomPos = Random.Range(1, 5);
                int randomX = Random.Range(spawnXposMinus, spawnXposPluss);
                int randomY = Random.Range(spawnYposMinus, spawnYposPluss);

                Vector2 spawnPosition = Vector2.zero;
                Vector2 moveDirection = Vector2.zero;

                if (randomPos == 1) // Spawn on top
                {
                    spawnPosition = new Vector2(randomX, spawnYposPluss);
                    moveDirection = new Vector2(Random.Range(-1f, 1f), -1f); // Move slightly left/right and down
                }
                else if (randomPos == 2) // Spawn on bottom
                {
                    spawnPosition = new Vector2(randomX, spawnYposMinus);
                    moveDirection = new Vector2(Random.Range(-1f, 1f), 1f); // Move slightly left/right and up
                }
                else if (randomPos == 3) // Spawn on right
                {
                    spawnPosition = new Vector2(spawnXposPluss, randomY);
                    moveDirection = new Vector2(-1f, Random.Range(-1f, 1f)); // Move left with slight up/down variation
                }
                else if (randomPos == 4) // Spawn on left
                {
                    spawnPosition = new Vector2(spawnXposMinus, randomY);
                    moveDirection = new Vector2(1f, Random.Range(-1f, 1f)); // Move right with slight up/down variation
                }

                sawBlade.transform.localPosition = spawnPosition;

                Rigidbody2D rb = sawBlade.GetComponent<Rigidbody2D>();
                rb.velocity = moveDirection.normalized * randomSpeed;
            }
        }
        else
        {
            GameObject sawBlade = ObjectPool.instance.GetSawbladeFromPool();
            sawBlade.transform.localPosition = pos;

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            Rigidbody2D rb = sawBlade.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * randomSpeed;
        }
    }
    #endregion

    #region Shoot katana
    public GameObject cursorObject;
    public float spinDuration;
    public float shootSpeed = 10f;

    public void ShootKatana()
    {
        audioManager.Play("Katana");

        spinDuration = 0.35f;

        GameObject katana = ObjectPool.instance.GetKatanaFromPool();
        katana.transform.position = gameObject.transform.position;

        // Set a random initial Z rotation
        float randomZRotation = Random.Range(0f, 360f);
        katana.transform.rotation = Quaternion.Euler(0, 0, randomZRotation);

        // Start the spinning coroutine
        StartCoroutine(SpinAndShoot(katana, randomZRotation));
    }

    private IEnumerator SpinAndShoot(GameObject katana, float startRotation)
    {
        float elapsedTime = 0f;
        float targetRotation = startRotation + 360f; // One full spin

        while (elapsedTime < spinDuration)
        {
            float zRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / spinDuration);
            katana.transform.rotation = Quaternion.Euler(0, 0, zRotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it completes exactly at 360 degrees
        katana.transform.rotation = Quaternion.Euler(0, 0, targetRotation);

        // Calculate the direction to shoot
        Vector2 shootDirection = katana.transform.right; // Right direction of the final rotation
        katana.GetComponent<Rigidbody2D>().velocity = shootDirection * shootSpeed;
    }
    #endregion

    #region shoot log
    public void ShootLog(Vector2 pos)
    {
        GameObject log = ObjectPool.instance.GetLogFromPool();

        log.transform.position = pos;

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        log.transform.rotation = Quaternion.Euler(0, 0, angle);

        log.GetComponent<Rigidbody2D>().velocity = direction * 5.3f;
    }
    #endregion
}
