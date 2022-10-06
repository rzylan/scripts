using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTileGraphicHandler : MonoBehaviour {
  public Sprite sprite;
  public Sprite borderSprite;

  public void setProperties(Sprite sprite, Sprite borderSprite) {
    this.sprite = sprite;
    this.borderSprite = borderSprite;
  }

  public void createGraphic(Vector2Int vector2Int) {
    addField(
      vector2Int,
      2.0f,
      sprite
    );
  }

  public void addBorder(List<Vector2Int> coordinates, Vector2Int location) {
    addBorder(
      coordinates,
      location,
      1.8f,
      borderSprite
    );
  }

  protected void addField(Vector2Int coordinates, float layer, Sprite sprite) {
    int x = coordinates.x;
    int y = coordinates.y;
    gameObject.name = "Field_"+x+"_"+y;
    gameObject.transform.localPosition = new Vector3(x+0.5f,y+0.5f,layer);
    SpriteRenderer spriteR = gameObject.AddComponent<SpriteRenderer>();
    spriteR.sprite = sprite;
  }

  protected void addChild(
    Vector2Int coordinates, Direction direction, float layer, Sprite sprite
  ) {
    int x = coordinates.x;
    int y = coordinates.y;
    GameObject borderObject = new GameObject("Field_"+direction);
    borderObject.transform.localPosition = new Vector3(x+0.5f,y+0.5f,layer);

    borderObject.transform.rotation = Quaternion.Euler(0, 0, DirectionFunctions.getRotation(direction));

    SpriteRenderer spriteR = borderObject.AddComponent<SpriteRenderer>();
    spriteR.sprite = sprite;

    borderObject.transform.parent = gameObject.transform;
  }

  public void addBorder(List<Vector2Int> coordinates, Vector2Int location, float layer, Sprite sprite) {
    foreach(Vector2Int v in coordinates) {
      v.forEachAdjacent((Vector2Int neighbour, Direction direction)=>{
        if(!coordinates.Contains(neighbour)) {
          addChild(location.add(v), direction, layer, sprite);
        }
      });
    }
  }

  public void deleteGraphic() {
    Destroy(gameObject);
  }

  public void moveGraphic(Vector2Int coordinates) {
    gameObject.transform.localPosition = new Vector3(
      coordinates.x+0.5f, coordinates.y+0.5f,gameObject.transform.localPosition.z);
  }
}
