using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSlimes : MonoBehaviour, IDataPersistence
{
    public static int slimesSquished;
    public static int slimesWaveSpawnCount;

    public static bool isWaveCompleted;

    public static int slimeWave;
    public static float waveTime;

    public static float currentWaveTotalTime;

    public static int greenRegular_spawnCount, blueRegular_spawnCount, yellowRegular_spawnCount, redRegular_spawnCount, purpleRegular_spawnCount;
    public static int greenShooting_spawnCount, blueShooting_spawnCount, yellowShooting_spawnCount, redShooting_spawnCount, purpleShooting_spawnCount;
    public static int greenFast_spawnCount, blueFast_spawnCount, yellowFast_spawnCount, redFast_spawnCount, purpleFast_spawnCount;
    public static int greenBig_spawnCount, blueBig_spawnCount, yellowBig_spawnCount, redBig_spawnCount, purpleBig_spawnCount;

    public static bool allSlimesSpawned;
    public static int slimesSpawned;

    //Slime shoot speed and time
    public static float greenShooting_shotTimer, greenShooting_shotSpeed;
    public static float blueShooting_shotTimer, blueShooting_shotSpeed;
    public static float yellowShooting_shotTimer, yellowShooting_shotSpeed;
    public static float redShooting_shotTimer, redShooting_shotSpeed;
    public static float purpleShooting_shotTimer, purpleShooting_shotSpeed;

    //Slime health
    public static float greenRegular_health, blueRegular_health, yellowRegular_health, redRegular_health, purpleRegular_health;
    public static float greenFast_health, blueFast_health, yellowFast_health, redFast_health, purpleFast_health;
    public static float greenShooting_health, blueShooting_health, yellowShooting_health, redShooting_health, purpleShooting_health;
    public static float greenBig_health, blueBig_health, yellowBig_health, redBig_health, purpleBig_health;

    public static bool isPlayingRun;

    void Start()
    {
        isInGame = false;

        greenRegular_health = 10; blueRegular_health = 20; yellowRegular_health = 30; redRegular_health = 35; purpleRegular_health = 48;
        greenFast_health = 5; blueFast_health = 12; yellowFast_health = 15; redFast_health = 26; purpleFast_health = 32;
        greenShooting_health = 15; blueShooting_health = 25; yellowShooting_health = 35; redShooting_health = 40; purpleShooting_health = 55;
        greenBig_health = 40; blueBig_health = 65; yellowBig_health = 80; redBig_health = 100; purpleBig_health = 150;

        //Shooting slime timers
        greenShooting_shotTimer = 4.2f; greenShooting_shotSpeed = 1.3f;
        blueShooting_shotTimer = 3.1f; blueShooting_shotSpeed = 1.7f;
        yellowShooting_shotTimer = 2.68f; yellowShooting_shotSpeed = 2.2f;
        redShooting_shotTimer = 1.9f; redShooting_shotSpeed = 2.3f;
        purpleShooting_shotTimer = 2.1f; purpleShooting_shotSpeed = 2.7f;

        //Testing
        //greenRegular_health = 10; blueRegular_health = 10; yellowRegular_health = 10; redRegular_health = 10; purpleRegular_health = 10;
        //greenFast_health = 10; blueFast_health = 10; yellowFast_health = 10; redFast_health = 10; purpleFast_health = 10;
        //greenShooting_health = 10; blueShooting_health = 10; yellowShooting_health = 10; redShooting_health = 10; purpleShooting_health = 10;
        //greenBig_health = 10; blueBig_health = 10; yellowBig_health = 10; redBig_health = 10; purpleBig_health = 10;

        if (MainMenu.isTesting == false)
        {
            StartEasyGameModeWave();
        }
    }

    private void Update()
    {
        if(SelectGameMode.choseRampage == true)
        {
            timerText.gameObject.SetActive(true);
            waveText.gameObject.SetActive(false);
        }
        else
        {
            timerText.gameObject.SetActive(false);
            waveText.gameObject.SetActive(true);
        }
    }

    #region Reset
    public void ResetCurrentRun()
    {
        timerText.gameObject.SetActive(false);
        StopAllCoroutines();

        greenRegular_spawnCount = 0; blueRegular_spawnCount = 0; yellowRegular_spawnCount = 0; redRegular_spawnCount = 0; purpleRegular_spawnCount = 0;
        greenShooting_spawnCount = 0; blueShooting_spawnCount = 0; yellowShooting_spawnCount = 0; redShooting_spawnCount = 0; purpleShooting_spawnCount = 0;
        greenFast_spawnCount = 0; blueFast_spawnCount = 0; yellowFast_spawnCount = 0; redFast_spawnCount = 0; purpleFast_spawnCount = 0;
        greenBig_spawnCount = 0; blueBig_spawnCount = 0; yellowBig_spawnCount = 0; redBig_spawnCount = 0; purpleBig_spawnCount = 0;

        slimeWave = 0;
        slimesWaveSpawnCount = 0;
        slimesSquished = 0;

        isGreenBasicSpawning = false;
        isBlueRegularSpawning = false;
        isYellowRegularSpawning = false;
        isRedRegularSpawning = false;
        isPurpleRegularSpawning = false;

        isGreenFastSpawning = false;
        isBlueFastSpawning = false;
        isYellowFastSpawning = false;
        isRedFastSpawning = false;
        isPurpleFastSpawning = false;

        isGreenShootingSpawning = false;
        blueShootingSpawning = false;
        isYellowShootingSpawning = false;
        isRedShootingSpawning = false;
        isPurpleShootingSpawning = false;

        isGreenBigSpawning = false;
        isBlueBigSpawning = false;
        isYellowBigSpawning = false;
        redBigSpawning = false;
        isPurpleBigSpawning = false;
    }
    #endregion

    public static bool isInGame;

    #region Set all spawn count to 0
    public void SetSpawnCountToZero()
    {
        greenRegular_spawnCount = 0; blueRegular_spawnCount = 0; yellowRegular_spawnCount = 0; redRegular_spawnCount = 0; purpleRegular_spawnCount = 0;
        greenShooting_spawnCount = 0; blueShooting_spawnCount = 0; yellowShooting_spawnCount = 0; redShooting_spawnCount = 0; purpleShooting_spawnCount = 0;
        greenFast_spawnCount = 0; blueFast_spawnCount = 0; yellowFast_spawnCount = 0; redFast_spawnCount = 0; purpleFast_spawnCount = 0;
        greenBig_spawnCount = 0; blueBig_spawnCount = 0; yellowBig_spawnCount = 0; redBig_spawnCount = 0; purpleBig_spawnCount = 0;

        greenBasicSpawned = 0; blueRegularSpawned = 0; yellowRegularSpawned = 0; redRegularSpawned = 0; purpleRegularSpawned = 0;
        greenFastSpawned = 0; blueFastSpawned = 0; yellowFastSpawned = 0; redFastSpawned = 0; purpleFastSpawned = 0;
        greenShootingSpawned = 0; blueShootingSpawned = 0; yellowShootingSpawned = 0; redShootingSpawned = 0; purpleShootingSpawned = 0;
        greenBigSpawned = 0; blueBigSpawned = 0; yellowBigSpawned = 0; redBigSpawned = 0; purpleBigSpawned = 0;
    }
    #endregion

    //Regular gamemodes
    #region Easy gamemode - ALL waves
    public void StartEasyGameModeWave()
    {
        SetEasyWaves();
        isPlayingRun = true;
    }

    public void SetEasyWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        //These slimes should spawn on easy
        //Regular = Green, blue, yellow and red
        //Fast = Green, blue and yellow
        //Shooting = Green, blue and yellow
        //Big = Green and blue. 1-3 red or yellow at the final wave

        #region wave 1
        if (slimeWave == 1)
        {
            greenRegular_spawnCount = 6;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 2
        else if (slimeWave == 2)
        {
            greenRegular_spawnCount = 10;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 3
        else if (slimeWave == 3)
        {
            greenRegular_spawnCount = 14;
            greenFast_spawnCount = 3;

            currentWaveTotalTime = 5.5f;
        }
        #endregion

        #region wave 4
        else if (slimeWave == 4)
        {
            greenRegular_spawnCount = 20;
            greenFast_spawnCount = 3;
            greenShooting_spawnCount = 2;

            currentWaveTotalTime = 7f;
        }
        #endregion

        #region wave 5
        else if (slimeWave == 5)
        {
            greenRegular_spawnCount = 21;
            greenFast_spawnCount = 4;
            greenShooting_spawnCount = 2;

            blueRegular_spawnCount = 3;

            currentWaveTotalTime = 7.5f;
        }
        #endregion

        #region wave 6
        else if (slimeWave == 6)
        {
            greenRegular_spawnCount = 25;
            greenFast_spawnCount = 6;
            greenShooting_spawnCount = 4;

            blueRegular_spawnCount = 6;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 7
        else if (slimeWave == 7)
        {
            greenRegular_spawnCount = 17;
            greenFast_spawnCount = 7;
            greenShooting_spawnCount = 5;

            blueRegular_spawnCount = 11;

            currentWaveTotalTime = 8.7f;
        }
        #endregion

        #region wave 8
        else if (slimeWave == 8)
        {
            greenRegular_spawnCount = 13;
            greenFast_spawnCount = 5;

            blueRegular_spawnCount = 17;
            blueFast_spawnCount = 4;

            currentWaveTotalTime = 9.5f;
        }
        #endregion

        #region wave 9
        else if (slimeWave == 9)
        {
            greenRegular_spawnCount = 7;
            greenFast_spawnCount = 8;
            greenShooting_spawnCount = 5;

            blueRegular_spawnCount = 17;
            blueFast_spawnCount = 3;

            currentWaveTotalTime = 10f;
        }
        #endregion

        #region wave 10
        else if (slimeWave == 10)
        {
            greenRegular_spawnCount = 70;
            greenFast_spawnCount = 25;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 11
        else if (slimeWave == 11)
        {
            greenShooting_spawnCount = 3;

            blueRegular_spawnCount = 31;
            blueFast_spawnCount = 7;
            blueShooting_spawnCount = 3;

            currentWaveTotalTime = 11f;
        }
        #endregion

        #region wave 12
        else if (slimeWave == 12)
        {
            greenRegular_spawnCount = 10;
            greenFast_spawnCount = 4;
            greenShooting_spawnCount = 3;

            blueRegular_spawnCount = 23;
            blueFast_spawnCount = 6;
            blueShooting_spawnCount = 3;

            currentWaveTotalTime = 11f;
        }
        #endregion

        #region wave 13
        else if (slimeWave == 13)
        {
            greenRegular_spawnCount = 14;
            greenFast_spawnCount = 6;
            greenShooting_spawnCount = 3;
            greenBig_spawnCount = 3;

            blueRegular_spawnCount = 22;
            blueFast_spawnCount = 7;
            blueShooting_spawnCount = 3;

            currentWaveTotalTime = 11.5f;
        }
        #endregion

        #region wave 14
        else if (slimeWave == 14)
        {
            greenFast_spawnCount = 13;
            greenBig_spawnCount = 5;

            blueRegular_spawnCount = 23;
            blueFast_spawnCount = 12;
            blueShooting_spawnCount = 4;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 15
        else if (slimeWave == 15)
        {
            blueBig_spawnCount = 8;
            blueRegular_spawnCount = 53;
            blueFast_spawnCount = 7;

            currentWaveTotalTime = 10f;
        }
        #endregion

        #region wave 16
        else if (slimeWave == 16)
        {
            greenRegular_spawnCount = 38;
            greenBig_spawnCount = 15;

            blueRegular_spawnCount = 38;
            blueBig_spawnCount = 5;

            currentWaveTotalTime = 11f;
        }
        #endregion

        #region wave 17
        else if (slimeWave == 17)
        {
            greenRegular_spawnCount = 60;
            greenShooting_spawnCount = 3;

            blueRegular_spawnCount = 50;
            blueShooting_spawnCount = 2;

            yellowRegular_spawnCount = 13;

            currentWaveTotalTime = 14f;
        }
        #endregion

        #region wave 18
        else if (slimeWave == 18)
        {
            greenFast_spawnCount = 20;
            greenBig_spawnCount = 7;

            yellowRegular_spawnCount = 10;

            blueFast_spawnCount = 21;
            blueBig_spawnCount = 8;

            currentWaveTotalTime = 12.5f;
        }
        #endregion

        #region wave 19
        else if (slimeWave == 19)
        {
            greenShooting_spawnCount = 3;

            greenFast_spawnCount = 5;
            greenBig_spawnCount = 12;

            yellowRegular_spawnCount = 21;

            blueFast_spawnCount = 8;
            blueBig_spawnCount = 12;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 20
        else if (slimeWave == 20)
        {
            greenShooting_spawnCount = 8;
            greenBig_spawnCount = 20;

            blueShooting_spawnCount = 7;
            blueBig_spawnCount = 20;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 21
        else if (slimeWave == 21)
        {
            greenShooting_spawnCount = 3;
            greenBig_spawnCount = 5;

            blueRegular_spawnCount = 23;
            blueFast_spawnCount = 14;
            blueShooting_spawnCount = 3;
            blueBig_spawnCount = 5;

            redRegular_spawnCount = 7;

            currentWaveTotalTime = 14f;
        }
        #endregion

        #region wave 22
        else if (slimeWave == 22)
        {
            greenBig_spawnCount = 3;
            blueBig_spawnCount = 7;

            blueRegular_spawnCount = 35;
            redRegular_spawnCount = 18;

            blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 4;

            currentWaveTotalTime = 15f;
        }
        #endregion

        #region wave 23
        else if (slimeWave == 23)
        {
            greenShooting_spawnCount = 6;
            blueShooting_spawnCount = 6;
            yellowShooting_spawnCount = 4;

            redRegular_spawnCount = 24;

            currentWaveTotalTime = 17f;
        }
        #endregion

        #region wave 24
        else if (slimeWave == 24)
        {
            blueRegular_spawnCount = 15;
            yellowRegular_spawnCount = 15;
            redRegular_spawnCount = 15;

            blueFast_spawnCount = 7;
            yellowFast_spawnCount = 5;

            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 3;

            blueBig_spawnCount = 12;

            currentWaveTotalTime = 17f;
        }
        #endregion

        //Final wave for the demo
        #region wave 25 
        else if (slimeWave == 25)
        {
            greenRegular_spawnCount = 20;
            blueRegular_spawnCount = 20;
            yellowRegular_spawnCount = 20;
            redRegular_spawnCount = 20;

            greenFast_spawnCount = 9;
            blueFast_spawnCount = 9;
            yellowFast_spawnCount = 9;

            greenShooting_spawnCount = 3;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 3;

            greenBig_spawnCount = 12;
            blueBig_spawnCount = 12;

            redBig_spawnCount = 4;

            currentWaveTotalTime = 18f;
        }
        #endregion

        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion

    #region Normal gamemode - ALL waves
    public void StartNormalGameModeWave()
    {
        SetNormalWaves();
        isPlayingRun = true;
    }

    public void SetNormalWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        //These slimes should spawn on normal
        //Regular = Green, blue, yellow and red and purple
        //Fast = Green, blue and yellow and red
        //Shooting = Green, blue and yellow
        //Big = Green, blue and yellow. 

        //Ad waves here
        #region wave 1
        if (slimeWave == 1)
        {
            //greenRegular_spawnCount = 3;
            //blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 6;
            //blueFast_spawnCount = 1;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 2
        if (slimeWave == 2)
        {
            greenRegular_spawnCount = 7;
            blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 4;
            //blueFast_spawnCount = 1;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 4;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 2.5f;
        }
        #endregion

        #region wave 3
        if (slimeWave == 3)
        {
            greenRegular_spawnCount = 8;
            blueRegular_spawnCount = 8;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 6;
            blueFast_spawnCount = 6;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 4;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 3.5f;
        }
        #endregion

        #region wave 4
        if (slimeWave == 4)
        {
            //greenRegular_spawnCount = 15;
            blueRegular_spawnCount = 14;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 3;
            blueFast_spawnCount = 9;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 4;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 3.5f;
        }
        #endregion

        #region wave 5
        if (slimeWave == 5)
        {
            //greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 8;
            blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 4;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 3.5f;
        }
        #endregion

        #region wave 6
        if (slimeWave == 6)
        {
            greenRegular_spawnCount = 7;
            blueRegular_spawnCount = 7;
            yellowRegular_spawnCount = 7;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 24;
            //blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 3;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 4f;
        }
        #endregion

        #region wave 7
        if (slimeWave == 7)
        {
            //greenRegular_spawnCount = 7;
            //blueRegular_spawnCount = 6;
            yellowRegular_spawnCount = 18;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 16;
            //blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 8
        if (slimeWave == 8)
        {
            //greenRegular_spawnCount = 7;
            //blueRegular_spawnCount = 6;
            //yellowRegular_spawnCount = 10;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 16;
            blueFast_spawnCount = 42;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 4;
            blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 9
        if (slimeWave == 9)
        {
            greenRegular_spawnCount = 12;
            blueRegular_spawnCount = 12;
            //yellowRegular_spawnCount = 10;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 9;
            blueFast_spawnCount = 8;
            yellowFast_spawnCount = 7;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 4;
            //yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 5;
            yellowBig_spawnCount = 5;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 10
        if (slimeWave == 10)
        {
            greenRegular_spawnCount = 33;
            blueRegular_spawnCount = 33;
            yellowRegular_spawnCount = 33;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 3;
            //yellowFast_spawnCount = 4;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 5;
            //yellowBig_spawnCount = 4;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 8.5f;
        }
        #endregion

        #region wave 11
        if (slimeWave == 11)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 25;
            yellowRegular_spawnCount = 29;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 3;
            yellowFast_spawnCount = 16;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 5;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 5;
            //yellowBig_spawnCount = 4;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 12
        if (slimeWave == 12)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 25;
            //yellowRegular_spawnCount = 25;
            redRegular_spawnCount = 28;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 3;
            //yellowFast_spawnCount = 10;
            redFast_spawnCount = 11;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 5;
            //yellowBig_spawnCount = 4;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 13
        if (slimeWave == 13)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 25;
            //yellowRegular_spawnCount = 25;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            blueFast_spawnCount = 18;
            yellowFast_spawnCount = 18;
            //redFast_spawnCount = 8;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            blueBig_spawnCount = 6;
            yellowBig_spawnCount = 6;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 10f;
        }
        #endregion

        #region wave 14
        if (slimeWave == 14)
        {
            //greenRegular_spawnCount = 30;
            blueRegular_spawnCount = 37;
            yellowRegular_spawnCount = 17;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 15;
            //yellowFast_spawnCount = 15;
            redFast_spawnCount = 15;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 12;
            //blueBig_spawnCount = 6;
            //yellowBig_spawnCount = 6;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 15
        if (slimeWave == 15)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 25;
            //yellowRegular_spawnCount = 25;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 10;
            //yellowFast_spawnCount = 25;
            //redFast_spawnCount = 12;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 12;
            blueShooting_spawnCount = 12;
            yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 12;
            //blueBig_spawnCount = 12;
            //yellowBig_spawnCount = 4;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 16
        if (slimeWave == 16)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 35;
            //yellowRegular_spawnCount = 15;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 20;
            blueFast_spawnCount = 16;
            yellowFast_spawnCount = 14;
            redFast_spawnCount = 11;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 7;
            blueBig_spawnCount = 5;
            //yellowBig_spawnCount = 4;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 17
        if (slimeWave == 17)
        {
            greenRegular_spawnCount = 35;
            blueRegular_spawnCount = 30;
            yellowRegular_spawnCount = 25;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 23;
            blueFast_spawnCount = 16;
            yellowFast_spawnCount = 13;
            //redFast_spawnCount = 11;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 4;
            yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 5;
            blueBig_spawnCount = 4;
            yellowBig_spawnCount = 3;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 18
        if (slimeWave == 18)
        {
            //greenRegular_spawnCount = 35;
            blueRegular_spawnCount = 35;
            yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 27;
            //blueFast_spawnCount = 16;
            yellowFast_spawnCount = 18;
            //redFast_spawnCount = 11;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 6;
            //blueShooting_spawnCount = 5;
            //yellowShooting_spawnCount = 4;
            redShooting_spawnCount = 5;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 3;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 10f;
        }
        #endregion

        #region wave 19
        if (slimeWave == 19)
        {
            greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 35;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 70;
            //blueFast_spawnCount = 16;
            //yellowFast_spawnCount = 18;
            //redFast_spawnCount = 11;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 6;
            //blueShooting_spawnCount = 5;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 5;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 3;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 11f;
        }
        #endregion

        #region wave 20
        if (slimeWave == 20)
        {
            //greenRegular_spawnCount = 100;
            blueRegular_spawnCount = 80;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 50;
            blueFast_spawnCount = 48;
            //yellowFast_spawnCount = 18;
            //redFast_spawnCount = 11;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 18;
            //blueShooting_spawnCount = 5;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 5;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            //blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 3;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 11f;
        }
        #endregion

        #region wave 21
        if (slimeWave == 21)
        {
            //greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 80;
            //yellowRegular_spawnCount = 35;
            redRegular_spawnCount = 15;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 50;
            //blueFast_spawnCount = 45;
            yellowFast_spawnCount = 15;
            redFast_spawnCount = 16;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 7;
            blueShooting_spawnCount = 7;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 5;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 5;
            blueBig_spawnCount = 7;
            yellowBig_spawnCount = 7;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 22
        if (slimeWave == 22)
        {
            greenRegular_spawnCount = 75;
            //blueRegular_spawnCount = 80;
            //yellowRegular_spawnCount = 35;
            redRegular_spawnCount = 32;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 17;
            //blueFast_spawnCount = 45;
            //yellowFast_spawnCount = 15;
            redFast_spawnCount = 19;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 5;
            //blueShooting_spawnCount = 8;
            //yellowShooting_spawnCount = 4;
            redShooting_spawnCount = 5;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 10;
            //blueBig_spawnCount = 7;
            //yellowBig_spawnCount = 7;
            redBig_spawnCount = 8;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 23
        if (slimeWave == 23)
        {
            //greenRegular_spawnCount = 75;
            //blueRegular_spawnCount = 80;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 35;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 52;
            blueFast_spawnCount = 42;
            yellowFast_spawnCount = 33;
            redFast_spawnCount = 22;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 7;
            //blueShooting_spawnCount = 8;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 7;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 10;
            //blueBig_spawnCount = 7;
            //yellowBig_spawnCount = 7;
            //redBig_spawnCount = 8;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 15.5f;
        }
        #endregion

        #region wave 24
        if (slimeWave == 24)
        {
            //greenRegular_spawnCount = 75;
            //blueRegular_spawnCount = 80;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 35;
            purpleRegular_spawnCount = 18;

            //greenFast_spawnCount = 55;
            //blueFast_spawnCount = 45;
            //yellowFast_spawnCount = 35;
            //redFast_spawnCount = 25;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 7;
            blueShooting_spawnCount = 10;
            yellowShooting_spawnCount = 8;
            //redShooting_spawnCount = 7;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 10;
            //blueBig_spawnCount = 10;
            //yellowBig_spawnCount = 6;
            //redBig_spawnCount = 8;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 25
        if (slimeWave == 25)
        {
            //greenRegular_spawnCount = 75;
            //blueRegular_spawnCount = 80;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 35;
            purpleRegular_spawnCount = 32;

            //greenFast_spawnCount = 55;
            blueFast_spawnCount = 35;
            //yellowFast_spawnCount = 35;
            //redFast_spawnCount = 25;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 7;
            //blueShooting_spawnCount = 8;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 7;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 20;
            //blueBig_spawnCount = 22;
            //yellowBig_spawnCount = 6;
            //redBig_spawnCount = 8;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 15f;
        }
        #endregion

        #region wave 26
        if (slimeWave == 26)
        {
            //greenRegular_spawnCount = 75;
            //blueRegular_spawnCount = 80;
            yellowRegular_spawnCount = 25;
            redRegular_spawnCount = 25;
            purpleRegular_spawnCount = 25;

            //greenFast_spawnCount = 55;
            blueFast_spawnCount = 25;
            yellowFast_spawnCount = 25;
            redFast_spawnCount = 25;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 7;
            //blueShooting_spawnCount = 8;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 7;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 20;
            //blueBig_spawnCount = 22;
            //yellowBig_spawnCount = 6;
            //redBig_spawnCount = 8;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 15f;
        }
        #endregion

        #region wave 27
        if (slimeWave == 27)
        {
            greenRegular_spawnCount = 30;
            blueRegular_spawnCount = 25;
            yellowRegular_spawnCount = 15;
            redRegular_spawnCount = 15;
            purpleRegular_spawnCount = 15;

            greenFast_spawnCount = 12;
            blueFast_spawnCount = 12;
            yellowFast_spawnCount = 12;
            redFast_spawnCount = 12;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 7;
            //blueShooting_spawnCount = 8;
            yellowShooting_spawnCount = 6;
            redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 20;
            //blueBig_spawnCount = 22;
            //yellowBig_spawnCount = 6;
            //redBig_spawnCount = 8;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 16f;
        }
        #endregion

        #region wave 28
        if (slimeWave == 28)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 25;
            //yellowRegular_spawnCount = 15;
            redRegular_spawnCount = 35;
            purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 18;
            //blueFast_spawnCount = 17;
            //yellowFast_spawnCount = 16;
            redFast_spawnCount = 35;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 10;
            blueShooting_spawnCount = 10;
            yellowShooting_spawnCount = 5;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 6;
            //blueBig_spawnCount = 22;
            //yellowBig_spawnCount = 6;
            redBig_spawnCount = 7;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 17f;
        }
        #endregion

        #region wave 29
        if (slimeWave == 29)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 25;
            //yellowRegular_spawnCount = 15;
            //redRegular_spawnCount = 35;
            //purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 18;
            //blueFast_spawnCount = 17;
            //yellowFast_spawnCount = 16;
            //redFast_spawnCount = 35;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 50;
            //blueShooting_spawnCount = 10;
            //yellowShooting_spawnCount = 5;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 6;
            blueBig_spawnCount = 5;
            yellowBig_spawnCount = 4;
            redBig_spawnCount = 4;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 18f;
        }
        #endregion

        #region wave 30
        if (slimeWave == 30)
        {
            greenRegular_spawnCount = 50;
            blueRegular_spawnCount = 50;
            yellowRegular_spawnCount = 50;
            redRegular_spawnCount = 50;
            purpleRegular_spawnCount = 15;

            greenFast_spawnCount = 45;
            blueFast_spawnCount = 23;
            yellowFast_spawnCount = 17;
            redFast_spawnCount = 15;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 20;
            blueShooting_spawnCount = 10;
            yellowShooting_spawnCount = 7;
            redShooting_spawnCount = 4;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 9;
            blueBig_spawnCount = 7;
            yellowBig_spawnCount = 5;
            redBig_spawnCount = 3;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 21f;
        }
        #endregion

        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion

    #region Hard gamemode - ALL waves
    public void StartHardGameModeWave()
    {
        SetHardWaves();
        isPlayingRun = true;
    }

    public void SetHardWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        //These slimes should spawn on easy
        //Regular = Green, blue, yellow and red
        //Fast = Green, blue and yellow
        //Shooting = Green, blue and yellow
        //Big = Green and blue. 1-3 red or yellow at the final wave

        //Ad waves here

        #region wave 1
        if (slimeWave == 1)
        {
            //greenRegular_spawnCount = 3;
            //blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 6;
            //blueFast_spawnCount = 1;
            //yellowFast_spawnCount = 1;
            redFast_spawnCount = 1;
            purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 2
        if (slimeWave == 2)
        {
            greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 6;
            blueFast_spawnCount = 3;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 3
        if (slimeWave == 3)
        {
            greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 6;
            //blueFast_spawnCount = 3;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 3f;
        }
        #endregion

        #region wave 4
        if (slimeWave == 4)
        {
            //greenRegular_spawnCount = 30;
            blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 6;
            blueFast_spawnCount = 6;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 3;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 4f;
        }
        #endregion

        #region wave 5
        if (slimeWave == 5)
        {
            //greenRegular_spawnCount = 30;
            //blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 6;
            //blueFast_spawnCount = 4;
            yellowFast_spawnCount = 8;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 6;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 6
        if (slimeWave == 6)
        {
            greenRegular_spawnCount = 26;
            //blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 8;
            blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 5;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 5;
            blueShooting_spawnCount = 4;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 7
        if (slimeWave == 7)
        {
            //greenRegular_spawnCount = 20;
            //blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 18;
            //blueFast_spawnCount = 7;
            //yellowFast_spawnCount = 5;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 12;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 6f;
        }
        #endregion

        #region wave 8
        if (slimeWave == 8)
        {
            //greenRegular_spawnCount = 20;
            //blueRegular_spawnCount = 10;
            yellowRegular_spawnCount = 10;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            blueFast_spawnCount = 10;
            //yellowFast_spawnCount = 5;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 7;
            blueShooting_spawnCount = 6;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 1;
            blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 7f;
        }
        #endregion

        #region wave 9
        if (slimeWave == 9)
        {
            greenRegular_spawnCount = 35;
            //blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 5;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 8;
            yellowFast_spawnCount = 11;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 12;
            //blueShooting_spawnCount = 6;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 2;
            blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 7f;
        }
        #endregion

        #region wave 10
        if (slimeWave == 10)
        {
            //greenRegular_spawnCount = 20;
            //blueRegular_spawnCount = 10;
            //yellowRegular_spawnCount = 18;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 8;
            yellowFast_spawnCount = 25;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            //blueShooting_spawnCount = 6;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 11
        if (slimeWave == 11)
        {
            //greenRegular_spawnCount = 20;
            blueRegular_spawnCount = 50;
            //yellowRegular_spawnCount = 18;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 18;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            blueShooting_spawnCount = 9;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 10f;
        }
        #endregion

        #region wave 12
        if (slimeWave == 12)
        {
            greenRegular_spawnCount = 115;
            blueRegular_spawnCount = 30;
            //yellowRegular_spawnCount = 18;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 18;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            //blueShooting_spawnCount = 6;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 13
        if (slimeWave == 13)
        {
            //greenRegular_spawnCount = 115;
            //blueRegular_spawnCount = 40;
            yellowRegular_spawnCount = 45;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 8;
            //yellowFast_spawnCount = 18;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            //blueShooting_spawnCount = 6;
            yellowShooting_spawnCount = 6;
            redShooting_spawnCount = 5;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 11f;
        }
        #endregion

        #region wave 14
        if (slimeWave == 14)
        {
            //greenRegular_spawnCount = 115;
            //blueRegular_spawnCount = 40;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            blueFast_spawnCount = 25;
            yellowFast_spawnCount = 14;
            redFast_spawnCount = 8;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            //blueShooting_spawnCount = 6;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 3;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 6;
            blueBig_spawnCount = 4;
            yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 15
        if (slimeWave == 15)
        {
            //greenRegular_spawnCount = 115;
            //blueRegular_spawnCount = 40;
            //yellowRegular_spawnCount = 35;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 18;
            //yellowFast_spawnCount = 12;
            //redFast_spawnCount = 6;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            //blueShooting_spawnCount = 6;
            yellowShooting_spawnCount = 6;
            redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 15;
            //blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 16
        if (slimeWave == 16)
        {
            //greenRegular_spawnCount = 115;
            blueRegular_spawnCount = 80;
            yellowRegular_spawnCount = 55;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 18;
            //yellowFast_spawnCount = 12;
            //redFast_spawnCount = 6;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 10;
            blueShooting_spawnCount = 20;
            //yellowShooting_spawnCount = 6;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 15;
            //blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 17
        if (slimeWave == 17)
        {
            //greenRegular_spawnCount = 115;
            //blueRegular_spawnCount = 70;
            //yellowRegular_spawnCount = 45;
            //redRegular_spawnCount = 1;
            purpleRegular_spawnCount = 30;

            //greenFast_spawnCount = 15;
            //blueFast_spawnCount = 18;
            //yellowFast_spawnCount = 12;
            //redFast_spawnCount = 6;
            purpleFast_spawnCount = 12;

            //greenShooting_spawnCount = 10;
            //blueShooting_spawnCount = 20;
            //yellowShooting_spawnCount = 6;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 15;
            //blueBig_spawnCount = 4;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 18
        if (slimeWave == 18)
        {
            greenRegular_spawnCount = 10;
            blueRegular_spawnCount = 9;
            yellowRegular_spawnCount = 7;
            redRegular_spawnCount = 6;
            purpleRegular_spawnCount = 5;

            greenFast_spawnCount = 10;
            blueFast_spawnCount = 9;
            yellowFast_spawnCount = 8;
            redFast_spawnCount = 7;
            purpleFast_spawnCount = 6;

            greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 2;
            redShooting_spawnCount = 1;
            purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 2;
            blueBig_spawnCount = 2;
            yellowBig_spawnCount = 2;
            redBig_spawnCount = 2;
            purpleBig_spawnCount = 2;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 19
        if (slimeWave == 19)
        {
            //greenRegular_spawnCount = 10;
            //blueRegular_spawnCount = 9;
            //yellowRegular_spawnCount = 7;
            redRegular_spawnCount = 25;
            //purpleRegular_spawnCount = 5;

            //greenFast_spawnCount = 10;
            //blueFast_spawnCount = 9;
            //yellowFast_spawnCount = 8;
            redFast_spawnCount = 15;
            //purpleFast_spawnCount = 6;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 2;
            redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            redBig_spawnCount = 5;
            //purpleBig_spawnCount = 2;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 20
        if (slimeWave == 20)
        {
            //greenRegular_spawnCount = 10;
            //blueRegular_spawnCount = 9;
            //yellowRegular_spawnCount = 7;
            //redRegular_spawnCount = 15;
            purpleRegular_spawnCount = 17;

            //greenFast_spawnCount = 10;
            //blueFast_spawnCount = 9;
            //yellowFast_spawnCount = 8;
            //redFast_spawnCount = 15;
            purpleFast_spawnCount = 14;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 6;
            purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            purpleBig_spawnCount = 4;

            currentWaveTotalTime = 14f;
        }
        #endregion

        #region wave 21
        if (slimeWave == 21)
        {
            //greenRegular_spawnCount = 10;
            blueRegular_spawnCount = 110;
            yellowRegular_spawnCount = 110;
            //redRegular_spawnCount = 15;
            //purpleRegular_spawnCount = 12;

            //greenFast_spawnCount = 10;
            //blueFast_spawnCount = 9;
            //yellowFast_spawnCount = 8;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            //purpleBig_spawnCount = 4;

            currentWaveTotalTime = 15f;
        }
        #endregion

        #region wave 22
        if (slimeWave == 22)
        {
            //greenRegular_spawnCount = 10;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 100;
            //redRegular_spawnCount = 15;
            //purpleRegular_spawnCount = 12;

            greenFast_spawnCount = 42;
            blueFast_spawnCount = 35;
            yellowFast_spawnCount = 4;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            //purpleBig_spawnCount = 4;

            currentWaveTotalTime = 16f;
        }
        #endregion

        #region wave 23
        if (slimeWave == 23)
        {
            //greenRegular_spawnCount = 10;
            //blueRegular_spawnCount = 100;
            yellowRegular_spawnCount = 105;
            //redRegular_spawnCount = 15;
            //purpleRegular_spawnCount = 12;

            //greenFast_spawnCount = 35;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 8;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            //greenShooting_spawnCount = 2;
            //blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 18;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            //purpleBig_spawnCount = 4;

            currentWaveTotalTime = 16f;
        }
        #endregion

        #region wave 24
        if (slimeWave == 24)
        {
            //greenRegular_spawnCount = 10;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 100;
            redRegular_spawnCount = 50;
            purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 35;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 8;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            greenShooting_spawnCount = 16;
            //blueShooting_spawnCount = 2;
            //yellowShooting_spawnCount = 18;
            //redShooting_spawnCount = 6;
            purpleShooting_spawnCount = 5;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            purpleBig_spawnCount = 8;

            currentWaveTotalTime = 17f;
        }
        #endregion

        #region wave 25
        if (slimeWave == 25)
        {
            //greenRegular_spawnCount = 10;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 100;
            //redRegular_spawnCount = 35;
            //purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 35;
            //blueFast_spawnCount = 25;
            yellowFast_spawnCount = 35;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            //greenShooting_spawnCount = 16;
            blueShooting_spawnCount = 34;
            //yellowShooting_spawnCount = 18;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 18f;
        }
        #endregion

        #region wave 26
        if (slimeWave == 26)
        {
            greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 100;
            //redRegular_spawnCount = 35;
            //purpleRegular_spawnCount = 35;

            greenFast_spawnCount = 65;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 28;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            greenShooting_spawnCount = 35;
            //blueShooting_spawnCount = 30;
            //yellowShooting_spawnCount = 18;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 18f;
        }
        #endregion

        #region wave 27
        if (slimeWave == 27)
        {
            greenRegular_spawnCount = 100;
            blueRegular_spawnCount = 100;
            yellowRegular_spawnCount = 100;
            redRegular_spawnCount = 100;
            //purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 65;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 28;
            //redFast_spawnCount = 15;
            //purpleFast_spawnCount = 10;

            //greenShooting_spawnCount = 20;
            blueShooting_spawnCount = 14;
            yellowShooting_spawnCount = 10;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 4;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 18f;
        }
        #endregion

        #region wave 28
        if (slimeWave == 28)
        {
            //greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 90;
            //redRegular_spawnCount = 70;
            //purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 65;
            //blueFast_spawnCount = 25;
            yellowFast_spawnCount = 34;
            redFast_spawnCount = 16;
            purpleFast_spawnCount = 16;

            //greenShooting_spawnCount = 20;
            //blueShooting_spawnCount = 8;
            //yellowShooting_spawnCount = 8;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            yellowBig_spawnCount = 12;
            redBig_spawnCount = 12;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 19.3f;
        }
        #endregion

        #region wave 29
        if (slimeWave == 29)
        {
            //greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 90;
            //redRegular_spawnCount = 70;
            purpleRegular_spawnCount = 115;

            //greenFast_spawnCount = 65;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 24;
            //redFast_spawnCount = 12;
            //purpleFast_spawnCount = 12;

            //greenShooting_spawnCount = 20;
            //blueShooting_spawnCount = 8;
            //yellowShooting_spawnCount = 8;
            //redShooting_spawnCount = 6;
            //purpleShooting_spawnCount = 4;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 12;
            //redBig_spawnCount = 12;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 20f;
        }
        #endregion

        #region wave 30
        if (slimeWave == 30)
        {
            //greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 90;
            //redRegular_spawnCount = 70;
            //purpleRegular_spawnCount = 100;

            //greenFast_spawnCount = 65;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 24;
            //redFast_spawnCount = 12;
            //purpleFast_spawnCount = 12;

            greenShooting_spawnCount = 9;
            blueShooting_spawnCount = 8;
            yellowShooting_spawnCount = 7;
            redShooting_spawnCount = 6;
            purpleShooting_spawnCount = 5;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 12;
            //redBig_spawnCount = 12;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 21f;
        }
        #endregion

        #region wave 31
        if (slimeWave == 31)
        {
            greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            yellowRegular_spawnCount = 75;
            //redRegular_spawnCount = 70;
            purpleRegular_spawnCount = 35;

            greenFast_spawnCount = 27;
            //blueFast_spawnCount = 25;
            yellowFast_spawnCount = 16;
            //redFast_spawnCount = 12;
            purpleFast_spawnCount = 12;

            //greenShooting_spawnCount = 5;
            //blueShooting_spawnCount = 5;
            //yellowShooting_spawnCount = 5;
            //redShooting_spawnCount = 5;
            purpleShooting_spawnCount = 5;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 12;
            //redBig_spawnCount = 12;
            //purpleBig_spawnCount = 8;

            currentWaveTotalTime = 21f;
        }
        #endregion

        #region wave 32
        if (slimeWave == 32)
        {
            //greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            yellowRegular_spawnCount = 65;
            redRegular_spawnCount = 65;
            //purpleRegular_spawnCount = 35;

            //greenFast_spawnCount = 25;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 15;
            //redFast_spawnCount = 12;
            //purpleFast_spawnCount = 10;

            greenShooting_spawnCount = 30;
            blueShooting_spawnCount = 25;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 2;
            //blueBig_spawnCount = 2;
            //yellowBig_spawnCount = 12;
            redBig_spawnCount = 4;
            purpleBig_spawnCount = 6;

            currentWaveTotalTime = 22f;
        }
        #endregion

        #region wave 33
        if (slimeWave == 33)
        {
            //greenRegular_spawnCount = 100;
            //blueRegular_spawnCount = 100;
            //yellowRegular_spawnCount = 65;
            redRegular_spawnCount = 60;
            purpleRegular_spawnCount = 60;

            //greenFast_spawnCount = 25;
            //blueFast_spawnCount = 25;
            //yellowFast_spawnCount = 15;
            redFast_spawnCount = 22;
            purpleFast_spawnCount = 22;

            //greenShooting_spawnCount = 25;
            //blueShooting_spawnCount = 15;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 10;
            blueBig_spawnCount = 10;
            yellowBig_spawnCount = 10;
            redBig_spawnCount = 10;
            purpleBig_spawnCount = 10;

            currentWaveTotalTime = 23f;
        }
        #endregion

        #region wave 34
        if (slimeWave == 34)
        {
            greenRegular_spawnCount = 115;
            blueRegular_spawnCount = 110;
            yellowRegular_spawnCount = 105;
            redRegular_spawnCount = 100;
            purpleRegular_spawnCount = 100;

            greenFast_spawnCount = 30;
            blueFast_spawnCount = 25;
            yellowFast_spawnCount = 20;
            redFast_spawnCount = 15;
            purpleFast_spawnCount = 10;

            //greenShooting_spawnCount = 25;
            //blueShooting_spawnCount = 15;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 10;
            //blueBig_spawnCount = 10;
            //yellowBig_spawnCount = 10;
            //redBig_spawnCount = 10;
            //purpleBig_spawnCount = 10;

            currentWaveTotalTime = 25f;
        }
        #endregion

        #region wave 35
        if (slimeWave == 35)
        {
            //greenRegular_spawnCount = 115;
            //blueRegular_spawnCount = 110;
            yellowRegular_spawnCount = 50;
            redRegular_spawnCount = 50;
            purpleRegular_spawnCount = 50;

            greenFast_spawnCount = 45;
            blueFast_spawnCount = 14;
            yellowFast_spawnCount = 14;
            redFast_spawnCount = 14;
            purpleFast_spawnCount = 14;

            //greenShooting_spawnCount = 25;
            //blueShooting_spawnCount = 15;
            yellowShooting_spawnCount = 5;
            redShooting_spawnCount = 3;
            purpleShooting_spawnCount = 2;

            greenBig_spawnCount = 12;
            //blueBig_spawnCount = 10;
            yellowBig_spawnCount = 12;
            //redBig_spawnCount = 10;
            purpleBig_spawnCount = 12;

            currentWaveTotalTime = 27f;
        }
        #endregion

        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion 

    //Challenges
    #region Bullethell gamemode - ALL waves
    public void StartBullethellGameModeWave()
    {
        SetBullethellWaves();
        isPlayingRun = true;
    }

    public void SetBullethellWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        #region wave 1
        if (slimeWave == 1)
        {
            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 1;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 2
        if (slimeWave == 2)
        {
            greenShooting_spawnCount = 8;
            //blueShooting_spawnCount = 1;

            currentWaveTotalTime = 2.2f;
        }
        #endregion

        #region wave 3
        if (slimeWave == 3)
        {
            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 4;

            currentWaveTotalTime = 3.5f;
        }
        #endregion

        #region wave 4
        if (slimeWave == 4)
        {
            greenShooting_spawnCount = 3;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 3;

            currentWaveTotalTime = 4f;
        }
        #endregion

        #region wave 5
        if (slimeWave == 5)
        {
            greenShooting_spawnCount = 15;
            //yellowShooting_spawnCount = 3;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 6
        if (slimeWave == 6)
        {
            greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 5;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 7
        if (slimeWave == 7)
        {
            greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 7;

            currentWaveTotalTime = 6f;
        }
        #endregion

        #region wave 8
        if (slimeWave == 8)
        {
            //greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 18;
            //yellowShooting_spawnCount = 7;

            currentWaveTotalTime = 6f;
        }
        #endregion

        #region wave 9
        if (slimeWave == 9)
        {
            greenShooting_spawnCount = 3;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 3;
            redShooting_spawnCount = 2; 

            currentWaveTotalTime = 6f;
        }
        #endregion

        #region wave 10
        if (slimeWave == 10)
        {
            //greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 3;
            //yellowShooting_spawnCount = 3;
            redShooting_spawnCount = 7;

            currentWaveTotalTime = 6.2f;
        }
        #endregion

        #region wave 11
        if (slimeWave == 11)
        {
            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 4;
            redShooting_spawnCount = 3;

            currentWaveTotalTime = 6.8f;
        }
        #endregion

        #region wave 12
        if (slimeWave == 12)
        {
            greenShooting_spawnCount = 36;
            //blueShooting_spawnCount = 4;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 4;

            currentWaveTotalTime = 7.5f;
        }
        #endregion

        #region wave 13
        if (slimeWave == 13)
        {
            //greenShooting_spawnCount = 25;
            blueShooting_spawnCount = 23;
            //yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 4;

            currentWaveTotalTime = 7.7f;
        }
        #endregion

        #region wave 14
        if (slimeWave == 14)
        {
            //greenShooting_spawnCount = 25;
            //blueShooting_spawnCount = 23;
            yellowShooting_spawnCount = 17;
            //redShooting_spawnCount = 4;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 15
        if (slimeWave == 15)
        {
            //greenShooting_spawnCount = 25;
            //blueShooting_spawnCount = 23;
            //yellowShooting_spawnCount = 16;
            redShooting_spawnCount = 13;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 16
        if (slimeWave == 16)
        {
            //greenShooting_spawnCount = 25;
            //blueShooting_spawnCount = 23;
            //yellowShooting_spawnCount = 16;
            //redShooting_spawnCount = 14;
            purpleShooting_spawnCount = 11;

            currentWaveTotalTime = 9f;
        }
        #endregion

        #region wave 17
        if (slimeWave == 17)
        {
            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 4;
            yellowShooting_spawnCount = 4;
            redShooting_spawnCount = 4;
            purpleShooting_spawnCount = 4;

            currentWaveTotalTime = 10f;
        }
        #endregion

        #region wave 18
        if (slimeWave == 18)
        {
            greenShooting_spawnCount = 6;
            blueShooting_spawnCount = 6;
            yellowShooting_spawnCount = 5;
            redShooting_spawnCount = 5;
            purpleShooting_spawnCount = 4;

            currentWaveTotalTime = 12f;
        }
        #endregion

        #region wave 19
        if (slimeWave == 19)
        {
            greenShooting_spawnCount = 5;
            blueShooting_spawnCount = 5;
            yellowShooting_spawnCount = 5;
            redShooting_spawnCount = 5;
            purpleShooting_spawnCount = 8;

            currentWaveTotalTime = 13f;
        }
        #endregion

        #region wave 20
        if (slimeWave == 20)
        {
            greenShooting_spawnCount = 15;
            blueShooting_spawnCount = 10;
            yellowShooting_spawnCount = 7;
            redShooting_spawnCount = 6;
            purpleShooting_spawnCount = 7;

            currentWaveTotalTime = 16f;
        }
        #endregion

        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion

    #region Flash gamemode - ALL waves
    public void StartFlashGameModeWave()
    {
        SetFlashWaves();
        isPlayingRun = true;
    }

    public void SetFlashWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        #region wave 1
        if (slimeWave == 1)
        {
            greenFast_spawnCount = 6;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 2
        if (slimeWave == 2)
        {
            greenFast_spawnCount = 5;
            blueFast_spawnCount = 3;

            currentWaveTotalTime = 3f;
        }
        #endregion

        #region wave 3
        if (slimeWave == 3)
        {
            greenFast_spawnCount = 10;
            blueFast_spawnCount = 3;

            currentWaveTotalTime = 3f;
        }
        #endregion

        #region wave 4
        if (slimeWave == 4)
        {
            greenFast_spawnCount = 8;
            blueFast_spawnCount = 6;

            currentWaveTotalTime = 4f;
        }
        #endregion

        #region wave 5
        if (slimeWave == 5)
        {
            greenFast_spawnCount = 12;
            blueFast_spawnCount = 7;
            //yellowFast_spawnCount = 2;

            currentWaveTotalTime = 4.5f;
        }
        #endregion

        #region wave 6
        if (slimeWave == 6)
        {
            greenFast_spawnCount = 7;
            blueFast_spawnCount = 5;
            yellowFast_spawnCount = 6;

            currentWaveTotalTime = 4.5f;
        }
        #endregion

        #region wave 7
        if (slimeWave == 7)
        {
            greenFast_spawnCount = 14;
            blueFast_spawnCount = 8;
            yellowFast_spawnCount = 6;

            currentWaveTotalTime = 5;
        }
        #endregion

        #region wave 8
        if (slimeWave == 8)
        {
            greenFast_spawnCount = 6;
            blueFast_spawnCount = 7;
            yellowFast_spawnCount = 13;

            currentWaveTotalTime = 6.2f;
        }
        #endregion

        #region wave 9
        if (slimeWave == 9)
        {
            //greenFast_spawnCount = 12;
            blueFast_spawnCount = 38;
            //yellowFast_spawnCount = 12;

            currentWaveTotalTime = 8f;
        }
        #endregion

        #region wave 10
        if (slimeWave == 10)
        {
            //greenFast_spawnCount = 12;
            //blueFast_spawnCount = 25;
            yellowFast_spawnCount = 11;
            redFast_spawnCount = 8;

            currentWaveTotalTime = 9;
        }
        #endregion

        #region wave 11
        if (slimeWave == 11)
        {
            //greenFast_spawnCount = 12;
            blueFast_spawnCount = 18;
            //yellowFast_spawnCount = 10;
            redFast_spawnCount = 13;

            currentWaveTotalTime = 10;
        }
        #endregion

        #region wave 12
        if (slimeWave == 12)
        {
            greenFast_spawnCount = 60;
            blueFast_spawnCount = 10;
            //yellowFast_spawnCount = 10;
            //redFast_spawnCount = 15;

            currentWaveTotalTime = 10;
        }
        #endregion

        #region wave 13
        if (slimeWave == 13)
        {
            //greenFast_spawnCount = 45;
            //blueFast_spawnCount = 17;
            yellowFast_spawnCount = 19;
            redFast_spawnCount = 14;

            currentWaveTotalTime = 10.5f;
        }
        #endregion

        #region wave 14
        if (slimeWave == 14)
        {
            greenFast_spawnCount = 15;
            blueFast_spawnCount = 15;
            yellowFast_spawnCount = 14;
            redFast_spawnCount = 14;

            currentWaveTotalTime = 10;
        }
        #endregion

        #region wave 15
        if (slimeWave == 15)
        {
            greenFast_spawnCount = 25;
            blueFast_spawnCount = 17;
            yellowFast_spawnCount = 16;
            redFast_spawnCount = 14;

            currentWaveTotalTime = 12.3f;
        }
        #endregion

        #region wave 16
        if (slimeWave == 16)
        {
            greenFast_spawnCount = 55;
            //blueFast_spawnCount = 20;
            //yellowFast_spawnCount = 17;
            //redFast_spawnCount = 16;
            purpleFast_spawnCount = 10;

            currentWaveTotalTime = 13;
        }
        #endregion

        #region wave 17
        if (slimeWave == 17)
        {
            //greenFast_spawnCount = 50;
            //blueFast_spawnCount = 20;
            //yellowFast_spawnCount = 17;
            redFast_spawnCount = 13;
            purpleFast_spawnCount = 11;

            currentWaveTotalTime = 14;
        }
        #endregion

        #region wave 18
        if (slimeWave == 18)
        {
            greenFast_spawnCount = 25;
            blueFast_spawnCount = 25;
            yellowFast_spawnCount = 18;
            redFast_spawnCount = 9;
            purpleFast_spawnCount = 9;

            currentWaveTotalTime = 15;
        }
        #endregion

        #region wave 19
        if (slimeWave == 19)
        {
            //greenFast_spawnCount = 32;
            //blueFast_spawnCount = 65;
            yellowFast_spawnCount = 35;
            //redFast_spawnCount = 20;
            purpleFast_spawnCount = 13;

            currentWaveTotalTime = 16;
        }
        #endregion

        #region wave 20
        if (slimeWave == 20)
        {
            greenFast_spawnCount = 17;
            blueFast_spawnCount = 16;
            yellowFast_spawnCount = 15;
            redFast_spawnCount = 14;
            purpleFast_spawnCount = 13;

            currentWaveTotalTime = 18;
        }
        #endregion

        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion

    #region Fragile gamemode - ALL waves
    public void StartFragileGameModeWave()
    {
        SetFragileWaves();
        isPlayingRun = true;
    }

    public void SetFragileWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        //Ad waves here

        slimeWave = 15;

        #region wave 1
        if (slimeWave == 1)
        {
            greenRegular_spawnCount = 3;
            blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 1;
            //blueFast_spawnCount = 1;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 1;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 2f;
        }
        #endregion

        #region wave 2
        if (slimeWave == 2)
        {
            //greenRegular_spawnCount = 4;
            blueRegular_spawnCount = 4;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 2;
            blueFast_spawnCount = 2;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 3f;
        }
        #endregion

        #region wave 3
        if (slimeWave == 3)
        {
            greenRegular_spawnCount = 10;
            blueRegular_spawnCount = 2;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 2;
            blueFast_spawnCount = 2;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 2;
            blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 4f;
        }
        #endregion

        #region wave 4
        if (slimeWave == 4)
        {
            greenRegular_spawnCount = 3;
            blueRegular_spawnCount = 3;
            //yellowRegular_spawnCount = 1;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 5;
            blueFast_spawnCount = 5;
            //yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 4;
            blueShooting_spawnCount = 1;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 4f;
        }
        #endregion

        #region wave 5
        if (slimeWave == 5)
        {
            greenRegular_spawnCount = 4;
            blueRegular_spawnCount = 4;
            yellowRegular_spawnCount = 2;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 2;
            blueFast_spawnCount = 2;
            yellowFast_spawnCount = 2;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 3;
            blueShooting_spawnCount = 4;
            //yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 6
        if (slimeWave == 6)
        {
            //greenRegular_spawnCount = 4;
            //blueRegular_spawnCount = 4;
            yellowRegular_spawnCount = 10;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 2;
            yellowFast_spawnCount = 6;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 4;
            yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            //yellowBig_spawnCount = 1;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 5f;
        }
        #endregion

        #region wave 7
        if (slimeWave == 7)
        {
            greenRegular_spawnCount = 17;
            blueRegular_spawnCount = 17;
            //yellowRegular_spawnCount = 10;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            blueFast_spawnCount = 7;
            //yellowFast_spawnCount = 4;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 3;
            blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 2;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            //blueBig_spawnCount = 1;
            yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 6f;
        }
        #endregion

        #region wave 8
        if (slimeWave == 8)
        {
            greenRegular_spawnCount = 30;
            blueRegular_spawnCount = 15;
            //yellowRegular_spawnCount = 10;
            //redRegular_spawnCount = 1;
            //purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 4;
            blueFast_spawnCount = 5;
            yellowFast_spawnCount = 1;
            //redFast_spawnCount = 1;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 1;
            blueShooting_spawnCount = 2;
            yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 1;
            blueBig_spawnCount = 2;
            yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 6.5f;
        }
        #endregion

        #region wave 9
        if (slimeWave == 9)
        {
            //greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 15;
            yellowRegular_spawnCount = 6;
            redRegular_spawnCount = 6;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 6;
            yellowFast_spawnCount = 6;
            redFast_spawnCount = 2;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 5;
            blueShooting_spawnCount = 1;
            yellowShooting_spawnCount = 1;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 2;
            blueBig_spawnCount = 2;
            yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 6.5f;
        }
        #endregion

        #region wave 10
        if (slimeWave == 10)
        {
            //greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 15;
            yellowRegular_spawnCount = 8;
            redRegular_spawnCount = 8;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 6;
            yellowFast_spawnCount = 7;
            redFast_spawnCount = 3;
            //purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 3;
            blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 4;
            blueBig_spawnCount = 3;
            yellowBig_spawnCount = 2;
            redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 7f;
        }
        #endregion

        #region wave 11
        if (slimeWave == 11)
        {
            //greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 15;
            yellowRegular_spawnCount = 12;
            redRegular_spawnCount = 12;
            //purpleRegular_spawnCount = 1;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 6;
            yellowFast_spawnCount = 7;
            redFast_spawnCount = 7;
            //purpleFast_spawnCount = 1;

            //greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 3;
            //yellowShooting_spawnCount = 3;
            //redShooting_spawnCount = 1;
            //purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 4;
            //blueBig_spawnCount = 3;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 1;
            //purpleBig_spawnCount = 1;

            currentWaveTotalTime = 7.5f;
        }
        #endregion

        #region wave 12
        if (slimeWave == 12)
        {
            //greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 15;
            yellowRegular_spawnCount = 5;
            redRegular_spawnCount = 5;
            purpleRegular_spawnCount = 5;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 6;
            yellowFast_spawnCount = 3;
            redFast_spawnCount = 3;
            purpleFast_spawnCount = 2;

            //greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 3;
            yellowShooting_spawnCount = 2;
            redShooting_spawnCount = 2;
            purpleShooting_spawnCount = 1;

            //greenBig_spawnCount = 4;
            //blueBig_spawnCount = 3;
            yellowBig_spawnCount = 2;
            redBig_spawnCount = 2;
            purpleBig_spawnCount = 2;

            currentWaveTotalTime = 7.5f;
        }
        #endregion

        #region wave 13
        if (slimeWave == 13)
        {
            //greenRegular_spawnCount = 15;
            //blueRegular_spawnCount = 15;
            //yellowRegular_spawnCount = 4;
            redRegular_spawnCount = 10;
            purpleRegular_spawnCount = 10;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 6;
            //yellowFast_spawnCount = 3;
            //redFast_spawnCount = 3;
            //purpleFast_spawnCount = 3;

            //greenShooting_spawnCount = 3;
            //blueShooting_spawnCount = 3;
            //yellowShooting_spawnCount = 2;
            redShooting_spawnCount = 3;
            purpleShooting_spawnCount = 3;

            //greenBig_spawnCount = 4;
            //blueBig_spawnCount = 3;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 2;
            purpleBig_spawnCount = 6;

            currentWaveTotalTime = 7.5f;
        }
        #endregion

        #region wave 14
        if (slimeWave == 14)
        {
            greenRegular_spawnCount = 30;
            blueRegular_spawnCount = 25;
            yellowRegular_spawnCount = 12;
            redRegular_spawnCount = 7;
            purpleRegular_spawnCount = 5;

            //greenFast_spawnCount = 2;
            //blueFast_spawnCount = 6;
            //yellowFast_spawnCount = 3;
            //redFast_spawnCount = 3;
            //purpleFast_spawnCount = 3;

            greenShooting_spawnCount = 6;
            blueShooting_spawnCount = 5;
            yellowShooting_spawnCount = 4;
            //redShooting_spawnCount = 3;
            //purpleShooting_spawnCount = 3;

            //greenBig_spawnCount = 4;
            //blueBig_spawnCount = 3;
            //yellowBig_spawnCount = 2;
            //redBig_spawnCount = 2;
            purpleBig_spawnCount = 6;

            currentWaveTotalTime = 7.5f;
        }
        #endregion

        #region wave 15
        if (slimeWave == 15)
        {
            greenRegular_spawnCount = 5;
            blueRegular_spawnCount = 4;
            yellowRegular_spawnCount = 3;
            redRegular_spawnCount = 2;
            purpleRegular_spawnCount = 1;

            greenFast_spawnCount = 5;
            blueFast_spawnCount = 4;
            yellowFast_spawnCount = 3;
            redFast_spawnCount = 2;
            purpleFast_spawnCount = 1;

            greenShooting_spawnCount = 5;
            blueShooting_spawnCount = 4;
            yellowShooting_spawnCount = 3;
            redShooting_spawnCount = 2;
            purpleShooting_spawnCount = 1;

            greenBig_spawnCount = 5;
            blueBig_spawnCount = 4;
            yellowBig_spawnCount = 3;
            redBig_spawnCount = 2;
            purpleBig_spawnCount = 1;

            currentWaveTotalTime = 11f;
        }
        #endregion

        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion

    #region Narrow gamemode - ALL waves
    public void StartNarrowGameModeWave()
    {
        SetNarrowWaves();
        isPlayingRun = true;
    }

    public void SetNarrowWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

        //These slimes should spawn on easy
        //Regular = Green, blue, yellow and red
        //Fast = Green, blue and yellow
        //Shooting = Green, blue and yellow
        //Big = Green and blue. 1-3 red or yellow at the final wave

        //Ad waves here


        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion

    #region Rampage gamemode - ALL waves
    public TextMeshProUGUI timerText, waveText;
    public static bool isRampagePlaying;

    public float stopTime;
    public float zeroTime = 0;

    public Coroutine rampageCoroutine;

    public static bool isRampageDone, isRampageStop;
    public static int rampageTimesStopped;

    private IEnumerator UpdateTimer()
    {
        zeroTime = 0;
        isInGame = true;
        stopTime = 20;
        greenShooting_spawnCount = 2;
        greenRegular_spawnCount = 22;
        currentWaveTotalTime = stopTime;
        CheckSpawnCoroutines();

        while (isRampagePlaying)
        {
            if(waveTime >= (SelectGameMode.rampage_MinuteToReach * 60) && isRampageDone == false)
            { 
                waveTime = (SelectGameMode.rampage_MinuteToReach * 60);
                isRampageDone = true;
                StopCoroutine(rampageCoroutine);
            }

            if (isRampageDone == false)
            {
                waveTime += Time.deltaTime;
                UpdateTimerDisplay();

                zeroTime += Time.deltaTime;
                if (zeroTime >= stopTime)
                {
                    zeroTime = 0;
                    isRampageStop = true;
                    rampageTimesStopped += 1;

                    Debug.Log(rampageTimesStopped);

                    SetSpawnCountToZero();

                    #region Stop 1
                    if (rampageTimesStopped == 1)
                    {
                        greenRegular_spawnCount = 9;
                        blueRegular_spawnCount = 9;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 6;
                        //blueFast_spawnCount = 1;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 2;
                        //blueShooting_spawnCount = 1;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 1;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 2
                    if (rampageTimesStopped == 2)
                    {
                        //greenRegular_spawnCount = 6;
                        blueRegular_spawnCount = 12;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 4;
                        blueFast_spawnCount = 2;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 3;
                        //blueShooting_spawnCount = 1;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 1;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 3
                    if (rampageTimesStopped == 3)
                    {
                        greenRegular_spawnCount = 30;
                        //blueRegular_spawnCount = 4;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 7;
                        //blueFast_spawnCount = 2;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 5;
                        //blueShooting_spawnCount = 1;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 1;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 4
                    if (rampageTimesStopped == 4)
                    {
                        greenRegular_spawnCount = 22;
                        blueRegular_spawnCount = 22;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 6;
                        blueFast_spawnCount = 4;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 4;
                        //blueShooting_spawnCount = 1;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 1;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 5
                    if (rampageTimesStopped == 5)
                    {
                        //greenRegular_spawnCount = 25;
                        //blueRegular_spawnCount = 25;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 4;
                        blueFast_spawnCount = 10;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 3;
                        blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 1;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 6
                    if (rampageTimesStopped == 6)
                    {
                        //greenRegular_spawnCount = 25;
                        blueRegular_spawnCount = 35;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 4;
                        blueFast_spawnCount = 8;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 4;
                        blueShooting_spawnCount = 3;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 1;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 7
                    if (rampageTimesStopped == 7)
                    {
                        greenRegular_spawnCount = 35;
                        //blueRegular_spawnCount = 35;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 4;
                        blueFast_spawnCount = 12;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 4;
                        //blueShooting_spawnCount = 4;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 6;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 8
                    if (rampageTimesStopped == 8)
                    {
                        greenRegular_spawnCount = 5;
                        //blueRegular_spawnCount = 35;
                        //yellowRegular_spawnCount = 1;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 6;
                        //blueFast_spawnCount = 12;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 4;
                        blueShooting_spawnCount = 3;
                        //yellowShooting_spawnCount = 1;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 10;
                        //blueBig_spawnCount = 1;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 9
                    if (rampageTimesStopped == 9)
                    {
                        greenRegular_spawnCount = 5;
                        //blueRegular_spawnCount = 35;
                        yellowRegular_spawnCount = 3;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 8;
                        blueFast_spawnCount = 8;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 2;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 4;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 4;
                        blueBig_spawnCount = 4;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 10
                    if (rampageTimesStopped == 10)
                    {
                        greenRegular_spawnCount = 15;
                        blueRegular_spawnCount = 35;
                        yellowRegular_spawnCount = 15;
                        //redRegular_spawnCount = 1;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 16;
                        blueFast_spawnCount = 2;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 2;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 3;
                        blueBig_spawnCount = 3;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 11
                    if (rampageTimesStopped == 11)
                    {
                        greenRegular_spawnCount = 4;
                        blueRegular_spawnCount = 3;
                        yellowRegular_spawnCount = 3;
                        redRegular_spawnCount = 6;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 15;
                        //blueFast_spawnCount = 2;
                        //yellowFast_spawnCount = 1;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 4;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 3;
                        blueBig_spawnCount = 6;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 12
                    if (rampageTimesStopped == 12)
                    {
                        greenRegular_spawnCount = 6;
                        blueRegular_spawnCount = 5;
                        yellowRegular_spawnCount = 5;
                        redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 10;
                        //blueFast_spawnCount = 2;
                        yellowFast_spawnCount = 7;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 4;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 3;
                        blueBig_spawnCount = 3;
                        //yellowBig_spawnCount = 1;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 13
                    if (rampageTimesStopped == 13)
                    {
                        greenRegular_spawnCount = 65;
                        //blueRegular_spawnCount = 5;
                        //yellowRegular_spawnCount = 5;
                        //redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 5;
                        blueFast_spawnCount = 5;
                        yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 5;
                        //blueShooting_spawnCount = 3;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 2;
                        blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 2;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 14
                    if (rampageTimesStopped == 14)
                    {
                        greenRegular_spawnCount = 50;
                        blueRegular_spawnCount = 35;
                        yellowRegular_spawnCount = 35;
                        //redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 5;
                        //blueFast_spawnCount = 5;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 6;
                        //blueShooting_spawnCount = 3;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 2;
                        //blueBig_spawnCount = 2;
                        //yellowBig_spawnCount = 2;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 15
                    if (rampageTimesStopped == 15)
                    {
                        greenRegular_spawnCount = 10;
                        blueRegular_spawnCount = 10;
                        yellowRegular_spawnCount = 10;
                        //redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 5;
                        blueFast_spawnCount = 16;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 6;
                        blueShooting_spawnCount = 3;
                        yellowShooting_spawnCount = 3;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 8;
                        //blueBig_spawnCount = 2;
                        //yellowBig_spawnCount = 2;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 16
                    if (rampageTimesStopped == 16)
                    {
                        greenRegular_spawnCount = 5;
                        blueRegular_spawnCount = 5;
                        yellowRegular_spawnCount = 5;
                        //redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 20;
                        blueFast_spawnCount = 20;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 6;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 2;
                        blueBig_spawnCount = 3;
                        yellowBig_spawnCount = 4;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 17
                    if (rampageTimesStopped == 17)
                    {
                        greenRegular_spawnCount = 8;
                        blueRegular_spawnCount = 8;
                        yellowRegular_spawnCount = 8;
                        //redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 6;
                        blueFast_spawnCount = 9;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 10;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 3;
                        blueBig_spawnCount = 6;
                        yellowBig_spawnCount = 3;
                        //redBig_spawnCount = 1;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 18
                    if (rampageTimesStopped == 18)
                    {
                        greenRegular_spawnCount = 8;
                        blueRegular_spawnCount = 8;
                        yellowRegular_spawnCount = 8;
                        redRegular_spawnCount = 8;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 17;
                        //blueFast_spawnCount = 9;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 4;
                        blueShooting_spawnCount = 3;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 2;
                        blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 2;
                        redBig_spawnCount = 2;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 19
                    if (rampageTimesStopped == 19)
                    {
                        greenRegular_spawnCount = 20;
                        blueRegular_spawnCount = 20;
                        yellowRegular_spawnCount = 20;
                        redRegular_spawnCount = 20;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 17;
                        //blueFast_spawnCount = 9;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 2;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 2;
                        //blueBig_spawnCount = 2;
                        //yellowBig_spawnCount = 2;
                        redBig_spawnCount = 5;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 20
                    if (rampageTimesStopped == 20)
                    {
                        greenRegular_spawnCount = 100;
                        //blueRegular_spawnCount = 20;
                        //yellowRegular_spawnCount = 20;
                        // redRegular_spawnCount = 20;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 17;
                        //blueFast_spawnCount = 9;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 2;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 2;
                        //blueBig_spawnCount = 2;
                        //yellowBig_spawnCount = 2;
                        //redBig_spawnCount = 5;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 21
                    if (rampageTimesStopped == 21)
                    {
                        greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        yellowRegular_spawnCount = 40;
                        // redRegular_spawnCount = 20;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 17;
                        //blueFast_spawnCount = 9;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        //greenShooting_spawnCount = 2;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 2;
                        //blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 6;
                        redBig_spawnCount = 6;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 22
                    if (rampageTimesStopped == 22)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        //yellowRegular_spawnCount = 40;
                        redRegular_spawnCount = 40;
                        //purpleRegular_spawnCount = 1;

                        //greenFast_spawnCount = 17;
                        blueFast_spawnCount = 8;
                        yellowFast_spawnCount = 8;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount = 5;
                        blueShooting_spawnCount = 3;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 2;
                        //blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 6;
                        redBig_spawnCount = 6;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 22
                    if (rampageTimesStopped == 22)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        //yellowRegular_spawnCount = 40;
                        redRegular_spawnCount = 45;
                        //purpleRegular_spawnCount = 1;

                        greenFast_spawnCount = 17;
                        blueFast_spawnCount = 8;
                        yellowFast_spawnCount = 8;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 1;

                        greenShooting_spawnCount =2;
                        blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 2;
                        blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 2;
                        //redBig_spawnCount = 6;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 23
                    if (rampageTimesStopped == 23)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        //yellowRegular_spawnCount = 40;
                        redRegular_spawnCount = 10;
                        purpleRegular_spawnCount = 8;

                        greenFast_spawnCount = 3;
                        blueFast_spawnCount = 3;
                        yellowFast_spawnCount = 3;
                        //redFast_spawnCount = 1;
                        purpleFast_spawnCount = 3;

                        greenShooting_spawnCount = 3;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 7;
                        //blueBig_spawnCount = 2;
                        //yellowBig_spawnCount = 2;
                        //redBig_spawnCount = 6;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 24
                    if (rampageTimesStopped == 24)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        //yellowRegular_spawnCount = 40;
                        redRegular_spawnCount = 2;
                        purpleRegular_spawnCount = 2;

                        greenFast_spawnCount = 2;
                        blueFast_spawnCount = 2;
                        yellowFast_spawnCount = 2;
                        //redFast_spawnCount = 1;
                        purpleFast_spawnCount = 2;

                        greenShooting_spawnCount = 2;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 6;
                        blueBig_spawnCount = 7;
                        yellowBig_spawnCount = 6;
                        redBig_spawnCount = 4;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 25
                    if (rampageTimesStopped == 25)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        //yellowRegular_spawnCount = 40;
                        redRegular_spawnCount = 6;
                        purpleRegular_spawnCount = 6;

                        greenFast_spawnCount = 10;
                        blueFast_spawnCount = 20;
                        yellowFast_spawnCount = 3;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 2;

                        greenShooting_spawnCount = 7;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 2;
                        blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 2;
                        redBig_spawnCount = 2;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 26
                    if (rampageTimesStopped == 26)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        yellowRegular_spawnCount = 55;
                        redRegular_spawnCount = 3;
                        purpleRegular_spawnCount = 3;

                        greenFast_spawnCount = 25;
                        blueFast_spawnCount = 5;
                        yellowFast_spawnCount = 10;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 2;

                        greenShooting_spawnCount = 2;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 3;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 2;
                        blueBig_spawnCount = 2;
                        yellowBig_spawnCount = 2;
                        redBig_spawnCount = 2;
                        //purpleBig_spawnCount = 1;
                    }
                    #endregion

                    #region Stop 27
                    if (rampageTimesStopped == 27)
                    {
                        //greenRegular_spawnCount = 40;
                        //blueRegular_spawnCount = 20;
                        yellowRegular_spawnCount = 5;
                        redRegular_spawnCount = 5;
                        purpleRegular_spawnCount = 5;

                        greenFast_spawnCount = 5;
                        blueFast_spawnCount = 5;
                        yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 2;

                        greenShooting_spawnCount = 2;
                        blueShooting_spawnCount = 2;
                        yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 4;
                        blueBig_spawnCount = 4;
                        yellowBig_spawnCount = 4;
                        redBig_spawnCount = 4;
                        purpleBig_spawnCount = 4;
                    }
                    #endregion

                    #region Stop 28
                    if (rampageTimesStopped == 28)
                    {
                        greenRegular_spawnCount = 100;
                        blueRegular_spawnCount = 100;
                        yellowRegular_spawnCount = 100;
                        redRegular_spawnCount = 15;
                        purpleRegular_spawnCount = 15;

                        greenFast_spawnCount = 15;
                        blueFast_spawnCount = 15;
                        //yellowFast_spawnCount = 5;
                        //redFast_spawnCount = 1;
                        //purpleFast_spawnCount = 2;

                        //greenShooting_spawnCount = 2;
                        //blueShooting_spawnCount = 2;
                        //yellowShooting_spawnCount = 2;
                        //redShooting_spawnCount = 1;
                        //purpleShooting_spawnCount = 1;

                        greenBig_spawnCount = 10;
                        //blueBig_spawnCount = 4;
                        //yellowBig_spawnCount = 4;
                        //redBig_spawnCount = 4;
                        //purpleBig_spawnCount = 4;
                    }
                    #endregion

                    #region Stop 29
                    if (rampageTimesStopped == 29)
                    {
                        greenRegular_spawnCount = 45;
                        blueRegular_spawnCount = 35;
                        yellowRegular_spawnCount = 25;
                        redRegular_spawnCount = 15;
                        purpleRegular_spawnCount = 5;

                        greenFast_spawnCount = 10;
                        blueFast_spawnCount = 10;
                        yellowFast_spawnCount = 10;
                        redFast_spawnCount = 10;
                        //purpleFast_spawnCount = 2;

                        //greenShooting_spawnCount = 2;
                        blueShooting_spawnCount = 3;
                        yellowShooting_spawnCount = 3;
                        redShooting_spawnCount = 3;
                        //purpleShooting_spawnCount = 1;

                        //greenBig_spawnCount = 10;
                        //blueBig_spawnCount = 4;
                        //yellowBig_spawnCount = 4;
                        //redBig_spawnCount = 4;
                        //purpleBig_spawnCount = 4;
                    }
                    #endregion

                    CheckSpawnCoroutines();
                }
            }

             yield return null; // Wait for next frame
        }
    }
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(waveTime / 60);
        int seconds = Mathf.FloorToInt(waveTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}"; 
    }

    public void StartRampageGameModeWave()
    {
        isRampagePlaying = true;
        rampageCoroutine = StartCoroutine(UpdateTimer());
        timerText.gameObject.SetActive(true);

        //SetRampageWaves();
        isPlayingRun = true;
    }

    public void SetRampageWaves()
    {
        if (slimeWave == 0) { slimeWave = 1; }
        else { slimeWave += 1; }

        //Set all to 0 before starting a wave
        SetSpawnCountToZero();

       


        bool testWave = false;

        #region test wave
        if (testWave == true)
        {
            greenShooting_spawnCount = 2;
        }
        #endregion

        CheckTotalWaveHealth();

        TotalSlimesToSpawn();

        CheckSpawnCoroutines();

        isInGame = true;
    }
    #endregion


    #region Check spawn and coroutine
    public void CheckSpawnCoroutines()
    {
        //Regular slimes
        if (isGreenBasicSpawning == false && greenRegular_spawnCount > 0) { StartCoroutine(SpawnGreenSlime_basic()); isGreenBasicSpawning = true; }
        if (isBlueRegularSpawning == false && blueRegular_spawnCount > 0) { StartCoroutine(SpawnBlueSlime_regular()); isBlueRegularSpawning = true; }
        if (isYellowRegularSpawning == false && yellowRegular_spawnCount > 0) { StartCoroutine(SpawnYellowSlime_regular()); isYellowRegularSpawning = true; }
        if (isRedRegularSpawning == false && redRegular_spawnCount > 0) { StartCoroutine(SpawnRedSlime_regular()); isRedRegularSpawning = true; }
        if (isPurpleRegularSpawning == false && purpleRegular_spawnCount > 0) { StartCoroutine(SpawnPurpleSlime_regular()); isPurpleRegularSpawning = true; }

        //Fast slimes
        if (isGreenFastSpawning == false && greenFast_spawnCount > 0) { StartCoroutine(SpawnGreenSlime_fast()); isGreenFastSpawning = true; }
        if (isBlueFastSpawning == false && blueFast_spawnCount > 0) { StartCoroutine(SpawnBlueSlime_fast()); isBlueFastSpawning = true; }
        if (isYellowFastSpawning == false && yellowFast_spawnCount > 0) { StartCoroutine(SpawnYellowSlime_fast()); isYellowFastSpawning = true; }
        if (isRedFastSpawning == false && redFast_spawnCount > 0) { StartCoroutine(SpawnRedSlime_fast()); isRedFastSpawning = true; }
        if (isPurpleFastSpawning == false && purpleFast_spawnCount > 0) { StartCoroutine(SpawnPurpleSlime_fast()); isPurpleFastSpawning = true; }

        //Shooting slimes
        if (isGreenShootingSpawning == false && greenShooting_spawnCount > 0) { StartCoroutine(SpawnGreenSlime_shooting()); isGreenShootingSpawning = true; }
        if (blueShootingSpawning == false && blueShooting_spawnCount > 0) { StartCoroutine(SpawnBlueSlime_shooting()); blueShootingSpawning = true; }
        if (isYellowShootingSpawning == false && yellowShooting_spawnCount > 0) { StartCoroutine(SpawnYellowSlime_shooting()); isYellowShootingSpawning = true; }
        if (isRedShootingSpawning == false && redShooting_spawnCount > 0) { StartCoroutine(SpawnRedSlime_shooting()); isRedShootingSpawning = true; }
        if (isPurpleShootingSpawning == false && purpleShooting_spawnCount > 0) { StartCoroutine(SpawnPurpleSlime_shooting()); isPurpleShootingSpawning = true; }

        //Big slimes
        if (isGreenBigSpawning == false && greenBig_spawnCount > 0) { StartCoroutine(SpawnGreenSlime_big()); isGreenBigSpawning = true; }
        if (isBlueBigSpawning == false && blueBig_spawnCount > 0) { StartCoroutine(SpawnBlueSlime_big()); isBlueBigSpawning = true; }
        if (isYellowBigSpawning == false && yellowBig_spawnCount > 0) { StartCoroutine(SpawnYellowSlime_big()); isYellowBigSpawning = true; }
        if (redBigSpawning == false && redBig_spawnCount > 0) { StartCoroutine(SpawnRedSlime_big()); redBigSpawning = true; }
        if (isPurpleBigSpawning == false && purpleBig_spawnCount > 0) { StartCoroutine(SpawnPurpleSlime_big()); isPurpleBigSpawning = true; }
    }
    #endregion

    //All regular slimes
    #region Green slime - REGULAR
    public static int greenBasicSpawned;
    public Coroutine basicGreenCoroutine;
    public bool isGreenBasicSpawning;

    IEnumerator SpawnGreenSlime_basic()
    {
        while (true)
        {
            if (greenRegular_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / greenRegular_spawnCount); }
            SpawnGreenSlime();
        }
    }

    public void SpawnGreenSlime()
    {
        if(greenRegular_spawnCount == 0) { return; }
        if(StrawberryMechanics.isInDeathFrame == true) { return; }

        if (greenBasicSpawned < greenRegular_spawnCount) 
        {
            GameObject slime = ObjectPool.instance.GetSlime1FromPool();
            greenBasicSpawned += 1;
        }
    }
    #endregion

    #region Blue slime - REGULAR
    public static int blueRegularSpawned;
    public Coroutine regularBlueCoroutine;
    public bool isBlueRegularSpawning;

    IEnumerator SpawnBlueSlime_regular()
    {
        while (true)
        {
            if (blueRegular_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / blueRegular_spawnCount); }
            SpawnBlueSlime();
        }
    }

    public void SpawnBlueSlime()
    {
        if (blueRegular_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (blueRegularSpawned < blueRegular_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetRegularBlueFromPool();
            blueRegularSpawned += 1;
        }
    }
    #endregion

    #region Yellow slime - REGULAR
    public static int yellowRegularSpawned;
    public Coroutine regularYellowCoroutine;
    public bool isYellowRegularSpawning;

    IEnumerator SpawnYellowSlime_regular()
    {
        while (true)
        {
            if (yellowRegular_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / yellowRegular_spawnCount); }
            SpawnYellowSlime();
        }
    }

    public void SpawnYellowSlime()
    {
        if (yellowRegular_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (yellowRegularSpawned < yellowRegular_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetRegularYellowFromPool();
            yellowRegularSpawned += 1;
        }
    }
    #endregion

    #region Red slime - REGULAR
    public static int redRegularSpawned;
    public Coroutine regularRedCoroutine;
    public bool isRedRegularSpawning;

    IEnumerator SpawnRedSlime_regular()
    {
        while (true)
        {
            if (redRegular_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / redRegular_spawnCount); }
            SpawnRedSlime();
        }
    }

    public void SpawnRedSlime()
    {
        if (redRegular_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (redRegularSpawned < redRegular_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetRegularRedFromPool();
            redRegularSpawned += 1;
        }
    }
    #endregion

    #region Spawn Regular Purple Slime
    public static int purpleRegularSpawned;
    public Coroutine regularPurpleCoroutine;
    public bool isPurpleRegularSpawning;

    IEnumerator SpawnPurpleSlime_regular()
    {
        while (true)
        {
            if (purpleRegular_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / purpleRegular_spawnCount); }
            SpawnPurpleSlime();
        }
    }

    public void SpawnPurpleSlime()
    {
        if (purpleRegular_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (purpleRegularSpawned < purpleRegular_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetRegularPurpleFromPool();
            purpleRegularSpawned += 1;
        }
    }
    #endregion

    //All fast slimes
    #region Spawn Green Fast Slime
    public static int greenFastSpawned;
    public Coroutine fastGreenCoroutine;
    public bool isGreenFastSpawning;

    IEnumerator SpawnGreenSlime_fast()
    {
        while (true)
        {
            if (greenFast_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / greenFast_spawnCount); }
            SpawnGreenSlimeFast();
        }
    }

    public void SpawnGreenSlimeFast()
    {
        if (greenFast_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (greenFastSpawned < greenFast_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetFastGreenFromPool();
            greenFastSpawned += 1;
        }
    }
    #endregion

    #region Spawn Blue Fast Slime
    public static int blueFastSpawned;
    public Coroutine fastBlueCoroutine;
    public bool isBlueFastSpawning;

    IEnumerator SpawnBlueSlime_fast()
    {
        while (true)
        {
            if (blueFast_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / blueFast_spawnCount); }
            SpawnBlueSlimeFast();
        }
    }

    public void SpawnBlueSlimeFast()
    {
        if (blueFast_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (blueFastSpawned < blueFast_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetFastBlueFromPool();
            blueFastSpawned += 1;
        }
    }
    #endregion

    #region Spawn Yellow Fast Slime
    public static int yellowFastSpawned;
    public Coroutine fastYellowCoroutine;
    public bool isYellowFastSpawning;

    IEnumerator SpawnYellowSlime_fast()
    {
        while (true)
        {
            if (yellowFast_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / yellowFast_spawnCount); }
            SpawnYellowSlimeFast();
        }
    }

    public void SpawnYellowSlimeFast()
    {
        if (yellowFast_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (yellowFastSpawned < yellowFast_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetFastYellowFromPool();
            yellowFastSpawned += 1;
        }
    }
    #endregion

    #region Spawn Red Fast Slime
    public static int redFastSpawned;
    public Coroutine fastRedCoroutine;
    public bool isRedFastSpawning;

    IEnumerator SpawnRedSlime_fast()
    {
        while (true)
        {
            if (redFast_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / redFast_spawnCount); }
            SpawnRedSlimeFast();
        }
    }

    public void SpawnRedSlimeFast()
    {
        if (redFast_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (redFastSpawned < redFast_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetFastRedFromPool();
            redFastSpawned += 1;
        }
    }
    #endregion

    #region Spawn Purple Fast Slime
    public static int purpleFastSpawned;
    public Coroutine fastPurpleCoroutine;
    public bool isPurpleFastSpawning;

    IEnumerator SpawnPurpleSlime_fast()
    {
        while (true)
        {
            if (purpleFast_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / purpleFast_spawnCount); }
            SpawnPurpleSlimeFast();
        }
    }

    public void SpawnPurpleSlimeFast()
    {
        if (purpleFast_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (purpleFastSpawned < purpleFast_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetFastPurpleFromPool();
            purpleFastSpawned += 1;
        }
    }
    #endregion

    //All shooting slimes
    #region Spawn Green Shooting Slime
    public static int greenShootingSpawned;
    public Coroutine shootingGreenCoroutine;
    public bool isGreenShootingSpawning;

    IEnumerator SpawnGreenSlime_shooting()
    {
        while (true)
        {
            if (greenShooting_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / greenShooting_spawnCount); }
         
            SpawnGreenSlimeShooting();
        }
    }

    public void SpawnGreenSlimeShooting()
    {

        if (greenShooting_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (greenShootingSpawned < greenShooting_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetShootingGreenFromPool();
            greenShootingSpawned += 1;
        }
    }
    #endregion

    #region Spawn blue slime - shooting
    public Coroutine blueShootingCoroutine;
    public static int blueShootingSpawned;
    public bool blueShootingSpawning;

    IEnumerator SpawnBlueSlime_shooting()
    {
        while (true)
        {
            if (blueShooting_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / blueShooting_spawnCount); }
            SpawnBlueShootingSlime();
        }
    }

    public void SpawnBlueShootingSlime()
    {
        if (blueShooting_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (blueShootingSpawned < blueShooting_spawnCount)
        {
            GameObject blueShootingSlime = ObjectPool.instance.GetBlueShootingFromPool();
            blueShootingSpawned += 1;
        }
    }
    #endregion

    #region Spawn Yellow Shooting Slime
    public static int yellowShootingSpawned;
    public Coroutine shootingYellowCoroutine;
    public bool isYellowShootingSpawning;

    IEnumerator SpawnYellowSlime_shooting()
    {
        while (true)
        {
            if (yellowShooting_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / yellowShooting_spawnCount); }
            SpawnYellowSlimeShooting();
        }
    }

    public void SpawnYellowSlimeShooting()
    {
        if (yellowShooting_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (yellowShootingSpawned < yellowShooting_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetShootingYellowFromPool();
            yellowShootingSpawned += 1;
        }
    }
    #endregion

    #region Spawn Red Shooting Slime
    public static int redShootingSpawned;
    public Coroutine shootingRedCoroutine;
    public bool isRedShootingSpawning;

    IEnumerator SpawnRedSlime_shooting()
    {
        while (true)
        {
            if (redShooting_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / redShooting_spawnCount); }
            SpawnRedSlimeShooting();
        }
    }

    public void SpawnRedSlimeShooting()
    {
        if (redShooting_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (redShootingSpawned < redShooting_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetShootingRedFromPool();
            redShootingSpawned += 1;
        }
    }
    #endregion

    #region Spawn Purple Shooting Slime
    public static int purpleShootingSpawned;
    public Coroutine shootingPurpleCoroutine;
    public bool isPurpleShootingSpawning;

    IEnumerator SpawnPurpleSlime_shooting()
    {
        while (true)
        {
            if (purpleShooting_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / purpleShooting_spawnCount); }
            SpawnPurpleSlimeShooting();
        }
    }

    public void SpawnPurpleSlimeShooting()
    {
        if (purpleShooting_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (purpleShootingSpawned < purpleShooting_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetShootingPurpleFromPool();
            purpleShootingSpawned += 1;
        }
    }
    #endregion

    //All big slimes
    #region Spawn Green Big Slime
    public static int greenBigSpawned;
    public Coroutine bigGreenCoroutine;
    public bool isGreenBigSpawning;

    IEnumerator SpawnGreenSlime_big()
    {
        while (true)
        {
            if (greenBig_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / greenBig_spawnCount); }
            SpawnGreenSlimeBig();
        }
    }

    public void SpawnGreenSlimeBig()
    {
        if (greenBig_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (greenBigSpawned < greenBig_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetBigGreenFromPool();
            greenBigSpawned += 1;
        }
    }
    #endregion

    #region Spawn Blue Big Slime
    public static int blueBigSpawned;
    public Coroutine bigBlueCoroutine;
    public bool isBlueBigSpawning;

    IEnumerator SpawnBlueSlime_big()
    {
        while (true)
        {
            if (blueBig_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / blueBig_spawnCount); }
            SpawnBlueSlimeBig();
        }
    }

    public void SpawnBlueSlimeBig()
    {
        if (blueBig_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (blueBigSpawned < blueBig_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetBigBlueFromPool();
            blueBigSpawned += 1;
        }
    }
    #endregion

    #region Spawn Yellow Big Slime
    public static int yellowBigSpawned;
    public Coroutine bigYellowCoroutine;
    public bool isYellowBigSpawning;

    IEnumerator SpawnYellowSlime_big()
    {
        while (true)
        {
            if (yellowBig_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / yellowBig_spawnCount); }
            SpawnYellowSlimeBig();
        }
    }

    public void SpawnYellowSlimeBig()
    {
        if (yellowBig_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (yellowBigSpawned < yellowBig_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetBigYellowFromPool();
            yellowBigSpawned += 1;
        }
    }
    #endregion

    #region Spawn red big slime
    public Coroutine redBigCoroutine;
    public static int redBigSpawned;
    public bool redBigSpawning;

    IEnumerator SpawnRedSlime_big()
    {
        while (true)
        {
            if (redBig_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / redBig_spawnCount); }
            SpawnRedBigSlime();
        }
    }

    public void SpawnRedBigSlime()
    {
        if (redBig_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (redBigSpawned < redBig_spawnCount)
        {
            GameObject redBigSlime = ObjectPool.instance.GetRedBigFromPool();
            redBigSpawned += 1;
        }
    }
    #endregion

    #region Spawn Purple Big Slime
    public static int purpleBigSpawned;
    public Coroutine bigPurpleCoroutine;
    public bool isPurpleBigSpawning;

    IEnumerator SpawnPurpleSlime_big()
    {
        while (true)
        {
            if (purpleBig_spawnCount == 0) { yield return new WaitForSeconds(0.1f); }
            else { yield return new WaitForSeconds(currentWaveTotalTime / purpleBig_spawnCount); }
            SpawnPurpleSlimeBig();
        }
    }

    public void SpawnPurpleSlimeBig()
    {
        if (purpleBig_spawnCount == 0) { return; }
        if (StrawberryMechanics.isInDeathFrame == true) { return; }

        if (purpleBigSpawned < purpleBig_spawnCount)
        {
            GameObject slime = ObjectPool.instance.GetBigPurpleFromPool();
            purpleBigSpawned += 1;
        }
    }
    #endregion

    //Total slimes this wave to be spawned
    #region total slimes to spawn
    public void TotalSlimesToSpawn()
    {
        //Total slimes that will spawn this round
        slimesWaveSpawnCount = 
        greenRegular_spawnCount + blueRegular_spawnCount + yellowRegular_spawnCount + redRegular_spawnCount + purpleRegular_spawnCount +
        greenShooting_spawnCount + blueShooting_spawnCount + yellowShooting_spawnCount + redShooting_spawnCount + purpleShooting_spawnCount +
        greenFast_spawnCount + blueFast_spawnCount + yellowFast_spawnCount + redFast_spawnCount + purpleFast_spawnCount +
        greenBig_spawnCount + blueBig_spawnCount + yellowBig_spawnCount + redBig_spawnCount + purpleBig_spawnCount;
    }
    #endregion

    #region choose what to spawn
    public void SpawnSpecific(int slimesToSpawn, int slimeSpawnCount, bool spawnOnly1)
    {
        if(spawnOnly1 == true) 
        {
            if (slimesToSpawn == 0) { greenRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 1) { blueRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 2) { yellowRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 3) { redRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 4) { purpleRegular_spawnCount = slimeSpawnCount; }

            if (slimesToSpawn == 5) { greenFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 6) { blueFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 7) { yellowFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 8) { redFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 9) { purpleFast_spawnCount = slimeSpawnCount; }

            if (slimesToSpawn == 10) { greenShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 11) { blueShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 12) { yellowShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 13) { redShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 14) { purpleShooting_spawnCount = slimeSpawnCount; }

            if (slimesToSpawn == 15) { greenBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 16) { blueBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 17) { yellowBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 18) { redBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn == 19) { purpleBig_spawnCount = slimeSpawnCount; }
        }
        else
        {
            if (slimesToSpawn > 0) { greenRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 1) { blueRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 2) { yellowRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 3) { redRegular_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 4) { purpleRegular_spawnCount = slimeSpawnCount; }

            if (slimesToSpawn > 5) { greenFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 6) { blueFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 7) { yellowFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 8) { redFast_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 9) { purpleFast_spawnCount = slimeSpawnCount; }

            if (slimesToSpawn > 10) { greenShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 11) { blueShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 12) { yellowShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 13) { redShooting_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 14) { purpleShooting_spawnCount = slimeSpawnCount; }

            if (slimesToSpawn > 15) { greenBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 16) { blueBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 17) { yellowBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 18) { redBig_spawnCount = slimeSpawnCount; }
            if (slimesToSpawn > 19) { purpleBig_spawnCount = slimeSpawnCount; }
        }
    }
    #endregion

    public void CheckTotalWaveHealth()
    {
        float health = 0;

        health += (greenRegular_spawnCount * greenRegular_health);
        health += (blueRegular_spawnCount * blueRegular_health);
        health += (yellowRegular_spawnCount * yellowRegular_health);
        health += (redRegular_spawnCount * redRegular_health);
        health += (purpleRegular_spawnCount * purpleRegular_health);

        health += (greenShooting_spawnCount * greenShooting_health);
        health += (blueShooting_spawnCount * blueShooting_health);
        health += (yellowShooting_spawnCount * yellowShooting_health);
        health += (redShooting_spawnCount * redShooting_health);
        health += (purpleShooting_spawnCount * purpleShooting_health);

        health += (greenFast_spawnCount * greenFast_health);
        health += (blueFast_spawnCount * blueFast_health);
        health += (yellowFast_spawnCount * yellowFast_health);
        health += (redFast_spawnCount * redFast_health);
        health += (purpleFast_spawnCount * purpleFast_health);

        health += (greenBig_spawnCount * greenBig_health);
        health += (blueBig_spawnCount * blueBig_health);
        health += (yellowBig_spawnCount * yellowBig_health);
        health += (redBig_spawnCount * redBig_health);
        health += (purpleBig_spawnCount * purpleBig_health);

        Debug.Log($"Wave {slimeWave} has a total of {health} health");
    }

    public void AllSpawnDoThis()
    {

    }

    #region Load Data
    public void LoadData(GameData data)
    {
        slimeWave = data.slimeWave;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.slimeWave = slimeWave;
    }
    #endregion
}
