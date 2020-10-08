using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg : MonoBehaviour {

  [SerializeField] private Transform R0;
  [SerializeField] private Transform L0;
  [SerializeField] private Transform R1;
  [SerializeField] private Transform L1;

  private List<Transform> parts = new List<Transform>();
  private float baseY;

  public void Start() {
    baseY = -R1.localPosition.y;
    parts.Add(R0);
    parts.Add(R1);
    parts.Add(L0);
    parts.Add(L1);
  }

  public void Update() {
    foreach(var p in parts) {
      p.Translate(Vector3.up * MainScene.ScrollSpeed * Time.deltaTime * 60, Space.Self);
      var pos = p.localPosition;
      if (baseY < pos.y) {
        p.localPosition = new Vector3(pos.x, pos.y - baseY * 2, pos.z);
      }
    }
  }

}