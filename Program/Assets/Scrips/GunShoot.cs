using UnityEngine;
using TMPro;
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

    [Header("UI")]
    public TMP_Text ammoText;

    private float nextFire;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        // 재장전 중에는 아무 동작도 하지 않음
        if (isReloading)
            return;

        // 탄약이 0발이면 자동 재장전
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // R키를 누르면 재장전
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        // 발사
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;

            Shoot();

            currentAmmo--;
            UpdateAmmoUI();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }

    IEnumerator Reload()
    {
        isReloading = true;

        ammoText.text = "Reloading...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;

        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = $"{currentAmmo} / {maxAmmo}";
    }
}