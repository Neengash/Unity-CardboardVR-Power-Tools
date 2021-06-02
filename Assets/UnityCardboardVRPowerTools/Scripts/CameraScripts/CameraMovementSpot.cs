using UnityEngine;

public class CameraMovementSpot : MonoBehaviour
{
    public static CameraMovementSpot instance;
    [SerializeField, Range(1f, 10f)] float speed = 1f;

    [SerializeField]
    Transform baseTransform; 
    private Vector3 destination;
    private bool moving;

    void Start() {
        instance = this;
        moving = false;
        Debug.Log("Start - speedValue = " + speed);
    }

    void Update() {
        Debug.Log("Update - Start - movingValue = " + moving.ToString());
        if (moving) {
            baseTransform.position = Vector3.MoveTowards(baseTransform.position, destination, speed*Time.deltaTime);
        }

        if (Vector3.Distance(baseTransform.position, destination) < 0.01f) {
            Debug.Log("Update - arrived at destination");
            baseTransform.position = destination;
            moving = false;
        }
    }

    public void MoveToPoint(Vector3 newDestination) {
        Debug.Log("MoveToPoint - Start");
        if (!moving) {
            Debug.Log("MoveToPoint - Inside if not moving");
            destination = new Vector3(
                newDestination.x,
                baseTransform.position.y,
                newDestination.z
            );
            moving = true;
        }
    }

}
