using System.Collections.Generic;
using UnityEngine;

public class QuadtreeVisualizer : MonoBehaviour
{
    private QuadtreeNode rootNode;
    private List<QuadtreeNode> nodes = new();

    [HideInInspector]
    public bool drawGizmos = false;
    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;
        rootNode = GetComponent<DungeonGenerator>().GetRootNode();
        if (rootNode == null) return;

        Gizmos.color = Color.green;

        GetAllNodes(rootNode);

        foreach (var node in nodes)
        {
            DrawRectAsLines(node.bounds);
        }
        // Debug.Log("DrawHappened!!");
        //drawGizmos = false; // 只绘制一次
    }

    private void GetAllNodes(QuadtreeNode node)
    {
        nodes.Add(node);
        foreach (var child in node.subSpace)
        {
            GetAllNodes(child);
        }
    }

    private void DrawRectAsLines(Rect rect)
    {
        Vector3 p1 = new(rect.x, 0, rect.y);
        Vector3 p2 = new(rect.x + rect.width, 0, rect.y);
        Vector3 p3 = new(rect.x + rect.width, 0, rect.y + rect.height);
        Vector3 p4 = new(rect.x, 0, rect.y + rect.height);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}