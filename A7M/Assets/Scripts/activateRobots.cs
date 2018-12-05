using UnityEngine;

public class activateRobots : MonoBehaviour {

	// Use this for initialization
	void Start () {

        gameObject.GetComponent<RobotA>().enabled = true;
        gameObject.GetComponent<RobotB>().enabled = true;
        gameObject.GetComponent<RobotC>().enabled = true;

        if (PlayerPrefs.GetString("robotA") == "off")
        {
            gameObject.GetComponent<RobotA>().enabled = false;
        }
        if (PlayerPrefs.GetString("robotB") == "off")
        {
            gameObject.GetComponent<RobotB>().enabled = false;
        }
        if (PlayerPrefs.GetString("robotC") == "off")
        {
            gameObject.GetComponent<RobotC>().enabled = false;
        }

    }

}
