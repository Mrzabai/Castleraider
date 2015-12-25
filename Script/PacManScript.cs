using UnityEngine;
using System.Collections;

public class PacManScript : MonoBehaviour {

    float speed;
    float rotationSpeed;
    float distToGround;
    Rigidbody rb;
    Vector3 mouse;
    bool jumping;
    bool move;
    bool faling;
    public GameObject cameraRotation;
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speed = 20.0f;
        rotationSpeed = 3;
        move = true;
        jumping = false;
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (move)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(0, 0, speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(0, 0, -speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-speed, 0, 0);
            }
            if (Input.GetButtonDown("Jump"))
                rb.velocity = new Vector3(0, 10, 0);

            
            float h = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
            cameraRotation.transform.Rotate(0, h, 0);

            Debug.Log(h.ToString());
            distToGround = GetComponent<Collider>().bounds.extents.y;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Pallet")
        {
            Destroy(other.gameObject);
        }
    }

    void Jump()
    {
        jumping = true;
        if (jumping)
        {

        }
        rb.AddForce(0, 100, 0);
    }
}
