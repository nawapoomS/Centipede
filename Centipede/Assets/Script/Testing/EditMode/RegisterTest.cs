using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

public class RegisterTest
{
    public string url = "http://localhost/UnityBackend/";
   

    
    /*public IEnumerator RegisterUserTest()
    {
        string username = "gtafg1234";
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
                Assert.AreEqual("Response = " + responseText);              
            }            
        }             
    }*/
    //[UnityTest]
    /*public IEnumerator RegisterUserTest(){
        var gameObject = new GameObject();
        var register = gameObject.addComponent<Register>();

        register.AuthManager(username.nawapoom);

        yield return null;

        Assert.AreEqual(expected: userid);
    }*/
}
