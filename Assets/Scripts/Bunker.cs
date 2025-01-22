using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int hitsTaken;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[0];

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        hitsTaken = 0;
        ResetBunker();
    }

    public void ResetBunker()
    {
        hitsTaken = 0;
        gameObject.SetActive(true);
        spriteRenderer.sprite = sprites[0];
    }

    private void Update()
    {
        if (hitsTaken >= 15)
        {
            gameObject.SetActive(false);
        } else if (hitsTaken >= 10) {
            spriteRenderer.sprite = sprites[2];
        } else if (hitsTaken >= 5) { 
            spriteRenderer.sprite = sprites[1];
        }
        
        
    }

    // if the invader crashes into a bunkerm the entire bunker goes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invaders"))
        {
            hitsTaken += 5;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets") || collision.gameObject.layer == LayerMask.NameToLayer("Missiles"))
        {
           hitsTaken++;
        }
    }

}