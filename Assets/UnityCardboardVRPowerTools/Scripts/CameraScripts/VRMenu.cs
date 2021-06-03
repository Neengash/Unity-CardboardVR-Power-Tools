using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class VRMenu : MonoBehaviour
{
    [SerializeField] bool FollowCamera = true;
    [SerializeField] Transform VRCameraTransform;
    [SerializeField] Transform ownTransform;
    [SerializeField] Camera m_camera;

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    List<GameObject> hits, previousHits;

    private void Start() {
        hits = new List<GameObject>();
        previousHits = new List<GameObject>();
        m_EventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update() {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = new Vector2(m_camera.pixelWidth / 2, m_camera.pixelHeight / 2);

        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        hits.Clear();
        foreach (RaycastResult result in results) {
            hits.Add(result.gameObject);
        }

        CheckPointerExit(hits, previousHits);
        CheckPointerEnter(hits, previousHits);

        if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetButtonDown("Fire1")) {
            CheckPointerClick(hits);
        }

        UpdatePreviousHits(hits);
    }

    private void CheckPointerEnter(List<GameObject> hits, List<GameObject> prevHits) {
        foreach (GameObject hit in hits) {
            if (!prevHits.Contains(hit)) {
                VRInteraction element = hit.GetComponent<VRInteraction>();
                element?.OnPointerEnter();
            }
        }
    }

    private void CheckPointerExit(List<GameObject> hits, List<GameObject> prevHits) {
        foreach (GameObject prevHit in prevHits) {
            if (!hits.Contains(prevHit)) {
                VRInteraction element = prevHit.GetComponent<VRInteraction>();
                element?.OnPointerExit();
            }
        }
    }

    private void CheckPointerClick(List<GameObject> hits) {
        foreach (GameObject hit in hits) {
            VRInteraction element = hit.GetComponent<VRInteraction>();
            element?.OnPointerClick();
        }
    }

    private void UpdatePreviousHits(List<GameObject> hits) {
        previousHits.Clear();

        foreach (GameObject hit in hits) {
            previousHits.Add(hit);
        }
    }

    void LateUpdate() {
        if (FollowCamera) {
            ownTransform.rotation = Quaternion.Euler(
                ownTransform.rotation.eulerAngles.x,
                VRCameraTransform.rotation.eulerAngles.y,
                ownTransform.rotation.eulerAngles.z
            );
        }
    }
}
