using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    enum MobState
    {
        Friendly,
        Netural,
        Aggressive
    }
    class Mob
    {
        protected string name;
        private int HP { get; set; }
        private float mana;
        protected int damage;
        public MobState state;
        public double speed;

        public Mob(string name, int hp, float mana, int damage, double speed, MobState state)
        {
            this.name = name;
            this.HP = hp;
            this.mana = mana;
            this.damage = damage;
            this.speed = speed;
            this.state = state;
        }

        public void usePotion(double newSpeed)
        {
            speed = newSpeed;
            Console.WriteLine($"The mob used the potion of speed. Speed: {newSpeed}");
        }

        public void usePotion(int restoreHP)
        {
            HP += restoreHP;
            Console.WriteLine($"The mob used the potion of health. Health: {HP}");
        }

        public void DecreaseHP(int amount)
        {
            HP -= amount;
            Console.WriteLine($"Hit by {amount}. Hp is: {HP}");
        }
        public override string ToString()
        {
            return $"{name}\n\t-- Health: {HP} --\n";
        }
    }
}
