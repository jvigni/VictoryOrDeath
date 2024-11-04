using UnityEngine;

public class HighlightWithEmission : MonoBehaviour
{
    private Renderer mobRenderer;
    private Color originalEmissionColor;
    public Color highlightEmissionColor = Color.yellow; // Customize the color as needed
    public float emissionIntensity = 1.5f; // Adjust this for the glow strength

    void Start()
    {
        mobRenderer = GetComponent<Renderer>();

        // Store the original emission color; assume it’s black if emission isn’t used initially
        originalEmissionColor = mobRenderer.material.GetColor("_EmissionColor");
    }

    void Update()
    {
        HandleMouseHover();
    }

    private void HandleMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object is this mob
            if (hit.collider.gameObject == gameObject)
            {
                ApplyHighlight();
            }
            else
            {
                RemoveHighlight();
            }
        }
        else
        {
            RemoveHighlight();
        }
    }

    private void ApplyHighlight()
    {
        // Set the emission color and intensity
        mobRenderer.material.SetColor("_EmissionColor", highlightEmissionColor * emissionIntensity);
        mobRenderer.material.EnableKeyword("_EMISSION");
    }

    private void RemoveHighlight()
    {
        // Reset the emission color to the original
        mobRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
        mobRenderer.material.DisableKeyword("_EMISSION"); // Optionally disable emission when not highlighted
    }
}
