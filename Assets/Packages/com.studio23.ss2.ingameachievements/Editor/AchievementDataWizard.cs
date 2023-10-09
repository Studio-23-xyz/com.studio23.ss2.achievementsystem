using Studio23.SS2.IngameAchievements.Data;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AchievementDataWizard : EditorWindow
{

	[MenuItem("Studio-23/Ingame Achievements/Achievement Data Wizard")]
	private static void ShowWindow()
	{
		GetWindow<AchievementDataWizard>("Achievement Data Wizard");
	}

	private List<AchievementIdData> _achievementIdData;
	private string _achievementName;
	private string _achievementDescription;
	private Sprite _achievementSprite;
	private Sprite _achievementLockedSprite;
	private AchievementType _achievementType;
	private float _achievementProgressTrack;
	private static Vector2 descriptionScrollPosition;

	private Vector2 _scrollPos;

	private static GUIStyle _headerStyle;
	private static GUIStyle _subHeaderStyle;

	private void OnEnable()
	{
		_achievementIdData = new List<AchievementIdData>();
		_headerStyle = new GUIStyle
		{
			alignment = TextAnchor.MiddleCenter,
			fontStyle = FontStyle.Bold,
			fontSize = 24,
			normal = new GUIStyleState
			{
				textColor = Color.white
			}
		};
		_subHeaderStyle = new GUIStyle
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
		GUILayout.BeginScrollView(_scrollPos);
		GUILayout.Label("Achievement Data Settings", _headerStyle);
		GUILayout.Space(10f);
#if STUDIO23_INGAMEACHIEVEMENTS_THIRDPARTY_INSTALLED
		RenderAchievementIDSection();
#endif
		_achievementName = EditorGUILayout.TextField("Achievement Name", _achievementName);
		GUILayout.Space(10f);
		GUILayout.Label("Item Description", _subHeaderStyle);
		GUILayout.Space(10f);
		GUILayout.Label("Achievement Description");
		_achievementDescription = EditorGUILayout.TextArea(_achievementDescription, GUILayout.Height(80));
		_achievementSprite = (Sprite)EditorGUILayout.ObjectField("Achievement Icon", _achievementSprite, typeof(Sprite), false) as Sprite;
		_achievementLockedSprite = (Sprite)EditorGUILayout.ObjectField("Achievement Locked Icon", _achievementLockedSprite, typeof(Sprite), false) as Sprite;
		GUILayout.Space(10f);
		GUILayout.Label("Achievement Type", EditorStyles.boldLabel);
		_achievementType = (AchievementType)EditorGUILayout.EnumPopup(_achievementType);
		GUILayout.FlexibleSpace();


		if (GUILayout.Button("Create Achievement Data"))
		{
			CreateAchievementData();
		}
		GUILayout.EndScrollView();
	}

	private void RenderAchievementIDSection()
	{
		GUILayout.Label($"Achievement IDs", _subHeaderStyle);

		foreach (var achievementProvider in _achievementIdData)
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(15);

			achievementProvider.AchievementProvider =
				(AchievementProvider)EditorGUILayout.EnumPopup(achievementProvider.AchievementProvider,
					GUILayout.Width(120f));
			achievementProvider.Id = EditorGUILayout.TextField("ID:", achievementProvider.Id, GUILayout.Width(350f));

			if (GUILayout.Button("Remove", GUILayout.Width(80)))
			{
				_achievementIdData.Remove(achievementProvider);
			}

			EditorGUILayout.EndHorizontal();
		}

		if (GUILayout.Button("Add Achievement Provider", GUILayout.Width(200)))
		{
			_achievementIdData.Add(new AchievementIdData
			{
				AchievementProvider = AchievementProvider.Steam,
				Id = "AchievementID"
			});
		}

		GUILayout.Space(10f);
	}

	private void CreateAchievementData()
	{
		if (string.IsNullOrEmpty(_achievementName) || string.IsNullOrEmpty(_achievementDescription) ||
		    _achievementSprite == null || _achievementLockedSprite == null)
		{
			EditorUtility.DisplayDialog("Empty Fields", "Please ensure that all fields are filled.", "Ok");
			return;
		}
		if (File.Exists("Assets/Resources/"+_achievementName+".asset"))
		{
			bool result = EditorUtility.DisplayDialog("Already Exists", "Achievement Data with the same name already exists!\nDo you want to overwrite it?", "Yes, replace.", "No.");
			if (!result)
				return;
		}
		AssetDatabase.Refresh();
		//Creating data
		AchievementData _data = ScriptableObject.CreateInstance<AchievementData>();
#if STUDIO23_INGAMEACHIEVEMENTS_THIRDPARTY_INSTALLED
		_data.AchievementIDs = _achievementIdData;
#endif
		_data.AchievementName = _achievementName;
		_data.AchievementDescription = _achievementDescription;
		_data.LockedIcon = _achievementSprite.texture;
		_data.UnlockedIcon = _achievementLockedSprite.texture;
		_data.Type = _achievementType;
		_data.ProgressGoal = _achievementProgressTrack;
		//Store Data
		string path = "Assets/Resources/" + _achievementName + ".asset";
		AssetDatabase.CreateAsset(_data, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		Debug.Log("Achievement data created and saved at " + path);
	}
}
