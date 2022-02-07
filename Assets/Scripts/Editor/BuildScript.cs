using System;
using System.IO;
using UnityEditor;
using UnityEngine;

// This is a script that adds a new Build option in the top navigation bar.
// It can build a dedicated Linux server executable or the WebGL client. 

// TODO: Make the scenes to build not need to be hard coded.

public class BuildScript
{
    [MenuItem("Build/Build All")]

    public static void BuildAll()
    {
        Console.WriteLine("Building Server and Client...");

        BuildLinuxServer();
        BuildWebClient();

        Console.WriteLine("Built Server and Client.");
    }

    [MenuItem("Build/Build Server")]
    public static void BuildLinuxServer()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/MirrorNetworking.unity" };
        buildPlayerOptions.locationPathName = "Builds/Linux/Server/LearnVerseServer.x86_64";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC | BuildOptions.EnableHeadlessMode;

        Console.WriteLine("Building Server...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Server. Please upload and notify team.");
    }

    [MenuItem("Build/Build Client")]
    public static void BuildWebClient()
    {
        Console.WriteLine("NOT CORRECTLY IMPLEMENTED!");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/MirrorNetworking.unity" };
        buildPlayerOptions.locationPathName = "Builds/WebGL/Client";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;

        Console.WriteLine("Building Client...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client. Please upload and notify team.");
    }
}
