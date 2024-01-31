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
        ListenerResponsivePanel create_instruction_panel(){
            var main_panel = new ListenerResponsivePanel(){
                Location = new Point(0, 0),
                Size = new Size(250, 250),
            };
            Label content = new Label(){
                Text = "Witaj w Rosyjskiej Ruletce, w grze w której zasady są bardzo proste. Albo znasz odpowiedź i wygrywasz pieniądze albo jej nie znasz i wpadasz do dziury. W grze bierze udział 6 zawodników, natomiast gra składa się z 5 rund i finału.",
                Location = new Point(10, 10),
                Size= new Size(230,120),
                TabIndex = 0,
            };
            Label previus = new Label(){
                Text = "",
                Location = new Point(10, 140),
                TabIndex = 1
            };
            Label next = new Label(){
                Text = "Następna strona",
                Location = new Point(150, 140),
                TabIndex = 2
            };
            Label back_to_menu = new Label(){
                Text = "Powrót do menu",
                Location = new Point(150, 190),
                TabIndex = 3
            };


            previus.Click += new EventHandler(delegate (object sender, EventArgs e) {

            });
            next.Click += new EventHandler(delegate (object sender, EventArgs e){
                
            });
            back_to_menu.Click += new EventHandler(delegate (object sender, EventArgs e){
                Controls.Clear();
                currentPanel = create_main_panel();
                Controls.Add(currentPanel);
            });

            main_panel.Controls.Add(content);

            return main_panel;
        }
    }
}
