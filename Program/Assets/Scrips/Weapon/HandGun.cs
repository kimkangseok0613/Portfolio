using UnityEngine;

public class HandGun : GunBase
{
    protected override void Awake()
    {
        fireRate = 0.3f;
        maxAmmo = 12;

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