using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
/*
 * Author: Clara Lönnkrans
 * 
*/
public class PausMenu : MonoBehaviour
{
    private bool isPaused = false;
    private GameObject player;
    private float normalTimeScale;
    [SerializeField] GameObject hudCanvas;
    [SerializeField] GameObject pausMenuCanvas;
    AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPause(InputValue input)
    {
        if(isPaused)
        {
            UnPauseGame();
        }
        else if(!isPaused)
        {
            PauseGame();
        }
    }
    void PauseGame()
    {
        isPaused = true;
        normalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pausMenuCanvas.SetActive(true);
        Cursor.visible = true;
        player.GetComponent<PlayerInput>().enabled = false;
        hudCanvas.SetActive(false);
        PlaySound("OpenMenu");

    }

    void UnPauseGame()
    {
        Debug.Log("Unpause");
        isPaused = false;
        Time.timeScale = normalTimeScale;
        pausMenuCanvas.SetActive(false);
        Cursor.visible = false;
        player.GetComponent<PlayerInput>().enabled = true;
        hudCanvas.SetActive(true);
        PlaySound("OpenMenu");
    }
    public void ResumeButton()
    {
        UnPauseGame();
    }
    public void SettingsButton()
    {
        
    }
    public void ReturnToCheckpointButton()
    {
        
    }
    public void ExitButton()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    private void PlaySound(string music)
    {

        AudioClip sound = SoundBank.Instance.GetMenuSound(music);
        if (sound == null)
        {
            Debug.Log("No Sound");
            return;
        }
        audioSource.PlayOneShot(sound);
    }
}
