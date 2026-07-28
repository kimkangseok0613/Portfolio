using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour
{
    [Header("총 설정")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;


    [Header("탄약")]
    public int maxAmmo = 10;


    private int currentAmmo;
    private float nextFire;
    private bool isReloading;



    void Awake()
    {
        currentAmmo = maxAmmo;
    }



    void OnEnable()
    {
        // 무기를 다시 들면 탄약 초기화
        currentAmmo = maxAmmo;

        isReloading = false;

        UpdateAmmoUI();
    }



    void OnDisable()
    {
        StopAllCoroutines();
        isReloading = false;
    }



    void Update()
    {
        // 무기 매니저 체크
        if (WeaponManager.Instance == null)
            return;


        // 현재 들고 있는 총인지 확인
        if (WeaponManager.Instance.currentWeaponIndex == -1)
            return;


        // 비활성화 총 발사 방지
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
        if (bulletPrefab == null)
        {
            Debug.LogWarning(
                "Bullet Prefab이 없습니다."
            );
            return;
        }


        if (muzzle == null)
        {
            Debug.LogWarning(
                "Muzzle이 연결되지 않았습니다."
            );
            return;
        }



        // 총알 생성
        GameObject bullet =
            Instantiate(
                bulletPrefab,
                muzzle.position,
                muzzle.rotation
            );



        Debug.Log(
            "총알 생성 위치 : "
            + bullet.transform.position
        );



        // 총알이 총과 충돌하지 않도록 설정
        Collider bulletCol =
            bullet.GetComponent<Collider>();


        Collider gunCol =
            GetComponent<Collider>();


        if (bulletCol != null &&
           gunCol != null)
        {
            Physics.IgnoreCollision(
                bulletCol,
                gunCol
            );
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



        currentAmmo = maxAmmo;

        isReloading = false;


        UpdateAmmoUI();
    }







    void UpdateAmmoUI()
    {
        if (gameObject.activeInHierarchy &&
           WeaponManager.Instance != null &&
           WeaponManager.Instance.globalAmmoText != null)
        {
            WeaponManager.Instance.globalAmmoText.text =
                currentAmmo +
                " / " +
                maxAmmo;
        }
    }
}