using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class C_特效_残影残留_小黄龙Pro : MonoBehaviour
{
	public float _残影间隔 = 0.1f;

	public float _残影持续时间 = 0.5f;

	public SpriteRenderer _SR;

	protected Coroutine _AAA;

	public List<Color> _List_Color;

	private int _CurrIndex;

	private void OnEnable()
	{
		_AAA = StartCoroutine(AAA());
	}

	public void F_停止生成()
	{
		if (_AAA != null)
		{
			StopCoroutine(_AAA);
		}
	}

	protected virtual IEnumerator AAA()
	{
		yield return new WaitForEndOfFrame();
		WaitForSeconds time1 = new WaitForSeconds(_残影间隔);
		while (true)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = base.transform.position;
			gameObject.transform.rotation = base.transform.rotation;
			gameObject.transform.localScale = base.transform.lossyScale;
			SpriteRenderer goSR = gameObject.AddComponent<SpriteRenderer>();
			gameObject.AddComponent<SortingGroup>();
			goSR.sprite = _SR.sprite;
			_CurrIndex++;
			if (_CurrIndex >= _List_Color.Count)
			{
				_CurrIndex = 0;
			}
			goSR.color = _List_Color[_CurrIndex];
			goSR.renderingLayerMask = _SR.renderingLayerMask;
			goSR.sortingOrder = _SR.sortingOrder;
			goSR.material = _SR.sharedMaterial;
			goSR.DOFade(0f, _残影持续时间).OnComplete(delegate
			{
				Object.Destroy(goSR.gameObject);
			});
			yield return time1;
		}
	}
}
