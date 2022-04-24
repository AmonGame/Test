using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
	[SerializeField] private InstantiateFruits instantiateFruits;
	[SerializeField] private Blender blender;
	[SerializeField] private Receipt receipt;
	[SerializeField] private Bot bot;
	public InstantiateFruits InstantiateFruits { get => instantiateFruits;  }
	public Blender Blender { get => blender; }
	public Receipt Receipt { get => receipt; }
	public Bot Bot { get => bot; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this; 
		}
	}

}
