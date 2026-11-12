using System;
using DG.Tweening;
using DSB;
using UnityEngine;

public class CameraController : DSB_MonoSingleton<CameraController>
{
	public Transform _Tsf_Player;

	public Transform _Tsf_CameraPos;

	public Camera _Camera;

	public bool _水平保持中间;

	public bool _垂直保持0;

	private float _CameraMaxX;

	private float _CameraMinX;

	private float _CameraMaxY;

	private float _CameraMinY;

	private bool _启动补间跟随;

	private float _启动补间跟随Time;

	private float _启动补间跟随Timer;

	public bool _由外部控制中;

	public event Action<float> _On_LateUpdate = delegate
	{
	};

	private void Start()
	{
		_Camera = Camera.main;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, delegate
		{
			_Camera.DOKill();
			_Camera.orthographicSize = Lunch._镜头默认大小;
		});
		_Camera.transform.localPosition = new Vector2(0f, Lunch._镜头偏移.y);
	}

	public void F_屏幕震动(float 持续时间 = 0.5f, float 震动幅度 = 0.1f, int 震动频率 = 10)
	{
		if (_Camera != null)
		{
			_Camera.transform.localPosition = new Vector2(0f, Lunch._镜头偏移.y);
			_Camera.DOShakePosition(持续时间, 震动幅度, 震动频率);
		}
	}

	public void F_屏幕缩放_闪一下(float 持续时间 = 0.5f, float 缩放幅度 = 1f)
	{
		if (_Camera != null)
		{
			float val = _Camera.orthographicSize;
			_Camera.DOOrthoSize(val - 缩放幅度, 持续时间 / 2f).OnComplete(delegate
			{
				_Camera.DOOrthoSize(val, 持续时间 / 2f);
			});
		}
	}

	public void F_刷新边界(Vector2 左边界X, Vector2 右边界X, Vector2 上边界Y, Vector2 下边界Y)
	{
		float orthographicSize = _Camera.orthographicSize;
		_CameraMinY = 下边界Y.y + orthographicSize - Lunch._镜头偏移.y;
		_CameraMaxY = 上边界Y.y - orthographicSize - Lunch._镜头偏移.y;
		float num = orthographicSize * _Camera.aspect;
		_CameraMinX = 左边界X.x + num;
		_CameraMaxX = 右边界X.x - num;
		F_检测边界是否固定();
		_启动补间跟随 = false;
		_由外部控制中 = false;
	}

	public void F_检测边界是否固定()
	{
		_ = _Camera.orthographicSize;
		_ = _Camera.aspect;
		float num = _CameraMaxY - _CameraMinY;
		_垂直保持0 = false;
		if (num <= 0f)
		{
			_垂直保持0 = true;
			Vector3 position = _Tsf_CameraPos.position;
			position.z = -15f;
			position.y = (_CameraMinY + _CameraMaxY) / 2f - Lunch._镜头偏移.y;
			_Tsf_CameraPos.position = position;
		}
		float num2 = _CameraMaxX - _CameraMinX;
		_水平保持中间 = false;
		if (num2 <= 0f)
		{
			_水平保持中间 = true;
			Vector3 position2 = _Tsf_CameraPos.position;
			position2.z = -15f;
			position2.x = (_CameraMinX + _CameraMaxX) / 2f;
			_Tsf_CameraPos.position = position2;
		}
	}

	public void F_更新上边界(float 上边界Y, float time = -1f)
	{
		float orthographicSize = _Camera.orthographicSize;
		_ = _Camera.aspect;
		_CameraMaxY = 上边界Y - orthographicSize - Lunch._镜头偏移.y;
		F_检测边界是否固定();
		if (time > 0.1f)
		{
			F_设置补间跟随时间(time);
		}
	}

	public void F_更新下边界(float 下边界Y, float time = -1f)
	{
		float orthographicSize = _Camera.orthographicSize;
		_ = _Camera.aspect;
		_CameraMinY = 下边界Y + orthographicSize - Lunch._镜头偏移.y;
		F_检测边界是否固定();
		if (time > 0.1f)
		{
			F_设置补间跟随时间(time);
		}
	}

	public void F_更新左边界(float 左边界X, float time = -1f)
	{
		float num = _Camera.orthographicSize * _Camera.aspect;
		_CameraMinX = 左边界X + num;
		F_检测边界是否固定();
		if (time > 0.1f)
		{
			F_设置补间跟随时间(time);
		}
	}

	public void F_更新右边界(float 右边界X, float time = -1f)
	{
		float num = _Camera.orthographicSize * _Camera.aspect;
		_CameraMaxX = 右边界X - num;
		F_检测边界是否固定();
		if (time > 0.1f)
		{
			F_设置补间跟随时间(time);
		}
	}

	public void F_更新上下边界(float 上边界Y, float 下边界Y)
	{
		float orthographicSize = _Camera.orthographicSize;
		_CameraMinY = 下边界Y + orthographicSize - Lunch._镜头偏移.y;
		_CameraMaxY = 上边界Y - orthographicSize - Lunch._镜头偏移.y;
	}

	public void F_设置补间跟随时间(float time)
	{
		if (!_启动补间跟随)
		{
			_启动补间跟随 = true;
			_启动补间跟随Time = time;
			_启动补间跟随Timer = 0f;
		}
	}

	private void LateUpdate()
	{
		if (_由外部控制中)
		{
			return;
		}
		if (_Tsf_Player == null)
		{
			PlayerController pC = DSB_MonoSingleton<GameManager>.Instance._PC;
			if (pC != null)
			{
				_Tsf_Player = pC.transform;
			}
			return;
		}
		float num = 0f;
		float num2 = 0f - Lunch._镜头偏移.y;
		if (_水平保持中间)
		{
			num = (_CameraMinX + _CameraMaxX) / 2f;
		}
		else
		{
			num = _Tsf_Player.position.x;
			if (num < _CameraMinX)
			{
				num = _CameraMinX;
			}
			else if (num > _CameraMaxX)
			{
				num = _CameraMaxX;
			}
			this._On_LateUpdate(num);
		}
		if (_垂直保持0)
		{
			num2 = (_CameraMinY + _CameraMaxY) / 2f;
		}
		else
		{
			num2 = _Tsf_Player.position.y;
			if (num2 < _CameraMinY)
			{
				num2 = _CameraMinY;
			}
			else if (num2 > _CameraMaxY)
			{
				num2 = _CameraMaxY;
			}
		}
		if (_启动补间跟随)
		{
			_启动补间跟随Timer += Time.deltaTime;
			if (_启动补间跟随Timer >= _启动补间跟随Time)
			{
				_启动补间跟随 = false;
			}
			_Tsf_CameraPos.position = Vector3.Lerp(_Tsf_CameraPos.position, new Vector3(num, num2, -15f), _启动补间跟随Timer / _启动补间跟随Time * 60f * Time.deltaTime);
		}
		else
		{
			_Tsf_CameraPos.position = new Vector3(num, num2, -15f);
		}
	}
}
