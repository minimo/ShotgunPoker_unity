using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSaveData {

  public const float MaxStamina = 5;
  public const float StaminaPerMinute = 0.20f;

  private static SystemLanguage languageCache;

  public static void Init() {
    languageCache = GetLanguage(false);
  }

  public static bool IsPayed() {
    return PlayerPrefs.GetInt("hyper.payed", 0) != 0;
  }

  public static void SetPayed(bool v) {
    PlayerPrefs.SetInt("hyper.payed", v ? 1 : 0);
    PlayerPrefs.Save();
  }

  public static SystemLanguage GetLanguage(bool fromCache = true) {
    if (fromCache) return languageCache;
    int key = PlayerPrefs.GetInt("hyper.language", -1);
    if (key == 0) {
      return SystemLanguage.English;
    } else if (key == 1) {
      return SystemLanguage.Japanese;
    } else if (key == 2) {
      return SystemLanguage.Chinese;
    } else {
      return SystemLanguage.Unknown;
    }
  }

  public static void SetLanguage(SystemLanguage language) {
    languageCache = language;
    if (language == SystemLanguage.Japanese) {
      PlayerPrefs.SetInt("hyper.language", 1);
    } else if (language == SystemLanguage.Chinese) {
      PlayerPrefs.SetInt("hyper.language", 2);
    } else {
      PlayerPrefs.SetInt("hyper.language", 0);
    }
    PlayerPrefs.Save();
  }

  public static float GetStamina() {
    long nowMs = DateTime.Now.Ticks / 10000;

    var stamina = PlayerPrefs.GetFloat("hyper.stamina", MaxStamina);
    if (stamina < MaxStamina) {
      var lastupdate = long.Parse(PlayerPrefs.GetString("hyper.stamina.lastupdate", "" + nowMs));
      var t = Convert.ToSingle(nowMs - lastupdate);
      var minutes = t / 1000.0f / 60.0f;
      return Mathf.Min(stamina + StaminaPerMinute * minutes, MaxStamina);
    } else {
      return stamina;
    }
  }

  public static void SetStamina(float v) {
    long nowMs = DateTime.Now.Ticks / 10000;
    PlayerPrefs.SetFloat("hyper.stamina", v);
    PlayerPrefs.SetString("hyper.stamina.lastupdate", "" + nowMs);
    PlayerPrefs.Save();
  }

  public static float GetHighscore() {
    return PlayerPrefs.GetFloat("hyper.highscore", 0);
  }

  public static void SetHighscore(float v) {
    PlayerPrefs.SetFloat("hyper.highscore", v);
    PlayerPrefs.Save();
  }

  public static int GetUpdateHighscoreCount() {
    return PlayerPrefs.GetInt("hyper.updatehighscorecount", 0);
  }

  public static void SetUpdateHighscoreCount(int v) {
    PlayerPrefs.SetInt("hyper.updatehighscorecount", v);
    PlayerPrefs.Save();
  }

}