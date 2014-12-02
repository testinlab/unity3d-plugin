手游自动化测试插件
============

Testin支持Unity3D引擎手游测试的插件集，支持Unity3D 4.5.5f1版本游戏引擎。包含如下模块：

- 手游自动化测试插件：通过dump游戏对象信息，实现游戏对象的控件化，以实现精准的手游自动化测试
- 崩溃收集插件：C#代码到Native代码（Java/ObjectiveC）的桥梁，方便在C#调用Testin崩溃分析SDK，用于传递自定义用户信息、场景、错误等；实现对JavaScript、c#脚本的错误收集


## 下载
-----------
使用github工具，或者在命令行clone本项目到本地电脑
```bash
git clone https://github.com/testinlab/unity3d-plugin.git
```

## 拷贝代码
-----------
将下载内容中的autotest目录，复制到unity3d工程目录的Plugins目录下。

## 添加代码支持
-----------
- [手游自动化支持](autotest/README.md)
- [崩溃收集支持](crashhelper/README.md)