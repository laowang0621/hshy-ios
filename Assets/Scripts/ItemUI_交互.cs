using DSB;
using UnityEngine.UI;

public class ItemUI_交互 : ItemUIBase<ItemUI_交互>
{
	public Button _Btn_交互;

	public Text _Text;

	private void Start()
	{
		_Btn_交互.onClick.AddListener(F_交互);
	}

	private void F_交互()
	{
		DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_交互();
	}

	public void F_SetText(string text)
	{
		_Text.text = text;
	}
}
