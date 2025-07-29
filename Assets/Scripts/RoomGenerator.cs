using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    private QuadtreeNode rootNode; // 四叉树根节点
    private GameObject roomPrefab; // 房间预制体
    private GameObject wallPrefab; // 墙壁预制体
    private int minRoomWidth = 5; // 最小房间宽度
    private int minRoomHeight = 5;
    private int minRoomNumber = 10; // 最小房间数量
    private int minSpaceWidth; // 最小空间宽度
    private int minSpaceHeight;
    private readonly List<Room> roomList = new(); // 存储所有生成的房间

    public List<Room> GenerateRooms(QuadtreeNode rootNode, GameObject roomPrefab, GameObject wallPrefab,
        int minRoomWidth = 5, int minRoomHeight = 5, int minRoomNumber = 10,
        int minSpaceWidth = 15, int minSpaceHeight = 15)
    {
        this.roomPrefab = roomPrefab;
        this.wallPrefab = wallPrefab;
        this.minRoomWidth = minRoomWidth;
        this.minRoomHeight = minRoomHeight;
        this.minRoomNumber = minRoomNumber;
        this.rootNode = rootNode;
        this.minSpaceWidth = minSpaceWidth;
        this.minSpaceHeight = minSpaceHeight;
        GenerateQuadtreeSpace(this.rootNode);
        return roomList; // 返回生成的房间列表
    }

    // 递归生成四叉树并在每个叶子节点生成房间
    private void GenerateQuadtreeSpace(QuadtreeNode space)
    {
        if (space.bounds.width < 2 * minSpaceWidth || space.bounds.height < 2 * minSpaceHeight)
        {
            // 在给定的空间中生成房间
            GenerateRoom(space);
            return;
        }
        // 如何一定程度上控制随机的情况
        if (Random.value >= 1 - space.Size() * RoomCountFunc() / rootNode.Size())
        {
            space.Subdivide();
        }
        else
        {
            GenerateRoom(space);
            return;
        }

        // 递归处理子空间
        foreach (var subSpace in space.subSpace)
        {
            GenerateQuadtreeSpace(subSpace);
        }
    }

    private int RoomCountFunc()
    {
        return minRoomNumber / (roomList.Count + 1) > 4 ? minRoomNumber / (roomList.Count + 1) : 4;
    }


    // 在给定节点的区域内生成一个房间
    private void GenerateRoom(QuadtreeNode space)
    {
        Room room = new(roomPrefab, wallPrefab, space, minRoomWidth, minRoomHeight);
        space.room = room;
        roomList.Add(room); // 将生成了房间的空间添加到列表
    }
}
