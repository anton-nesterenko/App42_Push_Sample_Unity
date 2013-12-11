using UnityEngine;
using System.Collections;
using System.IO;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class PushSample: MonoBehaviour
{
	string message = "No Message yet";
	private AndroidJavaObject testobj = null;
	private AndroidJavaObject playerActivityContext = null;
	private string userName = "pintu";
	private string myMsg = "Hi I am using Push App42";
	public const string UnityRegistrationMethod = "StoreDeviceId";
	public string app42Response="";
	
	
	
#if UNITY_ANDROID
	void OnGUI()
    {
        GUI.Label(new Rect (5, 10, 300, 50), message);
		app42Response=Callback.response;
		app42Response = GUI.TextField (new Rect (5, 60, 300, 80),app42Response);
		
		userName = GUI.TextField (new Rect (5, 150, 300, 30),userName);
		myMsg = GUI.TextField (new Rect (5, 190, 300, 40),myMsg);
			
		 if (GUI.Button(new Rect (5, 240, 300, 50), "Send Push To User"))
        {
			sendPushToUser(userName,myMsg);
		}
		 if (GUI.Button(new Rect (5, 300, 300, 50), "Send Push To All"))
        {
			sendPushToAll(myMsg);
		}
		 if (GUI.Button(new Rect(5, 360, 300, 50), "Exit game"))
        {
			 Application.Quit();
		}
	}
	public static bool Validator (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}
	public void sendPushToUser(string userName,string msg){
	
		App42API.BuildPushNotificationService().SendPushMessageToUser(userName,msg,new Callback());
		
	}
	
	public void sendPushToAll(string msg){
		
		  	App42API.BuildPushNotificationService().SendPushMessageToAll(msg,new Callback());
	     
	}
	
	void Start (){
		ServicePointManager.ServerCertificateValidationCallback = Validator;
		    GameObject androidtest = GameObject.Find(Constants.GameObjectName);
			DontDestroyOnLoad(transform.gameObject);
			this.gameObject.name = Constants.GameObjectName;
		    App42API.Initialize(Constants.ApiKey,Constants.SecretKey);
	    	App42API.SetLoggedInUser(Constants.UserId);
		    RegisterForPush();
	}
	
	
	
	public void RegisterForPush(){
		 object[] googleProjectNo = new object[]{Constants.GoogleProjectNo};
          object[] unityParam = new object[]{Constants.CallBackMethod,Constants.GameObjectName,UnityRegistrationMethod};
		     if (testobj == null) {
          using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            
		     using (var pluginClass = new AndroidJavaClass("com.shephertz.app42.android.pushservice.App42Service")) {
                if (pluginClass != null) {
                    testobj = pluginClass.CallStatic<AndroidJavaObject>("instance",playerActivityContext);
				    testobj.Call("setProjectNo",googleProjectNo);
                    testobj.Call("registerForNotification",unityParam);
                }
          	  }
            }
	     }
	
	}
	/*
	 * This Function calls from Native Android Code to store Device Id for Push Notification.
	 * @param deviceId require to register for Push Notification on App42 API
	 * 
	 */
	public void StoreDeviceId(string deviceId) {
		  	App42API.BuildPushNotificationService().StoreDeviceToken(App42API.GetLoggedInUser(),deviceId,
			Constants.DeviceType,new Callback());
    }	
	
	/*
	 * This Function calls from Native Android Code to whenever any Push Notification receive.
	 * @param msg Push Notification message
	 * 
	 */
     public void Success(string msg) {
         if(msg != null) {
	       Debug.Log("Push Message is Here: " + msg);
	       message=msg;
          }
       }
	
#endif
}
	