using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
  public delegate void Action();
  public List<Action> actions = new List<Action>();
  public delegate void ChosenAction(
    float x, float y, MouseInput mouseInput, List<Action> actions, GameObject colliderObject);
  public ChosenAction chosenAction;
  public delegate void CancelAction();
  public CancelAction cancelAction;
  public bool buttonClicked = false;
  public Action turnAction;

  void Start() {
    this.chosenAction = (x, y, mouseInput, actions, collider) => {};
    this.cancelAction = () => {};
  }

  private void Update() {
    MouseInput mouseInput = Utilities.GetMouseInput();
    mouseMoved(mouseInput);
  }

  private void FixedUpdate() {
    foreach (var action in actions) {
      try {
        action();
      }
      catch(System.Exception e) {
        Debug.Log(e);
      }
    }
    actions = new List<Action>();
    turnAction();
    buttonClicked = false;
  }

  void mouseMoved(MouseInput mouseInput)
  {
    if(!buttonClicked) {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      RaycastHit[] hits;
      hits = Physics.RaycastAll (ray);
      foreach (RaycastHit hit in hits)
      {
        GameObject collider = hit.collider.gameObject;
        if(collider.tag == "ActionCollider") {
          GameObject colliderParentObject = collider.transform.parent.gameObject;
          chosenAction(
            hit.point.x - colliderParentObject.transform.position.x,
            hit.point.y - colliderParentObject.transform.position.y,
            mouseInput,
            actions,
            collider
          );
        }
      }
    }
  }

  public void setEmptyAction() {
    buttonClicked = true;

    this.chosenAction = (x, y, mouseInput, actions, colliderObject) => {};
    this.cancelAction();
    this.cancelAction = () => {};
  }

  public void setChosenAction(ChosenAction chosenAction, CancelAction cancelAction){
    buttonClicked = true;

    this.chosenAction = chosenAction;
    this.cancelAction();
    this.cancelAction = cancelAction;
  }

  public void signleAction(Action action) {
    buttonClicked = true;

    actions.Add(action);
  }

  public static void print(string s) {
    Debug.Log(s);
  }
}
