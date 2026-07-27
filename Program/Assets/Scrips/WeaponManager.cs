using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [Header("카메라 밑 무기들을 순서대로 등록 (0번, 1번, 2번...)")]
    public GameObject[] weapons;

    [Header("UI (탄약 표시)")]
    public TMP_Text globalAmmoText;

    // 현재 들고 있는 무기의 인덱스 (-1은 맨손 상태)
    public int currentWeaponIndex = -1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    void Start()
    {
        Unarmed(); // 시작 시 맨손 상태
    }

    // 모든 무기를 끄고 맨손 상태로 만드는 함수
    public void Unarmed()
    {
        currentWeaponIndex = -1;
        HideAllWeapons();
        ShowEmptyUI();
    }

    // 카메라 밑의 모든 총을 끄는 함수
    public void HideAllWeapons()
    {
        if (weapons == null) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].SetActive(false);
            }
        }
    }

    // 인덱스 번호로 해당 무기만 켜는 함수
    public void EquipWeapon(int index)
    {
        if (weapons == null || index < 0 || index >= weapons.Length)
        {
            Debug.LogError($"[WeaponManager] {index}번 무기가 weapons 배열 범위를 벗어났습니다!");
            return;
        }

        // 1. 기존 모든 손 무기 비활성화
        HideAllWeapons();

        // 2. 지정된 번호의 무기만 활성화
        if (weapons[index] != null)
        {
            weapons[index].SetActive(true);
            currentWeaponIndex = index;
            Debug.Log($"[WeaponManager] {index}번 무기('{weapons[index].name}') 장착 완료!");
        }
    }

    public void ShowEmptyUI()
    {
        if (globalAmmoText != null)
        {
            globalAmmoText.text = "EMPTY";
        }
    }
}