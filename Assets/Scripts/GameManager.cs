using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public static int[][] SpawnTimeTable =
    {
        new[] {8, 13},
        new[] {7, 12},
        new[] {6, 11},
        new[] {5, 10},
        new[] {4, 8},
        new[] {4, 5},
    };
    public static int Difficulty = 0;

    static public List<Scene> scenes = new List<Scene>();
    public static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("シーンをロードした: " + scene.name);

        scenes.Add(scene);
    }
}
