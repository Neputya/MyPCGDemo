using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Money : ItemBase
{
    public GameObject MoneyPrefab; // The prefab associated with this money item
    public int Amount;
    public Money(int amount)
    {
        Amount = amount;
    }
}
