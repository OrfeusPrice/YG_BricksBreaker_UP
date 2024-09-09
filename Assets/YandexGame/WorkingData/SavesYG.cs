
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];
        
        public int _wins = 0;
        public int _money = 0;
        public int _damage = 1;
        public int _count_of_balls = 1;
        public int _getXP = 1;
        public int _crit = 50;
        public int _crit_chance = 2;
        public int _oneshot_chance = 0;
    
        public int _price_of_damage = 50;
        public int _price_of_count_of_balls = 150;
        public int _price_of_exp_get = 200;
        public int _price_of_crit = 250;
        public int _price_of_crit_chance = 500;
        public int _price_of_oneshot_chance = 1000;

        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
