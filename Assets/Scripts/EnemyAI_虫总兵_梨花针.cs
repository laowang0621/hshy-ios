using DG.Tweening;
using UnityEngine;

public class EnemyAI_虫总兵_梨花针 : C_弹道
{
	[SerializeField]
	protected SpriteRenderer _SR_Model;

	private bool _有伤害;

	private bool _开启旋转;

	private Vector2 _Pos_上一帧位置;

	private void Start()
	{
		_有伤害 = false;
		_Col2D.enabled = false;
	}

	private void FixedUpdate()
	{
		if (!_有伤害 && _SR_Model.transform.localPosition.y < 2f)
		{
			_有伤害 = true;
			_Col2D.enabled = true;
		}
		if (_开启旋转)
		{
			Vector2 vector = _SR_Model.transform.position;
			Vector2 vector2 = vector - _Pos_上一帧位置;
			if (vector2.sqrMagnitude > 0.01f)
			{
				_SR_Model.transform.right = vector2.normalized;
			}
			_Pos_上一帧位置 = vector;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_SR_Model.transform.DOKill();
		base.transform.DOKill();
	}

	public override void F_Init(Vector2 目标点, float 伤害)
	{
		_有伤害 = true;
		_Pos_上一帧位置 = _SR_Model.transform.position;
		_开启旋转 = true;
		Vector2 vector = base.transform.position;
		Vector2 vector2 = 目标点 - vector;
		float magnitude = vector2.magnitude;
		_ = vector2 / magnitude;
		float num = magnitude / _speed;
		float time_1 = num / 2f;
		float endValue = Mathf.Clamp(magnitude / 2f, 2f, 8f);
		base.transform.DOMove(目标点, num).SetEase(Ease.Linear);
		_SR_Model.transform.DOLocalMoveY(endValue, time_1).SetEase(Ease.OutSine).OnComplete(delegate
		{
			_SR_Model.transform.DOLocalMoveY(0.2f, time_1).SetEase(Ease.InSine).OnComplete(delegate
			{
				_开启旋转 = false;
				_Col2D.enabled = false;
				_SR_Model.DOFade(0f, 0.75f).OnComplete(delegate
				{
					Object.Destroy(base.gameObject);
				});
			});
		});
		_伤害 = 伤害;
	}
}
