using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    public class MapGenerator
    {
        const int MAP_WIDTH = 50;
        const int MAP_HEIGHT = 50;

        /// <summary>
        /// Генерирует карту
        /// </summary>
        /// <returns>первая комната карты</returns>
        public static Room GenerateAndRenderMap()
        {
            // Убедиться, что существует родительский объект для карты
            EnsureMapParentExists();

            // Генерация структуры карты
            List<Room> rooms = GenerateRooms();

            // Отрисовка комнат
            foreach (Room room in rooms)
            {
                MapRenderer.AddRoom(room);
            }

            // Соединение комнат коридорами
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                ConnectRooms(rooms[i], rooms[i + 1]);
            }

            //комната спавна героя
            return rooms[0];
        }


        private static void EnsureMapParentExists()
        {
            if (MapRenderer.MapParent == null)
            {
                GameObject mapRoot = new GameObject("MapRoot");
                MapRenderer.MapParent = mapRoot.transform;
            }
        }

        private static List<Room> GenerateRooms()
        {
            Node root = new Node(0, 0, MAP_WIDTH, MAP_HEIGHT);
            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                Node current = nodes.Dequeue();
                if (current.Split())
                {
                    nodes.Enqueue(current.LeftChild);
                    nodes.Enqueue(current.RightChild);
                }
            }

            root.CreateRoom();
            return root.GetRooms();
        }

        private static void ConnectRooms(Room room1, Room room2)
        {
            MapRenderer.AddCorridor(room1, room2);
        }
    }
}