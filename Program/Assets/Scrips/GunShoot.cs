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
        // 1. 매니저가 없거나 맨손 상태(currentWeaponIndex == -1)이면 동작 안 함
        if (WeaponManager.Instance == null || WeaponManager.Instance.currentWeaponIndex == -1)
        {
            return;
        }

        // 2. 이 총 오브젝트 자체가 켜져있지 않으면 발사 금지
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy)
        {
            return;
        }

        // 3. 재장전 중이면 발사 불가
        if (isReloading)
        {
            return;
        }

        // 자동 재장전
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // R키 수동 재장전
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        // 발사 (마우스 좌클릭)
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && muzzle != null)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                muzzle.position,
                Camera.main != null ? Camera.main.transform.rotation : transform.rotation
            );

            bullet.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] Bullet Prefab 또는 Muzzle이 설정되지 않았습니다!");
        }

        currentAmmo--;
        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (WeaponManager.Instance != null && gameObject.activeInHierarchy)
        {
            WeaponManager.Instance.globalAmmoText.text = "Reloading...";
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;

        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        // 내 총이 실제로 활성화되어 있을 때만 UI 업데이트
        if (gameObject.activeInHierarchy && WeaponManager.Instance != null && WeaponManager.Instance.globalAmmoText != null)
        {
            WeaponManager.Instance.globalAmmoText.text = currentAmmo + " / " + maxAmmo;
        }
    }
}