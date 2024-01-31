using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Russian_Roulette.InGameServer;
using static System.Windows.Forms.AxHost;

namespace Russian_Roulette{
    public enum playerState{
        normal,chosing,answering,enable
    }
    public partial class PlayerPanel : Panel{
        readonly static Dictionary<playerState, Color> stateColors = new Dictionary<playerState, Color>(){
            {playerState.normal,SystemColors.ActiveCaptionText},
            {playerState.enable,Color.Yellow },
            {playerState.chosing,Color.Blue },
            {playerState.answering,Color.Red }
        };

        public readonly static int HEIGHT = 25;

        public delegate void ClickFunction(uint id);
        
        public Label name, money;
        bool enable;
       
        public PlayerPanel(uint id,Point location, string player_name, ClickFunction clickFunction){
            Location = location;
            Size = new Size(200, HEIGHT);
            Margin = new Padding(0);
            Padding = new Padding(0);
            enable = false;

            name = new Label(){
                Location = new Point(0, 0),
                Size = new Size(130, HEIGHT),
                Margin = new Padding(0),
                Text = player_name,
                ForeColor = Color.White
            };

            money = new Label(){
                Location = new Point(130, 0),
                Size = new Size(70, HEIGHT),
                Margin = new Padding(0),
                Text = "0",
                ForeColor = Color.White
            };

            Controls.Add(name);
            Controls.Add(money);

            InitializeComponent();

            name.MouseHover += delegate (object? sender, EventArgs e){
                spotlightPlayer();
            };

            name.MouseLeave += delegate (object? sender, EventArgs e){
                delightPlayer();
            };

            name.Click += delegate (object? sender, EventArgs e){
                if (enable){
                    clickFunction(id);
                }
            };

            money.MouseHover += delegate (object? sender, EventArgs e) {
                spotlightPlayer();
            };

            money.MouseLeave += delegate (object? sender, EventArgs e) {
                delightPlayer();
            };

            money.Click += delegate (object? sender, EventArgs e) {
                if (enable)
                {
                    clickFunction(id);
                }
            };
        }

        public void setMoney(uint value){
            money.Text= value.ToString();
        }

        public void setStateColor(playerState state){
            BackColor = stateColors[state];
            if (state == playerState.enable){
                enable = true; 
            }
            if(state== playerState.normal){
                enable = false;
            }

        }

        public void spotlightPlayer(){
            if (enable){
                BackColor = Color.Orange;
            }
        }

        public void delightPlayer(){
            if (enable){
                BackColor = stateColors[playerState.enable];
            }
        }

        public void clearPlayer(){
            name.Text = "";
            money.Text = "";
            BackColor = stateColors[playerState.normal];
        }

    }
}

