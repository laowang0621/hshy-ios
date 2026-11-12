using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyUI修行精魄选择 : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	public Image _Img;

	public bool _IsLeftBtn;

	private Color _选中颜色 = Color.green;

	private bool _已选中;

	private Button _Btn;

	private My修行_精魄选择 _My修行_精魄选择;

	private void Awake()
	{
		_Btn = GetComponent<Button>();
		_My修行_精魄选择 = GetComponentInParent<My修行_精魄选择>();
	}

	private void Start()
	{
		_Btn.onClick.AddListener(delegate
		{
			if (_已选中)
			{
				_My修行_精魄选择.F_Btn_点击按钮_is左箭头(_IsLeftBtn);
			}
			else
			{
				_已选中 = true;
			}
		});
	}

	public virtual void OnSelect(BaseEventData eventData)
	{
		_Img.DOColor(_选中颜色, 0.2f).SetUpdate(isIndependentUpdate: true);
		_My修行_精魄选择.F_选中按钮();
	}

	public virtual void OnDeselect(BaseEventData eventData)
	{
		_Img.DOColor(Color.white, 0.2f).SetUpdate(isIndependentUpdate: true);
		_已选中 = false;
	}
}
