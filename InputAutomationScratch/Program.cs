﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace aclick
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            while(true)
            {
                //MouseOperations.MouseEventAt(MouseOperations.MouseEventFlags.LeftDown, 1807, 1787);
                //MouseOperations.MouseEventAt(MouseOperations.MouseEventFlags.LeftUp, 1807, 1787);
                //var mouseLocation = MouseOperations.GetCursorPosition();
                //Console.WriteLine($"X: {mouseLocation.X} , Y: {mouseLocation.Y}");
                Thread.Sleep(1000);
            }
            
        }
    }
    

    public class MouseOperations
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);      

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int x, int y) 
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                position.X,
                position.Y,
                0,
                0)
                ;
        }

        public static void MouseEventAt(MouseEventFlags value, int x, int y)
        {
            var position = new MousePoint(x, y);

            mouse_event
                ((int)value,
                position.X,
                position.Y,
                0,
                0)
                ;
        }



        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
