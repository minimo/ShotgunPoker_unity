using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BasicDialog))]
public class GameStartDialog : MonoBehaviour {

  [SerializeField] private RectTransform HeartParent;
  [SerializeField] private Heart HeartPrefab;
  [SerializeField] private TextMeshProUGUI StaminaValue;
  [SerializeField] private RecoverStaminaDialog RecoverStaminaDialog;
  public UnityEvent OnStart;

  private Heart headHeart;
  private BasicDialog basicDialog;
  private bool started;

  public void Awake() {
    basicDialog = GetComponent<BasicDialog>();
    basicDialog.Cancelable = false;
    basicDialog.OnCloseAnimationFinished.AddListener(() => {
      if (started) OnStart.Invoke();
    });
  }

  public void Start() {
    var x = HeartParent.anchoredPosition.x - HeartParent.rect.width * 0.5f;
    var dx = HeartParent.rect.width / (HyperSaveData.MaxStamina - 1);
    for (int i = 0; i < HyperSaveData.MaxStamina; i++) {
      var heart = Instantiate(HeartPrefab, HeartParent);
      var rt = heart.GetComponent<RectTransform>();
      rt.anchoredPosition = new Vector2(x + dx * i, 0);
      heart.OnConsumed.AddListener(() => {
        basicDialog.Close();
      });
    }
  }

  public void Update() {
    if (started) return;
    var stamina = HyperSaveData.GetStamina();
    StaminaValue.text = "" + Mathf.FloorToInt(stamina);
    headHeart = null;
    for (int i = 0; i < HeartParent.childCount; i++) {
      var heart = HeartParent.GetChild(i);
      var inner = heart.transform.Find("Inner").gameObject;
      if (Mathf.Floor(stamina) < i + 1) {
        inner.SetActive(false);
      } else {
        inner.SetActive(true);
        headHeart = heart.GetComponent<Heart>();
      }
    }
  }

  public void OnStartGame() {
    if (headHeart != null) {
      started = true;
      var stamina = HyperSaveData.GetStamina();
      StaminaValue.text = "" + Mathf.FloorToInt(stamina - 1);
      HyperSaveData.SetStamina(HyperSaveData.GetStamina() - 1);
      headHeart.Consume();
    } else {
      // スタミナ切れ
      RecoverStaminaDialog.Open();
    }
  }

  public void Open() {
    started = false;
    for (int i = 0; i < HeartParent.childCount; i++) {
      var heart = HeartParent.GetChild(i);
      heart.GetComponent<Heart>().Reset();
    }
    basicDialog.Open();
  }

}