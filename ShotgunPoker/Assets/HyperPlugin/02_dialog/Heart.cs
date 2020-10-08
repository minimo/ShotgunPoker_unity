using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Heart : MonoBehaviour {

  private Animator animator;
  private UnityEvent onConsumed = new UnityEvent();
  public UnityEvent OnConsumed {
    get {
      return onConsumed;
    }
  }

  public void Awake() {
    animator = GetComponent<Animator>();
  }

  public void FireConsumed() {
    onConsumed.Invoke();
  }

  public void Consume() {
    animator.SetTrigger("Consume");
  }

  public void Reset() {
    
  }
}