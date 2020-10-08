using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour {
  private enum States {
    Pause,
    Play,
  }

  public static float ScrollSpeed;

  [SerializeField] private GameConfig GameConfig;
  [SerializeField] private GameStartDialog GameStartDialog;
  [SerializeField] private Player Player;
  [SerializeField] private Ring RingPrefab;
  [SerializeField] private Ceil Ceil;
  [SerializeField] private Animator CameraAnimator;
  [SerializeField] private Transform RingSpawnPosition;

  private States State;

  public void Start() {
    State = States.Pause;
    GameStartDialog.OnStart.AddListener(() => {
      GameStart();
    });
    Player.OnMiss.AddListener(() => {
      if (State == States.Play) {
        State = States.Pause;
        Player.Death();
        CameraAnimator.SetTrigger("GameEnd");
        StartCoroutine(WaitAndRestart());
      }
    });
    Ceil.OnMiss.AddListener(() => {
      if (State == States.Play) {
        State = States.Pause;
        Player.Death();
        CameraAnimator.SetTrigger("GameEnd");
        StartCoroutine(WaitAndRestart());
      }
    });

    Main();
  }

  public void Main() {
    CameraAnimator.SetTrigger("New");
    ScrollSpeed = GameConfig.ScrollSpeed;
    GameStartDialog.Open();
  }

  public void Update() { }

  private void GameStart() {
    State = States.Play;
    Player.State = Player.States.Game;
    CameraAnimator.SetTrigger("GameStart");
    StartCoroutine(WaitAndSpawnRing());
  }

  private IEnumerator WaitAndSpawnRing() {
    while (State == States.Play) {
      var x = UnityEngine.Random.Range(GameConfig.MinX, GameConfig.MaxX);
      var ring = Instantiate(RingPrefab, RingSpawnPosition);
      var pos = ring.transform.localPosition;
      ring.transform.localPosition = new Vector3(x, 0, 0);
      yield return new WaitForSeconds(GameConfig.RingSpawnInterval);
    }
  }

  private IEnumerator WaitAndRestart() {
    yield return new WaitForSeconds(2.0f);
    foreach (Transform ring in RingSpawnPosition) {
      Destroy(ring.gameObject);
    }
    Main();
  }

}