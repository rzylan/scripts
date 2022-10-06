using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseInput {
  NONE,
  LEFT_DOWN,
  LEFT_UP
}

public static class Utilities {
  public static MouseInput GetMouseInput() {
    if(Input.GetMouseButtonUp(0)) {
      return MouseInput.LEFT_UP;
    }
    if(Input.GetMouseButtonDown(0)) {
      return MouseInput.LEFT_DOWN;
    }
    return MouseInput.NONE;
  }
}
