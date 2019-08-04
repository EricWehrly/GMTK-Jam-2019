using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TeleportGun : MonoBehaviour
{
    public bool EnableDebugLines = true;
    public bool EnableTeleport = true;
    public string TagFilter = null;
    public Material ShadowMaterial;
    public GameObject CreepySounds;

    private Gradient gradient;
    private RigidbodyFirstPersonController m_CharacterController;

    // TODO: initial 'mesh' for player entering level ...
    public GameObject lastTarget;

    private void Awake()
    {
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;

        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        m_CharacterController = GetComponent<RigidbodyFirstPersonController>();
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

                if (ShouldTeleport(ref hit))
                {
                    SpawnShadow();

                    StealBody(hit.transform.gameObject);

                    // TODO: we want to actually step the ray back from the hit point,
                    // as this spawns the player 'closer' to what is hit than where they'd normally collide
                    TeleportTo(hit.point);
                }
            }
        }
    }

    private bool ShouldTeleport(ref RaycastHit hit)
    {
        return TagFilter != null
            && hit.transform.tag == TagFilter
            && EnableTeleport;
    }

    private void SpawnShadow()
    {
        if (lastTarget != null)
        {
            if (ShadowMaterial != null)
            {
                SkinnedMeshRenderer shadowRenderer = lastTarget.GetComponentInChildren<SkinnedMeshRenderer>();
                Material[] newMaterials = new Material[shadowRenderer.materials.Length];
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = ShadowMaterial;
                }
                shadowRenderer.materials = newMaterials;
            }

            lastTarget.gameObject.SetActive(true);
        }
    }

    private void StealBody(GameObject target)
    {
        lastTarget = target;
        target.SetActive(false);

        // we could probably do this in a different thread
        target.tag = "Untagged";
        if (target.GetComponent<FollowPlayer>() == null)
        {
            target.AddComponent<FollowPlayer>();
        }

        if (CreepySounds)
        {
            Instantiate(CreepySounds).transform.parent = target.transform;
        }
    }

    private void TeleportTo(Vector3 point)
    {
        m_CharacterController.enabled = false;
        this.transform.position = point;
        m_CharacterController.enabled = true;
    }

    private void DrawLine(Vector3 pointOne, Vector3 pointTwo)
    {
        if (!EnableDebugLines) return;

        GameObject newLine = new GameObject();
        newLine.name = "Debug Line";
        LineRenderer lineRenderer = newLine.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;

        lineRenderer.colorGradient = gradient;

        lineRenderer.SetPosition(0, pointOne);
        lineRenderer.SetPosition(1, pointTwo);
    }
}
