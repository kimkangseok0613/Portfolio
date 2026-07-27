using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("플레이어가 가지고 있는 총들")]
    public GameObject[] weapons;

    private GameObject currentWeapon;

    void Start()
    {
        // 시작 시 모든 총 비활성화
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // 첫 번째 총 장착(원하면 삭제 가능)
        //if (weapons.Length > 0)
        //{
        //    EquipWeapon(weapons[0]);
        //}
    }

    public void EquipWeapon(GameObject newWeapon)
    {
        // 모든 총 비활성화
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // 선택한 총만 활성화
        newWeapon.SetActive(true);
        currentWeapon = newWeapon;
    }
}