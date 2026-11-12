# 黑神话像素版 · iOS 移植工程

原 Android `com.DefaultCompany.heimalouxiangsutest` v0.7 / PC 版 `黑神话像素版.exe` 的 iOS 移植工程。

- 引擎：Unity **2021.3.14f1**（必须精确匹配，否则 Addressables bundle 版本号对不上）
- 后端：IL2CPP + ARM64，Graphics API 仅 Metal
- 方向：横屏
- 脚本来源：PC 版 `Assembly-CSharp.dll`（Mono 构建）经 ILSpy 8.2 反编译，536 个 `.cs`

## 目录

```
Assets/Scripts/         反编译得到的全部 C# 源码
Assets/Plugins/         第三方 DLL（DOTween / DemiLib / EPPlus / Newtonsoft）
Assets/Resources/       运行时 Resources.Load 的资源
Assets/StreamingAssets/ Addressables 资源（iOS 平台变体）
Packages/manifest.json  包依赖，锁 2021.3.14f1
ProjectSettings/        工程设置
.github/workflows/      CI：macOS runner 出未签名 ipa
```

## 构建（GitHub Actions）

1. `Acquire Unity License` 手动跑一次，下载 `.alf`，到 https://license.unity3d.com/manual 换 `.ulf`
2. 把 `.ulf` 的 base64 存进 secret `UNITY_LICENSE`，另存 `UNITY_EMAIL` / `UNITY_PASSWORD`
3. 跑 `Build iOS`，等 25~40 分钟
4. 下载 artifact `hshy-ipa`，用 Sideloadly / AltStore 自签安装

## 待办

- [ ] 第三方 DLL 迁入 Assets/Plugins
- [ ] Addressables 资源从 PC 版 bundle 还原为 iOS 变体
- [ ] Google Play Billing → StoreKit 替换
- [ ] Unity Ads 模块移除
- [ ] Input System 触摸摇杆按 iPhone 安全区重排
