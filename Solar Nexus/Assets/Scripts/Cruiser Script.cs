using UnityEngine;

public class CruiserScript : MonoBehaviour
{
    [SerializeField] CharacterController ShipController;
    [SerializeField] int sens, lockVertMin, lockVertMax;
    [SerializeField] bool invertY;
    [SerializeField] ParticleSystem DefaultFlame1, DefaultFlame2, DefaultFlame3, DefaultFlame4, BoostFlame;
    //[SerializeField] SphereCollider solarSystemEnd;
    public float speed, BoostMod;

    bool isMoving;
    bool boostON;
    float rotX;
    float rotY;
    float rotZ;
    Vector3 moveDir;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        Boost();

        // Play flame ONLY when moving forward/backward (Z direction)
        if (moveDir.z != 0)
        {
            DefaultFlame1.gameObject.SetActive(true);
            DefaultFlame2.gameObject.SetActive(true);
            DefaultFlame3.gameObject.SetActive(true);
            DefaultFlame4.gameObject.SetActive(true);
        }
        else  {
            DefaultFlame1.gameObject.SetActive(false);
            DefaultFlame2.gameObject.SetActive(false);
            DefaultFlame3.gameObject.SetActive(false);
            DefaultFlame4.gameObject.SetActive(false);
        }
        if (boostON && moveDir.z != 0)
            BoostFlame.gameObject.SetActive(true);
        else
            BoostFlame.gameObject.SetActive(false);

        // Get input
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        float keyLR = Input.GetAxis("Horizontal") * sens * Time.deltaTime;

        // Apply rotations using local axes instead of world space
        if (invertY)
        {
            transform.Rotate(mouseY, -mouseX, keyLR, Space.Self);
        }
        else
        {
            transform.Rotate(-mouseY, mouseX, -keyLR, Space.Self);
        }
    }

    void Movement()
    {
        moveDir = Input.GetAxis("Vertical") * transform.forward; // Forward/backward movement
        ShipController.Move(moveDir * speed * Time.deltaTime);
    }

    void Boost()
    {
        
        if (Input.GetButtonDown("Boost"))
        {
            boostON = true;
            speed *= BoostMod;
        }
        else if (Input.GetButtonUp("Boost"))
        {
            speed /= BoostMod;
            boostON = false;
        }
    }
   
}
