using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    // Velocidad de rotación
    public float rotationSpeed = 10f;

    // Update es llamado una vez por frame
    void Update()
    {
        // Rota la cámara alrededor del eje Y a una velocidad constante
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
