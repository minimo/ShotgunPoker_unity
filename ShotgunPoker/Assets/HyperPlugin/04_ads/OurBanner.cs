using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class OurBanner : MonoBehaviour {

  private RectTransform rectTransform;

  public void Awake() {
    rectTransform = GetComponent<RectTransform>();
  }

  public void Update() {
    rectTransform.anchoredPosition = new Vector2(0, 0);
  }
}