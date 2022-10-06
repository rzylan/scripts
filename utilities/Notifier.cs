using System;
using System.Collections;
using System.Collections.Generic;

public class Notifier{
  public delegate void SimpleNotifier();

  public delegate void Action();
  public Dictionary<Object, Action> listeners = new Dictionary<Object, Action>();

  public void subscribe(Object obj, Action action) {
    listeners.Add(obj, action);
  }

  public void unsubscribe(Object obj) {
    listeners.Remove(obj);
  }

  public void notify() {
    foreach(KeyValuePair<Object, Action> entry in listeners)
    {
      entry.Value();
    }
  }
}
