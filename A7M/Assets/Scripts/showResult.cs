using UnityEngine;
using UnityEngine.UI;

public class showResult : MonoBehaviour {

    public Canvas resultCanvas;
    public Text resultText;
    bool finishedRA, finishedRB, finishedRC;
    bool resultsShowed;
    string[] classific;
    int position;

    void Awake()
    {
        resultCanvas.enabled = false;
    }

    // Use this for initialization
    void Start () {

        resultsShowed = false;
        finishedRA = false;
        finishedRB = false;
        finishedRC = false;
        classific = new string[3];
        position = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if(PlayerPrefs.GetString("robotA") == "on")
        {
            if (PlayerPrefs.GetInt("ROBOT_A") == 1)
            {
                classific[position] = "RobotA";
                position++;
                PlayerPrefs.SetInt("ROBOT_A", 0);
                finishedRA = true;
            }
        }else
        {
            finishedRA = true;
        }
        if (PlayerPrefs.GetString("robotB") == "on")
        {
            if (PlayerPrefs.GetInt("ROBOT_B") == 1)
            {
                classific[position] = "RobotB";
                position++;
                PlayerPrefs.SetInt("ROBOT_B", 0);
                finishedRB = true;
            }
        }
        else
        {
            finishedRB = true;
        }
        if (PlayerPrefs.GetString("robotC") == "on")
        {
            if (PlayerPrefs.GetInt("ROBOT_C") == 1)
            {
                classific[position] = "RobotC";
                position++;
                PlayerPrefs.SetInt("ROBOT_C", 0);
                finishedRC = true;
            }
        }
        else
        {
            finishedRC = true;
        }
        
        if (finishedRA && finishedRB && finishedRC)
        {
            if (!resultsShowed)
            {
                resultsShowed = true;
                resultCanvas.enabled = true;
                string s;

                s = "Classific: \n";
                for (int y = 0; y < (position); y++)
                {
                    s += classific[y];
                    s += " \n ";
                }
                s += " \n \n";
                if (PlayerPrefs.GetString("robotA") == "on")
                {
                    s += PlayerPrefs.GetString("ROBOTA");
                    s += " \n ";
                }
                if (PlayerPrefs.GetString("robotB") == "on")
                {
                    s += PlayerPrefs.GetString("ROBOTB");
                    s += " \n ";
                }
                if (PlayerPrefs.GetString("robotC") == "on")
                {
                    s += PlayerPrefs.GetString("ROBOTC");
                }
                resultText.text = s;
            }
        }

    }
}
