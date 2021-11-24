using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class proceduralGenerationAlgorythims 
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength){

        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i = 0; i < walkLength; i++){
            var newPosition = previousPosition + Direction2D.getRandomCardinalDirection(); //generates random direction until walk length is reached 
            path.Add(newPosition); //adds new pos
            previousPosition = newPosition; //set previous pos to new
        }
        return path; 


    }

    public static List<Vector2Int> RandomWalkCoridor(Vector2Int startPosition, int corridoorLength){
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.getRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridoorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition); 
        } 
        return corridor; 
    } 

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight){
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt> ();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        while(roomsQueue.Count > 0){
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f){
                    if(room.size.y >= minHeight * 2){
                        SplitHorizontally(minHeight,roomsQueue,room);
                    }else if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth,roomsQueue,room);
                    }else{
                        roomsList.Add(room);
                    }
                }
                else{
                   
                     if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth,roomsQueue,room);
                    }
                    if(room.size.y >= minHeight * 2){
                        SplitHorizontally(minHeight,roomsQueue,room);
                    }
                    else{
                        roomsList.Add(room);
                    }
                }
            }
        }
        return(roomsList);
    }
    private static void SplitVertically(int minWidth,Queue<BoundsInt> roomsQueue,BoundsInt room){
        var xSplit = Random.Range(1,room.size.x);
        BoundsInt room1 = new BoundsInt(room.min,new Vector3Int(xSplit,room.size.y,room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit,room.min.y,room.min.z),
        new Vector3Int(room.size.x - xSplit,room.size.y,room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight,Queue<BoundsInt> roomsQueue,BoundsInt room){
        var ySplit = Random.Range(1,room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.y));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x,room.min.y + ySplit,room.min.z),
        new Vector3Int(room.size.x,room.size.y - ySplit,room.size.z)); 
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

}



public static class Direction2D{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>{//defines 4 different directions as Vector2Int
        new Vector2Int(0,1), //Up
         new Vector2Int(1,0), //Right
          new Vector2Int(0,-1), //Down
           new Vector2Int(-1,0) //Left

    };

    public static Vector2Int getRandomCardinalDirection(){//picks a random direction 
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];

    }
}
