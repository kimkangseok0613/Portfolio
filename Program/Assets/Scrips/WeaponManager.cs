using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("ÃÑ »ý¼º À§Ä¡")]
    public Transform weaponParent;



    [Header("ÃÖ´ë º¸À¯ ÃÑ")]
    public int maxWeapon = 2;



    private GameObject[] weapons;


    private int currentWeaponIndex = -1;



    void Awake()
    {
        weapons = new GameObject[maxWeapon];
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
    }







    public void PickupWeapon(GameObject weaponPrefab)
    {

        for (int i = 0; i < weapons.Length; i++)
        {

            if (weapons[i] == null)
            {
                CreateWeapon(
                    weaponPrefab,
                    i
                );

                return;
            }

        }



        SwapWeapon(weaponPrefab);

    }







    void CreateWeapon(GameObject weaponPrefab, int slot)
    {

        GameObject newWeapon =
            Instantiate(
                weaponPrefab,
                weaponParent
            );



        // ÇÁ¸®ÆÕ ±âÁØ Àû¿ë
        WeaponSetting setting =
            newWeapon.GetComponent<WeaponSetting>();


        if (setting != null)
        {
            newWeapon.transform.localPosition =
                setting.position;


            newWeapon.transform.localEulerAngles =
                setting.rotation;


            newWeapon.transform.localScale =
                setting.scale;
        }



        weapons[slot] = newWeapon;



        EquipWeapon(slot);

    }







    public void EquipWeapon(int index)
    {

        if (index < 0 ||
           index >= weapons.Length)
            return;


        if (weapons[index] == null)
            return;



        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].SetActive(false);
            }
        }



        weapons[index].SetActive(true);


        currentWeaponIndex = index;


        Debug.Log(
            "ÀåÂø : " +
            weapons[index].name
        );

    }







    void SwapWeapon(GameObject weaponPrefab)
    {

        if (currentWeaponIndex == -1)
            return;



        Destroy(
            weapons[currentWeaponIndex]
        );



        CreateWeapon(
            weaponPrefab,
            currentWeaponIndex
        );

    }

}