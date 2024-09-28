using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    // Public fields
    public Item item; // Referência ao item que foi dropado
    public float TimeToDestroy = 10f; // Tempo em segundos para destruir o item

    // Métodos Unity
    private void Update()
    {
        // Verifica se o tempo para destruir é maior que zero
        if (TimeToDestroy > 0)
        {
            // Decrementa o tempo com base no tempo decorrido desde o último frame
            TimeToDestroy -= Time.deltaTime;

            // Se o tempo para destruir for menor ou igual a zero, destrói o objeto
            if (TimeToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}