using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlimeMechanics : MonoBehaviour
{
    public bool isTutoritalSlime;

    public bool isRegularSlime, isFastSlime, isShootingslime, isBigSlime;

    public bool isGreenSlime_Regular, isBlueSlime_regular, isYellowSlime_regular, isRedSlime_regular, isPurpleSlime_Regular;
    public bool isGreenSlime_fast, isBlueSlime_fast, isYellowSlime_fast, isRedSlime_fast, isPurpleSlime_fast;
    public bool isGreenSlime_shooting, isBlueSlime_shooting, isYellowSlime_shooting, isRedSlime_shooting, isPurpleSlime_shooting;
    public bool isGrenSlime_big, isBlueSlime_big, isYellowSlime_big, isRedSlime_big, isPurpleSlime_big;

    public GameObject middleObject, projectileParent, decoy;
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
    private bool isSlimeDead;

    private bool staplerHit, bearTrapHit;

    public AudioManager audioManager;
    public GameObject audioGameobject;

    public OverlappingSounds overlappingSound;
    public GameObject overlappingObject;

    #region Awake
    private void Awake()
    {
        audioGameobject = GameObject.Find("AudioManager");
        audioManager = audioGameobject.GetComponent<AudioManager>();

        overlappingObject = GameObject.Find("OverlappingSounds");
        overlappingSound = overlappingObject.GetComponent<OverlappingSounds>();

        projectileParent = GameObject.Find("ProjectilesParent");

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
        extraSpeed = 0;

        if(SelectGameMode.choseNormal == true) 
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

        #region is regular slime
        if (isGreenSlime_Regular == true)
        {
            slimeHealth = SpawnSlimes.greenRegular_health;

            float randomSpeed = Random.Range(0.20f, 0.22f);
            moveSpeed = randomSpeed;
        }
        if (isBlueSlime_regular == true)
        {
            slimeHealth = SpawnSlimes.blueRegular_health;

            float randomSpeed = Random.Range(0.21f, 0.23f);
            moveSpeed = randomSpeed;
        }
        if (isYellowSlime_regular == true)
        {
            slimeHealth = SpawnSlimes.yellowRegular_health;

            float randomSpeed = Random.Range(0.22f, 0.24f);
            moveSpeed = randomSpeed;
        }
        if (isRedSlime_regular == true)
        {
            slimeHealth = SpawnSlimes.redRegular_health;

            float randomSpeed = Random.Range(0.24f, 0.25f);
            moveSpeed = randomSpeed;
        }
        if (isPurpleSlime_Regular == true)
        {
            slimeHealth = SpawnSlimes.purpleRegular_health;

            float randomSpeed = Random.Range(0.26f, 0.29f);
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
            moveSpeed = randomSpeed;
        }
        if (isYellowSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.yellowFast_health;

            float randomSpeed = Random.Range(0.55f, 0.62f);
            moveSpeed = randomSpeed;
        }
        if (isRedSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.redFast_health;

            float randomSpeed = Random.Range(0.65f, 0.71f);
            moveSpeed = randomSpeed;
        }
        if (isPurpleSlime_fast == true)
        {
            slimeHealth = SpawnSlimes.purpleFast_health;

            float randomSpeed = Random.Range(0.8f, 1f);
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
        moveSpeed = 0.85f;
        originalMoveSpeed = moveSpeed;
        yield return new WaitForSeconds(1.2f);
        float randomSpeed = Random.Range(0.07f, 0.09f);
        moveSpeed = randomSpeed;
        originalMoveSpeed = moveSpeed;
    }

    #region Set spawn position
    public void SetPos()
    {
        if(isTutoritalSlime == true) { return; }

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

        if (randomPos == 1) { gameObject.transform.localPosition = new Vector2(randomX, spawnYposPluss); }
        else if (randomPos == 2) { gameObject.transform.localPosition = new Vector2(randomX, spawnYposMinus); }
        else if (randomPos == 3) { gameObject.transform.localPosition = new Vector2(spawnXposPluss, randomY); }
        else if (randomPos == 4) { gameObject.transform.localPosition = new Vector2(spawnXposMinus, -randomY); }

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
        if(isSlimeDead == true)
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
                if (ActiveMechanics.usedDeathToSlimes == true && deathToSlime == false)
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

            if(isTutoritalSlime == true) { moveSpeed = 0; }

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
                    staplerHit = true;
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
            if (PickUpgrade.choseSawBlade == true)
            {
                if (collision.gameObject.tag == "SawBlade")
                {
                    DamageSlime(PickUpgrade.sawBladeDamage, false, false);
                }
            }
            if (PickUpgrade.choseKatana == true)
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
            if (PickUpgrade.choseNailGun == true)
            {
                if (collision.gameObject.tag == "Nail")
                {
                    hitByNail = true;
                    nailBleedCoroutine = StartCoroutine(NailBleedDamage());
                }
            }
            if (PickUpgrade.choseBearTrap == true)
            {
                if (collision.gameObject.tag == "BearTrap")
                {
                    bearTrapHit = true;
                    DamageSlime(PickUpgrade.bearTrapDamage, false, false);
                    StartCoroutine(BearTrapWait());
                }
            }
            if (PickUpgrade.choseLog == true)
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

    public void CheckIfStaplerStuck(bool stapleCheck)
    {
        foreach (Transform staple in transform)
        {
            if (staple.CompareTag("Staple"))
            {
                staple.transform.SetParent(projectileParent.transform);
                ObjectPool.instance.ReturnStapleToPool(staple.gameObject);

                break;
            }
        }

        if(DemoScript.isDemo == false && stapleCheck == false)
        {
            foreach (Transform nail in transform)
            {
                if (nail.CompareTag("Nail"))
                {
                    nail.transform.SetParent(projectileParent.transform);
                    ObjectPool.instance.ReturnNailFromPool(nail.gameObject);

                    break;
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
            if (random < 15 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) { triggerArrowRain = true; } //15%
        }

        if (PickUpgrade.choseScythe == true)
        {
            int random2 = Random.Range(0, 100);
            if (random2 < 15 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) //15%
            {
                triggerScythe = true;
                scytheStartPos = gameObject.transform.position; 
            }
        }

        if (PickUpgrade.choseSword == true)
        {
            int random3 = Random.Range(0, 100);
            if (random3 < 12 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) //12%
            {
                triggerSword = true;
            }
        }

        if (PickUpgrade.choseBoulder == true)
        {
            int random3 = Random.Range(0, 100); //16%
            if (random3 < 16 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                boulderStartPos = gameObject.transform.position;
                triggerBoulder = true;
            }
        }

        if (PickUpgrade.choseMeteor == true)
        {
            int random3 = Random.Range(0, 100); //15%
            if (random3 < 15 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                triggerMeteor = true;
            }
        }

        if (PickUpgrade.choseSawBlade == true)
        {
            int random = Random.Range(0, 100); //16%
            if (random < 16 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseHIGH + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                triggerSawblade = true;
            }
        }

        if (PickUpgrade.choseKatana == true)
        {
            int random = Random.Range(0, 100); //19%
            if (random < 18 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseHIGH + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                triggerKatana = true;
            }
        }

        if (PickUpgrade.choseLog == true)
        {
            int random = Random.Range(0, 100); //13%
            if (random < 13 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                cursorMechanicsScript.ShootLog(gameObject.transform.position);
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
            DamageSlime(PickUpgrade.poisonDamage, false, true);
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
            DamageSlime(PickUpgrade.bladeBleedDamage, false, false);
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
            DamageSlime(PickUpgrade.nailGunBleedDamage, false, false);
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

    #region damage slime
    public static Vector2 slimeSquishedPos;
    private Coroutine flashCoroutine;

    public void DamageSlime(float damage, bool clicked, bool poison)
    {
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

    public void OnSimeDeath()
    {
        if (PickUpgrade.chosePaperShot == true)
        {
            int randomPaper = Random.Range(0, 100);
            if (randomPaper < 20 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) { CursorMechanics.triggerPaperClip = true; } //20%
        }

        //PoisonDart
        if (PickUpgrade.chosePoisonDart == true)
        {
            int randomPoisonDart = Random.Range(0, 100);
            if (randomPoisonDart < 20 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) 
            {
                cursorMechanicsScript.ShootPoisonDart(gameObject.transform.position); 
            }//18%
        }

        if (PickUpgrade.choseThorn == true)
        {
            int randomThorn = Random.Range(0, 100);
            if (randomThorn < 27 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseMID + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) 
            {
                cursorMechanicsScript.ShootThorn(gameObject.transform.localPosition, true);
            } //27%
        }

        if (PickUpgrade.choseBouncyBall == true)
        {
            int randomBouncy = Random.Range(0, 100);
            if (randomBouncy < 16 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                CursorMechanics.bouncyBallStartPos = gameObject.transform.position; //16%
                ShootBouncyBall();
            }
        }

        if (PickUpgrade.choseKunai == true)
        {
            int randomKunai = Random.Range(0, 100);
            if (randomKunai < 14 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease)
            {
                CursorMechanics.kunaiStartPos = gameObject.transform.position; //14%
                triggerKunai = true;
            }
        }

        if (PickUpgrade.choseBearTrap == true)
        {
            int randomBEarTRap = Random.Range(0, 100);
            if (randomBEarTRap < 13 + ActiveMechanics.cloverChanceAdd + PickUpgrade.totalChanceIncreaseLOW + MetaProgressionUpgrades.onSlime_CD_ChanceIncrease) //13%
            {
                GameObject bearTrap = ObjectPool.instance.GetBearTrapFromPool();
                bearTrap.transform.position = gameObject.transform.position;
            }
        }
    }
    #endregion


    #region shoot bouncy ball
    public void ShootBouncyBall()
    {
        GameObject bouncy = ObjectPool.instance.GetBouncyBallFromPool();
        bouncy.transform.position = gameObject.transform.position;
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
        SpawnSlimes.slimesSquished += 1;

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

        if(isTutoritalSlime == true) { yield return new WaitForSeconds(0.1f); }
        else { yield return new WaitForSeconds(1); }

        while (true)
        {
            float shootWait = 0f;
            float shootTime = time;

            while (true)
            {
                while (shootWait < shootTime)
                {
                    shootWait += Time.deltaTime;
                    yield return null;
                }

                shootWait = 0;

                StartCoroutine(ChargeBullet(shotSpeed, 0f));
                if(isYellowSlime_shooting == true) { StartCoroutine(ChargeBullet(shotSpeed, 0.2f)); }
                if (isPurpleSlime_shooting == true) { StartCoroutine(ChargeBullet(shotSpeed, 0.14f)); }
                if (isRedSlime_shooting == true) 
                { 
                    StartCoroutine(ChargeBullet(shotSpeed, 0.17f));
                    StartCoroutine(ChargeBullet(shotSpeed, 0.34f));
                }
            }
        }
    }

    IEnumerator ChargeBullet(float shootSpeed, float extraWaitTime)
    {
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

            GameObject bullet = ObjectPool.instance.GetEnemyBulletFromPool();
            bullet.transform.position = shootSpawnPos.transform.position;

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
                bullet.tag = "EnemyBullet";
                bullet.layer = 12;
            }

            float chargeWait = 0f;
            float chargeTime = 1.5f;

            while (chargeWait < chargeTime)
            {
                CheckBullet(bullet);

                bullet.transform.position = shootSpawnPos.transform.position;
                chargeWait += Time.deltaTime;

                bullet.transform.localScale = new Vector2(chargeWait / 2f, chargeWait / 2f);

                yield return null;
            }

            bullet.transform.position = shootSpawnPos.transform.position;

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
