using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Russian_Roulette.LayoutObjects.ListenerResponsivePanel;

namespace Russian_Roulette.LayoutObjects{
    public class ListenerResponsivePanel: Panel{
        public delegate void PlayerNameSetter(uint playerIndex, string name);
        public delegate void GamePrepereing();
        public delegate void GameStarting(int port);
        public delegate void StartNextRound();
        public delegate void StartRoulette();
        public delegate void ChangeRouletteState(uint index);
        public delegate void StartEliminationRoulette();
        public delegate void PreQuestionSequence();
        public delegate void ChangeQuestionData(string questionData);
        public delegate void ChangeAnswersData(string[] answerData);
        public delegate void AnswerRequire();
        public delegate void MarkAnswer(uint index, answerMark mark);
        public delegate void ClearAnswers();
        public delegate void ChangePlayerState(uint index, playerState state);
        public delegate void ChangePlayerMoney(uint index, uint value);
        public delegate void EliminatePlayer(uint index);
        public delegate void StartTimer();
        public delegate void ChangeOneTrap(uint index, trap_state state);
        public delegate void ChangeAllTraps(trap_state state);
        
        public PlayerNameSetter playerNameSetter;
        public GamePrepereing gamePrepereing;
        public GameStarting gameStarting;
        public StartNextRound startNextRound;
        public StartRoulette startRoulette;
        public ChangeRouletteState changeRouletteState;
        public StartEliminationRoulette startEliminationRoulette;
        public PreQuestionSequence preQuestionSequence;
        public ChangeQuestionData changeQuestionData;
        public ChangeAnswersData changeAnswersData;
        public AnswerRequire answerRequire;
        public MarkAnswer markAnswer;
        public ClearAnswers clearAnswers;
        public ChangePlayerState changePlayerState;
        public ChangePlayerMoney changePlayerMoney;
        public EliminatePlayer eliminatePlayer;
        public StartTimer startTimer;
        public ChangeOneTrap changeOneTrap;
        public ChangeAllTraps changeAllTraps;


        public ListenerResponsivePanel(){
            playerNameSetter = delegate (uint playerIndex, string name) { };
            gamePrepereing = delegate () { };
            gameStarting = delegate (int port) { };
            startNextRound = delegate () { };
            startRoulette = delegate () { };
            changeRouletteState = delegate (uint index) { };
            startEliminationRoulette = delegate () { };
            preQuestionSequence = delegate () { };
            changeQuestionData = delegate (string questionData) { };
            changeAnswersData = delegate (string[] answerData) { };
            answerRequire = delegate () { };
            markAnswer = delegate (uint index, answerMark mark) { };
            clearAnswers = delegate () { };
            changePlayerState = delegate (uint index, playerState state) { };
            changePlayerMoney = delegate(uint index,uint value) { };
            eliminatePlayer = delegate (uint index) { };
            startTimer= delegate () { };
            changeOneTrap = delegate (uint index, trap_state state) { };
            changeAllTraps = delegate (trap_state state) { };
        }

    }
}
