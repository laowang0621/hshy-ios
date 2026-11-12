using System;
using DSB;
using UnityEngine;

public class MyInputManager : DSB_MonoSingleton<MyInputManager>
{
	private bool _Down_交互;

	public bool _是手柄布局;

	public static float Horizontal { get; private set; }

	public static float Vertical { get; private set; }

	public Joystick _摇杆 { get; set; }

	public HMLInput _HMLInput { get; private set; }

	public event Action _Key_Down_出租云 = delegate
	{
	};

	public event Action _Key_Down_交互 = delegate
	{
	};

	public event Action _Key_Down_饮酒 = delegate
	{
	};

	public event Action _Key_Down_跳跃 = delegate
	{
	};

	public event Action _Key_Up_跳跃 = delegate
	{
	};

	public event Action _Key_Down_轻攻击 = delegate
	{
	};

	public event Action _Key_Up_轻攻击 = delegate
	{
	};

	public event Action _Key_Down_翻滚 = delegate
	{
	};

	public event Action _Key_Down_棍花 = delegate
	{
	};

	public event Action _Key_Up_棍花 = delegate
	{
	};

	public event Action _Key_Down_重攻击 = delegate
	{
	};

	public event Action _Key_Up_重攻击 = delegate
	{
	};

	public event Action _Key_Down_定身术 = delegate
	{
	};

	public event Action _Key_Down_隐身 = delegate
	{
	};

	public event Action _Key_Down_猴子猴孙 = delegate
	{
	};

	public event Action _Key_Down_化形 = delegate
	{
	};

	public event Action _Key_Down_精魄 = delegate
	{
	};

	public event Action _Key_Down_AK = delegate
	{
	};

	public event Action _Key_Down_修行 = delegate
	{
	};

	public event Action _Key_Down_ESC = delegate
	{
	};

	public event Action _Key_Down_Any键盘 = delegate
	{
	};

	public event Action _Key_Down_Any手柄 = delegate
	{
	};

	private void Start()
	{
		_HMLInput = new HMLInput();
		_HMLInput.Enable();
		_HMLInput.HMLPaly.AnyKeyDown键盘.started += delegate
		{
			if (_是手柄布局)
			{
				_是手柄布局 = false;
				this._Key_Down_Any键盘();
			}
		};
		_HMLInput.HMLPaly.AnyKeyDown手柄.started += delegate
		{
			if (!_是手柄布局)
			{
				_是手柄布局 = true;
				this._Key_Down_Any手柄();
			}
		};
		_HMLInput.HMLPaly.交互.started += delegate
		{
			_Down_交互 = true;
			F_Key_Down_交互();
		};
		_HMLInput.HMLPaly.交互.canceled += delegate
		{
			_Down_交互 = false;
		};
		_HMLInput.HMLPaly.轻攻击.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_轻攻击();
			}
		};
		_HMLInput.HMLPaly.轻攻击.canceled += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Up_轻攻击();
			}
		};
		_HMLInput.HMLPaly.重攻击.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_重攻击();
			}
		};
		_HMLInput.HMLPaly.重攻击.canceled += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Up_重攻击();
			}
		};
		_HMLInput.HMLPaly.翻滚.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_翻滚();
			}
		};
		_HMLInput.HMLPaly.跳跃.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_跳跃();
			}
		};
		_HMLInput.HMLPaly.跳跃.canceled += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Up_跳跃();
			}
		};
		_HMLInput.HMLPaly.AK.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_AK();
			}
		};
		_HMLInput.HMLPaly.出租云.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_出租云();
			}
		};
		_HMLInput.HMLPaly.修行.started += delegate
		{
			if (!_Down_交互)
			{
				F_Key_Down_修行();
			}
		};
		_HMLInput.HMLPaly.棍花.started += delegate
		{
			F_Key_Down_棍花();
		};
		_HMLInput.HMLPaly.棍花.canceled += delegate
		{
			F_Key_Up_棍花();
		};
		_HMLInput.HMLPaly.饮酒.started += delegate
		{
			F_Key_Down_饮酒();
		};
		_HMLInput.HMLPaly.技能1.started += delegate
		{
			F_Key_Down_定身术();
		};
		_HMLInput.HMLPaly.技能2.started += delegate
		{
			F_Key_Down_隐身();
		};
		_HMLInput.HMLPaly.技能4.started += delegate
		{
			F_Key_Down_化形();
		};
		_HMLInput.HMLPaly.技能3.started += delegate
		{
			F_Key_Down_猴子猴孙();
		};
		_HMLInput.HMLPaly.精魄.started += delegate
		{
			F_Key_Down_精魄();
		};
		_HMLInput.HMLPaly.暂停.started += delegate
		{
			this._Key_Down_ESC();
		};
		_HMLInput.HMLPaly.返回.started += delegate
		{
			DSB_MonoSingleton<UIManager>.Instance.F_InGame_返回主页();
		};
	}

	private void Update()
	{
		Vector2 vector = _HMLInput.HMLPaly.移动.ReadValue<Vector2>();
		if ((bool)_摇杆)
		{
			Horizontal = vector.x + _摇杆.Horizontal;
			Vertical = vector.y + _摇杆.Vertical;
		}
		else
		{
			Horizontal = vector.x;
			Vertical = vector.y;
		}
	}

	public void F_Key_Down_修行()
	{
		this._Key_Down_修行();
	}

	public void F_Key_Down_交互()
	{
		this._Key_Down_交互();
	}

	public void F_Key_Down_饮酒()
	{
		this._Key_Down_饮酒();
	}

	public void F_Key_Down_定身术()
	{
		this._Key_Down_定身术();
	}

	public void F_Key_Down_隐身()
	{
		this._Key_Down_隐身();
	}

	public void F_Key_Down_猴子猴孙()
	{
		this._Key_Down_猴子猴孙();
	}

	public void F_Key_Down_化形()
	{
		this._Key_Down_化形();
	}

	public void F_Key_Down_精魄()
	{
		this._Key_Down_精魄();
	}

	public void F_Key_Down_跳跃()
	{
		this._Key_Down_跳跃();
	}

	public void F_Key_Up_跳跃()
	{
		this._Key_Up_跳跃();
	}

	public void F_Key_Down_AK()
	{
		this._Key_Down_AK();
	}

	public void F_Key_Down_出租云()
	{
		this._Key_Down_出租云();
	}

	public void F_Key_Down_轻攻击()
	{
		this._Key_Down_轻攻击();
	}

	public void F_Key_Up_轻攻击()
	{
		this._Key_Up_轻攻击();
	}

	public void F_Key_Down_翻滚()
	{
		this._Key_Down_翻滚();
	}

	public void F_Key_Down_棍花()
	{
		this._Key_Down_棍花();
	}

	public void F_Key_Up_棍花()
	{
		this._Key_Up_棍花();
	}

	public void F_Key_Down_重攻击()
	{
		this._Key_Down_重攻击();
	}

	public void F_Key_Up_重攻击()
	{
		this._Key_Up_重攻击();
	}
}
