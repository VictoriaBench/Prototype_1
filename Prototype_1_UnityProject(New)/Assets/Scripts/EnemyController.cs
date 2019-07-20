using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyID; //0 for rock, 1 for paper, 2 for scissors.

    int hitPoints = 1;

    Vector3 spawnPoint; //The point where this enemy spawns.
    Vector3 playerPos; //The point where the player is.

    public float enemySpeed = 1f; //How fast the enemy moves.

    float lerpPos; //used to correctly lerp, no touchie.
    float lerpTime;
    float currLerp = 0;


    void Start()
    {
        SetRotation();
        spawnPoint = this.transform.position;
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerPos = Vector3.zero;

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

    void SetRotation() //rotates the enemy in relation to the player
    {
        float newAngle = 0;
        newAngle = Vector3.SignedAngle(Vector3.up, this.transform.position, Vector3.forward);

        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y, newAngle); //sets enemy angle
    }

    public void HitByBullet(int playerBulletID)
    {
        //Player hurt sound
        FindObjectOfType<SoundManager>().Play("Death Sound"); //find name of sound

        switch (enemyID)
        {
            case 0: //Enemy is rock
                switch (playerBulletID)
                {
                    case 0: //Enemy ROCK hit by player ROCK
                        hitPoints++;
                        break;
                    case 1: //Enemy ROCK hit by player PAPER
                        LoseHitPoint();
                        break;
                    case 2: //Enemy ROCK hit by player SCISSORS
                        lerpTime = lerpTime / 1.5f;
                        break;
                }
                break;
            case 1: //Enemy is paper
                switch (playerBulletID)
                {
                    case 0: //Enemy PAPER hit by player ROCK
                        lerpTime = lerpTime / 1.5f;
                        break;
                    case 1: //Enemy PAPER hit by player PAPER
                        hitPoints++;
                        break;
                    case 2: //Enemy PAPER hit by player SCISSORS
                        LoseHitPoint();
                        break;
                }
                break;
            case 2: //Enemy is scissors
                switch (playerBulletID) { 
                    case 0: //Enemy SCISSORS hit by player ROCK
                        LoseHitPoint();
                        break;
                    case 1: //Enemy SCISSORS hit by player PAPER
                        lerpTime = lerpTime / 1.5f;
                        break;
                    case 2: //Enemy SCISSORS hit by player SCISSORS
                        hitPoints++;
                        break;
                }
                break;
        }
        
    }

    void LoseHitPoint() //minuses a hit point AND deletes if dead.
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            print("Player hit!");
            GameObject.Find("Gun").GetComponent<GunController>().HitByEnemy();
            Destroy(this.gameObject);
        }
    }
}
