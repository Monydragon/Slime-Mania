using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void D_Void();
    public delegate void D_Int(int value);
    public delegate void D_String(string value);
    public delegate void D_Float(float value);
    public delegate void D_Bool(bool value);
    public delegate void D_Gameobject(GameObject value);
    public delegate void D_SlimeTypeAndMulti(SlimeType type, int value);

    public static event D_Void onGameReset;
    public static event D_Void onLoadNextLevel;
    public static event D_SlimeTypeAndMulti onMultiplierChange;
    public static event D_Gameobject onSlimeCapture;
    public static event D_Void onTimeChanged;
    public static event D_Void onSlimeConvertToPoints;
    public static event D_Void onWinLevel;
    public static event D_Void onLoseLevel;
    public static event D_Void onScoreChanged;

    public static void GameReset() { onGameReset?.Invoke(); }
    public static void LoadNextLevel() { onLoadNextLevel?.Invoke(); }
    public static void MultiplierChange(SlimeType type, int value) { onMultiplierChange?.Invoke(type, value); }
    public static void SlimeCapture(GameObject go) { onSlimeCapture?.Invoke(go); }
    public static void SlimeConvertToPoints() { onSlimeConvertToPoints?.Invoke(); }
    public static void TimeChanged() { onTimeChanged?.Invoke(); }
    public static void LevelComplete() { onWinLevel?.Invoke(); }
    public static void LevelFail() { onLoseLevel?.Invoke(); }
    public static void ScoreChanged() { onScoreChanged?.Invoke(); }

}