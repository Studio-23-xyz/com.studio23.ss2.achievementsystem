using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AchievementDataWizard : EditorWindow
{

    [MenuItem("Custom Tools/Achievement  Data Wizard")]
    private static void ShowWindow()
    {
        GetWindow<AchievementDataWizard>("Achievement Data Wizard");
    }

    private string _steamID;
    private string _xboxID;
    private string _achievementName;
    private string _achievementDescription;
    private Sprite _achievementSprite;
    private AchievementType _achievementType;
    private float _achievementProgressTrack;
    private static Vector2 descriptionScrollPosition;

    //private string _achievementID;


    private void OnGUI()
    {
        GUILayout.Label("Achievement Data Settings", EditorStyles.boldLabel);

        // _achievementID = EditorGUILayout.IntField("Achievement ID", _achievementID);
        _steamID = EditorGUILayout.TextField("Steam ID", _steamID);
        _xboxID = EditorGUILayout.TextField("Xbox ID", _xboxID);
        _achievementName = EditorGUILayout.TextField("Achievement Name", _achievementName);

        GUILayout.Label("Item Description", EditorStyles.boldLabel);
        
        _achievementDescription = EditorGUILayout.TextField("Achievement Description", _achievementDescription);
        

        _achievementSprite = (Sprite)EditorGUILayout.ObjectField("Achievement Icon",_achievementSprite, typeof(Sprite), false) as Sprite;
        // _achievementType = (AchievementType)EditorGUILayout.EnumFlagsField("Achievement Type", _achievementType);
        // _achievementProgressTrack =
        //  EditorGUILayout.FloatField("Progress Tracked", _achievementProgressTrack);

        GUILayout.Label("Achievement Type", EditorStyles.boldLabel);
        _achievementType = (AchievementType)EditorGUILayout.EnumPopup(_achievementType);

        GUILayout.FlexibleSpace();



        if (GUILayout.Button("Create Achievement Data"))
        { 
            CreateAchievementData();
        }

    }

    private void CreateAchievementData()
    {
        //Creating data
        AchievementData _data = ScriptableObject.CreateInstance<AchievementData>();
        //_data.AchievementID = _achievementID;
        _data.SteamID = _steamID;
         _data.XBOXID = _xboxID;
        _data.AchievementName = _achievementName;
        _data.AchievementDescription = _achievementDescription;
        _data.Icon = _achievementSprite;
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
