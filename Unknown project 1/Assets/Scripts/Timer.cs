using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool isAnsweringQuestion;
    public bool loadNextQuestion;
    public float fillFraction;

   float timerValue;
    
    void Update()
    {
        UpdateTimer();
    }
    
    public void CancelTimer(){
        timerValue = 0;
    }

    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion){
            if(timerValue > 0){
            fillFraction = timerValue / timeToCompleteQuestion;
            }
           else{
           
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        }
            else
            {
                if(timerValue > 0)
                {
                 fillFraction = timerValue / timeToShowCorrectAnswer;
                }
                else
                {
                    isAnsweringQuestion = true;
                    timerValue = timeToCompleteQuestion;
                    loadNextQuestion = true;
                }
            }
        
        
        Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}
