App42_Push_Sample_Unity
=======================

# About Application

1. This application shows how can we integrate our Unity3D game with PushNotification using android Framework.
2. Once a game registered for PushNotification Unity3D game get Notification message accordingly.

# Running Sample

This is a sample Android Unity 3D app is made by using App42 backend platform. It uses Push Notification of App42 platform.
Here are the few easy steps to run this sample app.

1. [Register] (https://apphq.shephertz.com/register) with App42 platform.
2. Create an app once you are on Quick start page after registration.
3. If you are already registered, login to [AppHQ] (http://apphq.shephertz.com) console and create an app from App Manager Tab.
4. To use push Notification service in your application go to https://code.google.com/apis/console a new project here.
5. Click on services option in Google console and enable Google Cloud Messaging for Android service.
6. Click on API Access tab and create a new server key for your application with blank server information.
7. Go to [AppHQ] (http://apphq.shephertz.com) console and click on Push Notification and select Android setting in Settings option.
8. Select your app and copy server key that is generated by using Google API console, and submit it.
9. Download the Unity project from [here] (https://github.com/shephertz/App42_Push_Sample_Unity/archive/master.zip) and open the PushSample.unity from your assets folder , it contains PushSample.cs & Constants.cs.
10. Open Constants.cs file in sample app and make these changes.

```
A. Change apiKey and secretKey that you have received in step 2 or 3.
B. Change projectNo with your Google Project Number.
C. Change gameObjectName with your GameObject on which you have to receive notfication callBack from Android.
D. Change callBackMethod with your method name on which you have to receive notification callBack from Android e.g. Success.
```
11.Build your android apk.

__Test and verify PushNotification__
```
A. After registering for PushNotification go to AppHQ console and click on Push Notification and select application after selecting User tab.
B. Select desired user from registered UserList and click on Send Message Button.
C. Send appropriate message to user by clicking Send Button.
D. Now you will get same message on your android device and your callBack Method of Unity3D.
```

__System Requirements:__
```
A. Unity 3D.
B. Android SDK with 4.0 API .
```


# Design Details:

__Push Registration:__ To use Notification message in your game you have to register your game for PushNotification 
by calling this method in your main cs file. If you have change the package name by building your own app42pushservice.jar
than make following change in this method.

A. com.shephertz.app42.android.pushservice must be replaced by "YOUR PACAKGE NAME" (if using customize jar file)

```
public void RegisterForPush(){
		 object[] args1 = new object[]{constants.projectNo};
		 object[] args0 = new object[]{constants.apiKey,constants.secretKey};
		 object[] args2= new object[]{constants.userId};
          object[] args3 = new object[]{constants.callBackMethod,constants.gameObjectName};
		     if (testobj == null) {
          using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            
		
		     using (var pluginClass = new AndroidJavaClass("com.shephertz.app42.android.pushservice.App42Service")) {
                if (pluginClass != null) {
                    testobj = pluginClass.CallStatic<AndroidJavaObject>("instance",playerActivityContext);
					testobj.Call("intialize",args0);
				    testobj.Call("setProjectNo",args1);
				    testobj.Call("setCurrentUser",args2);
                    testobj.Call("registerForNotification",args3);
                }
            }
	 }
      }
    }
```

__AndroidManifest Changes:__ To use Notification message in your game you have to make following changes in AndroidManifest.xml file.

1. Add Your Launcher Activty in AndroidManifest.xml file.</br>
2. Change "com.GoLiveGaming.HumansAndDemons.MainActivity" with your Activity on which you want to navigate when PushNotification is clicked by user. 

```
  <meta-data android:name="onMessageOpen" android:value="com.GoLiveGaming.HumansAndDemons.MainActivity" />
```

