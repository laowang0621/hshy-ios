using DSB;
using UnityEngine;

public class C_宝箱 : MonoBehaviour, IEnemy
{
	[Header("101-经验\t102-攻击\t103-生命\t104-法力\t105气力")]
	[Header("哪个boss被击杀后出现,需要'C_Boss生成器'引用,在boss死亡后回判断刷新宝箱状态,小于0应该都没用")]
	public Enum_Enemy _BossName;

	public bool _需要同步位置;

	public int _宝箱ID;

	public int _物品ID = 101;

	public Transform _Tsf_UI;

	private ItemUI_交互 _ItemUI_交互键;

	public SpriteRenderer _SR;

	public Sprite _宝箱开;

	public Sprite _宝箱关;

	private bool _已经开启;

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
		_已经开启 = true;
		_ItemUI_交互键._可见性 = false;
		_SR.sprite = (_已经开启 ? _宝箱开 : _宝箱关);
		if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断宝箱已开启_新(_宝箱ID))
		{
			DSB_MonoSingleton<GameManager>.Instance.F_开箱(_物品ID);
			DSB_MonoSingleton<MyDataManager>.Instance.F_打开宝箱_新(_宝箱ID);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_已经开启 && collision.gameObject.layer == 3)
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
		if (!_已经开启 && collision.gameObject.layer == 3)
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
		_已经开启 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断宝箱已开启_新(_宝箱ID);
		_SR.sprite = (_已经开启 ? _宝箱开 : _宝箱关);
	}

	public void F_空降显示(Vector2 pos)
	{
		if (_需要同步位置)
		{
			base.transform.position = pos;
			DSB_MonoSingleton<MyDataManager>.Instance.F_保存空降宝箱位置(_宝箱ID, pos);
		}
		if (_BossName == Enum_Enemy.NULL)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_BossName))
		{
			base.gameObject.SetActive(value: true);
			base.gameObject.GetComponent<Animator>().Play("空降");
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
		_已经开启 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断宝箱已开启_新(_宝箱ID);
		_SR.sprite = (_已经开启 ? _宝箱开 : _宝箱关);
	}

	public void F_重置()
	{
	}
}
