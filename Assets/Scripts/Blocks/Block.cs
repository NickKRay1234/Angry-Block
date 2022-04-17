using UnityEngine;

public class Block : MonoBehaviour
{
    private int count;
    private const int _borderY = 7;

    private void Update()
    {
        BlockDrop(_borderY);
    }

    private void BlockDrop(int borderY)
    {
        if (transform.position.y <= borderY) 
            Destroy(gameObject);
    }

    public void SetStartingCount(int count)
    {
        this.count = count;
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.name == "Ball" && count > 0)
        {
            count--;
            if(count == 0)
                Destroy(gameObject);
        }
    }
}
