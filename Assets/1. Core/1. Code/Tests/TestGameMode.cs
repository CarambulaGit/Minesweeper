using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeBase.Infrastructure;
using CodeBase.Logic;
using NUnit.Framework;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class TestGameMode {
    private static readonly GameMode[] GameModes = {
        GameMode.GameModeWithDifficulty(GameMode.Difficulty.Easy),
        GameMode.GameModeWithDifficulty(GameMode.Difficulty.Medium),
        GameMode.GameModeWithDifficulty(GameMode.Difficulty.Hard),
        GameMode.CustomMode(1, 1, 1)
    };

    private static readonly GameMode[] BasicGameModes = {
        GameMode.GameModeWithDifficulty(GameMode.Difficulty.Easy),
        GameMode.GameModeWithDifficulty(GameMode.Difficulty.Medium),
        GameMode.GameModeWithDifficulty(GameMode.Difficulty.Hard),
    };

    [Test]
    public void TestGameModeJsonConvert([ValueSource(nameof(GameModes))] GameMode gameMode) {
        var json = JsonConvert.SerializeObject(gameMode);
        Debug.Log(json);
        var deserializedGameMode = JsonConvert.DeserializeObject<GameMode>(json);
        Debug.Log(deserializedGameMode);
        Assert.AreEqual(gameMode, deserializedGameMode);
    }

    [Test]
    public void TestGameModeCreation() {
        var allDifficulties = Enum.GetValues(typeof(GameMode.Difficulty));
        var difficulties = allDifficulties.Cast<GameMode.Difficulty>().ToList();
        difficulties.Remove(GameMode.Difficulty.Custom);
        var gameModes = difficulties.Select(GameMode.GameModeWithDifficulty).ToList();
        gameModes.ForEach(mode => Assert.IsTrue(BasicGameModes.Any(mode.Equals)));
        gameModes.Add(GameMode.CustomMode(1, 1, 1));
        Assert.IsTrue(gameModes.Count == allDifficulties.Length);
    }

    [Test]
    public void TestGameModeFactoryMethods() {
        var type = typeof(GameMode);
        var methods = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
            .Where(m => m.ReturnType == type).ToList();
        Assert.IsTrue(methods.Count == BasicGameModes.Length);
        foreach (var method in methods) {
            var parameters = method.GetParameters();
            if (parameters.Length > 0) return;
            var gameMode = (GameMode) method.Invoke(null, null);
            Assert.IsTrue(BasicGameModes.Any(mode => mode.Equals(gameMode)));
        }
    }
}