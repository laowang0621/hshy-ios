using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_石母 : EnemyAI_Boss
{
	public List<Sprite> _上;

	public List<Sprite> _下;

	public SpriteRenderer _SR_身体;

	public SpriteRenderer _SR_头发1;

	public SpriteRenderer _SR_头发2;

	public SpriteRenderer _SR_头发3;

	public Transform _Tsf_影子;

	public EnemyAI_石双双 _GO_石双双Prefab;

	public EnemyAI_石双双 _GO_石苍苍Prefab;

	public Transform _Tsf_召唤范围;

	private Vector2 _召唤_Pos左下;

	private Vector2 _召唤_Pos右上;

	public C_宝箱 _宝箱;

	private int _Max波数 = 5;

	private int _剩余波数;

	public bool _无敌;

	public int _m石子石孙Count;

	public override Enum_对话 _对话类型_见面 => Enum_对话.NULL;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.NULL;

	public int _石子石孙Count
	{
		get
		{
			return _m石子石孙Count;
		}
		set
		{
			_m石子石孙Count = value;
			if (_m石子石孙Count > 0)
			{
				if (!_无敌)
				{
					_无敌 = true;
					_SR_头发2.DOKill();
					_SR_头发2.DOFade(0f, 0.28f);
					_SR_头发3.DOKill();
					_SR_头发3.DOFade(1f, 0.28f);
				}
			}
			else if (_无敌)
			{
				_无敌 = false;
				_SR_头发2.DOKill();
				_SR_头发2.DOFade(1f, 0.18f);
				_SR_头发3.DOKill();
				_SR_头发3.DOFade(0f, 0.18f);
			}
		}
	}

	protected override void Awake()
	{
	}

	private void Start()
	{
		Init();
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		MyLog.Log("初始化<<" + base.name + ">>开始");
		_已经进入初始化 = true;
		MyLog.LogError("-------------------");
		_ = _Data;
		_Data.F_显示();
		_Data._StartDrag = _Data._Mono组件._Rb2D.drag;
		_m石子石孙Count = 0;
		F_添加状态();
		ChangeState(EnemyStateName_Type.站立);
		if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_对话类型_见面))
		{
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 0.2f, delegate
			{
				GameManager._CanPlay = false;
				DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_对话类型_见面);
			});
		}
		base._Boss死亡 += delegate
		{
			_宝箱?.F_空降显示(_宝箱.transform.position);
		};
		MyLog.Log("初始化<<" + base.name + ">>结束");
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		F_播放BossBGM();
	}

	public override void F_隐藏()
	{
		_SR_头发2.DOKill();
		_SR_头发2.SetAlpha(0f);
		_无敌 = false;
		_m石子石孙Count = 0;
		_SR_头发3.DOKill();
		_SR_头发3.SetAlpha(0f);
		_Data._UI_BossHP.gameObject.SetActive(value: false);
	}

	public override void F_显示()
	{
		_无敌 = false;
		_m石子石孙Count = 0;
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)_Data._Mono组件._EnemyID))
		{
			_宝箱?.F_隐藏();
			_Data._UI_BossHP.gameObject.SetActive(value: false);
			_Data._Is死亡 = true;
			_Data.F_设置UI血量(1f, 1f, _可见性: false);
			_SR_头发2.DOKill();
			_SR_头发2.SetAlpha(0f);
			_SR_头发3.DOKill();
			_SR_头发3.SetAlpha(0f);
		}
		else
		{
			_宝箱?.F_显示();
			_Data._UI_BossHP.gameObject.SetActive(value: true);
			_Data.F_显示();
			_Data._Is死亡 = false;
			_SR_头发2.DOKill();
			_SR_头发2.SetAlpha(1f);
			_SR_头发3.DOKill();
			_SR_头发3.SetAlpha(0f);
			_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_石母_站立().Init(_Data));
		}
	}

	private Vector2 F_获取随机召唤点()
	{
		return new Vector2(Random.Range(_召唤_Pos左下.x, _召唤_Pos右上.x), Random.Range(_召唤_Pos左下.y, _召唤_Pos右上.y));
	}

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_石母_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_石母_死亡().Init(_Data));
		_剩余波数 = _Max波数;
		Vector2 vector = _Tsf_召唤范围.transform.localScale / 2f;
		_召唤_Pos左下 = (Vector2)_Tsf_召唤范围.transform.position - vector;
		_召唤_Pos右上 = (Vector2)_Tsf_召唤范围.transform.position + vector;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		if (_无敌)
		{
			_SR_头发3.DOKill();
			_SR_头发3.SetAlpha(1f);
			_SR_头发3.DOFade(0.3f, 0.08f).OnComplete(delegate
			{
				_SR_头发3.DOFade(1f, 0.08f);
			});
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_撞击();
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		base._BeAttack_Pos = attackInfo.atk_Pos;
		base._BeAttack_击退力度 = attackInfo.atk_击退力度;
		_SR_头发2.SetAlpha(1f);
		_SR_头发2.DOKill();
		_SR_头发2.DOFade(0.3f, 0.08f).OnComplete(delegate
		{
			_SR_头发2.DOFade(1f, 0.08f);
		});
		_Data._Curr生命 -= attackInfo.atk_生命;
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		if (_Data._Curr生命 <= 0f)
		{
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
			ChangeState(EnemyStateName_Type.死亡);
		}
		else
		{
			F_判断并召唤石头();
		}
	}

	private void F_判断并召唤石头()
	{
		int value = (int)(_Data._Curr生命 / _Data._MaxHP * ((float)_Max波数 + 1f));
		value = Mathf.Clamp(value, 0, 5);
		int num = _剩余波数 - value;
		_剩余波数 = value;
		MyLog.LogError($"{value}>>{num}>>");
		if (num <= 0)
		{
			return;
		}
		while (num > 0)
		{
			num--;
			int num2 = Random.Range(2, 4);
			for (int i = 0; i < num2; i++)
			{
				_石子石孙Count++;
				Object.Instantiate((Random.value < 0.5f) ? _GO_石双双Prefab : _GO_石苍苍Prefab, F_获取随机召唤点(), Quaternion.identity).F_召唤(delegate
				{
					_石子石孙Count--;
				});
			}
		}
	}
}
