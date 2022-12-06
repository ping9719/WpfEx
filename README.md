# Ping9719.WpfEx

### WPF扩展库，在 HandyControl 的基础上。
##### WPF extension library, based on HandyControl.
#

### 文档：[Document：]
### 本地[Local]：\doc\WpfExDoc.docx
### 在线[on-line]：https://view.officeapps.live.com/op/view.aspx?src=https://github.com/ping9719/WpfEx/blob/main/doc/WpfExDoc.docx?raw=true
#

### 未来计划 [plan]
#### 控件
#遮罩加载控件<br/>
#适用于瀑布流分页控件<br/>
#适用与设备的启动与停止（或可选暂停、继续）控件<br/>
#适用与设备的三色状态控件<br/>
#对HandyControl的基础选项卡美化
#### 窗体
#启动加载窗体<br/>
#登录窗体<br/>
#出现异常友好提示窗体（重试，终止）<br/>
#### 主题
#待定<br/>

### 下载包 [download、install]
```CSharp
Install-Package Ping9719.WpfEx
```
#

### 列子:[ensample code:]
```CSharp
//
```
#

### 快速开始 [How To Use]
##### 一、在App.xaml中添加以下代码 [Add to App.xaml]
```xml
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!--Ping9719.WpfEx-->
                <ResourceDictionary Source="pack://application:,,,/Ping9719.WpfEx;component/Themes/Theme.xaml"/>
                <!--HandyControl-->
                <ResourceDictionary Source="pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml"/>
                <ResourceDictionary Source="pack://application:,,,/HandyControl;component/Themes/Theme.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
```
##### 二、添加命名空间 [namespace]
```CSharp
xmlns:pi="https://github.com/ping9719/wpfex"
```

