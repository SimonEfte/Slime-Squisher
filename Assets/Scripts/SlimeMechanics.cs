using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlimeMechanics : MonoBehaviour
{
    public bool isBossEASY, isBossNORMAL, isBossHARD;

    public bool isTutoritalSlime;

    public bool isRegularSlime, isFastSlime, isShootingslime, isBigSlime;

    public bool isGreenSlime_Regular, isBlueSlime_regular, isYellowSlime_regular, isRedSlime_regular, isPurpleSlime_Regular;
    public bool isGreenSlime_fast, isBlueSlime_fast, isYellowSlime_fast, isRedSlime_fast, isPurpleSlime_fast;
    public bool isGreenSlime_shooting, isBlueSlime_shooting, isYellowSlime_shooting, isRedSlime_shooting, isPurpleSlime_shooting;
    public bool isGrenSlime_big, isBlueSlime_big, isYellowSlime_big, isRedSlime_big, isPurpleSlime_big;

    public GameObject middleObject, projectileParent, decoy, hardGoo;
    public Animator animator;
    public float moveSpeed, originalMoveSpeed;
    public float slimeHealth;

    private Color whiteFlashColor = Color.white;
    private SpriteRenderer spriteRenderer;
    private Material material;

    public Collider2D slimeCollider;
    public Transform targetObject;

    public Animation slimeAnimation;

    public int slimeNumber;
    public static int slimeNumbersSpawned;

    public CursorMechanics cursorMechanicsScript;
    public GameObject cursorScriptObject;

    public Transform squishObject, textureObject, shootSpawnPos;
    public Transform shootSpawnPos2, shootSpawnPos3, shootSpawnPos4;
    private bool isSlimeDead;

    private bool staplerHit, bearTrapHit;

    public AudioManager audioManager;
    public GameObject audioGameobject;

    public OverlappingSounds overlappingSound;
    public GameObject overlappingObject;

    float split1Size, split2Size, split3Size, split4Size;

    public bool isBossDoneFastMove;

    #region Awake
    private void Awake()
    {
        split1Size = 1.2f;
        split2Size = 0.8f;
        split3Size = 0.6f;
        split4Size = 0.4f;

        audioGameobject = GameObject.Find("AudioManager");
        audioManager = audioGameobject.GetComponent<AudioManager>();

        overlappingObject = GameObject.Find("OverlappingSounds");
        overlappingSound = overlappingObject.GetComponent<OverlappingSounds>();

        projectileParent = GameObject.Find("ProjectilesParent");

        if(isBossHARD == true)
        {
            shootSpawnPos = transform.Find("SlimeTexture/BulletSpawnPos");
            shootSpawnPos2 = transform.Find("SlimeTexture/BulletSpawnPos2");
            shootSpawnPos3 = transform.Find("SlimeTexture/BulletSpawnPos3");
            shootSpawnPos4 = transform.Find("SlimeTexture/BulletSpawnPos4");
        }

        if (isShootingslime == true)
        {
            shootSpawnPos = transform.Find("SlimeTexture/BulletSpawnPos");
            if(isTutoritalSlime == false)
            {
                CursorMechanics.AddShootingSlime(gameObject);
            }
        }
        else if (isBigSlime)
        {
            if (isTutoritalSlime == false)
            {
                CursorMechanics.AddBigSlime(gameObject);
            }
        }

        textureObject = transform.Find("SlimeTexture");
        if(isTutoritalSlime == false) 
        { 
            targetObject = transform.Find("TargetObject");
            squishObject = transform.Find("SquishObject");
            animator = squishObject.gameObject.GetComponent<Animator>();
        }

        if(isBossHARD == true)
        {
            hardGoo = GameObject.Find("HardBossGoo");
        }

        if(isTutoritalSlime == false) { middleObject = GameObject.Find("Strawberry"); decoy = GameObject.Find("DecoyMoveTo"); }

        slimeCollider = GetComponent<Collider2D>();

        cursorScriptObject = GameObject.Find("ClickObjectFollowCursor");
        cursorMechanicsScript = cursorScriptObject.GetComponent<CursorMechanics>();

        if (isGreenSlime_Regular == true)
        {
            slimeNumber = slimeNumbersSpawned;
            slimeNumbersSpawned += 1;
        }

        Transform childTransform = gameObject.transform.Find("SlimeTexture");
        slimeAnimation = childTransform.GetComponent<Animation>();
        spriteRenderer = childTransform.GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }
    #endregion

    float extraSpeed;

    private void OnEnable()
    {
        isBossDoneFastMove = false;

        extraSpeed = 0;
        timesHardBossShot = 0;

        if (SelectGameMode.choseNormal == true) 
        {
            extraSpeed = Random.Range(0, 0.07f);
        }
        if (SelectGameMode.choseHard == true)
        {
            extraSpeed = Random.Range(0.04f, 0.11f);
        }

        CheckIfStaplerStuck(false);

        bearTrapHit = false;
        hitByNail = false;
        playerDied = false;
        isCollidingWithStrawberry = false;
        squishSlimeCoroutine = null;

        deathToSlime = false;
        staplerHit = false;
        isSlimeDead = false;

        material.SetFloat("_FlashAmount", 0);
        textureObject.gameObject.SetActive(true);
        if(isTutoritalSlime == false) { squishObject.gameObject.SetActive(false); }

        if(isRegularSlime == true) { slimeAnimation.Play("GreenSlimeMovement"); }
        if(isShootingslime == true) { slimeAnimation.Play("GreenSlimeMovement"); }
        if(isFastSlime == true) { slimeAnimation.Play("GreenSlimeMovement"); }
        if(isBigSlime == true) { slimeAnimation.Play("BigSlimeMovement"); }

        if(isBossEASY == true || isBossNORMAL == true || isBossHARD == true)
        {
            slimeAnimation.Play("BigSlimeMovement");
        }

        #region is regular slime
        if (isGreenSlime_Regular == true)
        {
            slimeHealth = SpawnSlimes.greenRegular_health;

            float randomSpeed = Random.Range(0.20f, 0.22f);
            if(MobileScript.isMobile == true) { randomSpeed = Random.Range(0.16f, 0.18f); }
            moveSpeed = randomSpeed;
        }
        if (isBlueSlime_regular == true)
        {
            slimeHealth = SpawnSlimes.blueRegular_health;

            float randomSpeed = Random.Range(0.21f, 0.23f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.17f, 0.18f); }
            moveSpeed = randomSpeed;
        }
        if (isYellowSlime_regular == true)
        {
            slimeHealth = SpawnSlimes.yellowRegular_health;

            float randomSpeed = Random.Range(0.22f, 0.24f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.19f, 0.2f); }
            moveSpeed = randomSpeed;
        }
        if (isRedSlime_regular == true)
        {
            slimeHealth = SpawnSlimes.redRegular_health;

            float randomSpeed = Random.Range(0.24f, 0.25f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.2f, 0.21f); }
            moveSpeed = randomSpeed;
        }
        if (isPurpleSlime_Regular == true)
        {
            slimeHealth = SpawnSlimes.purpleRegular_health;

            float randomSpeed = Random.Range(0.26f, 0.29f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.2f, 0.23f); }
            moveSpeed = randomSpeed;
        }
        #endregion

        #region is fast slime
        if (isGreenSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.greenFast_health;

            float randomSpeed = Random.Range(0.39f, 0.45f);
            moveSpeed = randomSpeed;
        }
        if (isBlueSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.blueFast_health;

            float randomSpeed = Random.Range(0.45f, 0.53f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.35f, 0.38f); }
            moveSpeed = randomSpeed;
        }
        if (isYellowSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.yellowFast_health;

            float randomSpeed = Random.Range(0.55f, 0.62f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.38f, 0.4f); }
            moveSpeed = randomSpeed;
        }
        if (isRedSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.redFast_health;

            float randomSpeed = Random.Range(0.65f, 0.71f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.4f, 0.41f); }
            moveSpeed = randomSpeed;
        }
        if (isPurpleSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.purpleFast_health;

            float randomSpeed = Random.Range(0.8f, 1f);
            if (MobileScript.isMobile == true) { randomSpeed = Random.Range(0.42f, 0.45f); }
            moveSpeed = randomSpeed;
        }
        #endregion

        #region is shooting slime 
        if (isGreenSlime_shooting == true)
        {
            slimeHealth = SpawnSlimes.greenShooting_health;

            StartCoroutine(WaitSetShootingSlimeSpeed());
            ShootEnemyBullet(SpawnSlimes.greenShooting_shotTimer, SpawnSlimes.greenShooting_shotSpeed);
        }
        if (isBlueSlime_shooting == true)
        {
            slimeHealth = SpawnSlimes.blueShooting_health;

            StartCoroutine(WaitSetShootingSlimeSpeed());
            ShootEnemyBullet(SpawnSlimes.blueShooting_shotTimer, SpawnSlimes.blueShooting_shotSpeed);
        }
        if (isYellowSlime_shooting == true)
        {
            slimeHealth = SpawnSlimes.yellowShooting_health;

            StartCoroutine(WaitSetShootingSlimeSpeed());
            ShootEnemyBullet(SpawnSlimes.yellowShooting_shotTimer, SpawnSlimes.yellowShooting_shotSpeed);
        }
        if (isRedSlime_shooting == true)
        {
            slimeHealth = SpawnSlimes.redShooting_health;

            StartCoroutine(WaitSetShootingSlimeSpeed());
            ShootEnemyBullet(SpawnSlimes.redShooting_shotTimer, SpawnSlimes.redShooting_shotSpeed);
        }
        if (isPurpleSlime_shooting == true)
        {
            slimeHealth = SpawnSlimes.purpleShooting_health;

            StartCoroutine(WaitSetShootingSlimeSpeed());
            ShootEnemyBullet(SpawnSlimes.purpleShooting_shotTimer, SpawnSlimes.purpleShooting_shotSpeed);
        }
        #endregion

        #region is big slime
        if (isGrenSlime_big == true)
        {
            slimeHealth = SpawnSlimes.greenBig_health;

            float randomSpeed = Random.Range(0.11f, 0.13f);
            moveSpeed = randomSpeed;
        }
        if (isBlueSlime_big == true)
        {
            slimeHealth = SpawnSlimes.blueBig_health;

            float randomSpeed = Random.Range(0.13f, 0.14f);
            moveSpeed = randomSpeed;
        }
        if (isYellowSlime_big == true)
        {
            slimeHealth = SpawnSlimes.yellowBig_health;

            float randomSpeed = Random.Range(0.15f, 0.16f);
            moveSpeed = randomSpeed;
        }
        if (isRedSlime_big == true)
        {
            slimeHealth = SpawnSlimes.redBig_health;

            float randomSpeed = Random.Range(0.2f, 0.23f);
            moveSpeed = randomSpeed;
        }
        if (isPurpleSlime_big == true)
        {
            slimeHealth = SpawnSlimes.purpleBig_health;

            float randomSpeed = Random.Range(0.23f, 0.25f);
            moveSpeed = randomSpeed;
        }
        #endregion

        if(isBossEASY == true)
        {
            slimeHealth = 1125;

            StartCoroutine(WaitSetShootingSlimeSpeed());
        }
        if (isBossNORMAL == true)
        {
            slimeHealth = 500;

            StartCoroutine(WaitSetShootingSlimeSpeed());
        }
        if (isBossHARD == true)
        {
            slimeHealth = 2250;

            ShootEnemyBullet(1, 1);
            StartCoroutine(WaitSetShootingSlimeSpeed());
        }

        if(isShootingslime == false && isBossEASY == false && isBossNORMAL == false && isBossHARD == false) { isBossDoneFastMove = true; }

        slimeCollider.enabled = true;

        //animator.SetBool("Slime1Move", true);

        if(isShootingslime == false)
        {
            originalMoveSpeed = moveSpeed;
        }

        SetPos();
        if(isTutoritalSlime == false) { targetObject.gameObject.SetActive(true); }

        if(isTutoritalSlime == false) { moveCoroutine = StartCoroutine(MoveTowardsTarget()); }
    }

    IEnumerator WaitSetShootingSlimeSpeed()
    {
        float xScale = 0;

        if (isBossNORMAL == true) 
        { 
            yield return new WaitForSeconds(0.09f);

            xScale = (float)gameObject.transform.localScale.x;

            if (xScale == 1.65f) { moveSpeed = 1f; }

            if (xScale == split1Size) { slimeHealth = 420; moveSpeed = 0.085f; }
            else if (xScale == split2Size) { slimeHealth = 300; moveSpeed = 0.081f; }
            else if(xScale == split3Size) { slimeHealth = 130; moveSpeed = 0.074f; }
            else if(xScale == split4Size) { slimeHealth = 95; moveSpeed = 0.066f; }
        }
        else
        {
            moveSpeed = 0.85f;
            if(isBossEASY == true) { moveSpeed = 1.1f; }
            if (isBossHARD == true) { moveSpeed = 1.15f; }
        }

        originalMoveSpeed = moveSpeed;
        yield return new WaitForSeconds(1.2f);
        if (isBossEASY == true) { yield return new WaitForSeconds(2f); }
        if (isBossNORMAL == true) { yield return new WaitForSeconds(2.3f); }
        if (isBossHARD == true) { yield return new WaitForSeconds(2.4f); }

        float randomSpeed = Random.Range(0.07f, 0.09f);
      
        if(isBossEASY == true) { moveSpeed = 0.093f; }
        else if (isBossHARD == true) { moveSpeed = 0.080f; }
        else if (isBossNORMAL == true || xScale == 1.65f) { moveSpeed = 0.094f; }
        else if (isBossEASY == false && isBossNORMAL == false && isBossHARD == false) { moveSpeed = randomSpeed; }

        isBossDoneFastMove = true;

        originalMoveSpeed = moveSpeed;
    }

    #region Set spawn position
    public void SetPos()
    {
        if(isTutoritalSlime == true) { return; }

        if(isBossEASY == true || isBossNORMAL == true || isBossHARD == true)
        {
            if(isBossEASY == true)
            {
                gameObject.transform.localPosition = new Vector2(-1200, 108);
            }
            if (isBossNORMAL == true)
            {
                gameObject.transform.localPosition = new Vector2(-1250, 200);
            }
            if (isBossHARD == true)
            {
                gameObject.transform.localPosition = new Vector2(-1300, 70);
            }
        }
        else
        {
            int spawnXposPluss = 1000; int spawnXposMinus = -1000;
            int spawnYposPluss = 570; int spawnYposMinus = -570;

            if (isBigSlime == true)
            {
                spawnXposPluss = 1080; spawnXposMinus = -1080;
                spawnYposPluss = 635; spawnYposMinus = -635;
            }

            int randomPos = Random.Range(1, 5);
            int randomX = Random.Range(spawnXposMinus, spawnXposPluss);
            int randomY = Random.Range(spawnYposPluss, spawnYposMinus);

            //Spawns on top
            if (randomPos == 1) 
            {
                if (MobileScript.isMobile == true)
                {
                    randomX = Random.Range(-630, 1000);
                    if (isBigSlime == true)
                    {
                        randomX = Random.Range(-620, 1080);
                    }
                }

                gameObject.transform.localPosition = new Vector2(randomX, spawnYposPluss); 
            }

            //Spawns on bottom
            else if (randomPos == 2) { gameObject.transform.localPosition = new Vector2(randomX, spawnYposMinus); }

            //Spawns to the right
            else if (randomPos == 3) { gameObject.transform.localPosition = new Vector2(spawnXposPluss, randomY); }

            //Spawns to the left
            else if (randomPos == 4) 
            {
                if (MobileScript.isMobile == true)
                {
                    randomY = Random.Range(-570, 250);
                    if (isBigSlime == true)
                    {
                        randomY = Random.Range(-635, 250);
                    }
                }

                gameObject.transform.localPosition = new Vector2(spawnXposMinus, randomY); 
            }
        }

        if (gameObject.transform.localPosition.x < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (gameObject.transform.localPosition.x > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    #endregion

    public void InstaKill()
    {
        if (isBossEASY == true || isBossNORMAL == true || isBossHARD == true)
        {
            return;
        }

        if (isSlimeDead == true)
        {
            return;
        }

        SetDeathStuff();

        GameObject skull = ObjectPool.instance.GetSkullFromPool();
        skull.transform.localPosition = gameObject.transform.localPosition;

        targetObject.gameObject.SetActive(false);
        OnSimeDeath();
        
        if (squishSlimeCoroutine == null) { squishSlimeCoroutine = StartCoroutine(SquishTheSlime(true, false)); }
    }

    #region move towards the strawberry
    public Coroutine moveCoroutine;
    bool deathToSlime;

    bool playerDied;

    IEnumerator MoveTowardsTarget()
    {
        GameObject objetToMoveTo = null;
        if(ActiveMechanics.isDecoyPlaced == true) { objetToMoveTo = decoy; }
        else { objetToMoveTo = middleObject; }

        while (Vector3.Distance(transform.position, objetToMoveTo.transform.position) > 0.01f)
        {
            if (ActiveMechanics.isDecoyPlaced == true) { objetToMoveTo = decoy;  }
            else { objetToMoveTo = middleObject;  }

            #region Death to slimes
            if (ActiveMechanics.choseDeathToSlimes == true)
            {
                if (ActiveMechanics.usedDeathToSlimes == true && deathToSlime == false && isBossEASY == false && isBossNORMAL == false && isBossHARD == false)
                {
                    if(ActiveMechanics.deathToSlimes_slimesKilled < ActiveMechanics.deathToSlimes_killAmount)
                    {
                        if(MainMenu.isInMainMenu == false)
                        {
                            if (CursorMechanics.totalSlimesActive < 6)
                            {
                                InstaKill();
                                ActiveMechanics.deathToSlimes_slimesKilled += 1;
                            }
                            else
                            {
                                int random = Random.Range(0, CursorMechanics.totalSlimesActive - ActiveMechanics.deathToSlimes_checked);
                                if (random < ActiveMechanics.deathToSlimes_killAmount)
                                {
                                    InstaKill();
                                    ActiveMechanics.deathToSlimes_slimesKilled += 1;
                                }

                                ActiveMechanics.deathToSlimes_checked += 1;
                            }
                        }
                        else
                        {
                            ActiveMechanics.deathToSlimes_slimesKilled += 1;
                        }
                    }
                    deathToSlime = true;
                }
            }
            #endregion

            if(StrawberryMechanics.isInDeathFrame == true && playerDied == false)
            {
                playerDied = true;
                if (squishSlimeCoroutine == null) { squishSlimeCoroutine = StartCoroutine(SquishTheSlime(false, true)); }
            }

            if (PickUpgrade.isInWonRunScene == true && playerDied == false)
            {
                playerDied = true;
                if (squishSlimeCoroutine == null) { squishSlimeCoroutine = StartCoroutine(SquishTheSlime(false, true)); }
            }

            if (isTutoritalSlime == true) { moveSpeed = 0; }

            if(hitByNail == true) 
            {
                float nailReduced = (PickUpgrade.nailGunMovementSpeed + MetaProgressionUpgrades.slowerSlimes) / 100;
                moveSpeed = (originalMoveSpeed + extraSpeed) * (1 - nailReduced);
            }
            else 
            {
                float reducedSpeed = (MetaProgressionUpgrades.slowerSlimes) / 100;
                moveSpeed = (originalMoveSpeed + extraSpeed) * (1 - reducedSpeed);
            }

            if (staplerHit == false && bearTrapHit == false)
            {
                if (ActiveMechanics.isDecoyPlaced == true) 
                {
                    transform.position = Vector3.MoveTowards(
                           transform.position,
                           decoy.transform.position,
                           moveSpeed * Time.deltaTime
                       );
                }
                else
                {
                    transform.position = Vector3.MoveTowards(
                           transform.position,
                           middleObject.transform.position,
                           moveSpeed * Time.deltaTime
                       );
                }
            }

            if(gameObject.transform.localPosition.x < objetToMoveTo.transform.localPosition.x)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            yield return null;
        }
    }
    #endregion

    public static bool triggerArrowRain, triggerScythe, triggerSword, triggerBoulder, triggerMeteor, triggerSawblade, triggerKatana, triggerLog;

    #region collision 2d
    public bool isCollidingWithStrawberry;
    public static Vector2 scytheStartPos, boulderStartPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PickUpgrade.choseCursorSlash == true)
        {
            //Cursor slash
            if (collision.gameObject.layer == 10)
            {
                DamageSlime(PickUpgrade.cursorSlashDamage, false, false);
            }
        }

        if (collision.gameObject.layer == 9)
        {
            //Collidint with the strawberry
            if(isCollidingWithStrawberry == false)
            {
                isCollidingWithStrawberry = true;
                StrawberryMechanics.slimesCurrentlyColliding += 1;
                StopCoroutine(moveCoroutine);
            }
        }
        else if (collision.gameObject.layer == 6)
        {
            //Click
            DamageSlime(PickUpgrade.clickDamage + MetaProgressionUpgrades.clickDamageIncrease, true, false);

            if (PickUpgrade.choseStapler == true)
            {
                int random3 = Random.Range(0, 100);
                if (random3 < 7) //7%
                {
                    if (isSlimeDead == false)
                    {
                        GameObject staple = ObjectPool.instance.GetStapleFromPool();
                        staple.transform.SetParent(gameObject.transform);
                        staple.transform.localPosition = new Vector2(0, 0);

                        int random = Random.Range(0, 360);
                        staple.transform.localRotation = Quaternion.Euler(0, 0, random);
                    }
                }
            }

            OnClick();
        }

        else if (collision.gameObject.layer == 8)
        {
            #region projectile damaged
            if(PickUpgrade.chosePaperShot == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "PaperClip")
                {
                    DamageSlime(PickUpgrade.paperShotDamage, false, false);
                }
            }
            if (PickUpgrade.choseArrowRain == true)
            {
                if (collision.gameObject.tag == "Arrow")
                {
                    DamageSlime(PickUpgrade.arrowRainDamage, false, false);
                }
            }
            if (PickUpgrade.choseKnifeOrbital == true)
            {
                if (collision.gameObject.tag == "Knife")
                {
                    DamageSlime(PickUpgrade.knifeStabDamage, false, false);
                }
            }
            if (PickUpgrade.choseLaserGun == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "LaserGun")
                {
                    DamageSlime(PickUpgrade.laserGunDamage, false, false);
                }
            }
            if (PickUpgrade.choseScythe == true)
            {
                if (collision.gameObject.tag == "Scythe")
                {
                    DamageSlime(PickUpgrade.scytheDamage / 3, false, false);
                }
            }
            if (PickUpgrade.chosePoisonDart == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "PoisonDart")
                {
                    DamageSlime(PickUpgrade.poisonDartDamage, false, false);
                    poisonCoroutine = StartCoroutine(PoisonDamage());
                }
            }
            if (PickUpgrade.choseSword == true)
            {
                if (collision.gameObject.tag == "Sword")
                {
                    DamageSlime(PickUpgrade.swordDamage, false, false);
                }
            }
            if (PickUpgrade.choseChainBall == true)
            {
                if (collision.gameObject.tag == "BigBall")
                {
                    DamageSlime(PickUpgrade.chainBallDamage, false, false);
                }
            }
            if (PickUpgrade.choseThorn == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Thorn")
                {
                    DamageSlime(PickUpgrade.thornDamage, false, false);
                }
            }
            if (PickUpgrade.choseBigLaser == true)
            {
                if (collision.gameObject.tag == "BigLaser")
                {
                    DamageSlime(PickUpgrade.bigLaserDamage, false, false);
                }
            }
            if (PickUpgrade.choseBoulder == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Boulder")
                {
                    DamageSlime(PickUpgrade.boulderDamage, false, false);
                }
            }
            if (PickUpgrade.choseBouncyBall == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "BouncyBall")
                {
                    DamageSlime(PickUpgrade.bouncyBallDamage, false, false);
                }
            }
            if (PickUpgrade.choseMeteor == true)
            {
                if (collision.gameObject.tag == "Meteor")
                {
                    DamageSlime(PickUpgrade.meteorDamage, false, false);
                }
            }
            if (PickUpgrade.choseStapler == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Staple")
                {
                    DamageSlime(PickUpgrade.staplerDamage, false, false);
                    if(isBossDoneFastMove == true) { staplerHit = true; }
                    StartCoroutine(StaplerWait());
                }
            }
            if (PickUpgrade.choseKunai == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Kunai")
                {
                    int random = Random.Range(0,100);

                    if(random < PickUpgrade.kunaiInstaKill)
                    {
                        InstaKill();
                    }
                    else
                    {
                        DamageSlime(PickUpgrade.kunaiDamage, false, false);
                    }
                }
            }
            if (PickUpgrade.choseSpikyShield == true)
            {
                if (collision.gameObject.tag == "SpikyShield")
                {
                    DamageSlime(PickUpgrade.spikyShieldDamage, false, false);
                }
            }
            if (PickUpgrade.choseFriendlyBullets == true)
            {
                if (collision.gameObject.tag == "FriendlyBullet")
                {
                    DamageSlime(PickUpgrade.friendlyBulletsDamage, false, false);
                }
               
            }
            if (ActiveMechanics.choseAntiSlime == true)
            {
                if (collision.gameObject.tag == "AntiSlimeBullet")
                {
                    int random = Random.Range(0, 100);

                    if (random < ActiveMechanics.antiBulletDeathChance)
                    {
                        InstaKill();
                    }
                    else
                    {
                        DamageSlime(ActiveMechanics.antiSlimeDamage, false, false);
                    }
                }
            }
            if (PickUpgrade.choseSawBlade == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "SawBlade")
                {
                    DamageSlime(PickUpgrade.sawBladeDamage, false, false);
                }
            }
            if (PickUpgrade.choseKatana == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Katana")
                {
                    DamageSlime(PickUpgrade.katanaDamage, false, false);
                }
            }
            if (PickUpgrade.choseSpikes == true)
            {
                if(PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
                {
                    if (collision.gameObject.tag == "Spike")
                    {
                        DamageSlime(PickUpgrade.spikeDamage, false, false);
                    }
                }
            }
            if (PickUpgrade.choseBlade == true)
            {
                if (collision.gameObject.tag == "Blade")
                {
                    int randomInstaKill = Random.Range(0,100);
                    if(randomInstaKill < PickUpgrade.bladeInstaKillChance)
                    {
                        InstaKill();
                    }
                    else
                    {
                        bleedCoroutine = StartCoroutine(BleedDamage());
                    }
                }
            }
            if (PickUpgrade.choseNailGun == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Nail")
                {
                    if (isBossDoneFastMove == true) { hitByNail = true; }
                    nailBleedCoroutine = StartCoroutine(NailBleedDamage());
                }
            }
            if (PickUpgrade.choseBearTrap == true)
            {
                if (collision.gameObject.tag == "BearTrap")
                {
                    if (isBossDoneFastMove == true) { bearTrapHit = true; }
                    DamageSlime(PickUpgrade.bearTrapDamage, false, false);
                    StartCoroutine(BearTrapWait());
                }
            }
            if (PickUpgrade.choseLog == true || ActiveMechanics.choseProjectileFrenzy == true)
            {
                if (collision.gameObject.tag == "Log")
                {
                    DamageSlime(PickUpgrade.logDamage, false, false);
                }
            }
            if(PickUpgrade.choseLegs == true)
            {
                if (collision.gameObject.tag == "EnemyBulletKicked")
                {
                    DamageSlime(PickUpgrade.kickedBulletDamage, false, false);
                }
            }
            #endregion
        }
    }

    bool hitByNail;

    IEnumerator StaplerWait()
    {
        yield return new WaitForSeconds(PickUpgrade.staplerStunTine);
        CheckIfStaplerStuck(true);

        staplerHit = false;
    }

    IEnumerator BearTrapWait()
    {
        yield return new WaitForSeconds(PickUpgrade.bearTrapStunTimer);

        bearTrapHit = false;
    }
    #endregion


    #region check stapler and nail stuck
    public void CheckIfStaplerStuck(bool stapleCheck)
    {
        foreach (Transform staple in transform)
        {
            if (staple.CompareTag("Staple"))
            {
                staple.transform.SetParent(projectileParent.transform);
                ObjectPool.instance.ReturnStapleToPool(staple.gameObject);
            }
        }

        if (DemoScript.isDemo == false && stapleCheck == false)
        {
            foreach (Transform nail in transform)
            {
                if (nail.CompareTag("Nail"))
                {
                    nail.transform.SetParent(projectileParent.transform);
                    ObjectPool.instance.ReturnNailFromPool(nail.gameObject);
                }
            }
        }
    }
    #endregion


    #region on slime click
    public void OnClick()
    {
        if (PickUpgrade.choseArrowRain == true)
        {
            int random = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true) 
            {
                if(random < ActiveMechanics.cloverChanceAdd) { triggerArrowRain = true; }
            }
            else
            {
                if (random < 15  + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                { triggerArrowRain = true; } //15%
            }
        }

        if (PickUpgrade.choseScythe == true)
        {
            int random2 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random2 < ActiveMechanics.cloverChanceAdd) { triggerScythe = true; scytheStartPos = gameObject.transform.position; }
            }
            else
            {
                if (random2 < 16 + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) //15%
                {
                    triggerScythe = true;
                    scytheStartPos = gameObject.transform.position;
                }
            }
        }

        if (PickUpgrade.choseSword == true)
        {
            int random3 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random3 < ActiveMechanics.cloverChanceAdd) { triggerSword = true; }
            }
            else
            {
                if (random3 < 12 + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) //12%
                {
                    triggerSword = true;
                }
            }
        }

        if (PickUpgrade.choseBoulder == true)
        {
            int random3 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random3 < ActiveMechanics.cloverChanceAdd) { triggerBoulder = true; boulderStartPos = gameObject.transform.position; }
            }
            else
            {
                if (random3 < 17 + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    boulderStartPos = gameObject.transform.position;
                    triggerBoulder = true;
                }
            }
        }

        if (PickUpgrade.choseMeteor == true)
        {
            int random3 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random3 < ActiveMechanics.cloverChanceAdd) { triggerMeteor = true; }
            }
            else
            {
                if (random3 < 14 + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    triggerMeteor = true;
                }
            }
        }

        if (PickUpgrade.choseSawBlade == true)
        {
            int random3 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random3 < ActiveMechanics.cloverChanceAdd) { triggerSawblade = true; }
            }
            else
            {
                if (random3 < 16 + PickUpgrade.totalChanceIncreaseHIGH + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    triggerSawblade = true;
                }
            }
        }

        if (PickUpgrade.choseKatana == true)
        {
            int random3 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random3 < ActiveMechanics.cloverChanceAdd) { triggerKatana = true; }
            }
            else
            {
                if (random3 < 18 + PickUpgrade.totalChanceIncreaseHIGH + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    triggerKatana = true;
                }
            }
        }

        if (PickUpgrade.choseLog == true)
        {
            int random3 = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random3 < ActiveMechanics.cloverChanceAdd) { cursorMechanicsScript.ShootLog(gameObject.transform.position); }
            }
            else
            {
                if (random3 < 14 + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    cursorMechanicsScript.ShootLog(gameObject.transform.position);
                }
            }
        }
    }
    #endregion


    #region poison damage
    public Coroutine poisonCoroutine;
    IEnumerator PoisonDamage()
    {
        int timesDealtPoison = 0;

        while (timesDealtPoison < 5)
        {
            yield return new WaitForSeconds(1);
            timesDealtPoison += 1;
            if(isSlimeDead == false) { DamageSlime(PickUpgrade.poisonDamage, false, true); }
        }
    }
    #endregion

    #region bleed
    public Coroutine bleedCoroutine;
    IEnumerator BleedDamage()
    {
        int timesDealtPoison = 0;

        while (timesDealtPoison < 3)
        {
            timesDealtPoison += 1;
            if (isSlimeDead == false) { DamageSlime(PickUpgrade.bladeBleedDamage, false, false); }
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

    #region bleed from nail
    public Coroutine nailBleedCoroutine;
    IEnumerator NailBleedDamage()
    {
        int timesDealtPoison = 0;

        while (timesDealtPoison < 3)
        {
            timesDealtPoison += 1;
            if (isSlimeDead == false) { DamageSlime(PickUpgrade.nailGunBleedDamage, false, false); }
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

    #region damage slime
    public static Vector2 slimeSquishedPos;
    private Coroutine flashCoroutine;

    public void DamageSlime(float damage, bool clicked, bool poison)
    {
        if(isSlimeDead == true) { return; }

        if(flashCoroutine == null) 
        {
            flashCoroutine = StartCoroutine(DamageWhiteFlash());
        }

        float damageIncrease = (PickUpgrade.totalIncreaseDamage + MetaProgressionUpgrades.damageIncrease) / 100;

        damage *= (1 + damageIncrease);

        bool hitCrit = false;

        if(clicked == true)
        {
            float totalCritIncrease = PickUpgrade.critIncrease + MetaProgressionUpgrades.critIncreaseIncrease;

            if (ActiveMechanics.punchyClicksIsUsed == true)
            {
                hitCrit = true;
                if (PickUpgrade.critIncrease <= 2) { damage *= 2; }
                else
                {
                    damage *= totalCritIncrease;
                }
            }
            else 
            {
                int randomCrit = Random.Range(0, 100);
                if (randomCrit < PickUpgrade.critChance + MetaProgressionUpgrades.critChanceIncrease)
                {
                    hitCrit = true;
                    damage *= totalCritIncrease;
                }
            }
        }

        slimeHealth -= damage;

        TextMeshProUGUI damageText = ObjectPool.instance.GetDamageTextFromPool();
        if(poison == true) { damageText.color = Color.green; }
        else { damageText.color = Color.red; }

        if (hitCrit == true)
        {
            damageText.transform.localScale = new Vector2(1.1f, 1.1f);
            damageText.text = $"{LocalizationSCRIPT.crit}\n" + damage.ToString("F0");
        }
        else
        {
            damageText.transform.localScale = new Vector2(0.85f, 0.85f);
            damageText.text = damage.ToString("F0");
        }

        if(isTutoritalSlime == true) 
        {
            damageText.transform.position = gameObject.transform.position;
            damageText.transform.localScale = new Vector2(1.5f, 1.5f);
        }
        else
        {
            damageText.transform.localPosition = gameObject.transform.localPosition;
        }

        if(isTutoritalSlime == true)
        {
            float currentTime = Time.time;
            overlappingSound.PlaySound(2, currentTime, true); return; 
        }

        if (slimeHealth <= 0 && isSlimeDead == false)
        {
            SetDeathStuff();
            StopCoroutine(moveCoroutine);

            OnSimeDeath();

            if (squishSlimeCoroutine == null) { squishSlimeCoroutine = StartCoroutine(SquishTheSlime(false, false)); }
        }
        else
        {
            float currentTime = Time.time;
            overlappingSound.PlaySound(2, currentTime, true);
        }
    }

    #region Do Death stuff
    public void SetDeathStuff()
    {
        CheckIfStaplerStuck(false);

        isSlimeDead = true;
        targetObject.gameObject.SetActive(false);

        slimeSquishedPos = gameObject.transform.position;

        slimeCollider.enabled = false;
    }
    #endregion

    #endregion

    #region on slime death
    public static bool triggerKunai;

    public static Vector2 bouncyBallStartPos;

    public void OnSimeDeath()
    {
        if (PickUpgrade.chosePaperShot == true)
        {
            int random = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random < ActiveMechanics.cloverChanceAdd) { CursorMechanics.triggerPaperClip = true; }
            }
            else
            {
                if (random < 22 + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) { CursorMechanics.triggerPaperClip = true; } //20%
            }
        }

        //PoisonDart
        if (PickUpgrade.chosePoisonDart == true)
        {
            int random = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random < ActiveMechanics.cloverChanceAdd) { cursorMechanicsScript.ShootPoisonDart(gameObject.transform.position); }
            }
            else
            {
                if (random < 20 + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    cursorMechanicsScript.ShootPoisonDart(gameObject.transform.position);
                }//18%
            }
        }

        if (PickUpgrade.choseThorn == true)
        {
            int random = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random < ActiveMechanics.cloverChanceAdd) { cursorMechanicsScript.ShootThorn(gameObject.transform.localPosition, true); }
            }
            else
            {
                if (random < 27 + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    cursorMechanicsScript.ShootThorn(gameObject.transform.localPosition, true);
                } //27%
            }
        }

        if (PickUpgrade.choseBouncyBall == true)
        {
            int random = Random.Range(0, 100);

            if (ActiveMechanics.isCloverInUse == true)
            {
                bouncyBallStartPos = gameObject.transform.position;
                if (random < ActiveMechanics.cloverChanceAdd) { cursorMechanicsScript.SelectRandomTargetObject(3); }
            }
            else
            {
                bouncyBallStartPos = gameObject.transform.position;
                if (random < 16 + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    cursorMechanicsScript.SelectRandomTargetObject(3);
                }
            }
        }

        if (PickUpgrade.choseKunai == true)
        {
            int random = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random < ActiveMechanics.cloverChanceAdd)
                {
                    CursorMechanics.kunaiStartPos = gameObject.transform.position; 
                    triggerKunai = true;
                }
            }
            else
            {
                if (random < 15 + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
                {
                    CursorMechanics.kunaiStartPos = gameObject.transform.position; //14%
                    triggerKunai = true;
                }
            }
        }

        if (PickUpgrade.choseBearTrap == true)
        {
            int random = Random.Range(0, 100);
            if (ActiveMechanics.isCloverInUse == true)
            {
                if (random < ActiveMechanics.cloverChanceAdd)
                {
                    GameObject bearTrap = ObjectPool.instance.GetBearTrapFromPool();
                    bearTrap.transform.position = gameObject.transform.position;
                }
            }
            else
            {
                if (random < 13 + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) //13%
                {
                    GameObject bearTrap = ObjectPool.instance.GetBearTrapFromPool();
                    bearTrap.transform.position = gameObject.transform.position;
                }
            }
        }
    }
    #endregion


    #region damage white flash
    IEnumerator DamageWhiteFlash()
    {
        material.SetColor("_FlashColor", whiteFlashColor);

        float flashTime = 0.12f;
        float flashTimer = 0;

        float flashAmount;

        while (flashTimer < flashTime)
        {
            flashTimer += Time.deltaTime;

            flashAmount = Mathf.Lerp(0f, 1f, flashTimer / flashTime);
            material.SetFloat("_FlashAmount", flashAmount);

            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        while (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;

            flashAmount = Mathf.Lerp(0f, 1f, flashTimer / flashTime);

            material.SetFloat("_FlashAmount", flashAmount);
            yield return null;
        }

        //material.SetColor("_FlashColor", whiteFlashColor);
        material.SetFloat("_FlashAmount", 0);
        flashCoroutine = null;
    }
    #endregion
    

    //SLIME DEATH!
    #region slimes squished/killed

    private Coroutine squishSlimeCoroutine;

    IEnumerator SquishTheSlime(bool waitMore, bool strawberryDeath)
    {
        if(waitMore == true)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.4f));
        }

        textureObject.gameObject.SetActive(false);

        if(isBossEASY == true || isBossNORMAL == true || isBossHARD == true) { }
        else { SpawnSlimes.slimesSquished += 1; }

        //Debug.Log(SpawnSlimes.slimesSquished);
        //Debug.Log(SpawnSlimes.slimesWaveSpawnCount);

        if (SpawnSlimes.slimesSquished >= SpawnSlimes.slimesWaveSpawnCount)
        {
            SpawnSlimes.isWaveCompleted = true;
        }

        if(DemoScript.isDemo == false)
        {
            int randomCoin = Random.Range(0, 100);
            if (randomCoin < MetaProgressionUpgrades.goldStartDropChance + MetaProgressionUpgrades.goldChanceIncrease)
            {
                if (strawberryDeath == false)
                {
                    SpawnCoin();
                }
            }
        }

        squishObject.gameObject.SetActive(true);

        GameObject goo = null;
        float gooSize = 0;
        float gooOffset = 13;

        //Spawn goo
        #region is regular
        if (isGreenSlime_Regular == true)
        {
            goo = ObjectPool.instance.GetGooFromPool();
            gooSize = Random.Range(0.78f, 0.87f);
        }
        else if (isBlueSlime_regular == true)
        {
            goo = ObjectPool.instance.GetBlueGooFromPool();
            gooSize = Random.Range(0.78f, 0.87f);
        }
        else if (isYellowSlime_regular == true)
        {
            goo = ObjectPool.instance.GetOrangeGooFromPool();
            gooSize = Random.Range(0.78f, 0.87f);
        }
        else if (isRedSlime_regular == true)
        {
            goo = ObjectPool.instance.GetRedGooFromPool();
            gooSize = Random.Range(0.78f, 0.87f);
        }
        else if (isPurpleSlime_Regular == true)
        {
            goo = ObjectPool.instance.GetPurpleGooFromPool();
            gooSize = Random.Range(0.78f, 0.87f);
        }
        #endregion

        #region is fast
        else if (isGreenSlime_fast == true)
        {
            goo = ObjectPool.instance.GetGooFromPool();
            gooSize = Random.Range(0.75f, 0.8f);
        }
        else if (isBlueSlime_fast == true)
        {
            goo = ObjectPool.instance.GetBlueGooFromPool();
            gooSize = Random.Range(0.75f, 0.8f);
        }
        else if (isYellowSlime_fast == true)
        {
            goo = ObjectPool.instance.GetOrangeGooFromPool();
            gooSize = Random.Range(0.75f, 0.8f);
        }
        else if (isRedSlime_fast == true)
        {
            goo = ObjectPool.instance.GetRedGooFromPool();
            gooSize = Random.Range(0.75f, 0.8f);
        }
        else if (isPurpleSlime_fast == true)
        {
            goo = ObjectPool.instance.GetPurpleGooFromPool();
            gooSize = Random.Range(0.75f, 0.8f);
        }
        #endregion

        #region is shooting
        else if (isGreenSlime_shooting == true)
        {
            goo = ObjectPool.instance.GetGooFromPool();
            gooSize = Random.Range(1.35f, 1.55f);
        }
        else if (isBlueSlime_shooting == true)
        {
            goo = ObjectPool.instance.GetBlueGooFromPool();
            gooSize = Random.Range(1.35f, 1.55f);
        }
        else if (isYellowSlime_shooting == true)
        {
            goo = ObjectPool.instance.GetOrangeGooFromPool();
            gooSize = Random.Range(1.35f, 1.55f);
        }
        else if (isRedSlime_shooting == true)
        {
            goo = ObjectPool.instance.GetRedGooFromPool();
            gooSize = Random.Range(1.35f, 1.55f);
        }
        else if (isPurpleSlime_shooting == true)
        {
            goo = ObjectPool.instance.GetPurpleGooFromPool();
            gooSize = Random.Range(1.35f, 1.55f);
        }
        #endregion

        #region is big
        else if (isGrenSlime_big)
        {
            goo = ObjectPool.instance.GetGooFromPool();
            gooSize = Random.Range(2.45f, 2.8f);
            gooOffset = 55;
        }
        else if (isBlueSlime_big)
        {
            goo = ObjectPool.instance.GetBlueGooFromPool();
            gooSize = Random.Range(2.45f, 2.8f); gooOffset = 55;
        }
        else if (isYellowSlime_big)
        {
            goo = ObjectPool.instance.GetOrangeGooFromPool();
            gooSize = Random.Range(2.45f, 2.8f); gooOffset = 55;
        }
        else if (isRedSlime_big)
        {
            goo = ObjectPool.instance.GetRedGooFromPool();
            gooSize = Random.Range(2.45f, 2.8f); gooOffset = 55;
        }
        else if (isPurpleSlime_big)
        {
            goo = ObjectPool.instance.GetPurpleGooFromPool();
            gooSize = Random.Range(2.45f, 2.8f); gooOffset = 55;
        }
        #endregion

        if(isBossEASY == true)
        {
            goo = ObjectPool.instance.GetBlueGooFromPool();
            gooSize = Random.Range(5.2f, 5.2f); gooOffset = 120;
        }
        if (isBossHARD == true)
        {
            goo = hardGoo;
            hardGoo.SetActive(true);
            gooSize = Random.Range(2.6f, 2.6f); gooOffset = 130;
        }

        #region is normal boss splits
        if (isBossNORMAL == true && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
        {
            Vector2 thisPos = gameObject.transform.localPosition;
            int randomXPosOffset1 = Random.Range(-30, 90);
            int randomXPosOffset2 = Random.Range(-30, 90);

            int randomPosOffsetMinus = Random.Range(-50, -120);
            int randomPosOffsetPluss = Random.Range(50, 120);

            float xScale = (float)gameObject.transform.localScale.x;

            if (xScale == 1.65f) //First split
            {
                GameObject smoll1_1 = ObjectPool.instance.GetNormalBossFromPool();
                GameObject smoll1_2 = ObjectPool.instance.GetNormalBossFromPool();

                smoll1_1.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset1, thisPos.y + randomPosOffsetMinus);
                smoll1_2.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset2, thisPos.y + randomPosOffsetPluss);

                smoll1_1.transform.localScale = new Vector2(split1Size, split1Size);
                smoll1_2.transform.localScale = new Vector2(split1Size, split1Size);
            }

            if (xScale == split1Size) //Second split
            {
                GameObject smoll1_1 = ObjectPool.instance.GetNormalBossFromPool();
                GameObject smoll1_2 = ObjectPool.instance.GetNormalBossFromPool();

                smoll1_1.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset1, thisPos.y + randomPosOffsetMinus);
                smoll1_2.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset2, thisPos.y + randomPosOffsetPluss);

                smoll1_1.transform.localScale = new Vector2(split2Size, split2Size);
                smoll1_2.transform.localScale = new Vector2(split2Size, split2Size);
            }

            if (xScale == split2Size) //Third split
            {
                GameObject smoll1_1 = ObjectPool.instance.GetNormalBossFromPool();
                GameObject smoll1_2 = ObjectPool.instance.GetNormalBossFromPool();

                smoll1_1.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset1, thisPos.y + randomPosOffsetMinus);
                smoll1_2.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset2, thisPos.y + randomPosOffsetPluss);

                smoll1_1.transform.localScale = new Vector2(split3Size, split3Size);
                smoll1_2.transform.localScale = new Vector2(split3Size, split3Size);
            }

            if (xScale == split3Size) //Fourth split
            {
                GameObject smoll1_1 = ObjectPool.instance.GetNormalBossFromPool();
                GameObject smoll1_2 = ObjectPool.instance.GetNormalBossFromPool();

                smoll1_1.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset1, thisPos.y + randomPosOffsetMinus);
                smoll1_2.transform.localPosition = new Vector2(thisPos.x + randomXPosOffset2, thisPos.y + randomPosOffsetPluss);

                smoll1_1.transform.localScale = new Vector2(split4Size, split4Size);
                smoll1_2.transform.localScale = new Vector2(split4Size, split4Size);
            }
        }
        #endregion

        #region normal boss goo mechanics
        if (isBossNORMAL == true)
        {
            goo = ObjectPool.instance.GetNormalBossGooFromPool();
            float xScale = (float)gameObject.transform.localScale.x;

            if (xScale == 1.65f) //First split
            {
                gooSize = Random.Range(3.4f, 3.4f); gooOffset = 140;
            }

            if (xScale == split1Size) //Second split
            {
                gooSize = Random.Range(2.5f, 2.5f); gooOffset = 100;
            }

            if (xScale == split2Size) //Third split
            {
                gooSize = Random.Range(1.45f, 1.45f); gooOffset = 70;
            }

            if (xScale == split3Size) //Fourth split
            {
                gooSize = Random.Range(1.3f, 1.3f); gooOffset = 50;
            }

            if (xScale == split4Size) //Fourth split
            {
                gooSize = Random.Range(0.8f, 0.8f); gooOffset = 40;
            }
        }
        #endregion

        Vector2 pos = gameObject.transform.localPosition;
        Vector2 gooSpawnPos = new Vector2(pos.x, pos.y - gooOffset);
        goo.transform.localPosition = gooSpawnPos;
        goo.transform.localScale = new Vector2(gooSize, gooSize);

        if (strawberryDeath == false)
        {
            float currentTime = Time.time;
            overlappingSound.PlaySound(1, currentTime, true);
        }

        #region is regular
        if (isGreenSlime_Regular == true)
        {
            animator.SetTrigger("Squish_green_basic");
        }
        else if (isBlueSlime_regular == true)
        {
            animator.SetTrigger("Squish_blue_regular");
        }
        else if (isYellowSlime_regular == true)
        {
            animator.SetTrigger("Squish_orange_regular");
        }
        else if (isRedSlime_regular == true)
        {
            animator.SetTrigger("Squish_red_regular");
        }
        else if (isPurpleSlime_Regular == true)
        {
            animator.SetTrigger("Squish_purple_regular");
        }
        #endregion

        #region is fast
        else if (isGreenSlime_fast == true)
        {
            animator.SetTrigger("Squish_green_fast");
        }
        else if (isBlueSlime_fast == true)
        {
            animator.SetTrigger("Squish_blue_fast");
        }
        else if (isYellowSlime_fast == true)
        {
            animator.SetTrigger("Squish_orange_fast");
        }
        else if (isRedSlime_fast == true)
        {
            animator.SetTrigger("Squish_red_fast");
        }
        else if (isPurpleSlime_fast == true)
        {
            animator.SetTrigger("Squish_purple_fast");
        }
        #endregion

        #region is shooting
        else if (isGreenSlime_shooting == true)
        {
            animator.SetTrigger("Squish_green_shooting");
        }
        else if (isBlueSlime_shooting == true)
        {
            animator.SetTrigger("Squish_blue_shooting");
        }
        else if (isYellowSlime_shooting == true)
        {
            animator.SetTrigger("Squish_orange_shooting");
        }
        else if (isRedSlime_shooting == true)
        {
            animator.SetTrigger("Squish_red_shooting");
        }
        else if (isPurpleSlime_shooting == true)
        {
            animator.SetTrigger("Squish_purple_shooting");
        }
        #endregion

        #region is big
        else if (isGrenSlime_big)
        {
            animator.SetTrigger("Squish_green_big");
        }
        else if (isBlueSlime_big)
        {
            animator.SetTrigger("Squish_blue_big");
        }
        else if (isYellowSlime_big)
        {
            animator.SetTrigger("Squish_orange_big");
        }
        else if (isRedSlime_big)
        {
            animator.SetTrigger("Squish_red_big");
        }
        else if (isPurpleSlime_big)
        {
            animator.SetTrigger("Squish_purple_big");
        }
        #endregion

        if(isBossEASY == true) { animator.SetTrigger("Squish_EasyBoss"); }
        if (isBossNORMAL == true) { animator.SetTrigger("Squish_NormalBoss"); }
        if (isBossHARD == true) { animator.SetTrigger("Squish_HardBoss"); }

        yield return new WaitForSeconds(0.3f);
        squishObject.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        isSlimeDead = true;

        CheckIfStaplerStuck(false);

        squishSlimeCoroutine = null;
        if(isGreenSlime_Regular == true) { ObjectPool.instance.ReturnSlime1FromPool(gameObject); }
        if (isBlueSlime_regular == true) { ObjectPool.instance.ReturnRegularBlueToPool(gameObject); }
        if (isYellowSlime_regular == true) { ObjectPool.instance.ReturnRegularYellowToPool(gameObject); }
        if (isRedSlime_regular == true) { ObjectPool.instance.ReturnRegularRedToPool(gameObject); }
        if (isPurpleSlime_Regular == true) { ObjectPool.instance.ReturnRegularPurpleToPool(gameObject); }

        else if (isGreenSlime_fast == true) { ObjectPool.instance.ReturnFastGreenToPool(gameObject); }
        else if (isBlueSlime_fast == true) { ObjectPool.instance.ReturnFastBlueToPool(gameObject); }
        else if (isYellowSlime_fast == true) { ObjectPool.instance.ReturnFastYellowToPool(gameObject); }
        else if (isRedSlime_fast == true) { ObjectPool.instance.ReturnFastRedToPool(gameObject); }
        else if (isPurpleSlime_fast == true) { ObjectPool.instance.ReturnFastPurpleToPool(gameObject); }

        else if (isGreenSlime_shooting == true) { ObjectPool.instance.ReturnShootingGreenToPool(gameObject); }
        else if (isBlueSlime_shooting == true) { ObjectPool.instance.ReturnBlueShootingFromPool(gameObject); }
        else if (isYellowSlime_shooting == true) { ObjectPool.instance.ReturnShootingYellowToPool(gameObject); }
        else if (isRedSlime_shooting == true) { ObjectPool.instance.ReturnShootingRedToPool(gameObject); }
        else if (isPurpleSlime_shooting == true) { ObjectPool.instance.ReturnShootingPurpleToPool(gameObject); }

        else if (isGrenSlime_big == true) { ObjectPool.instance.ReturnBigGreenToPool(gameObject); }
        else if (isBlueSlime_big == true) { ObjectPool.instance.ReturnBigBlueToPool(gameObject); }
        else if (isYellowSlime_big == true) { ObjectPool.instance.ReturnBigYellowToPool(gameObject); }
        else if (isRedSlime_big == true) { ObjectPool.instance.ReturnRedBigToPool(gameObject); }
        else if (isPurpleSlime_big == true) { ObjectPool.instance.ReturnBigPurpleToPool(gameObject); }

        if(isBossEASY == true) { SpawnSlimes.isEasyBossAlive = false; gameObject.SetActive(false); }
        if (isBossNORMAL == true) 
        { 
            ObjectPool.instance.ReturnNormalBossToPool(gameObject);
            SpawnSlimes.normalBossKills += 1;
            //Debug.Log(SpawnSlimes.normalBossKills);
            if (SpawnSlimes.normalBossKills == 31)
            {
                SpawnSlimes.isNormalBossAlive = false;
            }
        }
        if (isBossHARD == true) { SpawnSlimes.isHardBossAlive = false; gameObject.SetActive(false); }
    }
    #endregion


    #region Spawn coin
    public void SpawnCoin()
    {
        GameObject coin = ObjectPool.instance.GetCoinFromPool();
        coin.transform.position = gameObject.transform.position;
    }
    #endregion

    #region Shoot bullet
    public int timesHardBossShot;

    public void ShootEnemyBullet(float time, float shotSpeed)
    {
        if(isTutoritalSlime == true) { time = 1.5f; }
    
        StartCoroutine(ContinueToShoot(time, shotSpeed));
    }

    IEnumerator ContinueToShoot(float time, float shotSpeed)
    {
        if(SelectGameMode.choseHard == true)
        {
            time -= Random.Range(0.15f, 0.35f);
        }

        if (isBossHARD == true)
        {
            yield return new WaitForSeconds(2f);
            shotSpeed = Random.Range(2.5f, 3.4f);
        }

        if (isTutoritalSlime == true) { yield return new WaitForSeconds(0.1f); }
        else { yield return new WaitForSeconds(1); }

        while (true)
        {
            float shootWait = 0f;
            float shootTime = time;

            while (true)
            {
                if (isBossHARD == true)
                {
                    if (timesHardBossShot < 9) { time = Random.Range(0.2f, 0.32f); }
                    if (timesHardBossShot > 8) { time = Random.Range(3f, 4f); }
                    shootTime = time;
                }

                while (shootWait < shootTime)
                {
                    shootWait += Time.deltaTime;
                    yield return null;
                }

                shootWait = 0;

                StartCoroutine(ChargeBullet(shotSpeed, 0f));

                if(MobileScript.isMobile == false)
                {
                    if (isYellowSlime_shooting == true) { StartCoroutine(ChargeBullet(shotSpeed, 0.2f)); }
                    if (isPurpleSlime_shooting == true) { StartCoroutine(ChargeBullet(shotSpeed, 0.14f)); }
                    if (isRedSlime_shooting == true)
                    {
                        StartCoroutine(ChargeBullet(shotSpeed, 0.17f));
                        StartCoroutine(ChargeBullet(shotSpeed, 0.34f));
                    }
                }
            }
        }
    }

    IEnumerator ChargeBullet(float shootSpeed, float extraWaitTime)
    {
        if (isBossHARD == true)
        {
            timesHardBossShot += 1;
            if (timesHardBossShot == 10) { timesHardBossShot = 0; }
        }

        if (SelectGameMode.choseNormal == true)
        {
            shootSpeed += Random.Range(0.1f, 0.2f);
        }

        if (SelectGameMode.choseRampage == true && PickUpgrade.isInChooseUpgrade == true)
        {
        }
        else
        {
            yield return new WaitForSeconds(extraWaitTime);

            Transform bulletSetPos = null;

            if(isBossHARD == true)
            {
                int randomPos = Random.Range(1, 5);
                if (randomPos == 1) { bulletSetPos = shootSpawnPos; }
                if (randomPos == 2) { bulletSetPos = shootSpawnPos2; }
                if (randomPos == 3) { bulletSetPos = shootSpawnPos3; }
                if (randomPos == 4) { bulletSetPos = shootSpawnPos4; }
            }
            else
            {
                bulletSetPos = shootSpawnPos;
            }

            GameObject bullet = ObjectPool.instance.GetEnemyBulletFromPool();
            bullet.transform.position = bulletSetPos.transform.position;

            bool isFriendly = false;

            int randomFriendly = 0;

            if(PickUpgrade.choseFriendlyBullets == true)
            {
                randomFriendly = Random.Range(0, 100);
                if (randomFriendly < 14)
                {
                    isFriendly = true;
                    bullet.tag = "FriendlyBullet";
                    bullet.layer = 8;
                }
                else
                {
                    bullet.tag = "EnemyBullet";
                    bullet.layer = 12;
                }
            }
            else
            {
                isFriendly = false;
                bullet.tag = "EnemyBullet";
                bullet.layer = 12;
            }

            float chargeWait = 0f;
            float chargeTime = 1.5f;

            while (chargeWait < chargeTime)
            {
                CheckBullet(bullet);

                bullet.transform.position = bulletSetPos.transform.position;
                chargeWait += Time.deltaTime;

                bullet.transform.localScale = new Vector2(chargeWait / 2f, chargeWait / 2f);

                yield return null;
            }

            bullet.transform.position = bulletSetPos.transform.position;

            yield return new WaitForSeconds(0.1f);

            CheckBullet(bullet);

            if (isSlimeDead == false && bullet.activeInHierarchy)
            {
                GameObject flash = ObjectPool.instance.GetShootFlashFromPool();
                flash.transform.position = bullet.transform.position;
                audioManager.Play("slimeShot");
            }

            yield return new WaitForSeconds(0.2f);

            CheckBullet(bullet);

            if (bullet.activeInHierarchy == true)
            {
                Vector2 direction = new Vector2(0, 0);

                if (isTutoritalSlime == false)
                {
                    Vector2 strawberryPos = new Vector2(0, 0);
                    if (ActiveMechanics.isDecoyPlaced == true) { strawberryPos = decoy.transform.position; }
                    else { strawberryPos = middleObject.transform.position; }
                    Vector2 bulletPos = bullet.transform.position;

                    direction = (strawberryPos - bulletPos).normalized;
                }

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float reducedShotSpeed = MetaProgressionUpgrades.slowerBullets / 100;
                float speed = shootSpeed * (1 - reducedShotSpeed);

                if (isTutoritalSlime == true)
                {
                    direction = Vector2.right;
                    rb.velocity = direction * speed;
                }
                else 
                {
                    if(PickUpgrade.choseFriendlyBullets == true)
                    {
                        if(isFriendly == true)
                        {
                            cursorMechanicsScript.SelectRandomTargetObject(5);
                            Vector2 bulletPos = bullet.transform.position;
                            direction = (CursorMechanics.friendlyBulletTarget - bulletPos).normalized;
                            speed += 4;
                        }
                    }

                    rb.velocity = direction * speed;
                }
            }

            CheckBullet(bullet);
            if (isSlimeDead == false && StrawberryMechanics.isDeath == false && PickUpgrade.isInWonRunScene == false)
            {
                StartCoroutine(CheckBulletOnMoreTime(bullet));
            }
        }
    }

    IEnumerator CheckBulletOnMoreTime(GameObject bullet)
    {
        yield return new WaitForSeconds(0.05f);
        CheckBullet(bullet);
    }

    public void CheckBullet(GameObject bullet)
    {
        if (bullet.activeInHierarchy)
        {
            if (isSlimeDead == true || StrawberryMechanics.isDeath == true || PickUpgrade.isInWonRunScene == true)
            {
                ObjectPool.instance.ReturnEnemyBulletFromPool(bullet);
            }
        }
    }
    #endregion

    private void OnDisable()
    {
        if(isTutoritalSlime == false)
        {
            targetObject.gameObject.SetActive(false);
        }

        if (isCollidingWithStrawberry == true) { StrawberryMechanics.slimesCurrentlyColliding -= 1; }
        StopAllCoroutines();
    }
}
