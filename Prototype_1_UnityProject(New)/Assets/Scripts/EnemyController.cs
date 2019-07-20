using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyID; //0 for rock, 1 for paper, 2 for scissors.

    Vector3 spawnPoint; //The point where this enemy spawns.
    Vector3 playerPos; //The point where the player is.

    public float enemySpeed = 2.5f; //How fast the enemy moves.

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

    public void HitByBullet()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Gun").GetComponent<GunController>().HitByEnemy();
        }
    }
}
