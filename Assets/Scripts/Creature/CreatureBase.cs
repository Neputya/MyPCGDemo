using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class CreatureBase
{ 
    public GameObject Prefab; // Creature prefab
    public string Name; // Creature name
    public Texture2D Icon; // Creature icon
}
