using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceController : MonoBehaviour
{
    // Lista pública de GameObjects. ¡Deben estar deshabilitados inicialmente!
    public List<GameObject> objetosSecuencia = new List<GameObject>();

    // Variable pública para el tiempo de espera después de la colisión
    public float tiempoEsperaEntreObjetos = 2f;

    // Índice para saber qué objeto debe activarse a continuación
    private int indiceActual = -1;

    private void Start()
    {
        // 1. Inicializa cada objeto con una referencia a este controlador
        foreach (GameObject obj in objetosSecuencia)
        {
            SequentialObject seqObj = obj.GetComponent<SequentialObject>();
            if (seqObj != null)
            {
                seqObj.SetController(this);
            }
            else
            {
                Debug.LogError("El objeto " + obj.name + " no tiene el script SequentialObject.");
            }
        }

        // 2. Inicia la secuencia habilitando el primer objeto
        AvanzarASiguienteObjeto();
    }

    // Método principal para habilitar el siguiente objeto en la secuencia
    private void AvanzarASiguienteObjeto()
    {
        indiceActual++;

        // Verifica si hemos pasado por todos los objetos
        if (indiceActual < objetosSecuencia.Count)
        {
            GameObject siguienteObjeto = objetosSecuencia[indiceActual];

            Debug.Log("Habilitando objeto: " + siguienteObjeto.name);

            // Habilitar el siguiente objeto
            siguienteObjeto.SetActive(true);
        }
        else
        {
            Debug.Log("¡Secuencia completada! Todos los objetos han colisionado con el Tomate.");
        }
    }

    // Público: Llamado por SequentialObject cuando ocurre la colisión y se deshabilita
    public void ObjetoDeshabilitado()
    {
        Debug.Log("Colisión detectada. Esperando " + tiempoEsperaEntreObjetos + " segundos...");

        // Iniciar la Coroutine para el tiempo de espera y el avance
        StartCoroutine(EsperarYAvanzar());
    }

    // Coroutine para manejar el tiempo de espera de 2 segundos
    IEnumerator EsperarYAvanzar()
    {
        // Esperar el tiempo definido en el Inspector
        yield return new WaitForSeconds(tiempoEsperaEntreObjetos);

        // Habilitar el siguiente objeto de la lista
        AvanzarASiguienteObjeto();
    }
}