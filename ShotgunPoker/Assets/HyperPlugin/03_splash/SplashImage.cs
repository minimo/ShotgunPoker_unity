using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SplashImage : MonoBehaviour {

  private Animator animator;
  public UnityEvent OnAnimationComplete;

  public void Awake() {
    animator = GetComponent<Animator>();
  }

  public virtual void Hide() {
    var img = GetComponent<Image>();
    img.color = new Color(1, 1, 1, 0);
  }

  public void Show() {
    animator.SetTrigger("Show");
  }

  public void TriggerTap() {
    animator.SetTrigger("Tap");
  }

  public void AnimationComplete() {
    OnAnimationComplete.Invoke();
  }

}