using UnityEngine;


public class GunShoot : GunBase
{


    protected override void Shoot()
    {

        Instantiate(
            bulletPrefab,
            muzzle.position,
            muzzle.rotation
        );

    }


}