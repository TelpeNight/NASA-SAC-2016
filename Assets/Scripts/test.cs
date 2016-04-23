using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    SpriteRenderer _renderer;
    Texture2D _texture;
    int tact = 0;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        //_texture = _renderer.sprite.texture;
	}
	
	// Update is called once per frame
	void Update () {
        Texture2D textre = new Texture2D(200, 200);
        for (int x=0; x<textre.width; ++x)
        {
            for (int y=0; y<textre.height; ++y)
            {
                //if (tact == 0)
                //{
                //    break;
                //}
                if (tact > 0)
                {
                    tact--;
                    textre.SetPixel(x, y, Color.red);
                }
                else
                {
                    textre.SetPixel(x, y, Color.white);
                }
                
            }
            //if (tact == 0)
            //{
            //    break;
            //}
        }
        //_texture.Apply();
        _renderer.sprite = Sprite.Create(textre, _renderer.sprite.rect, _renderer.sprite.pivot);
        tact++;
	}
}
