using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class C_亢金_雷枪 : MonoBehaviour
{
	public float _飞行速度 = 20f;

	public Transform _Tsf_雷枪;

	public SpriteRenderer _SR_雷枪;

	public SpriteRenderer _SR_影子;

	public C_攻击判断 _C_攻击判断_自身;

	public C_攻击判断 _C_电圈_Prefab;

	private float _dis_距离;

	private float _电圈_伤害;

	private float _电圈_击退;

	public void F_Init_生成(Vector2 pos_雷枪, Vector2 dir_雷枪, float dis_距离)
	{
		_dis_距离 = dis_距离;
		_SR_影子.SetAlpha(0f);
		_SR_雷枪.SetAlpha(0f);
		_SR_雷枪.DOFade(1f, 0.25f);
		_Tsf_雷枪.position = pos_雷枪;
		_Tsf_雷枪.right = dir_雷枪;
	}

	public void F_设置伤害(float 雷枪_伤害, float 雷枪_击退, float 电圈_伤害, float 电圈_击退)
	{
		_C_攻击判断_自身._伤害 = 雷枪_伤害;
		_C_攻击判断_自身._击退力度 = 雷枪_击退;
		_电圈_伤害 = 电圈_伤害;
		_电圈_击退 = 电圈_击退;
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
		_Tsf_雷枪.transform.DOLocalMove(Vector2.zero, num).SetEase(Ease.InCubic);
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
		F_生成电圈(base.transform.position);
		int count = Random.Range(2, 5);
		for (int i = 0; i < count; i++)
		{
			yield return new WaitForSeconds(1.75f);
			F_生成电圈(base.transform.position);
		}
		yield return new WaitForSeconds(0.75f);
		_SR_雷枪.DOFade(0f, 0.45f);
		_SR_影子.DOFade(0f, 0.45f);
		yield return new WaitForSeconds(0.55f);
		Object.Destroy(base.gameObject);
	}

	private void F_生成电圈(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(_C_电圈_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = _电圈_伤害;
		c_攻击判断._击退力度 = _电圈_击退;
		c_攻击判断._是地面伤害 = false;
	}
}
