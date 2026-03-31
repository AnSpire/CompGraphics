using System;
using System.Windows.Forms;

namespace pr2
{
    /// <summary>
    /// Точка входа в приложение.
    /// [STAThread] обязателен для корректной работы OpenGL и WinForms.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Стандартная инициализация WinForms приложения
            ApplicationConfiguration.Initialize();
            
            // Запуск главной формы
            Application.Run(new Form1());
        }
    }
}
