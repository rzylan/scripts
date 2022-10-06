using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Building<T>:PlainBuilding where T : BuildingLogic{
  public T logic;

  public delegate void NotifyEndOfActiveAction();

  public Dictionary<Vector2Int, BuildingTile<T>> tiles =
    new Dictionary<Vector2Int, BuildingTile<T>>();
  public Dictionary<Vector2Int, TypeOfBuildingTile> blueprint;
  public BuildingsSet<T> buildingsSet;
  public BuildingGraphicHandler buildingGraphicHandler;

  public Building(
    Dictionary<Vector2Int, TypeOfBuildingTile> blueprint,
    TypeOfBuilding typeOfBuilding,
    BuildingsSet<T> buildingsSet,
    T logic
  ): base(typeOfBuilding) {
    this.blueprint = blueprint;
    this.typeOfBuilding = typeOfBuilding;
    this.buildingsSet = buildingsSet;
    // this.buildingGraphicHandler =
    //   BuildingGraphicFactory.instance.getBuildingGraphicHandler(this);
    this.logic = logic;
  }

  public Building(
    TypeOfBuilding typeOfBuilding,
    BuildingsSet<T> buildingsSet
  ): base(typeOfBuilding) {
    this.typeOfBuilding = typeOfBuilding;
    this.buildingsSet = buildingsSet;
    this.blueprint = new Dictionary<Vector2Int, TypeOfBuildingTile>();
    blueprint.Add(new Vector2Int(0,0), TypeOfBuildingTile.TEXTURE);
    // this.buildingGraphicHandler =
    //   BuildingGraphicFactory.instance.getBuildingGraphicHandler(this);
  }

  public override bool place(
    Vector2Int location,
    GameState gameState
  ) {
    this.buildingGraphicHandler = logic.getBuildingGraphicHandler();
    if(!logic.checkIfCanBePlaced(location, gameState)) {
      return false;
    }

    foreach(KeyValuePair<Vector2Int, TypeOfBuildingTile> entry in blueprint)
    {
      tiles.Add(location.add(entry.Key), new BuildingTile<T>(entry.Value, this));
    }
    if(!buildingsSet.tileMap.addTiles(tiles)) {
      return false;
    }
    logic.afterPlace(gameState);
    return true;
  }

  public override void destroy(GameState gameState) {
    buildingsSet.tileMap.deleteTiles(tiles);
    buildingGraphicHandler.deleteGraphic();
    logic.afterDestroy(gameState);
  }

  public override GameObject getHandlersGameObject() {
    return buildingGraphicHandler.gameObject;
  }

  public override GameObject getBuildingsSetsHandlersGameObject() {
    return buildingsSet.buildingsSetGraphicHandler.gameObject;
  }

  public override Dictionary<Vector2Int, TypeOfBuildingTile> getBlueprint() {
    return blueprint;
  }

  public override void deleteGraphic() {
    buildingGraphicHandler.deleteGraphic();
  }

  public override List<Vector2Int> getTilesCoordinates() {
    return new List<Vector2Int>(tiles.Keys);
  }
}
