using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    public float cooldownTime = 5f;
    public KeyCode skillKey = KeyCode.E;

    public Image skillImage; // 스킬 아이콘
    public TextMeshProUGUI cooldownText; // 숫자 표시

    public Color normalColor = Color.white; // 기본 색상
    public Color cooldownColor = new Color(0.3f, 0.3f, 0.3f); // 어두운 색상

    private float cooldownTimer = 0f;
    private bool isCooldown = false;

    void Start()
    {
        cooldownText.gameObject.SetActive(false);
        skillImage.color = normalColor;
    }

    void Update()
    {
        // 스킬 사용
        if (Input.GetKeyDown(skillKey) && !isCooldown)
        {
            UseSkill();
        }

        // 쿨타임 진행
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            cooldownText.gameObject.SetActive(true);
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            // 이미지 어둡게
            skillImage.color = cooldownColor;

            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0;
                isCooldown = false;

                // 숫자 숨기기
                cooldownText.gameObject.SetActive(false);

                // 이미지 원상복구
                skillImage.color = normalColor;
            }
        }
    }

    void UseSkill()
    {
        Debug.Log("스킬 사용!");

        isCooldown = true;
        cooldownTimer = cooldownTime;

        // 스킬 효과 추가
    }
}