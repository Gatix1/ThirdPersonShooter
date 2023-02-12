using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int starterHealth = 100;
    public int health = 100;
    private bool isDead = false;
    public Animator animator;
    public NavMeshAgent enemy;
    public GameObject player;
    public bool isAttacking = false;
    public float visibleRange = 1.0f;
    void Start()
    {
        InvokeRepeating("attackPlayer", 0f, 2f);
    }

    void Update()
    {
        animator.SetBool("isMoving", enemy.velocity.x!=0);
        if(health <= 0 && !isDead)
        {
            isDead = true;
            animator.enabled = false;
            animator.enabled = true;
            animator.SetTrigger("deathTrigger");
            StartCoroutine("Death");
        }
        if(!isDead)
        {
            float distance = Vector3.Distance(player.transform.position,transform.position);
            Debug.Log(distance);
            bool playerIsCloseEnough = (distance <= visibleRange); 
            if(playerIsCloseEnough || health != starterHealth)
                enemy.SetDestination(player.transform.position);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }
    public void attackPlayer()
    {
        if(isAttacking)
        {
            animator.SetTrigger("attackTrigger");
            if(player.GetComponent<PlayerMotor>().isEnemyColliding)
                player.GetComponent<PlayerMotor>().health--;
        }
    }

    public IEnumerator Death()
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            player = other.gameObject;
            isAttacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            isAttacking = false;
        }
    }
}
