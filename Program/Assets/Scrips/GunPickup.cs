using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [Header("획득할 총 프리팹")]
    public GameObject weaponPrefab;


    [Header("줍기 UI")]
    public GameObject pickupUI;


    private bool canPickUp = false;


    private WeaponManager weaponManager;



    void Start()
    {
        if (pickupUI != null)
        {
            pickupUI.SetActive(false);
        }
    }




    void Update()
    {
        if (canPickUp &&
           Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }





    void PickUp()
    {
        if (weaponManager == null)
            return;


        // WeaponManager에게 총 프리팹 전달
        weaponManager.PickupWeapon(weaponPrefab);



        if (pickupUI != null)
        {
            pickupUI.SetActive(false);
        }


        // 바닥 총 제거
        //Destroy(gameObject);
    }





    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;


            weaponManager =
                other.GetComponent<WeaponManager>();


            if (pickupUI != null)
            {
                pickupUI.SetActive(true);
            }
        }
    }





    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;


            if (pickupUI != null)
            {
                pickupUI.SetActive(false);
            }
        }
    }
}