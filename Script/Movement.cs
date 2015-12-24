using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotateSpeed = 50.0f;

	void LateUpdate ()
    {
        MovementOfCharacter();
        RotationWithCamera();
    }

    void MovementOfCharacter()
    {
        GetComponent<Rigidbody>().transform.Translate(Vector3.right * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        GetComponent<Rigidbody>().transform.Translate(Vector3.back * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
    }

    void RotationWithCamera()
    {
        float h = rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(0, h, 0);
    }
}
