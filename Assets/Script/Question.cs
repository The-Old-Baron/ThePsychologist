using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question/Question")]
public class Question : ScriptableObject
{
    public string question;
    public List<Answer> answers;
}