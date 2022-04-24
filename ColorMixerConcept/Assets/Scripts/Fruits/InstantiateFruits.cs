using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstantiateFruits : MonoBehaviour
{
	[SerializeField] private Transform startPosition;
	[SerializeField] private float timeToSpawn = 1f;
	[SerializeField] private int matchPercentage = 90;
	[SerializeField] private TMP_Text text;
	private List<Fruits> fruitsInstantiated = new List<Fruits>(); 
	private PoolFruits poolFruits;
	private Receipt receipt;
	private Blender blender;
	private Bot bot;
	
	private void Start()
	{
		poolFruits = PoolFruits.Instance;
		receipt = GameManager.Instance.Receipt;
		blender = GameManager.Instance.Blender;
		bot = GameManager.Instance.Bot;
		EventDispatcher.Add(EventNames.Blend, Comparasion);
	}

	public Color GetColorInBlender()
	{
		return receipt.GetColorBlender(fruitsInstantiated).ConvertColorUnity();
	}

	public void InstatntiateFruit(FruitsType fruitsType)
	{
		this.WaitSecond(timeToSpawn, () =>
		{
			fruitsInstantiated.Add(poolFruits.Get(fruitsType, startPosition.position, startPosition));
		});
	}

	private void Comparasion() ///////////////////////////////////////////////////////////
	{
		//receipt.ComparisonColor(fruitsInstantiated); // color comparasion
		Color blenderColor = blender.Color.ConvertColorInt();
		Color botNeed = bot.ColorToNeed.ConvertColorInt();

		var percentComparasion = ColorComparison.ColorCompare(botNeed, blenderColor);
		
		if(percentComparasion < matchPercentage)
		{
			text.color = Color.red;
		}
		else
		{
			text.color = Color.green;
			bot.DoneRecept();
		}

		text.text = percentComparasion + " %"; ;

		this.WaitSecond(2, () => { text.color = Color.blue; text.text = 0 + " %"; });

		for (int i = 0; i < fruitsInstantiated.Count; i++)
		{
			poolFruits.ReturnToPool(fruitsInstantiated[i]);
		}
		fruitsInstantiated.Clear();
	}

	public int GetCountFruitScene()
	{
		return fruitsInstantiated.Count;	
	}

}
