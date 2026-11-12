using DSB;
using UnityEngine;

public class C_二进墙 : MonoBehaviour
{
	public enum Enum_二进墙类型
	{
		左开右闭,
		左闭右开,
		上开下闭,
		上闭下开
	}

	[Header("只有--左开右闭")]
	public Transform _Tsf_更新边界锚点;

	public Enum_二进墙类型 _Enum_二进墙类型;

	public float _镜头补间时间 = 1f;

	private C_Boss生成器 _C_Boss生成器;

	public void F_接收_Boss生成器(C_Boss生成器 c_Boss生成器)
	{
		base.gameObject.SetActive(value: true);
		_C_Boss生成器 = c_Boss生成器;
	}

	public void F_关闭二进墙()
	{
		switch (_Enum_二进墙类型)
		{
		case Enum_二进墙类型.左开右闭:
			DSB_MonoSingleton<CameraController>.Instance.F_更新左边界(_Tsf_更新边界锚点.position.x, _镜头补间时间);
			break;
		case Enum_二进墙类型.左闭右开:
			DSB_MonoSingleton<CameraController>.Instance.F_更新右边界(_Tsf_更新边界锚点.position.x, _镜头补间时间);
			break;
		case Enum_二进墙类型.上开下闭:
			DSB_MonoSingleton<CameraController>.Instance.F_更新上边界(_Tsf_更新边界锚点.position.y, _镜头补间时间);
			break;
		case Enum_二进墙类型.上闭下开:
			DSB_MonoSingleton<CameraController>.Instance.F_更新下边界(_Tsf_更新边界锚点.position.y, _镜头补间时间);
			break;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer != 3)
		{
			return;
		}
		MyLog.LogError($"C_二进墙>>_C_Boss生成器>>{_C_Boss生成器}");
		if (_C_Boss生成器 == null)
		{
			switch (_Enum_二进墙类型)
			{
			case Enum_二进墙类型.左开右闭:
				if (collision.transform.position.x > base.transform.position.x)
				{
					DSB_MonoSingleton<CameraController>.Instance.F_更新左边界(_Tsf_更新边界锚点.position.x, _镜头补间时间);
				}
				break;
			case Enum_二进墙类型.左闭右开:
				if (collision.transform.position.x < base.transform.position.x)
				{
					DSB_MonoSingleton<CameraController>.Instance.F_更新右边界(_Tsf_更新边界锚点.position.x, _镜头补间时间);
				}
				break;
			case Enum_二进墙类型.上开下闭:
				if (collision.transform.position.y < base.transform.position.y)
				{
					DSB_MonoSingleton<CameraController>.Instance.F_更新上边界(_Tsf_更新边界锚点.position.y, _镜头补间时间);
				}
				break;
			case Enum_二进墙类型.上闭下开:
				if (collision.transform.position.y > base.transform.position.y)
				{
					DSB_MonoSingleton<CameraController>.Instance.F_更新下边界(_Tsf_更新边界锚点.position.y, _镜头补间时间);
				}
				break;
			}
			return;
		}
		switch (_Enum_二进墙类型)
		{
		case Enum_二进墙类型.左开右闭:
			if (collision.transform.position.x > base.transform.position.x)
			{
				_C_Boss生成器.F_关闭二进墙();
			}
			break;
		case Enum_二进墙类型.左闭右开:
			if (collision.transform.position.x < base.transform.position.x)
			{
				_C_Boss生成器.F_关闭二进墙();
			}
			break;
		case Enum_二进墙类型.上开下闭:
			if (collision.transform.position.y < base.transform.position.y)
			{
				_C_Boss生成器.F_关闭二进墙();
			}
			break;
		case Enum_二进墙类型.上闭下开:
			if (collision.transform.position.y > base.transform.position.y)
			{
				_C_Boss生成器.F_关闭二进墙();
			}
			break;
		}
	}

	public void F_生成Boss()
	{
		base.gameObject.SetActive(value: true);
	}

	public void F_Boss死亡()
	{
		DSB_MonoSingleton<MapManager>.Instance.F_得到当前地图().F_更新边界();
		DSB_MonoSingleton<CameraController>.Instance.F_设置补间跟随时间(_镜头补间时间);
		base.gameObject.SetActive(value: false);
	}

	public void F_Boss已死亡()
	{
		base.gameObject.SetActive(value: false);
	}
}
