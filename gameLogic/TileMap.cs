using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TileMap<T> where T : BuildingLogic{
  public delegate void Action(BuildingTile<T> tile);

  public Dictionary<Vector2Int, BuildingTile<T>> tiles =
    new Dictionary<Vector2Int, BuildingTile<T>>();
  public TileMap() {
  }

  public bool addTiles(
    Dictionary<Vector2Int, BuildingTile<T>> newTiles
  ) {
    foreach(KeyValuePair<Vector2Int, BuildingTile<T>> entry in newTiles)
    {
      if(tiles.ContainsKey(entry.Key)) {
        return false;
      }
    }
    foreach(KeyValuePair<Vector2Int, BuildingTile<T>> entry in newTiles)
    {
      tiles.Add(entry.Key, entry.Value);
      entry.Value.created(entry.Key);
    }
    return true;
  }

  public void deleteTiles(Dictionary<Vector2Int, BuildingTile<T>> tilesToDelete) {
    foreach(KeyValuePair<Vector2Int, BuildingTile<T>> entry in tilesToDelete)
    {
      if(tiles.ContainsKey(entry.Key)) {
        tiles.Remove(entry.Key);
        entry.Value.deleted();
      }
    }
  }

  public bool callIfPresent(Vector2Int location, Action action) {
    if(tiles.ContainsKey(location)) {
      action(tiles[location]);
      return true;
    } else {
      return false;
    }
  }
}
