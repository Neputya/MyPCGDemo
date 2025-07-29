using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TreasureChest : ItemBase
{
    public GameObject TreasureChestPrefab;
    public int ChestID { get; set; } // Unique identifier for the chest
    public List<ItemBase> Contents { get; set; } // List of items contained in the chest
   
}
