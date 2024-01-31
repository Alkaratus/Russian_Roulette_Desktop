using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Russian_Roulette{
    partial class Form1{
        ListenerResponsivePanel createGameMenu(){
            var panel = new ListenerResponsivePanel(){
                Location = new Point(0, 0),
                Size = new Size(250, 250),
            };
            Label joinGame = new Label()
            {
                Text = "Dołącz do gry",
                Location = new Point(10, 10),
                Size= new Size(150,30),
                TabIndex = 0,
                ForeColor = Color.White,
            };
            Label createGame = new Label() {
                Text = "Stwórz grę",
                Location = new Point(10, 50),
                Size = new Size(150, 30),
                TabIndex = 1,
                ForeColor = Color.White,
            };
            Label returnToMenu = new Label(){
                Text = "Powrót do menu",
                Location = new Point(10, 90),
                Size = new Size(150, 30),
                TabIndex = 2,
                ForeColor = Color.White
            };
            Label content = new Label(){
                Location = new Point(10, 210),
                TabIndex = 3,
                ForeColor = Color.White,
            };

            void mouseOut(object sender, EventArgs e){
                content.Text = "";
            }

            joinGame.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
                content.Text = "Dołącz do istniejącej gry online";
            });
            joinGame.MouseLeave += new EventHandler(mouseOut);
            joinGame.Click += new EventHandler(delegate (object sender, EventArgs e) {
                Controls.Clear();
                currentPanel = createJoinGame();
                Controls.Add(currentPanel);
            });
            panel.Controls.Add(joinGame);

            createGame.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
                content.Text = "Utwórz nową grę online";
            });
            createGame.MouseLeave += new EventHandler(mouseOut);
            createGame.Click += new EventHandler(delegate (object sender, EventArgs e){
                Controls.Clear();
                currentPanel = createCreateGame();
                Controls.Add(currentPanel);
            });
            panel.Controls.Add(createGame);

            returnToMenu.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
                content.Text = "Powrót do menu głównego";
            });
            returnToMenu.MouseLeave += new EventHandler(mouseOut);
            returnToMenu.Click+= new EventHandler(delegate (object sender, EventArgs e) {
                Controls.Clear();
                currentPanel = create_main_panel();
                Controls.Add(currentPanel);
            });
            panel.Controls.Add(returnToMenu);

            panel.Controls.Add(content);

            return panel;
        }

        
    }
}
