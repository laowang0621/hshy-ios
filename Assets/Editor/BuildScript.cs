using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

public class BuildScript
{
    public static void BuildIOS()
    {
        try
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.iOS.targetOSVersionString = "12.0";
            PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, new[] { UnityEngine.Rendering.GraphicsDeviceType.Metal });
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.laowang.hshypixel");
            PlayerSettings.productName = "HeiShenHuaPixel";
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
            PlayerSettings.iOS.appleEnableAutomaticSigning = false;
        }
        catch (Exception e) { Debug.Log("SETUP_WARN " + e.Message); }

        string[] scenes = new string[0];
        try
        {
            scenes = AssetDatabase.FindAssets("t:Scene")
                .Select(g => AssetDatabase.GUIDToAssetPath(g))
                .Where(p => !string.IsNullOrEmpty(p))
                .ToArray();
        }
        catch (Exception e) { Debug.Log("SCENE_WARN " + e.Message); }
        Debug.Log("SCENES_COUNT " + scenes.Length);

        var opts = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "build/ios",
            target = BuildTarget.iOS,
            options = BuildOptions.None
        };
        var report = BuildPipeline.BuildPlayer(opts);
        Debug.Log("BUILD_RESULT " + report.summary.result);
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            EditorApplication.Exit(1);
    }
}
