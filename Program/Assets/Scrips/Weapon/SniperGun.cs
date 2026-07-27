using UnityEngine;

public class SniperRifle : GunBase
{
    protected override void Awake()
    {
        fireRate = 1.5f;
        maxAmmo = 5;

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