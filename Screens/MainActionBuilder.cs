

public class MainActionBuilder{
  MainScript mainScript;

  public MainActionBuilder(MainScript mainScript) {
    this.mainScript = mainScript;
  }

  public void setActiveAction(
    MainController.ChosenAction chosenAction,
    MainController.CancelAction cancelAction
  ){
    mainScript.mainController.setChosenAction(
      chosenAction,
      cancelAction
    );
  }
}
