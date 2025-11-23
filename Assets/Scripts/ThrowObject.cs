using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Animator animator;
    public GameObject projectilePrefab; // Prefab del proyectil (Tomate)

    [Header("Ajustes de Aparición")]
    public float spawnDistance = 5f; // Distancia delante de la cámara donde aparecerá el tomate.

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

            // Llama a la función para instanciar el proyectil
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        if (projectilePrefab == null || mainCamera == null)
        {
            return;
        }

        // 1. Calcular la posición de aparición: Posición de la cámara + dirección de la cámara * distancia
        // Esto asegura que aparece a 5 unidades (o el valor de spawnDistance) delante de donde mira la cámara.
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;

        // Instanciar el proyectil en esa posición y con la rotación de la cámara.
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, mainCamera.transform.rotation);

        // 2. Obtener el Rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Importante: Aseguramos que el Rigidbody está activo y no es cinemático
            // para que la gravedad actúe sobre él inmediatamente.
            rb.isKinematic = false;

            // Ya NO aplicamos fuerza (AddForce), solo se crea y cae.
            // La gravedad de Unity (editada en Project Settings > Physics) se encargará de esto.
        }
        else
        {
            Debug.LogError("El prefab del proyectil no tiene un Rigidbody adjunto. La gravedad no funcionará.", projectile);
        }
    }
}