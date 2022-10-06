
public class BattleBuildingLogic : BuildingLogic {
  public int health = 10;

  Building<BattleBuildingLogic> building;
  BattleBuildingGraphicHandler buildingGraphicHandler;
  public BattleBuildingLogic(Building<BattleBuildingLogic> building){
    this.building = building;
    buildingGraphicHandler =
      BuildingGraphicFactory.instance.getBattleBuildingGraphicHandler(building);
    building.buildingGraphicHandler = buildingGraphicHandler;

  }

  public virtual void mainAction(
    GameState gameState,
    MainActionBuilder mainActionBuilder,
    Notifier.SimpleNotifier endMainAction)
  {
      building.buildingGraphicHandler.addBorder();
      mainActionBuilder.setActiveAction(
        (x, y, mouseInput, actions, selectedBuildingsSetCollider) => {
          BuildingsSet<BattleBuildingLogic> selectedBuildingsSet =
            (BuildingsSet<BattleBuildingLogic>)selectedBuildingsSetCollider.GetComponent<ObjectContainer>().obj;
        if(mouseInput == MouseInput.LEFT_UP) {
          if(selectedBuildingsSet != building.buildingsSet) {
              selectedBuildingsSet.callIfPresent(
                new Vector2Int(x,y),
                (tile)=> {
                  tile.building.logic.hurt(4, gameState);
                }
              );
              endMainAction();
          }
        }
      },
      () => {
        building.buildingGraphicHandler.deleteBorder();
      }
    );
  }

  public override void afterPlace(GameState gameState){
    this.building.buildingGraphicHandler.addText("health", health + "hp");
  }

  public void hurt(int damage, GameState gameState) {
    health -= damage;
    if(health <= 0) {
      this.building.destroy(gameState);
    } else {
      this.building.buildingGraphicHandler.changeText("health", health + "hp");
    }
  }

  public override BuildingGraphicHandler getBuildingGraphicHandler(){
    //return buildingGraphicHandler;
    return BuildingGraphicFactory.instance.getBattleBuildingGraphicHandler(building);
  }
}
