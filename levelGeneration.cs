using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=XNQQLr0E9TY&t=11s

public class levelGeneration : MonoBehaviour
{
   public Transform[] startingPositions; 
   public GameObject[] rooms; 

   private int direction;
   public float moveAmount; 

    private float timeBtRoom; 
     public float startTimeBtRoom = 0.25f; 

    public float minX;
    public float maxX;
    public float minY; 

    private bool stopGeneration; 

    public LayerMask room; 

    int downCounter; 

   private void Start(){

       int randStartingPos = Random.Range(0,startingPositions.Length); 
       transform.position = startingPositions[randStartingPos].position;
       Instantiate(rooms[0],transform.position,Quaternion.identity);

       direction = Random.Range(1,6);
   }

   private void Update(){
    if(timeBtRoom <= 0 && stopGeneration == false){
        move(); 
        timeBtRoom = startTimeBtRoom; 

     }
        else{
        timeBtRoom -= Time.deltaTime; 
            }

    }

   private void move(){
    if(direction == 1 || direction == 2){//generate to the right 
     
        if(transform.position.x < maxX){
            downCounter = 0; 

            Vector2 newPos = new Vector2(transform.position.x + moveAmount,transform.position.y) ;
            transform.position = newPos;

             int rand = Random.Range(0,rooms.Length); 
            Instantiate(rooms[rand],transform.position,Quaternion.identity);

            direction = Random.Range(1,6);
                if(direction == 3){
                    direction = 2;

                      }
         else if(direction == 4){
            direction = 5; 
        }
    }
    else{
        direction = 5; 
    }
    
    }
    else if(direction == 3 || direction == 4){//generate to the left
    
    if(transform.position.x > minX){
        downCounter = 0; 

        Vector2 newPos = new Vector2(transform.position.x - moveAmount,transform.position.y) ;
        transform.position = newPos;

         int rand = Random.Range(0,rooms.Length); 
        Instantiate(rooms[rand],transform.position,Quaternion.identity);

        direction = Random.Range(3,6);
       
    }
    else{
        direction = 5; 
    }
   
    }
    else if(direction == 5){//generate down 

        downCounter++; 

        if(transform.position.y > minY){

        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position,1,room);
        if(roomDetection.GetComponent<roomType>().type !=1 && roomDetection.GetComponent<roomType>().type !=3){
           
           if(downCounter >= 2){
                 roomDetection.GetComponent<roomType>().roomDestruction();
               Instantiate(rooms[3],transform.position,Quaternion.identity);
           }
           else{

                roomDetection.GetComponent<roomType>().roomDestruction();

                int randBottomRoom = Random.Range(1,4);
                if (randBottomRoom==2){
                    randBottomRoom =1; 
                }

                Instantiate(rooms[randBottomRoom],transform.position,Quaternion.identity); 

           }


        }

        Vector2 newPos = new Vector2(transform.position.x,transform.position.y - moveAmount) ;
        transform.position = newPos;

        int rand = Random.Range(2,4);
         Instantiate(rooms[rand],transform.position,Quaternion.identity);

        direction = Random.Range(1,6);
        
    }
    else{//stop level generator 

        stopGeneration = true; 
    }
   
    }
    
    }

   
}
