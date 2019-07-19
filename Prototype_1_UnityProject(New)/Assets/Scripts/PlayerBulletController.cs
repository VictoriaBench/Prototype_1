using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    float travelSpeed = 5;  //The speed of the player bullet
    Vector3 travelDirection = Vector3.right;    //The direction of the player bullet (updated by the gun script)
    float timeUntilDestroy = 10;    

    void Start()
    {
        Destroy(this.gameObject, timeUntilDestroy); //Deletes bullets after a set time to prevent gameobject spam
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();   //Moves the bullet at a constant speed
    }

    void MoveObject()   //Moves the bullet at a constant speed
    {
        this.transform.position += travelDirection.normalized * travelSpeed * Time.deltaTime;
    }

    public void SetTravelDirection(Vector3 newTravelDir)    //Used to set the travel direction of the bullet
    {
        travelDirection = newTravelDir;
    }

}
