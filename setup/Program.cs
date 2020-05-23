﻿using System;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace setup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Install NTer";
            Console.OutputEncoding = Encoding.UTF8;
            if(!File.Exists("NTer.exe"))
            {
                Console.WriteLine("(!) Không tìm thấy file chương trình");
                FailureMsg();
                return;
            }
            string fiw = $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}\\nter.exe";
            Console.WriteLine("> Cài đặt khởi chạy bằng run");
            try
            {
                File.Copy("nter.exe", fiw, true);
            }
            catch
            {
                ErrorMsg();
                return;
            }
            Console.WriteLine("> Cài đặt khởi động cùng Windows");
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                regKey.SetValue("nter", fiw, RegistryValueKind.String);
            }
            catch
            {
                ErrorMsg();
                return;
            }
            //Process.Start($"REG ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\" /v nter /t REG_SZ /d fiw");
            Console.WriteLine("Cài đặt thành công");
            Console.ReadKey();
            return;
        }

        private static void ErrorMsg()
        {
            Console.WriteLine("(!) Chương trình cần chạy dưới quyền administrator");
            FailureMsg();
        }

        private static void FailureMsg()
        {
            Console.WriteLine("\nCài đặt không thành công\nNhấn phím bất kì để thoát");
            Console.ReadKey();
        }
    }
}
