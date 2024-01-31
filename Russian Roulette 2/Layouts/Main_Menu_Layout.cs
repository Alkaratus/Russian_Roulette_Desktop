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
        ListenerResponsivePanel create_main_panel(){
           var main_panel = new ListenerResponsivePanel(){
                Location = new Point(0, 0),
                Size = new Size(250, 250),
            };
            Label play = new Label(){
                Text = "Graj",
                Location = new Point(10, 10),
                TabIndex = 0,
                ForeColor = Color.White,
            };
            Label instruction = new Label(){
                Text = "Instrukcja",
                Location = new Point(10, 50),
                TabIndex = 1,
                ForeColor = Color.White,
            };
            Label authors = new Label(){
                Text = "Autorzy",
                Location = new Point(10, 90),
                TabIndex = 2,
                ForeColor = Color.White,
            };
            Label exit = new Label(){
                Text = "Wyjście",
                Location = new Point(10, 130),
                TabIndex = 3,
                ForeColor = Color.White,
            };
            Label content = new Label(){
                Text = "",
                Location = new Point(10, 210),
                Size = new Size(200, 30),
                TabIndex = 4,
                ForeColor = Color.White,
            };

            void mouse_out(object sender, EventArgs e){
                content.Text = "";
            }

            play.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
                content.Text = "Rozpocznij grę";
            });
            play.MouseLeave += mouse_out;
            play.Click += new EventHandler(delegate (object sender, EventArgs e) {
                Controls.Clear();
                currentPanel = createGameMenu();
                Controls.Add(currentPanel);
            });
            main_panel.Controls.Add(play);

            instruction.MouseEnter += new EventHandler(delegate (object sender, EventArgs e){
                content.Text = "Zobacz zasady gry";
            });
            instruction.MouseLeave += mouse_out;
            main_panel.Controls.Add(instruction);

            authors.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
                content.Text = "Zobacz autorów gry";
            });
            authors.MouseLeave += mouse_out;
            main_panel.Controls.Add(authors);

            exit.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) {
                content.Text = "Powrót do Windows";
            });
            exit.MouseLeave += mouse_out;
            exit.Click += new EventHandler(delegate (object sender, EventArgs e){
                System.Environment.Exit(0);
            });
            main_panel.Controls.Add(exit);

            main_panel.Controls.Add(content);

            return main_panel;
        }
    }
}
