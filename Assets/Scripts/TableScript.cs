using UnityEngine;
using System.Collections;

public enum BallDistribution
{
    Random,
    Triangle,
    Rectangle,
    Central
}

public class TableScript : MonoBehaviour {
    //Probably will be intended for board generation and distribution

    #region Objects
    private Object ball;

    [HideInInspector]
    public GameObject[] balls;

    [HideInInspector]
    public GameObject cue;
    #endregion

    #region Generation variables
    public float ballDisplace = 6f;
    public BallDistribution dist;
    public Vector3 ballCenter;

    private float separationIndex = 1.1f;
    private float rootFactor;
    #endregion

    // Use this for initialization
    void Start () {
        ball = Resources.Load("Prefabs/Ball");
        rootFactor = separationIndex * (Mathf.Sqrt(3) / 2);
        ballCenter += 0.5f * Vector3.up;

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
        cue.transform.name = "Cue";

        balls = new GameObject[15];

        //Instantiate the rest of the balls randomly
        for (int i = 0; i < 15; i++)
        {
            GameObject aux = (GameObject)Instantiate(ball);
            aux.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Balls/" + (i + 1), typeof(Material));
            balls[i] = aux;
            aux.transform.position = new Vector3(Random.Range(-ballDisplace, ballDisplace), aux.transform.position.y, Random.Range(-ballDisplace, ballDisplace));
        }
        #region Shape distributions
        //we distribute the balls accoridng to the distribution. 
        if(dist == BallDistribution.Triangle){
            cue.transform.position = (4 * rootFactor * Vector3.back) + ballCenter;
            balls[0].transform.position = (2 * rootFactor * Vector3.back) + ballCenter;
            balls[1].transform.position = (rootFactor * Vector3.back) + ((0.5f * separationIndex) * Vector3.left) + ballCenter;
            balls[2].transform.position = (rootFactor * Vector3.back) + ((0.5f * separationIndex) * Vector3.right) + ballCenter;
            balls[3].transform.position = (separationIndex * Vector3.left) + ballCenter;
            balls[4].transform.position = ballCenter;
            balls[5].transform.position = (separationIndex * Vector3.right) + ballCenter;
            balls[6].transform.position = (rootFactor * Vector3.forward) + ((1.5f * separationIndex) * Vector3.left) + ballCenter;
            balls[7].transform.position = (rootFactor * Vector3.forward) + ((0.5f * separationIndex) * Vector3.left) + ballCenter;
            balls[8].transform.position = (rootFactor * Vector3.forward) + ((0.5f * separationIndex) * Vector3.right) + ballCenter;
            balls[9].transform.position = (rootFactor * Vector3.forward) + ((1.5f * separationIndex) * Vector3.right) + ballCenter;
            balls[10].transform.position = (2 * rootFactor * Vector3.forward) + ((2f * separationIndex) * Vector3.left) + ballCenter;
            balls[11].transform.position = (2 * rootFactor * Vector3.forward) + ((separationIndex) * Vector3.left) + ballCenter;
            balls[12].transform.position = (2 * rootFactor * Vector3.forward) + ballCenter;
            balls[13].transform.position = (2 * rootFactor * Vector3.forward) + ((separationIndex) * Vector3.right) + ballCenter;
            balls[14].transform.position = (2 * rootFactor * Vector3.forward) + ((2f * separationIndex) * Vector3.right) + ballCenter;
        }
        else if (dist == BallDistribution.Rectangle)
        {
            cue.transform.position = (3 * separationIndex * Vector3.back) + ballCenter;
            balls[0].transform.position = (separationIndex * Vector3.back) + (2 * separationIndex * Vector3.left) + ballCenter;
            balls[1].transform.position = (separationIndex * Vector3.back) + (separationIndex * Vector3.left) + ballCenter;
            balls[2].transform.position = (separationIndex * Vector3.back) + ballCenter;
            balls[3].transform.position = (separationIndex * Vector3.back) + (separationIndex * Vector3.right) + ballCenter;
            balls[4].transform.position = (separationIndex * Vector3.back) + (2 * separationIndex * Vector3.right) + ballCenter;
            balls[5].transform.position = (2 * separationIndex * Vector3.left) + ballCenter;
            balls[6].transform.position = (separationIndex * Vector3.left) + ballCenter;
            balls[7].transform.position = ballCenter;
            balls[8].transform.position = (separationIndex * Vector3.right) + ballCenter;
            balls[9].transform.position = (2 * separationIndex * Vector3.right) + ballCenter;
            balls[10].transform.position = (separationIndex * Vector3.forward) + (2 * separationIndex * Vector3.left) + ballCenter;
            balls[11].transform.position = (separationIndex * Vector3.forward) + (separationIndex * Vector3.left) + ballCenter;
            balls[12].transform.position = (separationIndex * Vector3.forward) + ballCenter;
            balls[13].transform.position = (separationIndex * Vector3.forward) + (separationIndex * Vector3.right) + ballCenter;
            balls[14].transform.position = (separationIndex * Vector3.forward) + (2 * separationIndex * Vector3.right) + ballCenter;
        }
        else if(dist == BallDistribution.Central)
        {
            cue.transform.position = (4 * separationIndex * Vector3.back) + ballCenter;
            balls[0].transform.position = (2 * separationIndex * Vector3.back) + ballCenter;
            balls[1].transform.position = (1.5f * separationIndex * Vector3.back) + (rootFactor * Vector3.left) + ballCenter;
            balls[2].transform.position = (1.5f * separationIndex * Vector3.back) + (rootFactor * Vector3.right) + ballCenter;
            balls[3].transform.position = (separationIndex * Vector3.back) + ballCenter;
            balls[4].transform.position = (0.5f * separationIndex * Vector3.back) + (rootFactor * Vector3.left) + ballCenter;
            balls[5].transform.position = (0.5f * separationIndex * Vector3.back) + (rootFactor * Vector3.right) + ballCenter;
            balls[6].transform.position = (2 * rootFactor * Vector3.left) + ballCenter;
            balls[7].transform.position = ballCenter;
            balls[8].transform.position = (2 * rootFactor * Vector3.right) + ballCenter;
            balls[9].transform.position = (0.5f * separationIndex * Vector3.forward) + (rootFactor * Vector3.left) + ballCenter;
            balls[10].transform.position = (0.5f * separationIndex * Vector3.forward) + (rootFactor * Vector3.right) + ballCenter;
            balls[11].transform.position = (separationIndex * Vector3.forward) + ballCenter;
            balls[12].transform.position = (1.5f * separationIndex * Vector3.forward) + (rootFactor * Vector3.left) + ballCenter;
            balls[13].transform.position = (1.5f * separationIndex * Vector3.forward) + (rootFactor * Vector3.right) + ballCenter;
            balls[14].transform.position = (2 * separationIndex * Vector3.forward) + ballCenter;
        }
        #endregion
    }
}
