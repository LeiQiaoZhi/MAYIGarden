using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    public bool playOnAwake = false;
    [HideInInspector]
    public AudioSource source;
    public AudioMixerGroup output;
}

[System.Serializable]
public class SoundSet
{
    public string name;
    public List<AudioClip> clips;
    [HideInInspector] public AudioSource source;
    public AudioMixerGroup output;
}

// SINGLETON
public class AudioManager : MonoBehaviour
{
    private const string MusicVolumeParameterName = "Volume";
    [Tooltip("Just an empty child object. Script will add AudioSource to it")]
    public GameObject audioSourceHolder; // where audio sources are attached
    public List<Sound> soundBank = new List<Sound>();
    public List<SoundSet> soundSets = new List<SoundSet>();
    public List<Sound> bgms = new List<Sound>();

    public static AudioManager Instance;

    private Sound _playingMusic;

    private void Awake()
    {
        // Keep between scenes
        DontDestroyOnLoad(gameObject);

        // init singleton pattern
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            // we already have an AudioManager in the scene
            Destroy(gameObject);
        }

        // init audio source
        foreach (Sound s in soundBank)
        {
            s.source = audioSourceHolder.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.output;
        }
        foreach(SoundSet set in soundSets)
        {
            set.source = audioSourceHolder.AddComponent<AudioSource>();
            set.source.outputAudioMixerGroup = set.output;
        }
        foreach(Sound s in bgms)
        {
            s.source = audioSourceHolder.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = soundBank.Find(s => s.name == name);
        if(sound!=null)
        {
            sound.source.Play();
        }
    }

    public void PlayRandomFromSoundSet(string setName)
    {
        SoundSet set = soundSets.Find(s => s.name == setName);
        if (set != null)
        {
            AudioClip clip = set.clips[Random.Range(0, set.clips.Count)];
            set.source.PlayOneShot(clip);
        }
    }

    public void PlayRandomMusic()
    {
        if (_playingMusic != null)
        {
            _playingMusic.source.Stop();
        }
        Sound music = bgms[Random.Range(0, bgms.Count)];
        music.source.Play();
        Debug.LogWarning($"Start playing {music.name}");
        _playingMusic = music;
    }

    public void PlayMusic(string name)
    {
        Sound music = bgms.Find(s => s.name == name);
        if (music != null)
        {
            music.source.Play();
            _playingMusic = music;
        }
    }

    public void FadeAwayMusic()
    {
        if (_playingMusic==null)
            return;
        StartCoroutine(FadeMusic(-40, 1f, _playingMusic.output.audioMixer));
    }

    public void StopMusic()
    {
        Debug.LogWarning("Stop Music");
        if (_playingMusic!=null)
        {
            _playingMusic.source.Stop();
            _playingMusic = null;
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator FadeMusic(float targetVolume, float fadeTime, AudioMixer audioMixer)
    {
        Debug.LogWarning("Fading away music");
        float currentTime = 0;
        float startVolume;
        audioMixer.GetFloat(MusicVolumeParameterName, out startVolume);

        while (currentTime < fadeTime)
        {
            currentTime += 0.01f;
            float volume = Mathf.Lerp(startVolume, targetVolume, currentTime / fadeTime);
            audioMixer.SetFloat(MusicVolumeParameterName, volume);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
