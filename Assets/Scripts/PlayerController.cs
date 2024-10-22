using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bulletPrefab;
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
}
