
using Studio23.SS2.AchievementSystem.Data;

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Editor
{
    public class DummyProviderEditor : EditorWindow
    {
        private static string AssetSaveFolderPath => "Assets/Resources/AchievementSystem/Providers";

        [MenuItem("Studio-23/AchievementSystem/Providers/Dummy")]
        public static void CreateDefaultProvider()
        {
            DummyAchievementProvider provider = ScriptableObject.CreateInstance<DummyAchievementProvider>();

            provider.PlatformProvider = PlatformProvider.Default;

            if (!Directory.Exists(AssetSaveFolderPath))
            {
                Directory.CreateDirectory(AssetSaveFolderPath);
            }

            AssetDatabase.CreateAsset(provider, $"{AssetSaveFolderPath}/DummyAchievementProvider.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }
    }
}