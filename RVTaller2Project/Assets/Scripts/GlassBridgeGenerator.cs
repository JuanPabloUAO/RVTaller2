using UnityEngine;

public class GlassBridgeGenerator : MonoBehaviour
{
    [Header("References")]
    public GameObject glassPrefab;
    public Transform startPlane;
    public Transform endPlane;

    [Header("Bridge Settings")]
    public float spacingX = 2f;
    public float rowSpacing = 2f;

    void Start()
    {
        GenerateBridge();
    }

    void GenerateBridge()
    {
        if (startPlane == null || endPlane == null)
        {
            Debug.LogError("Assign StartPlane and EndPlane!");
            return;
        }

        // 🔹 Calcular tamaño real del plane (Unity plane base es 10)
        float startLength = 10f * startPlane.localScale.z;
        float endLength = 10f * endPlane.localScale.z;

        // 🔹 Obtener borde delantero del StartPlane
        Vector3 startEdge = startPlane.position +
                            startPlane.forward * (startLength / 2f);

        // 🔹 Obtener borde delantero del EndPlane (lado que mira al puente)
        Vector3 endEdge = endPlane.position -
                          endPlane.forward * (endLength / 2f);

        float totalDistance = Vector3.Distance(startEdge, endEdge);

        int rows = Mathf.FloorToInt(totalDistance / rowSpacing);

        Vector3 direction = (endEdge - startEdge).normalized;

        // 🔹 Construimos el mapa visual del puente
        System.Text.StringBuilder bridgeMap = new System.Text.StringBuilder();

        for (int i = 0; i < rows; i++)
        {
            bool leftIsSafe = Random.value > 0.5f;

            Vector3 rowCenter = startEdge + direction * (i * rowSpacing);
            Vector3 rightOffset = transform.right * (spacingX / 2f);

            CreateTile(rowCenter - rightOffset, leftIsSafe);
            CreateTile(rowCenter + rightOffset, !leftIsSafe);

            // 🔹 Debug visual tipo matriz
            string left = leftIsSafe ? "[ ]" : "[X]";
            string right = leftIsSafe ? "[X]" : "[ ]";

            bridgeMap.AppendLine(left + " " + right);
        }

        Debug.Log("PUENTE GENERADO:\n\n" + bridgeMap.ToString());
    }


    void CreateTile(Vector3 position, bool isSafe)
    {
        GameObject tile = Instantiate(glassPrefab, position, Quaternion.identity);
        tile.GetComponent<GlassTile>().isSafe = isSafe;
    }
}
