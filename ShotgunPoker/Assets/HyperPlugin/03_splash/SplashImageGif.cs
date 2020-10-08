using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UniGifImage), typeof(UniGifImageAspectController))]
public class SplashImageGif : SplashImage {

  public override void Hide() {
    var img = GetComponent<RawImage>();
    img.color = new Color(1, 1, 1, 0);
  }

}