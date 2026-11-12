using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class C_是马楼就下地狱100层 : MonoBehaviour
{
	public float _权重Max;

	[Header("第一个为普通平台")]
	public List<C_平台> _List_平台;

	public List<C_平台> _List_平台_远程敌人;

	public float _最下面的普通Y值;

	public C_是马楼就下地狱100层_墙壁 _是马楼就下地狱100层_墙壁;

	public C_是马楼就下地狱100层_背景 _是马楼就下地狱100层_背景;

	public C_是马楼就下地狱100层_死亡区域 _是马楼就下地狱100层_死亡区域;

	public C_是马楼就下地狱100层_底部 _是马楼就下地狱100层_底部;

	public Transform _Tsf_下地狱起点位置;

	private Camera _Camera;

	private bool _镜头来时向下移动;

	public float _镜头移动速度 = 3.5f;

	public Vector2 _随机平台间隔 = new Vector2(3f, 5f);

	public int _每X平台必定刷新普通平台 = 4;

	private int _当前连续非平台平台数量;

	private int _已经生成平台数量;

	private int _最大平台数量 = 50;

	private bool _停止平台生成;

	public Vector2Int _随机刷新平台数量 = new Vector2Int(20, 50);

	public Vector2Int _随机刷新平台数量_刷新敌人 = new Vector2Int(3, 5);

	private int _间隔多少次后刷新敌人;

	private bool _背景墙壁等可以运动;

	private float _镜头移动下限 = float.MinValue;

	private void OnEnable()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_结束._EventID, F_EventArgs_切换地图_结束);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_死亡._EventID, F_EventArgs_Player_死亡);
		_镜头来时向下移动 = false;
		_Camera = DSB_MonoSingleton<CameraController>.Instance._Camera;
		_权重Max = 0f;
		foreach (C_平台 item in _List_平台)
		{
			_权重Max += item._权重;
		}
	}

	private void F_EventArgs_切换地图_结束(DSB_EventArgs e)
	{
		MyLog.LogWarning(1114514 + ">>C_是马楼就下地狱100层");
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.下地狱100层, 是进入: true);
		DSB_MonoSingleton<GameManager>.Instance._PC.F_开始下地狱100层();
		_是马楼就下地狱100层_墙壁.F_Init();
		_是马楼就下地狱100层_背景.F_Init();
		_是马楼就下地狱100层_死亡区域.F_Init(this, _Tsf_下地狱起点位置.position);
		_是马楼就下地狱100层_底部.F_Init(this);
		_停止平台生成 = false;
		_最大平台数量 = Random.Range(_随机刷新平台数量.x, _随机刷新平台数量.y) + 5;
		_已经生成平台数量 = 0;
		_背景墙壁等可以运动 = true;
		_最下面的普通Y值 = _Tsf_下地狱起点位置.position.y;
		_间隔多少次后刷新敌人 = 2 + Random.Range(_随机刷新平台数量_刷新敌人.x, _随机刷新平台数量_刷新敌人.y);
		F_生成地板(必须是平台地板: true);
		F_生成地板(必须是平台地板: true);
		F_生成地板(必须是平台地板: true);
		F_生成地板();
		F_生成地板();
	}

	private void F_EventArgs_Player_死亡(DSB_EventArgs e)
	{
		_镜头来时向下移动 = false;
	}

	private void OnDisable()
	{
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.下地狱100层, 是进入: false);
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_结束._EventID, F_EventArgs_切换地图_结束);
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_Player_死亡._EventID, F_EventArgs_Player_死亡);
		F_玩家离开();
	}

	public void F_生成地板(bool 必须是平台地板 = false)
	{
		if (_停止平台生成)
		{
			return;
		}
		if (_已经生成平台数量 >= _最大平台数量)
		{
			_停止平台生成 = true;
			_是马楼就下地狱100层_底部.F_到达底部(_是马楼就下地狱100层_背景.F_获得最下层的位置().y + _是马楼就下地狱100层_背景._间距 / 2f);
			_是马楼就下地狱100层_死亡区域.F_到达底部();
			return;
		}
		_最下面的普通Y值 -= Random.Range(_随机平台间隔.x, _随机平台间隔.y);
		Vector2 vector = new Vector2(Random.Range(-7, 7), _最下面的普通Y值);
		float num = Random.Range(0f, _权重Max);
		if (_当前连续非平台平台数量 >= _每X平台必定刷新普通平台)
		{
			必须是平台地板 = true;
			_当前连续非平台平台数量 = 0;
		}
		_间隔多少次后刷新敌人--;
		if (_间隔多少次后刷新敌人 <= 0)
		{
			_间隔多少次后刷新敌人 = Random.Range(_随机刷新平台数量_刷新敌人.x, _随机刷新平台数量_刷新敌人.y);
			Vector2 vector2 = vector;
			vector2.x += Random.Range(5, 9) * ((Random.value < 0.5f) ? 1 : (-1));
			if (vector2.x < -7f)
			{
				vector2.x += 14f;
			}
			if (vector2.x > 7f)
			{
				vector2.x -= 14f;
			}
			vector2.y += Random.Range(-1.75f, 1.75f);
			Object.Instantiate(_List_平台_远程敌人[Random.Range(0, _List_平台_远程敌人.Count)], vector2, Quaternion.identity);
		}
		if (必须是平台地板)
		{
			Object.Instantiate(_List_平台[0], vector, Quaternion.identity);
			_当前连续非平台平台数量 = 0;
		}
		else
		{
			int num2 = 0;
			int num3;
			while (true)
			{
				num3 = num2++ % _List_平台.Count;
				float 权重 = _List_平台[num3]._权重;
				if (!(num >= 权重))
				{
					break;
				}
				num -= 权重;
			}
			if (num3 == 0)
			{
				_当前连续非平台平台数量 = 0;
			}
			else
			{
				_当前连续非平台平台数量++;
			}
			Object.Instantiate(_List_平台[num3], vector, Quaternion.identity);
		}
		_已经生成平台数量++;
	}

	public void F_玩家开始下地狱()
	{
		DSB_MonoSingleton<CameraController>.Instance._由外部控制中 = true;
		Camera camera = DSB_MonoSingleton<CameraController>.Instance._Camera;
		ShortcutExtensions.DOMove(duration: Vector2.Distance(camera.transform.position, _Tsf_下地狱起点位置.position) / 4.5f, target: camera.transform, endValue: _Tsf_下地狱起点位置.position).SetEase(Ease.Linear).OnComplete(delegate
		{
			_镜头来时向下移动 = true;
			_是马楼就下地狱100层_死亡区域.F_开始下地狱();
		});
	}

	public void F_玩家成功下地狱()
	{
		if (DSB_MonoSingleton<CameraController>.Instance._由外部控制中)
		{
			Camera camera = DSB_MonoSingleton<CameraController>.Instance._Camera;
			Vector3 position = DSB_MonoSingleton<CameraController>.Instance._Tsf_CameraPos.position;
			position.x = camera.transform.position.x;
			position.y = camera.transform.position.y - Lunch._镜头偏移.y;
			DSB_MonoSingleton<CameraController>.Instance._Tsf_CameraPos.position = position;
			camera.transform.localPosition = new Vector2(0f, Lunch._镜头偏移.y);
			DSB_MonoSingleton<CameraController>.Instance._由外部控制中 = false;
			DSB_MonoSingleton<CameraController>.Instance.F_设置补间跟随时间(1.5f);
		}
	}

	public void F_玩家离开()
	{
		if (DSB_MonoSingleton<CameraController>.Instance._由外部控制中)
		{
			DSB_MonoSingleton<CameraController>.Instance._由外部控制中 = false;
			Camera camera = DSB_MonoSingleton<CameraController>.Instance._Camera;
			camera.transform.DOKill();
			camera.transform.localPosition = new Vector2(0f, Lunch._镜头偏移.y);
		}
	}

	public void F_停止背景墙壁等移动()
	{
		_镜头来时向下移动 = false;
	}

	public void F_恢复玩家视野()
	{
		_镜头来时向下移动 = false;
		F_玩家成功下地狱();
	}

	public void F_设置_镜头移动下限(float val)
	{
		_镜头移动下限 = val;
		DSB_MonoSingleton<CameraController>.Instance.F_更新上下边界(val + Lunch._镜头默认大小 * 2f, val);
	}

	private void LateUpdate()
	{
		if (_镜头来时向下移动)
		{
			Vector3 position = _Camera.transform.position;
			position.y -= _镜头移动速度 * Time.deltaTime;
			if (position.y < _镜头移动下限 + Lunch._镜头默认大小)
			{
				position.y = _镜头移动下限 + Lunch._镜头默认大小;
			}
			_Camera.transform.position = position;
			_是马楼就下地狱100层_死亡区域.F_镜头移动调用(position.y);
			if (_背景墙壁等可以运动)
			{
				_是马楼就下地狱100层_墙壁.F_镜头移动调用(position.y);
				_是马楼就下地狱100层_背景.F_镜头移动调用(position.y);
				_是马楼就下地狱100层_底部.F_镜头移动调用(position.y, ref _背景墙壁等可以运动);
			}
			if (_最下面的普通Y值 > position.y - 10f)
			{
				F_生成地板();
			}
		}
	}
}
