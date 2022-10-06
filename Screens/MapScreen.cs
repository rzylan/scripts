using UnityEngine;

public class MapScreen{
  MainScript mainScript;
  BuildingsSet<BattleBuildingLogic> player;
  GameState gameState;
  BattleBuildingFactory buildingFactory;

  public MapScreen(MainScript mainScript) {
    this.mainScript = mainScript;
    this.buildingFactory = new BattleBuildingFactory();
    player = new BuildingsSet<BattleBuildingLogic>(new Vector2(2.1f,0), new Vector2Int(4,4), buildingFactory);
    gameState = new GameState();
    createScreen();
  }

  public void createBuildScreen() {
    new BuildScreen<BattleBuildingLogic>(
      mainScript,
      player,
      gameState,
      () => {createScreen();}
    );
  }

  public void createFightScreen() {
    new FightScreen(
      mainScript,
      player,
      gameState,
      buildingFactory,
      () => {createScreen();}
    );
  }


  public void createScreen() {
    mainScript.mainController.setEmptyAction();
    UiSet uiSet = new UiSet(mainScript.uiController, mainScript.logicTurnNotifier);
    uiSet.addButton("Go to build",()=>mainScript.mainController.signleAction(
      ()=>{
        createBuildScreen();
        uiSet.clear();
    }));
    uiSet.addButton("Go to fight",()=>mainScript.mainController.signleAction(
      ()=>{
        createFightScreen();
        uiSet.clear();
    }));
  }
}
