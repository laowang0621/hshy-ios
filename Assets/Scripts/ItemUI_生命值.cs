using UnityEngine;
using UnityEngine.UI;

public class ItemUI_生命值 : ItemUI_能量条
{
	[SerializeField]
	protected Image _Img_虚;

	protected virtual void Update()
	{
		_Img_虚.fillAmount = Mathf.Lerp(_Img_虚.fillAmount, _Img_实.fillAmount, Time.deltaTime * 3f);
	}
}
