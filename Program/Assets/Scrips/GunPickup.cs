using UnityEngine;
using TMPro;


public class GunPickup : MonoBehaviour
{

    [Header("ÁÙ ÃÑ ¹øÈ£")]
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

        if (playerNear &&
           Input.GetKeyDown(KeyCode.E))
        {

            Pickup();

        }

    }



    void Pickup()
    {

        if (WeaponManager.Instance != null)
        {

            WeaponManager.Instance
                .EquipWeapon(weaponIndex);

        }


        HideText();


        // Áß¿ä
        // ¹Ù´Ú ÃÑ »èÁ¦ ¾È ÇÔ
        // gameObject.SetActive(false);
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