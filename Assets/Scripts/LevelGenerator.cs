using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator sharedInstance;

    //[nivel_, nivel_2, nivel_3...]
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPoint;
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

    void Awake() {
        sharedInstance = this;    
    }

    void Start() {
        GenerateInitialBlocks();
    }

    public void AddLevelBlock(){

        //Generamos un número aleatorio entero entre a<= y b<
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        currentBlock.transform.SetParent(this.transform, false); //Pone el nuevo bloque de nivel como hijo del LevelGenerator

        Vector3 spawnPosition = Vector3.zero;

        if(currentBlocks.Count == 0) {
            spawnPosition = levelStartPoint.position;
        } else {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(spawnPosition.x-currentBlock.startPoint.position.x, spawnPosition.y-currentBlock.startPoint.position.y, 0);

        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);
    }

    public void RemoveOldestLevelBlock(){

        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);

    }

    public void RemoveAllTheBlocks(){

        while(currentBlocks.Count>0){
            RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlocks(){
        for (int i = 0; i < 5; i++){
            AddLevelBlock();
        }
    }
}
