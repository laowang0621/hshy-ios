using UnityEngine;

public class ItemUI_Player_生命值 : ItemUI_生命值
{
	public Color _虚_扣血;

	public Color _虚_回血;

	private bool _is扣血;

	protected override void Update()
	{
		if (_is扣血)
		{
			_Img_虚.fillAmount = Mathf.Lerp(_Img_虚.fillAmount, _Img_实.fillAmount, Time.deltaTime * 3f);
		}
		else
		{
			_Img_实.fillAmount = Mathf.Lerp(_Img_实.fillAmount, _Img_虚.fillAmount, Time.deltaTime * 3f);
		}
	}

	public override void F_SetVal(float currVal, float maxVal)
	{
		float num = currVal / maxVal;
		float fillAmount = _Img_实.fillAmount;
		if (num >= fillAmount)
		{
			_is扣血 = false;
			_Img_虚.color = _虚_回血;
			_Img_虚.fillAmount = num;
		}
		else
		{
			_is扣血 = true;
			_Img_虚.color = _虚_扣血;
			_Img_实.fillAmount = num;
		}
	}
}
