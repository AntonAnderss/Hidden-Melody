using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        /**/

        SaveSystem myScript = (SaveSystem) target;

        GUILayout.Label("", GUILayout.Height(50));
        if (GUILayout.Button("Create New"))
        {
            myScript.CreateSaveFile();
        }

        GUILayout.Label("", GUILayout.Height(2.5f));
        if (GUILayout.Button("Save"))
        {
            myScript.Save();
        }

        GUILayout.Label("", GUILayout.Height(2.5f));
        if (GUILayout.Button("Load"))
        {
            //
        }

    }
}
