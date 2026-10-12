using System;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Author: Clara Lönnkrans
 *Used for playing sounds
 *
 *
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
        //Debug.Log("SoundBank");
        Sound sound = Array.Find(enemySounds, x => x.soundName == name);
        int i = Random.Range(0, sound.sound.Length);
        Debug.Log(sound.soundName);
        return sound?.sound[i];
    }
}
