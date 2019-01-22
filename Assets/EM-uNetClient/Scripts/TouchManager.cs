using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace kde.tech
{
    
public class TouchManager : MonoBehaviour {

    public Camera         targetCamera;    
    public ParticleSystem touchParticle;
    public Transform      cursorGroup;
    public Vector2        m_lastTouchScreenPos = Vector2.zero;
    
    Vector3 touchPos = Vector3.zero;
    bool isTouched = false;
    bool isTouchedPrevFrame = false;
    bool isTouchDown = false;
    
    List<ParticleSystem> m_particlPool;
    ParticleSystem m_lastEmitParticle;
    int m_particlePoolSize = 5;
    int m_particleSeek = 0;

    [System.Serializable]
    public class AmTouchEvent : UnityEvent<Vector2>{}
    
    [SerializeField]
    AmTouchEvent m_anyTouchEvents = new AmTouchEvent();

    public void AddListener(UnityAction<Vector2> handler){
	m_anyTouchEvents.AddListener(handler);
    }
    public void RemoveListener(UnityAction<Vector2> handler){
	m_anyTouchEvents.RemoveListener(handler);
    }
    
    void Start(){
	isTouched          = false;
	isTouchedPrevFrame = false;
	touchPos           = Vector3.zero;	
	isTouchDown        = false;
	Cursor.visible     = true; 
	
	m_particlPool = new List<ParticleSystem>();
	for(int idx = 0; idx < m_particlePoolSize; ++idx){
	    var go = Instantiate(touchParticle.gameObject);
	    go.transform.parent = touchParticle.transform.parent;
	    m_particlPool.Add(go.GetComponent<ParticleSystem>());
	    go.SetActive(false);
	}
	m_lastEmitParticle = m_particlPool[0];

	//AddListener((Vector2 pos) => { Debug.Log("OnAnyTouchHandler : " + pos.ToString()); });
    }
    
    void Update(){

	if(targetCamera == null){ return; }
	
	isTouched = false;
	Vector2 touchPosScreen = Vector2.zero;

	if(Input.GetMouseButton(0)){
	    isTouched = true;
	    touchPosScreen = Input.mousePosition;
	}
	else if(Input.touchCount == 1){
	    TouchPhase touchPhase = Input.GetTouch(0).phase;
	    if (touchPhase == TouchPhase.Began ||
		touchPhase == TouchPhase.Moved ||
		touchPhase == TouchPhase.Stationary)
	    {
		isTouched = true;
		touchPosScreen = Input.GetTouch(0).position;
	    }
	}
	
	if(isTouched){	    
	    touchPos = targetCamera.ScreenToWorldPoint
		(new Vector3(touchPosScreen.x, touchPosScreen.y, 0)); 

	    if(isTouchedPrevFrame){ isTouchDown = true; }
	    else {
		m_lastTouchScreenPos.x = touchPosScreen.x;
		m_lastTouchScreenPos.y = touchPosScreen.y;
		m_anyTouchEvents.Invoke(m_lastTouchScreenPos);
	    }
	}
	else {
	    isTouchDown = false;
	    
	    if(m_lastEmitParticle.isPlaying){
		m_lastEmitParticle.Stop();
	    }
	    
	    if(Input.mousePresent){
		Vector2 mousePosW = targetCamera.ScreenToWorldPoint
		    (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)); 
		
		cursorGroup.position = new Vector3(mousePosW.x, mousePosW.y, cursorGroup.position.z);
	    }	    
	}
	
	if(isTouched){
	    if(!isTouchDown){
		cursorGroup.position = new Vector3(touchPos.x, touchPos.y, cursorGroup.position.z);
		m_lastEmitParticle = m_particlPool[m_particleSeek];
		m_lastEmitParticle.gameObject.SetActive(true);
		m_lastEmitParticle.transform.position = cursorGroup.position;
		m_lastEmitParticle.Play();
		m_particleSeek = (m_particleSeek + 1) % m_particlePoolSize;
	    }
	}
	
	isTouchedPrevFrame = isTouched;	
    }	
	
}
}
