using UnityEngine;

public class CameraMovementSpot : MonoBehaviour
{
    public static CameraMovementSpot instance;
    [SerializeField, Range(1f, 10f)] float speed = 1f;
    private Vector3 destination;
    private bool moving;

    void Start() {
        instance = this;
        moving = false;
    }

    void Update() {
        if (moving) {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 0.001f) {
                transform.position = destination;
                moving = false;
            }
        }
    }

    public void MoveToPoint(Vector3 newDestination) {
        if (!moving) {
            destination = new Vector3(
                newDestination.x,
                transform.position.y,
                newDestination.z
            );
            moving = true;
        }
    }

}
