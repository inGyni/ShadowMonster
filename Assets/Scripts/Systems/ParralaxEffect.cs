using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    private Transform camTrans;
    private Vector3 lastCamPos;
    public Vector2 Multiplier;
    private float textureUnitSizeX;

    private void Start()
    {
        camTrans = Camera.main.transform;
        lastCamPos = camTrans.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camTrans.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * Multiplier.x, deltaMovement.y * Multiplier.y);
        lastCamPos = camTrans.position;

        if (Mathf.Abs(camTrans.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPosX = (camTrans.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTrans.position.x + offsetPosX, transform.position.y);
        }
    }

}
