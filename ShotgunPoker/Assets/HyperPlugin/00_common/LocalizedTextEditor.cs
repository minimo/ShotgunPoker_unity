using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(LocalizedText))]
public class LocalizedTextEditor : Editor {

  public override void OnInspectorGUI() {
    serializedObject.Update();

    var _target = target as LocalizedText;

    EditorGUILayout.PropertyField(serializedObject.FindProperty("LocalizedTextData"));

    if (_target.LocalizedTextData != null) {
      string[] data = _target.LocalizedTextData.texts.Select(_ => _.key).ToArray();
      int beforeIndex = Array.IndexOf(data, _target.Key);
      int index = EditorGUILayout.Popup("Key", beforeIndex, data);
      if (index >= 0) {
        _target.Key = data[index];
        _target.Refresh();
      }
    }

    serializedObject.ApplyModifiedProperties();
  }

}
#endif