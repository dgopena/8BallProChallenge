using UnityEngine;
using System.Collections;

public class StickScript : MonoBehaviour {
    //more than anything this class controls collsion of the stick and the animation for the movement
    #region Movement variables
    private Vector3 startPosition;
    private int movePhase = 0;

    private Vector3 hitDir;

    private float backSpeed = 10f; //how fast does it go back
    private float backFactor = 4f; //how back the cue goes
    private float forwSpeed = 46f; //how fast does it go forward
    private float forwFactor = 20f; //how forward does the cue goes

    private float startTime;

    private GameObject hitTarget;
    private Vector3 hitPos;
    #endregion

    private float hitForce;

    private Renderer rend;

    // Use this for initialization
    void Start () {
        rend = transform.FindChild("Stick").GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(movePhase == 1) //backing up
        {
            float distCovered = (Time.time - startTime) * backSpeed;
            float fracJourney = distCovered / backFactor;
            transform.position = Vector3.Lerp(startPosition, (backFactor * hitDir) + startPosition, fracJourney);
            if (fracJourney > 1)
            {
                movePhase++;
                startPosition = transform.position;
                startTime = Time.time;
            }
        }
        else if(movePhase == 2) //hitting
        {
            float distCovered = (Time.time - startTime) * forwSpeed;
            float fracJourney = distCovered / forwFactor;
            transform.position = Vector3.Lerp(startPosition, (-forwFactor * hitDir) + startPosition, fracJourney);
            if (fracJourney > 1)
            {
                startPosition = transform.position;
                startTime = Time.time;
                movePhase++;
            }
        }
        else if(movePhase == 3) //post hitting back up
        {
            float distCovered = (Time.time - startTime) * (backSpeed/3f);
            float fracJourney = distCovered / (backFactor/4f);
            transform.position = Vector3.Lerp(startPosition, ((backFactor/5f) * hitDir) + startPosition, fracJourney);
            if (fracJourney > 1)
            {
                movePhase = 0;
                gameObject.SetActive(false);
            }
        }
	}

    public void Hit(float hitForce, Vector3 hitDir)
    {
        if (movePhase == 0)
        {
            startPosition = transform.position;
            startTime = Time.time;
            this.hitForce = hitForce;
            this.hitDir = hitDir;
            movePhase = 1;

            TargetObj();
        }
    }

    //we throw a ray to check which is the first object we'll hit
    void TargetObj()
    {
        Ray ray = new Ray(transform.position, -hitDir);
        RaycastHit hit;
        //we get point of collision
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Ball")
            {
                hitTarget = hit.transform.gameObject;
                hitPos = hit.point;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (movePhase == 2) //only hit if the cue is in hitting phase
        {
            if (hitTarget != null)
            {
                Vector3 forceDir = (hitTarget.transform.position - hitPos).normalized;
                Rigidbody rBall = hitTarget.GetComponent<Rigidbody>();
                rBall.AddForce(hitForce * forceDir, ForceMode.Impulse);
            }

            //we back up
            startPosition = transform.position;
            startTime = Time.time;
            movePhase++;
        }
    }
}
