using UnityEngine;

public class FightScreen {
  MainScript mainScript;
  BuildingsSet<BattleBuildingLogic> player;
  BuildingsSet<BattleBuildingLogic> enemy;
  UiSet uiSet;
  GameState gameState;
  MainActionBuilder mainActionBuilder;
  Notifier.SimpleNotifier exitScreenNotifier;

  public FightScreen(
    MainScript mainScript,
    BuildingsSet<BattleBuildingLogic> player,
    GameState gameState,
    BattleBuildingFactory buildingFactory,
    Notifier.SimpleNotifier exitScreenNotifier
  ){
    this.mainScript = mainScript;
    this.exitScreenNotifier = exitScreenNotifier;
    this.gameState = gameState;
    this.player = player;

    mainActionBuilder = new MainActionBuilder(mainScript);
    uiSet = new UiSet(mainScript.uiController, mainScript.logicTurnNotifier);
    uiSet.addButton("Exit",()=>{
      mainScript.mainController.signleAction(()=>exit());
    });

    enemy = new BuildingsSet<BattleBuildingLogic>(new Vector2(-2.1f,0), new Vector2Int(4,4), buildingFactory);
    createExampleEnemy();
    setMainActionFunction();
  }

  public void createExampleEnemy() {
    BuildingFactory<BattleBuildingLogic> buildingFactory = enemy.getBuildingFactory();
    Building<BattleBuildingLogic> building;
    building = buildingFactory.getBuilding(TypeOfBuilding.BOOK, enemy);
    enemy.placeBuilding(building, new Vector2Int(0,0), gameState);
    building = buildingFactory.getBuilding(TypeOfBuilding.VIDEO_CAMERA, enemy);
    enemy.placeBuilding(building, new Vector2Int(0,1), gameState);
  }

  public void exit() {
    uiSet.clear();
    enemy.destroy(gameState);
    exitScreenNotifier();
  }

  public void setMainActionFunction() {
    mainScript.mainController.setChosenAction(
      (x, y, mouseInput, actions, selectedBuildingsSetCollider) => {
        BuildingsSet<BattleBuildingLogic> selectedBuildingsSet =
          (BuildingsSet<BattleBuildingLogic>)selectedBuildingsSetCollider.GetComponent<ObjectContainer>().obj;
        if(mouseInput == MouseInput.LEFT_UP) {
          if(selectedBuildingsSet == player) {
            selectedBuildingsSet.callIfPresent(
              new Vector2Int(x,y),
              (BuildingTile<BattleBuildingLogic> tile)=>{
                tile.building.logic.mainAction(
                  gameState,
                  mainActionBuilder,
                  ()=>setMainActionFunction()
                );
              }
            );
          }
        }
      },
      () => {}
    );
  }
}
