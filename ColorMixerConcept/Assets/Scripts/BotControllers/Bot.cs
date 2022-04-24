using System;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
	[SerializeField] private List<Recetps> recept = new List<Recetps>();
	[SerializeField] private MeshRenderer colorShow;
	[SerializeField] private Animator animator;
	[SerializeField] private float speedPlayer = 0.5f;
	[SerializeField] private List<Vector3> waypoints = new List<Vector3>();

	private Vector3 startPosition;
	private Receipt receipt;
	private int curentReceptInd = 0;
	private Color colorToNeed;
	private string animMooveSpeed = "MoveSpeed";// float
	private string animWave = "Wave"; // triger
	private string animPickup = "Pickup";//triger

	private bool mooving = false;
	
	private int currentWp = 0;

	public Color ColorToNeed { get => colorToNeed; }

	[ContextMenu("AddWayPoints")]
	public void AddWayPoints()
	{
		waypoints.Clear();
		foreach (Transform child in transform)
			if (child.tag == "Waypoint")
				waypoints.Add(child.position);
	}


	private void Awake()
	{
		receipt = GameManager.Instance.Receipt;
	}

	private void Start()
	{
		startPosition = transform.position;
		StartInit();
	}

	private void StartInit()
	{
		colorToNeed = receipt.GetNeedColorReceipt(recept[curentReceptInd].fruitsTypes).ConvertColorUnity();
		SetColorShow(colorToNeed);
		Mooving();
	}

	private void Stop()
	{
		animator.SetFloat(animMooveSpeed, 0);
		mooving = false;
	}

	private void SetColorShow(Color color)
	{
		colorShow.material.SetColor("_BaseColor", color); 
	}

	private void Mooving()
	{
		animator.SetFloat(animMooveSpeed, speedPlayer);
		mooving = true;
	}


	private void Update()
	{
		if (mooving)
		{
			MoveTo();
		}
	}
	private void MoveTo()
	{
		Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
		Vector3 posW = new Vector3(waypoints[currentWp].x, 0, waypoints[currentWp].z);
		transform.LookAt(new Vector3(posW.x, transform.position.y, posW.z));

		transform.position = Vector3.MoveTowards(pos, posW, speedPlayer * Time.deltaTime);

		if (Vector3.Distance(pos, posW) < 0.01f)
		{
			Stop();
			if (currentWp == 0) // standing next to the table
			{
				transform.Rotate(0, 90, 0);
				animator.SetTrigger(animWave);
				EventDispatcher.SendEvent(EventNames.SetPlanelInteractable);
			}
			else 
			{
				ResetPlayer();
			}
		}
	}

	private void ResetPlayer()
	{
		if (curentReceptInd < recept.Count - 1)
		{
			curentReceptInd++;
		}
		else
		{
			curentReceptInd = 0; // game over or reset
		}
		currentWp = 0;
		StartInit();
		transform.position = startPosition;
		StartInit();
	}

	public void DoneRecept()
	{
		EventDispatcher.SendEvent(EventNames.SetPlanelNoInteractable);

		animator.SetTrigger(animPickup);
		this.WaitSecond(1, () =>
		{
			currentWp++;
			animator.SetFloat(animMooveSpeed, speedPlayer);
			mooving = true;
		});
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		foreach (Transform child in transform)
		{
			if (child.tag == "Waypoint")
				Gizmos.DrawWireSphere(child.position, .3f);
		}
	}
}


[Serializable]
public class Recetps
{
	public String Name;
	public List<FruitsType> fruitsTypes = new List<FruitsType>();
}