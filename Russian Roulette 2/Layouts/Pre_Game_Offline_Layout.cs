using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Russian_Roulette
{
    partial class Form1{
        ListenerResponsivePanel create_pre_game_offline_panel(){
            var pre_game_panel = new ListenerResponsivePanel(){
                Location = new Point(0, 0),
                Size = new Size(260, 330),
            };

            PreGameOfflinePlayerPanel player1 = new PreGameOfflinePlayerPanel(10, 10, "Gracz 1");
            PreGameOfflinePlayerPanel player2 = new PreGameOfflinePlayerPanel(10, 50, "Gracz 2");
            PreGameOfflinePlayerPanel player3 = new PreGameOfflinePlayerPanel(10, 90, "Gracz 3");
            PreGameOfflinePlayerPanel player4 = new PreGameOfflinePlayerPanel(10, 130, "Gracz 4");
            PreGameOfflinePlayerPanel player5 = new PreGameOfflinePlayerPanel(10, 170, "Gracz 5");
            PreGameOfflinePlayerPanel player6 = new PreGameOfflinePlayerPanel(10, 210, "Gracz 6");

            Label start_game = new Label(){
                Location = new Point(10,250),
                Text= "Rozpocznij grę",
                ForeColor = Color.White,
            };

            Label back_to_main_menu = new Label(){
                Location = new Point(150, 290),
                Text = "Powrót do menu",
                ForeColor = Color.White,
            };

            pre_game_panel.Controls.Add(player1);
            pre_game_panel.Controls.Add(player2);
            pre_game_panel.Controls.Add(player3);
            pre_game_panel.Controls.Add(player4);
            pre_game_panel.Controls.Add(player5);
            pre_game_panel.Controls.Add(player6);
            pre_game_panel.Controls.Add(start_game);
            start_game.Click += delegate (object sender, EventArgs e){
                Controls.Clear();
                var players_names = new string[6]{
                    player1.name_text_box.Text!="" ? player1.name_text_box.Text:"Player 1", 
                    player2.name_text_box.Text!="" ? player2.name_text_box.Text:"Player 2",
                    player3.name_text_box.Text!="" ? player3.name_text_box.Text:"Player 3", 
                    player4.name_text_box.Text!="" ? player4.name_text_box.Text:"Player 4",
                    player5.name_text_box.Text!="" ? player5.name_text_box.Text:"Player 5",
                    player6.name_text_box.Text!="" ? player6.name_text_box.Text:"Player 6",
                };
                Controls.Add(create_game_panel(players_names,0));
            };

            back_to_main_menu.Click+=(delegate (object sender, EventArgs e) {
                Controls.Clear();
                Controls.Add(create_main_panel());
            });
            pre_game_panel.Controls.Add(back_to_main_menu);

            return pre_game_panel;
        }
    }
}
