using System.Collections;
using UnityEngine;

public class C_烟花 : MonoBehaviour
{
	public C_烟花_控制器 _C_烟花_控制器;

	public SpriteRenderer _SR_烟花_1;

	public SpriteRenderer _SR_烟花_2;

	public SpriteRenderer _SR_烟花_3;

	public Animator _Ani;

	public bool _Is_大烟花 = true;

	private Coroutine _Cor_烟花;

	private void OnEnable()
	{
		if (_Cor_烟花 != null)
		{
			StopCoroutine(_Cor_烟花);
		}
		_Cor_烟花 = StartCoroutine(Cor_烟花());
	}

	private void OnDisable()
	{
		if (_Cor_烟花 != null)
		{
			StopCoroutine(_Cor_烟花);
			_Cor_烟花 = null;
		}
	}

	private IEnumerator Cor_烟花()
	{
		_Ani.Play("停止", 0, 0f);
		yield return new WaitForSeconds(Random.Range(2f, 4f));
		while (true)
		{
			_Ani.Play((Random.value < 0.5f) ? "烟花" : "亮闪闪", 0, 0f);
			yield return new WaitForSeconds(Random.Range(2f, 4f));
		}
	}

	public void F_Ani_烟花()
	{
		if (_Is_大烟花)
		{
			var (sprite, sprite2, sprite3) = _C_烟花_控制器.F_得到烟花();
			_SR_烟花_1.sprite = sprite;
			_SR_烟花_2.sprite = sprite2;
			_SR_烟花_3.sprite = sprite3;
		}
		else
		{
			_SR_烟花_1.sprite = _C_烟花_控制器.F_得到小烟花();
			_SR_烟花_2.sprite = null;
			_SR_烟花_3.sprite = null;
		}
		base.transform.position = _C_烟花_控制器.F_得到随机烟花位置();
	}

	public void F_Ani_亮闪闪()
	{
		_SR_烟花_1.sprite = null;
		_SR_烟花_2.sprite = null;
		_SR_烟花_3.sprite = _C_烟花_控制器.F_得到亮闪闪();
		base.transform.position = _C_烟花_控制器.F_得到随机烟花位置();
	}
}
