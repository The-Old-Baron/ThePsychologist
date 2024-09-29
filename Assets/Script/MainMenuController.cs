using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject Initial;
    public GameObject Questions;
    public void HideAll()
    {
        Initial.SetActive(false);
        Questions.SetActive(false);
    }
    public void ShowInitial()
    {
        HideAll();
        Initial.SetActive(true);
    }
    public void ShowQuestions()
    {
        HideAll();
        Questions.SetActive(true);
    }
    public void startNewGame()
    {
        ShowQuestions();
        QuestionController.instance.StartQuestion();

    }
}
