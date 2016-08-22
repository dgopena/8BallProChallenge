using UnityEngine;
using System.Collections;

public class TableScript : MonoBehaviour {
    //Probably will be intended for board generation and distribution

    #region Objects
    private Object ball;

    [HideInInspector]
    public GameObject[] balls = new GameObject[14];

    [HideInInspector]
    public GameObject cue;
    #endregion

    #region Generation variables
    public float ballDisplace = 6f;
    #endregion

    // Use this for initialization
    void Start () {
        ball = Resources.Load("Prefabs/Ball");
        GenerateBalls();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenerateBalls()
    {
        // Instantiate Cue
        cue = (GameObject)Instantiate(ball);
        cue.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Balls/Cue", typeof(Material));

        //Instantiate the rest of the balls randomly
        for(int i=0; i<14; i++)
        {
            GameObject aux = (GameObject)Instantiate(ball);
            aux.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Balls/"+(i+1), typeof(Material));
            balls[i] = aux;
            aux.transform.position = new Vector3(Random.Range(-ballDisplace, ballDisplace), aux.transform.position.y, Random.Range(-ballDisplace, ballDisplace));
        }
    }
}
