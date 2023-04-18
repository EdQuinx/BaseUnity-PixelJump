using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

[InitializeOnLoad]
public class AutoSave
{
    static AutoSave()
    {
        EditorApplication.playModeStateChanged += SaveOnPlay;
    }

    private static void SaveOnPlay(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            Debug.Log("Auto-saving before entering play mode: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveOpenScenes();
        }
    }
}
