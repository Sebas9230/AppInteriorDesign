using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Agrega el espacio de nombres necesario

public class MenuInicio : MonoBehaviour
{
    // No es necesario definir Start() y Update() si no los estás utilizando

    // Este método cambiará de escena cuando se llame desde un botón
    public void CambiarScena(string nombre)
    {
        SceneManager.LoadScene(nombre); // El nombre del método es "LoadScene", no "Manager"
    }
}
