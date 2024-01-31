using Grpc.Net.Client;
using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Russian_Roulette.LayoutObjects.ListenerResponsivePanel;
using static System.Windows.Forms.AxHost;

namespace Russian_Roulette{
    partial class Form1{
        enum round_state{
            start,questioning,end
        }
    ListenerResponsivePanel create_game_panel(string[] players_names,int port){
            round_state state = round_state.start;
            const uint NONE_PLAYER_MARKED = 6;
            uint round = 1;
            uint marked_player = 6;
            uint currentRouletteState = 0;
            bool rouletteRunning = false;

            var channel = GrpcChannel.ForAddress($"http://192.168.26.132:{port}");
            var in_game_server = new InGameServer.InGameServerClient(channel) ;

            var main_panel = new ListenerResponsivePanel(){
                Location = new Point(0, 0),
                Size = new Size(600, 400)
            };

            PictureBox game_field = new PictureBox() {
                Location = new Point(0, 0),
                Size = new Size(400, 400),
                Image = new Bitmap($@"{AppDomain.CurrentDomain.BaseDirectory}img\Game_base.png")
            };

            var traps = new Trap[6];
            traps[0] = new Trap(new Point(150,25));
            traps[1] = new Trap(new Point(259,87));
            traps[2] = new Trap(new Point(259,213));
            traps[3] = new Trap(new Point(150,275));
            traps[4] = new Trap(new Point(41,213));
            traps[5] = new Trap(new Point(41,87));

            var players= new PlayerPanel[6];

            for(int i=0; i<players.Length; i++){
                players[i] = new PlayerPanel(Convert.ToUInt32(i), new Point(400, PlayerPanel.HEIGHT * i), players_names[i], delegate(uint index){
                    for(uint j = 0; j < players.Length; j++){
                        if (j != marked_player) {
                            players[j].setStateColor(playerState.normal);
                        }
                    }
                    in_game_server.select_player(new InGame_Selected_Player { Id = index });
                });
            }

            var question = new Label(){
                Location = new Point(400, 150),
                Size = new Size(200, 100),
                ForeColor = Color.White,
            };

            var answersPanel = new Panel(){
                Location= new Point(400,250),
                Size = new Size(200, 150),
                ForeColor = Color.White,
            };

            var answers = new AnswerLabel[5];
            for(int i = 0; i < answers.Length; i++){
                answers[i] = new AnswerLabel(Convert.ToUInt32(i),new Point(0, (30 * i)),delegate(uint index){
                    foreach(var answer in answers) {
                        answer.deactivate();
                    }
                    in_game_server.get_answer(new InGame_Selected_Answer { Id=index});
                });
            }

            var rouletteActivator = new AnswerLabel(0,new Point(0,30),delegate(uint index){
                if (!rouletteRunning){
                    Text = "Zatrzymaj ruletkę";
                    in_game_server.trigger_roulette(new InGame_Empty_Message { });
                    rouletteRunning = true;
                }
                else{
                    addAnswersToAnswerPanel();
                    in_game_server.trigger_roulette(new InGame_Empty_Message { });
                    rouletteRunning = false;
                }
            });
            rouletteActivator.activate();


            // Adding controls to main panel
            main_panel.Controls.AddRange(traps);
            main_panel.Controls.Add(game_field);
            main_panel.Controls.AddRange(players);
            main_panel.Controls.Add(question);
            addAnswersToAnswerPanel();
            main_panel.Controls.Add(answersPanel);

            //Panel management methods

            void addAnswersToAnswerPanel(){
                answersPanel.Controls.Clear();
                foreach (var answer in answers){
                    answersPanel.Controls.Add(answer);
                }
            }

            void addRouletteActivatorToAnswerPanel(){
                answersPanel.Controls.Clear();
                answersPanel.Controls.Add(rouletteActivator);
            }

            //Panel gRPC service methods

            main_panel.preQuestionSequence = delegate (){
                main_panel.BeginInvoke(new Action(delegate {
                    game_field.Image = new Bitmap($@"{AppDomain.CurrentDomain.BaseDirectory}img\Question_player{marked_player+1}.png");
                    foreach (var trap in traps){
                        trap.change_trap_color(trap_state.red);
                        trap.Refresh();
                    }
                    game_field.Refresh();
                    Thread.Sleep(500);
                    foreach (var trap in traps){
                        trap.change_trap_color(trap_state.closed);
                        trap.Refresh();
                    }
                    game_field.Refresh();
                    Thread.Sleep(500);
                    game_field.Image = new Bitmap($@"{AppDomain.CurrentDomain.BaseDirectory}img\Game_player{marked_player + 1}.png");
                    game_field.Refresh();
                }));
            };

            main_panel.changeQuestionData = delegate (string questionData) {
                main_panel.Invoke(new Action(delegate {
                    question.Text= questionData;
                }));
            };

            main_panel.startNextRound = delegate () {
                main_panel.Invoke(new Action(delegate {
                    round++;
                    state = round_state.start;
                }));
            };

            main_panel.startRoulette = delegate () {
                main_panel.Invoke(new Action(delegate {
                    foreach (var trap in traps){
                        trap.change_trap_color(round < 3 ? trap_state.blue : trap_state.red);
                    }
                    if(inGameID == marked_player){
                        rouletteActivator.Text = "Aktywuj ruletkę";
                        addRouletteActivatorToAnswerPanel();
                    }
                }));
            };

            main_panel.changeRouletteState = delegate (uint index) {
                main_panel.BeginInvoke(new Action(delegate {
                    switch (round){
                        case 1: {
                            traps[currentRouletteState].change_trap_color(trap_state.blue);
                            break; 
                        }
                        case 2: {
                            traps[currentRouletteState].change_trap_color(trap_state.blue);
                            traps[currentRouletteState + 3].change_trap_color(trap_state.blue);
                            break; 
                        }
                        case 3: {
                            traps[currentRouletteState].change_trap_color(trap_state.red);
                            traps[currentRouletteState + 2].change_trap_color(trap_state.red);
                            traps[currentRouletteState + 4].change_trap_color(trap_state.red);
                            break; 
                        }
                        case 4: {
                            traps[currentRouletteState].change_trap_color(trap_state.red);
                            traps[currentRouletteState + 3].change_trap_color(trap_state.red);
                            break; 
                        }
                        case 5: {
                            traps[currentRouletteState].change_trap_color(trap_state.red);
                            break; 
                        }
                    }
                    currentRouletteState = index;
                    switch (round){
                        case 1: {
                            traps[currentRouletteState].change_trap_color(trap_state.red);
                            break; 
                        }
                        case 2: {
                            traps[currentRouletteState].change_trap_color(trap_state.red);
                            traps[currentRouletteState + 3].change_trap_color(trap_state.red);
                            break; 
                        }
                        case 3: {
                            traps[currentRouletteState].change_trap_color(trap_state.blue);
                            traps[currentRouletteState + 2].change_trap_color(trap_state.blue);
                            traps[currentRouletteState + 4].change_trap_color(trap_state.blue);
                            break; 
                        }
                        case 4: {
                            traps[currentRouletteState].change_trap_color(trap_state.blue);
                            traps[currentRouletteState + 3].change_trap_color(trap_state.blue);
                            break; 
                        }
                        case 5: {
                            traps[currentRouletteState].change_trap_color(trap_state.blue);
                            break; 
                        }
                    }
                }));
            };

            main_panel.startEliminationRoulette = delegate (){
                main_panel.Invoke(new Action(delegate {
                    foreach (var trap in traps){
                        trap.change_trap_color(trap_state.blue);
                    }
                    if (inGameID == marked_player){
                        rouletteActivator.Text = "Aktywuj ruletkę";
                        addRouletteActivatorToAnswerPanel();
                    }
                }));
            };

            main_panel.changeAnswersData = delegate (string[] answerData) {
                main_panel.Invoke(new Action(delegate{
                    for (int i = 0; i < (round + 1); i++){
                        answers[i].Text = answerData[i];
                        if (inGameID == marked_player){
                            answers[i].activate();
                        }
                    }
                }));
                    
            };

            main_panel.answerRequire = delegate () {
                
            };

            main_panel.markAnswer = delegate (uint index, answerMark mark) {
                main_panel.Invoke(new Action(delegate {
                    answers[index].mark_answer(mark);
                }));
            };

            main_panel.clearAnswers = delegate (){
                main_panel.Invoke(new Action(delegate {
                    for (int i = 0; i < (round + 1); i++){
                        answers[i].mark_answer(answerMark.default_);
                        answers[i].Text = "";
                    }
                }));
            };

            main_panel.changePlayerState = delegate (uint index, playerState state) {
                main_panel.BeginInvoke(new Action(delegate {
                    if (index != NONE_PLAYER_MARKED){
                        players[index].setStateColor(state);
                        if (state == playerState.chosing || state == playerState.answering){
                            marked_player = index;
                            game_field.Image = new Bitmap($@"{AppDomain.CurrentDomain.BaseDirectory}img\Game_player{marked_player + 1}.png");
                        }
                    }
                    else{
                        marked_player = index;
                        game_field.Image = new Bitmap($@"{AppDomain.CurrentDomain.BaseDirectory}img\Game_base.png");
                    }
                    
                }));
            };
            
            main_panel.changePlayerMoney = delegate (uint index, uint value) {
                main_panel.Invoke(new Action(delegate {
                    players[index].setMoney(value);
                }));
            };

            main_panel.eliminatePlayer = delegate (uint index){
                main_panel.Invoke(new Action(delegate {
                    if (inGameID == index){
                        Controls.Clear();
                        currentPanel = createGameOverLayout();
                        Controls.Add(currentPanel);
                    }
                    else{
                        traps[index].change_trap_color(trap_state.open);
                        players[index].clearPlayer();
                    }
                }));    
            };
            
            main_panel.startTimer = delegate () { 
                
            };
            
            main_panel.changeOneTrap = delegate (uint index, trap_state state){
                main_panel.Invoke(new Action(delegate {
                    traps[index].change_trap_color(state);
                }));
            };

            main_panel.changeAllTraps = delegate (trap_state state) {
                main_panel.Invoke(new Action(delegate {
                    foreach (var trap in traps){
                        trap.change_trap_color(state);
                    }
                }));
            };

            //Test
            //answers[0].Text = "Pierwsza";
            //answers[1].Text = "Druga";
            //inGameID= 0;
            //marked_player = 0;
            //for (int i = 0; i < (round + 1); i++)
            //{
            //    if (inGameID == marked_player)
            //    {
            //        answers[i].activate();
            //    }
            //}

            return main_panel;
        }
    }
}
