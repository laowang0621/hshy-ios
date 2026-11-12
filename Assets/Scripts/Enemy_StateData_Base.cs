using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

[Serializable]
public class Enemy_StateData_Base<TAI, TState, TStateData> : IStateData where TAI : EnemyAI_Base<TAI, TState, TStateData> where TState : EnemyState_Base<TAI, TState, TStateData> where TStateData : Enemy_StateData_Base<TAI, TState, TStateData>
{
	public TAI _Mono;

	protected EnemyMono组件 _mMono组件;

	public float _StartDrag;

	public float _MaxHP;

	public float _Curr生命;

	public float _Atk;

	public float _僵直数值上限;

	public float _僵直数值增长;

	public float _Max僵直数值;

	public float _Curr僵直数值;

	public float _击杀经验值;

	public float _攻击欲望 = 80f;

	public float _站立时间_x;

	public float _站立时间_y;

	public float _追击时间_x;

	public float _追击时间_y;

	public Vector2 _StartPos;

	public Vector2 _StartScale;

	public bool _Is死亡;

	public bool _Is虚幻;

	public ItemUI_EnemyHP _UI_HP;

	public Dictionary<string, float> _Dict_Anis_name_time;

	public EnemyMono组件 _Mono组件
	{
		get
		{
			if (_mMono组件 == null)
			{
				_mMono组件 = _Mono.GetComponent<EnemyMono组件>();
				if (_mMono组件 == null)
				{
					throw new Exception("没有<<EnemyMono组件>>");
				}
			}
			return _mMono组件;
		}
	}

	public virtual bool _视野范围有敌人
	{
		get
		{
			if (!_Mono组件._视野范围._范围内发现敌人)
			{
				return _Mono组件._攻击范围._范围内发现敌人;
			}
			return true;
		}
	}

	public virtual bool _攻击范围有敌人 => _Mono组件._攻击范围._范围内发现敌人;

	public virtual Vector3 _Pos_追击目标位置 => _Mono组件._视野范围._目标位置;

	public Enemy_StateData_Base(TAI mono)
	{
		_Mono = mono;
		_ = _Mono组件 == null;
		_StartDrag = _Mono组件._Rb2D.drag;
		F_初始化();
	}

	public void F_强制清空追击()
	{
		_Mono组件._视野范围?.Clear();
		_Mono组件._攻击范围?.Clear();
	}

	protected void F_初始化()
	{
		DataInfo_Enemy基础数值 dataInfo_Enemy基础数值 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_基础数值((int)_Mono组件._EnemyID);
		_MaxHP = dataInfo_Enemy基础数值._生命值 * DSB_MonoSingleton<MyDataManager>.Instance._周目加成;
		_Atk = dataInfo_Enemy基础数值._攻击力 * DSB_MonoSingleton<MyDataManager>.Instance._周目加成;
		_僵直数值上限 = dataInfo_Enemy基础数值._僵直上限;
		_僵直数值增长 = dataInfo_Enemy基础数值._僵直增幅;
		_Max僵直数值 = dataInfo_Enemy基础数值._僵直数值;
		_击杀经验值 = dataInfo_Enemy基础数值._击杀经验值;
		_站立时间_x = dataInfo_Enemy基础数值._站立时间_x;
		_站立时间_y = dataInfo_Enemy基础数值._站立时间_y;
		_追击时间_x = dataInfo_Enemy基础数值._追击时间_x;
		_追击时间_y = dataInfo_Enemy基础数值._追击时间_y;
		_Dict_Anis_name_time = new Dictionary<string, float>();
		AnimationClip[] animationClips = _Mono组件._Ani.runtimeAnimatorController.animationClips;
		foreach (AnimationClip animationClip in animationClips)
		{
			_Dict_Anis_name_time.TryAdd(animationClip.name, animationClip.length);
		}
		F_获取血条UI();
		_Mono组件._C_攻击判断._伤害 = _Atk;
		_Mono组件._C_攻击判断._击退力度 = 10f;
		_StartPos = _Mono.transform.position;
		_StartScale = _Mono.transform.localScale;
		_Mono组件._C_攻击判断._阵营 = _Mono._阵营;
		if (_Mono组件.transform.parent == null)
		{
			_Mono组件._Tsf_移动目标.parent = null;
		}
		else
		{
			_Mono组件._Tsf_移动目标.parent = _Mono组件.transform.parent.parent;
		}
		_Mono._FSM.OnStateChange += delegate(EnemyStateName_Type a, EnemyStateName_Type b)
		{
			_Mono.当前状态_仅显示 = b;
		};
	}

	public virtual void F_获取血条UI()
	{
		_UI_HP = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_EnemyHP();
		_UI_HP.F_SetVal(1f, 1f);
	}

	public virtual void F_设置UI血量(float currHP, float maxHP, bool _可见性 = true)
	{
		if (!(_UI_HP == null))
		{
			_UI_HP._可见性 = _可见性;
			_UI_HP.F_SetVal(currHP, maxHP);
		}
	}

	public virtual void F_设置UI位置(Vector2 pos)
	{
		if (!(_UI_HP == null))
		{
			_UI_HP.F_SetPos(pos);
		}
	}

	public virtual void F_重置死亡状态()
	{
		_Is死亡 = false;
	}

	public virtual void F_显示()
	{
		_Mono._Is被定 = false;
		_Mono组件._Co2D_自身碰撞器.enabled = true;
		_Is虚幻 = false;
		_Curr僵直数值 = 0f;
		_Curr生命 = _MaxHP;
		_Mono.transform.position = _StartPos;
		_Mono组件._SR_Model.SetAlpha(1f);
		_Mono组件._SR_影子.SetAlpha(1f);
		F_强制清空追击();
		F_设置UI血量(_Curr生命, _MaxHP, _可见性: false);
		_Mono组件._Tsf_移动目标.gameObject.SetActive(value: true);
	}

	public virtual void F_隐藏()
	{
		_Mono.transform.position = _StartPos;
		F_强制清空追击();
		_Mono组件._Tsf_移动目标.gameObject.SetActive(value: false);
		_UI_HP._可见性 = false;
	}

	public virtual void F_销毁()
	{
		if (_UI_HP != null)
		{
			UnityEngine.Object.Destroy(_UI_HP.gameObject);
		}
	}
}
