using UnityEngine;

public class TestArt : MonoBehaviour
{
    [SerializeField] Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isAttacking",!anim.GetBool("isAttacking"));
        }
        if(Input.GetKeyDown(KeyCode.A)) 
        {
            anim.SetTrigger("Happy");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("Get_Hit");
        }
    }
}
