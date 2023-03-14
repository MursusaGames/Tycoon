using UnityEngine;
public class Converter : MonoBehaviour 
{
    public static Converter instance;

    private void Awake()
    {
        instance = this;
    }
    public string ConvertMoneyView(double score)
    {
        if (score >= 1000000000000000000000d) return (score / 1000000000000000000000d).ToString("F1") + "B";
        else if (score >= 1000000000000000000d) return (score / 1000000000000000000d).ToString("F1") + "M";
        else if (score >= 1000000000000000d) return (score / 1000000000000000d).ToString("F1") + "K";
        else if (score >= 1000000000000d) return (score / 1000000000000d).ToString("F1") + "t";
        else if (score >= 1000000000d) return (score / 1000000000d).ToString("F1") + "b";
        else if (score >= 1000000d) return (score / 1000000d).ToString("F1") + "m";
        else if (score >= 1000d) return (score / 1000d).ToString("F1") + "k";
        else return score.ToString("F1");
    }
}
