using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperPlugin;
using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class AdBanners : MonoBehaviour {

  private static TitleJson titleJson;

  [SerializeField] private HyperSettings Settings;
  [SerializeField] private Image OurBanner;
  [SerializeField] private OtherGame OtherGamePrefab;
  [SerializeField] private RectTransform[] OtherGamePositions;
  [SerializeField] private IAPButton IAPButton;

  private string ourBannerURL;

  private void Awake() {
    HyperSaveData.Init();
    if (HyperSaveData.GetLanguage(false) == SystemLanguage.Unknown) {
      HyperSaveData.SetLanguage(Application.systemLanguage);
    }
  }

  public async void Start() {
    if (!Advertisement.isInitialized) {
#if UNITY_IOS
      Advertisement.Initialize(Settings.gameIdForIOS, Settings.testMode);
#else
      Advertisement.Initialize(Settings.gameIdForAndroid, Settings.testMode);
#endif
    }

    OurBanner.gameObject.SetActive(false);

    if (titleJson == null) {
      titleJson = await getJson();
    }

    if (IAPButton != null) {
      IAPButton.gameObject.SetActive(false);
      IAPButton.onPurchaseComplete.AddListener((product) => {
        OnPurchaceComplete();
      });
    }

    await show();
  }

  private async Task<TitleJson> getJson() {
    var req = UnityWebRequest.Get(Settings.jsonURL);
    await req.SendWebRequest();
    if (req.responseCode == 200L) {
      return JsonUtility.FromJson<TitleJson>(req.downloadHandler.text);
    } else {
      Debug.Log("エラー");
      return null;
    }
  }

  private async Task show() {
    if (titleJson == null) return;

    if (!HyperSaveData.IsPayed()) {
      if (IAPButton != null) {
        IAPButton.gameObject.SetActive(true);
      }

      if (UnityEngine.Random.value < titleJson.adsShowRate || titleJson.banners.Length == 0) {
        Debug.Log("Adsバナー");
        StartCoroutine(WaitAndOpenBanner());
      } else {
        // 自社バナー
        Debug.Log("自社バナー");
        await showOurBanner();
      }
    }

    var max = OtherGamePositions.Length;
    if (0 < titleJson.otherGames.Length && 0 < max) {
      var games = ArrayShuffle.Shuffle(titleJson.otherGames);
      for (int i = 0; i < Math.Min(max, games.Length); i++) {
        var game = games[i];
        var icon = Instantiate<OtherGame>(OtherGamePrefab, OtherGamePositions[i]);
        icon.SetData(game.url, game.image);
      }
    }
  }

  private async Task showOurBanner() {
    var banner = titleJson.banners[UnityEngine.Random.Range(0, titleJson.banners.Length)];
    ourBannerURL = banner.url;
    var req = UnityWebRequestTexture.GetTexture(banner.image);
    await req.SendWebRequest();
    if (req.responseCode == 200L) {
      try {
        var textureHandler = req.downloadHandler as DownloadHandlerTexture;
        var texture2d = textureHandler.texture;
        var sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
        OurBanner.sprite = sprite;
        OurBanner.preserveAspect = true;
        OurBanner.gameObject.SetActive(true);
      } catch (Exception e) {
        Debug.Log(e);
        Debug.Log("エラー");
        StartCoroutine(WaitAndOpenBanner());
      }
    } else {
      Debug.Log("エラー");
      StartCoroutine(WaitAndOpenBanner());
    }
  }

  private IEnumerator WaitAndOpenBanner() {
    while (Advertisement.IsReady(Settings.placementIdBanner)) {
      yield return new WaitForSeconds(0.5f);
    }
    Debug.Log("Advertisement.IsReady");
    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    Advertisement.Banner.Show(Settings.placementIdBanner);
    Debug.Log("Advertisement.Banner.Show OK");
  }

  public void OnPurchaceComplete() {
    HyperSaveData.SetPayed(true);
    if (OurBanner.gameObject.activeSelf) {
      OurBanner.gameObject.SetActive(false);
    } else {
      Advertisement.Banner.Hide();
    }
  }

}