using UnityEngine;

[CreateAssetMenu(menuName = "Hyper/Create Settings", fileName = "HyperSettings")]
public class HyperSettings : ScriptableObject {

  public string jsonURL = "https://ue.pease.jp/narazakidev01/hyperplugin/banner.json";
  public string gameIdForAndroid = "12345";
  public string gameIdForIOS = "12345";
  public string placementIdBanner = "bannerPlacement";
  public string placementIdVideo = "rewardedVideo";
  public bool testMode = true;

}