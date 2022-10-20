using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sight : MonoBehaviour
{
    public PlayerControl player=null;
    public Camera renderCamera=null;
    public float sightDistance=15f;
    CanvasScaler canvasScaler=null;
    RectTransform rectTransform=null;
    Canvas parentCanvas=null;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform=GetComponent<RectTransform>();
        if(rectTransform==null)
        {
            print(gameObject.name+": the HUD sight needs a rect transform");
            return;
        }
        parentCanvas=transform.parent.GetComponent<Canvas>();
        if(rectTransform==null)
        {
            print(parentCanvas.name+": the HUD sight needs to be in a canvas");
            return;
        }
        if(renderCamera==null)
        {
            print(gameObject.name+": the HUD needs a camera");
            return;
        }
        if(player==null)
        {
            print(gameObject.name+": the HUD needs a player instance");
            return;
        }
        canvasScaler=transform.parent.GetComponent<CanvasScaler>();
        if(canvasScaler==null)
        {
            print(gameObject.name+": no CanvasScaler component in parent");
            return;
        }
        setPosition();
    }

    void setPosition()
    {
        if(rectTransform==null || parentCanvas==null || renderCamera==null || player==null || canvasScaler==null)
            return;
        Vector3 position=renderCamera.WorldToViewportPoint(
            player.estimateSight(sightDistance));
        Vector3 canvasSize=canvasScaler.referenceResolution/2;
        rectTransform.anchoredPosition=new Vector3((position.x-0.5f)*canvasSize.x,(position.y-0.5f)*canvasSize.y);
    }

    // Update is called once per frame
    void Update()
    {
        setPosition();
    }
}
