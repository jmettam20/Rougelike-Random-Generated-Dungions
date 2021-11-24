using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class tilemapVisualiser : MonoBehaviour
{
    [SerializeField]
   private Tilemap floorTilemap, wallTilemap;

    [SerializeField]
   private TileBase floorTile, wallTop;

   

   public void paintFloorTiles(IEnumerable<Vector2Int> floorPositions){
       PaintTiles(floorPositions,floorTilemap,floorTile);

   }

   private void PaintTiles(IEnumerable<Vector2Int> positions,Tilemap tilemap,TileBase tile){

       foreach(var position in positions){
           paintSingleTile(tilemap,tile,position);

           

       }

   }

   internal void PaintSingleBasicWall(Vector2Int position){
       paintSingleTile(wallTilemap,wallTop,position);

   }

    private void paintSingleTile(Tilemap tilemap,TileBase tile,Vector2Int position){

        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);

    }

    public void Clear(){
    floorTilemap.ClearAllTiles();
    wallTilemap.ClearAllTiles(); 

    }

}
