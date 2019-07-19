using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject playerObj;
    float distanceFromPlayer; //How far the gun should be from the centre object

    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlayer = 0.6f; //helps with positioning. Puts the gun a bit off centre of player
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetMouseButtonDown(0)) //on mouse left click
        {
            ShootProjectile(GetMousePoint() - playerObj.transform.position);
        }

        //RotateGunTowards();
    }

    private void FixedUpdate()
    {
        RotateGunTowards();
    }

    void RotateGunTowards() //spins gun towards mouse cursor
    {
        Vector3 mousePoint = GetMousePoint();
        Vector3 localPoint = mousePoint - playerObj.transform.position; //The mouse point relative to the player. Is useful.
        print(localPoint);

        if (localPoint.magnitude > 0.1f)//stops annoying case when mouse is directly over player
        {
            float newAngle = 0;
            newAngle = Vector3.SignedAngle(Vector3.up, localPoint, Vector3.forward); //OMG THEY ADDED THIS METHOD IT IS AMAZING

            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y, newAngle); //sets gun angle
            this.transform.position = playerObj.transform.position + localPoint.normalized * distanceFromPlayer; //sets gun position
        }
    }

    Vector3 GetMousePoint() //Get and return the mouse point
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        //print(point);

        return point;
    }

    void ShootProjectile(Vector3 shootPoint) //Spawns the bullet, and sets its travel direction
    {
        GameObject shotBullet = Instantiate(bulletObj, this.transform.position, Quaternion.identity);
        shotBullet.GetComponent<PlayerBulletController>().SetTravelDirection(shootPoint);
    }

}
