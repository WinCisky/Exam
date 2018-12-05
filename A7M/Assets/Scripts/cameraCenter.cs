using UnityEngine;

public class cameraCenter : MonoBehaviour {

    public Camera myCamera;

	// Use this for initialization
	void Start () {
        float posx = (float)GetComponent<Maze>().lengthx / 2;
        float posy = (float)GetComponent<Maze>().lengthy / 2;
        //0.5f is unity deafult center scene
        transform.position = new Vector3(posx-0.5f, posy-0.5f, -2f);

        
        
        if(GetComponent<Maze>().lengthy> (GetComponent<Maze>().lengthx / myCamera.aspect))
        {
            myCamera.orthographicSize = (float)(GetComponent<Maze>().lengthy + 1) / 2;
        }else
        {
            myCamera.orthographicSize = (float)((GetComponent<Maze>().lengthx / myCamera.aspect) + 1) / 2;
        }

        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
