using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainScene : MonoBehaviour {
  public void Execute() {
    SceneManager.LoadScene("HyperPlugin/99_example/MainScene");
  }
}