using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage = 1;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
