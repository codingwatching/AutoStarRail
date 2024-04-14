using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Game
{
    public static class SceneTools
    {
        [MenuItem("����/����Ϸ")]
        public static void OpenGame()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }
            var scenePath = "Assets/Scenes/Launch.unity";
            var currentScenePath = SceneManager.GetActiveScene().path;
            if (currentScenePath != scenePath)
            {
                EditorSceneManager.OpenScene(scenePath);
            }

            EditorApplication.isPlaying = true;
        }
    }
}