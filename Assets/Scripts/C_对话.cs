using DSB;
using UnityEngine;
using UnityEngine.Events;

public class C_对话 : MonoBehaviour
{
	public Enum_对话 _Enum_对话类型;

	[HideInInspector]
	public bool _已触发对话;

	public GameObject _GO_对话角色;

	public GameObject _GO_烟雾爆炸特效;

	private int _this对话TimerID;

	public UnityEvent _Event_加载对话;

	public UnityEvent _Event_触发对话;

	public UnityEvent _Event_对话结束前_触发保存前;

	[Header("完全对话后")]
	public UnityEvent _Event_对话结束;

	private void OnEnable()
	{
		_已触发对话 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Enum_对话类型);
		MyLog.Log($"对话是否触发过<<{_Enum_对话类型}<<{_已触发对话}");
		if (_GO_对话角色 != null)
		{
			_GO_对话角色.SetActive(!_已触发对话);
		}
		_Event_加载对话?.Invoke();
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<DSB_TimerManager>.Instance.RemoveTimer(_this对话TimerID);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_已触发对话 && collision.gameObject.layer == 3)
		{
			F_触发对话();
		}
	}

	public void F_触发对话()
	{
		_已触发对话 = true;
		DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_Enum_对话类型, _Event_对话结束前_触发保存前.Invoke);
		_Event_触发对话?.Invoke();
		if (_GO_对话角色 != null)
		{
			_this对话TimerID = DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(this, 0.7114514f, F_土地离开);
		}
	}

	public void F_Boss_隐藏()
	{
		base.gameObject.SetActive(value: false);
	}

	public void F_Boss_显示()
	{
		_已触发对话 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_Enum_对话类型);
		if (!_已触发对话 && _GO_对话角色 != null && _GO_烟雾爆炸特效 != null)
		{
			F_生成烟雾(_GO_对话角色.transform.position);
		}
		base.gameObject.SetActive(value: true);
	}

	private void F_土地离开()
	{
		if (!(_GO_对话角色 == null))
		{
			_GO_对话角色.SetActive(value: false);
			_Event_对话结束?.Invoke();
			F_生成烟雾(_GO_对话角色.transform.position + new Vector3(0f, 1f));
		}
	}

	public void F_生成烟雾(Vector2 pos)
	{
		if (_GO_烟雾爆炸特效 != null)
		{
			Object.Instantiate(_GO_烟雾爆炸特效, pos, Quaternion.identity);
		}
	}

	public void F_生成烟雾(Transform tsf)
	{
		F_生成烟雾(tsf.position);
	}
}
