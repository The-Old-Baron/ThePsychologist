using System.Collections.Generic;
using UnityEngine;

// Classe que representa uma entrada no bestiário
public class BestiaryEntry
{
    public Enemy enemy; // Referência ao inimigo
    public int count;   // Contador de quantas vezes o inimigo foi adicionado
}

public class Bestiary : MonoBehaviour
{
    // Instância singleton do Bestiary
    public static Bestiary Instance;

    // Lista de criaturas no bestiário
    public List<BestiaryEntry> creatures;

    private void Start()
    {
        // Inicializa a instância singleton
        Instance = this;
    }

    // Método para adicionar uma criatura ao bestiário
    public void AddCreature(Enemy creature)
    {
        // Inicializa a lista de criaturas se estiver nula
        if (creatures == null)
        {
            creatures = new List<BestiaryEntry>();
        }

        // Procura a entrada da criatura na lista
        BestiaryEntry entry = creatures.Find(x => x.enemy == creature);

        // Se a entrada não existir, cria uma nova
        if (entry == null)
        {
            entry = new BestiaryEntry
            {
                enemy = creature,
                count = 1
            };
            creatures.Add(entry);
        }
        else
        {
            // Se a entrada já existir, incrementa o contador
            entry.count++;
        }
    }
}