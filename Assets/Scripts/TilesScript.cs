using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector3 targetPosition; //where puzzle tile should go
    public Vector3 _correctPostion;
    private SpriteRenderer _sprite;
    public int number;
    
    // Start is called before the first frame update
    //Awake called before game starts 
    //allows tiles to save their target positions before is called
    //since it is called before the start() in GameScript
    //otherwise some tiles will move to the center.
    void Awake()
    {
        //targetposition by default is 0,0 so we must r=set the respective block's positions to their starting position
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
        targetPosition = transform.position;
        _correctPostion = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //move object to another point with vector3.lerp and specify how much it moves towards the target position in one fra
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        // if (targetPosition == _correctPostion)
        // {
        //     _sprite.color = Color.green;
        // }
        // else
        // {
        //     _sprite.color = Color.white;
        // }

    }
}
