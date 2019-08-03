using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TeleportGun : MonoBehaviour
{
    private Gradient gradient;
    private CharacterController m_CharacterController;

    public bool DrawLines = true;

    private void Awake()
    {
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;

        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                DrawLine(this.transform.position, hit.point);

                // TODO: we want to actually step the ray back from the hit point,
                // as this spawns the player 'closer' to what is hit than where they'd normally collide
                m_CharacterController.enabled = false;
                this.transform.position = hit.point;
                m_CharacterController.enabled = true;
            }
        }
    }

    private void DrawLine(Vector3 pointOne, Vector3 pointTwo)
    {
        if (!DrawLines) return;

        GameObject newLine = new GameObject();
        LineRenderer lineRenderer = newLine.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;

        lineRenderer.colorGradient = gradient;

        lineRenderer.SetPosition(0, pointOne);
        lineRenderer.SetPosition(1, pointTwo);
    }
}
