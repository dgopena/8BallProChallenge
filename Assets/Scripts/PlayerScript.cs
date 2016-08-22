using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Camera cam;
    private Vector3 tapStartPos;

    public float hitForce = 2f;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            tapStartPos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(tapStartPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Ball")
                {
                    Vector3 forceDir = (hit.transform.position - hit.point).normalized;
                    Rigidbody rBall = hit.transform.GetComponent<Rigidbody>();
                    rBall.AddForce(hitForce * forceDir, ForceMode.Impulse);
                }
            }
        }
    }
}
