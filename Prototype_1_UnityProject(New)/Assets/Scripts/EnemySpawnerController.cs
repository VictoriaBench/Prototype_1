using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject [] enemyObjArr; //put your enemies in here

    float spawnRate = 3;    //Spawn rate of enemies (can be changed in runtime)
    //float spawnEnemySpeed = 5;  //How fast the enemies move (can be changed in runtime)
    float spawnDistance = 10;    //How far away the enemies spawn from the player

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnTimer"); //Your main boi. It's the lamb sauce.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTimer() //coroutine to continually spawn enemies.
    {
        while (true) //spooky
        {
            yield return new WaitForSeconds(spawnRate); //Hopefully adapts to new spawnrates
            SpawnEnemy();
        }
    }

    void SpawnEnemy()   //Spawns a random
    {
        Vector3 newSpawnPos = new Vector3 (Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); //Get a random direction to spawn enemy from
        newSpawnPos = newSpawnPos.normalized * spawnDistance;

        Instantiate(enemyObjArr[Random.Range(0, enemyObjArr.Length)], newSpawnPos, Quaternion.identity);
        
    }


}
