using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNGUIScrollPage : MonoBehaviour {
	// Prefab chua UISprite 
	public GameObject indicator;
	// Ten anh dc luu trong atlas
	public string activeName,inactiveName;
	int numberOfPages;
	UIScrollView mScrollView;
	List<GameObject> indicatorList;
	//Grid chua chuoi indicator
	public UIGrid indicatorGrid;
	public int currentPage;
	public GameObject nextButton,previousButton;
	// Use this for initialization
	void Start () {
		mScrollView = GetComponent<UIScrollView> ();
		indicatorList = new List<GameObject> ();
	
		numberOfPages = transform.childCount;
		for (int i = 0; i < numberOfPages; i++) {
			indicatorList.Add ((GameObject)Instantiate (indicator, indicatorGrid.transform));

		}
		indicatorGrid.Reposition ();
		currentPage = -1;

//		mScrollView.centerOnChild.onFinished += onDragFinished;

	}

	public void setCurrentPage(int num){
		deactiveAllIndicators ();
		indicatorList [num].GetComponent<UISprite> ().spriteName = activeName;
		indicatorList [num].GetComponent<UISprite> ().MakePixelPerfect ();
	}

	public void deactiveAllIndicators(){
		for (int i = 0; i < indicatorList.Count; i++) {
			indicatorList [i].GetComponent<UISprite> ().spriteName = inactiveName;
			indicatorList [i].GetComponent<UISprite> ().MakePixelPerfect ();
		}
	}
	public int getPagePos(GameObject obj){
		
		for (int i = 0; i < mScrollView.transform.childCount; i++) {
			Debug.Log ("getPagePos : " + mScrollView.transform.GetChild (i));
			if (obj.name.Equals( mScrollView.transform.GetChild (i).name)) {
				return i;
			}
		}
		return 0;
	}
	void OnEnable() {
		GetComponent<UICenterOnChild> ().onCenter += onCenter;
	}
	void OnDisable(){
	}
	void onCenter(GameObject centeredObject){
	//	Debug.Log ("getPagePos (centeredObject): " +centeredObject);
	//	Debug.Log ("getPagePos (centeredObject): " +getPagePos (centeredObject) );
	
			int pageIndex = getPagePos (centeredObject);
		if (currentPage != pageIndex) {
			
			setCurrentPage (pageIndex);
			currentPage = pageIndex;

			if (currentPage < numberOfPages - 1) {
				MyUtil.enableButton (nextButton);
			} else {
				MyUtil.disableButton (nextButton);	
			}
		
				if (currentPage > 0) {
					MyUtil.enableButton (previousButton);
				} else {
					MyUtil.disableButton (previousButton);	
				}
		}

	
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void nextPage(GameObject nextButton){
		if (currentPage < numberOfPages - 1) {
			GetComponent<UICenterOnChild> ().CenterOn ( mScrollView.transform.GetChild (currentPage+1));

		}
	}
	public void previousPage(GameObject previousButton){
		if (currentPage>0) {
			GetComponent<UICenterOnChild> ().CenterOn ( mScrollView.transform.GetChild (currentPage-1));

		
		}
	}
}
