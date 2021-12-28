using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mouseHitPoint;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform basePoint;
    [SerializeField] LineRenderer lineVisual;
    [SerializeField] private GameObject projectile;
    [SerializeField] Text poolSizeText;
    [SerializeField] ObjectPool objectPool;
    [Space]
    [Header("Sliders")]
    [SerializeField] Slider heightSlider;
    [Header("Variables")]
    private Camera cam;
    [SerializeField] private int _lineSegment = 150;
    [SerializeField] public bool _isLocationSet = false;
    [SerializeField] private float timeCollapse = 1.0f;
     
   

   
    void Start()
    {
        objectPool = GameObject.FindObjectOfType<ObjectPool>();  
        cam = Camera.main;
        lineVisual.positionCount = _lineSegment;
    }
    
    void Update()
    {
        timeCollapse -= Time.deltaTime;
        if(_isLocationSet)
        {
            LaunchProjectile();
        }
            
        
      
    }
   
    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(camRay,out hit,100f,layer))
        {
            mouseHitPoint.SetActive(true);
            mouseHitPoint.transform.position = hit.point + Vector3.up * 0.5f;
            Vector3 Vo = CalculateVelocity(hit.point, basePoint.position, heightSlider.value);
            Visualize(Vo);
            if (Input.GetMouseButtonDown(0) && timeCollapse<=0)
            {
                timeCollapse=1;
                Rigidbody obj = Instantiate(objectPool.GetPooledObject(), basePoint.position, Quaternion.identity);
                obj.velocity = Vo;
                objectPool.poolSize--;
                objectPool.poolSizeText.text= "Remaining Balls In The Pool:  " +objectPool.poolSize;
                
                
            }
             }else
                 {
                    mouseHitPoint.SetActive(false);
                 }
    }



    Vector3 CalculateVelocity(Vector3 targetPoint,Vector3 basePoint,float time)
    {
        //Define the distance between basePoint and targetPoint
        Vector3 distance = targetPoint - basePoint;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0.0f;

        // Create variables that represent the distance
        float Sy = distance.y; // vertical height
        float Sxz = distanceXZ.magnitude; // distance on XZ plane

        //Velocities
        float Vxz = (Sxz / time) ; //horizontal plane velocity
        float Vy = (Sy / time)+ 0.5f * (Mathf.Abs(Physics.gravity.y)) * time ; // vertical velocity
        Vector3 result = distanceXZ.normalized*Vxz;
        //result*= Vxz;
        result.y = Vy;

        return result ;
    }

    Vector3 CalculatePositionInTime(Vector3 Vo,float time)
    {
        Vector3 Vxz = Vo;
        Vxz.y = 0.0f;
        Vector3 result = basePoint.position +Vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (Vo.y * time) + basePoint.position.y;
        result.y = sY;

        return result;

    }

    private void Visualize(Vector3 Vo)
    {     
            for (int i = 0; i < _lineSegment; i++)
            {
                Vector3 pos = CalculatePositionInTime(Vo, i / (float)_lineSegment * heightSlider.value);
                lineVisual.SetPosition(i, pos);
            }       
    }

    public void SetBasePosition()
    {
       
        _isLocationSet = true;
        projectile.gameObject.GetComponent<LineRenderer>().enabled = true;
    }
    public void ChangeBasePosition()
    {
       
        _isLocationSet = false;
        projectile.gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    

}
