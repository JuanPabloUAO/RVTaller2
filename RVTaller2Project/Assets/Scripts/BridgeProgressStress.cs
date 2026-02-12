using UnityEngine;

public class BridgeProgressStress : MonoBehaviour
{
    [Header("References")]
    public Transform startPlane;
    public Transform endPlane;
    public Transform player;

    [Header("Heartbeat")]
    public AudioSource heartbeatAudio;
    public float minPitch = 0.8f;
    public float maxPitch = 1.8f;

    float totalDistance;
    Vector3 startEdge;
    Vector3 endEdge;

    void Start()
    {
        CalculateEdges();
        totalDistance = Vector3.Distance(startEdge, endEdge);

        heartbeatAudio.Play();
    }

    void Update()
    {
        UpdateHeartbeat();
    }

    void CalculateEdges()
    {
        float startLength = 10f * startPlane.localScale.z;
        float endLength = 10f * endPlane.localScale.z;

        startEdge = startPlane.position +
                    startPlane.forward * (startLength / 2f);

        endEdge = endPlane.position -
                  endPlane.forward * (endLength / 2f);
    }

    void UpdateHeartbeat()
    {
        float distanceFromStart = Vector3.Distance(startEdge, player.position);

        float progress = Mathf.Clamp01(distanceFromStart / totalDistance);

        float targetPitch = Mathf.Lerp(minPitch, maxPitch, progress);

        heartbeatAudio.pitch = Mathf.Lerp(
            heartbeatAudio.pitch,
            targetPitch,
            Time.deltaTime * 2f
        );
    }
}
