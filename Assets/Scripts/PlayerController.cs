using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bulletPrefab;

    public Material flashMaterial;
    public Material defaultMaterial;

    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip deadSound;

    Vector3 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move += new Vector3(1, 0, 0);
        }

        move = move.normalized;

        if (move.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (move.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (move.magnitude > 0)
        {
            GetComponent<Animator>().SetTrigger("Move");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Stop");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        worldPosition -= transform.position;

        GameObject newBullet = GetComponent<ObjectPool>().Get();

        if (newBullet != null)
        {
            newBullet.transform.position = transform.position;
            newBullet.GetComponent<Bullet>().Direction = worldPosition;
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(move * speed * Time.fixedDeltaTime);
    }

    // isTrigger가 아닌 오브젝트끼리 충돌할 때 호출됩니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (GetComponent<Character>().Hit(1))
            {
                GetComponent<AudioSource>().PlayOneShot(hitSound);

                // 플레이어가 살아있으면 피격 효과를 줍니다.
                Flash();
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(deadSound);

                // 플레이어가 죽으면 게임 오버 처리를 합니다.
                Die();
            }
        }
    }

    void Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("AfterFlash", 0.1f); // 0.1초 후에 AfterFlash 함수를 실행합니다.
    }

    private void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");

        // 적이 죽으면 0.7초 후에 파괴합니다.
        Invoke("AfterDying", 0.875f);
    }

    void AfterDying()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
