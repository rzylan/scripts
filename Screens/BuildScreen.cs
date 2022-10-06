using UnityEngine;

public class BuildScreen<T> where T : BuildingLogic {
  MainScript mainScript;
  BuildingsSet<T> buildingsSet1;
  UiSet uiSet;
  GameState gameState;
  Notifier.SimpleNotifier exitScreenNotifier;

  public BuildScreen(
    MainScript mainScript,
    BuildingsSet<T> buildingSet,
    GameState gameState,
    Notifier.SimpleNotifier exitScreenNotifier
  ){
    this.mainScript = mainScript;
    this.exitScreenNotifier = exitScreenNotifier;
    this.gameState = gameState;

    mainScript.logicTurnNotifier.subscribe(
      this,
      () => {
        gameState.turn();
        buildingsSet1.turn(gameState);
      }
    );

    uiSet = new UiSet(mainScript.uiController, mainScript.logicTurnNotifier);

    buildingsSet1 = buildingSet;
    setBuildingActions(buildingsSet1);
  }

  public void setBuildingActions(BuildingsSet<T> buildingsSet) {
    uiSet.clear();
    uiSet.addButton("Exit",()=>
        mainScript.mainController.signleAction(()=>exit()));
    uiSet.addButton("Delete",()=>{setDeleteAction(buildingsSet);});
    uiSet.addButton("Build book",()=>{
      setBuildAction(TypeOfBuilding.BOOK, buildingsSet);});
    uiSet.addButton("Build camera",()=>{
      setBuildAction(TypeOfBuilding.VIDEO_CAMERA, buildingsSet);});
    uiSet.addButton("Build processor",()=>{
      setBuildAction(TypeOfBuilding.PROCESSOR, buildingsSet);});
    uiSet.addText(() =>
      "money: " + gameState.playerState.money);
    uiSet.addText(() =>
      "unprocessed knowledge: " + gameState.playerState.unprocessedKnowledge);
  }

  public void setBuildAction(TypeOfBuilding  typeOfBuilding, BuildingsSet<T> buildingsSet) {
    BuildingFactory<T> buildingFactory = buildingsSet.getBuildingFactory();
    Building<T> building = buildingFactory.getBuilding(typeOfBuilding, buildingsSet);
    BuildingCursor buildingCursor = new BuildingCursor(building);

    mainScript.mainController.setChosenAction(
      (x, y, mouseInput, actions, selectedBuildingsSetCollider) => {
        BuildingsSet<T> selectedBuildingsSet =
          (BuildingsSet<T>)selectedBuildingsSetCollider.GetComponent<ObjectContainer>().obj;
        if(selectedBuildingsSet == buildingsSet) {
          if(mouseInput == MouseInput.LEFT_UP) {
            actions.Add(() => {
              buildingCursor.removeCursor();
              buildingsSet.placeBuilding(building, new Vector2Int(x,y), gameState);
              building = buildingFactory.getBuilding(typeOfBuilding, buildingsSet);
              buildingCursor = new BuildingCursor(building);
            });
          } else if(mouseInput == MouseInput.NONE) {
            buildingCursor.cursorMoved(new Vector2Int(x, y));
          }
        }
      },
      () => {
        buildingCursor.removeCursor();
      }
    );
  }

  public void setDeleteAction(BuildingsSet<T> buildingsSet) {
    mainScript.mainController.setChosenAction(
      (x, y, mouseInput, actions, selectedBuildingsSetCollider) => {
        BuildingsSet<T> selectedBuildingsSet =
          (BuildingsSet<T>)selectedBuildingsSetCollider.GetComponent<ObjectContainer>().obj;
        if(mouseInput == MouseInput.LEFT_UP) {
          selectedBuildingsSet.destroyBuilding(new Vector2Int(x,y), gameState);
        }
      },
      () => {}
    );
  }

  public void exit() {
    uiSet.clear();
    mainScript.mainController.setEmptyAction();
    //buildingsSet1.destroy(gameState);
    exitScreenNotifier();
  }
}
