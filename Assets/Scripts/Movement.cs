using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    // Velocidad de movimiento del jugador, ajustable en el Inspector
    public float moveSpeed = 10f;

    // Controla si el jugador puede moverse. Si es 'false', el método Update no hace nada.
    public bool isMoveable = true;

    void Update()
    {
        // 0. Comprobación de Movilidad (Guard Clause)
        // Si 'isMoveable' es false, salimos de la función inmediatamente.
        if (!isMoveable) return;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }
        // 1. Obtener la entrada del usuario para los ejes de movimiento
        // El eje "Horizontal" corresponde a A y D
        float x = Input.GetAxis("Horizontal");
        // El eje "Vertical" corresponde a W y S
        float z = Input.GetAxis("Vertical");

        // 2. Crear un vector de movimiento
        // Vector3(x, 0, z) crea el vector en el plano horizontal (x, z)
        Vector3 move = new Vector3(x, 0f, z);

        // 3. Normalizar el vector de movimiento
        // Esto previene que moverse en diagonal (W+A, por ejemplo) sea más rápido que moverse hacia adelante.
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        // 4. Aplicar la velocidad y el tiempo para que el movimiento sea fluido
        // Se multiplica por la velocidad y Time.deltaTime para que sea independiente de la tasa de frames
        move = move * moveSpeed * Time.deltaTime;

        // 5. Mover el cuerpo del jugador
        // 'transform.Translate' mueve el objeto relativo a su propia orientación (local space).
        // Esto es crucial para que el jugador avance en la dirección en la que está mirando.
        transform.Translate(move);
    }
}