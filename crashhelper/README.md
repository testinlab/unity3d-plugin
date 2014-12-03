崩溃收集支持
============

崩溃收集支持插件for Unity3D引擎，是C＃代码到Native代码（Java/ObjectiveC）的桥梁，方便在C＃调用Testin崩溃分析SDK，用于传递自定义用户信息、场景、错误等；实现对C#脚本的错误收集

注意：在使用本插件前，须确保Testin的崩溃分析SDK已加入到工程中。

要求的崩溃分析SDK版本：[Android/iOS](http://crash.testin.cn/help/doc/13) 


## 添加SDK到你的工程
-----------
复制以下文件到相应的目录中：   
Unity3D-plugin/crashhelper/Plugins/Testin_Plugins/* —> Assets/Plugins/Testin_Plugins/

## 初化始TestinAgent
-----------
方法一：  
在/Assets/Plugins/Testin_Plugins/TestinInit.cs文件中修改TestinAppKey和TestinChannel，然后将TestinInit脚本绑定到任意一个GameObject上；  
方法二：  
在任意脚本里面添加初始化代码：
        TestinCrashHelper.Init (TestinAppKey, TestinChannel);//TestinAppKey为你应用的appKey，TestinChannel为渠道号。

注意：只有在TestinCrashHelper成功初始化之后，Unity中发生异常，SDK才能成功捕获，对于初始化之前发生的异常不能捕获。因此，建议程序启动后，尽可能提前初始化TestinCrashHelper。

## 处理收集的异常
-----------
开发者可以自己捕获异常然后进行上报。  
示例代码如下：  
        try {  
                throw new System.Exception();  
        } catch (System.Exception error) {  
                TestinCrashHelper.LogHandledException(error);  
        }
		
## 设置用户信息
-----------
开发者可以设置用户信息。  
示例代码如下：  
        TestinCrashHelper.SetUserInfo("userInfo");

