using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Receipt : MonoBehaviour
{
	//private PoolFruits poolFruits;

	//private void Start()
	//{
		
	//}

	public Color GetNeedColorReceipt(List<FruitsType> recept)
	{
		var poolFruits = PoolFruits.Instance;
		var needColors = (from firstItem in poolFruits.PooledPrefabs  //find all color by type fruit
						  join secondItem in recept
						  on firstItem.FruitsType equals secondItem
						  select firstItem.Color).ToList();


		var colorToNeed = needColors.BlendMultipleColor();  // blend colors

		return colorToNeed;
	}

	public void ComparisonColor(List<Fruits> blender, Color colorToNeed)
	{
		var colorByBlender = GetColorBlender(blender);

		colorByBlender.DebColor(Deb.ColorText.green);

		ColorComparison.ColorCompare(colorToNeed, colorByBlender).ToString().Debag();
	}


	public Color GetColorBlender(List<Fruits> blender)
	{
		List<Color> returned = new List<Color>();
		for (int i = 0; i < blender.Count; i++)
		{
			returned.Add(blender[i].Color);
		}
		var color = returned.BlendMultipleColor();
		return color;
	}
}
