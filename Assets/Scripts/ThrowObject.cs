using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Animator animator;
    public GameObject projectilePrefab; // Prefab del proyectil (Tomate)
    public Transform throwPoint;       // ¡El Empty Object para la posición de instanciación!

    [Header("Ajustes de Impulso")]
    public float forwardForce = 15f; // Fuerza en la dirección de la cámara
    public float verticalLift = 3f;  // Fuerza hacia arriba (para el arco)

    // Referencia a la cámara principal
    private Camera mainCamera;

    void Start()
    {
        // Encontrar la cámara principal al inicio
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No se encontró la Cámara Principal (Camera.main). Asegúrate de que tu cámara tenga el tag 'MainCamera'.", this);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo del ratón
        {
            if (animator != null)
            {
                animator.SetTrigger("Throw");
            }

            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        if (projectilePrefab == null || mainCamera == null)
        {
            return;
        }

        if (throwPoint == null)
        {
            Debug.LogError("El ThrowPoint no está asignado. ¡Arrastra un Empty Object al inspector!", this);
            return;
        }

        // 1. Instanciar el proyectil en la posición y rotación del ThrowPoint
        // La rotación se mantiene para que el proyectil pueda tener una orientación inicial
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);

        // 2. Obtener el Rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // 3. Calcular la dirección y el impulso

            // Vector Principal: Usa la dirección "adelante" de la CÁMARA.
            Vector3 cameraForwardImpulse = mainCamera.transform.forward * forwardForce;

            // Vector Vertical: Añade la elevación.
            Vector3 upwardImpulse = Vector3.up * verticalLift; // Vector3.up es el eje Y global.

            // 4. Aplicar la fuerza combinada como un impulso
            rb.AddForce(cameraForwardImpulse + upwardImpulse, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("El prefab del proyectil NO tiene un Rigidbody adjunto. La física no funcionará.", projectile);
        }
    }
}