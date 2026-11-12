using DG.Tweening;
using DSB;
using UnityEngine;

public class C_弹道_电疗针 : C_弹道
{
	[SerializeField]
	protected Transform _Tsf_Model;

	[SerializeField]
	protected SpriteRenderer _SR;

	private Tween tween;

	public void F_Init_初始状态()
	{
		_Col2D.enabled = false;
		_SR.SetAlpha(0f);
		_SR.DOFade(1f, 0.23f);
	}

	public void F_Init_第一次瞄准(Vector2 dir)
	{
		Vector2 startRight = _Tsf_Model.right;
		tween = DOTween.To(() => 0f, delegate(float x)
		{
			_Tsf_Model.right = Vector2.Lerp(startRight, dir, x);
		}, 1f, 0.25f);
	}

	public void F_Init_发射(Vector2 dir, float 攻击力)
	{
		if (tween != null)
		{
			tween.Kill();
			Vector2 startRight = _Tsf_Model.right;
			DOTween.To(() => 0f, delegate(float x)
			{
				_Tsf_Model.right = Vector2.Lerp(startRight, dir, x);
			}, 1f, 0.1f);
		}
		_Tsf_Model.right = dir;
		_Col2D.enabled = true;
		base.F_Init(dir, 攻击力);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_发射();
	}

	public void F_Init_直接转向_直接发射(Vector2 dir, float 攻击力)
	{
		_Col2D.enabled = false;
		_SR.SetAlpha(0f);
		_Tsf_Model.right = dir;
		_SR.DOFade(1f, 0.2f).OnComplete(delegate
		{
			_Col2D.enabled = true;
			base.F_Init(dir, 攻击力);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_发射();
		});
	}

	public override void F_Init(Vector2 dir, float 攻击力)
	{
		_Col2D.enabled = false;
		_SR.SetAlpha(0f);
		Vector2 startRight = _Tsf_Model.right;
		_SR.DOFade(1f, 0.23f);
		DOTween.To(() => 0f, delegate(float x)
		{
			_Tsf_Model.right = Vector2.Lerp(startRight, dir, x);
		}, 1f, 0.45f).OnComplete(delegate
		{
			_Col2D.enabled = true;
			base.F_Init(dir, 攻击力);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_发射();
		});
	}
}
