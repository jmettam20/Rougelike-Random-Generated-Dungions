using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class wallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, tilemapVisualiser TileMapVisualiser){
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);

        foreach (var position in basicWallPositions)
        {
         TileMapVisualiser.PaintSingleBasicWall(position);   
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList){
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neighborPosition  = position + direction; 
                if(floorPositions.Contains(neighborPosition)== false)
                {
                    wallPositions.Add(neighborPosition);
                }

            }
            
        }
        return wallPositions; 
    }
}
