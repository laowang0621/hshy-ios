using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class C_弹道_骨灵精 : C_弹道
{
	[SerializeField]
	protected SpriteRenderer _SR_Model;

	[SerializeField]
	protected List<Sprite> _List_投射物品;

	private bool _有伤害;

	private void Start()
	{
		_SR_Model.sprite = _List_投射物品[Random.Range(0, _List_投射物品.Count)];
	}

	private void FixedUpdate()
	{
		_SR_Model.transform.Rotate(Vector3.forward, -8f);
		if (_有伤害)
		{
			if (_SR_Model.transform.localPosition.y > 2f)
			{
				_有伤害 = false;
				_Col2D.enabled = false;
			}
		}
		else if (_SR_Model.transform.localPosition.y < 2f)
		{
			_有伤害 = true;
			_Col2D.enabled = true;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_SR_Model.transform.DOKill();
	}

	public override void F_Init(Vector2 目标点, float 伤害)
	{
		_有伤害 = true;
		Vector2 vector = 目标点 - (Vector2)base.transform.position;
		float magnitude = vector.magnitude;
		Vector2 vector2 = vector / magnitude;
		_RB2D.velocity = vector2 * _speed;
		float time1 = magnitude / _speed / 2f;
		float endValue = Mathf.Clamp(magnitude / 2f, 1f, 6f);
		_SR_Model.transform.DOLocalMoveY(endValue, time1).SetEase(Ease.OutSine).OnComplete(delegate
		{
			_SR_Model.transform.DOLocalMoveY(0.6f, time1).SetEase(Ease.InOutCubic).OnComplete(delegate
			{
				Object.Destroy(base.gameObject);
			});
		});
		_伤害 = 伤害;
	}
}
