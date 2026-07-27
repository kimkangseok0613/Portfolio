using UnityEngine;

public class Rifle : GunBase
{
    protected override void Awake()
    {
        fireRate = 0.08f;
        maxAmmo = 30;

        base.Awake();
    }


    protected override void Shoot()
    {
        Instantiate(
            bulletPrefab,
            muzzle.position,
            muzzle.rotation
        );
    }
}