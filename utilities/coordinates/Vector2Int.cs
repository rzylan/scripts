using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Vector2Int {
  public delegate void IterateOverVectorFunction(Vector2Int vector2Int);
  public delegate void IterateOverVectorAndDirectionFunction(
    Vector2Int vector2Int, Direction direction);
  public readonly int x, y;
  public Vector2Int(int x, int y) {
    this.x = x;
    this.y = y;
  }
  public bool equals(Vector2Int vector2Int) {
    return vector2Int.x == x && vector2Int.y == y;
  }
  public Vector2Int(float x, float y) {
    this.x = (int)Mathf.Floor(x);
    this.y = (int)Mathf.Floor(y);
  }
  public override bool Equals(System.Object obj)
  {
    if ((obj == null) || ! this.GetType().IsAssignableFrom(obj.GetType()))
    {
       return false;
    }
    else {
       Vector2Int p = (Vector2Int) obj;
       return (x == p.x) && (y == p.y);
    }
  }
  public override int GetHashCode()
  {
     return (x << 2) ^ y;
  }
  public Vector2Int add(Vector2Int v) {
    return new Vector2Int(x + v.x, y + v.y);
  }
  public Vector2Int up(){
    return new Vector2Int(x, y+1);
  }
  public Vector2Int down(){
    return new Vector2Int(x, y-1);
  }
  public Vector2Int rigth(){
    return new Vector2Int(x+1, y);
  }
  public Vector2Int left(){
    return new Vector2Int(x-1, y);
  }

  public void forEachAdjacent(
    IterateOverVectorFunction iterateOverVectorFunction
  ) {
    iterateOverVectorFunction(new Vector2Int(x-1, y));
    iterateOverVectorFunction(new Vector2Int(x+1, y));
    iterateOverVectorFunction(new Vector2Int(x, y-1));
    iterateOverVectorFunction(new Vector2Int(x, y+1));
  }

  public void forEachAdjacent(
    IterateOverVectorAndDirectionFunction function
  ) {
    function(new Vector2Int(x-1, y), Direction.LEFT);
    function(new Vector2Int(x+1, y), Direction.RIGHT);
    function(new Vector2Int(x, y-1), Direction.DOWN);
    function(new Vector2Int(x, y+1), Direction.UP);
  }

  public void forEachNeighbour(
    IterateOverVectorFunction iterateOverVectorFunction
  ) {
    iterateOverVectorFunction(new Vector2Int(x-1, y));
    iterateOverVectorFunction(new Vector2Int(x+1, y));
    iterateOverVectorFunction(new Vector2Int(x, y-1));
    iterateOverVectorFunction(new Vector2Int(x, y+1));
    iterateOverVectorFunction(new Vector2Int(x-1, y-1));
    iterateOverVectorFunction(new Vector2Int(x-1, y+1));
    iterateOverVectorFunction(new Vector2Int(x+1, y-1));
    iterateOverVectorFunction(new Vector2Int(x+1, y+1));
  }

  public static void forEachInDirection(
    IterateOverVectorFunction iterateOverVectorFunction, Vector2Int location, Direction direction, int length)
  {
    Vector2Int directionVector = direction.toVector2Int();
    for(int i = 0; i<length; i++) {
      iterateOverVectorFunction(location);
      location = location.add(directionVector);
    }
  }

  public int distanceTo(Vector2Int v, bool withDiagonal) {
    if(withDiagonal) {
      return distanceTo(v);
    } else {
      return distanceToWithDiagonal(v);
    }
  }

  public int distanceTo(Vector2Int v) {
    return Math.Abs(x - v.x) + Math.Abs(y - v.y);
  }

  public int distanceToWithDiagonal(Vector2Int v) {
    return Math.Max(Math.Abs(x - v.x), Math.Abs(y - v.y));
  }

  public static int distanceBetween(
    List<Vector2Int> vectors1, List<Vector2Int> vectors2, bool withDiagonal)
  {
    int minDistance = int.MaxValue;
    int distance;
    foreach(Vector2Int v1 in vectors1) {
      foreach(Vector2Int v2 in vectors2) {
        distance = v1.distanceTo(v2, withDiagonal);
        if(distance < minDistance) {
          minDistance = distance;
        }
      }
    }
    return minDistance;
  }
}
