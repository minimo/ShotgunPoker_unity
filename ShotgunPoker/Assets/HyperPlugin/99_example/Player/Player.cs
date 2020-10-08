using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
  public enum States {
    Idle,
    Game,
    Death,
  }

  [SerializeField] private GameConfig GameConfig;
  public UnityEvent OnMiss;

  public States State { get; set; }

  private float speed;

  public void Start() {
    State = States.Idle;
  }

  public void Update() {
    if (State == States.Game) {
      int input = 0;
#if UNITY_EDITOR
      if (Input.GetMouseButton(0)) {
        if (Input.mousePosition.x < Screen.width * 0.5f) {
          input = -1;
        } else {
          input = 1;
        }
      }
#else
      if (Input.touchCount > 0) {
        if (Input.GetTouch(0).position.x < Screen.width * 0.5f) {
          input = -1;
        } else {
          input = 1;
        }
      }
#endif

      var bs = speed;
      if (input != 0) {
        speed += input * GameConfig.PlayerSpeed;
        if (GameConfig.MaxPlayerSpeed < Mathf.Abs(speed)) {
          speed = Mathf.Sign(speed) * GameConfig.MaxPlayerSpeed;
        }
        transform.Translate(speed * Time.deltaTime * 60, 0, 0);
        if (transform.localPosition.x < GameConfig.MinX) {
          transform.localPosition = new Vector3(GameConfig.MinX, 0, 0);
          speed = 0;
        } else if (GameConfig.MaxX < transform.localPosition.x) {
          transform.localPosition = new Vector3(GameConfig.MaxX, 0, 0);
          speed = 0;
        }
      } else {
        speed += -Mathf.Sign(transform.localPosition.x) * GameConfig.PlayerBackSpeed;
        transform.Translate(speed * Time.deltaTime * 60, 0, 0);
        speed *= GameConfig.PlayerBackRate;
        if (transform.localPosition.x < GameConfig.MinX) {
          transform.localPosition = new Vector3(GameConfig.MinX, 0, 0);
        } else if (GameConfig.MaxX < transform.localPosition.x) {
          transform.localPosition = new Vector3(GameConfig.MaxX, 0, 0);
        }
      }

    } else if (State == States.Death) {
      transform.Translate(Vector3.down * 0.2f * Time.deltaTime * 60);
    }
  }

  public void Death() {
    State = States.Death;
    speed = 0;
    StartCoroutine(WaitAndDestroy());
  }

  public IEnumerator WaitAndDestroy() {
    yield return new WaitForSeconds(2.0f);
    State = States.Idle;
    transform.localPosition = Vector3.zero;
  }

  public void OnTriggerEnter(Collider col) {
    if (col.gameObject.name == "RingFrame") {
      OnMiss.Invoke();
    }
  }
}