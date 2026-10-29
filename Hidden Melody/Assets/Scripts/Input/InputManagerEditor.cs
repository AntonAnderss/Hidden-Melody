using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        /**/

        InputManager myScript = (InputManager)target;

        GUILayout.Label("", GUILayout.Height(50));
        if (GUILayout.Button("Gameplay"))
        {
            myScript.SetMapToGameplay();
        }

        GUILayout.Label("", GUILayout.Height(2.5f));
        if (GUILayout.Button("Minigame"))
        {
            myScript.SetMapToMinigame();
        }

    }
}
