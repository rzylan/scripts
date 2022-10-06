public enum Direction {
  UP,
  RIGHT,
  DOWN,
  LEFT
}
[System.Serializable]
public static class DirectionFunctions {
  public static Direction turn(
    this Direction startDirection, Direction endDirection)
  {
    return (Direction)(((int)startDirection + (int)endDirection) % 4);
  }
  public static Vector2Int toVector2Int(this Direction direction) {
    switch(direction) {
      case Direction.UP:
      {
        return new Vector2Int(0,1);
      }
      case Direction.RIGHT:
      {
        return new Vector2Int(1,0);
      }
      case Direction.DOWN:
      {
        return new Vector2Int(0,-1);
      }
      case Direction.LEFT:
      {
        return new Vector2Int(-1,0);
      }
    }
    return new Vector2Int(0,0);
  }

  public static float getRotation(Direction direction) {
    switch(direction) {
      case Direction.UP:
      {
        return 270.0f;
      }
      case Direction.RIGHT:
      {
        return 180.0f;
      }
      case Direction.DOWN:
      {
        return 90.0f;
      }
      case Direction.LEFT:
      {
        return 0.0f;
      }
    }
    return 0.0f;
  }
}
