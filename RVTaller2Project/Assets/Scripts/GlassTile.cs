using UnityEditor.Rendering.LookDev;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GlassTile : MonoBehaviour
{
    public bool isSafe = true;

    bool broken = false;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void StepOn()
    {
        if (broken) return;

        if (!isSafe)
        {
            broken = true;
            Break();
        }
    }

    void Break()
    {
        rb.isKinematic = false;
        rb.useGravity = true;

        Destroy(gameObject, 3f);

        CameraShake shaker = FindObjectOfType<CameraShake>();
        if (shaker != null)
        {
            shaker.Shake(0.2f, 0.15f);
        }

    }

}
