using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScript", menuName = "ScriptableObjects/ItemScript", order = 1)]
public class ItemScript : ScriptableObject
{
    public Key[] keysInfo;
    public Money[] moneyInfo;
    public TreasureChest[] treasureChestsInfo;
}
