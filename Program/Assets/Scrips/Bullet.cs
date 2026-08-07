using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("총알")]
    public float speed = 20f;

    public float damage = 40f;

    public float lifeTime = 3f;



    [Header("넉백")]
    public float knockbackPower = 8f;



    [Header("회전 보정")]
    public Vector3 visualRotationOffset =
        new Vector3(90f, 0f, 0f);



    private Vector3 moveDirection;





    void Start()
    {

        // 총구 방향 저장

        moveDirection =
            transform.forward;



        // 외형만 회전

        transform.Rotate(
            visualRotationOffset,
            Space.Self
        );



        Destroy(
            gameObject,
            lifeTime
        );

    }







    void Update()
    {

        transform.position +=
            moveDirection *
            speed *
            Time.deltaTime;

    }







    private void OnCollisionEnter(
        Collision collision)
    {

        EnemyHealth enemy =
            collision.gameObject
            .GetComponent<EnemyHealth>();



        if (enemy != null)
        {

            Vector3 direction =
                (collision.transform.position -
                 transform.position)
                 .normalized;



            direction.y = 0;



            enemy.TakeDamage(
                damage,
                direction,
                knockbackPower
            );

        }



        Destroy(gameObject);

    }

}