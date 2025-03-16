using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    #region Regular slimes
    //Regular slimes
    [SerializeField] private GameObject slime1Prefab;
    private Queue<GameObject> slime1Pool = new Queue<GameObject>();
    [SerializeField] private int slime1PoolSize = 50;
    public static float slime1Size;

    [SerializeField] private GameObject regularBluePrefab;
    private Queue<GameObject> regularBluePool = new Queue<GameObject>();
    [SerializeField] private int regularBluePoolSize = 50;

    [SerializeField] private GameObject regularYellowPrefab;
    private Queue<GameObject> regularYellowPool = new Queue<GameObject>();
    [SerializeField] private int regularYellowPoolSize = 50;

    [SerializeField] private GameObject regularRedPrefab;
    private Queue<GameObject> regularRedPool = new Queue<GameObject>();
    [SerializeField] private int regularRedPoolSize = 50;

    [SerializeField] private GameObject regularPurplePrefab;
    private Queue<GameObject> regularPurplePool = new Queue<GameObject>();
    [SerializeField] private int regularPurplePoolSize = 50;
    #endregion


    #region fast slimes
    [SerializeField] private GameObject fastGreenPrefab;
    private Queue<GameObject> fastGreenPool = new Queue<GameObject>();
    [SerializeField] private int fastGreenPoolSize = 50;

    [SerializeField] private GameObject fastBluePrefab;
    private Queue<GameObject> fastBluePool = new Queue<GameObject>();
    [SerializeField] private int fastBluePoolSize = 50;

    [SerializeField] private GameObject fastYellowPrefab;
    private Queue<GameObject> fastYellowPool = new Queue<GameObject>();
    [SerializeField] private int fastYellowPoolSize = 50;

    [SerializeField] private GameObject fastRedPrefab;
    private Queue<GameObject> fastRedPool = new Queue<GameObject>();
    [SerializeField] private int fastRedPoolSize = 50;

    [SerializeField] private GameObject fastPurplePrefab;
    private Queue<GameObject> fastPurplePool = new Queue<GameObject>();
    [SerializeField] private int fastPurplePoolSize = 50;
    #endregion


    #region shooting slimes
    //Shooting slimes
    [SerializeField] private GameObject shootingGreenPrefab;
    private Queue<GameObject> shootingGreenPool = new Queue<GameObject>();
    [SerializeField] private int shootingGreenPoolSize = 50;

    [SerializeField] private GameObject blueShootingPrefab;
    private Queue<GameObject> blueShootingPool = new Queue<GameObject>();
    [SerializeField] private int blueShootingPoolSize = 50;

    [SerializeField] private GameObject shootingYellowPrefab;
    private Queue<GameObject> shootingYellowPool = new Queue<GameObject>();
    [SerializeField] private int shootingYellowPoolSize = 50;

    [SerializeField] private GameObject shootingRedPrefab;
    private Queue<GameObject> shootingRedPool = new Queue<GameObject>();
    [SerializeField] private int shootingRedPoolSize = 50;

    [SerializeField] private GameObject shootingPurplePrefab;
    private Queue<GameObject> shootingPurplePool = new Queue<GameObject>();
    [SerializeField] private int shootingPurplePoolSize = 50;
    #endregion


    #region big slimes
    [SerializeField] private GameObject bigGreenPrefab;
    private Queue<GameObject> bigGreenPool = new Queue<GameObject>();
    [SerializeField] private int bigGreenPoolSize = 50;

    [SerializeField] private GameObject bigBluePrefab;
    private Queue<GameObject> bigBluePool = new Queue<GameObject>();
    [SerializeField] private int bigBluePoolSize = 50;

    [SerializeField] private GameObject bigYellowPrefab;
    private Queue<GameObject> bigYellowPool = new Queue<GameObject>();
    [SerializeField] private int bigYellowPoolSize = 50;

    [SerializeField] private GameObject redBigPrefab;
    private Queue<GameObject> redBigPool = new Queue<GameObject>();
    [SerializeField] private int redBigPoolSize = 50;

    [SerializeField] private GameObject bigPurplePrefab;
    private Queue<GameObject> bigPurplePool = new Queue<GameObject>();
    [SerializeField] private int bigPurplePoolSize = 50;
    #endregion


    #region projectiles
    [SerializeField] private GameObject paperClipPrefab;
    private Queue<GameObject> paperClipPool = new Queue<GameObject>();
    [SerializeField] private int paperClipPoolSize = 50;
    public static float paperClipSize;
  
    [SerializeField] private GameObject laserPrefab;
    private Queue<GameObject> laserPool = new Queue<GameObject>();
    [SerializeField] private int laserPoolSize = 50;

    [SerializeField] private GameObject arrowPrefab;
    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    [SerializeField] private int arrowPoolSize = 50;

    [SerializeField] private GameObject scythePrefab;
    private Queue<GameObject> scythePool = new Queue<GameObject>();
    [SerializeField] private int scythePoolSize = 10;

    [SerializeField] private GameObject poisonDartPrefab;
    private Queue<GameObject> poisonDartPool = new Queue<GameObject>();
    [SerializeField] private int poisonDartPoolSize = 15;

    [SerializeField] private GameObject thornPrefab;
    private Queue<GameObject> thornPool = new Queue<GameObject>();
    [SerializeField] private int thornPoolSize = 40;

    [SerializeField] private GameObject boulderPrefab;
    private Queue<GameObject> boulderPool = new Queue<GameObject>();
    [SerializeField] private int boulderPoolSize = 10;

    [SerializeField] private GameObject bouncyBallPrefab;
    private Queue<GameObject> bouncyBallPool = new Queue<GameObject>();
    [SerializeField] private int bouncyBallPoolSize = 20;

    [SerializeField] private GameObject swordPrefab;
    private Queue<GameObject> swordPool = new Queue<GameObject>();
    [SerializeField] private int swordPoolSize = 5;

    [SerializeField] private GameObject meteorPrefab;
    private Queue<GameObject> meteorPool = new Queue<GameObject>();
    [SerializeField] private int meteorPoolSize = 5;

    [SerializeField] private GameObject staplePrefab;
    private Queue<GameObject> staplePool = new Queue<GameObject>();
    [SerializeField] private int staplePoolSize = 7;

    [SerializeField] private GameObject kunaiPrefab;
    private Queue<GameObject> kunaiPool = new Queue<GameObject>();
    [SerializeField] private int kunaiPoolSize = 20;

    [SerializeField] private GameObject antiSlimeBulletPrefab;
    private Queue<GameObject> antiSlimeBulletPool = new Queue<GameObject>();
    [SerializeField] private int antiSlimeBulletPoolSize = 50;

    [SerializeField] private GameObject frenzyProjectilePrefab;
    private Queue<GameObject> frenzyProjectilePool = new Queue<GameObject>();
    [SerializeField] private int frenzyProjectilePoolSize = 35;

    [SerializeField] private GameObject sawbladePrefab;
    private Queue<GameObject> sawbladePool = new Queue<GameObject>();
    [SerializeField] private int sawbladePoolSize = 20;

    [SerializeField] private GameObject katanaPrefab;
    private Queue<GameObject> katanaPool = new Queue<GameObject>();
    [SerializeField] private int katanaPoolSize = 15;

    [SerializeField] private GameObject nailPrefab;
    private Queue<GameObject> nailPool = new Queue<GameObject>();
    [SerializeField] private int nailPoolsize = 20;

    [SerializeField] private GameObject bearTrapPrefab;
    private Queue<GameObject> bearTrapPool = new Queue<GameObject>();
    [SerializeField] private int bearTrapPoolSize = 35;

    [SerializeField] private GameObject logPrefab;
    private Queue<GameObject> logPool = new Queue<GameObject>();
    [SerializeField] private int logPoolSize = 15;
    #endregion

    #region goo and other
    [SerializeField] private GameObject gooPrefab;
    private Queue<GameObject> gooPool = new Queue<GameObject>();
    [SerializeField] private int gooPoolSize = 50;

    [SerializeField] private GameObject blueGooPrefab;
    private Queue<GameObject> blueGooPool = new Queue<GameObject>();
    [SerializeField] private int blueGooPoolSize = 50;

    [SerializeField] private GameObject orangeGooPrefab;
    private Queue<GameObject> orangeGooPool = new Queue<GameObject>();
    [SerializeField] private int orangeGooPoolSize = 50;

    [SerializeField] private GameObject redGooPrefab;
    private Queue<GameObject> redGooPool = new Queue<GameObject>();
    [SerializeField] private int redGooPoolSize = 50;

    [SerializeField] private GameObject purpleGooPrefab;
    private Queue<GameObject> purpleGooPool = new Queue<GameObject>();
    [SerializeField] private int purpleGooPoolSize = 50;

    [SerializeField] private GameObject coinsPrefab;
    private Queue<GameObject> coinsPool = new Queue<GameObject>();
    [SerializeField] private int coinsPoolSize = 200;

    [SerializeField] private GameObject enemyBulletPrefab;
    private Queue<GameObject> enemyBulletPool = new Queue<GameObject>();
    [SerializeField] private int enemyBulletPoolSize = 50;

    [SerializeField] private GameObject shootFlashPrefab;
    private Queue<GameObject> shootFlashPool = new Queue<GameObject>();
    [SerializeField] private int shootFlashPoolSize = 20;

    [SerializeField] private GameObject skullPrefab;
    private Queue<GameObject> skullPool = new Queue<GameObject>();
    [SerializeField] private int skullPoolSize = 8;

    [SerializeField] private GameObject shadowPrefab;
    private Queue<GameObject> shadowPool = new Queue<GameObject>();
    [SerializeField] private int shadowPoolSize = 75;

    [SerializeField] private GameObject bulletHitParticlePrefab;
    private Queue<GameObject> bulletHitParticlePool = new Queue<GameObject>();
    [SerializeField] private int bulletHitParticlePoolSize = 20;
    #endregion

    #region texts
    //Texts
    [SerializeField] private TextMeshProUGUI textPrefab;
    private Queue<TextMeshProUGUI> textPool = new Queue<TextMeshProUGUI>();
    [SerializeField] private int textPoolSize = 200;

    [SerializeField] private TextMeshProUGUI damageTextPrefab;
    private Queue<TextMeshProUGUI> damageTextPool = new Queue<TextMeshProUGUI>();
    [SerializeField] private int damageTextPoolSize = 200;
    #endregion

    public float fastSize1, fastSize2;
    public float regularSize1, regularSize2;

    private void Awake()
    {
        fastSize1 = 0.33f;
        fastSize2 = 0.36f;

        regularSize1 = 0.37f;
        regularSize2 = 0.4f;

        paperClipSize = 0.39f;

        if (instance == null)
        {
            instance = this;
        }
    }

    public Transform slimeParent, projectileParent, coinsParent, textParent, damageTextParent, arrowParent, bulletsParent, gooParent;

    void Start()
    {
        //Regular slimes
        #region Regular green
        for (int i = 0; i < slime1PoolSize; i++)
        {
            GameObject slime1 = Instantiate(slime1Prefab);
            slime1.name = "Green Basic " + i;
            slime1Pool.Enqueue(slime1);
            slime1.SetActive(false);
            slime1.transform.SetParent(slimeParent);
            float randomSize = Random.Range(regularSize1, regularSize2);
            slime1.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Regular Blue
        for (int i = 0; i < regularBluePoolSize; i++)
        {
            GameObject regularBlue = Instantiate(regularBluePrefab);
            regularBlue.name = "RegularBlue " + i;
            regularBluePool.Enqueue(regularBlue);
            regularBlue.SetActive(false);
            regularBlue.transform.SetParent(slimeParent);
            float randomSize = Random.Range(regularSize1, regularSize2);
            regularBlue.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Regular Yellow
        for (int i = 0; i < regularYellowPoolSize; i++)
        {
            GameObject regularYellow = Instantiate(regularYellowPrefab);
            regularYellow.name = "RegularYellow " + i;
            regularYellowPool.Enqueue(regularYellow);
            regularYellow.SetActive(false);
            regularYellow.transform.SetParent(slimeParent);
            float randomSize = Random.Range(regularSize1, regularSize2);
            regularYellow.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Regular Red
        for (int i = 0; i < regularRedPoolSize; i++)
        {
            GameObject regularRed = Instantiate(regularRedPrefab);
            regularRed.name = "RegularRed " + i;
            regularRedPool.Enqueue(regularRed);
            regularRed.SetActive(false);
            regularRed.transform.SetParent(slimeParent);
            float randomSize = Random.Range(regularSize1, regularSize2);
            regularRed.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Regular Purple
        for (int i = 0; i < regularPurplePoolSize; i++)
        {
            GameObject regularPurple = Instantiate(regularPurplePrefab);
            regularPurple.name = "RegularPurple " + i;
            regularPurplePool.Enqueue(regularPurple);
            regularPurple.SetActive(false);
            regularPurple.transform.SetParent(slimeParent);
            float randomSize = Random.Range(regularSize1, regularSize2);
            regularPurple.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        //Fast slimes
        #region Fast Green
        for (int i = 0; i < fastGreenPoolSize; i++)
        {
            GameObject fastGreen = Instantiate(fastGreenPrefab);
            fastGreen.name = "FastGreen " + i;
            fastGreenPool.Enqueue(fastGreen);
            fastGreen.SetActive(false);
            fastGreen.transform.SetParent(slimeParent);
            float randomSize = Random.Range(fastSize1, fastSize2);
            fastGreen.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Fast Blue
        for (int i = 0; i < fastBluePoolSize; i++)
        {
            GameObject fastBlue = Instantiate(fastBluePrefab);
            fastBlue.name = "FastBlue " + i;
            fastBluePool.Enqueue(fastBlue);
            fastBlue.SetActive(false);
            fastBlue.transform.SetParent(slimeParent);
            float randomSize = Random.Range(fastSize1, fastSize2);
            fastBlue.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Fast Yellow
        for (int i = 0; i < fastYellowPoolSize; i++)
        {
            GameObject fastYellow = Instantiate(fastYellowPrefab);
            fastYellow.name = "FastYellow " + i;
            fastYellowPool.Enqueue(fastYellow);
            fastYellow.SetActive(false);
            fastYellow.transform.SetParent(slimeParent);
            float randomSize = Random.Range(fastSize1, fastSize2);
            fastYellow.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Fast Red
        for (int i = 0; i < fastRedPoolSize; i++)
        {
            GameObject fastRed = Instantiate(fastRedPrefab);
            fastRed.name = "FastRed " + i;
            fastRedPool.Enqueue(fastRed);
            fastRed.SetActive(false);
            fastRed.transform.SetParent(slimeParent);
            float randomSize = Random.Range(fastSize1, fastSize2);
            fastRed.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Fast Purple
        for (int i = 0; i < fastPurplePoolSize; i++)
        {
            GameObject fastPurple = Instantiate(fastPurplePrefab);
            fastPurple.name = "FastPurple " + i;
            fastPurplePool.Enqueue(fastPurple);
            fastPurple.SetActive(false);
            fastPurple.transform.SetParent(slimeParent);
            float randomSize = Random.Range(fastSize1, fastSize2);
            fastPurple.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        //Shooting slimes
        #region Shooting Green
        for (int i = 0; i < shootingGreenPoolSize; i++)
        {
            GameObject shootingGreen = Instantiate(shootingGreenPrefab);
            shootingGreen.name = "ShootingGreen " + i;
            shootingGreenPool.Enqueue(shootingGreen);
            shootingGreen.SetActive(false);
            shootingGreen.transform.SetParent(slimeParent);
            float randomSize = Random.Range(0.6f, 0.7f);
            shootingGreen.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Blue slime - shooting
        for (int i = 0; i < blueShootingPoolSize; i++)
        {
            GameObject blueShooting = Instantiate(blueShootingPrefab);
            blueShooting.name = "Blue Shooting " + i;
            blueShootingPool.Enqueue(blueShooting);
            blueShooting.SetActive(false);
            blueShooting.transform.SetParent(slimeParent);
            float randomSize = Random.Range(0.6f, 0.7f);
            blueShooting.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Shooting Yellow
        for (int i = 0; i < shootingYellowPoolSize; i++)
        {
            GameObject shootingYellow = Instantiate(shootingYellowPrefab);
            shootingYellow.name = "ShootingYellow " + i;
            shootingYellowPool.Enqueue(shootingYellow);
            shootingYellow.SetActive(false);
            shootingYellow.transform.SetParent(slimeParent);
            float randomSize = Random.Range(0.6f, 0.7f);
            shootingYellow.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Shooting Red
        for (int i = 0; i < shootingRedPoolSize; i++)
        {
            GameObject shootingRed = Instantiate(shootingRedPrefab);
            shootingRed.name = "ShootingRed " + i;
            shootingRedPool.Enqueue(shootingRed);
            shootingRed.SetActive(false);
            shootingRed.transform.SetParent(slimeParent);
            float randomSize = Random.Range(0.6f, 0.7f);
            shootingRed.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Shooting Purple
        for (int i = 0; i < shootingPurplePoolSize; i++)
        {
            GameObject shootingPurple = Instantiate(shootingPurplePrefab);
            shootingPurple.name = "ShootingPurple " + i;
            shootingPurplePool.Enqueue(shootingPurple);
            shootingPurple.SetActive(false);
            shootingPurple.transform.SetParent(slimeParent);
            float randomSize = Random.Range(0.6f, 0.7f);
            shootingPurple.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        //Big slimes
        #region Big Green
        for (int i = 0; i < bigGreenPoolSize; i++)
        {
            GameObject bigGreen = Instantiate(bigGreenPrefab);
            bigGreen.name = "BigGreen " + i;
            bigGreenPool.Enqueue(bigGreen);
            bigGreen.SetActive(false);
            bigGreen.transform.SetParent(slimeParent);
            float randomSize = Random.Range(1.1f, 1.2f);
            bigGreen.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Big Blue
        for (int i = 0; i < bigBluePoolSize; i++)
        {
            GameObject bigBlue = Instantiate(bigBluePrefab);
            bigBlue.name = "BigBlue " + i;
            bigBluePool.Enqueue(bigBlue);
            bigBlue.SetActive(false);
            bigBlue.transform.SetParent(slimeParent);
            float randomSize = Random.Range(1.15f, 1.25f);
            bigBlue.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Red Big
        for (int i = 0; i < redBigPoolSize; i++)
        {
            GameObject redBig = Instantiate(redBigPrefab);
            redBig.name = "RedBig " + i;
            redBigPool.Enqueue(redBig);
            redBig.SetActive(false);
            redBig.transform.SetParent(slimeParent);
            float randomSize = Random.Range(1.25f, 1.3f);
            redBig.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Big Yellow
        for (int i = 0; i < bigYellowPoolSize; i++)
        {
            GameObject bigYellow = Instantiate(bigYellowPrefab);
            bigYellow.name = "BigYellow " + i;
            bigYellowPool.Enqueue(bigYellow);
            bigYellow.SetActive(false);
            bigYellow.transform.SetParent(slimeParent);
            float randomSize = Random.Range(1.35f, 1.45f);
            bigYellow.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        #region Big Purple
        for (int i = 0; i < bigPurplePoolSize; i++)
        {
            GameObject bigPurple = Instantiate(bigPurplePrefab);
            bigPurple.name = "BigPurple " + i;
            bigPurplePool.Enqueue(bigPurple);
            bigPurple.SetActive(false);
            bigPurple.transform.SetParent(slimeParent);
            float randomSize = Random.Range(1.5f, 1.6f);
            bigPurple.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        #endregion

        //Projectiles and stuff
        #region Paper clip
        for (int i = 0; i < paperClipPoolSize; i++)
        {
            GameObject paperClip = Instantiate(paperClipPrefab);
            paperClipPool.Enqueue(paperClip);
            paperClip.SetActive(false);
            paperClip.transform.SetParent(projectileParent);
            paperClip.transform.localScale = new Vector3(paperClipSize, paperClipSize, paperClipSize);
        }
        #endregion

        #region Goo
        for (int i = 0; i < gooPoolSize; i++)
        {
            GameObject goo = Instantiate(gooPrefab);
            gooPool.Enqueue(goo);
            goo.SetActive(false);
            goo.transform.SetParent(gooParent);
        }
        #endregion

        #region Blue Goo
        for (int i = 0; i < blueGooPoolSize; i++)
        {
            GameObject blueGoo = Instantiate(blueGooPrefab);
            blueGooPool.Enqueue(blueGoo);
            blueGoo.SetActive(false);
            blueGoo.transform.SetParent(gooParent);
        }
        #endregion

        #region Orange Goo
        for (int i = 0; i < orangeGooPoolSize; i++)
        {
            GameObject orangeGoo = Instantiate(orangeGooPrefab);
            orangeGooPool.Enqueue(orangeGoo);
            orangeGoo.SetActive(false);
            orangeGoo.transform.SetParent(gooParent);
        }
        #endregion

        #region Red Goo
        for (int i = 0; i < redGooPoolSize; i++)
        {
            GameObject redGoo = Instantiate(redGooPrefab);
            redGooPool.Enqueue(redGoo);
            redGoo.SetActive(false);
            redGoo.transform.SetParent(gooParent);
        }
        #endregion

        #region Purple Goo
        for (int i = 0; i < purpleGooPoolSize; i++)
        {
            GameObject purpleGoo = Instantiate(purpleGooPrefab);
            purpleGooPool.Enqueue(purpleGoo);
            purpleGoo.SetActive(false);
            purpleGoo.transform.SetParent(gooParent);
        }
        #endregion

        #region Enemy Bullets
        for (int i = 0; i < enemyBulletPoolSize; i++)
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab);
            enemyBulletPool.Enqueue(enemyBullet);
            enemyBullet.SetActive(false);
            enemyBullet.transform.SetParent(bulletsParent);
        }
        #endregion

        #region Shoot Flash
        for (int i = 0; i < shootFlashPoolSize; i++)
        {
            GameObject shootFlash = Instantiate(shootFlashPrefab);
            shootFlashPool.Enqueue(shootFlash);
            shootFlash.SetActive(false);
            shootFlash.transform.SetParent(bulletsParent);
            shootFlash.transform.localScale = new Vector2(55f, 55f);
        }
        #endregion

        #region Coin
        for (int i = 0; i < coinsPoolSize; i++)
        {
            GameObject goo = Instantiate(coinsPrefab);
            coinsPool.Enqueue(goo);
            goo.SetActive(false);
            goo.transform.SetParent(coinsParent);
            goo.transform.localScale = new Vector3(9f, 9f, 9f);
        }
        #endregion

        #region arrow
        for (int i = 0; i < arrowPoolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrowPool.Enqueue(arrow);
            arrow.SetActive(false);
            arrow.transform.SetParent(arrowParent);
            arrow.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        }
        #endregion

        #region laser
        for (int i = 0; i < laserPoolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            laserPool.Enqueue(laser);
            laser.SetActive(false);
            laser.transform.SetParent(projectileParent);
            laser.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        }
        #endregion

        #region scythe
        for (int i = 0; i < scythePoolSize; i++)
        {
            GameObject scythe = Instantiate(scythePrefab);
            scythePool.Enqueue(scythe);
            scythe.SetActive(false);
            scythe.transform.SetParent(projectileParent);
            scythe.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        #endregion

        #region poison dart
        for (int i = 0; i < poisonDartPoolSize; i++)
        {
            GameObject poison = Instantiate(poisonDartPrefab);
            poisonDartPool.Enqueue(poison);
            poison.SetActive(false);
            poison.transform.SetParent(projectileParent);
            poison.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        #endregion

        #region Thorn
        for (int i = 0; i < thornPoolSize; i++)
        {
            GameObject thorn = Instantiate(thornPrefab);
            thornPool.Enqueue(thorn);
            thorn.SetActive(false);
            thorn.transform.SetParent(projectileParent);
            thorn.transform.localScale = new Vector3(0.34f, 0.34f, 0.34f);
        }
        #endregion

        #region boulder
        for (int i = 0; i < boulderPoolSize; i++)
        {
            GameObject boulder = Instantiate(boulderPrefab);
            boulderPool.Enqueue(boulder);
            boulder.SetActive(false);
            boulder.transform.SetParent(projectileParent);
            boulder.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        #endregion

        #region bouncy ball
        for (int i = 0; i < bouncyBallPoolSize; i++)
        {
            GameObject bouncyBall = Instantiate(bouncyBallPrefab);
            bouncyBallPool.Enqueue(bouncyBall);
            bouncyBall.SetActive(false);
            bouncyBall.transform.SetParent(projectileParent);
            bouncyBall.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        #endregion

        #region Sword
        for (int i = 0; i < swordPoolSize; i++)
        {
            GameObject sword = Instantiate(swordPrefab);
            swordPool.Enqueue(sword);
            sword.SetActive(false);
            sword.transform.SetParent(projectileParent);
            sword.transform.localScale = new Vector2(0.8f, 0.8f);
        }
        #endregion

        #region Meteor
        for (int i = 0; i < meteorPoolSize; i++)
        {
            GameObject meteor = Instantiate(meteorPrefab);
            meteorPool.Enqueue(meteor);
            meteor.SetActive(false);
            meteor.transform.SetParent(projectileParent); 
            meteor.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        #endregion

        #region Kunai
        for (int i = 0; i < kunaiPoolSize; i++)
        {
            GameObject kunai = Instantiate(kunaiPrefab);
            kunaiPool.Enqueue(kunai);
            kunai.SetActive(false);
            kunai.transform.SetParent(projectileParent);
            kunai.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        #endregion

        #region Staple
        for (int i = 0; i < staplePoolSize; i++)
        {
            GameObject staple = Instantiate(staplePrefab);
            staplePool.Enqueue(staple);
            staple.SetActive(false);
            staple.transform.SetParent(projectileParent);
            staple.transform.localScale = new Vector2(0.35f, 0.35f);
        }
        #endregion

        #region Anti Slime Bullet
        for (int i = 0; i < antiSlimeBulletPoolSize; i++)
        {
            GameObject antiSlimeBullet = Instantiate(antiSlimeBulletPrefab);
            antiSlimeBulletPool.Enqueue(antiSlimeBullet);
            antiSlimeBullet.SetActive(false);
            antiSlimeBullet.transform.SetParent(projectileParent);
            antiSlimeBullet.transform.localScale = new Vector2(1.12f, 1.12f);
        }
        #endregion

        #region Frenzy Projectile
        for (int i = 0; i < frenzyProjectilePoolSize; i++)
        {
            GameObject frenzyProjectile = Instantiate(frenzyProjectilePrefab);
            frenzyProjectilePool.Enqueue(frenzyProjectile);
            frenzyProjectile.SetActive(false);
            frenzyProjectile.transform.SetParent(projectileParent);
            frenzyProjectile.transform.localScale = new Vector2(0.85f, 0.85f);
        }
        #endregion

        #region Sawblade
        for (int i = 0; i < sawbladePoolSize; i++)
        {
            GameObject sawblade = Instantiate(sawbladePrefab);
            sawbladePool.Enqueue(sawblade);
            sawblade.SetActive(false);
            sawblade.transform.SetParent(projectileParent);
            sawblade.transform.localScale = new Vector2(1,1);
        }
        #endregion

        #region Katana
        for (int i = 0; i < katanaPoolSize; i++)
        {
            GameObject katana = Instantiate(katanaPrefab);
            katanaPool.Enqueue(katana);
            katana.SetActive(false);
            katana.transform.SetParent(projectileParent);
            katana.transform.localScale = new Vector2(1.3f,1.3f);
        }
        #endregion

        #region nail
        for (int i = 0; i < nailPoolsize; i++)
        {
            GameObject nail = Instantiate(nailPrefab);
            nailPool.Enqueue(nail);
            nail.SetActive(false);
            nail.transform.SetParent(projectileParent);
            nail.transform.localScale = new Vector2(0.77f, 0.77f);
        }
        #endregion

        #region Bear Trap
        for (int i = 0; i < bearTrapPoolSize; i++)
        {
            GameObject bearTrap = Instantiate(bearTrapPrefab);
            bearTrapPool.Enqueue(bearTrap);
            bearTrap.SetActive(false);
            bearTrap.transform.SetParent(projectileParent);
            bearTrap.transform.localScale = new Vector2(0.9f, 0.9f);
        }
        #endregion

        #region Log
        for (int i = 0; i < logPoolSize; i++)
        {
            GameObject log = Instantiate(logPrefab);
            logPool.Enqueue(log);
            log.SetActive(false);
            log.transform.SetParent(projectileParent);
            log.transform.localScale = new Vector2(2.3f, 2.3f);
        }
        #endregion

        //
        #region Skull
        for (int i = 0; i < skullPoolSize; i++)
        {
            GameObject skull = Instantiate(skullPrefab);
            skullPool.Enqueue(skull);
            skull.SetActive(false);
            skull.transform.SetParent(slimeParent);
            skull.transform.localScale = new Vector2(1f, 1f);
        }
        #endregion

        #region Shadow
        for (int i = 0; i < shadowPoolSize; i++)
        {
            GameObject shadow = Instantiate(shadowPrefab);
            shadow.name = "Shadow " + i;
            shadowPool.Enqueue(shadow);
            shadow.SetActive(false);
            shadow.transform.SetParent(slimeParent); 
        }
        #endregion

        #region Bullet Hit Particle
        for (int i = 0; i < bulletHitParticlePoolSize; i++)
        {
            GameObject bulletHitParticle = Instantiate(bulletHitParticlePrefab);
            bulletHitParticlePool.Enqueue(bulletHitParticle);
            bulletHitParticle.SetActive(false);
            bulletHitParticle.transform.SetParent(bulletsParent);
            bulletHitParticle.transform.localScale = new Vector2(0.0025f, 0.0025f);
        }
        #endregion

        //Texts
        #region Text
        for (int i = 0; i < textPoolSize; i++)
        {
            TextMeshProUGUI text = Instantiate(textPrefab);
            textPool.Enqueue(text);
            text.gameObject.SetActive(false);
            text.transform.SetParent(textParent);
            text.transform.localScale = new Vector2(0.9f, 0.9f);
        }
        #endregion

        #region Damage Text
        for (int i = 0; i < damageTextPoolSize; i++)
        {
            TextMeshProUGUI damageText = Instantiate(damageTextPrefab);
            damageTextPool.Enqueue(damageText);
            damageText.gameObject.SetActive(false);
            damageText.transform.SetParent(damageTextParent);
            damageText.transform.localScale = new Vector2(0.82f, 0.82f);
        }
        #endregion
    }

    //Regular slimes
    #region Regular green
    public GameObject GetSlime1FromPool()
    {
        if (slime1Pool.Count > 0)
        {
            GameObject slime1 = slime1Pool.Dequeue();
            slime1.SetActive(true);
            return slime1;
        }
        else
        {
            GameObject slime1 = Instantiate(slime1Prefab);
            return slime1;
        }
    }

    public void ReturnSlime1FromPool(GameObject slime1)
    {
        slime1Pool.Enqueue(slime1);
        slime1.SetActive(false);
    }
    #endregion

    #region Regular Blue
    public GameObject GetRegularBlueFromPool()
    {
        if (regularBluePool.Count > 0)
        {
            GameObject regularBlue = regularBluePool.Dequeue();
            regularBlue.SetActive(true);
            return regularBlue;
        }
        else
        {
            GameObject regularBlue = Instantiate(regularBluePrefab);
            return regularBlue;
        }
    }

    public void ReturnRegularBlueToPool(GameObject regularBlue)
    {
        regularBluePool.Enqueue(regularBlue);
        regularBlue.SetActive(false);
    }
    #endregion

    #region Regular Yellow
    public GameObject GetRegularYellowFromPool()
    {
        if (regularYellowPool.Count > 0)
        {
            GameObject regularYellow = regularYellowPool.Dequeue();
            regularYellow.SetActive(true);
            return regularYellow;
        }
        else
        {
            GameObject regularYellow = Instantiate(regularYellowPrefab);
            return regularYellow;
        }
    }

    public void ReturnRegularYellowToPool(GameObject regularYellow)
    {
        regularYellowPool.Enqueue(regularYellow);
        regularYellow.SetActive(false);
    }
    #endregion

    #region Regular Red
    public GameObject GetRegularRedFromPool()
    {
        if (regularRedPool.Count > 0)
        {
            GameObject regularRed = regularRedPool.Dequeue();
            regularRed.SetActive(true);
            return regularRed;
        }
        else
        {
            GameObject regularRed = Instantiate(regularRedPrefab);
            return regularRed;
        }
    }

    public void ReturnRegularRedToPool(GameObject regularRed)
    {
        regularRedPool.Enqueue(regularRed);
        regularRed.SetActive(false);
    }
    #endregion

    #region Regular Purple
    public GameObject GetRegularPurpleFromPool()
    {
        if (regularPurplePool.Count > 0)
        {
            GameObject regularPurple = regularPurplePool.Dequeue();
            regularPurple.SetActive(true);
            return regularPurple;
        }
        else
        {
            GameObject regularPurple = Instantiate(regularPurplePrefab);
            return regularPurple;
        }
    }

    public void ReturnRegularPurpleToPool(GameObject regularPurple)
    {
        regularPurplePool.Enqueue(regularPurple);
        regularPurple.SetActive(false);
    }
    #endregion

    //Fast green
    #region Fast Green
    public GameObject GetFastGreenFromPool()
    {
        if (fastGreenPool.Count > 0)
        {
            GameObject fastGreen = fastGreenPool.Dequeue();
            fastGreen.SetActive(true);
            return fastGreen;
        }
        else
        {
            GameObject fastGreen = Instantiate(fastGreenPrefab);
            return fastGreen;
        }
    }

    public void ReturnFastGreenToPool(GameObject fastGreen)
    {
        fastGreenPool.Enqueue(fastGreen);
        fastGreen.SetActive(false);
    }
    #endregion

    #region Fast Blue
    public GameObject GetFastBlueFromPool()
    {
        if (fastBluePool.Count > 0)
        {
            GameObject fastBlue = fastBluePool.Dequeue();
            fastBlue.SetActive(true);
            return fastBlue;
        }
        else
        {
            GameObject fastBlue = Instantiate(fastBluePrefab);
            return fastBlue;
        }
    }

    public void ReturnFastBlueToPool(GameObject fastBlue)
    {
        fastBluePool.Enqueue(fastBlue);
        fastBlue.SetActive(false);
    }
    #endregion

    #region Fast Yellow
    public GameObject GetFastYellowFromPool()
    {
        if (fastYellowPool.Count > 0)
        {
            GameObject fastYellow = fastYellowPool.Dequeue();
            fastYellow.SetActive(true);
            return fastYellow;
        }
        else
        {
            GameObject fastYellow = Instantiate(fastYellowPrefab);
            return fastYellow;
        }
    }

    public void ReturnFastYellowToPool(GameObject fastYellow)
    {
        fastYellowPool.Enqueue(fastYellow);
        fastYellow.SetActive(false);
    }
    #endregion

    #region Fast Red
    public GameObject GetFastRedFromPool()
    {
        if (fastRedPool.Count > 0)
        {
            GameObject fastRed = fastRedPool.Dequeue();
            fastRed.SetActive(true);
            return fastRed;
        }
        else
        {
            GameObject fastRed = Instantiate(fastRedPrefab);
            return fastRed;
        }
    }

    public void ReturnFastRedToPool(GameObject fastRed)
    {
        fastRedPool.Enqueue(fastRed);
        fastRed.SetActive(false);
    }
    #endregion

    #region Fast Purple
    public GameObject GetFastPurpleFromPool()
    {
        if (fastPurplePool.Count > 0)
        {
            GameObject fastPurple = fastPurplePool.Dequeue();
            fastPurple.SetActive(true);
            return fastPurple;
        }
        else
        {
            GameObject fastPurple = Instantiate(fastPurplePrefab);
            return fastPurple;
        }
    }

    public void ReturnFastPurpleToPool(GameObject fastPurple)
    {
        fastPurplePool.Enqueue(fastPurple);
        fastPurple.SetActive(false);
    }
    #endregion

    //Shooting slimes
    #region Shooting Green
    public GameObject GetShootingGreenFromPool()
    {
        if (shootingGreenPool.Count > 0)
        {
            GameObject shootingGreen = shootingGreenPool.Dequeue();
            shootingGreen.SetActive(true);
            return shootingGreen;
        }
        else
        {
            GameObject shootingGreen = Instantiate(shootingGreenPrefab);
            return shootingGreen;
        }
    }

    public void ReturnShootingGreenToPool(GameObject shootingGreen)
    {
        shootingGreenPool.Enqueue(shootingGreen);
        shootingGreen.SetActive(false);
    }
    #endregion

    #region Blue shooting slime
    public GameObject GetBlueShootingFromPool()
    {
        if (blueShootingPool.Count > 0)
        {
            GameObject blueShooting = blueShootingPool.Dequeue();
            blueShooting.SetActive(true);
            return blueShooting;
        }
        else
        {
            GameObject blueShooting = Instantiate(blueShootingPrefab);
            return blueShooting;
        }
    }

    public void ReturnBlueShootingFromPool(GameObject blueShooting)
    {
        blueShootingPool.Enqueue(blueShooting);
        blueShooting.SetActive(false);
    }
    #endregion

    #region Shooting Yellow
    public GameObject GetShootingYellowFromPool()
    {
        if (shootingYellowPool.Count > 0)
        {
            GameObject shootingYellow = shootingYellowPool.Dequeue();
            shootingYellow.SetActive(true);
            return shootingYellow;
        }
        else
        {
            GameObject shootingYellow = Instantiate(shootingYellowPrefab);
            return shootingYellow;
        }
    }

    public void ReturnShootingYellowToPool(GameObject shootingYellow)
    {
        shootingYellowPool.Enqueue(shootingYellow);
        shootingYellow.SetActive(false);
    }
    #endregion

    #region Shooting Red
    public GameObject GetShootingRedFromPool()
    {
        if (shootingRedPool.Count > 0)
        {
            GameObject shootingRed = shootingRedPool.Dequeue();
            shootingRed.SetActive(true);
            return shootingRed;
        }
        else
        {
            GameObject shootingRed = Instantiate(shootingRedPrefab);
            return shootingRed;
        }
    }

    public void ReturnShootingRedToPool(GameObject shootingRed)
    {
        shootingRedPool.Enqueue(shootingRed);
        shootingRed.SetActive(false);
    }
    #endregion

    #region Shooting Purple
    public GameObject GetShootingPurpleFromPool()
    {
        if (shootingPurplePool.Count > 0)
        {
            GameObject shootingPurple = shootingPurplePool.Dequeue();
            shootingPurple.SetActive(true);
            return shootingPurple;
        }
        else
        {
            GameObject shootingPurple = Instantiate(shootingPurplePrefab);
            return shootingPurple;
        }
    }

    public void ReturnShootingPurpleToPool(GameObject shootingPurple)
    {
        shootingPurplePool.Enqueue(shootingPurple);
        shootingPurple.SetActive(false);
    }
    #endregion

    //Big slimes
    #region Big Green
    public GameObject GetBigGreenFromPool()
    {
        if (bigGreenPool.Count > 0)
        {
            GameObject bigGreen = bigGreenPool.Dequeue();
            bigGreen.SetActive(true);
            return bigGreen;
        }
        else
        {
            GameObject bigGreen = Instantiate(bigGreenPrefab);
            return bigGreen;
        }
    }

    public void ReturnBigGreenToPool(GameObject bigGreen)
    {
        bigGreenPool.Enqueue(bigGreen);
        bigGreen.SetActive(false);
    }
    #endregion

    #region Big Blue
    public GameObject GetBigBlueFromPool()
    {
        if (bigBluePool.Count > 0)
        {
            GameObject bigBlue = bigBluePool.Dequeue();
            bigBlue.SetActive(true);
            return bigBlue;
        }
        else
        {
            GameObject bigBlue = Instantiate(bigBluePrefab);
            return bigBlue;
        }
    }

    public void ReturnBigBlueToPool(GameObject bigBlue)
    {
        bigBluePool.Enqueue(bigBlue);
        bigBlue.SetActive(false);
    }
    #endregion

    #region Red Big
    public GameObject GetRedBigFromPool()
    {
        if (redBigPool.Count > 0)
        {
            GameObject redBig = redBigPool.Dequeue();
            redBig.SetActive(true);
            return redBig;
        }
        else
        {
            GameObject redBig = Instantiate(redBigPrefab);
            return redBig;
        }
    }

    public void ReturnRedBigToPool(GameObject redBig)
    {
        redBigPool.Enqueue(redBig);
        redBig.SetActive(false);
    }
    #endregion

    #region Big Yellow
    public GameObject GetBigYellowFromPool()
    {
        if (bigYellowPool.Count > 0)
        {
            GameObject bigYellow = bigYellowPool.Dequeue();
            bigYellow.SetActive(true);
            return bigYellow;
        }
        else
        {
            GameObject bigYellow = Instantiate(bigYellowPrefab);
            return bigYellow;
        }
    }

    public void ReturnBigYellowToPool(GameObject bigYellow)
    {
        bigYellowPool.Enqueue(bigYellow);
        bigYellow.SetActive(false);
    }
    #endregion

    #region Big Purple
    public GameObject GetBigPurpleFromPool()
    {
        if (bigPurplePool.Count > 0)
        {
            GameObject bigPurple = bigPurplePool.Dequeue();
            bigPurple.SetActive(true);
            return bigPurple;
        }
        else
        {
            GameObject bigPurple = Instantiate(bigPurplePrefab);
            return bigPurple;
        }
    }

    public void ReturnBigPurpleToPool(GameObject bigPurple)
    {
        bigPurplePool.Enqueue(bigPurple);
        bigPurple.SetActive(false);
    }
    #endregion

    //other
    #region Paper clip
    public GameObject GetPaperClipFromPool()
    {
        if (paperClipPool.Count > 0)
        {
            GameObject paperClip = paperClipPool.Dequeue();
            paperClip.SetActive(true);
            return paperClip;
        }
        else
        {
            GameObject paperClip = Instantiate(paperClipPrefab);
            return paperClip;
        }
    }

    public void ReturnPaperClipFromPool(GameObject paperClip)
    {
        paperClipPool.Enqueue(paperClip);
        paperClip.SetActive(false);
    }
    #endregion

    #region Goo
    public GameObject GetGooFromPool()
    {
        if (gooPool.Count > 0)
        {
            GameObject goo = gooPool.Dequeue();
            goo.SetActive(true);
            return goo;
        }
        else
        {
            GameObject goo = Instantiate(gooPrefab);
            return goo;
        }
    }

    public void ReturnGooFromPool(GameObject goo)
    {
        gooPool.Enqueue(goo);
        goo.SetActive(false);
    }
    #endregion

    #region Blue Goo
    public GameObject GetBlueGooFromPool()
    {
        if (blueGooPool.Count > 0)
        {
            GameObject blueGoo = blueGooPool.Dequeue();
            blueGoo.SetActive(true);
            return blueGoo;
        }
        else
        {
            GameObject blueGoo = Instantiate(blueGooPrefab);
            return blueGoo;
        }
    }

    public void ReturnBlueGooToPool(GameObject blueGoo)
    {
        blueGooPool.Enqueue(blueGoo);
        blueGoo.SetActive(false);
    }
    #endregion

    #region Orange Goo
    public GameObject GetOrangeGooFromPool()
    {
        if (orangeGooPool.Count > 0)
        {
            GameObject orangeGoo = orangeGooPool.Dequeue();
            orangeGoo.SetActive(true);
            return orangeGoo;
        }
        else
        {
            GameObject orangeGoo = Instantiate(orangeGooPrefab);
            return orangeGoo;
        }
    }

    public void ReturnOrangeGooToPool(GameObject orangeGoo)
    {
        orangeGooPool.Enqueue(orangeGoo);
        orangeGoo.SetActive(false);
    }
    #endregion

    #region Red Goo
    public GameObject GetRedGooFromPool()
    {
        if (redGooPool.Count > 0)
        {
            GameObject redGoo = redGooPool.Dequeue();
            redGoo.SetActive(true);
            return redGoo;
        }
        else
        {
            GameObject redGoo = Instantiate(redGooPrefab);
            return redGoo;
        }
    }

    public void ReturnRedGooToPool(GameObject redGoo)
    {
        redGooPool.Enqueue(redGoo);
        redGoo.SetActive(false);
    }
    #endregion

    #region Purple Goo
    public GameObject GetPurpleGooFromPool()
    {
        if (purpleGooPool.Count > 0)
        {
            GameObject purpleGoo = purpleGooPool.Dequeue();
            purpleGoo.SetActive(true);
            return purpleGoo;
        }
        else
        {
            GameObject purpleGoo = Instantiate(purpleGooPrefab);
            return purpleGoo;
        }
    }

    public void ReturnPurpleGooToPool(GameObject purpleGoo)
    {
        purpleGooPool.Enqueue(purpleGoo);
        purpleGoo.SetActive(false);
    }
    #endregion

    #region Enemy bullet
    public GameObject GetEnemyBulletFromPool()
    {
        if (enemyBulletPool.Count > 0)
        {
            GameObject enemyBullet = enemyBulletPool.Dequeue();
            enemyBullet.SetActive(true);
            return enemyBullet;
        }
        else
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab);
            return enemyBullet;
        }
    }

    public void ReturnEnemyBulletFromPool(GameObject enemyBullet)
    {
        enemyBulletPool.Enqueue(enemyBullet);
        enemyBullet.SetActive(false);
    }
    #endregion

    #region Shoot Flash
    public GameObject GetShootFlashFromPool()
    {
        if (shootFlashPool.Count > 0)
        {
            GameObject shootFlash = shootFlashPool.Dequeue();
            shootFlash.SetActive(true);
            return shootFlash;
        }
        else
        {
            GameObject shootFlash = Instantiate(shootFlashPrefab);
            return shootFlash;
        }
    }

    public void ReturnShootFlashToPool(GameObject shootFlash)
    {
        shootFlashPool.Enqueue(shootFlash);
        shootFlash.SetActive(false);
    }
    #endregion

    #region Coin
    public GameObject GetCoinFromPool()
    {
        if (coinsPool.Count > 0)
        {
            GameObject coin = coinsPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            GameObject coin = Instantiate(coinsPrefab);
            return coin;
        }
    }

    public void ReturnCoinFromPool(GameObject coin)
    {
        coinsPool.Enqueue(coin);
        coin.SetActive(false);
    }
    #endregion

    #region Arrow
    public GameObject GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            GameObject arrow = arrowPool.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab);
            return arrow;
        }
    }

    public void ReturnArrowFrompool(GameObject arrow)
    {
        arrowPool.Enqueue(arrow);
        arrow.SetActive(false);
    }
    #endregion

    #region Laser
    public GameObject GetLaserFromPool()
    {
        if (laserPool.Count > 0)
        {
            GameObject laser = laserPool.Dequeue();
            laser.SetActive(true);
            return laser;
        }
        else
        {
            GameObject laser = Instantiate(laserPrefab);
            return laser;
        }
    }

    public void ReturnLaserFromPool(GameObject laser)
    {
        laserPool.Enqueue(laser);
        laser.SetActive(false);
    }
    #endregion

    #region Scynthe
    public GameObject GetScyntheFromPool()
    {
        if (scythePool.Count > 0)
        {
            GameObject scynthe = scythePool.Dequeue();
            scynthe.SetActive(true);
            return scynthe;
        }
        else
        {
            GameObject scynthe = Instantiate(scythePrefab);
            return scynthe;
        }
    }

    public void ReturnScyntheFromPool(GameObject scynthe)
    {
        scythePool.Enqueue(scynthe);
        scynthe.SetActive(false);
    }
    #endregion

    #region poison dart
    public GameObject GetPoisonDartFromPool()
    {
        if (poisonDartPool.Count > 0)
        {
            GameObject poison = poisonDartPool.Dequeue();
            poison.SetActive(true);
            return poison;
        }
        else
        {
            GameObject poison = Instantiate(poisonDartPrefab);
            return poison;
        }
    }

    public void ReturnPoisonDartFromPool(GameObject poison)
    {
        poisonDartPool.Enqueue(poison);
        poison.SetActive(false);
    }
    #endregion

    #region thorn
    public GameObject GetThornFromPool()
    {
        if (thornPool.Count > 0)
        {
            GameObject thorn = thornPool.Dequeue();
            thorn.SetActive(true);
            return thorn;
        }
        else
        {
            GameObject thorn = Instantiate(thornPrefab);
            return thorn;
        }
    }

    public void ReturnThornFromPool(GameObject thorn)
    {
        thornPool.Enqueue(thorn);
        thorn.SetActive(false);
    }
    #endregion

    #region boulder
    public GameObject GetBoulderFromPool()
    {
        if (boulderPool.Count > 0)
        {
            GameObject boulder = boulderPool.Dequeue();
            boulder.SetActive(true);
            return boulder;
        }
        else
        {
            GameObject boulder = Instantiate(boulderPrefab);
            return boulder;
        }
    }

    public void ReturnBoulderFromPool(GameObject boulder)
    {
        boulderPool.Enqueue(boulder);
        boulder.SetActive(false);
    }
    #endregion

    #region bouncy ball
    public GameObject GetBouncyBallFromPool()
    {
        if (bouncyBallPool.Count > 0)
        {
            GameObject bouncyBall = bouncyBallPool.Dequeue();
            bouncyBall.SetActive(true);
            return bouncyBall;
        }
        else
        {
            GameObject bouncyBall = Instantiate(bouncyBallPrefab);
            return bouncyBall;
        }
    }

    public void ReturnBouncyBallFromPool(GameObject bouncyBall)
    {
        bouncyBallPool.Enqueue(bouncyBall);
        bouncyBall.SetActive(false);
    }
    #endregion

    #region Sword
    public GameObject GetSwordFromPool()
    {
        if (swordPool.Count > 0)
        {
            GameObject sword = swordPool.Dequeue();
            sword.SetActive(true);
            return sword;
        }
        else
        {
            GameObject sword = Instantiate(swordPrefab);
            return sword;
        }
    }

    public void ReturnSwordToPool(GameObject sword)
    {
        swordPool.Enqueue(sword);
        sword.SetActive(false);
    }
    #endregion

    #region Meteor
    public GameObject GetMeteorFromPool()
    {
        if (meteorPool.Count > 0)
        {
            GameObject meteor = meteorPool.Dequeue();
            meteor.SetActive(true);
            return meteor;
        }
        else
        {
            GameObject meteor = Instantiate(meteorPrefab);
            return meteor;
        }
    }

    public void ReturnMeteorToPool(GameObject meteor)
    {
        meteorPool.Enqueue(meteor);
        meteor.SetActive(false);
    }
    #endregion

    #region Staple
    public GameObject GetStapleFromPool()
    {
        if (staplePool.Count > 0)
        {
            GameObject staple = staplePool.Dequeue();
            staple.SetActive(true);
            return staple;
        }
        else
        {
            GameObject staple = Instantiate(staplePrefab);
            return staple;
        }
    }

    public void ReturnStapleToPool(GameObject staple)
    {
        staplePool.Enqueue(staple);
        staple.SetActive(false);
    }
    #endregion

    #region Skull
    public GameObject GetSkullFromPool()
    {
        if (skullPool.Count > 0)
        {
            GameObject skull = skullPool.Dequeue();
            skull.SetActive(true);
            return skull;
        }
        else
        {
            GameObject skull = Instantiate(skullPrefab);
            return skull;
        }
    }

    public void ReturnSkullToPool(GameObject skull)
    {
        skullPool.Enqueue(skull);
        skull.SetActive(false);
    }
    #endregion

    #region Shadow
    public GameObject GetShadowFromPool()
    {
        if (shadowPool.Count > 0)
        {
            GameObject shadow = shadowPool.Dequeue();
            shadow.SetActive(true);
            return shadow;
        }
        else
        {
            GameObject shadow = Instantiate(shadowPrefab);
            return shadow;
        }
    }

    public void ReturnShadowToPool(GameObject shadow)
    {
        shadowPool.Enqueue(shadow);
        shadow.SetActive(false);
    }
    #endregion

    #region Bullet Hit Particle
    public GameObject GetBulletHitParticleFromPool()
    {
        if (bulletHitParticlePool.Count > 0)
        {
            GameObject bulletHitParticle = bulletHitParticlePool.Dequeue();
            bulletHitParticle.SetActive(true);
            return bulletHitParticle;
        }
        else
        {
            GameObject bulletHitParticle = Instantiate(bulletHitParticlePrefab);
            return bulletHitParticle;
        }
    }

    public void ReturnBulletHitParticleToPool(GameObject bulletHitParticle)
    {
        bulletHitParticlePool.Enqueue(bulletHitParticle);
        bulletHitParticle.SetActive(false);
    }
    #endregion

    #region Kunai
    public GameObject GetKunaiFromPool()
    {
        if (kunaiPool.Count > 0)
        {
            GameObject kunai = kunaiPool.Dequeue();
            kunai.SetActive(true);
            return kunai;
        }
        else
        {
            GameObject kunai = Instantiate(kunaiPrefab);
            return kunai;
        }
    }

    public void ReturnKunaiToPool(GameObject kunai)
    {
        kunaiPool.Enqueue(kunai);
        kunai.SetActive(false);
    }
    #endregion

    #region Anti Slime Bullet
    public GameObject GetAntiSlimeBulletFromPool()
    {
        if (antiSlimeBulletPool.Count > 0)
        {
            GameObject antiSlimeBullet = antiSlimeBulletPool.Dequeue();
            antiSlimeBullet.SetActive(true);
            return antiSlimeBullet;
        }
        else
        {
            GameObject antiSlimeBullet = Instantiate(antiSlimeBulletPrefab);
            return antiSlimeBullet;
        }
    }

    public void ReturnAntiSlimeBulletToPool(GameObject antiSlimeBullet)
    {
        antiSlimeBulletPool.Enqueue(antiSlimeBullet);
        antiSlimeBullet.SetActive(false);
    }
    #endregion

    #region Frenzy Projectile
    public GameObject GetFrenzyProjectileFromPool()
    {
        if (frenzyProjectilePool.Count > 0)
        {
            GameObject frenzyProjectile = frenzyProjectilePool.Dequeue();
            frenzyProjectile.SetActive(true);
            return frenzyProjectile;
        }
        else
        {
            GameObject frenzyProjectile = Instantiate(frenzyProjectilePrefab);
            return frenzyProjectile;
        }
    }

    public void ReturnFrenzyProjectileToPool(GameObject frenzyProjectile)
    {
        frenzyProjectilePool.Enqueue(frenzyProjectile);
        frenzyProjectile.SetActive(false);
    }
    #endregion

    #region Sawblade
    public GameObject GetSawbladeFromPool()
    {
        if (sawbladePool.Count > 0)
        {
            GameObject sawblade = sawbladePool.Dequeue();
            sawblade.SetActive(true);
            return sawblade;
        }
        else
        {
            GameObject sawblade = Instantiate(sawbladePrefab);
            return sawblade;
        }
    }

    public void ReturnSawbladeToPool(GameObject sawblade)
    {
        sawbladePool.Enqueue(sawblade);
        sawblade.SetActive(false);
    }
    #endregion

    #region Katana
    public GameObject GetKatanaFromPool()
    {
        if (katanaPool.Count > 0)
        {
            GameObject katana = katanaPool.Dequeue();
            katana.SetActive(true);
            return katana;
        }
        else
        {
            GameObject katana = Instantiate(katanaPrefab);
            return katana;
        }
    }

    public void ReturnKatanaToPool(GameObject katana)
    {
        katanaPool.Enqueue(katana);
        katana.SetActive(false);
    }
    #endregion

    #region Nail
    public GameObject GetNailFromPool()
    {
        if (nailPool.Count > 0)
        {
            GameObject nail = nailPool.Dequeue();
            nail.SetActive(true);
            return nail;
        }
        else
        {
            GameObject nail = Instantiate(nailPrefab);
            return nail;
        }
    }

    public void ReturnNailFromPool(GameObject nail)
    {
        nailPool.Enqueue(nail);
        nail.SetActive(false);
    }
    #endregion

    #region Bear Trap
    public GameObject GetBearTrapFromPool()
    {
        if (bearTrapPool.Count > 0)
        {
            GameObject bearTrap = bearTrapPool.Dequeue();
            bearTrap.SetActive(true);
            return bearTrap;
        }
        else
        {
            GameObject bearTrap = Instantiate(bearTrapPrefab);
            return bearTrap;
        }
    }

    public void ReturnBearTrapToPool(GameObject bearTrap)
    {
        bearTrapPool.Enqueue(bearTrap);
        bearTrap.SetActive(false);
    }
    #endregion

    #region Log
    public GameObject GetLogFromPool()
    {
        if (logPool.Count > 0)
        {
            GameObject log = logPool.Dequeue();
            log.SetActive(true);
            return log;
        }
        else
        {
            GameObject log = Instantiate(logPrefab);
            return log;
        }
    }

    public void ReturnLogToPool(GameObject log)
    {
        logPool.Enqueue(log);
        log.SetActive(false);
    }
    #endregion

    //Texts
    #region Text
    public TextMeshProUGUI GetTextFromPool()
    {
        if (textPool.Count > 0)
        {
            TextMeshProUGUI text = textPool.Dequeue();
            text.gameObject.SetActive(true);
            return text;
        }
        else
        {
            TextMeshProUGUI text = Instantiate(textPrefab);
            return text;
        }
    }

    public void ReturnTextFromPool(TextMeshProUGUI text)
    {
        textPool.Enqueue(text);
        text.gameObject.SetActive(false);
    }
    #endregion

    #region Damage Text
    public TextMeshProUGUI GetDamageTextFromPool()
    {
        if (damageTextPool.Count > 0)
        {
            TextMeshProUGUI damageText = damageTextPool.Dequeue();
            damageText.gameObject.SetActive(true);
            return damageText;
        }
        else
        {
            TextMeshProUGUI damageText = Instantiate(damageTextPrefab);
            return damageText;
        }
    }

    public void ReturnDamageTextFromPool(TextMeshProUGUI damageText)
    {
        damageTextPool.Enqueue(damageText);
        damageText.gameObject.SetActive(false);
    }
    #endregion
}
