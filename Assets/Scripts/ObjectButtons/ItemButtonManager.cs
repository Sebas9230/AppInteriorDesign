using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    //private Sprite itemImage;
    private GameObject item3DModel;
    private string urlBundleModel;
    private RawImage imageBundle;

    public SaverManager sceneSaver;

    //asignar valores
    public string ItemName{
        set
        {
            itemName = value;
        }
    }
    public string ItemDescription { set => itemDescription = value; }
    //public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DModel { set => item3DModel = value; }
    public string URLBundleModel { set => urlBundleModel = value; }
    public RawImage ImageBundle { get => imageBundle; set => imageBundle = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        //transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        imageBundle = transform.GetChild(1).GetComponent<RawImage>();
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;
        
        var button = GetComponent<Button>();
        button.onClick.AddListener(MySceneManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);

        if (sceneSaver == null)
        {
            Debug.LogError("SceneSaver is not assigned!");
        }
    }

    private void Create3DModel()
    {
        //Instantiate(item3DModel);//creación Modelo 3D
        //string uniqueID = UniqueIDGenerator.GenerateID(); //generar ID
        //Debug.Log("ID: "+uniqueID);
        StartCoroutine(DownloadAssetBundle(urlBundleModel));
    }

    //nuevo método para creación del modelo 3D
    IEnumerator DownloadAssetBundle(string urlAssetBundle)
    {
          UnityWebRequest serverRequest = UnityWebRequestAssetBundle.GetAssetBundle(urlAssetBundle);
          yield return serverRequest.SendWebRequest();
          if (serverRequest.result == UnityWebRequest.Result.Success)
          {
            AssetBundle model3D = DownloadHandlerAssetBundle.GetContent(serverRequest);
            if (model3D != null)
            {
                //Item3DModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject); //esto se haría porque en un asset bundle se pueden empaquetar diferentes elemenetos
                GameObject model = model3D.LoadAsset<GameObject>(model3D.GetAllAssetNames()[0]);
                item3DModel = Instantiate(model);

                if (sceneSaver != null) {
                    sceneSaver.RegisterInstantiatedObject(item3DModel);// Registrar el objeto en SceneSaver
                } else {
                    Debug.LogError("SceneSaver is not initialized.");
                }
                
                model3D.Unload(false);
            }else{
                Debug.Log("Asset Bundle not valid");
            }
          }else{
            Debug.Log("Error while requesting for the asset bundles");
          }
    }
}