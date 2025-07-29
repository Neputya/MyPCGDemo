using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class NPC : CreatureBase
{
    public string Dialogue; // NPC对话内容
    public CapsuleCollider CapsuleCollider; // NPC胶囊碰撞器
}
