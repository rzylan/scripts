using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
  public Canvas canvas;

  public GameObject buttonPrefab;
  public delegate void Action();

  public GameObject textPrefab;
  public delegate string UpdateText();

  public GameObject addButton(
    string text, Action action, Vector2 initialPosition, int buttonNumber
  ) {
    GameObject button = Instantiate(buttonPrefab);
    button.transform.SetParent(canvas.transform, false);
    RectTransform buttonPosition = button.GetComponent<RectTransform>();
    buttonPosition.localPosition =
      buttonPosition.localPosition +
      new Vector3(initialPosition.x, 40 * buttonNumber + initialPosition.y, 0);
    button.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = text;
    button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{action();});
    return button;
  }

  public GameObject addText(
    UpdateText updateText, Vector2 initialPosition, int textNumber
  ) {
    GameObject textObject = Instantiate(textPrefab);
    textObject.transform.SetParent(canvas.transform, false);
    //textObject.GetComponent<UnityEngine.UI.Text>().text = text;
    RectTransform textObjectPosition = textObject.GetComponent<RectTransform>();
    textObjectPosition.localPosition =
      textObjectPosition.localPosition +
      new Vector3(initialPosition.x, -30 * textNumber + initialPosition.y, 0);
    return textObject;
  }

  public void destroyGameObject(GameObject gameObject)
  {
      Destroy(gameObject);
  }
}
