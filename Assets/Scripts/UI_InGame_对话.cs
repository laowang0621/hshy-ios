using System;
using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_对话 : UIBase
{
	public Image _Img_对话框;

	public Text _Text_说话的人Name;

	public Text _Text_对话文本;

	public float _打字间隔 = 0.2f;

	private Coroutine _Cor_对话;

	public void F_开始对话(DataInfo_一条对话 对话_一条对话, Action UI结束回调)
	{
		if (_Cor_对话 != null)
		{
			StopCoroutine(_Cor_对话);
		}
		_Cor_对话 = StartCoroutine(Cor_对话(对话_一条对话, UI结束回调));
	}

	public void F_关闭对话()
	{
		if (_Cor_对话 != null)
		{
			StopCoroutine(_Cor_对话);
			_Cor_对话 = null;
		}
	}

	private IEnumerator Cor_对话(DataInfo_一条对话 对话_一条对话, Action UI结束回调)
	{
		SOData_对话 Data_对话 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_对话;
		_Img_对话框.sprite = Data_对话.F_得到对话框(0)._说话人头像;
		_Text_说话的人Name.text = "";
		_Text_对话文本.text = "";
		base._CG.blocksRaycasts = true;
		base._CG.DOFade(1f, 0.3f).SetUpdate(isIndependentUpdate: true);
		foreach (DataInfo_对话内容_Item 对话内容 in 对话_一条对话._对话内容)
		{
			yield return null;
			DataInfo_对话框 dataInfo_对话框 = Data_对话.F_得到对话框(对话内容._说话人ID);
			_Img_对话框.sprite = dataInfo_对话框._说话人头像;
			_Text_说话的人Name.text = dataInfo_对话框._说话人Name + ":";
			string 最终文本 = 对话内容._说话内容;
			bool canNext = false;
			for (int i = 0; i < 最终文本.Length; i++)
			{
				float timer = 0f;
				while (true)
				{
					timer += Time.unscaledDeltaTime;
					if (timer >= _打字间隔)
					{
						break;
					}
					if (DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.AnyKeyDown.WasPressedThisFrame())
					{
						canNext = true;
					}
					yield return null;
				}
				if (canNext)
				{
					break;
				}
				string text = 最终文本.Substring(0, i);
				_Text_对话文本.text = text;
			}
			_Text_对话文本.text = 最终文本;
			yield return null;
			while (!DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.AnyKeyDown.WasPressedThisFrame())
			{
				yield return null;
			}
		}
		_Cor_对话 = null;
		base._CG.blocksRaycasts = false;
		base._CG.DOFade(0f, 0.3f).SetUpdate(isIndependentUpdate: true);
		yield return null;
		UI结束回调();
	}
}
