using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Agrega el espacio de nombres necesario

public class MenuInicio : MonoBehaviour
{
    // No es necesario definir Start() y Update() si no los est�s utilizando

    // Este m�todo cambiar� de escena cuando se llame desde un bot�n
    public void CambiarScena(string nombre)
    {
        SceneManager.LoadScene(nombre); // El nombre del m�todo es "LoadScene", no "Manager"
    }
}
