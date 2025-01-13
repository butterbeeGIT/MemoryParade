using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.MapGeneration
{
    public class Node
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Node LeftChild { get; private set; }
        public Node RightChild { get; private set; }
        public Room Room { get; private set; }

        private static Random random = new Random();

        public Node(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Split()
        {
            if (LeftChild != null || RightChild != null)
                return false; // Already split

            bool splitHorizontally = random.Next(2) == 0;
            if (Width > Height && Width / Height >= 1.25) splitHorizontally = false;
            else if (Height > Width && Height / Width >= 1.25) splitHorizontally = true;

            int max = (splitHorizontally ? Height : Width) - minSize; // Minimum room size is 6
            if (max <= minSize) return false; // Too small to split

            int split = random.Next(3, max);

            if (splitHorizontally)
            {
                LeftChild = new Node(X, Y, Width, split);
                RightChild = new Node(X, Y + split, Width, Height - split);
            }
            else
            {
                LeftChild = new Node(X, Y, split, Height);
                RightChild = new Node(X + split, Y, Width - split, Height);
            }

            return true;
        }

        private int minSize = 10;

        public void CreateRoom()
        {
            if (LeftChild != null || RightChild != null)
            {
                if (LeftChild != null) LeftChild.CreateRoom(); // Рекурсивно создаём комнаты в дочерних узлах.
                if (RightChild != null) RightChild.CreateRoom();
            }
            else
            {
                // Проверяем, достаточно ли места для создания комнаты.
                if (Width <= minSize || Height <= minSize)
                {
                    Room = null; // Слишком маленький узел для комнаты.
                    return;
                }

                // Генерируем размеры комнаты, чтобы они были хотя бы на 1 блок меньше, чем узел.
                int roomWidth = random.Next(minSize, Math.Max(minSize + 1, Width - 1));
                int roomHeight = random.Next(minSize, Math.Max(minSize + 1, Height - 1));

                // Генерируем случайное положение комнаты внутри узла.
                int roomX = random.Next(X + 1, X + Width - roomWidth);
                int roomY = random.Next(Y + 1, Y + Height - roomHeight);

                Room = new Room(roomX, roomY, roomWidth, roomHeight); // Создаём комнату.
            }
        }


        public List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();
            if (Room != null) rooms.Add(Room);
            if (LeftChild != null) rooms.AddRange(LeftChild.GetRooms());
            if (RightChild != null) rooms.AddRange(RightChild.GetRooms());
            return rooms;
        }
    }
}
