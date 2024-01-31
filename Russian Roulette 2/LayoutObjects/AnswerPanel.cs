using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Russian_Roulette.LayoutObjects{
    public enum answerMark{
        default_,marked,wrong,correct
    }
    public class AnswerLabel:Label{
        readonly static Dictionary<answerMark, Color> answersColors = new Dictionary<answerMark, Color>() {
            {answerMark.default_,SystemColors.ActiveCaptionText },
            {answerMark.marked,Color.Orange },
            {answerMark.wrong,Color.Red},
            {answerMark.correct,Color.Green }
        };


        public delegate void ClickFunction(uint id);

        bool active;
        public AnswerLabel(uint id,Point location, ClickFunction clickFunction){
            Location = location;
            Size = new Size(200, 30);
            Margin = new Padding(0);
            Padding = new Padding(0);
            ForeColor = Color.White;
            active = false;

            MouseHover += delegate (object? sender, EventArgs e){
                if (active){
                    Cursor = Cursors.Hand;
                    BackColor = Color.Yellow;
                }
            };

            MouseLeave += delegate (object? sender, EventArgs e){
                if (active){
                    BackColor = SystemColors.ActiveCaptionText;
                }
            };

            Click += delegate (object? sender, EventArgs e){
                if (active){
                    mark_answer(answerMark.marked);
                    clickFunction(id);
                }
            };
        }

        public void mark_answer(answerMark mark){
            BackColor = answersColors[mark];
        }

        public void clear(){
            Text = "";
            BackColor = SystemColors.ActiveCaptionText;
        }

        public void activate(){
            active = true;
        }

        public void deactivate() {
            active = false;
        }
    }

}
