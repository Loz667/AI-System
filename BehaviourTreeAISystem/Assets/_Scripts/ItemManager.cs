using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int itemAmount;

    private void Awake()
    {
        itemAmount = 50;
    }

    public bool Take()
    {
        itemAmount -= 10;
        bool IsDepleted = itemAmount <= 0;
        if (IsDepleted) Wither();
        return IsDepleted;
    }

    private void Wither()
    {
        Destroy(gameObject);
    }
}
