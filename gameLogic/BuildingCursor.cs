using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCursor {
  public bool alreadyDrawn;
  PlainBuilding building;
  BuildingTileGraphicFactory buildingTileGraphicFactory;
  Dictionary<Vector2Int, BuildingTileGraphicHandler> handlers =
    new Dictionary<Vector2Int, BuildingTileGraphicHandler>();

  public BuildingCursor(PlainBuilding building) {
    alreadyDrawn = false;
    this.building = building;
    this.buildingTileGraphicFactory = BuildingTileGraphicFactory.instance;
  }

  public void cursorMoved(Vector2Int coordinates) {
    if(alreadyDrawn) {
      moveCursor(coordinates);
    } else {
      alreadyDrawn = true;
      drawCursor(coordinates);
    }
  }

  protected void drawCursor(Vector2Int coordinates) {
    foreach(KeyValuePair<Vector2Int, TypeOfBuildingTile> entry in
        building.getBlueprint())
    {
      BuildingTileGraphicHandler newHandler =
        buildingTileGraphicFactory.getBuildingTileGraphicHandler(
          entry.Value, building
        );
      Vector2Int newCoordinates = coordinates.add(entry.Key);
      handlers.Add(
        entry.Key,
        newHandler
      );
      newHandler.createGraphic(newCoordinates);
    }
  }
  public void removeCursor() {
    building.deleteGraphic();
    foreach(KeyValuePair<Vector2Int, BuildingTileGraphicHandler> entry in handlers)
    {
      entry.Value.deleteGraphic();
    }
  }
  protected void moveCursor(Vector2Int coordinates) {
    foreach(
      KeyValuePair<Vector2Int, BuildingTileGraphicHandler> handler in handlers
    ){
      Vector2Int newCoordinates = coordinates.add(handler.Key);
      handler.Value.moveGraphic(newCoordinates);
    }
  }
}
