using UnityEngine;
/*
 * Author: Clara Lönnkrans
 * 
*/
public class PausMenu : MonoBehaviour
{
    private bool isPaused = false;
    private GameObject player;
    private float normalTimeScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public void OnLostLife()
    {
        PauseGame();
    }
    void PauseGame()
    {
        isPaused = true;
        normalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Cursor.visible = true;
        //Dissable player inputs

    }
    void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = normalTimeScale;
        gameObject.SetActive(false);
        Cursor.visible = false;
        //Enable player inputs
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
}
