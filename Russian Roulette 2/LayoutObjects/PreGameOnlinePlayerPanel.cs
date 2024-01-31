﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Russian_Roulette{
    public partial class PreGameOnlinePlayerPanel : Panel{
        public Label name_label;
        public Label userNameLabel;
        public PreGameOnlinePlayerPanel(int posx, int posy, string default_name){
            Location= new Point(posx, posy);
            Size = new Size(200, 30);
            Margin = new Padding(0);
            Padding = new Padding(0);
            name_label = new Label() {
                Location = new Point(0, 0),
                Size = new Size(70, 30),
                Margin= new Padding(0),
                Text= default_name,
                ForeColor = Color.White
            };
            userNameLabel = new Label() {
                Location = new Point(70, 0),
                Size = new Size(130, 30),
                Margin = new Padding(0),
                ForeColor = Color.White
            };
            Controls.Add(name_label);
            Controls.Add(userNameLabel);
            InitializeComponent();
        }
    }
}
