using System;
using UnityEngine;

[Serializable]
public class TitleJson {

  [Serializable]
  public class Banner {
    public string url;
    public string image;
  }

  [Serializable]
  public class OtherGame {
    public string url;
    public string image;
  }

  public float adsShowRate;
  public Banner[] banners;
  public OtherGame[] otherGames;

}