using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Spawn,
        Move,
        Die
    }

    public float speed = 2;

    public Material flashMaterial;
    public Material defaultMaterial;

    public AudioClip hitSound;
    public AudioClip deadSound;

    GameObject target;
    State state;

    void Start()
    {

    }

    public void Spwan(GameObject target)
    {
        this.target = target;
        state = State.Spawn;
        GetComponent<Character>().Init();
        GetComponent<Animator>().SetTrigger("Spawn");
        Invoke("StartMove", 1);
        GetComponent<Collider2D>().enabled = false;
    }

    private void StartMove()
    {
        state = State.Move;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Animator>().SetTrigger("Run");
    }

    private void FixedUpdate()
    {
        if (state == State.Move)
        {
            Vector2 direction = target.transform.position - transform.position;

            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);

            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            float damage = collision.GetComponent<Bullet>().damage;

            if (GetComponent<Character>().Hit(damage))
            {
                GetComponent<AudioSource>().PlayOneShot(hitSound);
                // �Ѿ˿� �°� ��������� ���� ��¦��
                Flash();
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(deadSound);
                Die();
            }
        }
    }

    void Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("AfterFlash", 0.1f); // 0.1�� �Ŀ� AfterFlash �Լ��� �����մϴ�.
    }

    private void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    void Die()
    {
        state = State.Die;
        GetComponent<Animator>().SetTrigger("Die");

        // ���� ������ 0.7�� �Ŀ� �ı��մϴ�.
        Invoke("AfterDying", 0.7f);
    }

    void AfterDying()
    {
        gameObject.SetActive(false);
    }
}
