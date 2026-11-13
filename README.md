# 黑神话像素版 · iOS 移植工程

原游戏：`com.DefaultCompany.heimalouxiangsutest` v0.7（Android）/ 黑神话像素版.exe（PC，Unity 2021.3.14f1 + Mono）。

本仓库 = PC 版 `Assembly-CSharp.dll` 反编译出的 536 个 C# 脚本 + 重建的 Unity 工程骨架 + 两条构建流水线。

## 目录

```
Assets/Scripts/          536 个反编译 C#（原逻辑，几乎一字未改）
Assets/Plugins/          DOTween / DOTweenPro / DemiLib / EPPlus / Newtonsoft.Json
Assets/Editor/BuildScript.cs   iOS 构建入口（IL2CPP + Metal + 横屏）
Packages/manifest.json   锁 2022.3 兼容的包版本
ProjectSettings/         ProjectVersion = 2022.3.62f3
codemagic.yaml           macOS 侧只跑 xcodebuild（不需要 Unity）
.github/workflows/build-win.yml   Windows 侧跑 Unity 出 Xcode 工程
Unity_lic.ulf            ← 需要你放进来（见下）
```

## 当前唯一阻塞点：Unity 授权文件

Unity 2021.3 的 legacy 激活协议已停用，2022.3 在无 Hub 激活过的账号上会报：

```
No ULF license found. / 0 entitlement groups
```

**需要你在任意 Windows 10/11 电脑上做一次（5 分钟，只需一次）：**

1. 下载 `https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe`
2. 安装 → 用 `3484372582@qq.com` / `Aa50022620100621!` 登录
3. 齿轮 Preferences → Licenses → Add → **Get a free personal license**
4. 打开 `C:\ProgramData\Unity`，把 `Unity_lic.ulf` 拷出来
5. 把这个文件拖到本仓库根目录（或直接发给我）

放好之后 `build-win.yml` 会自动检测到 `*.ulf`，用 `-manualLicenseFile` 激活，不需要账号密码。

## 构建流水线

### 第一步：Windows runner 出 Xcode 工程

`build-win.yml`（GitHub Actions，windows-latest）：

1. 装 Unity 2022.3.62f3 + iOS Build Support（有缓存）
2. 检测仓库里的 `*.ulf` → `-manualLicenseFile` 激活
3. `-executeMethod BuildScript.BuildIOS` 构建 iOS
4. 产出 `xcode-project.zip`（含 `Unity-iPhone.xcodeproj`）

### 第二步：Codemagic 出 ipa

`codemagic.yaml` 的 `unity-ios` workflow，`mac_mini_m2`：

1. 装 Unity（若需重编）
2. `xcodebuild archive` 出未签名 ipa
3. artifact：`hshy-unsigned.ipa`

### 第三步：自签装机

Sideloadly / AltStore + Apple ID，7 天有效期。

## 待办

- [ ] `Unity_lic.ulf` 放进仓库
- [ ] Addressables 资源从 PC 版 bundle 还原（Assets 目前只有脚本，场景为空）
- [ ] Google Play Billing → StoreKit 替换
- [ ] Unity Ads 模块移除
- [ ] Input System 触摸摇杆按 iPhone 安全区重排
