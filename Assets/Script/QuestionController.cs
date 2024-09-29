using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;
    public TMP_Text questionPanel;
    public TMP_Text answerButtonOne;
    public TMP_Text answerButtonTwo;

    public Animator animator;

    public int questionIndex;
    public List<Question> questions;
    public List<Answer> answers;

    public Question question;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void StartQuestion()
    {
        Debug.Log("Starting new game");
        questionIndex = 0;
        question = questions[questionIndex];
        LoadQuestionOnScreen(question);
        animator.SetBool("InQuestion", true);
    }

    public void NextQuestion()
    {
        questionIndex++;
        if (questionIndex < questions.Count)
        {
            question = questions[questionIndex];
            LoadQuestionOnScreen(question);
        }
        else
        {
            StartNewGame();
        }
    }

    public void LoadQuestionOnScreen(Question question)
    {
        questionPanel.text = question.question;

        if (question.answers.Count > 0)
        {
            answerButtonOne.text = question.answers[0].answer;
            answerButtonTwo.text = question.answers[1].answer;
        }
        else{
            Debug.Log("No answers found");
            answerButtonOne.text = "";
            answerButtonTwo.text = "";
            StartCoroutine(WaitForPlayerClick());
        }
        animator.SetBool("Ping", !animator.GetBool("Ping"));

    }

    public void SelectFirstAnswer()
    {
        if (question.answers.Count == 0)
        {
            Debug.Log("No second answer found");
            return;
        }
        Debug.Log("First answer selected");
        answers.Add(question.answers[0]);

        NextQuestion();
    }
    public void SelectLastAnswer() {
        if(question.answers.Count == 0)
        {
            Debug.Log("No second answer found");
            return;
        }
        Debug.Log("Last answer selected");
        answers.Add(question.answers[1]);

        NextQuestion();
    }
    public void StartNewGame()
    {
        StartCoroutine(PlayExitAnimationAndLoadScene());
    }

    private IEnumerator PlayExitAnimationAndLoadScene()
    {
        animator.SetTrigger("Exit"); // Assuming you have an "Exit" trigger in your animator
        yield return new WaitForSeconds(6.0f); // Wait for the animation to play (adjust the duration as needed)
        Debug.Log("Loading next scene");
        SceneManager.LoadScene("Level1");
    }
    private IEnumerator WaitForPlayerClick()
    {
        Debug.Log("Waiting for player click");
        bool clicked = false;
        while (!clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicked = true;
                Debug.Log("Player clicked");
            }
            yield return null;
        }
        NextQuestion();
    }

}



