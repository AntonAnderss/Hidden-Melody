using System;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Author: Clara Lönnkrans
 *Used for playing sounds
 *
 *In array, add a sound name and every sound that can be played for that name
 *Use the get note method for the category, and then the name for the sound, a random sound from that array will play
 *If you only want one sound, only have one sound in that array
*/
public class SoundBank : MonoBehaviour
{
    public static SoundBank Instance;

    public Sound[] playerSounds, musicalNotes, enemySounds, menuSounds,hudSounds, otherSounds;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public AudioClip GetNoteSound(string name)
    {
        Sound sound = Array.Find(musicalNotes, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        return sound?.sound[i];
    }
    public AudioClip GetPlayerSound(string name)
    {
        Sound sound = Array.Find(playerSounds, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        return sound?.sound[i];
    }
    public AudioClip GetEnemySound(string name)
    {
        Sound sound = Array.Find(enemySounds, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        return sound?.sound[i];
    }
    public AudioClip GetMenuSound(string name)
    {
        Debug.Log("SoundBank");
        Sound sound = Array.Find(menuSounds, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        Debug.Log(sound.soundName);
        return sound?.sound[i];
    }
    public AudioClip GetHUDSound(string name)
    {
        Sound sound = Array.Find(hudSounds, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        return sound?.sound[i];
    }
    public AudioClip GetOtherSound(string name)
    {
        Sound sound = Array.Find(otherSounds, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        return sound?.sound[i];
    }
}
