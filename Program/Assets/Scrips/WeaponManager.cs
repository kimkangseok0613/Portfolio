// WeaponManager.cs

using UnityEngine;
using TMPro;
using System.Collections;


public class WeaponManager : MonoBehaviour
{

    public static WeaponManager Instance;



    [Header("플레이어 카메라 밑 총들")]
    public GameObject[] weapons;



    [Header("탄약 UI")]
    public TMP_Text globalAmmoText;



    [Header("무기 슬롯 UI")]
    public TMP_Text slot1Text;
    public TMP_Text slot2Text;



    [Header("무기 교체")]
    public float weaponSwitchDelay = 0.5f;



    // 현재 장착 총 번호
    public int currentWeaponIndex = -1;



    // 슬롯
    // -1 = 비어있음
    public int[] weaponSlots = new int[2]
    {
        -1,
        -1
    };



    // 현재 슬롯
    public int currentSlot = -1;



    // 교체 중 여부
    public bool isSwitching;



    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }






    void Start()
    {

        HideAllWeapons();

        UpdateSlotUI();

    }







    void Update()
    {

        if (isSwitching)
            return;



        // 1번 무기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(
                SwitchWeapon(0)
            );
        }



        // 2번 무기
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(
                SwitchWeapon(1)
            );
        }



        // X = 맨손
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(
                SwitchNone()
            );
        }

    }









    // 모든 총 끄기

    public void HideAllWeapons()
    {

        for (int i = 0; i < weapons.Length; i++)
        {

            if (weapons[i] != null)
            {
                weapons[i].SetActive(false);
            }

        }

    }









    // 총 줍기

    public void PickupWeapon(int weaponIndex)
    {

        // 이미 가진 총인지 확인

        for (int i = 0; i < weaponSlots.Length; i++)
        {

            if (weaponSlots[i] == weaponIndex)
            {

                StartCoroutine(
                    SwitchWeapon(i)
                );

                return;

            }

        }






        // 빈 슬롯 찾기

        for (int i = 0; i < weaponSlots.Length; i++)
        {

            if (weaponSlots[i] == -1)
            {

                weaponSlots[i] = weaponIndex;


                UpdateSlotUI();


                StartCoroutine(
                    SwitchWeapon(i)
                );


                return;

            }

        }






        // 슬롯이 꽉 찬 경우 현재 슬롯 교체

        if (currentSlot == -1)
        {
            currentSlot = 0;
        }


        weaponSlots[currentSlot] =
            weaponIndex;



        UpdateSlotUI();



        StartCoroutine(
            SwitchWeapon(currentSlot)
        );

    }









    // 실제 장착

    public void EquipSlot(int slot)
    {

        if (slot < 0 ||
            slot >= weaponSlots.Length)
            return;



        int index =
            weaponSlots[slot];



        if (index == -1)
            return;



        if (index >= weapons.Length)
            return;



        HideAllWeapons();



        weapons[index].SetActive(true);



        currentWeaponIndex =
            index;



        currentSlot =
            slot;



        UpdateSlotUI();



        Debug.Log(
            "장착 : "
            + weapons[index].name
        );

    }









    // 무기 교체

    IEnumerator SwitchWeapon(int slot)
    {

        if (slot < 0 ||
            slot >= weaponSlots.Length)
            yield break;



        if (weaponSlots[slot] == -1)
            yield break;




        isSwitching = true;



        HideAllWeapons();



        if (globalAmmoText != null)
        {
            globalAmmoText.text =
                "Switching...";
        }





        yield return new WaitForSeconds(
            weaponSwitchDelay
        );





        EquipSlot(slot);



        isSwitching = false;

    }









    // 맨손

    IEnumerator SwitchNone()
    {

        isSwitching = true;



        HideAllWeapons();



        currentWeaponIndex =
            -1;



        currentSlot =
            -1;



        if (globalAmmoText != null)
        {
            globalAmmoText.text =
                "";
        }





        yield return new WaitForSeconds(
            weaponSwitchDelay
        );



        UpdateSlotUI();



        isSwitching = false;

    }









    // UI 업데이트

    public void UpdateSlotUI()
    {

        if (slot1Text != null)
        {

            if (weaponSlots[0] == -1)
            {
                slot1Text.text =
                    "1 : Empty";
            }
            else
            {

                string name =
                    weapons[weaponSlots[0]].name;



                if (currentSlot == 0)
                {
                    slot1Text.text =
                        ">>1 : " + name;
                }
                else
                {
                    slot1Text.text =
                        "1 : " + name;
                }

            }

        }







        if (slot2Text != null)
        {

            if (weaponSlots[1] == -1)
            {
                slot2Text.text =
                    "2 : Empty";
            }
            else
            {

                string name =
                    weapons[weaponSlots[1]].name;



                if (currentSlot == 1)
                {
                    slot2Text.text =
                        ">>2 : " + name;
                }
                else
                {
                    slot2Text.text =
                        "2 : " + name;
                }

            }

        }

    }


}