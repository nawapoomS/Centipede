using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{    
    public string url = "http://localhost/UnityBackend/";
    public InputField usernameInput;                                                  // Receive Name from user    
    public Text info;                                                                 // Show information text      
        
    private string userId;
    private string currentUsername;
    private string uKey = "accountusername";  
 
    void Start(){      
        //StartCoroutine(GetUserId());
        currentUsername = "";  

        if(PlayerPrefs.HasKey(uKey)){
            if(PlayerPrefs.GetString(uKey) != ""){
                currentUsername = PlayerPrefs.GetString(uKey);
                //info.text = "You are logged in = " + currentUsername;
            }else{
                info.text = "You are not logged in.";
            }
        }else{
            info.text = "You are not logged in.";
        }
    }

    // Call CreateNewUser to register new username
    public void RegisterUser(){
        string username = usernameInput.text;        
        StartCoroutine(RegisterUser(username));
    }

    // Call LogInUser to login to the game
    public void LoginUser(){
        string username = usernameInput.text;        
        StartCoroutine(LogInUser(username));
    }

    // Create new account to database
    IEnumerator RegisterUser(string username){
        WWWForm form = new WWWForm();        
        form.AddField("newAccountUsername", username);
        //form.AddField("newAccountUserid", userId);        
        using (UnityWebRequest www = UnityWebRequest.Post(url, form)){
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
  
            if (www.isNetworkError){
                Debug.Log(www.error);
            }
            else{
                string responseText = www.downloadHandler.text;
                Debug.Log("Response = " + responseText);
                info.text = "Response = " + responseText;
                Debug.Log("RegisterUser Successful");
                SceneManager.LoadSceneAsync("Menu");
            }
        }
    } 

    // User login
    IEnumerator LogInUser(string username){
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", username);        
        using (UnityWebRequest www = UnityWebRequest.Post(url, form)){
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
  
            if (www.isNetworkError){
                Debug.Log(www.error);
            }
            else{
                string responseText = www.downloadHandler.text;
                if(responseText.StartsWith("1")){
                    PlayerPrefs.SetString(uKey, username);
                    Debug.Log("Response = " + responseText);
                    info.text = "Login Success "+ username;
                    SceneManager.LoadSceneAsync("GameMap");
                }else{
                    info.text = "User does not exist.";
                }
            }
        }
    } 
  
}
