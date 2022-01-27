using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitHandler : MonoBehaviour
{
  void Update()
  {
    transform.Rotate(0, 2f * Time.deltaTime, 0);
  }
}
