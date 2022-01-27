using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
  void Update()
  {
    exitApplication(Input.GetKeyDown(KeyCode.Escape));
  }

  public void exitApplication(bool state = false)
  {
    if (!state) return;

    Debug.Log("Exiting...");
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
  }
}
