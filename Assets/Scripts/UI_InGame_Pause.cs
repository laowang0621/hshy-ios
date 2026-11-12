using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame_Pause : UIBase
{
	public Button _Btn_继续游戏;

	public Button _Btn_调戏休整;

	public Button _Btn_改日再战;

	public Text _Text_游戏时长;

	public Text _Text_游戏周目;

	public InGame_Pause_物品栏 _InGame_Pause_物品栏;

	[Header("音量")]
	private bool _展开调息休整;

	[SerializeField]
	private CanvasGroup _CG_调息休整;

	[SerializeField]
	private Slider _Slider_音乐;

	[SerializeField]
	private Slider _Slider_音效;

	[SerializeField]
	private Toggle _Toggle_固定摇杆;

	public void F_显示当前存档时间(float time)
	{
		int num = (int)(time / 60f);
		_Text_游戏时长.text = string.Format("当前存档游玩时间\n{0}分{1}秒", num, (time - (float)(num * 60)).ToString("00.00"));
		if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目 <= 1)
		{
			_Text_游戏周目.text = "";
		}
		else
		{
			_Text_游戏周目.text = $"{DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目}周目";
		}
	}

	public void F_刷新UI()
	{
		_CG_调息休整.alpha = 0f;
		_CG_调息休整.interactable = false;
		_CG_调息休整.transform.localScale = Vector3.zero;
		_展开调息休整 = false;
		_Slider_音乐.value = PlayerPrefs.GetFloat("_Slider_音乐", 1f);
		_Slider_音效.value = PlayerPrefs.GetFloat("_Slider_音效", 1f);
		_Toggle_固定摇杆.isOn = PlayerPrefs.GetInt("_Toggle_固定摇杆", 0) == 1;
	}

	public void F_保存调息休整相关数据()
	{
		DSB_MonoSingleton<UIManager>.Instance.F_设置摇杆类型(_Toggle_固定摇杆.isOn);
		PlayerPrefs.SetFloat("_Slider_音乐", _Slider_音乐.value);
		PlayerPrefs.SetFloat("_Slider_音效", _Slider_音效.value);
		PlayerPrefs.SetInt("_Toggle_固定摇杆", _Toggle_固定摇杆.isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	private void Start()
	{
		_Btn_继续游戏.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_InGame_继续游戏();
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
		});
		_Btn_调戏休整.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
			_展开调息休整 = !_展开调息休整;
			if (_展开调息休整)
			{
				_CG_调息休整.DOFade(1f, 0.2f).SetUpdate(isIndependentUpdate: true).OnComplete(delegate
				{
					_CG_调息休整.interactable = true;
				});
				_CG_调息休整.transform.DOScale(1f, 0.2f).SetUpdate(isIndependentUpdate: true);
			}
			else
			{
				_CG_调息休整.DOFade(0f, 0.2f).SetUpdate(isIndependentUpdate: true).OnComplete(delegate
				{
					_CG_调息休整.interactable = true;
				});
				_CG_调息休整.transform.DOScale(0f, 0.2f).SetUpdate(isIndependentUpdate: true);
			}
		});
		_Btn_改日再战.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_InGame_返回主页();
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
		});
		_Slider_音乐.onValueChanged.AddListener(delegate(float val)
		{
			DSB_MonoSingleton<AudioManager>.Instance._AudioS_BGM.volume = val;
		});
		_Slider_音效.onValueChanged.AddListener(delegate(float val)
		{
			DSB_MonoSingleton<AudioManager>.Instance._AudioS_UISFX.volume = val;
			DSB_MonoSingleton<AudioManager>.Instance._AudioS_SFX.volume = val;
		});
	}
}
