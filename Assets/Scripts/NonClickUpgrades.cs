using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonClickUpgrades : MonoBehaviour
{
    public Transform laserStartPos, staplerStartPos, nailStartPos;
    public Vector2 laserGunDirection, staplerDirection, nailDirection;
    public GameObject laserGun, staplerGun, nailGun;
    public GameObject THE_laserGun, THE_stapler, THE_Nail, THE_BigLaser;

    public AudioManager audioManager;
    private void Start()
    {
        if (MainMenu.isTesting == false)
        {
            startLaserGun = true;
        }
    }

    public static Coroutine laserCoroutine, staplerCorotuine, nailCoroutine;
    public static bool startLaserGun;

    public static Vector2 laserGunStartPos, staplerGunStartPos, bigLaserGunStartPos, nailGunStartPos;

    private void Update()
    {
        laserGunDirection = laserGun.transform.up;
        staplerDirection = staplerGun.transform.up;
        nailDirection = nailGun.transform.up;

        if (startLaserGun == true)
        {
            startLaserGun = false;
            if(laserCoroutine == null) { laserCoroutine = StartCoroutine(ChargeLaser()); }
            else { StopCoroutine(laserCoroutine); laserCoroutine = StartCoroutine(ChargeLaser()); }
        }

        if (PickUpgrade.startStapler == true)
        {
            PickUpgrade.startStapler = false;
            if (staplerCorotuine == null) { staplerCorotuine = StartCoroutine(ChargeStapler()); }
            else { StopCoroutine(staplerCorotuine); staplerCorotuine = StartCoroutine(ChargeStapler()); }
        }

        if (PickUpgrade.startNailGun == true)
        {
            PickUpgrade.startNailGun = false;
            if (nailCoroutine == null) { nailCoroutine = StartCoroutine(ChargeNail()); }
            else { StopCoroutine(nailCoroutine); nailCoroutine = StartCoroutine(ChargeNail()); }
        }

        if (PickUpgrade.startBigLaser == true)
        {
            bigLaserCollider.enabled = false;
            PickUpgrade.startBigLaser = false;
            if (bigLaserCoroutine == null) { bigLaserCoroutine = StartCoroutine(BigLaserCountdown()); }
            else { StopCoroutine(bigLaserCoroutine); bigLaserCoroutine = StartCoroutine(BigLaserCountdown()); }
        }
    }

    #region Shoot laser
    public ParticleSystem particleLaser;

    public void ShootLaser()
    {
        if (PickUpgrade.isInChooseUpgrade == false && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
        {
            int pos = 0;
            bool isXPos = false;
      
            if (laserGunStartPos.x != 0) { pos = (int)laserGunStartPos.x; isXPos = true; }
            else { pos = (int)laserGunStartPos.y; isXPos = false; }

            int endPos = 0;
            if (laserGunStartPos.x < 0 || laserGunStartPos.y < 0) { endPos = pos + 10; }
            else if (laserGunStartPos.x > 0 || laserGunStartPos.y > 0) { endPos = pos - 10; }

            StartCoroutine(ShootAnimAndParticle(isXPos, THE_laserGun, pos, endPos, false));

            particleLaser.Play();

            audioManager.Play("Laser");
            ShootTheLaser(laserStartPos.transform.position);
            int random2XLaser = Random.Range(0, 100);
            if(random2XLaser < PickUpgrade.laser2XChance)
            {
                StartCoroutine(Shoot2XLaser());
            }
        }

        StartCoroutine(ChargeLaser());
    }

    public void ShootTheLaser(Vector2 pos)
    {
        GameObject laser = ObjectPool.instance.GetLaserFromPool();
        laser.transform.position = pos;
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();

        GameObject shadow = ObjectPool.instance.GetShadowFromPool();
        shadow.transform.position = new Vector2(pos.x, pos.y - 0.15f);
        shadow.transform.localScale = new Vector2(0.4f, 0.4f);
        Rigidbody2D rb2 = shadow.GetComponent<Rigidbody2D>();

        rb.velocity = laserGunDirection * 15f; rb2.velocity = laserGunDirection * 15f;

        float laserRotation = Mathf.Atan2(laserGunDirection.y, laserGunDirection.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, laserRotation);
    }

    IEnumerator Shoot2XLaser()
    {
        yield return new WaitForSeconds(0.15f);
        audioManager.Play("Laser");
        ShootTheLaser(laserStartPos.transform.position);
    }

    IEnumerator ChargeLaser()
    {
        yield return new WaitForSeconds(PickUpgrade.laserGunTime);
        laserCoroutine = null;
        ShootLaser();
    }
    #endregion

    #region Shoot Stapler
    public ParticleSystem particleStapler;

    public void ShootStapler()
    {
        if (PickUpgrade.isInChooseUpgrade == false && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
        {
            ShootTheStapler();
        }

        StartCoroutine(ChargeStapler());
    }

    public void ShootTheStapler()
    {
        int pos = 0;
        bool isXPos = false;

        if (staplerGunStartPos.x != 0) { pos = (int)staplerGunStartPos.x; isXPos = true; }
        else { pos = (int)staplerGunStartPos.y; isXPos = false; }

        int endPos = 0;
        if (staplerGunStartPos.x < 0 || staplerGunStartPos.y < 0) { endPos = pos + 10; }
        else if (staplerGunStartPos.x > 0 || staplerGunStartPos.y > 0) { endPos = pos - 10; }

        StartCoroutine(ShootAnimAndParticle(isXPos, THE_stapler, pos, endPos, false));

        audioManager.Play("Staple1");
        particleStapler.Play();

        StapleShoot(staplerStartPos.transform.position);
    }

    public void StapleShoot(Vector2 pos)
    {
        GameObject stapler = ObjectPool.instance.GetStapleFromPool();
        stapler.transform.position = pos;
        Rigidbody2D rb = stapler.GetComponent<Rigidbody2D>();
        rb.velocity = staplerDirection * 12f;

        float staplerRotation = Mathf.Atan2(staplerDirection.y, staplerDirection.x) * Mathf.Rad2Deg;
        stapler.transform.rotation = Quaternion.Euler(0, 0, staplerRotation);

        GameObject shadow = ObjectPool.instance.GetShadowFromPool();

        shadow.transform.position = new Vector2(pos.x, pos.y - 0.17f);
        shadow.transform.localScale = new Vector2(0.4f, 0.4f);

        Rigidbody2D shadowRb = shadow.GetComponent<Rigidbody2D>();
        shadowRb.velocity = staplerDirection * 12f;
    }

    IEnumerator ChargeStapler()
    {
        yield return new WaitForSeconds(PickUpgrade.staplerTimer);
        staplerCorotuine = null;
        ShootStapler();
    }
    #endregion

    #region Shoot Nail
    public ParticleSystem particleNail;

    public void ShootNail()
    {
        if (PickUpgrade.isInChooseUpgrade == false && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
        {
            ShootTheNail();
        }

        StartCoroutine(ChargeNail());
    }

    public void ShootTheNail()
    {
        int pos = 0;
        bool isXPos = false;

        if (nailGunStartPos.x != 0) { pos = (int)nailGunStartPos.x; isXPos = true; }
        else { pos = (int)nailGunStartPos.y; isXPos = false; }

        int endPos = 0;
        if (nailGunStartPos.x < 0 || nailGunStartPos.y < 0) { endPos = pos + 10; }
        else if (nailGunStartPos.x > 0 || nailGunStartPos.y > 0) { endPos = pos - 10; }

        StartCoroutine(ShootAnimAndParticle(isXPos, THE_Nail, pos, endPos, false));
        
        particleNail.Play();

        NailShoot(nailStartPos.transform.position);
    }

    public void NailShoot(Vector2 pos)
    {
        audioManager.Play("NailGun");
        GameObject nail = ObjectPool.instance.GetNailFromPool();
        nail.transform.position = pos;
        Rigidbody2D rb = nail.GetComponent<Rigidbody2D>();
        rb.velocity = nailDirection * 12f;

        float nailRotation = Mathf.Atan2(nailDirection.y, nailDirection.x) * Mathf.Rad2Deg;
        nail.transform.rotation = Quaternion.Euler(0, 0, nailRotation);

        GameObject shadow = ObjectPool.instance.GetShadowFromPool();

        shadow.transform.position = new Vector2(pos.x, pos.y - 0.16f);
        shadow.transform.localScale = new Vector2(0.4f, 0.4f);

        Rigidbody2D shadowRb = shadow.GetComponent<Rigidbody2D>();
        shadowRb.velocity = nailDirection * 12f;
    }

    IEnumerator ChargeNail()
    {
        yield return new WaitForSeconds(PickUpgrade.nailGunTimer);
        nailCoroutine = null;
        ShootNail();
    }
    #endregion

    #region Shoot big laser
    public Coroutine bigLaserCoroutine;
    public GameObject bigLaserDeleteBullets;

    IEnumerator BigLaserCountdown()
    {
        yield return new WaitForSeconds(PickUpgrade.bigLaserTimer);
        ShootBigLaser();
    }

    public void ShootBigLaser()
    {
        if (PickUpgrade.isInChooseUpgrade == false && PickUpgrade.isInWonRunScene == false && StrawberryMechanics.isInDeathFrame == false)
        {
            int pos = 0;
            bool isXPos = false;

            if (bigLaserGunStartPos.x != 0) { pos = (int)bigLaserGunStartPos.x; isXPos = true; }
            else { pos = (int)bigLaserGunStartPos.y; isXPos = false; }

            int endPos = 0;
            if (bigLaserGunStartPos.x < 0 || bigLaserGunStartPos.y < 0) { endPos = pos + 10; }
            else if (bigLaserGunStartPos.x > 0 || bigLaserGunStartPos.y > 0) { endPos = pos - 10; }

            StartCoroutine(ShootAnimAndParticle(isXPos, THE_BigLaser, pos, endPos, true));

            StartCoroutine(SetBigLaserOff());
        }

        StartCoroutine(BigLaserCountdown());
    }

    public Animation bigLaserAnim;
    public Collider2D bigLaserCollider;

    IEnumerator SetBigLaserOff()
    {
        audioManager.Play("LaserCharge");

        bigLaserDeleteBullets.SetActive(false);
        bigLaserAnim.Play("BigLaserAnim");
        yield return new WaitForSeconds(.3f);
        bigLaserDeleteBullets.SetActive(true);
        bigLaserCollider.enabled = true;
        yield return new WaitForSeconds(1.7f);
        bigLaserAnim.Play("BigLaserOff");
        yield return new WaitForSeconds(0.14f);
        bigLaserCollider.enabled = false;
        bigLaserDeleteBullets.SetActive(false);
    }
    #endregion

    #region Shoot animation
    public IEnumerator ShootAnimAndParticle(bool xAxis, GameObject objectToMove, float startPos, float endPos, bool isBigLaser)
    {
        float duration = 0.24f; // Total duration for the full cycle
        float halfDuration = duration / 2f; // Half for each direction
        float elapsedTime = 0f;

        if (isBigLaser == true)
        {
            duration = 0.5f;
            halfDuration = 0.25f;
        }

        // Move from startPos to endPos
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration; // Normalized time
            float newX = Mathf.Lerp(startPos, endPos, t);

            if (xAxis == true) { objectToMove.transform.localPosition = new Vector3(newX, objectToMove.transform.localPosition.y, objectToMove.transform.localPosition.z); }
            else { objectToMove.transform.localPosition = new Vector3(objectToMove.transform.localPosition.x, newX, objectToMove.transform.localPosition.z); }

            yield return null;
        }

        // Reset elapsed time for reverse movement
        elapsedTime = 0f;

        if(isBigLaser == true)
        {
            yield return new WaitForSeconds(2f);
        }

        // Move from endPos back to startPos
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            float newX = Mathf.Lerp(endPos, startPos, t);

            if (xAxis == true) { objectToMove.transform.localPosition = new Vector3(newX, objectToMove.transform.localPosition.y, objectToMove.transform.localPosition.z); }
            else { objectToMove.transform.localPosition = new Vector3(objectToMove.transform.localPosition.x, newX, objectToMove.transform.localPosition.z); }

            yield return null;
        }
    }
    #endregion

    public void Reset()
    {
        laserCoroutine = null;
        bigLaserCoroutine = null;
        staplerCorotuine = null;
        nailCoroutine = null;
        StopAllCoroutines();
    }
}
