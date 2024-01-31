using Grpc.Core;
using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Russian_Roulette
{
    partial class Form1{
        ListenerResponsivePanel create_pre_game_online_panel() {

            var pre_game_panel = new ListenerResponsivePanel() {
                Location = new Point(0, 0),
                Size = new Size(260, 330),
            };

            var players = new PreGameOnlinePlayerPanel[6];

            for (int i = 0; i < 6; i++) {
                players[i] = new PreGameOnlinePlayerPanel(10, 40 * i + 10, $"Gracz {i + 1}");
            }

            Label gameStatus = new Label() {
                Location = new Point(10, 250),
                Size = new Size(200,30),
                Text = "Oczekiwanie na graczy",
                ForeColor = Color.White,
            };

            Label back_to_main_menu = new Label() {
                Location = new Point(150, 290),
                Text = "Powrót do menu",
                ForeColor = Color.White,
            };

            pre_game_panel.Controls.Add(gameStatus);

            var response = this.sender.get_players(new Server_Game_ID { Id = gameID });
            players[0].userNameLabel.Text = response.Player1;
            players[1].userNameLabel.Text = response.Player2;
            players[2].userNameLabel.Text = response.Player3;
            players[3].userNameLabel.Text = response.Player4;
            players[4].userNameLabel.Text = response.Player5;
            players[5].userNameLabel.Text = response.Player6;

            foreach (var player in players){
                pre_game_panel.Controls.Add(player);
            }

            back_to_main_menu.Click += delegate (object sender, EventArgs e) {
                Controls.Clear();
                Controls.Add(create_main_panel());
            };
            //pre_game_panel.Controls.Add(back_to_main_menu);

            pre_game_panel.playerNameSetter = delegate (uint playerIndex, string name) {
                players[playerIndex].userNameLabel.Invoke(new Action(delegate (){
                    players[playerIndex].userNameLabel.Text = name;
                }));
                
            };

            pre_game_panel.gamePrepereing = delegate (){
                gameStatus.Invoke(new Action(delegate{
                    gameStatus.Text = "Gra rozpocznie się za 3 sekundy";
                }));
            };

            pre_game_panel.gameStarting = delegate (int port) {
                this.BeginInvoke(new Action(delegate{
                    Controls.Clear();
                    var players_names = new string[6];
                    for(int i = 0; i < players_names.Length; i++){
                        players_names[i] = players[i].userNameLabel.Text;
                    }
                    currentPanel = create_game_panel(players_names,port);
                    Controls.Add(currentPanel);
                }));
            };


            return pre_game_panel;
        }
    }
}
