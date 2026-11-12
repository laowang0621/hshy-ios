using System.Collections.Generic;
using DSB;
using UnityEngine;

public class C_出租云 : MonoBehaviour
{
	public Transform _Tsf_UI;

	private ItemUI_出租云 _ItemUI_出租云;

	public Collider2D _Co2d;

	public Animator _Ani;

	private Dictionary<string, float> _Dict_AniName_time;

	private int id;

	private void Awake()
	{
		_Co2d.enabled = false;
	}

	private void Start()
	{
		_ItemUI_出租云 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_出租云();
		_ItemUI_出租云._可见性 = false;
		_ItemUI_出租云.F_SetPos(_Tsf_UI.position);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_销毁);
		_Dict_AniName_time = new Dictionary<string, float>();
		AnimationClip[] animationClips = _Ani.runtimeAnimatorController.animationClips;
		foreach (AnimationClip animationClip in animationClips)
		{
			_Dict_AniName_time.TryAdd(animationClip.name, animationClip.length);
		}
		id = GetHashCode();
		_Ani.Play("降落");
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, id, _Dict_AniName_time["降落"], delegate
		{
			_Co2d.enabled = true;
		});
	}

	public void F_起飞后销毁()
	{
		_Co2d.enabled = false;
		_Ani.SetTrigger("起飞");
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, id, _Dict_AniName_time["起飞"], delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	private void F_上云()
	{
		if (GameManager._CanPlay)
		{
			_ItemUI_出租云.F_上云();
		}
	}

	public void F_销毁(DSB_EventArgs args)
	{
		Object.Destroy(base.gameObject);
	}

	private void OnDestroy()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_上云;
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_销毁);
		if (_ItemUI_出租云 != null)
		{
			Object.Destroy(_ItemUI_出租云.gameObject, 0.1f);
		}
	}

	protected virtual void LateUpdate()
	{
		if (GameManager._CanPlay && _ItemUI_出租云._可见性)
		{
			_ItemUI_出租云.F_SetPos(_Tsf_UI.position);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_出租云.F_SetPos(_Tsf_UI.position);
			_ItemUI_出租云._可见性 = true;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_上云;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_出租云._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_上云;
		}
	}
}
