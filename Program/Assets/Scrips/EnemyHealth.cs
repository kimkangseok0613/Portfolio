using System.Collections;
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

    private bool dead = false;


    [Header("죽음 효과")]
    public float blinkTime = 1.5f;   // 깜빡이는 총 시간
    public float blinkSpeed = 0.1f;  // 깜빡임 속도



    private Renderer[] renderers;



    void Start()
    {
        hp = maxHp;


        spawner =
            FindFirstObjectByType<EnemySpawner>();


        renderers =
            GetComponentsInChildren<Renderer>();



        if (hpBar != null)
        {
            hpBar.maxValue = maxHp;
            hpBar.value = hp;
        }
    }



    public void TakeDamage(float damage)
    {
        if (dead)
            return;


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
        dead = true;


        // AI 정지
        EnemyAI ai = GetComponent<EnemyAI>();

        if (ai != null)
        {
            ai.enabled = false;
        }



        // 스폰 예약
        if (spawner != null)
        {
            spawner.RespawnEnemy();
        }



        StartCoroutine(BlinkAndDestroy());
    }





    IEnumerator BlinkAndDestroy()
    {
        float timer = 0;


        bool visible = true;



        while (timer < blinkTime)
        {
            timer += blinkSpeed;


            visible = !visible;


            foreach (Renderer r in renderers)
            {
                r.enabled = visible;
            }


            yield return new WaitForSeconds(blinkSpeed);
        }



        Destroy(gameObject);
    }
}