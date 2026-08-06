using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHp = 100f;
    private float hp;

    [Header("UI Reference")]
    public TextMeshProUGUI hpText;

    void Start()
    {
        hp = maxHp;
        UpdateHPUI();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hp = Mathf.Max(hp, 0f);

        Debug.Log("플레이어 HP : " + hp);

        UpdateHPUI();

        if (hp <= 0)
        {
            Die();
        }
    }

    void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = $"HP {Mathf.CeilToInt(hp)} / {Mathf.CeilToInt(maxHp)}";
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망");
        if (hpText != null)
        {
            hpText.text = $"HP 0 / " + Mathf.CeilToInt(maxHp);
        }
        GameManager.Instance.EndGame();
    }
}