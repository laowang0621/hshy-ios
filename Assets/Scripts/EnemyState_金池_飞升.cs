using DG.Tweening;
using UnityEngine;

public class EnemyState_金池_飞升 : EnemyState_Boss
{
	private Color _Co_功德金光;

	private Color _Co_光圈;

	private bool _下次僵尸死亡后返回站立;

	private bool _特效已经关闭;

	private int _死亡僵尸数量;

	private EnemyAI_幽魂_金池 _Mono_金池;

	private bool _可以生成僵尸;

	private int _已经生成的数量;

	private int _最大生成数量 = 12;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_Mono_金池 = (EnemyAI_幽魂_金池)data._Mono;
		_Co_功德金光 = _Mono_金池._SR_功德金光.color;
		_Co_光圈 = _Mono_金池._SR_光圈.color;
		_Mono_金池 = (EnemyAI_幽魂_金池)data._Mono;
		return this;
	}

	public override void OnEnter()
	{
		base._Ani.Play("飞升");
		_死亡僵尸数量 = 0;
		base._Mono组件._Co2D_自身碰撞器.isTrigger = true;
		_下次僵尸死亡后返回站立 = false;
		_Data._Is飞升 = true;
		_特效已经关闭 = false;
		_可以生成僵尸 = true;
		_已经生成的数量 = 0;
		_Timer = Random.Range(2f, 4f);
		Color co_功德金光 = _Co_功德金光;
		Color co_光圈 = _Co_光圈;
		co_功德金光.a = 0f;
		co_光圈.a = 0f;
		_Mono_金池._SR_功德金光.color = co_功德金光;
		_Mono_金池._SR_光圈.color = co_光圈;
		_Mono_金池._SR_功德金光.DOFade(_Co_功德金光.a, 1f);
		_Mono_金池._SR_光圈.DOFade(_Co_光圈.a, 1f);
		_Mono_金池._GO_飞升光环.SetActive(value: true);
	}

	public override void OnExit()
	{
		base._Mono组件._Co2D_自身碰撞器.isTrigger = false;
		_Data._Is飞升 = false;
		_Mono_金池._GO_飞升光环.SetActive(value: false);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (!(_Timer <= 0f))
		{
			return;
		}
		_Timer = Random.Range(1f, 2.5f);
		if (_下次僵尸死亡后返回站立)
		{
			if (_特效已经关闭)
			{
				base._Mono.ChangeState(EnemyStateName_Type.站立);
				return;
			}
			_特效已经关闭 = true;
			_Mono_金池._SR_功德金光.DOFade(0f, 1f);
			_Mono_金池._SR_光圈.DOFade(0f, 1f);
		}
		else if (_可以生成僵尸)
		{
			_已经生成的数量++;
			if (_已经生成的数量 >= _最大生成数量)
			{
				_可以生成僵尸 = false;
			}
			_Mono_金池._僵尸生成器.F_生成一只僵尸(base._Mono.transform, F_小僵尸死亡回调);
		}
	}

	private void F_小僵尸死亡回调()
	{
		_死亡僵尸数量++;
		if (_死亡僵尸数量 >= _最大生成数量)
		{
			_下次僵尸死亡后返回站立 = true;
		}
	}
}
