using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;

    public ApiManager apiManager;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    void OnLoginButtonClick()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        Debug.Log(email);
        StartCoroutine(apiManager.AuthenticateAndGetData(email, password));
    }
}
