using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 200f;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        Debug.Log("적 HP: " + hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}