using UnityEngine;
using TMPro;

public class GunPickup : MonoBehaviour
{
    [Header("이 바닥 아이템이 부여할 무기 번호 (0, 1, 2...)")]
    public int weaponIndex = 0;

    public TextMeshProUGUI text;
    private bool playerNear = false;

    void Start()
    {
        HideText();
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            Pickup(); 
        }
    }

    void Pickup()
    {
        // 1. 매니저에게 숫자 번호만 전달해서 플레이어 총을 켬
        if (WeaponManager.Instance != null)
        {
            WeaponManager.Instance.EquipWeapon(weaponIndex);
        }

        HideText();

        // 2. [핵심] Destroy 대신 자기 자신을 비활성화(숨기기)만 수행
        //gameObject.SetActive(false);
    }

    void ShowText()
    {
        if (text != null)
        {
            text.text = "Press [E] to get";
            text.gameObject.SetActive(true);
        }
    }

    void HideText()
    {
        if (text != null)
        {
            text.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            ShowText();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            HideText();
        }
    }
}