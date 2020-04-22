using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BarrierControl : MonoBehaviour
{
	[Serializable]
	private class BarrierKey
	{
		#region Nested Editor Variables
		[SerializeField]
		[Tooltip("Describes the color of this Barrier Key.")]
		private LodestoneColor _color;

		[SerializeField]
		[Tooltip("To open the barrier, you will need this many Barrier Keys.")]
		private int _amount;
		#endregion

		#region Nested Properties
		public LodestoneColor KeyColor { get { return _color; } }
		public int KeyAmount { get { return _amount; } set { _amount = value; } }
		#endregion

		public BarrierKey(LodestoneColor color, int amount)
		{
			_color = color;
			_amount = amount;
		}
	}

	#region Editor Variables
	[SerializeField]
	[Tooltip("The colors and amounts of Lodestones needed to bypass this Barrier.")]
	private List<BarrierKey> _barrierKeys = GetDefaultBarrierKeys();

	[SerializeField]
	[Tooltip("The light used by this barrier.")]
	private Light _barrierLight = null;
	#endregion

	#region Class Fields
	private List<BarrierKey> _currentKeys;
	private BoxCollider _barrierCollider;
	#endregion

	void Start ()
	{
		_barrierCollider = GetComponent<BoxCollider>();
		_currentKeys = new List<BarrierKey>();

		foreach (BarrierKey key in _barrierKeys)
		{
			_currentKeys.Add(new BarrierKey(key.KeyColor, 0));
		}
	}

	void LateUpdate ()
	{
		bool isOpen = true;

		for (int i = 0; i < _barrierKeys.Count; i++)
		{
			isOpen = isOpen && _currentKeys[i].KeyAmount >= _barrierKeys[i].KeyAmount;
		}

		_barrierCollider.isTrigger = isOpen;
		_barrierLight.enabled = !isOpen;

		foreach (BarrierKey key in _currentKeys)
		{
			key.KeyAmount = 0;
		}
	}
	
	public void AddKey(LodestoneColor color)
	{
		foreach (BarrierKey key in _currentKeys)
		{
			if (key.KeyColor == color)
			{
				key.KeyAmount++;
				return;
			}
		}
	}

	static List<BarrierKey> GetDefaultBarrierKeys()
	{
		List<BarrierKey> list = new List<BarrierKey>();
		foreach (LodestoneColor c in Enum.GetValues(typeof(LodestoneColor))) list.Add(new BarrierKey(c, 0));
		return list;
	}
}
