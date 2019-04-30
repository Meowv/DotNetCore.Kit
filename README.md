# ```DotNetCore.Kit```

### ```.NET Core``` 开发工具包扩展方法大全

#### 注：很多方法来源于网络，将其改为 Extension，虽已自测但还是酌情使用远离996。

#### Package Manager
```PM> Install-Package DotNetCore.Kit```

#### .NET CLI
```> dotnet add package DotNetCore.Kit```

#### PackageReference
```<PackageReference Include="DotNetCore.Kit" />```

#### Paket CLI
```> paket add DotNetCore.Kit```

#### Versions
##### v1.0.4
- 添加了一系列有关Collection的扩展方法

##### v1.0.3
- 修复v1.0.2存在的bug
- 添加Nuget的PackageTags

##### v1.0.2
- 添加以下扩展方法 ↓↓↓
- GetArrayValue:通过索引获取数组元素
- WhereFilter:通过指定表达式过滤集合
- ConvertToList:把LinqGroupBy结果转换成指定的集合
- DistinctByData:根据集合中指定的列过滤重复,并返回指定的列
- DistinctBy:根据集合中指定的列过滤重复
- ToDataTable:集合转DataTable
- GenerateTree:列表生成树形节点
- ArrayToString:把数组转为split分割后连接的字符串
- ArrayToString:把数组转为逗号连接的字符串
- ToDescripttion:针对Enum类型添加扩展方法，并使用反射读取当前枚举值所对应的显示值

##### v1.0.1
- 完善v1.0.0，又添加了很多嫌语法糖不够甜的新扩展

##### v1.0.0
- 在工作中，有很多帮助类，每次都要重复去写或者copy到新项目中，于是就有了```DotNetCore.Kit```的第一个版本

----

# ```DotNetCore.Kit.Captcha```

### ```.NET Core``` 生成图片验证码

#### Package Manager
```PM> Install-Package DotNetCore.Kit.Captcha```

#### .NET CLI
```> dotnet add package DotNetCore.Kit.Captcha```

#### PackageReference
```<PackageReference Include="DotNetCore.Kit.Captcha" />```

#### Paket CLI
```> paket add DotNetCore.Kit.Captcha```

#### Versions
##### v1.0.0
- 基于 System.Drawing.Common 生成图片验证码
- 可以自定义验证码长度和验证码图片宽高