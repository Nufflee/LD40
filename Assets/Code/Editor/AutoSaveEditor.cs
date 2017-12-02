using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
  static AutoSave()
  {
    EditorApplication.playModeStateChanged += change =>
    {
      if (change == PlayModeStateChange.ExitingEditMode)
      {
        if (EditorApplication.isPlaying)
          return;

        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
      }
    };
  }
}