using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_黄眉 : EnemyAI_Boss
{
	public bool _开启固定出招;

	public int _固定出招序号 = 1;

	public Enum_对话 _This_对话类型_见面;

	public Enum_对话 _This_对话类型_死亡;

	[HideInInspector]
	public bool _死亡转阶段;

	public ParticleSystem _PS_消散粒子;

	public C_策反Test _C_策反Test_Prefab;

	[HideInInspector]
	public bool _下次策反猴群;

	[HideInInspector]
	public List<EnemyAI_猴子猴孙> _List_猴子猴孙;

	public bool _初始影身;

	public GameObject _GO_金身反击;

	public C_攻击判断 _C_地裂_Prefab;

	public C_攻击判断 _C_自爆_Prefab;

	public C_攻击判断 _C_雷暴1_Prefab;

	public C_攻击判断 _C_雷暴2_Prefab;

	public C_攻击判断 _C_雷暴3_Prefab;

	public C_攻击判断 _C_小落雷_Prefab;

	public C_攻击判断 _C_大落雷_Prefab;

	public C_攻击判断 _C_电圈_Prefab;

	public C_攻击判断 _C_右半边电圈_Prefab;

	public List<Transform> _Tsf_小落雷_戳锤;

	public Transform _Tsf_跺脚_雷暴1;

	public Transform _Tsf_下砸_地裂;

	public Transform _Tsf_下砸_雷暴2;

	public Transform _Tsf_插地_雷暴3;

	public Transform _Tsf_自爆;

	public Transform _Tsf_自爆Tsf_策反特效生成位置;

	public bool _无敌;

	protected bool _m金身;

	private float _Outlines_Size;

	public bool _是二阶段;

	public override Enum_对话 _对话类型_见面 => _This_对话类型_见面;

	public override Enum_对话 _对话类型_死亡 => _This_对话类型_死亡;

	public bool _金身
	{
		get
		{
			return _m金身;
		}
		set
		{
			_m金身 = value;
			if (_m金身)
			{
				float num = (0.12f - _Outlines_Size) / 0.1f;
				_Data._Mono组件._SR_Model.DOColor(new Color(1f, 0.95f, 0.4f), 0.5f);
				if (num < 0.1f)
				{
					_Outlines_Size = 0.12f;
					_Data._Mono组件._SR_Model.material.SetFloat(MyID._Shader_Outlines_Size, _Outlines_Size);
					return;
				}
				DOTween.To(() => _Outlines_Size, delegate(float x)
				{
					_Outlines_Size = x;
					_Data._Mono组件._SR_Model.material.SetFloat(MyID._Shader_Outlines_Size, _Outlines_Size);
				}, 0.12f, num);
				return;
			}
			float num2 = _Outlines_Size / 0.3f;
			_Data._Mono组件._SR_Model.DOColor(new Color(1f, 1f, 1f), 0.5f);
			if (num2 < 0.1f)
			{
				_Outlines_Size = 0f;
				_Data._Mono组件._SR_Model.material.SetFloat(MyID._Shader_Outlines_Size, _Outlines_Size);
				return;
			}
			DOTween.To(() => _Outlines_Size, delegate(float x)
			{
				_Outlines_Size = x;
				_Data._Mono组件._SR_Model.material.SetFloat(MyID._Shader_Outlines_Size, _Outlines_Size);
			}, 0f, num2);
		}
	}

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_Boss_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_Boss_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_黄眉_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_Boss_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_黄眉_攻击().Init(_Data));
		_无敌 = false;
		if (_初始影身)
		{
			_Data._Mono组件._SR_Model.SetAlpha(0f);
			_Data._Mono组件._SR_影子.SetAlpha(0f);
			_Data._Mono组件._SR_Model.DOFade(1f, 1f);
			_Data._Mono组件._SR_影子.DOFade(1f, 1f);
		}
		_Outlines_Size = 0f;
		_Data._Mono组件._SR_Model.material.SetFloat(MyID._Shader_Outlines_Size, _Outlines_Size);
		if (_是二阶段)
		{
			DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_玩家释放身外身法._EventID, F_EventArgs_玩家释放身外身法);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (_是二阶段)
		{
			DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_玩家释放身外身法._EventID, F_EventArgs_玩家释放身外身法);
		}
	}

	private void F_EventArgs_玩家释放身外身法(DSB_EventArgs e)
	{
		EventArgs_玩家释放身外身法 eventArgs_玩家释放身外身法 = (EventArgs_玩家释放身外身法)e;
		if (!base._Is被定 && eventArgs_玩家释放身外身法._List_猴子猴孙 != null && eventArgs_玩家释放身外身法._List_猴子猴孙.Count > 0)
		{
			_下次策反猴群 = true;
			_List_猴子猴孙 = eventArgs_玩家释放身外身法._List_猴子猴孙;
		}
	}

	public void F_特殊调用1()
	{
		_死亡转阶段 = true;
	}

	public override void F_定身(float 时间)
	{
		if (!_金身)
		{
			if (_是二阶段 && _FSM.currStateName != EnemyStateName_Type.倒地 && _FSM.currStateName != EnemyStateName_Type.受击 && _FSM.currStateName != EnemyStateName_Type.死亡)
			{
				base.F_定身(0.2f);
				_FSM.ChangeState(EnemyStateName_Type.攻击);
			}
			else
			{
				base.F_定身(时间);
			}
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_金身)
		{
			MyLog.LogError($"{base.name}>>金身 被攻击,atk_强破坏力>>{attackInfo.atk_强破坏力},");
			if (attackInfo.atk_强破坏力)
			{
				attackInfo.atk_僵直 = 9999f;
				_金身 = false;
				base.BeAttack(attackInfo);
			}
			else if (attackInfo.atk_有僵直)
			{
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_打铁_1();
				DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(0.25f, 0.15f, 12);
				Invoke("F_金身反击", 0.075f);
			}
		}
		else
		{
			base.BeAttack(attackInfo);
		}
	}

	private void F_金身反击()
	{
		_GO_金身反击.SetActive(value: true);
	}
}
