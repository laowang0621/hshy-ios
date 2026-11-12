using UnityEngine;
using UnityEngine.UI;

public class ItemUI_能量条 : MonoBehaviour
{
	[SerializeField]
	public Image _Img_实;

	public virtual void F_SetVal(float currVal, float maxVal)
	{
		float fillAmount = currVal / maxVal;
		_Img_实.fillAmount = fillAmount;
	}
}
