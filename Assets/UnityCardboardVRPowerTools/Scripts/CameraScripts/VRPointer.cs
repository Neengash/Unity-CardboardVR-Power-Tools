using UnityEngine;

public class VRPointer : MonoBehaviour
{
    [SerializeField]
    private float pointerDistance = 5;
    private VRInteraction gazedObject = null;
    private RaycastHit hit;

    void Update() {
        if (
            Physics.Raycast(transform.position, transform.forward, out hit, pointerDistance) && 
            hit.transform.gameObject != gazedObject?.gameObject
        ) {
            gazedObject?.OnPointerExit();
            gazedObject = hit.transform.gameObject.GetComponent<VRInteraction>();
            gazedObject?.OnPointerEnter();
        } else {
            gazedObject?.OnPointerExit();
            gazedObject = null;
        }

        if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetButtonDown("Submit")) {
            gazedObject?.OnPointerClick();
        }
        
    }
}
