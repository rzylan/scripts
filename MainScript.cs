using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public MainController mainController;
    public UiController uiController;
    public BuildingTileGraphicFactory buildingTileGraphicFactory;
    public BuildingsSetGraphicFactory buildingsSetGraphicFactory;
    public BuildingGraphicFactory buildingGraphicFactory;

    public Notifier logicTurnNotifier = new Notifier();

    void Start()
    {
        BuildingTileGraphicFactory.instance = buildingTileGraphicFactory;
        BuildingsSetGraphicFactory.instance = buildingsSetGraphicFactory;
        BuildingGraphicFactory.instance = buildingGraphicFactory;

        mainController.turnAction = () =>
        {
            logicTurnNotifier.notify();
        };
        MapScreen mapScreen = new MapScreen(this);
    }
}
