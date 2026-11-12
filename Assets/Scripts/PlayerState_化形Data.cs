using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class PlayerState_化形Data : IStateData
{
	public EnemyMono组件 _Mono组件_化形;

	public PlayerMono组件 _Mono组件Player;

	public PlayerController _MonoPlayer;

	public PlayerState_化形 _PlayerState_化形;

	public int _化形ID;

	public DataInfo_Player招式 _Data_Player_招式系数;

	[Obsolete]
	public Dictionary<string, float> _Dict_Anis_name_time;

	private float _mCurr生命;

	private float _mCurr神力;

	private float _mCurr蓄力;

	private float _化形_攻击力;

	private float _mMax生命;

	private float _mMax神力;

	private float _mMax蓄力;

	public bool _释放蓄力状态;

	public bool _不可受击状态;

	public Enum_化形_重攻击 _重击选项;

	public float _闪避速度;

	public float _闪避持续时间;

	public float _移动速度;

	public float _闪避消耗神力;

	public int _Ani_移动ID;

	public int _Ani_站立ID;

	public int _轻攻击序号
	{
		get
		{
			return _PlayerState_化形._轻攻击序号;
		}
		set
		{
			_PlayerState_化形._轻攻击序号 = value;
		}
	}

	public bool _重攻击衔接切手技
	{
		get
		{
			return _PlayerState_化形._重攻击衔接切手技;
		}
		set
		{
			_PlayerState_化形._重攻击衔接切手技 = value;
		}
	}

	public float _State_攻击连段_判定Timer
	{
		get
		{
			return _PlayerState_化形._State_攻击连段_判定Timer;
		}
		set
		{
			_PlayerState_化形._State_攻击连段_判定Timer = value;
		}
	}

	public bool _朝向右边 => _MonoPlayer._Data._朝向右边;

	public Rigidbody2D _Rb2D => _MonoPlayer._Data._Mono组件._Rb2D;

	public Vector2 _Dir => _MonoPlayer._Data._Dir;

	public float _Curr生命
	{
		get
		{
			return _mCurr生命;
		}
		set
		{
			_mCurr生命 = value;
			EventArgs_Player状态提示_生命过低.Trigger(_mCurr生命 / _mMax生命 < 0.2f);
			EventArgs_Player_化形_生命_Change.Trigger(_mCurr生命, 1f, _mMax生命);
		}
	}

	public float _Curr神力
	{
		get
		{
			return _mCurr神力;
		}
		set
		{
			_mCurr神力 = value;
			EventArgs_Player_化形_神力_Change.Trigger(_mCurr神力, 1f, _mMax神力);
		}
	}

	public float _Curr蓄力
	{
		get
		{
			return _mCurr蓄力;
		}
		set
		{
			_mCurr蓄力 = Mathf.Clamp(value, 0f, _mMax蓄力);
			MyLog.LogWarning($"蓄力>>>{_mCurr蓄力}");
			EventArgs_Player_化形_蓄力_Change.Trigger(_mCurr蓄力, 1f, _mMax蓄力);
		}
	}

	public float _Max蓄力 => _mMax蓄力;

	public bool _Can大招 => _mCurr蓄力 >= _mMax蓄力 - 0.2f;

	public void F_Init()
	{
		_Ani_移动ID = Animator.StringToHash("移动");
		_Ani_站立ID = Animator.StringToHash("站立");
		F_获取基础数值();
		_mCurr生命 = _mMax生命;
		_mCurr神力 = _mMax神力;
		_mCurr蓄力 = 0f;
		_释放蓄力状态 = false;
		_不可受击状态 = false;
		_重击选项 = Enum_化形_重攻击.普通;
	}

	private void F_获取基础数值()
	{
		_mMax神力 = _PlayerState_化形._Data_Player_技能._持续时间 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1601);
		float num = DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(106);
		DataInfo_Player招式 dataInfo_Player招式 = MyDataManager.F_Get_Player_招式系数(_化形ID * 100 + 50);
		DataInfo_Player招式 dataInfo_Player招式2 = MyDataManager.F_Get_Player_招式系数(_化形ID * 100 + 51);
		float num2 = 1f + num * dataInfo_Player招式2._僵直伤害;
		_mMax生命 = _MonoPlayer._Data._Max生命 * dataInfo_Player招式._伤害系数 * num2;
		_化形_攻击力 = _MonoPlayer._Data._攻击力 * dataInfo_Player招式._气力消耗 * num2;
		_mMax蓄力 = dataInfo_Player招式._僵直伤害;
		_闪避速度 = dataInfo_Player招式._棍势值;
		_闪避持续时间 = dataInfo_Player招式._击退力度;
		_移动速度 = dataInfo_Player招式2._伤害系数;
		_闪避消耗神力 = dataInfo_Player招式2._气力消耗;
		MyLog.LogWarning($"当前化形加点强度:{num}>>生命值:{_mMax生命}>>攻击力:{_化形_攻击力}");
	}

	public void F_退出化形()
	{
		_PlayerState_化形.F_退出化形();
	}

	public void F_Move()
	{
		if (_PlayerState_化形.F_PlayerMove(_移动速度))
		{
			_Mono组件_化形._Ani.Play(_Ani_移动ID);
		}
		else
		{
			_Mono组件_化形._Ani.Play(_Ani_站立ID);
		}
	}

	public void F_Move_不播放动画()
	{
		_PlayerState_化形.F_PlayerMove(_移动速度);
	}

	[Obsolete]
	public void F_获取动画长度()
	{
	}

	[Obsolete]
	public void F_设置攻击()
	{
		F_设置攻击(_Data_Player_招式系数);
	}

	public void F_设置攻击(DataInfo_Player招式 player招式)
	{
		_Data_Player_招式系数 = player招式;
		_Mono组件_化形._C_攻击判断._伤害 = _化形_攻击力 * player招式._伤害系数;
		_Mono组件_化形._C_攻击判断._僵直伤害 = player招式._僵直伤害;
		_Mono组件_化形._C_攻击判断._击退力度 = player招式._击退力度;
		_Curr神力 -= player招式._气力消耗;
	}

	public virtual void BeAttack(AttackInfo attackInfo)
	{
		if (_不可受击状态)
		{
			return;
		}
		_MonoPlayer._BeAttack_Pos = attackInfo.atk_Pos;
		_MonoPlayer._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (_释放蓄力状态)
		{
			_Curr生命 -= attackInfo.atk_生命 * _MonoPlayer._Data.蓄力状态承受伤害;
			if (_Curr生命 <= 0f)
			{
				F_退出化形();
			}
			return;
		}
		_Curr生命 -= attackInfo.atk_生命;
		if (_Curr生命 <= 0f)
		{
			F_退出化形();
		}
		else if (attackInfo.atk_有僵直)
		{
			_PlayerState_化形.F_化形_ChangeState(TypeName_化形.受击);
		}
	}
}
