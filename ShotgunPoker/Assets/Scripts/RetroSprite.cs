using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RetroSprite : MonoBehaviour {

    public Sprite sprite;

    [SerializeField, HideInInspector]
    private Sprite[] _sprites;

    [SerializeField, HideInInspector]
    private SpriteRenderer _spriteRenderer;

    //Pushing this code back to OnValidate so this stuff doesn't have to execute at runtime
    private void Start()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        else if (gameObject.GetComponentInChildren<SpriteRenderer>() != null)
        {
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if ( sprite != null )
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.sortingLayerName = "Default";
            _spriteRenderer.sortingOrder = 1;

            Object[] data = AssetDatabase.LoadAllAssetRepresentationsAtPath( AssetDatabase.GetAssetPath(sprite) );
            _sprites = new Sprite[data.Length];
            for (int i = 0; i < data.Length; i++)   
            {
                _sprites[i] = (Sprite)data[i];
            }
            // Debug.Log("Set sprite " + AssetDatabase.GetAssetPath(sprite) + " and created an array of sprites of length " + _sprites.Length );
        }
    }

    public void Update ()
    {

    }


    public void setFrameIndex(int index)
    {
        // if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = _sprites[index];
    }
}