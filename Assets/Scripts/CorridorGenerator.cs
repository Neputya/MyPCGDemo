using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorridorGenerator
{
    private GameObject corridorPrefab; // Prefab for the corridor
    private List<Corridor> corridorList = new(); // List to store generated corridors

    public List<Corridor> GenerateCorridors(List<Room> roomList, GameObject corridorPrefab)
    {
        this.corridorPrefab = corridorPrefab;
        // 1. 自动连接房间
        ConnectRooms(roomList);
        return corridorList;
    }

    // 使用最小生成树算法自动连接房间
    private void ConnectRooms(List<Room> roomList)
    {
        // 1. 收集所有房间中心点
        List<Vector3> roomCenters = new();
        foreach (var room in roomList)
        {
            if (room != null)
                roomCenters.Add(room.position);
        }

        // 2. 构建最小生成树（Kruskal算法简化版）
        List<(int, int, float)> edges = new();
        int n = roomCenters.Count;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                float dist = Vector3.Distance(roomCenters[i], roomCenters[j]);
                edges.Add((i, j, dist));
            }
        }
        edges.Sort((a, b) => a.Item3.CompareTo(b.Item3));

        int[] parent = new int[n];
        for (int i = 0; i < n; i++)
            parent[i] = i;
        int Find(int x) { return parent[x] == x ? x : parent[x] = Find(parent[x]); }
        void Union(int x, int y) { parent[Find(x)] = Find(y); }

        int connected = 0;
        foreach (var (i, j, _) in edges)
        {
            if (Find(i) != Find(j))
            {
                CreateCorridor(roomList[i], roomList[j]);
                Union(i, j);
                connected++;
                if (connected >= n - 1) break;
            }
        }
    }

    private void CreateCorridor(Room roomA, Room roomB)
    {
        Vector3 start = roomA.position;
        Vector3 end = roomB.position;
        Vector3 point1, point2;

        float corridorWidth = Mathf.Min(roomA.width, roomB.width) / 4f;
        float tileSize = 1f; // 单元格边长

        if (Mathf.Abs(start.z - end.z) > Mathf.Abs(start.x - end.x))
        {
            point1 = new Vector3(start.x, start.y, (start.z + end.z) / 2);
            point2 = new Vector3(end.x, end.y, (start.z + end.z) / 2);
        }
        else
        {
            point1 = new Vector3((start.x + end.x) / 2, start.y, start.z);
            point2 = new Vector3((start.x + end.x) / 2, end.y, end.z);
        }

        GenerateCorridorCells(start, point1, corridorWidth, tileSize);
        GenerateCorridorCells(point1, point2, corridorWidth, tileSize);
        GenerateCorridorCells(point2, end, corridorWidth, tileSize);

        Corridor corridor = new()
        {
            roomA = roomA,
            roomB = roomB,
            length = Vector3.Distance(start, point1) + Vector3.Distance(point1, point2) + Vector3.Distance(point2, end),
        };
        corridorList.Add(corridor);
    }

    // 走廊单元格生成方法
    private void GenerateCorridorCells(Vector3 from, Vector3 to, float corridorWidth, float tileSize)
    {
        Vector3 direction = (to - from).normalized;
        float distance = Vector3.Distance(from, to);
        int steps = Mathf.CeilToInt(distance / tileSize);

        for (int i = 0; i <= steps; i++)
        {
            Vector3 cellPos = from + direction * (i * tileSize);
            GameObject cell = Object.Instantiate(corridorPrefab, cellPos, Quaternion.identity);
            cell.transform.localScale = new Vector3(corridorWidth / 10f, 1f, tileSize / 10f);
            cell.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
