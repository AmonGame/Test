using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Blending blend;
    [SerializeField] private Button blendButton;
    [SerializeField] private float timeBlend = 3f;
    [Header("Settings Shaked")]
    [Range(0f, 2f)]
    [SerializeField] private float time = 0.2f;
    [Range(0f, 2f)]
    [SerializeField] private float distance = 0.1f;
    [Range(0f, 0.1f)]
    [SerializeField] private float delayBetweenShakes = 0f;
  

    private Vector3 startPos;
    private float timer;
    private Vector3 randomPos;
    private string openAnimation = "Open";
    private InstantiateFruits instantiateFruits;
    private Color color;

	public Color Color { get => color; }

	private void Awake()
    {
        startPos = transform.position;
        instantiateFruits = GameManager.Instance.InstantiateFruits;
    }

	private void Start()
	{
        blendButton.onClick.AddListener(Blend);
    }

	private void OnValidate()
    {
        if (delayBetweenShakes > time)
            delayBetweenShakes = time;
    }


    public void OpenBlender()
	{
        animator.SetBool(openAnimation, true);
        blendButton.interactable = false; // &
    }

    public void CloseBlender()
    {
        animator.SetBool(openAnimation, false);

        blendButton.interactable = true;// &

        EventDispatcher.SendEvent(EventNames.SetPlanelInteractable);
    }

	private void OnTriggerEnter(Collider other)
	{
        AddFruit();
    }

	public void AddFruit()
	{
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        timer = 0f;

        while (timer < time)
        {
            timer += Time.deltaTime;

            randomPos = startPos + (Random.insideUnitSphere * distance);

            transform.position = randomPos;

            if (delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        transform.position = startPos;
        CloseBlender();
    }


    private void Blend()
	{
        if (instantiateFruits.GetCountFruitScene() > 0)
        {
            color = instantiateFruits.GetColorInBlender();
           
            EventDispatcher.SendEvent(EventNames.Blend, timeBlend);
            blendButton.interactable = false;
            blend.EnableWithColor(color);

            this.WaitSecond(timeBlend, () =>
            {
                blend.Deactivate();
                blendButton.interactable = true;
            });
        }
    }
}
