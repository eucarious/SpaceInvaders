using UnityEngine;

public class Invader : MonoBehaviour
{
    public ActualGM gm;
    public Sprite[] animationSprites = new Sprite[0];
    public float animationTime = 1f;
    public int score = 10;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

   

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

        gm = FindFirstObjectByType<ActualGM>();
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = animationSprites[animationFrame];
    }

    // Update is called once per frame
  
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            gm.OnInvaderKilled(this);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bunker"))
        {
            gm.OnInvaderKilledNon(this);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            gm.OnBoundaryReached();
        }
    }
}
