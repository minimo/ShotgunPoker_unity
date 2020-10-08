using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RecoverStaminaDialog : MonoBehaviour, IUnityAdsListener {

  [SerializeField] private HyperSettings Settings;
  [SerializeField] private Button yesButton;

  private BasicDialog basicDialog;

  public void Awake() {
    basicDialog = GetComponent<BasicDialog>();
  }

  public void Start() {
    if (!Advertisement.IsReady(Settings.placementIdVideo)) {
      yesButton.enabled = false;
    }

    Advertisement.AddListener(this);
  }

  public void OnStartVideo() {
    Advertisement.Show(Settings.placementIdVideo);
  }

  public void OnUnityAdsReady(string placementId) {
    if (placementId == Settings.placementIdVideo) {
      yesButton.enabled = true;
    }
  }

  public void OnUnityAdsDidError(string message) { }

  public void OnUnityAdsDidStart(string placementId) { }

  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
    if (placementId == Settings.placementIdVideo) {
      if (showResult == ShowResult.Finished) {
        HyperSaveData.SetStamina(HyperSaveData.GetStamina() + HyperSaveData.MaxStamina);
      }
      basicDialog.Close();
    }
  }

  public void Open() {
    basicDialog.Open();
  }
}