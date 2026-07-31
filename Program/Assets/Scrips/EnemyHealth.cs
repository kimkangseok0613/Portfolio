using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("체력")]
    public float maxHp = 200f;
    private float hp;

    [Header("체력바")]
    public Slider hpBar;

    private EnemySpawner spawner;


    void Start()
    {
        hp = maxHp;

        spawner = FindFirstObjectByType<EnemySpawner>();

        if (hpBar != null)
        {
            hpBar.maxValue = maxHp;
            hpBar.value = hp;
        }
    }


    public void TakeDamage(float damage)
    {
        hp -= damage;

        Debug.Log("적 HP : " + hp);


        if (hpBar != null)
        {
            hpBar.value = hp;
        }


        if (hp <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (spawner != null)
        {
            spawner.RespawnEnemy();
        }

        Destroy(gameObject);
    }
}