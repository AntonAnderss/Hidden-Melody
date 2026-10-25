using System;
using Unity.VisualScripting;
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
    [SerializeField] GameObject pausMenu;
    [SerializeField] GameObject menuList;
    private PlayerRespawn checkpoints;
    AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        checkpoints = player.GetComponent<PlayerRespawn>();
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
    void ResetMenu()
    {
        for( int i = 0; i < menuList.transform.childCount; i++ )
        {
            menuList.transform.GetChild(i).gameObject.SetActive(false);
        }
        hudCanvas.SetActive(true);
        pausMenuCanvas.SetActive(true);
    }
    void PauseGame()
    {
        isPaused = true;
        normalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pausMenu.SetActive(true);
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
        Cursor.visible = false;
        player.GetComponent<PlayerInput>().enabled = true;
        ResetMenu();
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
        checkpoints.Respawn();
        UnPauseGame();
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
