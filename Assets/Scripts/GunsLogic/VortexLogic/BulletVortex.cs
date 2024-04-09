using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVortex : MonoBehaviour
{
    private VortexGun gun;
    private float attractionForce;
    private float vortexDuration;
    private float vortexRadius;
    private Color vortexColor;
    private float destroyTime;
    private bool isVortexActive = false;
    private Vector3 vortexPosition;
    private GameObject vortexSphere;
    private const string vortexAffectedTag = "Vortex";

    public void Initialize(float attractionForce, float vortexDuration, float vortexRadius, Color vortexColor)
    {
        this.attractionForce = attractionForce;
        this.vortexDuration = vortexDuration;
        this.vortexRadius = vortexRadius;
        this.vortexColor = vortexColor;
    }

    private void Start()
    {
        destroyTime = Time.time + vortexDuration;
    }

    private void Update()
    {
        if (isVortexActive)
        {
            AttractObjects();
        }

        if (Time.time >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isVortexActive && collision.gameObject.CompareTag(vortexAffectedTag))
        {
            vortexPosition = collision.contacts[0].point;
            isVortexActive = true;
            ActivateVortexSphere();
        }
    }

    private void AttractObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(vortexPosition, vortexRadius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 directionToCenter = (vortexPosition - col.transform.position).normalized;
                rb.AddForce(directionToCenter * attractionForce, ForceMode.Impulse);
            }
        }
    }

    private void ActivateVortexSphere()
    {
        vortexSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        vortexSphere.transform.position = transform.position;
        vortexSphere.transform.localScale = new Vector3(vortexRadius * 2, vortexRadius * 2, vortexRadius * 2);
        Destroy(vortexSphere.GetComponent<Collider>());
        Destroy(vortexSphere, vortexDuration);

        Renderer sphereRenderer = vortexSphere.GetComponent<Renderer>();
        if (sphereRenderer != null)
        {
            Material material = new Material(Shader.Find("Standard"));
            vortexColor.a = 0.3f;
            material.color = vortexColor;
            material.SetFloat("_Mode", 3);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            sphereRenderer.material = material;
        }
    }

    private void OnDrawGizmos()
    {
        if (isVortexActive)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(vortexPosition, vortexRadius);
        }
    }
}
