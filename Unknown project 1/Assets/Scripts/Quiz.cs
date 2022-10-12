using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Вопросы")]
    [SerializeField]TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [Header("Ответы")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    [Header("Цвета кнопок")]
    [SerializeField]Sprite defaultAnswerSprite;
    [SerializeField]Sprite correctAnswerSprite;
    [Header("Таймер")]
    [SerializeField]Image timerImage;
    Timer timer;

    [Header("Очки")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    [Header("Линия")]
    [SerializeField] Slider progressBar;
    public bool isComplete;
    
    void Awake()
    {
        
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;


       
    }

    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            if(progressBar.value == progressBar.maxValue){
           isComplete = true;
           return;
       }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion){
         DisplayAnswer(-1);
         SetButtonsState(false);
        }
    }





    public void OnAnswerSelected(int index){

        hasAnsweredEarly = true;
       DisplayAnswer(index);
       SetButtonsState(false);
       timer.CancelTimer();
       scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
       
    }

    void DisplayAnswer(int index){
         Image buttonImage;
       if(index == currentQuestion.GetCorrectAnswerIndex()){
           questionText.text = "Правильно, Зоро лох";
            buttonImage = answerButtons[index].GetComponent<Image>();
           buttonImage.sprite = correctAnswerSprite;
           scoreKeeper.IncrementCorrectAnswers();
       }
       else{
           correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
           string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
           questionText.text = "Ебать ты еблан, вообщето правильный ответ:\n" + correctAnswer;
           buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
           buttonImage.sprite = correctAnswerSprite;
       }
    }


    void GetNextQuestion(){
       if(questions.Count > 0){
           GetRandomQuestion();
        SetButtonsState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
        progressBar.value++;
        scoreKeeper.IncrementQuestionSeen();
       }
        
    }

    void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion)){
         questions.Remove(currentQuestion);
        }
        
    }

    void DisplayQuestion(){
        questionText.text = currentQuestion.GetQuestion();
        
        for(int i = 0; i < answerButtons.Length; i++){
           TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
           buttonText.text = currentQuestion.GetAnswer(i); 
        }
    }

    void SetButtonsState(bool state){
     for(int i = 0; i < answerButtons.Length; i++){
         Button button = answerButtons[i].GetComponent<Button>();
         button.interactable = state;
     }
    }

    void SetDefaultButtonSprite(){
        for(int i = 0; i < answerButtons.Length; i++){
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    
   
}
