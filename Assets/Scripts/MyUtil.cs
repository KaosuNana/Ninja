using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtil : MonoBehaviour {

	public static bool isTouched(GameObject obj){

		Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		if (obj.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
		{
			return true;
		}
		return false;
	}

    public static void disableButton(GameObject go)
    {
      //  go.GetComponent<UIButton>().disabledColor = Color.gray;
    //    go.GetComponent<UIButton>().defaultColor = Color.gray;
     //   go.GetComponent<UIButton>().hover = Color.gray;
  //      go.GetComponent<UIButton>().pressed = Color.gray;
     //   go.GetComponent<UISprite>().color = Color.gray;
       
        go.GetComponent<UIButton>().isEnabled = false;
    }
    public static void enableButton(GameObject go)
    {
        //go.GetComponent<UIButton>().disabledColor = Color.white;
      //  go.GetComponent<UIButton>().defaultColor = Color.white;
      //  go.GetComponent<UIButton>().hover = Color.white;
      //  go.GetComponent<UIButton>().pressed = Color.white;
  //      go.GetComponent<UISprite>().color = Color.white;
        go.GetComponent<UIButton>().isEnabled = true;
    }
}
