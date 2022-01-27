using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchHandler : MonoBehaviour
{
  private Light light;

  private int currentColor = 0;
  private List<Color> colors = new List<Color>{
    new Color(1f, 1f, 1f, 1f),
    new Color(0f, 0f, 0f, 0f),
    new Color(1f, 0f, 0f, 1f)
  };

  void Start()
  {
    light = GetComponent<Light>();
  }

  void Update()
  {
    bool isMouseDown = Input.GetMouseButtonDown(0);
    switchColor(isMouseDown);
  }

  public void switchColor(bool state)
  {
    if (!state) return;
    Debug.Log(state);

    if (currentColor == colors.Count) currentColor = 0;
    light.color = colors[currentColor++];
  }
}
