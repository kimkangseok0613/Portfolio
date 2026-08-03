// GunShoot.cs

using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour
{
    [SerializeField] public static int zoomWeaponIndex;

    [Header("총 설정")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    [Tooltip("총알이 세워져서 나갈 때 회전 보정값 (예: X=90, Y=0, Z=0)")]
    public Vector3 bulletRotationOffset = new Vector3(90f, 0f, 0f); // 기본값 X축 90도 회전

    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;


    [Header("탄약")]
    public int maxAmmo = 10;


    private int currentAmmo;

    private float nextFire;

    private bool isReloading;


    // 탄약 저장 여부
    private bool initializedAmmo = false;

    public static bool canShoot = true;

    void Awake()
    {
        currentAmmo = maxAmmo;
    }




    void OnEnable()
    {
        // 처음 얻은 총이면 탄약 풀 충전
        if (!initializedAmmo)
        {
            currentAmmo = maxAmmo;

            initializedAmmo = true;
        }


        isReloading = false;


        UpdateAmmoUI();
    }





    void OnDisable()
    {
        StopAllCoroutines();

        isReloading = false;


        // 총을 바꿀 때 현재 탄약 저장
        SaveAmmo();
    }






    void Update()
    {
        if (WeaponManager.Instance != null &&
    WeaponManager.Instance.isSwitching)
        {
            return;
        }

        if (WeaponManager.Instance == null)
            return;



        // 현재 장착 총인지 확인
        if (WeaponManager.Instance.currentWeaponIndex == -1)
            return;



        if (WeaponManager.Instance.weapons[
            WeaponManager.Instance.currentWeaponIndex]
            != gameObject)
        {
            return;
        }




        if (!gameObject.activeInHierarchy)
            return;



        if (isReloading)
            return;





        // 자동 재장전
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());

            return;
        }





        // 수동 재장전
        if (Input.GetKeyDown(KeyCode.R)
            && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());

            return;
        }


        if (!canShoot)
            return;


        // 발사
        if (Input.GetMouseButton(0)
            && Time.time >= nextFire)
        {
            nextFire =
                Time.time + fireRate;


            Shoot();
        }

    }







    void Shoot()
    {
        if (bulletPrefab == null) return;
        if (muzzle == null) return;

        // 1. Muzzle의 원래 방향 그대로 총알 생성 (이동 방향 보장)
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        // 2. 총알 충돌 무시 로직
        Collider bulletCol = bullet.GetComponent<Collider>();
        Collider gunCol = GetComponent<Collider>();

        if (bulletCol != null && gunCol != null)
        {
            Physics.IgnoreCollision(bulletCol, gunCol);
        }

        currentAmmo--;
        UpdateAmmoUI();
    }









    IEnumerator Reload()
    {

        isReloading = true;



        if (WeaponManager.Instance != null &&
            WeaponManager.Instance.globalAmmoText != null)
        {
            WeaponManager.Instance.globalAmmoText.text =
                "Reloading...";
        }




        yield return new WaitForSeconds(
            reloadTime
        );





        currentAmmo =
            maxAmmo;



        isReloading = false;



        UpdateAmmoUI();

    }









    void SaveAmmo()
    {
        // 현재 탄약 저장
        // OnDisable 시 자동 호출
        // 다음 OnEnable에서 유지됨
    }









    void UpdateAmmoUI()
    {

        if (WeaponManager.Instance == null)
            return;



        if (WeaponManager.Instance.currentWeaponIndex == -1)
            return;




        if (WeaponManager.Instance.weapons[
            WeaponManager.Instance.currentWeaponIndex]
            != gameObject)
        {
            return;
        }





        if (WeaponManager.Instance.globalAmmoText != null)
        {
            WeaponManager.Instance.globalAmmoText.text =
                currentAmmo +
                " / " +
                maxAmmo;
        }

    }

}