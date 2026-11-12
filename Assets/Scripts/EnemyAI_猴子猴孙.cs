using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_猴子猴孙 : EnemyAI_Base<EnemyAI_猴子猴孙, EnemyState_猴子猴孙, Enemy_StateData_猴子猴孙>
{
	public GameObject _GO_完美闪避残影Prefab;

	public GameObject _GO_烟雾特效Prefab;

	public Player_蓄力特效 _Player_蓄力特效;

	public ParticleSystem _PS_识破特效;

	public Transform _Tsf_策反特效Pos;

	protected bool _已经进入初始化;

	private float _持续时间 = 1f;

	public override int _阵营 { get; set; } = 1;


	public override float _连续移动撞墙次数
	{
		get
		{
			return _m连续移动撞墙次数;
		}
		set
		{
			_m连续移动撞墙次数 = value;
			if (_m连续移动撞墙次数 >= 5f)
			{
				_m连续移动撞墙次数 = 0f;
				_Data.F_隐藏();
				ChangeState(EnemyStateName_Type.站立);
				_Data._Is虚幻 = true;
				_Data._Mono组件._SR_Model.DOFade(0f, 0.75f).OnComplete(delegate
				{
					_Data._Is虚幻 = false;
					base.transform.position = _Data._PC.transform.position;
					_Data._Mono组件._SR_Model.DOFade(1f, 0.5f);
				});
				MyLog.LogError("撞墙多次，刷新回玩家位置");
			}
		}
	}

	public void F_策反()
	{
		if (!_Data._Is死亡)
		{
			ChangeState(EnemyStateName_Type.受击);
			base.gameObject.layer = 6;
			_阵营 = 2;
			_Data._Mono组件._C_攻击判断._阵营 = 2;
			_Data._Mono组件._视野范围.gameObject.SetActive(value: false);
			_Data._Mono组件._攻击范围.gameObject.SetActive(value: false);
			_Data._Mono组件._Co2D_自身碰撞器.enabled = false;
			if ((bool)(_Data._Mono组件._视野范围 as C_AI_范围检测_猴子猴孙))
			{
				((C_AI_范围检测_猴子猴孙)_Data._Mono组件._视野范围)._敌人层级 = 3;
				((C_AI_范围检测_猴子猴孙)_Data._Mono组件._攻击范围)._敌人层级 = 3;
			}
			DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 5f, delegate
			{
				base.gameObject.tag = "Untagged";
				_Data._Mono组件._视野范围.gameObject.SetActive(value: true);
				_Data._Mono组件._攻击范围.gameObject.SetActive(value: true);
				_Data._Mono组件._Co2D_自身碰撞器.enabled = true;
			}, 0f, DSB_UpdateTimeMode.帧计算);
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_Data._Is死亡 && !_Data._Is虚幻)
		{
			MyLog.LogError($"伤害>>{attackInfo.atk_生命},,,当前生命>>{_Data._Curr生命}/{_Data._MaxHP}");
			_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
			MyLog.Log($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
			base._BeAttack_Pos = attackInfo.atk_Pos;
			base._BeAttack_击退力度 = attackInfo.atk_击退力度;
			if (_Cor_受击变色 != null)
			{
				StopCoroutine(_Cor_受击变色);
			}
			_Cor_受击变色 = StartCoroutine(Cor_受击变色());
			_Data._Curr生命 -= attackInfo.atk_生命;
			_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
			if (_Data._Curr生命 <= 0f)
			{
				ChangeState(EnemyStateName_Type.死亡);
			}
			else if (attackInfo.atk_有僵直)
			{
				ChangeState(EnemyStateName_Type.受击);
			}
		}
	}

	protected override void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		if (_Data._Mono组件._Tsf_移动目标 != null)
		{
			Object.Destroy(_Data._Mono组件._Tsf_移动目标.gameObject);
		}
		_Data.F_删除血条();
	}

	private void F_On_Event_切换地图(DSB_EventArgs args)
	{
		Object.Destroy(base.gameObject);
	}

	public override void Init()
	{
		if (_已经进入初始化)
		{
			MyLog.LogError(base.name + ">>已经初始化！");
			return;
		}
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
		_已经进入初始化 = true;
		_ = _Data;
		_Data.F_重置死亡状态();
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_猴子猴孙_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_猴子猴孙_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_猴子猴孙_攻击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_猴子猴孙_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_猴子猴孙_死亡().Init(_Data));
		MyLog.LogError("初始化<<" + base.name + ">>结束");
	}

	public void F_绑定玩家(PlayerController pc, float 持续时间, DataInfo_猴子猴孙等级 levelData)
	{
		_Data._MaxHP = levelData._生命;
		_Data._Atk = levelData._攻击;
		_Data._Mono组件._C_攻击判断._伤害 = _Data._Atk;
		_Data.F_显示();
		_Data._PC = pc;
		_持续时间 = 持续时间;
		ChangeState(EnemyStateName_Type.站立);
	}

	protected override void Update()
	{
		_持续时间 -= Time.deltaTime;
		if (_持续时间 <= 0f)
		{
			Object.Instantiate(_GO_烟雾特效Prefab, base.transform.position + new Vector3(0f, 1.15f), Quaternion.identity);
			Object.Destroy(base.gameObject);
		}
		else
		{
			base.Update();
		}
	}

	public bool F_PlayerMove(Vector2 dir, float speed, bool can转向 = true)
	{
		_Data._Dir = dir;
		_Data._Mono组件._Rb2D.velocity = Vector2.Lerp(_Data._Mono组件._Rb2D.velocity, speed * _Data._Dir, Time.deltaTime * 20f);
		if (can转向)
		{
			F_玩家转向(dir.x);
		}
		if (dir.x == 0f && dir.y == 0f)
		{
			return false;
		}
		return true;
	}

	public void F_玩家转向(float x)
	{
		if (x > 0f && !_Data._朝向右边)
		{
			_Data._朝向右边 = true;
			float num = Mathf.Abs(base.transform.localScale.x);
			base.transform.localScale = new Vector3(num, num, num);
		}
		else if (x < 0f && _Data._朝向右边)
		{
			_Data._朝向右边 = false;
			float num2 = Mathf.Abs(base.transform.localScale.x);
			base.transform.localScale = new Vector3(0f - num2, num2, num2);
		}
	}
}
