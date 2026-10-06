using UnityEngine;

public class LostLifeUI : MonoBehaviour
{
    private bool isPaused = false;
    private GameObject player;
    private HealthSystem healthSystem;
    private float normalTimeScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();

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
    public void ContinueButton()
    {
        UnPauseGame();
        healthSystem.RecoverPlayer();
    }
}
