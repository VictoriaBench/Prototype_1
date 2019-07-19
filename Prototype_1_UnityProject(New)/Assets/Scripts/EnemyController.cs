using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 spawnPoint; //The point where this enemy spawns.
    Vector3 playerPos; //The point where the player is.

    float enemySpeed = 5f; //How fast the enemy moves.

    float lerpPos; //used to correctly lerp, no touchie.
    float lerpTime;
    float currLerp = 0;


    void Start()
    {
        spawnPoint = this.transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        lerpTime = (spawnPoint - playerPos).magnitude / enemySpeed; //How long it should take to reach the destination.
    }

    // Update is called once per frame
    void Update()
    {
        TravelTowardsPlayer();
    }

    void TravelTowardsPlayer() //Moves the enemy towards the player
    { //uses a lerp. Can be changed to use a basic 'position += distance' method.
        currLerp += Time.deltaTime / lerpTime;
        this.transform.position = Vector3.Lerp(spawnPoint, playerPos, currLerp);
    }
}
