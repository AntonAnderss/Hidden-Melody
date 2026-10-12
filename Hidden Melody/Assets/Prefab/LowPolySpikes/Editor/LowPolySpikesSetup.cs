// Import utility for the supplied models. Editor only; no runtime dependency.
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public static class LowPolySpikesSetup
{
    [MenuItem("Tools/Low Poly Spikes/Create Prefabs")]
    public static void CreatePrefabs()
    {
        string root = null;
        foreach (string guid in AssetDatabase.FindAssets("LowPolySpikesSetup t:MonoScript"))
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.EndsWith("/Editor/LowPolySpikesSetup.cs"))
            {
                root = path.Substring(0, path.Length - "/Editor/LowPolySpikesSetup.cs".Length);
                break;
            }
        }
        if (root == null) { Debug.LogError("Cannot locate LowPolySpikes folder."); return; }
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("HDRP/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null) { Debug.LogError("No supported Lit shader found."); return; }
        Directory.CreateDirectory(root + "/Materials");
        Directory.CreateDirectory(root + "/Prefabs");
        AssetDatabase.Refresh();
        Material steel = GetMaterial(root, "Steel", shader, new Color(0.65f, 0.72f, 0.79f), 0.35f);
        Material plate = GetMaterial(root, "Base", shader, new Color(0.19f, 0.22f, 0.27f), 0.0f);
        foreach (string name in new[] { "SpikeTrap", "SingleSpike" })
        {
            string prefabPath = root + "/Prefabs/" + name + ".prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null) continue;
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(root + "/Models/" + name + ".obj");
            if (source == null) { Debug.LogError("Model not found: " + name); continue; }
            GameObject instance = Object.Instantiate(source);
            instance.name = name;
            foreach (MeshRenderer renderer in instance.GetComponentsInChildren<MeshRenderer>())
            {
                Material[] slots = renderer.sharedMaterials;
                for (int i = 0; i < slots.Length; i++)
                {
                    bool isBase = renderer.name.Contains("Base") ||
                                  (slots[i] != null && slots[i].name.Contains("Base"));
                    slots[i] = isBase ? plate : steel;
                }
                renderer.sharedMaterials = slots;
            }
            foreach (MeshFilter filter in instance.GetComponentsInChildren<MeshFilter>())
            {
                if (filter.sharedMesh == null) continue;
                MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = filter.sharedMesh;
                collider.convex = false;
            }
            PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
            Object.DestroyImmediate(instance);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<GameObject>(root + "/Prefabs/SpikeTrap.prefab");
        Debug.Log("Low Poly Spikes prefabs are ready in " + root + "/Prefabs.");
    }

    private static Material GetMaterial(string root, string name, Shader shader, Color color, float metallic)
    {
        string path = root + "/Materials/" + name + ".mat";
        Material existing = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (existing != null) return existing;
        Material material = new Material(shader) { name = name };
        if (material.HasProperty("_BaseColor")) material.SetColor("_BaseColor", color);
        if (material.HasProperty("_Color")) material.SetColor("_Color", color);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", metallic);
        if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", 0.25f);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", 0.25f);
        AssetDatabase.CreateAsset(material, path);
        return material;
    }
}
#endif
