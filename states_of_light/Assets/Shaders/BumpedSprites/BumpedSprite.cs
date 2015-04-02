using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class BumpedSprite : MonoBehaviour
{
	public Texture2D bumpMap;

	private SpriteRenderer spriteRenderer;
	private Texture2D lastBumpMap;

	private MaterialPropertyBlock properties = new MaterialPropertyBlock();
	private MaterialPropertyBlock tmpProps = new MaterialPropertyBlock();

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	private void Start ()
	{
		UpdateRenderer();
	}
	
	// Update is called once per frame
	private void Update()
	{
		if (bumpMap != lastBumpMap)
		{
			UpdateRenderer();
		}
	}

	private void UpdateRenderer()
	{
		properties.Clear();

		spriteRenderer.GetPropertyBlock(tmpProps);
		Texture _MainTex = tmpProps.GetTexture("_MainTex");
		if (_MainTex != null)
		{
			properties.AddTexture("_MainTex", _MainTex);
		}

		if (bumpMap != null)
		{
			properties.AddTexture("_BumpMap", bumpMap);
		}

		spriteRenderer.SetPropertyBlock(properties);
		lastBumpMap = bumpMap;
	}
}
