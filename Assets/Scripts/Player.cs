using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public ActualGM gm;
    public Projectiles bulletPrefab;
    public Projectiles bullet;

    public float speed = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<ActualGM>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 position = transform.position;

        // can't use "Input.GetAxis("Horizontal") as it makes the movement 'floaty' / almost as if on ice. Not Precise feeling.
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        // keep the player within the camera / playfield
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.6f, rightEdge.x - 0.6f);

        transform.position = position;

        if (bullet == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missiles") || other.gameObject.layer == LayerMask.NameToLayer("Invaders"))
        {
            Debug.Log("Hit registered");
            // Play player death animation
            // Play player death SFX

            gm.OnPlayerKilled(this);
        }
    }

}
