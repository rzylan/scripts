using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuildingsSetGraphicFactory: MonoBehaviour {

  public static BuildingsSetGraphicFactory instance;
  public GameObject planePrefab;

  public BuildingsSetGraphicHandler getBuildingsSetGraphicHandler<T> (
    Vector2 pozition,
    Vector2Int size,
    BuildingsSet<T> buildingSet
  ) where T : BuildingLogic {
    GameObject newObj = new GameObject();
    BuildingsSetGraphicHandler handler =
      newObj.AddComponent<BuildingsSetGraphicHandler>();
    handler.setProperties();
    newObj.transform.parent = gameObject.transform;
    newObj.transform.localPosition = new Vector3(pozition.x,pozition.y,0);
    GameObject plane =
      Instantiate(
        planePrefab,
        new Vector3(0,0,0),
        Quaternion.identity
      );
    plane.GetComponent<ObjectContainer>().obj = buildingSet;
    plane.transform.localScale = new Vector3(size.x,size.y,1);
    plane.transform.parent = newObj.transform;
    plane.transform.localPosition = new Vector3(0, 0, 0);
    return handler;
  }
}
