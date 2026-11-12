using UnityEngine;
using UnityEngine.UI;

public class ItemUI_Player_气力 : ItemUI_能量条
{
	[SerializeField]
	public Image _Img_BG;

	private float _目标_BG = 1f;

	private float _目标_Val = 1f;

	protected void Update()
	{
		_Img_BG.fillAmount = Mathf.Lerp(_Img_BG.fillAmount, _目标_BG, Time.deltaTime * 8f);
		_Img_实.fillAmount = Mathf.Lerp(_Img_实.fillAmount, _目标_Val, Time.deltaTime * 8f);
	}

	public override void F_SetVal(float currVal, float maxVal)
	{
		_目标_Val = currVal / maxVal;
	}

	public void F_SetVal_上限(float currVal, float maxVal)
	{
		_目标_BG = currVal / maxVal;
	}
}
