using UnityEngine;
using UnityEngine.InputSystem;

public class StartMenu : MonoBehaviour
{
    private bool isPaused = false;
    private GameObject player;
    private float normalTimeScale;
    [SerializeField] GameObject hudCanvas;
    [SerializeField] GameObject startMenuCanvas;
    private PlayerRespawn checkpoints;
    AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        checkpoints = player.GetComponent<PlayerRespawn>();
        PauseGame();
    }
    void PauseGame()
    {
        isPaused = true;
        normalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        startMenuCanvas.SetActive(true);
        Cursor.visible = true;
        player.GetComponent<PlayerInput>().enabled = false;
        hudCanvas.SetActive(false);
        PlaySound("OpenMenu");

    }

    public void ResumeButton()
    {
        isPaused = false;
        Time.timeScale = normalTimeScale;
        startMenuCanvas.SetActive(false);
        Cursor.visible = false;
        player.GetComponent<PlayerInput>().enabled = true;
        hudCanvas.SetActive(true);
        PlaySound("OpenMenu");
        checkpoints.Respawn();
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
