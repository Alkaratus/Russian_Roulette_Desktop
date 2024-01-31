using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Russian_Roulette.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Russian_Roulette
{
    internal class ClientListenerServer: ClientListener.ClientListenerBase{

        readonly static Dictionary<Player_State, playerState> inGamePlayerStates = new(){
            {Player_State.Normal,playerState.normal},
            {Player_State.Enable,playerState.enable},
            {Player_State.Chosing,playerState.chosing},
            {Player_State.Answering,playerState.answering}
        };

        readonly static Dictionary<Trap_State, trap_state> inGameTrapStates = new(){
            {Trap_State.Closed, trap_state.closed},
            {Trap_State.Open, trap_state.open},
            {Trap_State.Red, trap_state.red},
            {Trap_State.Blue, trap_state.blue}
        };

        readonly static Dictionary<Answer_State, answerMark> inGameAnswerMarks = new(){
            {Answer_State.Marked,answerMark.marked },
            {Answer_State.Wrong, answerMark.wrong},
            {Answer_State.Correct, answerMark.correct},
        };

        Form1 form;

        public ClientListenerServer(Form1 form){
            this.form = form;
        }

        public override Task<Listener_Empty_Message> send_player_information(Listener_Player_Data request, ServerCallContext context){
            form.currentPanel.playerNameSetter(request.Id,request.Name);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> countdown(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.gamePrepereing();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> start_game(Listener_Game_Service_Port request, ServerCallContext context){
            form.currentPanel.gameStarting(request.Port);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> start_next_round(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.startNextRound();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> start_roulette(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.startRoulette();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> change_roulette_state(Listener_Index request, ServerCallContext context){
            form.currentPanel.changeRouletteState(request.Id);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> start_elimination_roulette(Listener_Empty_Message request, ServerCallContext context)
        {
            form.currentPanel.startEliminationRoulette();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> pre_question_sequence(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.preQuestionSequence();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> question_data(Listener_Question_Data req, ServerCallContext context){
            form.currentPanel.changeQuestionData(req.Data);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> answers_data(Listener_Answers_Data req, ServerCallContext context){
            var answers = new string[5] {req.Answer1,req.Answer2,req.Answer3,req.Answer4,req.Answer5 }; 
            form.currentPanel.changeAnswersData(answers);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> answer_require(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.answerRequire();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> mark_answer(Listener_Answer_Mark request, ServerCallContext context){
            form.currentPanel.markAnswer(request.Id,inGameAnswerMarks[request.State]);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> clear_answers(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.clearAnswers();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> change_player_state(Listener_Choosen_Player_State request, ServerCallContext context){
            form.currentPanel.changePlayerState(request.Id, inGamePlayerStates[request.State]);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> change_player_money(Listener_Choosen_Player_Money request, ServerCallContext context){
            form.currentPanel.changePlayerMoney(request.Id,request.Money);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> eliminate_player(Listener_Index request, ServerCallContext context){
            form.currentPanel.eliminatePlayer(request.Id);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> start_timer(Listener_Empty_Message request, ServerCallContext context){
            form.currentPanel.startTimer();
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> change_one_trap(Listener_Trap_To_Change request, ServerCallContext context){
            form.currentPanel.changeOneTrap(request.Id, inGameTrapStates[request.State]);
            return Task.FromResult(new Listener_Empty_Message());
        }

        public override Task<Listener_Empty_Message> change_all_traps(Listener_Traps_State request, ServerCallContext context){
            form.currentPanel.changeAllTraps(inGameTrapStates[request.State]);
            return Task.FromResult(new Listener_Empty_Message());
        }
    }
}
