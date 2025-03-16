using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicEffectGroup;
    [SerializeField] private AudioMixerGroup soundEffectsGroup;

    public static AudioManager Instance;
    public Sounds[] sounds;

    private Dictionary<string, float> musicPlaybackPositions = new Dictionary<string, float>();

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        PlaySong(1);
    }

    public void Awake()
    {
        Instance = this;

        StartCoroutine(Wait());

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.Loop;

            switch (s.audioType)
            {
                case Sounds.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectsGroup;
                    break;

                case Sounds.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicEffectGroup;
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (Sounds s in sounds)
        {
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound clip not found: " + name);
            return;
        }

        if (s.source.isPlaying)
        {
            //s.playbackPosition = s.source.time; // Store the playback position in the Sounds object
            s.source.Stop();
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == name);

        if(name == "HoverUI_1")
        {
            s.pitch = Random.Range(0.9f, 1.1f);
        }
        else if (name == "SwordSlash")
        {
            s.pitch = Random.Range(1.2f, 1.7f);
        }
        else if (name == "arrow1")
        {
            s.pitch = Random.Range(0.85f, 1.15f);
        }
        else if (name == "MenuAppear")
        {
            s.pitch = Random.Range(0.85f, 1.15f);
        }
        else if (name == "MenuAppear")
        {
            s.pitch = Random.Range(0.9f, 1.1f);
            s.volume = Random.Range(0.28f, 0.3f);
        }
        else if (name == "Katana")
        {
            s.pitch = Random.Range(0.9f, 1.1f);
        }

        if (s == null)
        {
            Debug.LogWarning("Sound clip not found: " + name);
            return;
        }

        s.source.Play();

        if (name == "Music1" || name == "Music2" || name == "Music3")
        {
            StartCoroutine(WaitForSongToEnd(name));
            StartCoroutine(MonitorAudioClipEnd(name, s.source.clip.length));
        }
    }

    public int songNumber;
    IEnumerator WaitForSongToEnd(string songName)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == songName);

        if (s == null)
        {
            Debug.LogWarning("Sound clip not found: " + songName);
            yield break;
        }

        //Debug.Log(songName + " is playing!");

        while (s.source.isPlaying)
        {
            yield return null;
        }

        // The song has ended, replay the song, or play different one

        PlaySong(songNumber);
    }

    IEnumerator MonitorAudioClipEnd(string clipName, float clipLength)
    {
        float endTime = Time.time + clipLength;

        while (Time.time < endTime)
        {
            yield return null;
        }
    }

    //SettingsOptions.musicLength = s.source.clip.length;
    //s.source.time = s.playbackPosition; // Set the stored playback position

    public void UpdateMixerVolume()
    {
        musicEffectGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        soundEffectsGroup.audioMixer.SetFloat("Audio Volume", Mathf.Log10(AudioOptionsManager.soundEffectolume) * 20);
    }

    public void ChangeVolume(string name, float newVolume)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == name);

        if (s == null)
        {
            Debug.LogWarning("Sound clip not found: " + name);
            return;
        }

        s.volume = Mathf.Clamp01(newVolume); // Ensure the new volume is between 0 and 1
        s.source.volume = s.volume;
        s.source.volume = s.volume;
    }

    public Coroutine waitForSongCoroutine;
    public static bool music1Playing, music2Playing, music3Playing;

    public void PlaySong(int songNumber)
    {
        if(songNumber == 1) { Play("MainMenuMusic"); }
        if (songNumber == 2) { Play("MainMenuMusic"); }
    }

    public static float musicLength;

    IEnumerator CheckSongPlaying(int songNumber)
    {
        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;

        while (elapsedTime < musicLength)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;

            Debug.Log("Time Remaining: " + (musicLength - elapsedTime).ToString("F2") + " seconds");

            yield return null; // Yielding null makes it wait for the next frame
        }

        //Debug.Log("Song Ended");

        music1Playing = false; music2Playing = false; music3Playing = false;
        int randomSong;

        do
        {
            randomSong = Random.Range(1, 4); // Generates a number between 1 and 5
        } while (randomSong == songNumber);

        if (randomSong == 1) { Play("Music1"); music1Playing = true; }
        if (randomSong == 2) { Play("Music2"); music2Playing = true; }
        if (randomSong == 3) { Play("Music3"); music3Playing = true; }

        PlayNewsong(randomSong);

        waitForSongCoroutine = null;
    }

    public void PlayNewsong(int songNumber)
    {
        StartCoroutine(CheckSongPlaying(songNumber));
    }

    public void StopSongTimer()
    {
        if (waitForSongCoroutine != null)
        {
            StopCoroutine(waitForSongCoroutine);
            waitForSongCoroutine = null;
        }
    }
}
