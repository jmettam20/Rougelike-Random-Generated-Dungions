using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 
public class roomFirstDUngionGenerator : simpleRandomWalkmapGenerator
{
    [SerializeField]
 private int minRoomWidth =4, minRoomHeight = 4; 

[SerializeField]
 private int dungionWidth = 20, dungionHeight = 20; 

[SerializeField]
[Range(0,10)]
 private int offset = 1; 

[SerializeField]
 //private bool randomWalkRooms = false; 

 protected override void runProceduralGeneration(){
     CreateRooms();
  
 }

 private void CreateRooms(){
     var roomsList = proceduralGenerationAlgorythims.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
     new Vector3Int(dungionWidth,dungionHeight,0)),minRoomWidth,minRoomHeight);

     HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
     floor = CreateSimpleRooms(roomsList);


    List<Vector2Int> roomCenters = new  List<Vector2Int>();
    foreach (var room in roomsList)
    {
        roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));

    }
     HashSet<Vector2Int> corridoors = ConnectRooms(roomCenters);
        floor.UnionWith(corridoors);

     TileMapVisualiser.paintFloorTiles(floor);
     wallGenerator.CreateWalls(floor,TileMapVisualiser);
    }  

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters){
        HashSet<Vector2Int> corridoors = new HashSet<Vector2Int>();
        var currentRoomCentre = roomCenters[Random.Range(0,roomCenters.Count)];
        roomCenters.Remove(currentRoomCentre); 

        while (roomCenters.Count >0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCentre,roomCenters);
            roomCenters.Remove(closest);
         HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCentre,closest);
            currentRoomCentre = closest; 
            corridoors.UnionWith(newCorridor);
        }
        return corridoors; 
    }   
    

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination){
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>(); 
        var position = currentRoomCenter; 
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if(destination.y > position.y){

                position += Vector2Int.up;
            }else if(destination.y < position.y)
            {
                position += Vector2Int.down; 
            }
            corridor.Add(position); 
        }

        while (position.x != destination.x)
        {
            if(destination.x > position.x){
                position += Vector2Int.right; 
            }else if(destination.x < position.x){
                position += Vector2Int.left; 
            }
            corridor.Add(position); 
        }
        return corridor; 
    }
    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters){
        Vector2Int closest = Vector2Int.zero; 
        float distance = float.MaxValue; 

        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance (position,currentRoomCenter);
            if (currentDistance > distance)
            {
                distance = currentDistance; 
                closest = position; 
            }
        }
        return closest; 
    }
    

    

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList){
        HashSet<Vector2Int> floor  = new HashSet<Vector2Int>();

        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                    floor.Add(position);
                }
            }
        }
        return floor; 
    }
}
