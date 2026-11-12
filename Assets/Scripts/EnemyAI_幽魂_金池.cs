using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_幽魂_金池 : EnemyAI_幽魂
{
	public Enemy_生成_金池_僵尸 _僵尸生成器;

	public GameObject _GO_飞升光环;

	public SpriteRenderer _SR_功德金光;

	public SpriteRenderer _SR_光圈;

	private AudioSource _AudioS_金池长老入场;

	private bool _已经飞升;

	private bool _音效已经销毁;

	public override Enum_对话 _对话类型_见面 => Enum_对话.NULL;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.击败金池长老后;

	public override void F_添加状态()
	{
		base.F_添加状态();
		_FSM.states.Add(EnemyStateName_Type.飞升, new EnemyState_金池_飞升().Init(_Data));
		_已经飞升 = false;
		_AudioS_金池长老入场 = DSB_MonoSingleton<AudioManager>.Instance.F_GO_Play_SFX_金池长老入场();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_Player_死亡._EventID, F_销毁音效);
	}

	private void F_销毁音效(DSB_EventArgs args)
	{
		if (!_音效已经销毁 && !(_AudioS_金池长老入场 == null))
		{
			_音效已经销毁 = true;
			_AudioS_金池长老入场.DOFade(0f, 3f).SetEase(Ease.Linear).OnComplete(delegate
			{
				Object.Destroy(_AudioS_金池长老入场.gameObject);
			});
		}
	}

	protected override void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_Player_死亡._EventID, F_销毁音效);
		F_销毁音效(null);
		if (_僵尸生成器 != null)
		{
			Object.Destroy(_僵尸生成器.gameObject);
		}
		base.OnDestroy();
	}

	public override void F_定身(float 时间)
	{
		if (!_Data._Is飞升)
		{
			base.F_定身(时间);
		}
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (_Data._Is飞升 || _Data._Is死亡 || _Data._Is虚幻)
		{
			return;
		}
		_Data._Mono组件._Rb2D.drag = _Data._StartDrag;
		MyLog.LogWarning($"<<{base.name}>>被攻击<<{attackInfo.atk_生命}>>");
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
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
			ChangeState(EnemyStateName_Type.死亡);
			return;
		}
		if (!_已经飞升 && _Data._Curr生命 <= _Data._MaxHP / 2f)
		{
			_已经飞升 = true;
			ChangeState(EnemyStateName_Type.飞升);
			return;
		}
		_Data._Curr僵直数值 += attackInfo.atk_僵直;
		if (_Data._Curr僵直数值 >= _Data._Max僵直数值)
		{
			_Data._Curr僵直数值 = 0f;
			_Data._Max僵直数值 += _Data._僵直数值增长;
			ChangeState(EnemyStateName_Type.受击);
		}
		else
		{
			_Data._Mono组件._Rb2D.velocity = 1.1f * base._BeAttack_击退力度 * new Vector2((base.transform.position.x > base._BeAttack_Pos.x) ? 1 : (-1), 0f);
		}
	}

	public void F_接触僵尸_回复生命()
	{
		_Data._Curr生命 += _Data._MaxHP * 0.075f;
		_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
	}
}
