using System;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CreatureScript", menuName = "ScriptableObjects/CreatureScript", order = 2)]
public class CreatureScript : ScriptableObject
{
    public NPC[] NPCs;
    public PlayerSettings playerSettings;
}
