using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSet
{
    public List<GameObject> buttons = new List<GameObject>();
    public List<Pair<GameObject, UiController.UpdateText>> texts =
      new List<Pair<GameObject, UiController.UpdateText>>();
    public UiController uiController;
    Notifier textUpdateNotifier;

    Vector2 initialButtonPosition;
    Vector2 initialTextPosition;

    public UiSet(
      UiController uiController,
      Notifier textUpdateNotifier
    )
    {
        this.uiController = uiController;
        this.textUpdateNotifier = textUpdateNotifier;
        this.textUpdateNotifier.subscribe(this, () => updateTexts());
        this.initialButtonPosition = new Vector2(0, 40.0f);
        this.initialTextPosition = new Vector2(0, -30.0f);
    }

    public UiSet(
      UiController uiController,
      Notifier textUpdateNotifier,
      Vector2 initialButtonPosition,
      Vector2 initialTextPosition
    )
    {
        this.uiController = uiController;
        this.textUpdateNotifier = textUpdateNotifier;
        this.textUpdateNotifier.subscribe(this, () => updateTexts());
        this.initialButtonPosition = initialButtonPosition;
        this.initialTextPosition = initialTextPosition;
    }

    public void addButton(string text, UiController.Action action)
    {
        buttons.Add(
          uiController.addButton(text, action, initialButtonPosition, buttons.Count)
        );
    }

    public void addText(UiController.UpdateText updateText)
    {
        texts.Add(
          new Pair<GameObject, UiController.UpdateText>(
            uiController.addText(updateText, initialTextPosition, texts.Count),
            updateText)
        );
    }

    public void updateTexts()
    {
        foreach (Pair<GameObject, UiController.UpdateText> text in texts)
        {
            text.v1.GetComponent<UnityEngine.UI.Text>().text = text.v2();
        }
    }

    public void clear()
    {
        foreach (Pair<GameObject, UiController.UpdateText> text in texts)
        {
            uiController.destroyGameObject(text.v1);
        }
        texts.Clear();

        foreach (GameObject button in buttons)
        {
            uiController.destroyGameObject(button);
        }
        buttons.Clear();
        //textUpdateNotifier.unsubscribe(this);
    }
}
