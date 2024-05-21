using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;

    //asignar valores
    public string ItemName{
        set
        {
            itemName = value;
        }
    }
    public string ItemDescription { set => itemDescription = value; }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DModel { set => item3DModel = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;
        
        var button = GetComponent<Button>();
        button.onClick.AddListener(MySceneManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);
    }

    private void Create3DModel()
    {
        Instantiate(item3DModel);
    }
}
