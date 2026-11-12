using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class C_UI_蓄力 : MonoBehaviour
{
	[SerializeField]
	private Image _Img_实;

	[SerializeField]
	private List<Image> _List_豆子图片;

	public GameObject _豆子锁1;

	public GameObject _豆子锁2;

	public void F_SetVal(float currVal, float maxVal)
	{
		_Img_实.fillAmount = currVal / maxVal;
	}

	public void F_Set豆子(int val)
	{
		int i;
		for (i = 0; i < val; i++)
		{
			_List_豆子图片[i].DOFade(1f, 0.3f);
		}
		for (; i < _List_豆子图片.Count; i++)
		{
			_List_豆子图片[i].DOFade(0f, 0.3f);
		}
	}
}
