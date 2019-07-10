using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.IO;
using UnityEditor.Build.Player;
using System.Security.AccessControl;

public class Compiler  {

    [MenuItem("tool/compiler")]
    public static void CompilerCode()
    {
        Debug.Log("<<<compiling code<<<<");
        ScriptCompilationSettings scriptCompilationSettings = new ScriptCompilationSettings();
        scriptCompilationSettings.target = BuildTarget.StandaloneWindows64;
        scriptCompilationSettings.group = BuildTargetGroup.Standalone;
        scriptCompilationSettings.options = ScriptCompilationOptions.DevelopmentBuild;
        string dllpath = Application.streamingAssetsPath + "/my.dll";
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        PlayerBuildInterface.CompilePlayerScripts(scriptCompilationSettings, dllpath);
        Debug.Log("<<<compiling code<<<<");

    }

}
