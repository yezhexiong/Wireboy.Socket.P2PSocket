﻿using System;
using System.Reflection;
using System.IO;
using System.Threading;
using System.ServiceProcess;

namespace P2PSocket.StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = false;
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}P2PSocket/P2PSocket.Server.dll"))
            {
                Assembly assembly = Assembly.LoadFrom($"{AppDomain.CurrentDomain.BaseDirectory}P2PSocket/P2PSocket.Server.dll");
                AppDomain serverDomain = AppDomain.CreateDomain("Server");
                assembly = serverDomain.Load(assembly.FullName);
                object obj = assembly.CreateInstance("P2PSocket.Server.CoreModule");
                MethodInfo method = obj.GetType().GetMethod("Start");
                method.Invoke(obj, null);
                flag = true;
            }
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}P2PSocket/P2PSocket.Client.dll"))
            {
                Assembly assembly = Assembly.LoadFrom($"{AppDomain.CurrentDomain.BaseDirectory}P2PSocket/P2PSocket.Client.dll");
                AppDomain clientDomain = AppDomain.CreateDomain("Server");
                assembly = clientDomain.Load(assembly.FullName);

                object obj = assembly.CreateInstance("P2PSocket.Client.CoreModule");
                MethodInfo method = obj.GetType().GetMethod("Start");
                method.Invoke(obj, null);
                flag = true;
            }
            if (flag)
            {
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
            else
            {
                Console.WriteLine($"在目录{AppDomain.CurrentDomain.BaseDirectory}P2PSocket中，未找到P2PSocket.Client.dll和P2PSocket.Server.dll.");
                Thread.Sleep(10000);
            }
        }
    }
}
