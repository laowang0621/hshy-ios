using DSB;
using UnityEngine;

public class C_宝箱_新春 : MonoBehaviour, IEnemy
{
	[Header("哪个boss被击杀后出现,需要'C_Boss生成器'引用,在boss死亡后回判断刷新宝箱状态,小于0应该都没用")]
	public Enum_Enemy _BossName;

	public int _宝箱ID;

	public bool _需要同步位置;

	public Transform _Tsf_UI;

	private ItemUI_交互 _ItemUI_交互键;

	private void Awake()
	{
		_ItemUI_交互键 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_交互();
		_ItemUI_交互键._可见性 = false;
		_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
		_ItemUI_交互键.F_SetText("打开");
	}

	private void OnDestroy()
	{
		if (_ItemUI_交互键 != null)
		{
			Object.Destroy(_ItemUI_交互键.gameObject);
		}
	}

	public void F_开箱()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
		Application.OpenURL("https://www.bilibili.com/video/BV1zZ4y1d7jH");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_交互键._可见性 = true;
			_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_开箱;
		}
	}

	protected virtual void LateUpdate()
	{
		if (_ItemUI_交互键._可见性)
		{
			_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_交互键._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
		}
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
	}

	public void F_隐藏()
	{
		base.gameObject.SetActive(value: false);
	}

	public void F_显示()
	{
		if (_需要同步位置 && DSB_MonoSingleton<MyDataManager>.Instance.F_TryGet空降宝箱位置(_宝箱ID, out var 宝箱pos))
		{
			base.transform.position = 宝箱pos;
		}
		if (_BossName == Enum_Enemy.NULL)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			base.gameObject.SetActive(value: true);
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public void F_空降显示(Vector2 pos)
	{
		if (_需要同步位置)
		{
			base.transform.position = pos;
			DSB_MonoSingleton<MyDataManager>.Instance.F_保存空降宝箱位置(_宝箱ID, pos);
		}
		base.gameObject.SetActive(value: true);
		base.gameObject.GetComponent<Animator>().Play("空降");
	}

	public void F_重置()
	{
	}
}
