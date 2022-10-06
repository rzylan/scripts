using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum TypeOfBuilding {
  BOOK,
  VIDEO_CAMERA,
  PROCESSOR
}

public abstract class PlainBuilding {
  public TypeOfBuilding typeOfBuilding;

  public PlainBuilding(
    TypeOfBuilding typeOfBuilding
  ) {
    this.typeOfBuilding = typeOfBuilding;
  }

  public abstract bool place(Vector2Int location,GameState gameState);

  public abstract void destroy(GameState gameState);

  public abstract GameObject getHandlersGameObject();

  public abstract GameObject getBuildingsSetsHandlersGameObject();

  public abstract Dictionary<Vector2Int, TypeOfBuildingTile> getBlueprint();

  public abstract void deleteGraphic();

  public abstract List<Vector2Int> getTilesCoordinates();
}
