using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System; 

public class coridorFirstDungionGenerator : simpleRandomWalkmapGenerator
{
    [SerializeField]
    private int coridorLength = 14, coridorCount = 5;

[SerializeField]
[Range(0.1f,1)]
    private float roomPercent = 0.8f;

    
    protected override void runProceduralGeneration()
    {
        coridorFistGeneration();
    }

    private void coridorFistGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCoridors(floorPositions,potentialRoomPositions); 

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions); 

        TileMapVisualiser.paintFloorTiles(floorPositions);
        wallGenerator.CreateWalls(floorPositions, TileMapVisualiser); 
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors){
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position)==false){
                var room = runRandomWalk(randomWalkPeramiters,position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions){
        List<Vector2Int> deadEnds = new List<Vector2Int>(); 

        foreach (var position in floorPositions)
        {
            int neighborsCount = 1; 

            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if(floorPositions.Contains(position + direction)){
                    neighborsCount++; 
                }
                 if(neighborsCount == 0){
                     deadEnds.Add(position);
                 }
            }
        }
        return deadEnds;

    }

    private HashSet<Vector2Int> CreateRooms (HashSet<Vector2Int> potentialRoomPositions){
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count*roomPercent);  
        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = runRandomWalk(randomWalkPeramiters,roomPosition);
            roomPositions.UnionWith(roomFloor); 
        }
        return roomPositions; 
    }
 
    private void CreateCoridors(HashSet<Vector2Int> floorPositions,  HashSet<Vector2Int> potentialRoomPositions){
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < coridorCount; i++)
        {
            var corridor = proceduralGenerationAlgorythims.RandomWalkCoridor(currentPosition,coridorLength);
            currentPosition = corridor [corridor.Count - 1];

            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);

        }
    }
}
