using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuildingGraphicFactory : MonoBehaviour {

  public static BuildingGraphicFactory instance;
  public Sprite borderSprite;

  public BuildingGraphicHandler getPlainBuildingGraphicHandler(
    PlainBuilding building
  ) {
    GameObject newObj = new GameObject();
    BuildingGraphicHandler handler =
      newObj.AddComponent<BuildingGraphicHandler>();
    handler.setProperties(building, borderSprite);
    newObj.transform.parent =
      building.getBuildingsSetsHandlersGameObject().transform;
    newObj.transform.localPosition = new Vector3(0,0,0);
    return handler;
  }

  public BattleBuildingGraphicHandler getBattleBuildingGraphicHandler(
    PlainBuilding building
  ) {
    GameObject newObj = new GameObject();
    BattleBuildingGraphicHandler handler =
      newObj.AddComponent<BattleBuildingGraphicHandler>();
    handler.setProperties(building, borderSprite);
    newObj.transform.parent =
      building.getBuildingsSetsHandlersGameObject().transform;
    newObj.transform.localPosition = new Vector3(0,0,0);
    return handler;
  }
}
