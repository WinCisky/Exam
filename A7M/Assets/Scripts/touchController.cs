using UnityEngine;
using UnityEngine.SceneManagement;

public class touchController : MonoBehaviour {
	void Update () {
        #if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("widthandheight");
            }
        }
        #endif
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("widthandheight");
        }
    }
}
