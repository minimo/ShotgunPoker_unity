using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGroup : MonoBehaviour {

  private List<Animator> fishes = new List<Animator>();

  private void Start() {
    foreach (Transform fish in transform) {
      var animator = fish.GetComponent<Animator>();
      if (animator != null) {
        fishes.Add(animator);
        animator.Play("Take 001", -1, UnityEngine.Random.Range(0.0f, 1.0f));
      }
    }
  }

  private void Update() {
    transform.Translate(Vector3.up * MainScene.ScrollSpeed * Time.deltaTime * 60, Space.Self);
  }
}