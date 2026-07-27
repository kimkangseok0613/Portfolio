using UnityEngine;
using TMPro;
using System.Collections;

public abstract class GunBase : MonoBehaviour
{
    [Header("총 설정")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;

    [Header("탄약")]
    public int maxAmmo = 10;
    protected int currentAmmo;

    [Header("UI")]
    public TMP_Text ammoText;

    protected float nextFire;
    protected bool isReloading;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }
    protected virtual void Awake()
    {
        currentAmmo = maxAmmo;
    }
    protected virtual void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;

            Shoot();

            currentAmmo--;
            UpdateAmmoUI();
        }
    }

    // 자식 클래스가 반드시 구현
    protected abstract void Shoot();

    protected virtual IEnumerator Reload()
    {
        isReloading = true;

        ammoText.text = "Reloading...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;

        UpdateAmmoUI();
    }

    protected virtual void UpdateAmmoUI()
    {
        ammoText.text = $"{currentAmmo} / {maxAmmo}";
    }
}