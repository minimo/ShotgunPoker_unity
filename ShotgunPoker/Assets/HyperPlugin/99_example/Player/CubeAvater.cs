using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAvater : MonoBehaviour {

  [SerializeField] private Transform RotationParent;
  [SerializeField] private Transform Cube;

  private Vector3 rrRotateAxis;
  private Vector3 cubeRotateAxis;
  private Quaternion q;

  public void Start() {
    rrRotateAxis = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    cubeRotateAxis = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
  }

  public void Update() {
    RotationParent.Rotate(rrRotateAxis, 1.0f, Space.Self);
    Cube.Rotate(cubeRotateAxis, 6.0f, Space.Self);
  }
}