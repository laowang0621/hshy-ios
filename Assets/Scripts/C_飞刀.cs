using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class C_飞刀 : MonoBehaviour
{
	public float _飞行速度 = 20f;

	public Transform _Tsf_Model;

	public SpriteRenderer _SR_Model;

	public SpriteRenderer _SR_影子;

	public C_攻击判断 _C_攻击判断_自身;

	private float _dis_距离;

	public void F_Init_生成(Vector2 pos, Vector2 dir, float dis)
	{
		_dis_距离 = dis;
		_SR_影子.SetAlpha(0f);
		_SR_Model.SetAlpha(0f);
		_SR_Model.DOFade(1f, 0.25f);
		_Tsf_Model.position = pos;
		_Tsf_Model.right = dir;
	}

	public void F_设置伤害(float 伤害, float 击退)
	{
		_C_攻击判断_自身._伤害 = 伤害;
		_C_攻击判断_自身._击退力度 = 击退;
	}

	public void F_发射()
	{
		StartCoroutine(Cor_发射());
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}

	private IEnumerator Cor_发射()
	{
		yield return null;
		float num = _dis_距离 / _飞行速度;
		_Tsf_Model.transform.DOLocalMove(Vector2.zero, num).SetEase(Ease.InCubic);
		if (num - 0.25f > 0.1f)
		{
			yield return new WaitForSeconds(num - 0.25f);
			_SR_影子.DOFade(1f, 0.25f);
			yield return new WaitForSeconds(0.25f);
		}
		else
		{
			_SR_影子.DOFade(1f, 0.25f);
			yield return new WaitForSeconds(num);
		}
	}
}
