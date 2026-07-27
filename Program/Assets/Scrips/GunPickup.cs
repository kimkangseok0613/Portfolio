using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject playerGun;

    public GameObject pickupUI;

    private bool canPickUp = false;

    // 플레이어가 현재 들고 있는 총
    private static GameObject currentGun;

    void Start()
    {
        // 바닥에 있는 총이면 플레이어 총 숨김
        if (playerGun != null)
            playerGun.SetActive(false);

        // 안내 UI 숨김
        if (pickupUI != null)
            pickupUI.SetActive(false);
    }

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        // 기존에 들고 있던 총 숨기기
        if (currentGun != null)
        {
            currentGun.SetActive(false);
        }

        // 새로운 총 장착
        playerGun.SetActive(true);

        // 현재 총 저장
        currentGun = playerGun;

        // UI 숨기기
        if (pickupUI != null)
            pickupUI.SetActive(false);

        // 바닥 총 제거
        //gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;

            if (pickupUI != null)
                pickupUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;

            if (pickupUI != null)
                pickupUI.SetActive(false);
        }
    }
}