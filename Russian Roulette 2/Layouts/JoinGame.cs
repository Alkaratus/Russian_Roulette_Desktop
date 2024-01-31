using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Russian_Roulette{
    public partial class Form1{
        ListenerResponsivePanel createJoinGame(){

            var panel = new ListenerResponsivePanel(){
                Location = new Point(0, 0),
                Size = new Size(250, 250)
            };

            Label user = new Label(){
                Location = new Point(10, 10),
                Size = new Size(80, 30),
                Text = "Twój nick",
                ForeColor = Color.White,
            };

            TextBox userName = new TextBox(){
                Location = new Point(100, 10),
                Size = new Size(150, 20)
            };

            Label game = new Label(){
                Location = new Point(10, 50),
                Size = new Size(80, 30),
                Text = "Nazwa gry",
                ForeColor = Color.White,
            };

            TextBox gameName = new TextBox(){
                Location = new Point(100, 50),
                Size = new Size(150, 20)
            };

            Label joinGame = new Label() {
                Location = new Point(10, 90),
                Size = new Size(150, 30),
                Text = "Dołącz do gry",
                ForeColor = Color.White,
            };

            Label back = new Label(){
                Location = new Point(10, 130),
                Size = new Size(80, 30),
                Text = "Powrót",
                ForeColor = Color.White,
            };

            Label error = new Label(){
                Location = new Point(10, 210),
                Size = new Size(230, 30),
                ForeColor = Color.White,
            };

            panel.Controls.Add(user);
            panel.Controls.Add(userName);
            panel.Controls.Add(game);
            panel.Controls.Add(gameName);

            joinGame.Click += new EventHandler(delegate (object sender, EventArgs e) {
                var response = this.sender.join_game(new Server_New_Game_Data { PlayerName = userName.Text, GameName = gameName.Text, PlayerBonusIp=publicIP, PlayerListenerPort=listenerPort });
                if (response.Success){
                    gameID = response.GameId;
                    inGameID = response.PlayerId;
                    Controls.Clear();
                    currentPanel = create_pre_game_online_panel();
                    Controls.Add(currentPanel);
                }
                else{
                    error.Text = "Gra o podanej nazwie nie istnieje";
                }
            });
            panel.Controls.Add(joinGame);

            back.Click += new EventHandler(delegate (object sender, EventArgs e) {
                Controls.Clear();
                currentPanel = createGameMenu();
                Controls.Add(currentPanel);
            });
            panel.Controls.Add(back);

            panel.Controls.Add(error);

            return panel;
        }
    }
}
