using UnityEditor;
using UnityEngine;
using Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration;

public class MapGeneratorEditor : EditorWindow
{
    private MapGenerator generator;
    private int roomCount = 5;

    [MenuItem("Tools/Map Generator")]
    public static void ShowWindow()
    {
        GetWindow<MapGeneratorEditor>("Map Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Map Generator Settings", EditorStyles.boldLabel);
        roomCount = EditorGUILayout.IntField("Room Count", roomCount);

        if (GUILayout.Button("Generate Map"))
        {
            GenerateAndDisplayMap();
        }
    }

    private void GenerateAndDisplayMap()
    {
        Room[] requiredRooms = new Room[]
        {
            new Room(0, 0, 4, 4, RoomType.plot),
            new Room(10, 10, 3, 3, RoomType.boss)
        };

        generator = new MapGenerator(roomCount, requiredRooms);
        int[][] map = generator.GetMap();

        Debug.Log("Generated Map:");
        for (int y = 0; y < map.Length; y++)
        {
            string row = "";
            for (int x = 0; x < map[y].Length; x++)
            {
                row += map[y][x] + " ";
            }
            Debug.Log(row);
        }
    }
}
