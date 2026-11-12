using DSB;
using UnityEngine;

public class EnemyAI_赤尻马猴 : EnemyAI_Boss
{
	public Enum_对话 _This_对话类型_见面;

	public Enum_对话 _This_对话类型_死亡;

	public bool _可以转阶段_2 = true;

	public bool _生成时候蓄力攻击;

	[HideInInspector]
	public bool _无敌;

	public Transform _Tsf_幻影攻击;

	public Transform _Tsf_地上剑气;

	public C_弹道_赤尻马猴_黑边血色剑气 _C_弹道_赤尻马猴_黑边血色剑气;

	public C_弹道_弓箭 _弹道_弓箭_幻影攻击;

	public SpriteRenderer _SR_刀光;

	public Color _二阶段刀光颜色;

	public SpriteRenderer _SR_黑影特效;

	public float _生翼_透明度 = 0.8f;

	public bool _是生翼状态;

	public C_特效_残影残留 _C_特效_残影残留;

	public override Enum_对话 _对话类型_见面 => _This_对话类型_见面;

	public override Enum_对话 _对话类型_死亡 => _This_对话类型_见面;

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.Log(base.name + ">>已经初始化！");
			return;
		}
		MyLog.Log("初始化<<" + base.name + ">>开始");
		_已经进入初始化 = true;
		_ = _Data;
		_Data.F_重置死亡状态();
		_Data.F_显示();
		_Data._StartDrag = _Data._Mono组件._Rb2D.drag;
		F_添加状态();
		if (_生成时候蓄力攻击)
		{
			_Data._Mono组件._SR_Model.SetAlpha(0f);
			_Data._Mono组件._SR_影子.SetAlpha(0f);
			ChangeState(EnemyStateName_Type.攻击);
		}
		else
		{
			ChangeState(EnemyStateName_Type.站立);
		}
		if (!DSB_MonoSingleton<MyDataManager>.Instance.F_判断对话((int)_对话类型_见面))
		{
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 0.2f, delegate
			{
				GameManager._CanPlay = false;
				DSB_MonoSingleton<UIManager>.Instance.F_触发对话(_对话类型_见面);
			});
		}
		MyLog.Log("初始化<<" + base.name + ">>结束");
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
		F_播放BossBGM();
	}

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_Boss_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击(15f).Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_赤尻马猴_攻击().Init(_Data));
		if (_是生翼状态)
		{
			_可以转阶段_2 = false;
			_SR_刀光.color = _二阶段刀光颜色;
			_SR_黑影特效.gameObject.SetActive(value: true);
			_SR_黑影特效.SetAlpha(_生翼_透明度);
		}
		else
		{
			_SR_黑影特效.gameObject.SetActive(value: false);
		}
		base._Boss死亡 += delegate
		{
			_C_特效_残影残留.F_停止生成();
		};
	}

	public void F_特殊调用1()
	{
		_生成时候蓄力攻击 = true;
	}

	private void Start()
	{
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_无敌)
		{
			base.BeAttack(attackInfo);
		}
	}
}
