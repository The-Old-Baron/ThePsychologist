using System.Collections.Generic;
using UnityEngine;

public class BestiaryEntry{
    public Enemy enemy;
    public int count;
}

public class Bestiary : MonoBehaviour
{
    public static Bestiary Instance;

    private void Start()
    {
        Instance = this;
    }
    public List<BestiaryEntry> creatures;

    public void AddCreature(Enemy creature)
    {
        if(creatures == null)
        {
            creatures = new List<BestiaryEntry>();
        }
        BestiaryEntry entry = creatures.Find(x => x.enemy == creature);
        if(entry == null)
        {
            entry = new BestiaryEntry();
            entry.enemy = creature;
            entry.count = 1;
            creatures.Add(entry);
        }
        else
        {
            entry.count++;
        }
    }
    
}