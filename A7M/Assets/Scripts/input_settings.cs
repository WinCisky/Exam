using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class input_settings : MonoBehaviour {

    public InputField lenght_value;
    public InputField height_value;
    public Slider speed_value;
    public Toggle rA, rB, rC;

    public void CheckValues()
    {
        if ((int.Parse(lenght_value.text) > 0) && (int.Parse(lenght_value.text) < 201))
        {
            if ((int.Parse(height_value.text) > 0) && (int.Parse(height_value.text) < 201))
            {
                if (rA.isOn || rB.isOn || rC.isOn)
                {
                    PlayerPrefs.SetInt("x_value", int.Parse(lenght_value.text));
                    PlayerPrefs.SetInt("y_value", int.Parse(height_value.text));
                    PlayerPrefs.SetFloat("speed_value", speed_value.value);
                    if (rA.isOn)
                    {
                        PlayerPrefs.SetString("robotA", "on");
                    }else
                    {
                        PlayerPrefs.SetString("robotA", "off");
                    }
                    if (rB.isOn)
                    {
                        PlayerPrefs.SetString("robotB", "on");
                    }
                    else
                    {
                        PlayerPrefs.SetString("robotB", "off");
                    }
                    if (rC.isOn)
                    {
                        PlayerPrefs.SetString("robotC", "on");
                    }
                    else
                    {
                        PlayerPrefs.SetString("robotC", "off");
                    }
                    SceneManager.LoadScene("main");
                }
            }
        }
    }
}
