﻿using UnityEditor;
using UnityEngine;

namespace Kamgam.ExcludeFromBuild
{
    public class ExcludeFromBuildSettings : ScriptableObject
    {
        public const string Version = "1.6.3";
        public const string SettingsFilePath = "Assets/ExcludeFromBuildSettings.asset";

        public enum SceneFileBehaviour { NeverUpdateBuildSettings, AlwaysAsk, AlwaysUpdateBuildSettings }

        [SerializeField]
        public LogLevel LogLevel = LogLevel.Warning;

        [SerializeField, Tooltip("Show a red icon in the 'Project' window for excluded objects?")]
        public bool ShowIconInProjectView = true;

        [SerializeField, Tooltip("Show a red icon on the Component in the inspector and in scene view?")]
        public bool ShowIconOnComponent = true;

        [SerializeField, Tooltip("When a scene gets excluded, should it also be enabled or disabled in the 'BuildSettings > Scenes in Build' list?")]
        public SceneFileBehaviour SceneFilesBehaviour = SceneFileBehaviour.AlwaysAsk;

        [SerializeField, Tooltip("Ask the user if there is a conflict between hidden and existing files? If set to false then existing files will be deleted/overwritten without asking.")]
        public bool AskIfConflict = true;

        [SerializeField, Tooltip(_AllowEditorExclusionTooltip)]
        public bool AllowEditorExclusion = false;
        public const string _AllowEditorExclusionTooltip = "If enabled then Editor/ folders can be excluded too. This might be handy for test-aware build where you want Editor build process hooks to be disabled.";

        [SerializeField, Tooltip(_TestAwareBuildTooltip)]
        public bool TestAwareBuild = true;
        public const string _TestAwareBuildTooltip = "If active then the 'exclude/include files and directories' logic will NOT be executed during builds if a test is running.\n\nUseful if you want to do the exclusions manually before a build. It avoids having them undone automatically after the build.";

        [SerializeField, Tooltip(_ScanAllGroupsTooltip)]
        public bool ScanAllGroups = true;
        public const string _ScanAllGroupsTooltip = "Disable to only scan for Components matching the current active group.\nEnable to include results for all groups.";

        [SerializeField, Tooltip(_IgnoreMissingAssetsTooltips)]
        public bool IgnoreMissingAssets = false;
        public const string _IgnoreMissingAssetsTooltips = "If enabled then missing foldres or directories in the exclusion list will no longer be treated as errors. They will just be ignored.\n\n" +
            "NOTICE: It is recommended to keep the excluded list up to date since the tool may not always be able to differentiate between a missing asset and a generic error.";


        [SerializeField, Tooltip(_DelayBuildStartTooltip)]
        public bool DelayBuildStart = false;
        public const string _DelayBuildStartTooltip = "[EXPERIMENTAL FEATURE] Delays build start by a few seconds.\n\n" +
            "This means that once you start a build it will prepare the exclusions and then" +
            " ABORT the build. Then it will wait a bit and automatically restart the build.\n\n" +
            "This is done to resolve some rare occurrences of a missmatch between excluded and built assets. " +
            "Currently this is only known to happen on Windows (UWP) builds.\n\n" +
            "ONLY ENABLE THIS IF YOU NEED IT. Keep it disabled if possible.";
        public float BuildStartDelayInSec = 20f;

        [SerializeField, Tooltip(_DetectDebugAsDefineTooltip)]
        public bool DetectDebugAsDefine = true;
        public const string _DetectDebugAsDefineTooltip = "If enabled then a define named 'DEBUG' will also be considered as set if the build is a debug build instead of a realease build.";

        /// <summary>
        /// Whether or not prefabs files should be preprocessed.
        /// </summary>
        [SerializeField, Tooltip(_PreProcessPrefabsTooltip)]
        public bool PreProcessPrefabs = false;
        public const string _PreProcessPrefabsTooltip = "[EXPERIMENTAL FEATURE] Whether or not to execute the exclusion logic on prefabs before building.";

        [SerializeField, Tooltip(_AutoDisableExcludedScenesTooltip)]
        public bool AutoDisableExcludedScenes = false;
        public const string _AutoDisableExcludedScenesTooltip = "[EXPERIMENTAL FEATURE] If enabled then all excluded scenes will be automatically disabled before a build.\n\n" +
            "NOTICE: Sadly this ONLY works if used in combination with 'BuildStartDelayInSec' enabled. It's under investigation.\n\n" +
            "You can also call it manually before the build via: ExcludeFromBuildController.DisableExcludedScenes() and RevertDisabledScenes() afterwards to revert.";

        protected static ExcludeFromBuildSettings cachedSettings;

        public static ExcludeFromBuildSettings GetOrCreateSettings()
        {
            if (cachedSettings == null)
            {
                cachedSettings = AssetDatabase.LoadAssetAtPath<ExcludeFromBuildSettings>(SettingsFilePath);

                // Not found? Then search for it.
                if (cachedSettings == null)
                {
                    string[] results = AssetDatabase.FindAssets("t:ExcludeFromBuildSettings");
                    if (results.Length > 0)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(results[0]);
                        cachedSettings = AssetDatabase.LoadAssetAtPath<ExcludeFromBuildSettings>(path);
                    }
                }

                // Still not found? Then create settings.
                if (cachedSettings == null)
                {
                    cachedSettings = ScriptableObject.CreateInstance<ExcludeFromBuildSettings>();
                    cachedSettings.LogLevel = LogLevel.Warning;
                    cachedSettings.ShowIconInProjectView = true;
                    cachedSettings.ShowIconOnComponent = true;
                    cachedSettings.SceneFilesBehaviour = SceneFileBehaviour.AlwaysAsk;
                    cachedSettings.AskIfConflict = true;
                    cachedSettings.TestAwareBuild = true;
                    cachedSettings.AllowEditorExclusion = false;
                    cachedSettings.ScanAllGroups = true;
                    cachedSettings.IgnoreMissingAssets = false;
                    cachedSettings.DelayBuildStart = false;
                    cachedSettings.DetectDebugAsDefine = true;
                    cachedSettings.PreProcessPrefabs = false;
                    cachedSettings.AutoDisableExcludedScenes = false;
                    AssetDatabase.CreateAsset(cachedSettings, SettingsFilePath);
                    AssetDatabase.SaveAssets();

                    MaterialShaderFixer.FixMaterials(MaterialShaderFixer.RenderPiplelineType.Standard);
                }

            }

            return cachedSettings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }

        // settings
        public static void SelectSettings()
        {
            var settings = ExcludeFromBuildSettings.GetOrCreateSettings();
            if (settings != null)
            {
                Selection.activeObject = settings;
                EditorGUIUtility.PingObject(settings);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "ExcludeFromBuild settings could not be found or created.", "Ok");
            }
        }
    }

    static class ExcludeFromBuildSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateExcludeFromBuildSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Exclude From Build", SettingsScope.Project)
            {
                label = "Exclude From Build",
                guiHandler = (searchContext) =>
                {
                    var style = new GUIStyle(GUI.skin.label);
                    style.wordWrap = true;

                    var settings = ExcludeFromBuildSettings.GetSerializedSettings();

                    EditorGUILayout.LabelField("Version: " + ExcludeFromBuildSettings.Version);

                    EditorGUILayout.PropertyField(settings.FindProperty("LogLevel"), new GUIContent("Log level:"));

                    EditorGUILayout.PropertyField(settings.FindProperty("ShowIconInProjectView"), new GUIContent("Show icon in 'Project':"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label("Show a red icon in the 'Project' window for excluded objects?", style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("ShowIconOnComponent"), new GUIContent("Show icon on Component:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label("Show a red icon on the Component in the inspector and in scene view?", style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("SceneFilesBehaviour"), new GUIContent("Enable/disable scenes:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label("When a scene gets excluded, should it also be enabled or disabled in the 'BuildSettings > Scenes in Build' list?", style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("AskIfConflict"), new GUIContent("Ask if conflict:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label("Ask the user if there is a conflict between hidde and existing files? If set to false then existing files will be deleted/overwritten without asking.", style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("TestAwareBuild"), new GUIContent("Test Aware Build:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._TestAwareBuildTooltip, style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("AllowEditorExclusion"), new GUIContent("Allow Editor Exclusion:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._AllowEditorExclusionTooltip, style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("ScanAllGroups"), new GUIContent("Scan all groups:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._ScanAllGroupsTooltip, style);
                    GUILayout.EndVertical();
                    
                    EditorGUILayout.PropertyField(settings.FindProperty("IgnoreMissingAssets"), new GUIContent("Ignore Missing Assets:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._IgnoreMissingAssetsTooltips, style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("DelayBuildStart"), new GUIContent("Delay Build Start:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._DelayBuildStartTooltip, style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("DetectDebugAsDefine"), new GUIContent("Detect Debug As Define:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._DetectDebugAsDefineTooltip, style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("PreProcessPrefabs"), new GUIContent("Pre Process Prefabs:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._PreProcessPrefabsTooltip, style);
                    GUILayout.EndVertical();

                    EditorGUILayout.PropertyField(settings.FindProperty("AutoDisableExcludedScenes"), new GUIContent("Auto Disable Excluded Scenes:"));
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label(ExcludeFromBuildSettings._AutoDisableExcludedScenesTooltip, style);
                    GUILayout.EndVertical();

                    settings.ApplyModifiedProperties();
                },

                // Populate the search keywords to enable smart search filtering and label highlighting.
                keywords = new System.Collections.Generic.HashSet<string>(new[] { "exclude", "build", "exclude from build", "folder" })
            };

            return provider;
        }
    }
}