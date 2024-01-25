using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Editor
{
    public class Installer : UnityEditor.Editor
    {
        [MenuItem("Studio-23/AchievementSystem/Install")]
        public static void Install()
        {
           GameObject achievementSystem=new GameObject("Achievement System");
            achievementSystem.AddComponent<Core.AchievementSystem>();
        }
    }
}