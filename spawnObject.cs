using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject[] Objects; 

    private void Start(){
        int rand = Random.Range(0,Objects.Length); 
        GameObject instance = (GameObject)Instantiate(Objects[rand],transform.position,Quaternion.identity);
        instance.transform.parent = transform;
    }
}
