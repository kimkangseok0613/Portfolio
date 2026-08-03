using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 40f;
    public float lifeTime = 3f;

    [Header("회전 보정 (외형만)")]
    public Vector3 visualRotationOffset = new Vector3(90f, 0f, 0f); // 세워진 총알을 눕힐 각도

    private Vector3 moveDirection; // 날아갈 정면 방향

    void Start()
    {
        // 1. 총구가 바라보던 '진짜 정면 방향'을 먼저 저장합니다.
        moveDirection = transform.forward;

        // 2. 이동 방향을 저장한 후, 총알의 '외형 회전'만 눕혀줍니다.
        transform.Rotate(visualRotationOffset, Space.Self);

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 3. 외형이 회전했더라도, 아까 저장해둔 진짜 정면 방향으로 직진합니다.
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}