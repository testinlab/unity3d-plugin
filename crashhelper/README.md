崩溃收集支持
============

崩溃收集支持插件for Unity3D引擎，是C＃代码到Native代码（Java/ObjectiveC）的桥梁，方便在C＃调用Testin崩溃分析SDK，用于传递自定义用户信息、场景、错误等；实现对C#脚本的错误收集

注意：在使用本插件前，须确保Testin的崩溃分析SDK已加入到工程中。

要求的崩溃分析SDK版本：Android/iOS 1.7


## 添加SDK到你的工程
-----------
复制以下文件到相应的目录中：  
Unity3D-plugin/crashhelper/Plugins/Android/* —> Assets/Plugins/Android/  
Unity3D-plugin/crashhelper/Plugins/Testin_Android_Scripts/* —> Assets/Plugins/Testin_Android_Scripts/

## 修改工程的AndroidManifest文件
-----------
>确保{Unity Project}/Plugins/Android目录中存在AndroidManifiest.xml文件。在该文件中，你需要确认添加网络等权限在<manifest> ... </manifest>标签中，如下：  
<uses-permission android:name="android.permission.INTERNET"/>  
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>  
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE"/>  
<uses-permission android:name="android.permission.READ_PHONE_STATE"/>  
<uses-permission android:name="android.permission.READ_LOGS"/>  
<uses-permission android:name="android.permission.GET_TASKS"/>

## 初化始TestinAgent
-----------
在Unity3D-plugin/crashhelper/Plugins/Testin_Android_Scripts/TestinInit.cs文件中修改Appkey；  
TestinAndroid.Init (TestinAppKey); //TestinAppKey换成你程序的Appkey


## 处理收集的异常
-----------
try {  
    throw new System.Exception();  
} catch (System.Exception error) {  
    TestinAndroid.LogHandledException(error);  
}
		
## 设置用户信息
-----------
TestinAndroid.SetUserInfo("userInfo");

