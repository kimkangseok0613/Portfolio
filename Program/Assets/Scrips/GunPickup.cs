using UnityEngine;
using TMPro;

public class GunPickup : MonoBehaviour
{
    [Header("ÃÑ ¹øÈ£")]
    public int weaponIndex;

    public TextMeshProUGUI text;

    bool playerNear;

    void Start()
    {
        if (text != null)
            text.gameObject.SetActive(false);
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
        if (WeaponManager.Instance != null)
        {
            // ½½·Ô ½Ã½ºÅÛÀ¸·Î ÃÑ È¹µæ
            WeaponManager.Instance.PickupWeapon(weaponIndex);
        }

        HideText();

        // ¹Ù´Ú ÃÑÀº °è¼Ó ³²°ÜµÒ
        // gameObject.SetActive(false);
    }

    void ShowText()
    {
        if (text != null)
        {
            // ÀÌ¹Ì °¡Áö°í ÀÖ´Â ÃÑÀÎÁö È®ÀÎ
            bool owned = false;

            if (WeaponManager.Instance != null)
            {
                for (int i = 0; i < WeaponManager.Instance.weaponSlots.Length; i++)
                {
                    if (WeaponManager.Instance.weaponSlots[i] == weaponIndex)
                    {
                        owned = true;
                        break;
                    }
                }
            }

            if (owned)
            {
                text.text = "Press [E] to Equip";
            }
            else
            {
                text.text = "Press [E] to Pick Up";
            }

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