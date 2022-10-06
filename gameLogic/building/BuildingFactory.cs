using System.Collections;
using System.Collections.Generic;

public abstract class BuildingFactory<T> where T : BuildingLogic{

  public abstract Building<T> getBuilding(TypeOfBuilding typeOfBuilding, BuildingsSet<T> buildingsSet);
  // {
  //   return new Building(typeOfBuilding, buildingsSet);
  // }
}
