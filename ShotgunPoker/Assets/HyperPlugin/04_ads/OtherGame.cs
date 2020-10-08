using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperPlugin;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class OtherGame : MonoBehaviour {

  private string url;
  private string imageUrl;

  public void SetData(string url, string imageUrl) {
    this.url = url;
    this.imageUrl = imageUrl;
  }

  public void Awake() {
    var button = GetComponent<Button>();
    button.onClick.AddListener(() => {
      OnClick();
    });
  }

  public async void Start() {
    await Show();
  }

  private async Task Show() {
    var req = UnityWebRequestTexture.GetTexture(imageUrl);
    await req.SendWebRequest();
    if (req.responseCode == 200L) {
      try {
        var textureHandler = req.downloadHandler as DownloadHandlerTexture;
        var texture2d = textureHandler.texture;
        var sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
        var image = GetComponent<Image>();
        image.sprite = sprite;
      } catch (Exception e) {
        Debug.Log(e);
        Debug.Log("エラー");
      }
    } else {
      Debug.Log("エラー");
    }
  }

  public void OnClick() {
    Application.OpenURL(url);
  }

}