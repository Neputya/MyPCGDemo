using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab; // 房间预设体
    public GameObject corridorPrefab; // 走廊预设体
    public GameObject wallPrefab; // 墙壁预设体
    public int boundsWidth = 50; // 区域宽度
    public int boundsHeight = 50; // 区域高度
    public List<Room> roomList = new(); // 存储所有生成的房间
    public List<Corridor> corridorList = new();
    public int minRoomWidth = 5; // 最小房间大小
    public int minRoomHeight = 5; // 最小房间大小
    public int minSpaceWidth = 15; // 最小空间宽度
    public int minSpaceHeight = 15; // 最小空间高度
    public int minRoomNumber = 10; // 最小房间数量

    private QuadtreeNode rootNode; // 根节点
    private RoomGenerator roomGenerator; // 房间生成器实例
    private CorridorGenerator corridorGenerator; // 走廊生成器实例
    private QuadtreeVisualizer quadtreeVisualizer; // 四叉树可视化用于方便观察效果，后续删除！

    private void Awake()
    {
        roomGenerator = new RoomGenerator();
        corridorGenerator = new CorridorGenerator();
        quadtreeVisualizer = GetComponent<QuadtreeVisualizer>();  //后续删除！
    }

    private void Start()
    {
        Rect initialBounds = new(0, 0, boundsWidth, boundsHeight); // 初始区域边界
        rootNode = new QuadtreeNode(initialBounds);
        roomList = roomGenerator.GenerateRooms(rootNode, roomPrefab, wallPrefab, minRoomWidth, minRoomHeight,
                                                minRoomNumber, minSpaceWidth, minSpaceHeight);
        corridorList = corridorGenerator.GenerateCorridors(roomList, corridorPrefab);
        quadtreeVisualizer.drawGizmos = true; // 启用四叉树可视化，后续删除！
    }

    public QuadtreeNode GetRootNode()
    {
        return rootNode; // 返回根节点
    }
}
