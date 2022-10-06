using UnityEngine;

public class BuildingTileGraphicFactory : MonoBehaviour {

  public Sprite spriteDefault;
  public Sprite spriteCamera;
  public Sprite spriteBook;

  public Sprite spriteBorder;

  public static BuildingTileGraphicFactory instance;

  // public BuildingTileGraphicHandler getBuildingTileGraphicHandler(
  //   BuildingTile tile,
  //   PlainBuilding building
  // ) {
  //   GameObject newObj = new GameObject();
  //   BuildingTileGraphicHandler handler =
  //     newObj.AddComponent<BuildingTileGraphicHandler>();
  //   handler.setProperties(
  //     getSprite(tile.typeOfBuildingTile, tile.building.typeOfBuilding),
  //     spriteBorder);
  //   newObj.transform.parent = building.buildingGraphicHandler.gameObject.transform;
  //   return handler;
  // }

  public BuildingTileGraphicHandler getBuildingTileGraphicHandler(
    TypeOfBuildingTile tile,
    PlainBuilding building
  ) {
    GameObject newObj = new GameObject();
    BuildingTileGraphicHandler handler =
      newObj.AddComponent<BuildingTileGraphicHandler>();
    handler.setProperties(
      getSprite(tile, building.typeOfBuilding),
      spriteBorder);
    newObj.transform.parent = building.getHandlersGameObject().transform;
    return handler;
  }

  public Sprite getSprite(TypeOfBuildingTile tile, TypeOfBuilding building) {
    switch(building) {
      case TypeOfBuilding.VIDEO_CAMERA:
        return spriteCamera;
      case TypeOfBuilding.PROCESSOR:
        return spriteDefault;
      case TypeOfBuilding.BOOK:
        return spriteBook;
    }
    return spriteDefault;
  }
}
