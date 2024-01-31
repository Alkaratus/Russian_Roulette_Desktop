using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Russian_Roulette{
    public partial class Form1{
        ListenerResponsivePanel createGameOverLayout(string wonPoints=""){
            var panel = new ListenerResponsivePanel()
            {
                Location = new Point(0, 0),
                Size = new Size(250, 250)
            };

            Label content = new Label(){
                Location = new Point(10, 10),
                Size = new Size(200, 30),
                Text = "Zostałeś wyeliminowany z gry",
                ForeColor = Color.White,
            };
            if (wonPoints != ""){
                content.Text += $"Wygrałeś/aś {wonPoints} punktów";
            }

            Label back = new Label()
            {
                Location = new Point(100, 210),
                Size = new Size(150, 30),
                Text = "Powrót do Menu",
                ForeColor = Color.White,
            };

            panel.Controls.Add(content);

            back.Click += new EventHandler(delegate (object sender, EventArgs e) {
                Controls.Clear();
                currentPanel = createGameMenu();
                Controls.Add(currentPanel);
            });
            panel.Controls.Add(back);

            return panel;
        }
    }
}
