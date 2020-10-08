using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text)), ExecuteAlways]
public class LocalizedText : MonoBehaviour {

  public string Key;
  public LocalizedTextData LocalizedTextData;

  private TMPro.TMP_Text textMesh;

  public void Awake() {
    textMesh = GetComponent<TMPro.TMP_Text>();
  }

  public void Start() {
    Refresh();
  }

  public void Refresh() {
    if (textMesh == null) return;

    var systemLanguage = HyperSaveData.GetLanguage();

    if (LocalizedTextData == null) {
      textMesh.SetText("LocalizedTextDataが設定されてないよ");
    } else if (Key == null || Key == "") {
      textMesh.SetText("テキストキーが設定されてないよ");
    } else {
      var texts = LocalizedTextData.texts.FirstOrDefault(_ => _.key == Key);
      if (texts != null) {
        textMesh.SetText(texts.Get(systemLanguage));
      } else {
        textMesh.SetText("テキストデータが定義されてないよ");
      }
    }
  }

  public void Update() {
#if UNITY_EDITOR
    Refresh();
#endif
  }

}