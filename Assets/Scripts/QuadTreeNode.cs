using System.Collections.Generic;
using UnityEngine;

// 四叉树节点类
public class QuadtreeNode
{
    public Rect bounds; // 当前区域的边界
    public List<QuadtreeNode> subSpace;
    public Room room; // 当前空间内的房间

    // 构造函数
    public QuadtreeNode(Rect bounds)
    {
        this.bounds = bounds;
        subSpace = new List<QuadtreeNode>();
    }

    // 分割当前区域为四个子区域
    public void Subdivide()
    {
        float midX = bounds.x + bounds.width / 2;
        float midY = bounds.y + bounds.height / 2;

        // 创建四个子区域
        subSpace.Add(new QuadtreeNode(new Rect(bounds.x, bounds.y, bounds.width / 2, bounds.height / 2))); // Top-left
        subSpace.Add(new QuadtreeNode(new Rect(midX, bounds.y, bounds.width / 2, bounds.height / 2))); // Top-right
        subSpace.Add(new QuadtreeNode(new Rect(bounds.x, midY, bounds.width / 2, bounds.height / 2))); // Bottom-left
        subSpace.Add(new QuadtreeNode(new Rect(midX, midY, bounds.width / 2, bounds.height / 2))); // Bottom-right
    }

    public float Size()
    {
        return bounds.width * bounds.height; // 返回当前区域的大小
    }

    public void GetAllNodes(List<QuadtreeNode> nodes)
    {
        nodes.Add(this);
        foreach (var child in subSpace)
        {
            child.GetAllNodes(nodes);
        }
    }
}
