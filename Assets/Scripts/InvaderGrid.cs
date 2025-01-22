using UnityEngine;
using UnityEngine.Audio;

public class InvaderGrid : MonoBehaviour
{
    public ActualGM gm;

    [Header("Invaders")]
    public GameObject[] prefabs;
    public AnimationCurve speed = new AnimationCurve();
    private Vector3 _direction = Vector3.right;
    private Vector3 initialPosition;
    private int initialInvaders;


    [Header("Grid Settings")]
    public int rows = 5; // default 5
    public int columns = 11; // default 11
    public float rowPadding;
    public float columnPadding;


    [Header("Missiles")]
    public float missileAttackFreq;
    public Projectiles missilePrefab;

   

    private float percentKilled;

    private void Awake()
    {
        initialPosition = transform.position;
        CreateGrid();
    }


    void Start()
    {
        InvokeRepeating(nameof(MissileAttack), Random.Range(1, 4), this.missileAttackFreq);

        gm = FindFirstObjectByType<ActualGM>();
        ActiveInvaders();
    }

    void Update()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);


        percentKilled = (initialInvaders - gm.activeInvaders) / (float) initialInvaders;

        float speed = this.speed.Evaluate(percentKilled);
        transform.localPosition += _direction * speed * Time.deltaTime;

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.7f)) {
                AdvanceRow();
                break;
            }

            if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.7f)) {
                AdvanceRow();
                break;
            }
        }
    }

    void CreateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            Vector3 center = new Vector3(-(columnPadding * (columns - 1)) / 2, -(rowPadding * (rows - 1)) / 2, 0);
            Vector3 rowPosition = new Vector3(center.x, center.y + row * rowPadding, 0);
            for (int col = 0; col < columns; col++)
            {
                GameObject invader = Instantiate(prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * columnPadding;
                invader.transform.localPosition = position;
            }
        }

        initialInvaders = rows * columns;
    }

    void MissileAttack()
    {
        if (gm.activeInvaders == 0) { return; }

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) { continue; }

            if (Random.value < percentKilled) {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    public void ResetInvaders()
    {
        _direction = Vector3.right;
        transform.position = initialPosition;

        
        foreach (Transform invader in transform) {
            invader.gameObject.SetActive(true);
        }
        ActiveInvaders();
    }
    

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= rowPadding;
        this.transform.position = position;
    }

    private void ActiveInvaders()
    {
        int count = 0;

        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
            {
                count++;
            }
        }

        gm.StartActiveInvaders(count);
    }
}
