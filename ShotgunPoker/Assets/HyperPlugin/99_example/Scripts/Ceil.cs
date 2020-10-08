using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ceil : MonoBehaviour {

  public UnityEvent OnMiss;

  public void OnTriggerEnter(Collider other) {
    var ring = other.GetComponent<Ring>();
    if (ring != null) {
      if (!ring.Killed) {
        Debug.Log("miss");
        OnMiss.Invoke();
      }
    }
  }

}