using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float parallaxFloat;
    public GameObject centerPoint;
    public GameObject player;
    public float startPos;
    public float length;

    public float pointDis;
    public float temp;
    public float dist;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x ;
    }

    private void Update()
    {
        pointDis = Vector3.Distance(player.transform.position, centerPoint.transform.position);
        temp = (pointDis * (1 - parallaxFloat));
        dist = (pointDis * parallaxFloat);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}

