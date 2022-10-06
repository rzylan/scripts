using UnityEngine;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BuildingGraphicHandler : MonoBehaviour {
  public PlainBuilding building;
  public Sprite borderSprite;
  public GameObject borderParent;
  public Dictionary<string, TextMesh> texts =
    new Dictionary<string, TextMesh>();

  public void setProperties(PlainBuilding building, Sprite borderSprite) {
    this.building = building;
    this.borderSprite = borderSprite;
  }

  public void deleteGraphic() {
    Destroy(gameObject);
  }

  protected void addChild(
    Vector2Int coordinates, Direction direction, float layer, Sprite sprite
  ) {
    int x = coordinates.x;
    int y = coordinates.y;
    GameObject borderObject = new GameObject("Field_"+direction);
    borderObject.transform.parent = borderParent.transform;
    borderObject.transform.localPosition = new Vector3(x+0.5f,y+0.5f,layer);

    borderObject.transform.rotation = Quaternion.Euler(0, 0, DirectionFunctions.getRotation(direction));

    SpriteRenderer spriteR = borderObject.AddComponent<SpriteRenderer>();
    spriteR.sprite = sprite;
  }

  public void addBorder() {
    addBorder(-1.0f);
  }

  public void addBorder(float layer) {
    borderParent = new GameObject("Border");
    borderParent.transform.parent = gameObject.transform;
    borderParent.transform.localPosition = new Vector3(0,0,0);
    List<Vector2Int> coordinates = building.getTilesCoordinates();
    foreach(Vector2Int v in coordinates) {
      v.forEachAdjacent((Vector2Int neighbour, Direction direction)=>{
        if(!coordinates.Contains(neighbour)) {
          addChild(v, direction, layer, borderSprite);
        }
      });
    }
  }

  public void deleteBorder() {
    Destroy(borderParent);
  }

  public void addText(string name, string text) {
    GameObject textObject = new GameObject("Text");
    textObject.transform.parent = gameObject.transform;
    Vector2Int coordinates = building.getTilesCoordinates()[0];
    textObject.transform.localPosition = new Vector3(coordinates.x, coordinates.y, -1.0f);
    textObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    TextMesh textComponent = textObject.AddComponent<TextMesh>();
    textComponent.fontSize = 24;
    textComponent.text = text;
    texts.Add(name, textComponent);
  }

  public void changeText(string name, string text) {
    texts[name].text = text;
  }

  public void deleteText(string name) {
    texts.Remove(name);
    Destroy(texts[name].gameObject);
  }
}
