# lva

## hostcs

### setup

`hostcs.csproj` 要有以下几个配置

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    ...
    <PublishTrimmed>true</PublishTrimmed>
    <PublishReadyToRun>true</PublishReadyToRun>
    <PublishSingleFile>true</PublishSingleFile>
    <RuntimeIdentifier>linux-x64</RuntimeIdentifier>
  </PropertyGroup>

</Project>
```

## lua

### setup

`build.sh` 已经处理过自带的 Makefile 以便生成 so
