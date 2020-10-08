using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamMenu : MonoBehaviour {

  [SerializeField] private Image HamButton;
  [SerializeField] private Image BlackMask;
  [SerializeField] private RectTransform TitleMenu;

  private RectTransform rectTransform;
  private Animator animator;

  public void Awake() {
    rectTransform = GetComponent<RectTransform>();
    animator = GetComponent<Animator>();
  }

  public void OnClickHamMenu() {
    animator.SetTrigger("Open");
  }

  public void OnClickHamMenuClose() {
    animator.SetTrigger("Close");
  }

  // https://developers.facebook.com/docs/unity/gettingstarted
  // https://assetstore.unity.com/packages/tools/integration/twitter-kit-for-unity-84914
  // https://github.com/mrhdms/LINE-Share-Unity

  public void OpenVersionInfo() {

  }

  public void OpenCredit() {

  }

  public void OpenLicense() {

  }

  public void OpenPrivacyPolicy() {

  }

  public void OpenFacebook() {

  }

  public void OpenTwitter() {

  }

  public void OpenLine() {
    
  }
}