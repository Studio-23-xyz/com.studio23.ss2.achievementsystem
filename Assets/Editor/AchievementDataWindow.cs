using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AchievementDataEditorWindow : EditorWindow
{
	private AchievementData achievementData;

	[MenuItem("Window/Create Achievement Data")]
	public static void ShowWindow()
	{
		AchievementDataEditorWindow window = GetWindow<AchievementDataEditorWindow>("Achievement Data");
		window.minSize = new Vector2(300, 200);
	}

	private void OnEnable()
	{
		achievementData = ScriptableObject.CreateInstance<AchievementData>();
	}

	private void OnGUI()
	{
		GUILayout.Label("Create New Achievement Data", EditorStyles.boldLabel);

		achievementData.AchievementName = EditorGUILayout.TextField("Achievement Name:", achievementData.AchievementName);

		GUILayout.Space(10);

		GUILayout.Label("Achievement Ids", EditorStyles.boldLabel);

		if (GUILayout.Button("Add Achievement Id"))
		{
			achievementData.AchievementIds.Add(new AchievementIdData());
		}

		for (int i = 0; i < achievementData.AchievementIds.Count; i++)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Id " + (i + 1), GUILayout.Width(50));
			achievementData.AchievementIds[i].AchievementProvider = (AchievementProvider)EditorGUILayout.EnumPopup(achievementData.AchievementIds[i].AchievementProvider);
			achievementData.AchievementIds[i].Id = EditorGUILayout.TextField(achievementData.AchievementIds[i].Id);
			if (GUILayout.Button("Remove", GUILayout.Width(60)))
			{
				achievementData.AchievementIds.RemoveAt(i);
			}
			GUILayout.EndHorizontal();
		}

		GUILayout.Space(20);

		if (GUILayout.Button("Create Achievement Data"))
		{
			AssetDatabase.CreateAsset(achievementData, "Assets/NewAchievementData.asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			Close();
		}
	}
}

public class AchievementIdData
{
	public AchievementProvider AchievementProvider;
	public string Id;
}

public class AchievementData : ScriptableObject
{
	public List<AchievementIdData> AchievementIds;
	public string AchievementName;
}