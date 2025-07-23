using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Room
{
    public string roomName; // 房间名称
    public Vector3 position; // 房间位置
    public float width; // 房间宽度
    public float height; // 房间高度
    public Vector3 size; // 房间大小

    public bool isBossRoom = false; // 是否是Boss房间
    public bool isEliteRoom = false; // 是否是精英房间
    public bool isTreasureRoom = false; // 是否是宝藏房间
    public bool isShopRoom = false; // 是否是商店房间

    public BoxCollider boxCollider;

    private readonly int minRoomWidth;
    private readonly int minRoomHeight;
    private readonly int minSpaceWidth;
    private readonly int minSpaceHeight;

    public Room(GameObject prefab, GameObject wallPrefab, QuadtreeNode space, int minRoomWidth,
                int minRoomHeight, int minSpaceWidth, int minSpaceHeight)
    {
        this.minRoomWidth = minRoomWidth;
        this.minRoomHeight = minRoomHeight;
        this.minSpaceWidth = minSpaceWidth;
        this.minSpaceHeight = minSpaceHeight;
        Initialize(prefab, wallPrefab, space.bounds.x, space.bounds.y, space.bounds.width, space.bounds.height);
    }

    private void Initialize(GameObject tilePrefab, GameObject wallPrefab, float boundsX, float boundsY, float spaceWidth, float spaceHeight)
    {
        boxCollider = new BoxCollider();
        width = Random.Range(minRoomWidth, spaceWidth / 2);
        height = Random.Range(minRoomHeight, spaceHeight / 2);
        size = new Vector3(width, 1f, height);

        position = new Vector3(
            boundsX + Random.Range(width / 2, spaceWidth - width / 2),
            0f,
            boundsY + Random.Range(height / 2, spaceHeight - height / 2)
        );

        float tileSize = 1f; // 每个格子的边长
        int tilesX = Mathf.RoundToInt(width / tileSize);
        int tilesZ = Mathf.RoundToInt(height / tileSize);

        Vector3 bottomLeft = new Vector3(
            position.x - width / 2 + tileSize / 2,
            0f,
            position.z - height / 2 + tileSize / 2
        );

        for (int x = 0; x < tilesX; x++)
        {
            for (int z = 0; z < tilesZ; z++)
            {
                Vector3 tilePos = bottomLeft + new Vector3(x * tileSize, 0f, z * tileSize);
                GameObject tile = Object.Instantiate(tilePrefab, tilePos, Quaternion.identity);
                tile.transform.localScale = new Vector3(tileSize / 10f, 1f, tileSize / 10f);
            }
        }

        BuildWalls(wallPrefab);
    }

    private void BuildWalls(GameObject wallPrefab, float wallHeight = 5f, float wallThickness = 0.2f)
    {
        float cellSize = 1f; // 单元格宽度
        int cellsX = Mathf.CeilToInt(width / cellSize);
        int cellsZ = Mathf.CeilToInt(height / cellSize);
        float meshHeight = wallPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.y; // 对于Cube等竖直墙体

        // 上墙（+Z方向）
        for (int i = 0; i < cellsX; i++)
        {
            float x = position.x - width / 2 + cellSize / 2 + i * cellSize;
            float z = position.z + height / 2;
            Vector3 wallPos = new(x, meshHeight / 2, z);
            GameObject wall = Object.Instantiate(wallPrefab, wallPos, wallPrefab.transform.rotation);
            // wall.transform.localScale = new Vector3(cellSize, wallHeight, wallThickness);
        }

        // 下墙（-Z方向）
        for (int i = 0; i < cellsX; i++)
        {
            float x = position.x - width / 2 + cellSize / 2 + i * cellSize;
            float z = position.z - height / 2;
            Vector3 wallPos = new(x, meshHeight / 2, z);
            GameObject wall = Object.Instantiate(wallPrefab, wallPos, wallPrefab.transform.rotation);
            // wall.transform.localScale = new Vector3(cellSize, wallHeight, wallThickness);
        }

        Vector3 adjustEulerAngles = wallPrefab.transform.rotation.eulerAngles + new Vector3(0, 90, 0);
        // 左墙（-X方向）
        for (int i = 0; i < cellsZ; i++)
        {
            float x = position.x - width / 2;
            float z = position.z - height / 2 + cellSize / 2 + i * cellSize;
            Vector3 wallPos = new(x, meshHeight / 2, z);
            GameObject wall = Object.Instantiate(wallPrefab, wallPos, Quaternion.Euler(adjustEulerAngles));
            // wall.transform.localScale = new Vector3(wallThickness, wallHeight, cellSize);
        }

        // 右墙（+X方向）
        for (int i = 0; i < cellsZ; i++)
        {
            float x = position.x + width / 2;
            float z = position.z - height / 2 + cellSize / 2 + i * cellSize;
            Vector3 wallPos = new(x, meshHeight / 2, z);
            GameObject wall = Object.Instantiate(wallPrefab, wallPos, Quaternion.Euler(adjustEulerAngles));
            // wall.transform.localScale = new Vector3(wallThickness, wallHeight, cellSize);
        }
    }
}
