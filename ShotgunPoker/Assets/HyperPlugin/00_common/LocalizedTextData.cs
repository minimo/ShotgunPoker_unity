using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hyper/Create Localized Text Data", fileName = "LocalizedTextData")]
public class LocalizedTextData : ScriptableObject {

  [System.Serializable]
  public class LText {
    public string key;
    public string ja_JP;
    public string en_US;
    public string zh_CN;
    public string Get(SystemLanguage systemLanguage) {
      switch (systemLanguage) {
        case SystemLanguage.Japanese:
          return ja_JP;
        case SystemLanguage.Chinese:
          return zh_CN;
        default:
          return en_US;
      }
    }
  }

  [SerializeField]
  public LText[] texts;

#if UNITY_EDITOR
  public bool OnUpdateData() {
    bool changed = false;

    var list = new List<LText>(texts);
    for (var i = 0; i < list.Count; i++) {
      var item = list[i];

      if (item.key == null || item.key == "") {
        changed = true;
        list.Remove(item);
        i -= 1;
      } else {
        // editParamDic.Add(item.key, item);
      }

    }

    texts = list.ToArray();

    return changed;
  }

  public void AddNew() {
    var list = new List<LText>(texts);
    var item = new LText();
    item.key = list.Count.ToString("D3");
    list.Add(item);
    texts = list.ToArray();
  }
#endif
}