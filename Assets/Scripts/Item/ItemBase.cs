using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class ItemBase
{
    public string Name;
    public Texture2D Icon;
    public string Description;
    public BoxCollider BoxCollider;
}
public abstract class ItemBase<T> : ItemBase where T : ItemBase, new()
{
    public static T Create()
    {
        return new T();
    }
}