using DSB;
using UnityEngine;
using UnityEngine.UI;

public class InGame_物品栏_物品 : MonoBehaviour
{
	public int _物品ID;

	private Text _Text_物品数量;

	private string _物品UIName;

	private bool _IsOK_Init;

	private void F_Init()
	{
		if (!_IsOK_Init)
		{
			_IsOK_Init = true;
			_Text_物品数量 = base.transform.GetChild(1).GetComponent<Text>();
			_物品UIName = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Prop.F_Porp_ID_Get_Info(_物品ID)._PorpName;
			if (_物品ID == 1001 || _物品ID == 1002 || _物品ID == 1003)
			{
				_Text_物品数量.text = _物品UIName;
			}
		}
	}

	public void F_SetUI()
	{
		F_Init();
		switch (_物品ID)
		{
		case 102:
			F_设置UI信息(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_102);
			break;
		case 103:
			F_设置UI信息(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_103);
			break;
		case 104:
			F_设置UI信息(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_104);
			break;
		case 105:
			F_设置UI信息(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._道具_105);
			break;
		case 1001:
			base.gameObject.SetActive(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得醒酒石);
			break;
		case 1002:
			base.gameObject.SetActive(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得金片);
			break;
		case 1003:
			base.gameObject.SetActive(DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得石之厉);
			break;
		default:
			base.gameObject.SetActive(value: false);
			break;
		}
	}

	private void F_设置UI信息(int 数量)
	{
		MyLog.LogError(_物品ID + ">>" + 数量);
		_Text_物品数量.text = _物品UIName + " X " + 数量;
	}
}
