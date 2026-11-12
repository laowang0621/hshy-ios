using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
	protected CanvasGroup _mCG;

	protected bool _m可见性;

	public CanvasGroup _CG
	{
		get
		{
			if (_mCG == null)
			{
				_mCG = GetComponent<CanvasGroup>();
			}
			return _mCG;
		}
	}

	public virtual bool _可见性
	{
		get
		{
			return _m可见性;
		}
		set
		{
			if (value)
			{
				_CG.alpha = 1f;
			}
			else
			{
				_CG.alpha = 0f;
			}
			_CG.interactable = value;
			_CG.blocksRaycasts = value;
			_m可见性 = value;
		}
	}
}
