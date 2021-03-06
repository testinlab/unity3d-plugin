TestinAgent-SDK Unity插件支持
============

TestinAgent-SDK插件for Unity3D引擎，是C＃代码到Native代码（Java/ObjectiveC）的桥梁，方便在C＃调用TTestinAgent-SDK，用于传递自定义用户信息、场景、错误等；实现对C#脚本的错误收集

**注意：在使用本插件前，须确保Testin的崩溃分析SDK已加入到工程中。**

**要求的崩溃分析SDK版本：Android/iOS 2.0.0及以上版本**


## 添加SDK到你的工程
-----------
添加Android NDK版本到指定目录  
拷贝Testin-unity-plugin/Plugins目录下内容到Unity3d-prjject/Assets/Plugins/下
备注：iOS应用直接用Unity3d导出iOS工程，在工程中直接添加SDK中的Freamwork。  

## 初化始TestinAgent
-----------
方法一：  
在/Assets/Plugins/Testin_Plugins/TestinAgentHelperConfig.cs文件中修改appKey和appChannel以及各种开关，然后将TestinInit脚本绑定到任意一个GameObject上；  
方法二：  
在/Assets/Plugins/Testin_Plugins/TestinAgentHelperConfig.cs文件中修改appKey和appChannel以及各种开关，然后在任意脚本里面添加初始化代码：
        TestinAgentHelper.Init ();

**注意：只有在TestinAgentHelper成功初始化之后，Unity中发生异常，SDK才能成功捕获，对于初始化之前发生的异常不能捕获。因此，建议程序启动后，尽可能提前初始化TestinAgentHelper。**

## 处理收集的异常
-----------
开发者可以自己捕获异常然后进行上报。  
方法定义：  
        
       public static void LogHandledException(Exception error);  

示例代码如下：  
        
        try {  
                throw new System.Exception();  
        } catch (System.Exception error) {  
                TestinAgentHelper.LogHandledException(error);  
        }
		
## 设置用户信息
-----------
开发者可以设置用户信息。  
方法定义：  
        
        public static void SetUserInfo(string userInfo);  

示例代码如下：  
        
        TestinAgentHelper.SetUserInfo("userInfo");  

## 设置面包屑
-----------
开发者可以通过设置面包屑，来准确重现crash或者异常发生时用户的活动轨迹。  
方法定义：  
        
        public static void leaveBreadcrumb (string breadcrumb);  

示例代码如下：  

        TestinAgentHelper.leaveBreadcrumb("click button");  

## 设置本地调试
-----------
开发者可以开启或者关闭本地调试功能（仅限Android平台），如果开启改功能，开发者可以通过Logcat查看崩溃大师SDK所捕获的异常和Crash信息。  
方法定义：  

        public static void setLocalDebug(bool isDebug);  

示例代码如下：  

        TestinAgentHelper.setLocalDebug(true);  
