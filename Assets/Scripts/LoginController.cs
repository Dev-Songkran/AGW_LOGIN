using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginController : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject progressCircle;
    public GameObject loginButtonText;
    public GameObject errorText;
    public Button loginButton;
    public Button enterGameButton;
    public Button backButton;
    public Button closeButton;

    public InputField usernameInput;
    public InputField passwordInput;
    public string apiUrl;
    WWWForm form;

    public void OnShowLoginPanel()
    {
        Debug.Log("Login button clicked");
        loginPanel.SetActive(true);
    }

    public void OnCloseLoginPanel()
    {
        loginPanel.SetActive(false);
        progressCircle.SetActive(false);
        loginButtonText.SetActive(true);
        loginButton.interactable = true;
        usernameInput.text = "";
        passwordInput.text = "";
    }

    public void OnLoginButtonClicked()
    {
        loginButtonText.SetActive(false);
        progressCircle.SetActive(true);
        loginButton.interactable = false;
        errorText.SetActive(false);
        StartCoroutine(Login());
    }
    
    public void onSuccess(object data)
    {
        Debug.Log(data);
        loginPanel.SetActive(false);
        progressCircle.SetActive(false);
        loginButtonText.SetActive(true);
        loginButton.interactable = true;
        usernameInput.text = "";
        passwordInput.text = "";
        enterGameButton.interactable = true;
        backButton.interactable = true;
        closeButton.interactable = true;
        errorText.SetActive(false);
    }

    public void onError()
    {
        loginPanel.SetActive(true);
        progressCircle.SetActive(false);
        loginButtonText.SetActive(true);
        loginButton.interactable = true;
        usernameInput.text = "";
        passwordInput.text = "";
        errorText.SetActive(true);
    }
    // api call to login
    IEnumerator Login()
    {
        form = new WWWForm();
        form.AddField("username", usernameInput.text);
        form.AddField("password", passwordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl, form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                onError();
            }
            else
            {
                onSuccess(www.downloadHandler.text);
            }
        }

    }
}
