using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    ////////// PUBLIC FIELDS //////////
    public int enemyHealth = 3;
    public GameObject gibEffect;
    public float attackDelay = 1;
    public float attackDistance = 5;
	public Transform particleSpawner;

    ////////// PRIVATE FIELDS //////////
    GameObject player;
    PlayerController playerController;
    float aiDelay = 0.2f;
    bool aiUpdate = true;
    float attackStep = 0.0f;
    LevelManager lvlMan;
    UnityEngine.AI.NavMeshAgent agent;
    

    void Start()
    {
        ////////// INITIALIZATIONS //////////
        GameObject LM = GameObject.FindGameObjectWithTag("Level Manager");
        lvlMan = LM.GetComponent<LevelManager>();
        lvlMan.numberOfEnemies.Add(this.gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (aiUpdate == true)
        {
            StartCoroutine(EnemyAIDelay());
        }
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }

		Animator spiderBotAnim = this.transform.GetChild (0).GetComponent<Animator> ();

        if (agent.velocity.x < 0.001 && agent.velocity.z < 0.001 && Vector3.Distance(this.gameObject.transform.position, player.transform.position) < attackDistance)
        {
            if (Time.time > attackStep)
            {
                attackStep = Time.time + attackDelay;
                StartCoroutine(EnemyAttack());
				spiderBotAnim.SetBool ("Attack", true);
            }
        }
        else if (agent.velocity.x > 0.001 && agent.velocity.z > 0.001 && Vector3.Distance(this.gameObject.transform.position, player.transform.position) > attackDistance)
        {
            StopCoroutine(EnemyAttack());
			spiderBotAnim.SetBool ("Attack", false);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MyBullet(Clone)")
        {
            enemyHealth--;
        }
    }

    void EnemyDeath()
    {
        Instantiate(gibEffect, particleSpawner.transform.position, particleSpawner.transform.rotation);
        Destroy(gameObject);
        lvlMan.numberOfEnemies.Remove(this.gameObject);
    }

    IEnumerator EnemyAIDelay()
    {
        aiUpdate = false;
        yield return new WaitForSeconds(aiDelay);
        agent.destination = player.transform.position;
        aiUpdate = true;
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        playerController.playerHealth--;
    }
}
