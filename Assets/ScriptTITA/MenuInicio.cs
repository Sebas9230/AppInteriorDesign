using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Agrega el espacio de nombres necesario

public class MenuInicio : MonoBehaviour
{
    [SerializeField] private PrimitiveType[] primitiveTypes = { PrimitiveType.Cube, PrimitiveType.Sphere, PrimitiveType.Cylinder };

    [SerializeField] private Vector3 spawnPosition = new Vector3(1, 1, 2);
    [SerializeField] private Vector3 spawnRotation = new Vector3(45, 45, 0);
    [SerializeField] private Vector3 spawnScale = new Vector3(0.5f, 0.5f, 0.5f);

    private int currentIndex = -1;
    private bool hasBeenGenerated;
    private List<GameObject> objs = new List<GameObject>();
    public void GenerateObjects()
    {
        if (currentIndex >= 0)
        {
            return;
        }

        GameObject container = new GameObject("ObjectsContainer");
        for (int i = 0; i < primitiveTypes.Length; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(primitiveTypes[i]);
            obj.transform.parent = container.transform;
            obj.transform.position = spawnPosition;
            obj.transform.rotation = Quaternion.Euler(spawnRotation);
            obj.transform.localScale = spawnScale;
            obj.SetActive(false);
            objs.Add(obj);
        }
        currentIndex = 0;
        objs[currentIndex].SetActive(true);
        hasBeenGenerated = true;
    }

    public void CambiarScena(int numEscena)
    {
        SceneManager.LoadScene(numEscena);
    }
}
