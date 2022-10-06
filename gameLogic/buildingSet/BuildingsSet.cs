using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuildingsSet<T> where T : BuildingLogic {
  public TileMap<T> tileMap;
  public List<Building<T>> buildings = new List<Building<T>>();
  public delegate void BuildingChange(Building<T> building);
  public Dictionary<System.Object, BuildingChange> buildingPlaceListeners =
    new Dictionary<System.Object, BuildingChange>();
  public Dictionary<System.Object, BuildingChange> buildingDestroyListeners =
    new Dictionary<System.Object, BuildingChange>();
  public BuildingFactory<T> buildingFactory;
  public BuildingsSetGraphicHandler buildingsSetGraphicHandler;

  public BuildingsSet(Vector2 inputPozition, Vector2Int inputSize, BuildingFactory<T> buildingFactory) {
    tileMap = new TileMap<T>();
    this.buildingFactory = buildingFactory;
    buildingsSetGraphicHandler =
      BuildingsSetGraphicFactory.instance.getBuildingsSetGraphicHandler<T>(inputPozition, inputSize, this);
  }

  public void destroyBuilding(Vector2Int location, GameState gameState) {
    tileMap.callIfPresent(
      location,
      (tile)=> {
        tile.building.destroy(gameState);
        buildings.Remove(tile.building);
        foreach(KeyValuePair<System.Object, BuildingChange> action in buildingDestroyListeners) {
          action.Value(tile.building);
        }
        tile.building.logic.afterDestroyListeners(gameState);
      }
    );
  }

  public void destroyBuilding(Building<T> building, GameState gameState) {
    building.destroy(gameState);
    buildings.Remove(building);
    foreach(KeyValuePair<System.Object, BuildingChange> action in buildingDestroyListeners) {
      action.Value(building);
    }
    building.logic.afterDestroyListeners(gameState);
  }

  public bool placeBuilding(
    Building<T> building, Vector2Int location, GameState gameState
  ) {
    bool isPlaced = building.place(location, gameState);
    if(isPlaced) {
      buildings.Add(building);
      foreach(KeyValuePair<System.Object, BuildingChange> action in buildingPlaceListeners) {
        action.Value(building);
      }
      building.logic.afterPlaceListeners(gameState);
    }
    return isPlaced;
  }

  public void turn(GameState gameState) {
    // foreach(Building building in buildings) {
    //   building.logic.action(gameState);
    // }
  }

  public BuildingFactory<T> getBuildingFactory() {
    return buildingFactory;
  }

  public void destroy(GameState gameState) {
    for(int i = buildings.Count - 1; i>=0; i--) {
      destroyBuilding(buildings[i], gameState);
    }

    buildingsSetGraphicHandler.destroy();
  }

  public bool callIfPresent(Vector2Int location, TileMap<T>.Action action) {
    return tileMap.callIfPresent(location, action);
  }
}
