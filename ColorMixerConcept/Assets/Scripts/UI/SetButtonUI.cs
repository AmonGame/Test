using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonUI : MonoBehaviour
{
    [SerializeField] private List<Button> buttons = new List<Button>();
	[SerializeField] private CanvasGroup canvasGroup;

	private List<Image> imageButton = new List<Image>();
	private InstantiateFruits instantiateFruits;
	private PoolFruits poolFruits;
	private Blender blender;

	private void Start() 
	{
		instantiateFruits = GameManager.Instance.InstantiateFruits;
		poolFruits = PoolFruits.Instance;
		blender = GameManager.Instance.Blender;

		canvasGroup.interactable = false;

		EventDispatcher.Add(EventNames.SetPlanelInteractable, () =>
		{
			canvasGroup.interactable = true;
		});

		EventDispatcher.Add(EventNames.SetPlanelNoInteractable, () =>
		{
			canvasGroup.interactable = true;
		});

		EventDispatcher.Add(EventNames.Blend, (object[] args) =>
		{
			canvasGroup.interactable = false;
			this.WaitSecond((float)args[0], () => canvasGroup.interactable = true);
		});

		for (int i = 0; i < buttons.Count; i++)
		{
			var image = buttons[i].GetComponent<Image>();
			image.sprite = poolFruits.PooledPrefabs[i].Sprite;
			imageButton.Add(image);
			var z = i;
			buttons[i].onClick.AddListener( () => InstantiateFruit(poolFruits.PooledPrefabs[z]));
		}
	}

	private void InstantiateFruit(Fruits fruits)
	{
		blender.OpenBlender();
		instantiateFruits.InstatntiateFruit(fruits.FruitsType); // deley in instantiate fruits
		canvasGroup.interactable = false;
	}
}
