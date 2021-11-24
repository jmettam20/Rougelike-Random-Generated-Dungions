using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstractDungionGenerator : MonoBehaviour
{
    [SerializeField]
   protected tilemapVisualiser TileMapVisualiser = null;
   [SerializeField]
   protected Vector2Int startPosition = Vector2Int.zero;

   public void GenerateDungion(){
       TileMapVisualiser.Clear(); 
       runProceduralGeneration();

   } 
    protected abstract void runProceduralGeneration();

}
