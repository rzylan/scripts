using System.Collections;
using System.Collections.Generic;

public class BattleBuildingFactory : BuildingFactory<BattleBuildingLogic>{
  public BattleBuildingFactory() {
  }

  public override Building<BattleBuildingLogic> getBuilding(TypeOfBuilding typeOfBuilding, BuildingsSet<BattleBuildingLogic> buildingsSet) {
    Building<BattleBuildingLogic> building = new Building<BattleBuildingLogic>(typeOfBuilding, buildingsSet);
    building.logic = new BattleBuildingLogic(building);
    return building;
  }

  // public Building getBuilding(TypeOfBuilding typeOfBuilding) {
  //   if(typeOfBuilding == TypeOfBuilding.BOOK)
  //   {
  //     return getBook();
  //   }
  //   else if(typeOfBuilding == TypeOfBuilding.VIDEO_CAMERA)
  //   {
  //     return getVideoCamera();
  //   }
  //   else// if(typeOfBuilding == TypeOfBuilding.PROCESSOR)
  //   {
  //     return getProcessor();
  //   }
  // }
  //
  // public Building getBook(){
  //   Building createdBuilding =  new Building(buildingsSet);
  //   return createdBuilding;
  // }
  //
  // public Building getVideoCamera(){
  //   Building createdBuilding =  new Building(buildingsSet);
  //   return createdBuilding;
  // }
  //
  // public Building getProcessor(){
  //   Building createdBuilding =  new Building(buildingsSet);
  //   return createdBuilding;
  // }
}
