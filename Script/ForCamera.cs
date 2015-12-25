using UnityEngine;
using System.Collections;

public class ForCamera : MonoBehaviour
{
    public Transform cameraTarget;
    public GameObject Pacman;
    public float pacmanHeight = 1.5f;
    public float pacmanSpeed = 5.0f;

    public float minViewDistance = 3.0f;
    public float maxViewDistance = 5.0f;
    public int zoomRate = 70;
    public int lerpRate = 3;
    public float distance = 30; //begin distance for camera
    public int yRetrictionMin = -90;
    public int yRestrictionMax = 90;

    private float x = 0.0f;
    private float y = 0.0f;

    private int mouseXSpeed = 5;
    private int mouseYSpedd = 3;

    private float desiredDistance;
    private float correctedDistance;
    private float currentDistance;


    void Start ()
    {
        Pacman = GameObject.Find("Pacman");
        cameraTarget = Pacman.transform;

        Vector3 angles = transform.eulerAngles; //uses degreese from 0 to 360

        x = angles.x;
        y = angles.y;

        currentDistance = distance;
        correctedDistance = distance;
        desiredDistance = distance;
    }

    void LateUpdate ()
    {
        //move forward with both mouse buttons down
        if (Input.GetMouseButton(1) & Input.GetMouseButton(0))
        {
            //returns camera behind the player
            float targetRotationAngle = cameraTarget.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, lerpRate * Time.deltaTime);

            //rotate player with mouse
            cameraTarget.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseXSpeed, 0);

            x += Input.GetAxis("Mouse X") * mouseXSpeed;
            cameraTarget.transform.Translate(Vector3.forward * pacmanSpeed * Time.deltaTime);
            y -= Input.GetAxis("Mouse Y") * mouseYSpedd;
        }

        //rotate win left mouse button
        else if(Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * mouseXSpeed;
            y -= Input.GetAxis("Mouse Y") * mouseYSpedd;
        }

        //target rotation with right mouse button
        else if(Input.GetMouseButton(1))
        {
            //returns camera behind the player
            float targetRotationAngle = cameraTarget.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, lerpRate * Time.deltaTime);

            //rotate player with mouse
            cameraTarget.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseXSpeed, 0);
            //makes move on X more smoothe and removes a bug
            x += Input.GetAxis("Mouse X") * mouseXSpeed;
            y -= Input.GetAxis("Mouse Y") * mouseYSpedd;
        }

        //Change camera position back behind the player if WASD is used
        else if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float targetRotationAngle = cameraTarget.eulerAngles.y;
            float cameraRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(cameraRotationAngle, targetRotationAngle, lerpRate * Time.deltaTime);
        }

        //Min - Max angle for camera on Y-axes
        y = ClampAngle(y, yRetrictionMin, yRestrictionMax);

        //Set camera rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance); // Calculates scrolling
        desiredDistance = Mathf.Clamp(desiredDistance, minViewDistance, maxViewDistance);
        correctedDistance = desiredDistance;

        //calculates desired camera position
        Vector3 position = cameraTarget.position - (rotation * Vector3.forward * desiredDistance);

        //Check for collision
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y + pacmanHeight, cameraTarget.position.z);

        //if tere was a collision, calculate new camera position
        bool isCorrected = false;
        if(Physics.Linecast(trueTargetPosition, position, out collisionHit))
        {
            position = collisionHit.point;
            correctedDistance = Vector3.Distance(trueTargetPosition, position);
            isCorrected = true;
        }

        // For zooming, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomRate) : correctedDistance;

        //recalculate position with new currentDistance
        position = cameraTarget.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -pacmanHeight, 0));

        transform.rotation = rotation;  //rotation of camera
        transform.position = position;  //position of camera
    }

    //All stays in 360 degreese and restrict camera movement
    public static float ClampAngle (float angle, float min, float max)
    {
        if( angle < -360)
        {
            angle += 360;
        }

        if(angle > 360)
        {
            angle -= 360;
        }

        return (Mathf.Clamp(angle, min, max));
    }
}
