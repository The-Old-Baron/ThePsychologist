using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public Item item;
    public float TimeToDestroy = 0f;

    private void Start()
    {
        TimeToDestroy = 10f;
    }
    private void Update()
    {
        if (TimeToDestroy > 0)
        {
            TimeToDestroy -= Time.deltaTime;
            if (TimeToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    

}