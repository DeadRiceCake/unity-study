using UnityEngine;

public class Character : MonoBehaviour
{
    public float MaxHp = 3;
    public GameObject HPGauge;
    float HP;
    float HPMaxWidth;

    void Start()
    {
        HP = MaxHp;

        if (HPGauge != null)
        {
            HPMaxWidth = HPGauge.GetComponent<RectTransform>().sizeDelta.x;
        }
    }

    public void Init()
    {
        HP = MaxHp;
    }

    /// <summary>
    /// ��������� true, �׾����� false�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool Hit(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
        }

        if (HPGauge != null)
        {
            HPGauge.GetComponent<RectTransform>().sizeDelta = new Vector2
            (
                HPMaxWidth * HP / MaxHp, 
                HPGauge.GetComponent<RectTransform>().sizeDelta.y
            );
        }

        return HP > 0;
    }
}
