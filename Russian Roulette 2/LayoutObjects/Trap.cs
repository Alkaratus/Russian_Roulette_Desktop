using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Russian_Roulette {
    public enum trap_state {
        closed, blue, red, open,
    }
    public partial class Trap : PictureBox{
        static readonly Dictionary<trap_state, string> traps_images_source = new Dictionary<trap_state, string>(){
            { trap_state.closed, $@"{AppDomain.CurrentDomain.BaseDirectory}img\ZZ.png" },
            { trap_state.blue, $@"{AppDomain.CurrentDomain.BaseDirectory}img\NZ.png" },
            { trap_state.red, $@"{AppDomain.CurrentDomain.BaseDirectory}img\CZ.png" },
            { trap_state.open, $@"{AppDomain.CurrentDomain.BaseDirectory}img\OZ.png" }
        };
        const int WIDTH= 99, HEIGHT=99;
        

        public Trap(Point location){
        var drawing = new System.Drawing.Drawing2D.GraphicsPath();
        Location = location;
            drawing.AddEllipse(0, 0, WIDTH, HEIGHT);
            Region = new Region(drawing);
            Size= new Size(WIDTH,HEIGHT);
            Margin = new Padding(0);
            Image = new Bitmap(traps_images_source[trap_state.closed]);
            InitializeComponent();
        }

        public void change_trap_color(trap_state trap_state){
            Image = new Bitmap(traps_images_source[trap_state]);
        }
    }
}
