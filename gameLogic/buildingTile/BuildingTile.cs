using System;
using System.Collections;
using System.Collections.Generic;

public enum TypeOfBuildingTile {
  TEXTURE,
  FILL
}

public class BuildingTile<T> where T : BuildingLogic{
  public BuildingTileGraphicHandler buildingTileGraphicHandler;
  public TypeOfBuildingTile typeOfBuildingTile;
  public Building<T> building;

  public BuildingTile(
  TypeOfBuildingTile typeOfBuildingTile, Building<T> building) {
    this.typeOfBuildingTile = typeOfBuildingTile;
    this.building = building;
  }

  public void created(Vector2Int location) {
    this.buildingTileGraphicHandler =
      BuildingTileGraphicFactory.instance.getBuildingTileGraphicHandler(
        typeOfBuildingTile, building
      );
    buildingTileGraphicHandler.createGraphic(location);

    //List<Vector2Int> l  = new List<Vector2Int> { new Vector2Int(1, 1), new Vector2Int(0, 1),new Vector2Int(1, 0),new Vector2Int(1, -1),new Vector2Int(0, 0) };
    //buildingTileGraphicHandler.addBorder(l, location);
  }

  public void deleted() {
    buildingTileGraphicHandler.deleteGraphic();
  }
}
