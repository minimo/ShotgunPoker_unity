using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasicDialog : MonoBehaviour {

  private Image blackMask;
  private RectTransform bg;
  private Animator animator;
  public bool Cancelable { get; set; }

  public UnityEvent OnCloseAnimationFinished;

  public void Awake() {
    blackMask = transform.Find("BlackMask").GetComponent<Image>();
    bg = transform.Find("Bg").GetComponent<RectTransform>();
    animator = GetComponent<Animator>();

    blackMask.gameObject.SetActive(false);
    bg.gameObject.SetActive(false);
  }

  public void Open() {
    animator.SetTrigger("Open");
  }

  public void Close() {
    animator.SetTrigger("Close");
  }

  public void OnClickOK() {
    Close();
  }

  public void OnClickCancel() {
    if (Cancelable) Close();
  }

  public void CloseAnimationFinished() {
    OnCloseAnimationFinished.Invoke();
  }

}