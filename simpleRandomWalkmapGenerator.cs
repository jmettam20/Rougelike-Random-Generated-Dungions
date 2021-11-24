using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class simpleRandomWalkmapGenerator : abstractDungionGenerator
{
    [SerializeField]
   protected SimpleRandomWalkSO randomWalkPeramiters; 
 


    protected override void runProceduralGeneration(){
        HashSet<Vector2Int> floorPositions = runRandomWalk(randomWalkPeramiters,startPosition);
        TileMapVisualiser.Clear();
        TileMapVisualiser.paintFloorTiles(floorPositions);
        wallGenerator.CreateWalls(floorPositions,TileMapVisualiser);


        }


    
protected HashSet<Vector2Int> runRandomWalk(SimpleRandomWalkSO peramiters,Vector2Int position){

    var currentPosition = position; 

    HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

    for(int i = 0 ;i<peramiters.iterations;i++){

        var path = proceduralGenerationAlgorythims.SimpleRandomWalk(currentPosition,peramiters.walkLength); //run random walk algorythm


        floorPositions.UnionWith(path);//create floor positions 

        if(peramiters.startRandomlyEachItteration){
             currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));

                }
    }
    return floorPositions; 

}

  


}