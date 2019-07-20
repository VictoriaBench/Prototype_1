using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject[] bulletObjArr; //0 for rock, 1 for papper, 2 for scissors. The actual bullets being shot
    public GameObject playerObj;
    public GameObject playerGun;
    public RuntimeAnimatorController[] animators;

    float distanceFromPlayer; //How far the gun should be from the centre object

    int [] bulletQueueArr = new int [2];
    public GameObject[] bulletQueueSpritesArr; //0 for rock, 1 for papper, 2 for scissors. Holds the Visual Sprites for next shot
    public GameObject nextShotObj;

    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlayer = 2f; //helps with positioning. Puts the gun a bit off-centre of player

        for (int i = 0; i < bulletQueueArr.Length; i++) { //Fill the bullet queue
            bulletQueueArr[i] = Random.Range(0, bulletObjArr.Length);
        }
        UpdateVisualQueue();
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
        //print(localPoint);

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
        GameObject shotBullet = Instantiate(bulletObjArr[bulletQueueArr[0]], this.transform.position, this.transform.rotation);
        LoadBulletToQueue(); 
        shotBullet.GetComponent<PlayerBulletController>().SetTravelDirection(shootPoint);
    }

    void LoadBulletToQueue() //removes the shot bullet and adds a new one to the back of the queue
    {
        for (int i = 0; i < bulletQueueArr.Length -1; i++)
        {
            bulletQueueArr[i] = bulletQueueArr[i + 1];
        }
        bulletQueueArr[bulletQueueArr.Length-1] = Random.Range(0, bulletObjArr.Length);
        UpdateVisualQueue();
    }

    void UpdateVisualQueue()
    {
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[bulletQueueArr[0]];
        nextShotObj.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[bulletQueueArr[1]];
        //nextShotObj = bulletQueueSpritesArr[bulletQueueArr[1]];   //Next shot object update
    }

}
