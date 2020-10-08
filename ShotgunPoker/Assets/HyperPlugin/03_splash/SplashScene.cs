using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SplashScene : MonoBehaviour {

  [SerializeField] private SplashImage[] SplashImages;
  public UnityEvent OnSplashComplete;
  private int index;

  public void Start() {
    foreach (var image in SplashImages) {
      image.OnAnimationComplete.AddListener(() => Next());
      image.Hide();
    }
    index = -1;
    Next();
  }

  public void OnTap() {
    var img = SplashImages[index];
    img.TriggerTap();
  }

  public void Next() {
    if (index >= 0) {
      var pre = SplashImages[index];
      pre.Hide();
    }

    index += 1;
    if (SplashImages.Length <= index) {
      OnSplashComplete.Invoke();
    } else {
      var cur = SplashImages[index];
      cur.Show();
    }
  }

}