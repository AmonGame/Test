using UnityEngine;

public class Fruits : MonoBehaviour
{
	[SerializeField] private string itemName;
	[SerializeField] private Sprite sprite;
	[SerializeField] private Color color;
	[SerializeField] private FruitsType fruitsType;

	public Sprite Sprite { get => sprite; }
	public FruitsType FruitsType { get => fruitsType; }
	public Color Color { get => color;}
}

public enum FruitsType
{
	Apple,
	Banana,
	Orange,
	Cherry,
	Tomato,
	Cucumber,
	Eggplant,
}
