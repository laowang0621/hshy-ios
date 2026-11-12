using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class HMLInput : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct HMLPalyActions
	{
		private HMLInput m_Wrapper;

		public InputAction 移动 => m_Wrapper.m_HMLPaly_移动;

		public InputAction 交互 => m_Wrapper.m_HMLPaly_交互;

		public InputAction 右扳机 => m_Wrapper.m_HMLPaly_右扳机;

		public InputAction 暂停 => m_Wrapper.m_HMLPaly_暂停;

		public InputAction 返回 => m_Wrapper.m_HMLPaly_返回;

		public InputAction 出租云 => m_Wrapper.m_HMLPaly_出租云;

		public InputAction 技能1 => m_Wrapper.m_HMLPaly_技能1;

		public InputAction 技能2 => m_Wrapper.m_HMLPaly_技能2;

		public InputAction 技能3 => m_Wrapper.m_HMLPaly_技能3;

		public InputAction 技能4 => m_Wrapper.m_HMLPaly_技能4;

		public InputAction 精魄 => m_Wrapper.m_HMLPaly_精魄;

		public InputAction 轻攻击 => m_Wrapper.m_HMLPaly_轻攻击;

		public InputAction 重攻击 => m_Wrapper.m_HMLPaly_重攻击;

		public InputAction 翻滚 => m_Wrapper.m_HMLPaly_翻滚;

		public InputAction 跳跃 => m_Wrapper.m_HMLPaly_跳跃;

		public InputAction 棍花 => m_Wrapper.m_HMLPaly_棍花;

		public InputAction 饮酒 => m_Wrapper.m_HMLPaly_饮酒;

		public InputAction AK => m_Wrapper.m_HMLPaly_AK;

		public InputAction 面板 => m_Wrapper.m_HMLPaly_面板;

		public InputAction 修行 => m_Wrapper.m_HMLPaly_修行;

		public InputAction AnyKeyDown => m_Wrapper.m_HMLPaly_AnyKeyDown;

		public InputAction AnyKeyDown键盘 => m_Wrapper.m_HMLPaly_AnyKeyDown键盘;

		public InputAction AnyKeyDown手柄 => m_Wrapper.m_HMLPaly_AnyKeyDown手柄;

		public bool enabled => Get().enabled;

		public HMLPalyActions(HMLInput wrapper)
		{
			m_Wrapper = wrapper;
		}

		public InputActionMap Get()
		{
			return m_Wrapper.m_HMLPaly;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(HMLPalyActions set)
		{
			return set.Get();
		}

		public void SetCallbacks(IHMLPalyActions instance)
		{
			if (m_Wrapper.m_HMLPalyActionsCallbackInterface != null)
			{
				移动.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On移动;
				移动.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On移动;
				移动.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On移动;
				交互.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On交互;
				交互.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On交互;
				交互.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On交互;
				右扳机.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On右扳机;
				右扳机.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On右扳机;
				右扳机.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On右扳机;
				暂停.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On暂停;
				暂停.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On暂停;
				暂停.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On暂停;
				返回.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On返回;
				返回.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On返回;
				返回.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On返回;
				出租云.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On出租云;
				出租云.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On出租云;
				出租云.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On出租云;
				技能1.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能1;
				技能1.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能1;
				技能1.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能1;
				技能2.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能2;
				技能2.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能2;
				技能2.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能2;
				技能3.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能3;
				技能3.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能3;
				技能3.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能3;
				技能4.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能4;
				技能4.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能4;
				技能4.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On技能4;
				精魄.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On精魄;
				精魄.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On精魄;
				精魄.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On精魄;
				轻攻击.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On轻攻击;
				轻攻击.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On轻攻击;
				轻攻击.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On轻攻击;
				重攻击.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On重攻击;
				重攻击.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On重攻击;
				重攻击.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On重攻击;
				翻滚.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On翻滚;
				翻滚.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On翻滚;
				翻滚.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On翻滚;
				跳跃.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On跳跃;
				跳跃.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On跳跃;
				跳跃.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On跳跃;
				棍花.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On棍花;
				棍花.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On棍花;
				棍花.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On棍花;
				饮酒.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On饮酒;
				饮酒.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On饮酒;
				饮酒.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On饮酒;
				AK.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAK;
				AK.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAK;
				AK.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAK;
				面板.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On面板;
				面板.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On面板;
				面板.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On面板;
				修行.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On修行;
				修行.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On修行;
				修行.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.On修行;
				AnyKeyDown.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown;
				AnyKeyDown.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown;
				AnyKeyDown.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown;
				AnyKeyDown键盘.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown键盘;
				AnyKeyDown键盘.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown键盘;
				AnyKeyDown键盘.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown键盘;
				AnyKeyDown手柄.started -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown手柄;
				AnyKeyDown手柄.performed -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown手柄;
				AnyKeyDown手柄.canceled -= m_Wrapper.m_HMLPalyActionsCallbackInterface.OnAnyKeyDown手柄;
			}
			m_Wrapper.m_HMLPalyActionsCallbackInterface = instance;
			if (instance != null)
			{
				移动.started += instance.On移动;
				移动.performed += instance.On移动;
				移动.canceled += instance.On移动;
				交互.started += instance.On交互;
				交互.performed += instance.On交互;
				交互.canceled += instance.On交互;
				右扳机.started += instance.On右扳机;
				右扳机.performed += instance.On右扳机;
				右扳机.canceled += instance.On右扳机;
				暂停.started += instance.On暂停;
				暂停.performed += instance.On暂停;
				暂停.canceled += instance.On暂停;
				返回.started += instance.On返回;
				返回.performed += instance.On返回;
				返回.canceled += instance.On返回;
				出租云.started += instance.On出租云;
				出租云.performed += instance.On出租云;
				出租云.canceled += instance.On出租云;
				技能1.started += instance.On技能1;
				技能1.performed += instance.On技能1;
				技能1.canceled += instance.On技能1;
				技能2.started += instance.On技能2;
				技能2.performed += instance.On技能2;
				技能2.canceled += instance.On技能2;
				技能3.started += instance.On技能3;
				技能3.performed += instance.On技能3;
				技能3.canceled += instance.On技能3;
				技能4.started += instance.On技能4;
				技能4.performed += instance.On技能4;
				技能4.canceled += instance.On技能4;
				精魄.started += instance.On精魄;
				精魄.performed += instance.On精魄;
				精魄.canceled += instance.On精魄;
				轻攻击.started += instance.On轻攻击;
				轻攻击.performed += instance.On轻攻击;
				轻攻击.canceled += instance.On轻攻击;
				重攻击.started += instance.On重攻击;
				重攻击.performed += instance.On重攻击;
				重攻击.canceled += instance.On重攻击;
				翻滚.started += instance.On翻滚;
				翻滚.performed += instance.On翻滚;
				翻滚.canceled += instance.On翻滚;
				跳跃.started += instance.On跳跃;
				跳跃.performed += instance.On跳跃;
				跳跃.canceled += instance.On跳跃;
				棍花.started += instance.On棍花;
				棍花.performed += instance.On棍花;
				棍花.canceled += instance.On棍花;
				饮酒.started += instance.On饮酒;
				饮酒.performed += instance.On饮酒;
				饮酒.canceled += instance.On饮酒;
				AK.started += instance.OnAK;
				AK.performed += instance.OnAK;
				AK.canceled += instance.OnAK;
				面板.started += instance.On面板;
				面板.performed += instance.On面板;
				面板.canceled += instance.On面板;
				修行.started += instance.On修行;
				修行.performed += instance.On修行;
				修行.canceled += instance.On修行;
				AnyKeyDown.started += instance.OnAnyKeyDown;
				AnyKeyDown.performed += instance.OnAnyKeyDown;
				AnyKeyDown.canceled += instance.OnAnyKeyDown;
				AnyKeyDown键盘.started += instance.OnAnyKeyDown键盘;
				AnyKeyDown键盘.performed += instance.OnAnyKeyDown键盘;
				AnyKeyDown键盘.canceled += instance.OnAnyKeyDown键盘;
				AnyKeyDown手柄.started += instance.OnAnyKeyDown手柄;
				AnyKeyDown手柄.performed += instance.OnAnyKeyDown手柄;
				AnyKeyDown手柄.canceled += instance.OnAnyKeyDown手柄;
			}
		}
	}

	public interface IHMLPalyActions
	{
		void On移动(InputAction.CallbackContext context);

		void On交互(InputAction.CallbackContext context);

		void On右扳机(InputAction.CallbackContext context);

		void On暂停(InputAction.CallbackContext context);

		void On返回(InputAction.CallbackContext context);

		void On出租云(InputAction.CallbackContext context);

		void On技能1(InputAction.CallbackContext context);

		void On技能2(InputAction.CallbackContext context);

		void On技能3(InputAction.CallbackContext context);

		void On技能4(InputAction.CallbackContext context);

		void On精魄(InputAction.CallbackContext context);

		void On轻攻击(InputAction.CallbackContext context);

		void On重攻击(InputAction.CallbackContext context);

		void On翻滚(InputAction.CallbackContext context);

		void On跳跃(InputAction.CallbackContext context);

		void On棍花(InputAction.CallbackContext context);

		void On饮酒(InputAction.CallbackContext context);

		void OnAK(InputAction.CallbackContext context);

		void On面板(InputAction.CallbackContext context);

		void On修行(InputAction.CallbackContext context);

		void OnAnyKeyDown(InputAction.CallbackContext context);

		void OnAnyKeyDown键盘(InputAction.CallbackContext context);

		void OnAnyKeyDown手柄(InputAction.CallbackContext context);
	}

	private readonly InputActionMap m_HMLPaly;

	private IHMLPalyActions m_HMLPalyActionsCallbackInterface;

	private readonly InputAction m_HMLPaly_移动;

	private readonly InputAction m_HMLPaly_交互;

	private readonly InputAction m_HMLPaly_右扳机;

	private readonly InputAction m_HMLPaly_暂停;

	private readonly InputAction m_HMLPaly_返回;

	private readonly InputAction m_HMLPaly_出租云;

	private readonly InputAction m_HMLPaly_技能1;

	private readonly InputAction m_HMLPaly_技能2;

	private readonly InputAction m_HMLPaly_技能3;

	private readonly InputAction m_HMLPaly_技能4;

	private readonly InputAction m_HMLPaly_精魄;

	private readonly InputAction m_HMLPaly_轻攻击;

	private readonly InputAction m_HMLPaly_重攻击;

	private readonly InputAction m_HMLPaly_翻滚;

	private readonly InputAction m_HMLPaly_跳跃;

	private readonly InputAction m_HMLPaly_棍花;

	private readonly InputAction m_HMLPaly_饮酒;

	private readonly InputAction m_HMLPaly_AK;

	private readonly InputAction m_HMLPaly_面板;

	private readonly InputAction m_HMLPaly_修行;

	private readonly InputAction m_HMLPaly_AnyKeyDown;

	private readonly InputAction m_HMLPaly_AnyKeyDown键盘;

	private readonly InputAction m_HMLPaly_AnyKeyDown手柄;

	public InputActionAsset asset { get; }

	public InputBinding? bindingMask
	{
		get
		{
			return asset.bindingMask;
		}
		set
		{
			asset.bindingMask = value;
		}
	}

	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return asset.devices;
		}
		set
		{
			asset.devices = value;
		}
	}

	public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

	public IEnumerable<InputBinding> bindings => asset.bindings;

	public HMLPalyActions HMLPaly => new HMLPalyActions(this);

	public HMLInput()
	{
		asset = InputActionAsset.FromJson("{\n    \"name\": \"HMLInput\",\n    \"maps\": [\n        {\n            \"name\": \"HMLPaly\",\n            \"id\": \"1b353cff-2f21-4803-a05e-2ca3a1bb0d9b\",\n            \"actions\": [\n                {\n                    \"name\": \"移动\",\n                    \"type\": \"Value\",\n                    \"id\": \"0b25ee06-f395-40c9-a651-281db8f83486\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"交互\",\n                    \"type\": \"Button\",\n                    \"id\": \"09f35dae-325d-446d-b5f2-a547d9d577da\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"右扳机\",\n                    \"type\": \"Button\",\n                    \"id\": \"50ab1cbc-efda-4439-91e8-7ba95ef12d88\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"暂停\",\n                    \"type\": \"Button\",\n                    \"id\": \"56320819-7ef5-41b2-a678-9c510e4dba75\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"返回\",\n                    \"type\": \"Button\",\n                    \"id\": \"ddd8fe85-f10b-4cf4-a1d9-28ef53f5ea34\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"出租云\",\n                    \"type\": \"Button\",\n                    \"id\": \"bd076f3b-74d9-4863-8103-551ac29052c0\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"技能1\",\n                    \"type\": \"Button\",\n                    \"id\": \"0a7185ba-3928-4bbf-898d-aad3c4aa2051\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"技能2\",\n                    \"type\": \"Button\",\n                    \"id\": \"f6c257f0-da75-4723-b01c-f1817cdd235d\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"技能3\",\n                    \"type\": \"Button\",\n                    \"id\": \"2949ec65-b320-4847-b9e8-4831c3cf82dd\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"技能4\",\n                    \"type\": \"Button\",\n                    \"id\": \"e647b424-bf76-4365-b6a2-6007749e4022\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"精魄\",\n                    \"type\": \"Button\",\n                    \"id\": \"3e923828-f0d9-4181-811f-82a887a7f311\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"轻攻击\",\n                    \"type\": \"Button\",\n                    \"id\": \"af66c30b-97c5-450d-a225-8e8668b08c2b\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"重攻击\",\n                    \"type\": \"Button\",\n                    \"id\": \"f35a3b2b-dbf3-4cce-95b5-ff793432f51d\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"翻滚\",\n                    \"type\": \"Button\",\n                    \"id\": \"96137b42-fe37-48ca-931b-5522e581af40\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"跳跃\",\n                    \"type\": \"Button\",\n                    \"id\": \"f086492a-97e2-49a0-a95d-12097d26ef39\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"棍花\",\n                    \"type\": \"Button\",\n                    \"id\": \"d53196be-3d2b-477b-aedb-740916a2d1aa\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"饮酒\",\n                    \"type\": \"Button\",\n                    \"id\": \"49ae494b-7380-476c-afe3-f1e213b262dc\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AK\",\n                    \"type\": \"Button\",\n                    \"id\": \"c81afc0e-4b7e-412b-b0ee-e63af8bbd774\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"面板\",\n                    \"type\": \"Button\",\n                    \"id\": \"c2b03004-b7ca-4f16-8620-eb5d5e2ea932\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"修行\",\n                    \"type\": \"Button\",\n                    \"id\": \"4183079d-9e85-4b87-88f2-fac4ef4627e2\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AnyKeyDown\",\n                    \"type\": \"Button\",\n                    \"id\": \"f9e61f12-8121-4894-9ca0-d5f155a8d1f5\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AnyKeyDown键盘\",\n                    \"type\": \"Button\",\n                    \"id\": \"8b8a82bf-091b-48ff-b46d-06e08c105a79\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AnyKeyDown手柄\",\n                    \"type\": \"Button\",\n                    \"id\": \"0a973d47-26cb-460b-a852-12bb09ba04d0\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"WSAD\",\n                    \"id\": \"f22c1a8d-92e9-467e-be71-887133a15a48\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"f4070181-be12-4fe9-af62-095041164c2f\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"9c27b4f7-7e4b-48f7-9121-47496d01edb9\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"ef38a520-bdd9-4aa3-b705-940878cbac61\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"a3ac2a5c-67df-476a-967f-82b38dcab504\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"fa17aaaa-af58-4d82-8784-6a21250a9eca\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"0c3f76a8-1895-4a53-8f69-4f65032e1831\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"508d6462-6c69-4924-bfba-ec4c7bbdc1d3\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"0d4c7d81-5997-4e1d-bbbb-c0dcf520ec1d\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c3132d2f-7cd2-4213-9a6e-b8776f393de3\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"交互\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ffb276f1-540f-44ea-9696-8d2797342a57\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"交互\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"85f77bba-5290-4610-b8db-ec74d8f359db\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"轻攻击\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4acda704-999f-4be9-970e-db67eb0a52f3\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"重攻击\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3510779d-172c-4c4f-baf5-5e201a7ee32e\",\n                    \"path\": \"<Keyboard>/k\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"重攻击\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ec4ed12e-7fd6-46a9-ab59-7627d6fa29bb\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"重攻击\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"87329348-c92d-42a9-8f7d-c53f6594c872\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"翻滚\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1984a71c-5729-4b29-926d-bc882547396b\",\n                    \"path\": \"<Keyboard>/l\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"翻滚\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8c1de3d8-bd1a-4e6b-8e95-4dcbab96a445\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"翻滚\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"71264a1b-58da-40ca-ab38-ce735fe692fb\",\n                    \"path\": \"<Keyboard>/i\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"跳跃\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a3e64b3f-2f89-4ff3-81d4-7e0039839417\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"跳跃\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"26fd497d-ebf1-42a8-b185-7fa96619fb41\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"跳跃\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"55a78bbb-07ab-42c5-827c-7d4fb944e5d5\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"棍花\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e6c0e2a1-afe2-4865-911c-2afcfe186704\",\n                    \"path\": \"<Keyboard>/o\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"棍花\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0bb741f3-2c53-4ca9-87aa-74ba6f744132\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"棍花\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0550a8cf-636d-43a5-8784-e93485b17067\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"605cd9a9-1cc6-4763-a348-26eb0a401990\",\n                    \"path\": \"<AndroidJoystick>/stick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"移动\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"93e6898f-b880-4c88-9db3-204433d89cc0\",\n                    \"path\": \"<Keyboard>/j\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"轻攻击\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cacc8093-6497-4bae-bce8-d7c8f09e1eae\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"轻攻击\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b132be31-ff1f-404f-924e-e7cd65842017\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"交互\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e46e0f0f-f16a-4181-9547-035b368ac663\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"饮酒\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"23833c08-a81a-473c-a5e1-d22e040dca76\",\n                    \"path\": \"<Keyboard>/r\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"饮酒\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"32432b0d-2884-4b9a-9f0e-fd4392b0a7d0\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"饮酒\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9256ccc7-9b97-43c8-bb0f-37ebd4b6c2f3\",\n                    \"path\": \"<Keyboard>/1\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能1\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"One Modifier\",\n                    \"id\": \"1f3e3a2b-9156-4fce-9bd6-cc51c60aad2a\",\n                    \"path\": \"OneModifier\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能1\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"modifier\",\n                    \"id\": \"b4b4fef7-ca2f-45d5-b261-5e1ab99869d1\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能1\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Binding\",\n                    \"id\": \"0b74808c-b9bd-4dfc-aeca-62bf8f6ff1fe\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能1\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"10ac326a-e611-439d-8b4f-bd2424598ca9\",\n                    \"path\": \"<Keyboard>/2\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能2\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"One Modifier\",\n                    \"id\": \"1a9c7312-8b97-4010-afe9-7dd3f4e85599\",\n                    \"path\": \"OneModifier\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能2\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"modifier\",\n                    \"id\": \"b2279d81-6ea8-461e-8d88-83fbc65a32d0\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能2\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Binding\",\n                    \"id\": \"6c2d9f56-55eb-450b-9ba9-3edc5f046e0b\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能2\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"44dd6d09-2dda-415d-8182-85732c27619f\",\n                    \"path\": \"<Keyboard>/3\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能3\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"One Modifier\",\n                    \"id\": \"d808b401-9ec2-44ba-b55e-57cabc750935\",\n                    \"path\": \"OneModifier\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能3\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"modifier\",\n                    \"id\": \"ec6579d5-f8c9-41b3-821b-8196d737d239\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能3\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Binding\",\n                    \"id\": \"c215ccd2-67a9-4526-bc85-9579eaecdaac\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能3\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"dd2a2e1e-51a4-4a13-ab8e-6fc1f19d602b\",\n                    \"path\": \"<Keyboard>/4\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能4\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"One Modifier\",\n                    \"id\": \"2682da84-3514-4ce7-b4cb-7e5689cf6b18\",\n                    \"path\": \"OneModifier\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能4\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Modifier\",\n                    \"id\": \"c8f47558-162c-4ba1-b2c1-deb0adfbb68b\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能4\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Binding\",\n                    \"id\": \"6d180bd4-04a4-4139-8140-c72f8a1311a0\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"技能4\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9bde4c85-04bc-48dc-b3c3-e3f1450eb8b5\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"暂停\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4fb81e36-7805-4399-9950-4274f54a1ae1\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"暂停\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5765288d-fa46-4a73-9d98-eff2b9e34365\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"精魄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bc2b432e-730c-4047-8041-ea9b8bd50143\",\n                    \"path\": \"<Keyboard>/f\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"精魄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a9b4f8e1-c810-40df-9d0e-2c51ab68d9c0\",\n                    \"path\": \"\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"精魄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f8c6119d-423f-450e-b160-e97f4961b5dd\",\n                    \"path\": \"<Keyboard>/anyKey\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"02525dd9-d941-4fdc-bcee-98a09bca892c\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f68c0c2e-3579-4586-90fd-b2aa625d7be0\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e00374e5-12aa-45c3-82dc-6be0d529d589\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"783915c4-4254-441f-8f6f-c85ce34376a0\",\n                    \"path\": \"<Touchscreen>/touch*/Press\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"90bf4071-41a6-4ff0-bc8a-290340ef8f4c\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"返回\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f330f183-e148-415f-ba14-64ea931ca4bb\",\n                    \"path\": \"<Keyboard>/g\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AK\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"91948283-dbb4-41d4-8354-152de64c2f78\",\n                    \"path\": \"<Gamepad>/dpad/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AK\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9eae55ea-86dc-4ecf-be37-1deed80bf42b\",\n                    \"path\": \"<Keyboard>/anyKey\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown键盘\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f7e90f91-e073-49f0-91ab-aceda15d6c9d\",\n                    \"path\": \"<Gamepad>/leftStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2ab35f2e-8f87-4fb9-8780-8eb6e38352aa\",\n                    \"path\": \"<Gamepad>/leftStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c10d852a-4ed8-416d-81de-b990acd74e18\",\n                    \"path\": \"<Gamepad>/leftStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"230a6c84-72f3-455b-8957-e12b50caf3f7\",\n                    \"path\": \"<Gamepad>/leftStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"edf622f1-8102-442c-9bc1-f483a27661f0\",\n                    \"path\": \"<Gamepad>/dpad/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"01379cb1-497e-4d2d-9c63-ec8e4edfe90a\",\n                    \"path\": \"<Gamepad>/dpad/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"237a21f6-f42f-483f-8949-c2ed0543b258\",\n                    \"path\": \"<Gamepad>/dpad/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1b8481d7-5742-4cf7-bcb8-7e15d58f5a13\",\n                    \"path\": \"<Gamepad>/dpad/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"60b4f921-fe33-4a26-82d3-aa7f6480789c\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3e560d1f-5815-41d2-aa3b-f9c0e8ca748c\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"096dc208-67a9-41ee-b404-148d438356d5\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5ae83350-76e5-48e1-b3b6-c6295eef4c4a\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a6a6703d-dbcc-436e-8a63-14e37e926134\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e9c6a20f-7ae8-4e67-bb67-d996667886f8\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1801cdf4-6f6f-4227-94f6-40298cd3a888\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"26c082a5-2afd-49a2-81c8-4ac95439f32f\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b9291a48-cb99-4be6-8b3e-b1a41c47c139\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3c95e16d-4c63-4a75-a056-b8afb0cc15aa\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ec7a68f0-4a4f-44d1-9d7b-cadab76436a7\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ca0cc8a0-f53b-4059-bcce-2c0239e79095\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e9b5107b-8bb1-433a-9d82-706efb7a8a4a\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"56afc8e0-c511-4acd-9e42-55012d58538e\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"AnyKeyDown手柄\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"513688b5-c556-4a39-b049-96d9c5b9f55d\",\n                    \"path\": \"<Keyboard>/v\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"面板\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ec58a343-d504-4682-99f7-d5440ff79cdb\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"右扳机\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3957b695-6af2-423f-81f3-b56002e1b166\",\n                    \"path\": \"<Keyboard>/u\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"出租云\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4e8a04a1-b0f4-4bf6-a66b-3b810b3f2008\",\n                    \"path\": \"<Gamepad>/dpad/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"出租云\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b2a720b4-8b19-4fe4-8c69-450018549239\",\n                    \"path\": \"<Keyboard>/tab\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"修行\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2c0b1f1a-4350-484f-aa38-503019c0b46e\",\n                    \"path\": \"<Gamepad>/dpad/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"修行\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": []\n}");
		m_HMLPaly = asset.FindActionMap("HMLPaly", throwIfNotFound: true);
		m_HMLPaly_移动 = m_HMLPaly.FindAction("移动", throwIfNotFound: true);
		m_HMLPaly_交互 = m_HMLPaly.FindAction("交互", throwIfNotFound: true);
		m_HMLPaly_右扳机 = m_HMLPaly.FindAction("右扳机", throwIfNotFound: true);
		m_HMLPaly_暂停 = m_HMLPaly.FindAction("暂停", throwIfNotFound: true);
		m_HMLPaly_返回 = m_HMLPaly.FindAction("返回", throwIfNotFound: true);
		m_HMLPaly_出租云 = m_HMLPaly.FindAction("出租云", throwIfNotFound: true);
		m_HMLPaly_技能1 = m_HMLPaly.FindAction("技能1", throwIfNotFound: true);
		m_HMLPaly_技能2 = m_HMLPaly.FindAction("技能2", throwIfNotFound: true);
		m_HMLPaly_技能3 = m_HMLPaly.FindAction("技能3", throwIfNotFound: true);
		m_HMLPaly_技能4 = m_HMLPaly.FindAction("技能4", throwIfNotFound: true);
		m_HMLPaly_精魄 = m_HMLPaly.FindAction("精魄", throwIfNotFound: true);
		m_HMLPaly_轻攻击 = m_HMLPaly.FindAction("轻攻击", throwIfNotFound: true);
		m_HMLPaly_重攻击 = m_HMLPaly.FindAction("重攻击", throwIfNotFound: true);
		m_HMLPaly_翻滚 = m_HMLPaly.FindAction("翻滚", throwIfNotFound: true);
		m_HMLPaly_跳跃 = m_HMLPaly.FindAction("跳跃", throwIfNotFound: true);
		m_HMLPaly_棍花 = m_HMLPaly.FindAction("棍花", throwIfNotFound: true);
		m_HMLPaly_饮酒 = m_HMLPaly.FindAction("饮酒", throwIfNotFound: true);
		m_HMLPaly_AK = m_HMLPaly.FindAction("AK", throwIfNotFound: true);
		m_HMLPaly_面板 = m_HMLPaly.FindAction("面板", throwIfNotFound: true);
		m_HMLPaly_修行 = m_HMLPaly.FindAction("修行", throwIfNotFound: true);
		m_HMLPaly_AnyKeyDown = m_HMLPaly.FindAction("AnyKeyDown", throwIfNotFound: true);
		m_HMLPaly_AnyKeyDown键盘 = m_HMLPaly.FindAction("AnyKeyDown键盘", throwIfNotFound: true);
		m_HMLPaly_AnyKeyDown手柄 = m_HMLPaly.FindAction("AnyKeyDown手柄", throwIfNotFound: true);
	}

	public void Dispose()
	{
		UnityEngine.Object.Destroy(asset);
	}

	public bool Contains(InputAction action)
	{
		return asset.Contains(action);
	}

	public IEnumerator<InputAction> GetEnumerator()
	{
		return asset.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Enable()
	{
		asset.Enable();
	}

	public void Disable()
	{
		asset.Disable();
	}

	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return asset.FindBinding(bindingMask, out action);
	}
}
