using DG.Tweening;
using DSB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainMenu : UIBase
{
	[SerializeField]
	private CanvasGroup _CG_三连成功文本提示;

	[Header("Animator")]
	public Animator _Ani;

	public Animator _Ani_Btn;

	[Header("按钮")]
	[SerializeField]
	private Button _Btn_开始游戏;

	[SerializeField]
	private Button _Btn_载入游戏;

	[SerializeField]
	private Button _Btn_新的轮回;

	[SerializeField]
	private Button _Btn_再入轮回;

	[SerializeField]
	private Text _Btn_再入轮回_白字;

	[SerializeField]
	private Text _Btn_再入轮回_黑字;

	[SerializeField]
	private Button _Btn_调息休整;

	[SerializeField]
	private Button _Btn_退出游戏;

	[SerializeField]
	private Button _Btn_点赞;

	[SerializeField]
	private Button _Btn_投币;

	[SerializeField]
	private Button _Btn_收藏;

	[Header("音量")]
	[SerializeField]
	private Slider _Slider_音乐;

	[SerializeField]
	private Slider _Slider_音效;

	[SerializeField]
	private Toggle _Toggle_固定摇杆;

	public GameObject _GO_biligame;

	private bool _启动再入轮回;

	public void F_刷新UI()
	{
		Invoke("F_TTT", 0.33f);
		_Ani_Btn.Play("默认", 0, 0f);
		_启动再入轮回 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败((int)Lunch._Enemy_再入轮回Boss);
		_Btn_再入轮回.interactable = _启动再入轮回;
		_Btn_再入轮回_白字.SetAlpha(_启动再入轮回 ? 1f : 0.3f);
		_Btn_再入轮回_白字.text = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目 + 1 + "周目";
		_Btn_再入轮回_黑字.text = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目 + 1 + "周目";
		_Slider_音乐.value = PlayerPrefs.GetFloat("_Slider_音乐", 1f);
		_Slider_音效.value = PlayerPrefs.GetFloat("_Slider_音效", 1f);
		_Toggle_固定摇杆.isOn = PlayerPrefs.GetInt("_Toggle_固定摇杆", 0) == 1;
	}

	private void F_TTT()
	{
		EventSystem.current.SetSelectedGameObject(_Btn_开始游戏.gameObject);
	}

	private void Start()
	{
		_Slider_音乐.onValueChanged.AddListener(delegate(float val)
		{
			DSB_MonoSingleton<AudioManager>.Instance._AudioS_BGM.volume = val;
		});
		_Slider_音效.onValueChanged.AddListener(delegate(float val)
		{
			DSB_MonoSingleton<AudioManager>.Instance._AudioS_UISFX.volume = val;
			DSB_MonoSingleton<AudioManager>.Instance._AudioS_SFX.volume = val;
		});
		_Btn_开始游戏.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.开始游戏();
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
		});
		_Btn_再入轮回.onClick.AddListener(delegate
		{
			if (_启动再入轮回)
			{
				DSB_MonoSingleton<UIManager>.Instance.F_再入轮回();
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
			}
		});
		_Btn_新的轮回.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_新的轮回();
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
		});
		_Btn_载入游戏.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
			_Ani_Btn.SetTrigger("切换载入游戏");
		});
		_Btn_调息休整.onClick.AddListener(delegate
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_点击();
			_Ani_Btn.SetTrigger("切换调息休整");
			F_保存调息休整相关数据();
		});
		_Btn_点赞.onClick.AddListener(delegate
		{
			F_三连();
		});
		_Btn_投币.onClick.AddListener(delegate
		{
			F_三连();
		});
		_Btn_收藏.onClick.AddListener(delegate
		{
			Application.OpenURL("https://www.bilibili.com/video/BV1WQxee4Eyv");
		});
		_Btn_退出游戏.onClick.AddListener(delegate
		{
			if (Lunch._biligame)
			{
				_GO_biligame.SetActive(value: true);
			}
			else
			{
				Application.Quit();
			}
		});
		MyLog.Log("GameManager._已三连<<" + DSB_MonoSingleton<MyDataManager>.Instance._已三连);
		if (!Mathf.Approximately(DSB_MonoSingleton<MyDataManager>.Instance._已三连, 1.2f))
		{
			_CG_三连成功文本提示.alpha = 0f;
		}
	}

	public void F_保存调息休整相关数据()
	{
		DSB_MonoSingleton<UIManager>.Instance.F_设置摇杆类型(_Toggle_固定摇杆.isOn);
		PlayerPrefs.SetFloat("_Slider_音乐", _Slider_音乐.value);
		PlayerPrefs.SetFloat("_Slider_音效", _Slider_音效.value);
		PlayerPrefs.SetInt("_Toggle_固定摇杆", _Toggle_固定摇杆.isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	private void F_三连()
	{
		MyLog.LogError("GameManager._已三连<<" + DSB_MonoSingleton<MyDataManager>.Instance._已三连);
		if (!Mathf.Approximately(DSB_MonoSingleton<MyDataManager>.Instance._已三连, 1.2f))
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_三连();
			DSB_MonoSingleton<MyDataManager>.Instance._已三连 = 1.2f;
			Image 点赞 = _Btn_点赞.transform.GetChild(0).GetComponent<Image>();
			Image 投币 = _Btn_投币.transform.GetChild(0).GetComponent<Image>();
			Image 收藏 = _Btn_收藏.transform.GetChild(0).GetComponent<Image>();
			点赞.DOFillAmount(1f, 1f).SetEase(Ease.Linear);
			投币.DOFillAmount(1f, 1f).SetEase(Ease.Linear);
			收藏.DOFillAmount(1f, 1f).SetEase(Ease.Linear).OnComplete(delegate
			{
				点赞.DOFade(0f, 0.5f);
				投币.DOFade(0f, 0.5f);
				收藏.DOFade(0f, 0.5f);
				_CG_三连成功文本提示.DOFade(1f, 0.5f);
			});
		}
	}
}
