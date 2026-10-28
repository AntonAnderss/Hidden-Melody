/* Author: Anton Andersson */
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Switches inbetween input action maps
/// </summary>
public class InputManager : MonoBehaviour
{
    [Header("Action Maps")]
    [SerializeField] string gameplayName = "Gameplay";
    [SerializeField] string minigameName = "Minigame";

    [Header("References")]
    [SerializeField] PlayerInput Input;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }

    void ChangeMap(string mapName)
    {
        Input.DeactivateInput();
        Input.SwitchCurrentActionMap(mapName);
        Input.ActivateInput();
    }

    public void SetMapToGameplay() { ChangeMap(gameplayName); }
    public void SetMapToMinigame() { ChangeMap(minigameName); }
}
