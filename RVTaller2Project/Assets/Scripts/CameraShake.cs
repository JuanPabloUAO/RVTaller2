using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Transform cam;
    Vector3 originalPos;

    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
        originalPos = cam.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.localPosition = originalPos;
    }
}

