using UnityEngine;

public class PlayerStepDetector : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GlassTile tile = hit.collider.GetComponent<GlassTile>();

        if (tile != null)
        {
            tile.StepOn();
        }
    }
}
