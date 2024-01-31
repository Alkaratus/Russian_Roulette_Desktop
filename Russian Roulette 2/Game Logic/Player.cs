using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Russian_Roulette{
    public class Player{
        public readonly uint id;
        public readonly string name;
        private uint money;
        Player(uint id, string name){
            this.id = id;
            this.name = name;
            money = 0;
        }
        uint get_money() { return money; }
        void add_money(uint bonus_money) { money += bonus_money; }
        void move_money(ref Player other){
            other.money += money;
            money = 0;
        }
        void div_money(ref List<Player> others){
            foreach (var player in others){
                player.money += money/Convert.ToUInt32(others.Count);
            }
        }
    }
}
