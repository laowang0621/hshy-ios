using System;
using DSB;
using UnityEngine;

public class C_爆竹 : MonoBehaviour, IBeAttack
{
	public Rigidbody2D _Rb2D;

	public Collider2D _Co2D;

	public Collider2D _Co2D_触发器;

	public Transform _Tsf_UI;

	private ItemUI_交互 _ItemUI_交互键;

	[HideInInspector]
	public ItemUI_文本 _ItemUI_倒计时;

	public SpriteRenderer _SR;

	public GameObject _GO_触发特效;

	public C_攻击判断 _爆炸特效Prefab;

	public Animator _Ani;

	private bool _已经拿起;

	private bool _已经丢出;

	private bool _已经引爆;

	private Vector2 _Dir;

	private float _倒计时Timer;

	private bool _已经被攻击;

	public int _阵营
	{
		get
		{
			return 2;
		}
		set
		{
			MyLog.LogError(114514);
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public event Action _Event_引爆 = delegate
	{
	};

	private void Awake()
	{
		DataInfo_Enemy基础数值 dataInfo_Enemy基础数值 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_基础数值(10309);
		_爆炸特效Prefab._伤害 = dataInfo_Enemy基础数值._攻击力;
		_爆炸特效Prefab._僵直伤害 = dataInfo_Enemy基础数值._僵直数值;
		_爆炸特效Prefab._击退力度 = 10f;
		MyLog.LogError("我是爆竹");
		_Rb2D.bodyType = RigidbodyType2D.Static;
		_ItemUI_交互键 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_交互();
		_ItemUI_倒计时 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_文本();
		_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
		_ItemUI_交互键.F_SetText("举起");
		_已经丢出 = false;
		_已经引爆 = false;
		_已经拿起 = false;
		_ItemUI_交互键._可见性 = false;
		_ItemUI_倒计时._可见性 = false;
		_GO_触发特效.SetActive(value: false);
	}

	public void F_空降()
	{
		_Ani.Play("空降", 0, 0f);
	}

	private void OnEnable()
	{
	}

	private void OnDestroy()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_交互;
		this._Event_引爆 = null;
		if (_ItemUI_交互键 != null)
		{
			UnityEngine.Object.Destroy(_ItemUI_交互键.gameObject);
			_ItemUI_交互键 = null;
		}
		if (_ItemUI_倒计时 != null)
		{
			UnityEngine.Object.Destroy(_ItemUI_倒计时.gameObject);
			_ItemUI_倒计时 = null;
		}
	}

	public void F_被丢出(float 倒计时, Vector2 startPos, Vector2 dir)
	{
		_倒计时Timer = 倒计时;
		_已经丢出 = true;
		base.gameObject.SetActive(value: true);
		_Rb2D.bodyType = RigidbodyType2D.Dynamic;
		_Dir = dir;
		base.transform.position = startPos + dir;
		_Ani.Play("丢出", 0, 0f);
		_Co2D_触发器.enabled = true;
	}

	public void F_Ani_Set速度(float 速度)
	{
		_Rb2D.velocity = _Dir * 速度;
	}

	public void F_引爆(Vector2 pos)
	{
		MyLog.LogError("爆竹爆炸");
		this._Event_引爆();
		_ItemUI_倒计时._可见性 = false;
		_已经引爆 = true;
		UnityEngine.Object.Instantiate(_爆炸特效Prefab, pos, Quaternion.identity);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void F_交互()
	{
		if (DSB_MonoSingleton<GameManager>.Instance._PC._Is_正常移动)
		{
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_交互;
			_已经拿起 = true;
			_ItemUI_交互键._可见性 = false;
			_Co2D_触发器.enabled = false;
			DSB_MonoSingleton<GameManager>.Instance._PC.F_拿起爆竹(this);
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_已经引爆)
		{
			return;
		}
		if (_已经丢出)
		{
			if (collision.gameObject.layer == 6)
			{
				F_引爆(base.transform.position);
			}
		}
		else if (!_已经拿起 && collision.gameObject.layer == 3)
		{
			_ItemUI_交互键._可见性 = true;
			_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_交互;
		}
	}

	protected virtual void LateUpdate()
	{
		if (_已经引爆)
		{
			return;
		}
		if (_已经丢出)
		{
			_倒计时Timer -= Time.deltaTime;
			_ItemUI_倒计时.F_SetPos(_Tsf_UI.position);
			_ItemUI_倒计时.F_SetText(_倒计时Timer.ToString("0.0"));
			if (_倒计时Timer <= 0f)
			{
				F_引爆(base.transform.position);
			}
		}
		else if (_ItemUI_交互键._可见性)
		{
			_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!_已经拿起 && collision.gameObject.layer == 3)
		{
			_ItemUI_交互键._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_交互;
		}
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_交互;
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		if (!_已经被攻击)
		{
			_已经被攻击 = true;
			_已经拿起 = true;
			_已经丢出 = true;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_交互;
			MyLog.LogError(">>>>>>>>>>>>>>>>>>>>>>>>>");
			_Rb2D.bodyType = RigidbodyType2D.Dynamic;
			_ItemUI_交互键._可见性 = false;
			_ItemUI_倒计时._可见性 = true;
			_倒计时Timer = 0.7f;
			_Dir = new Vector2((attackInfo.atk_Pos.x < base.transform.position.x) ? 1 : (-1), 0f);
			F_Ani_Set速度(30f);
		}
	}
}
