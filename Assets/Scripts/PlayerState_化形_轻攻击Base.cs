using System.Collections;
using UnityEngine;

public abstract class PlayerState_化形_轻攻击Base : PlayerState_化形_Base
{
	protected DataInfo_Player招式 攻击_1;

	protected DataInfo_Player招式 攻击_2;

	protected DataInfo_Player招式 攻击_3;

	protected TypeName_化形 _NextState;

	protected Coroutine _Cor_轻攻击;

	protected float _该段后摇可缩短时间;

	protected bool _已经缩短过后摇;

	public override void OnEnter()
	{
		_Data._Mono组件_化形._C_攻击判断._攻击命中回调 += F_攻击命中回调;
		_NextState = TypeName_化形.正常移动;
		_Timer = 5f;
		_已经缩短过后摇 = false;
		_该段后摇可缩短时间 = 0.1f;
		_PlayAtk();
		MyLog.LogError("OnEnter>>" + _NextState);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_Timer = 5f;
			if (_NextState == TypeName_化形.轻攻击)
			{
				_PlayAtk();
			}
			else
			{
				F_化形_ChangeState(_NextState);
			}
		}
	}

	public override void OnExit()
	{
		_Data._Mono组件_化形._C_攻击判断._攻击命中回调 -= F_攻击命中回调;
		if (_Cor_轻攻击 != null)
		{
			_Data._Mono组件_化形.StopCoroutine(_Cor_轻攻击);
			_Cor_轻攻击 = null;
		}
	}

	public override void F_轻攻击()
	{
		if (_Data._轻攻击序号 <= 3 && _NextState != TypeName_化形.轻攻击)
		{
			if (!_已经缩短过后摇)
			{
				_已经缩短过后摇 = true;
				_Timer -= _该段后摇可缩短时间;
			}
			_NextState = TypeName_化形.轻攻击;
		}
	}

	protected void F_攻击前配置(DataInfo_Player招式 player招式, string aniName, float timer_攻击时长, float timer_连段判断额外时长, float timer_可缩短后摇)
	{
		_NextState = TypeName_化形.正常移动;
		_已经缩短过后摇 = false;
		_Data.F_设置攻击(player招式);
		_Data._Mono组件_化形._Ani.Play(aniName);
		_Timer = timer_攻击时长;
		_Data._State_攻击连段_判定Timer = timer_攻击时长 + timer_连段判断额外时长;
		_该段后摇可缩短时间 = timer_可缩短后摇;
	}

	protected void _PlayAtk()
	{
		if (_Data._轻攻击序号 == 1)
		{
			_Data._轻攻击序号 = 2;
			_Cor_轻攻击 = _Data._Mono组件_化形.StartCoroutine(Cor_攻击_1());
		}
		else if (_Data._轻攻击序号 == 2)
		{
			_Data._轻攻击序号 = 3;
			_Cor_轻攻击 = _Data._Mono组件_化形.StartCoroutine(Cor_攻击_2());
		}
		else
		{
			_Data._轻攻击序号 = 1;
			_Cor_轻攻击 = _Data._Mono组件_化形.StartCoroutine(Cor_攻击_3());
		}
	}

	protected abstract IEnumerator Cor_攻击_1();

	protected abstract IEnumerator Cor_攻击_2();

	protected abstract IEnumerator Cor_攻击_3();

	protected virtual void F_攻击命中回调()
	{
		_Data._Curr蓄力 += _Data._Data_Player_招式系数._棍势值;
	}

	public override void F_翻滚()
	{
		_NextState = TypeName_化形.闪避;
	}
}
