using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;

    public float fireRate = 0.2f;

    private float nextFire;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            muzzle.position,
            muzzle.rotation
        );
    }
}