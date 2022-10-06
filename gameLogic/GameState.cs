using System;
using System.Collections;
using System.Collections.Generic;

public class GameState {

  public PlayerState playerState;
  //public BuildingsSet buildingsSet;

  public GameState() {
    playerState = new PlayerState();
    //buildingsSet = new BuildingsSet();
  }

  // public void destroyBuilding(Vector2Int location) {
  //   buildingsSet.destroyBuilding(location, this);
  // }
  //
  // public bool placeBuilding(Building building, Vector2Int location) {
  //   return buildingsSet.placeBuilding(building, location, this);
  // }

  public void turn() {
    //buildingsSet.turn(this);
  }
}
