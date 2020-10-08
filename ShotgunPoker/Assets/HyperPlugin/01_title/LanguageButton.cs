using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class LanguageButton : MonoBehaviour {
  private static SystemLanguage[] langOrder = new SystemLanguage[] {
    SystemLanguage.Japanese,
    SystemLanguage.English,
    SystemLanguage.Chinese,
  };

  [SerializeField] private Sprite imageJa;
  [SerializeField] private Sprite imageZh;
  [SerializeField] private Sprite imageEn;

  private Image image;
  private List<LocalizedText> refreshTargets;

  public void Awake() {
    image = GetComponent<Image>();
    var btn = GetComponent<Button>();
    btn.onClick.AddListener(() => OnClickLangButton());
  }

  public void Start() {
    updateImage();

    refreshTargets = findAllLocalizedText();
  }

  public void OnClickLangButton() {
    var index = Array.IndexOf(langOrder, HyperSaveData.GetLanguage());
    index = (index + 1) % langOrder.Length;
    HyperSaveData.SetLanguage(langOrder[index]);
    updateImage();
    if (refreshTargets != null) {
      foreach (LocalizedText lt in refreshTargets) {
        lt.Refresh();
      }
    }
  }

  private void updateImage() {
    switch (HyperSaveData.GetLanguage()) {
      case SystemLanguage.Japanese:
        image.sprite = imageJa;
        break;
      case SystemLanguage.Chinese:
        image.sprite = imageZh;
        break;
      default:
        image.sprite = imageEn;
        break;
    }
  }

  private List<LocalizedText> findAllLocalizedText() {
    var root = findRoot(transform);
    var list = new List<LocalizedText>();
    recursive(root, (t) => {
      var localizedText = t.GetComponent<LocalizedText>();
      if (localizedText != null) {
        list.Add(localizedText);
      }
    });
    return list;
  }

  private Transform findRoot(Transform t) {
    if (t.parent == null) {
      return t;
    } else {
      return findRoot(t.parent);
    }
  }

  public void recursive(Transform t, Action<Transform> action) {
    action(t);
    foreach (Transform child in t) {
      recursive(child, action);
    }
  }

}