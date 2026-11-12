using DSB;
using UnityEngine;

public class PlayerState_定身术 : Player_StateBase
{
	private float _定身时间;

	private float _Time1 = 0.18f;

	private float _Time2 = 0.38f;

	private float _Timer1;

	private bool _成功释放;

	public PlayerState_定身术()
	{
		_Data_Player_技能 = MyDataManager.F_Get_Player_技能(101);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("定身术");
		_Timer1 = _Time1;
		_Timer = _Time2 - _Time1;
		_NextState = Player_StateName_Type.正常移动;
		_成功释放 = false;
		_Data.F_设置技能CD(_Data._定身术TimerID, _Data_Player_技能._冷却时间);
		_Data._Curr法力 -= _Data_Player_技能._法力消耗;
		_定身时间 = _Data_Player_技能._持续时间 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1401);
	}

	public override void OnExit()
	{
		if (!_成功释放)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_力竭();
		}
	}

	public override void OnUpdate()
	{
		if (_Timer1 > 0f)
		{
			_Timer1 -= Time.deltaTime;
			if (!(_Timer1 <= 0f))
			{
				return;
			}
			_成功释放 = true;
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_定身术();
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(104) > 0.5f)
			{
				if (_Data._List_定身目标 == null)
				{
					return;
				}
				foreach (I定身术 item in _Data._List_定身目标)
				{
					if (item.F_得到阵营() != base._Mono._阵营)
					{
						item.F_定身(_定身时间);
					}
				}
				_Data._List_定身目标 = null;
			}
			else if (_Data._定身目标 != null)
			{
				if (_Data._定身目标.F_得到阵营() != base._Mono._阵营)
				{
					_Data._定身目标.F_定身(_定身时间);
				}
				_Data._定身目标 = null;
			}
		}
		else
		{
			_Timer -= Time.deltaTime;
			if (_Timer <= 0f)
			{
				ChangeState(_NextState);
			}
		}
	}
}
