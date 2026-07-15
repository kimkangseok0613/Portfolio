using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject playerGun;

    public GameObject pickupUI;

    private bool canPickUp = false;

    void Start()
    {
        // 플레이어 총은 처음에 숨김
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
        // 플레이어 총 활성화
        playerGun.SetActive(true);

        // UI 숨기기
        if (pickupUI != null)
            pickupUI.SetActive(false);

        // 바닥 총 숨기기
        gameObject.SetActive(false);
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