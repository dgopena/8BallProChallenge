using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Camera cam;

    #region Input variables
    private bool tapEnded = true;

    private Vector3 tapStartPos;
    private Vector3 tapFinishPos;
    #endregion

    #region Play variables
    public float hitMaxForce = 200f;
    public float hitMaxDist = 10f;
    private Vector3 dir;

    private GameObject cue; //stick
    private GameObject hitLine; //hit trajectory
    #endregion

    // Use this for initialization
    void Start () {
        cam = Camera.main;

        cue = (GameObject)Instantiate(Resources.Load("Prefabs/Cue Stick"));
        cue.SetActive(false);
        hitLine = (GameObject)Instantiate(Resources.Load("Prefabs/HitLine"));
        hitLine.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        CheckInputPC();
    }

    void CheckInputPC()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                tapStartPos = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }
            tapEnded = false;
        }
        else if (!tapEnded)
        {
            if (Physics.Raycast(ray, out hit))
            {
                tapFinishPos = new Vector3(hit.point.x, 0.5f, hit.point.z);

                cue.SetActive(true);
                hitLine.SetActive(true);

                //movement and rotation of the movement line and cue stick
                float distPoints = Vector3.Distance(tapFinishPos, tapStartPos);
                if (distPoints <= hitMaxDist)
                {
                    dir = tapFinishPos - tapStartPos;
                    Vector3 pos = (distPoints / 2f) * dir.normalized + tapStartPos;
                    hitLine.transform.position = pos;
                    cue.transform.position = pos;
                    float sizeFactor = hitLine.transform.localScale.x;
                    hitLine.transform.localScale = new Vector3(sizeFactor, distPoints / 2f, sizeFactor);

                    if (dir.magnitude > 0.05f)
                    {
                        float angle = Mathf.Atan(dir.z / dir.x);
                        angle = (180f * angle) / Mathf.PI;
                        if (dir.x >= 0f)
                        {
                            angle += 180f;
                        }
                        hitLine.transform.rotation = Quaternion.Euler(90f, 0f, angle + 90f);
                        cue.transform.rotation = Quaternion.Euler(90f, 0f, angle + 90f);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            tapEnded = true;
            hitLine.SetActive(false);
            //we calculate total force of hit, truncating it by the maximum distance possible
            float hitLength = dir.magnitude;
            if(hitLength > hitMaxDist)
            {
                hitLength = hitMaxDist;
            }
            float hitForce = (hitMaxForce * hitLength) / hitMaxDist;
            cue.GetComponent<StickScript>().Hit(hitForce, dir.normalized);
        }
    }


}
