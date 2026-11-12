using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lunch : MonoBehaviour
{
	public bool _m开启Debug;

	public static bool _开启Debug;

	public Slider _Slider_进度;

	public Text _Text_进度;

	public int _测试帧率 = 60;

	public bool 开启垂直同步 = true;

	public bool _m开启测试入口;

	public static bool _开启测试入口;

	public bool _m开启面板;

	public static bool _开启面板;

	public bool _mPCUI;

	public static bool _PCUI;

	public bool _mbiligame;

	public static bool _biligame;

	public bool _m开启旧升级_等级数值;

	public static bool _开启旧升级_等级数值;

	public bool _m开启新升级系统 = true;

	public static bool _开启新升级系统;

	public bool _m在没有修行点的时候隐藏按钮 = true;

	public static bool _在没有修行点的时候隐藏按钮;

	public bool _m关闭多余UI = true;

	public static bool _关闭多余UI;

	public Enum_Enemy _mEnemy_再入轮回Boss;

	public static Enum_Enemy _Enemy_再入轮回Boss;

	public float _m镜头默认大小 = 9f;

	public static float _镜头默认大小;

	[Header("目前只偏上下")]
	[Header("Y偏移是3可不能乱改,所有图都会被影响估计")]
	public Vector2 _m镜头偏移 = Vector2.zero;

	public static Vector2 _镜头偏移;

	private void Awake()
	{
		Physics2D.queriesHitTriggers = false;
		if (_m开启Debug)
		{
			_开启Debug = true;
			Debug.unityLogger.logEnabled = true;
		}
		else
		{
			_开启Debug = false;
			Debug.unityLogger.logEnabled = false;
		}
		Debug.unityLogger.logEnabled = false;
		Application.targetFrameRate = 120;
		if (开启垂直同步)
		{
			QualitySettings.vSyncCount = 1;
		}
		_开启测试入口 = _m开启测试入口;
		_开启面板 = _m开启面板;
		_PCUI = _mPCUI;
		_biligame = _mbiligame;
		_开启旧升级_等级数值 = _m开启旧升级_等级数值;
		_开启新升级系统 = _m开启新升级系统;
		_在没有修行点的时候隐藏按钮 = _m在没有修行点的时候隐藏按钮;
		_关闭多余UI = _m关闭多余UI;
		_Enemy_再入轮回Boss = _mEnemy_再入轮回Boss;
		_镜头默认大小 = _m镜头默认大小;
		_镜头偏移 = _m镜头偏移;
	}

	private IEnumerator Start()
	{
		_Slider_进度.value = 0f;
		yield return new WaitForEndOfFrame();
		_Slider_进度.DOValue(0.3f, 2f);
		DSB_MonoSingleton<MyDataManager>.Instance.Init();
		yield return new WaitForEndOfFrame();
		AsyncOperationHandle<SOData_Player> handle_SOData_Player = Addressables.LoadAssetAsync<SOData_Player>("Assets/_05_Data/Resources_moved/SOData/SOData_Player.asset");
		while (!handle_SOData_Player.IsDone)
		{
			_Text_进度.text = "吗楼正在操练...";
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player = handle_SOData_Player.Result;
		AsyncOperationHandle<SOData_PlayerLevel> handle_SOData_PlayerLevel = Addressables.LoadAssetAsync<SOData_PlayerLevel>("Assets/_05_Data/Resources_moved/SOData/SOData_PlayerLevel.asset");
		while (!handle_SOData_PlayerLevel.IsDone)
		{
			_Text_进度.text = "吗楼正在升级...";
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_PlayerLevel = handle_SOData_PlayerLevel.Result;
		AsyncOperationHandle<SOData_PlayerOther> handle_SOData_PlayerOther = Addressables.LoadAssetAsync<SOData_PlayerOther>("Assets/_05_Data/Resources_moved/SOData/SOData_PlayerOther.asset");
		while (!handle_SOData_PlayerOther.IsDone)
		{
			_Text_进度.text = "广智在操练猴子猴孙...";
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_PlayerOther = handle_SOData_PlayerOther.Result;
		AsyncOperationHandle<SOData_Enemy> handle_SOData_Enemy = Addressables.LoadAssetAsync<SOData_Enemy>("Assets/_05_Data/Resources_moved/SOData/SOData_Enemy.asset");
		while (!handle_SOData_Enemy.IsDone)
		{
			_Text_进度.text = "二郎神真正在练兵...";
			yield return new WaitForEndOfFrame();
		}
		_Slider_进度.DOKill();
		_Slider_进度.DOValue(0.4f, 0.2f);
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy = handle_SOData_Enemy.Result;
		AsyncOperationHandle<SOData_对话> handle_SOData_对话 = Addressables.LoadAssetAsync<SOData_对话>("Assets/_05_Data/Resources_moved/SOData/SOData_对话.asset");
		while (!handle_SOData_对话.IsDone)
		{
			_Text_进度.text = "剧本编撰中...";
			yield return new WaitForEndOfFrame();
		}
		_Slider_进度.DOValue(0.5f, 0.2f);
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_对话 = handle_SOData_对话.Result;
		AsyncOperationHandle<SOData_Map> handle_SOData_Map = Addressables.LoadAssetAsync<SOData_Map>("Assets/_05_Data/Resources_moved/SOData/SOData_Map.asset");
		while (!handle_SOData_Map.IsDone)
		{
			_Text_进度.text = "开天辟地...";
			yield return new WaitForEndOfFrame();
		}
		_Slider_进度.DOValue(0.6f, 0.2f);
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_Map = handle_SOData_Map.Result;
		AsyncOperationHandle<SOData_Prop> handle_SOData_Prop = Addressables.LoadAssetAsync<SOData_Prop>("Assets/_05_Data/Resources_moved/SOData/SOData_Porp.asset");
		while (!handle_SOData_Prop.IsDone)
		{
			_Text_进度.text = "收集六根...";
			yield return new WaitForEndOfFrame();
		}
		_Slider_进度.DOValue(0.75f, 0.2f);
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_Prop = handle_SOData_Prop.Result;
		AsyncOperationHandle<SOData_Audio> handle_SOData_Audio = Addressables.LoadAssetAsync<SOData_Audio>("Assets/_05_Data/Resources_moved/SOData/SOData_Audio.asset");
		while (!handle_SOData_Audio.IsDone)
		{
			_Text_进度.text = "接着奏乐接着舞...";
			yield return new WaitForEndOfFrame();
		}
		_Slider_进度.DOValue(0.9f, 0.2f);
		yield return new WaitForSeconds(0.1f);
		DSB_MonoSingleton<MyDataManager>.Instance._SOData_Audio = handle_SOData_Audio.Result;
		DSB_MonoSingleton<AudioManager>.Instance.F_OnAwake();
		yield return new WaitForEndOfFrame();
		DSB_MonoSingleton<MyInputManager>.Instance.Init();
		yield return new WaitForEndOfFrame();
		AsyncOperationHandle<SceneInstance> handle_Game场景 = Addressables.LoadSceneAsync("Assets/_01_Scenes/Game.unity", LoadSceneMode.Single, activateOnLoad: false);
		while (!handle_Game场景.IsDone)
		{
			_Text_进度.text = "搭建场景中...";
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.1f);
		yield return new WaitForEndOfFrame();
		_Slider_进度.value = 1f;
		if (handle_Game场景.Status == AsyncOperationStatus.Succeeded)
		{
			handle_Game场景.Result.ActivateAsync();
		}
		else
		{
			_Text_进度.text = "场景加载失败!";
			MyLog.LogError("场景加载失败.");
		}
		_Text_进度.text = "场景加载成功!";
	}
}
