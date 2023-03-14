using System.Collections;
using TMPro;
using UnityEngine;

public class TextToChar : MonoBehaviour
{
    public bool skipText;
    private bool isPrint;
    public string textMess;
    public TextMeshProUGUI dialogField;
    
    void Start()
    {
        StartCoroutine(TextPrint( textMess, 0.03f, skipText));
    }
    IEnumerator TextPrint( string input, float delay, bool skip)
    {
        if (isPrint) yield break;
        isPrint = true;
        //вывод текста побуквенно
        for (int i = 1; i <= input.Length; i++)
        {
            if (skip) { dialogField.text = input; yield break; }
            dialogField.text = input.Substring(0, i);                      
            yield return new WaitForSeconds(delay);
        }
    }
}
