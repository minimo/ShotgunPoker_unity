using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

  [SerializeField] private GameConfig GameConfig;
  [SerializeField] private ParticleSystem BreakParticle;

  public bool Killed { get; private set; }
  private List<Transform> rings = new List<Transform>();
  private List<Collider> frameColliders = new List<Collider>();

  public void Awake() {
    foreach (Transform child in transform) {
      if (child.gameObject.name.StartsWith("ring")) {
        rings.Add(child);
      }
      if (child.gameObject.name.StartsWith("Frame")) {
        frameColliders.Add(child.GetComponent<Collider>());
      }
    }
  }

  public void Update() {
    transform.Translate(0, GameConfig.ScrollSpeed * Time.deltaTime * 60, 0);
    for (int i = 0; i < rings.Count; i++) {
      rings[i].Rotate(Vector3.up, 0.6f * (i % 2 == 0 ? 1 : -1));
    }
  }

  public void OnTriggerEnter(Collider c) {
    if (c.gameObject.name == "Player") {
      Killed = true;
    } else if (c.gameObject.name == "DestroyPoint") {
      Destroy(gameObject);
    }
  }

  public void OnTriggerExit(Collider collider) {
    if (collider.gameObject.name == "Player") {
      foreach (var ring in rings) {
        ring.gameObject.SetActive(false);
      }
      foreach (var frame in frameColliders) {
        frame.enabled = false;
      }
      BreakParticle.Play();
    }
  }

}