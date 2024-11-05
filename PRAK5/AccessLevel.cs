using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAK5
{
    public static class AccessLevel
    {
        // Статические поля для уровней доступа
        public static int UserLevel { get; } = 1;
        public static int AdminLevel { get; } = 100;

        public static string UserRole { get; } = "Пользователь";
        public static string AdminRole { get; } = "Админ";

        // Вспомогательный класс для элементов ComboBox
        public class AccessLevelItem
        {
            public int Level { get; set; }
            public string Role { get; set; }
        }

        // Метод для определения уровня доступа по значению level
        public static string GetAccessLevel(int level)
        {
            return level == UserLevel ? UserRole : level == AdminLevel ? AdminRole : "Неизвестный уровень";
        }

        // Метод для получения списка уровней доступа для ComboBox
        public static List<AccessLevelItem> GetAccessLevels()
        {
            return new List<AccessLevelItem>
            {
                new AccessLevelItem { Level = UserLevel, Role = UserRole },
                new AccessLevelItem { Level = AdminLevel, Role = AdminRole }
            };
        }
    }

}
