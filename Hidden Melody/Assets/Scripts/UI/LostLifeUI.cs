using UnityEngine;
/*
 * Author: Clara Lönnkrans
 * 
*/
public class LostLifeUI : MonoBehaviour
{
    private bool isPaused = false;
    private GameObject player;
    private HealthSystem healthSystem;
    private float normalTimeScale;
    [SerializeField] GameObject hudCanvas;

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
        hudCanvas.SetActive(false);
        //Dissable player inputs

    }
    void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = normalTimeScale;
        gameObject.SetActive(false);
        Cursor.visible = false;
        hudCanvas.SetActive(true);
        //Enable player inputs
    }
    public void ContinueButton()
    {
        UnPauseGame();
        healthSystem.RecoverPlayer();
    }
}
