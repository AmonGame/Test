using UnityEngine;

public class Blending : MonoBehaviour {
		
	[SerializeField] private Texture2D availableAnimations;
	[SerializeField] private int uvAnimationTileX = 2;
	[SerializeField] private int uvAnimationTileY = 2;
	[SerializeField] private float framesPerSecond = 18.0f;

	private Renderer renderer;
	private int randomAnimIndex = 0;
	private int index;
	private Vector2 size;
	private float uIndex;
	private float vIndex;
	private Vector2 offset;
	private bool enable = false;

	void Start() {
		renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = availableAnimations;
	}

	public void EnableWithColor(Color color)
	{
		renderer.enabled = true;
		enable = true;
		renderer.material.SetColor("_BaseColor", color);
	}

	public void Deactivate()
	{
		enable = false;
		renderer.enabled = false;
	}

	void Update ()
	{
		if (enable)
		{
			index = (int)(Time.time * framesPerSecond);
			index = index % (uvAnimationTileX * uvAnimationTileY);
			size = new Vector2(1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);
			uIndex = index % uvAnimationTileX;
			vIndex = index / uvAnimationTileX;
			offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);
			renderer.material.SetTextureOffset("_BaseMap", offset);
			renderer.material.SetTextureScale("_BaseMap", size);
		}

	}
}