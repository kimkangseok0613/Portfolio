using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;


    [Header("ÇÃ·¹ÀÌ¾î Ä«¸Þ¶ó ¹Ø ÃÑµé")]
    public GameObject[] weapons;


    [Header("Åº¾à UI")]
    public TMP_Text globalAmmoText;


    // ÇöÀç ÀåÂø ÃÑ ¹øÈ£
    public int currentWeaponIndex = -1;



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
    }



    // ¸ðµç ÃÑ ²ô±â
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



    // ÃÑ ÀåÂø
    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogError("Àß¸øµÈ ÃÑ ¹øÈ£ : " + index);
            return;
        }


        // ±âÁ¸ ÃÑ ¼û±è
        HideAllWeapons();


        // ¼±ÅÃÇÑ ÃÑ ÄÑ±â
        weapons[index].SetActive(true);


        currentWeaponIndex = index;


        Debug.Log(
            "ÀåÂø ¿Ï·á : "
            + weapons[index].name
        );
    }
}