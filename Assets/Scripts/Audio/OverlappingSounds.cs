using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappingSounds : MonoBehaviour
{
    public AudioSource slimeSquishSource, slimeDamagedSource, arrowHitSource, scytheSource, blockBulletSource, frenzySource, bearTrapSource;
    public AudioClip slimeDamagedSound, slimeDamagedSound2, slimeDamagedSound3, arrowHitSound;
    public AudioClip slimeSquishSound1, slimeSquishSound2, slimeSquishSound3, slimeSquishSound4, slimeSquishSound5;
    public AudioClip scytheSound;
    public AudioClip blockBulletSound;
    public AudioClip frenzySound;
    public AudioClip bearTrapSound;

    public int totalSounds, soundsPlaying;

    public static float theCurrentTime;

    public float minDamageVolume, maxDamageVolume, minDamagePitch, maxDamagePitch;

    private void Awake()
    {
        totalSounds = 20;
    }

    public void PlaySound(int soundNumber, float currentTime, bool isDeathOrDamage)
    {
        // Debug.Log(soundsPlaying);

        if(isDeathOrDamage == true)
        {
            if (theCurrentTime == currentTime)
            {
                //Debug.Log("Same time " + currentTime);
                return;
            }

            theCurrentTime = currentTime;
        }


        if (soundsPlaying >= totalSounds)
        {
            return;
        }

        soundsPlaying += 1;

        AudioClip clipToPlay = null;

        if (soundNumber == 1)
        {
            //Slime squish
            float randomPitch = Random.Range(0.78f, 1.12f);
            float randomVolume = Random.Range(0.67f, 0.75f);

            slimeSquishSource.pitch = randomPitch;
            slimeSquishSource.volume = randomVolume;

            int random = Random.Range(1,3);

            if (random == 1) { clipToPlay = slimeSquishSound1; }
            if (random == 2) { clipToPlay = slimeSquishSound2; }

            slimeSquishSource.PlayOneShot(clipToPlay);
        }

        if (soundNumber == 2)
        {
            //Slime damaged
            float randomPitch = Random.Range(minDamagePitch, maxDamagePitch);
            float randomVolume = Random.Range(minDamageVolume, maxDamageVolume);

            slimeDamagedSource.pitch = randomPitch;
            slimeDamagedSource.volume = randomVolume;

            int random = Random.Range(1, 4);

            clipToPlay = slimeDamagedSound;

            slimeDamagedSource.PlayOneShot(clipToPlay);
        }

        if (soundNumber == 3)
        {
            float randomPitch = Random.Range(1.1f, 1.5f);
            float randomVolume = Random.Range(0.65f, 0.75f);
            arrowHitSource.pitch = randomPitch;
            arrowHitSource.volume = randomVolume;

            clipToPlay = arrowHitSound;

            arrowHitSource.PlayOneShot(clipToPlay);
        }

        if (soundNumber == 4)
        {
            float randomPitch = Random.Range(0.9f, 1.05f);
            scytheSource.pitch = randomPitch;
            clipToPlay = scytheSound;

            scytheSource.PlayOneShot(clipToPlay);
        }

        if (soundNumber == 5)
        {
            float randomPitch = Random.Range(0.9f, 1.05f);

            clipToPlay = blockBulletSound;

            blockBulletSource.PlayOneShot(clipToPlay);
        }

        if (soundNumber == 6)
        {
            float randomPitch = Random.Range(0.8f, 1.15f);

            clipToPlay = frenzySound;
            frenzySource.pitch = randomPitch;
            frenzySource.PlayOneShot(clipToPlay);
        }

        if (soundNumber == 7)
        {
            float randomPitch = Random.Range(0.9f, 1.1f);

            clipToPlay = bearTrapSound;
            bearTrapSource.pitch = randomPitch;
            bearTrapSource.PlayOneShot(clipToPlay);
        }

        StartCoroutine(WaitForSoundToFinish(clipToPlay.length));
    }


    private IEnumerator WaitForSoundToFinish(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        soundsPlaying -= 1;
    }
}
