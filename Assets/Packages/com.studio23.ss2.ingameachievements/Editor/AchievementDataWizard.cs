using Studio23.SS2.AchievementSystem.Data;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AchievementDataWizard : EditorWindow
{
	[MenuItem("Studio-23/Achievement System/Achievement Data Wizard")]
	private static void ShowWindow()
	{
		GetWindow<AchievementDataWizard>("Achievement Data Wizard");
	}


	private string _achievementName;
	private string _achievementDescription;
	private Sprite _achievementSprite;
	private Sprite _achievementLockedSprite;
	private float _achievementProgressGoal;
	private AchievementType _achievementType;
	private static Vector2 m_descriptionScrollPosition;

	private static Vector2 m_scrollPos;
	private static GUIStyle m_headerStyle;
	private static GUIStyle m_subHeaderStyle;

	private void OnEnable()
	{
		m_scrollPos = Vector2.zero;
		m_descriptionScrollPosition = Vector2.zero;
		m_headerStyle = new GUIStyle
		{
			alignment = TextAnchor.MiddleCenter,
			fontStyle = FontStyle.Bold,
			fontSize = 24,
			normal = new GUIStyleState
			{
				textColor = Color.white
			}
		};
		m_subHeaderStyle = new GUIStyle
		{
			alignment = TextAnchor.MiddleCenter,
			fontStyle = FontStyle.Bold,
			fontSize = 18,
			normal = new GUIStyleState
			{
				textColor = Color.cyan
			}
		};
	}

	private void OnGUI()
	{
		GUILayout.BeginScrollView(m_scrollPos);
		GUILayout.Label("Achievement Data Settings", m_headerStyle);
		GUILayout.Space(10f);

        _achievementName = EditorGUILayout.TextField("Achievement Name", _achievementName);
		GUILayout.Space(10f);

		GUILayout.Label("Achievement Description");
		GUILayout.BeginScrollView(m_descriptionScrollPosition);
		_achievementDescription = EditorGUILayout.TextArea(_achievementDescription, GUILayout.Height(80));
		GUILayout.EndScrollView();

		_achievementSprite = (Sprite)EditorGUILayout.ObjectField("Achievement Icon", _achievementSprite, typeof(Sprite), false) as Sprite;
		_achievementLockedSprite = (Sprite)EditorGUILayout.ObjectField("Achievement Locked Icon", _achievementLockedSprite, typeof(Sprite), false) as Sprite;

		GUILayout.Space(10f);

		GUILayout.Label("Achievement Type", EditorStyles.boldLabel);
		_achievementType = (AchievementType)EditorGUILayout.EnumPopup(_achievementType);
		if (_achievementType == AchievementType.ProgressTracked)
		{
			_achievementProgressGoal =
				EditorGUILayout.FloatField("Achievement Progress Goal", _achievementProgressGoal);
		}
		GUILayout.FlexibleSpace();

		if (GUILayout.Button("Create Achievement Data"))
		{
			CreateAchievementData();
		}
		GUILayout.EndScrollView();
	}



	private void CreateAchievementData()
	{
		if (IsFormIncomplete())
			return;

		AssetDatabase.Refresh();
		AchievementData _data = ScriptableObject.CreateInstance<AchievementData>();

		_data.AchievementName = _achievementName;
		_data.AchievementDescription = _achievementDescription;
		_data.LockedIcon = _achievementSprite.texture;
		_data.UnlockedIcon = _achievementLockedSprite.texture;
		_data.Type = _achievementType;
		if (_achievementType == AchievementType.ProgressTracked)
			_data.ProgressGoal = _achievementProgressGoal;
		string path = $"Assets/Resources/AchievementSystem/AchievementData/{_achievementName}.asset";
		AssetDatabase.CreateAsset(_data, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		Debug.Log("Achievement data created and saved at " + path);
	}

	private bool IsFormIncomplete()
	{
		if (string.IsNullOrEmpty(_achievementName) || string.IsNullOrEmpty(_achievementDescription) ||
			_achievementSprite == null || _achievementLockedSprite == null)
		{
			EditorUtility.DisplayDialog("Empty Fields", "Please ensure that all fields are filled.", "Ok");
			return true;
		}

		if (File.Exists($"Assets/Resources/AchievementSystem/AchievementData/{_achievementName}.asset"))
		{
			bool result = EditorUtility.DisplayDialog("Already Exists",
				"Achievement Data with the same name already exists!\nDo you want to overwrite it?", "Yes, replace.",
				"No.");
			if (!result)
				return true;
		}

		return false;
	}
}
