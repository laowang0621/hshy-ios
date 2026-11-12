using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyState_沙国王_上柱子 : EnemyState_沙国王
{
	private Coroutine _Cor_攻击控制;

	private Tween tweenY;

	public override void OnEnter()
	{
		base._Mono组件._Co2D_自身碰撞器.enabled = false;
		base._Ani.Play("移动");
		_Cor_攻击控制 = base._Mono组件.StartCoroutine(Cor_上柱子());
	}

	public override void OnExit()
	{
		if (_Cor_攻击控制 != null)
		{
			base._Mono.StopCoroutine(_Cor_攻击控制);
			_Cor_攻击控制 = null;
		}
		if (tweenY != null)
		{
			tweenY.Kill();
		}
	}

	private IEnumerator Cor_上柱子()
	{
		Transform tsf = base._Mono组件._SR_Model.transform;
		tweenY = tsf.DOLocalMoveY(10f, 0.5f).SetEase(Ease.Linear);
		yield return new WaitForSeconds(0.51f);
		tweenY = tsf.DOLocalMoveY(0f, 0.5f).SetEase(Ease.Linear);
		_Data._Mono.transform.position = _mMono_沙国王._当前柱子.F_得到站立的位置();
		yield return new WaitForSeconds(0.51f);
		tweenY = null;
		ChangeState(EnemyStateName_Type.站立);
	}
}
