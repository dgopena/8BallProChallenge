using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public float orbitFactor = 0.3f;
    public Vector3 orbitCenter;
    public float orbitLookFactor = 4f; //how away from the center we look (positive is to look up from the center. negative is down from the center) 
    public float startAngle = 180;

    public float radius = 10f;
    private float angle;
    private float lastAngle;

    // Use this for initialization
    void Start() {
        angle = startAngle;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.A))
        {
            OrbitLeft();
            Debug.Log("left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            OrbitRight();
            Debug.Log("right");
        }
        if (angle != lastAngle)
        {
            //fix range terms
            if(angle < 0)
            {
                angle = 360 + angle;
            }
            else if(angle > 360)
            {
                angle = angle - 360;
            }
            float posX = radius * Mathf.Sin((angle * Mathf.PI) / 180);
            float posZ = Mathf.Sqrt(Mathf.Pow(radius, 2f) - Mathf.Pow(posX, 2f));
            //fix radian factor
            if(angle >= 90 && angle <= 270)
            {
                posZ *= -1;
            }
            transform.position = new Vector3(posX, transform.position.y, posZ);
            lastAngle = angle;
            Vector3 lookPos = (orbitLookFactor * new Vector3(transform.position.x, 0f, transform.position.z).normalized) + orbitCenter;
            transform.LookAt(lookPos);
        }
	}

    public void OrbitRight()
    {
        angle += orbitFactor;
        Debug.Log(angle);
    }

    public void OrbitLeft()
    {
        angle -= orbitFactor;
        Debug.Log(angle);
    }
}
