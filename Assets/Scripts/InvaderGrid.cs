using UnityEngine;

public class InvaderGrid : MonoBehaviour
{
    public ActualGM gm;
    public GameObject[] prefabs;
    
    public int rows = 5; // default 5
    public int columns = 11; // default 11

    public float rowPadding;
    public float columnPadding;

    public float speed;

    private Vector3 _direction = Vector3.right;
    private int initialInvaders;

    private void Awake()
    {
        for (int row  = 0; row < rows; row++)
        {
            Vector3 center = new Vector3(-(columnPadding * (columns-1))/2, -(rowPadding * (rows-1))/2, 0);
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


    void Start()
    {
        gm = FindFirstObjectByType<ActualGM>();
        ActiveInvaders();
    }

    void Update()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        this.transform.localPosition += _direction * speed * Time.deltaTime;

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.7f))
            {
                AdvanceRow();
            }
            if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.7f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= rowPadding;
        this.transform.position = position;

        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        speed = 0.5f + 0.1f * (initialInvaders - gm.activeInvaders);
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
