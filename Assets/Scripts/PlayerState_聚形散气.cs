using DG.Tweening;
using DSB;
using UnityEngine;

public class PlayerState_聚形散气 : Player_StateBase
{
	public DataInfo_Player招式 _Data_Player_招式系数_翻滚;

	public DataInfo_Player招式 _Data_Player_招式系数_跳跃;

	public DataInfo_Player招式 _Data_Player_招式系数_蓄力;

	public float _翻滚速度;

	private DSB_FSM<PlayerState_聚形散气_State, PlayerState_聚形散气_Base> _FSM_聚形散气;

	public float _EnterDrag;

	private float _Timer_前摇;

	private bool _前摇结束;

	private EnemyAI_猴子猴孙_聚形散气 _猴子猴孙_聚形散气;

	public PlayerState_聚形散气()
	{
		_Data_Player_技能 = MyDataManager.F_Get_Player_技能(102);
		_Data_Player_招式系数_翻滚 = MyDataManager.F_Get_Player_招式系数(1012);
		_Data_Player_招式系数_跳跃 = MyDataManager.F_Get_Player_招式系数(1013);
		_Data_Player_招式系数_蓄力 = MyDataManager.F_Get_Player_招式系数(1011);
		_翻滚速度 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.翻滚速度;
		_FSM_聚形散气 = new DSB_FSM<PlayerState_聚形散气_State, PlayerState_聚形散气_Base>();
		_FSM_聚形散气.AddState(PlayerState_聚形散气_State.正常移动, new PlayerState_聚形散气_正常移动().F_Init(this));
		_FSM_聚形散气.AddState(PlayerState_聚形散气_State.喝酒, new PlayerState_聚形散气_喝酒().F_Init(this));
		_FSM_聚形散气.AddState(PlayerState_聚形散气_State.翻滚, new PlayerState_聚形散气_翻滚().F_Init(this));
		_FSM_聚形散气.AddState(PlayerState_聚形散气_State.跳跃, new PlayerState_聚形散气_跳跃().F_Init(this));
		_FSM_聚形散气.AddState(PlayerState_聚形散气_State.蓄力, new PlayerState_聚形散气_蓄力().F_Init(this));
	}

	public void F_FSM_聚形散气_ChangeState(PlayerState_聚形散气_State state)
	{
		_FSM_聚形散气.ChangeState(state);
	}

	public override void OnEnter()
	{
		_Timer = _Data_Player_技能._持续时间 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1702);
		_FSM_聚形散气.currState = null;
		_前摇结束 = false;
		_Data.F_设置技能CD(_Data._聚形散气TimerID, _Data_Player_技能._冷却时间);
		_Data._Curr法力 -= _Data_Player_技能._法力消耗;
		_NextState = Player_StateName_Type.正常移动;
		base._Mono组件._SR_Model.DOColor(new Color(0.4f, 0.6f, 1f, 0.9f), 0.33f);
		_EnterDrag = 5f;
		base._Rb2D.drag = 0f;
		base._Rb2D.velocity = Vector2.zero;
		_猴子猴孙_聚形散气 = Object.Instantiate(base._Mono组件._PlayerAI_猴子猴孙_聚形散气, base._Mono.transform.position, Quaternion.identity);
		base._Ani.Play("向前闪");
		if (_Data._Dir == Vector2.zero)
		{
			base._Rb2D.velocity = new Vector2((float)(_Data._朝向右边 ? 1 : (-1)) * _翻滚速度, 0f);
		}
		else
		{
			base._Rb2D.velocity = _Data._Dir * _翻滚速度;
		}
		_Data._Is聚形散气 = true;
		_Timer_前摇 = 0.3f;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_完美闪避();
	}

	private void F_结束影身效果()
	{
		_Data._Is聚形散气 = false;
		base._Mono组件._SR_Model.DOColor(new Color(1f, 1f, 1f, 1f), 0.33f);
		if (_猴子猴孙_聚形散气 != null)
		{
			_猴子猴孙_聚形散气.F_销毁();
			_猴子猴孙_聚形散气 = null;
		}
	}

	public override void OnExit()
	{
		base._Rb2D.drag = _EnterDrag;
		F_结束影身效果();
		_FSM_聚形散气.currState?.OnExit();
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_前摇结束)
		{
			_FSM_聚形散气.currState?.OnUpdate();
		}
		else
		{
			_Timer_前摇 -= Time.deltaTime;
			if (_Timer_前摇 <= 0f)
			{
				_前摇结束 = true;
				_FSM_聚形散气.ChangeState(PlayerState_聚形散气_State.正常移动);
			}
		}
		if (_Timer <= 0f && _Data._Is聚形散气)
		{
			if (_FSM_聚形散气.currStateName == PlayerState_聚形散气_State.蓄力)
			{
				F_结束影身效果();
			}
			else
			{
				ChangeToState(Player_StateName_Type.正常移动);
			}
		}
	}

	public override void F_轻攻击Down()
	{
		if (_前摇结束)
		{
			ChangeToState(Player_StateName_Type.破隐一击);
		}
	}

	public override void F_喝酒()
	{
		if (_前摇结束)
		{
			_FSM_聚形散气.currState?.F_喝酒();
		}
	}

	public override void F_翻滚()
	{
		if (_前摇结束)
		{
			_FSM_聚形散气.currState?.F_翻滚();
		}
	}

	public override void F_跳跃Down()
	{
		if (_前摇结束)
		{
			_FSM_聚形散气.currState?.F_跳跃Down();
		}
	}

	public override void F_重攻击Down()
	{
		if (_前摇结束)
		{
			_FSM_聚形散气.currState?.F_重攻击Down();
		}
	}

	public override void F_重攻击Up()
	{
		if (_前摇结束)
		{
			_FSM_聚形散气.currState?.F_重攻击Up();
		}
	}
}
