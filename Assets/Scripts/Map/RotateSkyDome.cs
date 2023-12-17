using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyDome : MonoBehaviour
{
    float rotationSpeed = 1.25f; // Velocidade de rotação do objeto

    void Update()
    {
        // Girar o objeto lentamente em torno do eixo Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
