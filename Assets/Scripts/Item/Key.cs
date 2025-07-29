using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Key : ItemBase
{
    public GameObject KeyPrefab; // The prefab associated with this key

    public int KeyID { get; set; } // Unique identifier for the key

    public int RoomID { get; set; } // The ID of the room this key is associated with

    public Key(int keyID)
    {
        KeyID = keyID;
    }
}
