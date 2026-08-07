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



    private bool dead = false;



    [Header("죽음 효과")]
    public float blinkTime = 1.5f;
    public float blinkSpeed = 0.1f;



    private Renderer[] renderers;



    void Start()
    {

        hp = maxHp;


        renderers =
            GetComponentsInChildren<Renderer>();



        if (hpBar != null)
        {
            hpBar.maxValue = maxHp;
            hpBar.value = hp;
        }

    }







    // 기본 데미지
    public void TakeDamage(float damage)
    {

        if (dead)
            return;



        hp -= damage;



        Debug.Log(
            "적 HP : " + hp
        );



        if (hpBar != null)
        {
            hpBar.value = hp;
        }



        if (hp <= 0)
        {
            Die();
        }

    }







    // 총알 피격용
    public void TakeDamage(
        float damage,
        Vector3 hitDirection,
        float knockbackPower)
    {

        if (dead)
            return;



        hp -= damage;



        Debug.Log(
            "적 HP : " + hp
        );



        if (hpBar != null)
        {
            hpBar.value = hp;
        }





        // 넉백 전달

        EnemyAI ai =
            GetComponent<EnemyAI>();


        if (ai != null)
        {

            ai.Knockback(
                hitDirection,
                knockbackPower
            );

        }






        if (hp <= 0)
        {
            Die();
        }

    }









    void Die()
    {

        if (dead)
            return;


        dead = true;



        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddKill();
        }



        EnemyAI ai =
            GetComponent<EnemyAI>();


        if (ai != null)
        {
            ai.Die();
        }



        StartCoroutine(
            BlinkAndDestroy()
        );

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

                if (r != null)
                    r.enabled = visible;

            }



            yield return new WaitForSeconds(
                blinkSpeed
            );

        }




        Destroy(gameObject);

    }

}