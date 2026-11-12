using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_歪瓜_传送按钮 : MonoBehaviour
{
	public Button _Btn;

	public Text _Text;

	public void F_Init(int id, string text)
	{
		base.gameObject.SetActive(value: true);
		_Text.text = text;
		_Btn.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_通过传送区域切换场景(id, 0);
		});
	}
}
