using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{

    public static int score = 0;
    private Text myText;
    private TextMesh myTextMesh;
    private TextMeshProUGUI myTechMeshProUGUI;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        if (!myText)
        {
            myTextMesh = GetComponent<TextMesh>();
        }
        if (!myTextMesh)
        {
            myTechMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
        // Reset();
        if (myText)
            myText.text = score.ToString();
    }


    public void Score(int points)
    {
        score += points;
        if (myText)
            myText.text = score.ToString();
        else if (myTextMesh)
        {
            myTextMesh.text = score.ToString();
        }
        else if (myTechMeshProUGUI)
        {
            myTechMeshProUGUI.text = score.ToString();
        }
    }

    public static void Reset()
    {
        score = 0;
        //myText.text = score.ToString();
    }
}
