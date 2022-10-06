

public abstract class BuildingLogic{
  public virtual bool checkIfCanBePlaced(
    Vector2Int location, GameState gameState
  ){return true;}
  public virtual void afterPlace(GameState gameState){}
  public virtual void afterDestroy(GameState gameState){}
  public virtual void afterPlaceListeners(GameState gameState){}
  public virtual void afterDestroyListeners(GameState gameState){}
  public abstract BuildingGraphicHandler getBuildingGraphicHandler();
}
