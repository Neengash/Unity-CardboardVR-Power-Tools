using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRInteraction : MonoBehaviour
{
    [SerializeField, Range(0.5f, 10f)]
    private float LoadTime = 2f;
    Coroutine loadingCoroutine;

    [SerializeField]
    UnityEvent onPointerEnter, onPointerExit, onPointerClick, onPointerLoad;

    public void OnPointerEnter() {
        onPointerEnter?.Invoke();

        if (onPointerLoad != null) {
            loadingCoroutine = StartCoroutine(Loading());
        }
    }

    public void OnPointerExit() {
        onPointerExit?.Invoke();

        if (loadingCoroutine != null) {
            StopCoroutine(loadingCoroutine);
            VRPointerLoader.instance.resetLoad();
        }
    }

    public void OnPointerClick() {
        onPointerClick?.Invoke();
    }

    IEnumerator Loading() {
        float time = Time.deltaTime;
        VRPointerLoader.instance.SetMaxLoad(LoadTime);

        while (time < LoadTime) {
            VRPointerLoader.instance.SetLoad(time);
            yield return null;
            time += Time.deltaTime;
        }

        onPointerLoad?.Invoke();
        loadingCoroutine = null;
        VRPointerLoader.instance.resetLoad();
    }
}
