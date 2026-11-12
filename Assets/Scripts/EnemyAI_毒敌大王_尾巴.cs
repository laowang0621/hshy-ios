using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_毒敌大王_尾巴 : MonoBehaviour
{
	public List<Transform> _List_Tsf_尾巴节点;

	[Range(-10f, 10f)]
	public float _旋转角度;

	public void F_应用旋转角度(float 旋转角度)
	{
		_旋转角度 = Mathf.Clamp(旋转角度, -10f, 5f);
		if (旋转角度 < 0f)
		{
			_List_Tsf_尾巴节点[0].localRotation = Quaternion.Euler(0f, 0f, _旋转角度);
			_List_Tsf_尾巴节点[1].localRotation = Quaternion.Euler(0f, 0f, _旋转角度);
			_List_Tsf_尾巴节点[2].localRotation = Quaternion.Euler(0f, 0f, _旋转角度);
			_List_Tsf_尾巴节点[3].localRotation = Quaternion.Euler(0f, 0f, _旋转角度);
			_List_Tsf_尾巴节点[4].localRotation = Quaternion.Euler(0f, 0f, _旋转角度);
			_List_Tsf_尾巴节点[5].localRotation = Quaternion.Euler(0f, 0f, _旋转角度);
		}
		else
		{
			_List_Tsf_尾巴节点[0].localRotation = Quaternion.Euler(0f, 0f, 5f * _旋转角度);
			_List_Tsf_尾巴节点[1].localRotation = Quaternion.Euler(0f, 0f, 0f - _旋转角度);
			_List_Tsf_尾巴节点[2].localRotation = Quaternion.Euler(0f, 0f, 0f - _旋转角度);
			_List_Tsf_尾巴节点[3].localRotation = Quaternion.Euler(0f, 0f, 0f - _旋转角度);
			_List_Tsf_尾巴节点[4].localRotation = Quaternion.Euler(0f, 0f, 0f - _旋转角度);
			_List_Tsf_尾巴节点[5].localRotation = Quaternion.Euler(0f, 0f, 0f - _旋转角度);
		}
	}
}
