using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScene : MonoBehaviour {

  public void Execute() {
    SceneManager.LoadScene("HyperPlugin/99_example/TitleScene");
  }
}