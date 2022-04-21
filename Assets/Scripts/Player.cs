using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask; // which layer to hit with IsGrounded raycast
    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D capsuleCollider2d;
    private int initialPosition = 0;
    static public bool pauseCheck = true;
    public bool gravityEnabled;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private UIManager uIManager;

    private float jumpPower = 11f;
    private float deadPower = 5.5f;
    public float velocity = 0f;
    private float startingVelocity = 7f;
    private float velocityIncreaser = 1.07f;
    private float maxVelocity = 9.5f;
    private float velocityMilestone = 100f;
    public float playerLastVelocity = 0;

    private int distance;

    [SerializeField] private GameObject playerCoinMagnetCollider;
    public bool magnetCheck = false;
    public bool manualCheck = false;
    private float powerUpTime = 10f;
    private float rotationAngleManual = 20f;
    private float rotationSpeedManual = 5f;
    public bool isInmune = false;
    public bool inmuneCorroutine = true;

    // game status checks
    public bool gameHasStarted = false;
    public bool inputEnabled = false;
    public bool creditsActive = false;
    public bool diedInPortal = false;

    // jump
    public float jumpTimer;
    public bool isJumping = false;

    // skin
    [SerializeField] private DataManagerScript dataManagerScript;
    [SerializeField] private SpriteRenderer playerColor;
    [SerializeField] private SpriteRenderer topColor;
    [SerializeField] private SpriteRenderer botColor;
    [SerializeField] private SpriteRenderer skateColor;
    [SerializeField] private SpriteRenderer leftTireColor;
    [SerializeField] private SpriteRenderer rightTireColor;
    [SerializeField] private SpriteRenderer hat1;
    [SerializeField] private SpriteRenderer hat2;
    public SpriteRenderer halo;
    [SerializeField] private List<SpriteRenderer> spritesList;
    [SerializeField] private SkinsDB skinsDB;

    // particles
    public GameObject sparks;
    private GameObject sparksObject;
    private bool grindObstacle;
    private Vector2 originalSparkPosition;

    // sound
    private float nextSoundTime = 0;
    private float soundDuration = 0.5f;

    // stats
    public int tricks;
    public int gravitySwaps;

    private void Awake()
    {
        // default settings before game starts
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        capsuleCollider2d = transform.GetComponent<CapsuleCollider2D>();
        playerCoinMagnetCollider.SetActive(false);
        gravityEnabled = true;

        // add 
        spritesList = new List<SpriteRenderer>
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        LoadSkin(dataManagerScript.lastSkinUsedSaved);
        LoadSparkParticles();
    }

    private void Update()
    {
        // starts game
        if (Input.touchCount > 0 && !gameHasStarted && !creditsActive)
            IStartGame();

        // checks if touched an UI element 
        // if it dind't the player input is enabled
        if (pauseCheck && !EventSystem.current.IsPointerOverGameObject(0))
        {
            ManageInput();
        }

        // update distance on score
        distance = initialPosition + (int)gameObject.transform.position.x;
        ScoreManager.instance.ChangeDistanceScore(distance);

        // manual
        if (magnetCheck)
            Destroy(GameObject.FindGameObjectWithTag("MagnetCoin"));

        if (manualCheck)
            Destroy(GameObject.FindGameObjectWithTag("DoubleCoins"));

        // grind particles
        ManageSparkParticles(grindObstacle);

        if (transform.position.x >= 750)
            ObjectsDB.zone3Check = true;
        else
            ObjectsDB.zone3Check = false;

        if (isInmune && inmuneCorroutine)
            IInmunity();   
    }

    void FixedUpdate()
    {
        if (velocity <= maxVelocity)
        {
            // increase velocity as level progress
            if (transform.position.x > velocityMilestone)
            {
                velocity *= velocityIncreaser;

                if (velocityMilestone >= 200f && velocityMilestone < 800f)
                    velocityMilestone += 200f;

                if (velocityMilestone < 200f)
                    velocityMilestone += 100f;
            }
        }

        // forward velocity
        rigidbody2d.velocity = new Vector2(velocity, rigidbody2d.velocity.y);

        if (manualCheck)
            DoManual(rotationAngleManual, rotationSpeedManual);
        if (!manualCheck && gravityEnabled)
            DoManual(0, rotationSpeedManual); 
    }

    // skin
    public void LoadSkin(int index)
    {
        skinsDB = new SkinsDB();

        for(int i = 0; i < spritesList.Count; i++)
        {
            spritesList[i].color = skinsDB.GetSkin(index)[i];
        }
    }

    public void IncreaseOrderInLayer()
    {
        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = 120;
        }
        GetComponent<SpriteRenderer>().sortingOrder = 119;
    }

    public void DecreaseOrderInLayer()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = 51;
        }
        GetComponent<SpriteRenderer>().sortingOrder = 50;
    }

    private void IStartGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        gameHasStarted = true;
        velocity = startingVelocity;
        yield return new WaitForSeconds(1);
        inputEnabled = true;
    }

    private void ManageInput()
    {
        if (inputEnabled)
        {
            // Player controller 
            // Input.touchCount registers it for phone
            if (IsGrounded() && Input.touchCount > 0)
                Jump();
        }
    }

    private void Jump()
    {
        if(gravityEnabled)
            rigidbody2d.velocity = Vector2.up * jumpPower;
        if(!gravityEnabled)
            rigidbody2d.velocity = Vector2.down * jumpPower;

        FindObjectOfType<AudioManager>().Play("Jump");
    }

    // add logic to allow jumping only if grounded
    public bool IsGrounded()
    {
        float playerOffset = 0.1f;
        RaycastHit2D raycastHit2d;

        if (gravityEnabled)
        {
            raycastHit2d = Physics2D.BoxCast(capsuleCollider2d.bounds.center,
            capsuleCollider2d.bounds.size, 0f,
            Vector2.down, playerOffset,
            platformsLayerMask);

            if (raycastHit2d.collider != null)
            {
                grindObstacle = raycastHit2d.collider.name.Contains("Handrail") || 
                    raycastHit2d.collider.name.Contains("GrindBench") ||
                    raycastHit2d.collider.name.Contains("Streetlight");

                if ((raycastHit2d.collider.name.Contains("Floor") ||
                    raycastHit2d.collider.name.Contains("Bench") ||
                    raycastHit2d.collider.name.Contains("GrindBench") ||
                    raycastHit2d.collider.name.Contains("Car"))
                    && GetYVelocity() < 0)
                {
                    if (Time.time >= nextSoundTime)
                    {
                        FindObjectOfType<AudioManager>().Play("Ground");
                        nextSoundTime = Time.time + soundDuration;
                    }
                }
            }
                
            return raycastHit2d.collider != null; 
        }

        if (!gravityEnabled)
        {
            raycastHit2d = Physics2D.BoxCast(capsuleCollider2d.bounds.center,
            capsuleCollider2d.bounds.size, 0f,
            Vector2.up, playerOffset,
            platformsLayerMask);

            if (raycastHit2d.collider != null)
            {
                if ((raycastHit2d.collider.name.Contains("Ceiling") ||
                    raycastHit2d.collider.name.Contains("Bench") ||
                    raycastHit2d.collider.name.Contains("GrindBench") ||
                    raycastHit2d.collider.name.Contains("Car"))
                    && GetYVelocity() > 0)
                {
                    if (Time.time >= nextSoundTime)
                    {
                        FindObjectOfType<AudioManager>().Play("Ground");
                        nextSoundTime = Time.time + soundDuration;
                    }
                }
            }
                
            return raycastHit2d.collider != null;
        }

        return false;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetYVelocity()
    {
        return rigidbody2d.velocity.y;
    }

    public void ActivateMagnetCorroutine()
    {
        StartCoroutine(ActivateMagnet(powerUpTime));
    }
    IEnumerator ActivateMagnet(float powerUpTime)
    {
        playerCoinMagnetCollider.SetActive(true);
        magnetCheck = true;

        yield return new WaitForSeconds(powerUpTime);

        playerCoinMagnetCollider.SetActive(false);
        magnetCheck = false;
    }

    public void ActivateDoubleCoinsCorroutine()
    {
        StartCoroutine(ActivateDoubleCoins());
    }

    IEnumerator ActivateDoubleCoins()
    {
        manualCheck = true;
        ScoreManager.instance.doubleCoinsCheck = true;

        yield return new WaitForSeconds(powerUpTime);

        manualCheck = false;
        ScoreManager.instance.doubleCoinsCheck = false;
    }

    private void DoManual(float rotationAngle, float rotationSpeed)
    {
        // x and y are usually for 3d
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(0, 0, rotationAngle),
            rotationSpeed * Time.deltaTime);
    }

    public void DisableManual()
    {
        manualCheck = !manualCheck;
    }

    public void DeathAnimation()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.velocity = Vector2.up * deadPower;

        Color tmp;
        Color32 ghost = new Color32(228, 235, 242, 150);

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = ghost;

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = ghost;
        }

        tmp = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = ghost;
        halo.enabled = true;
    }

    // move down player to appear in the shop view
    public void ShowPlayerInShop()
    {
        // load skin again
        halo.enabled = false;
        LoadSkin(dataManagerScript.lastSkinUsedSaved);

        // avoid unity warnings
        rigidbody2d.constraints = RigidbodyConstraints2D.None;
        rigidbody2d.bodyType = RigidbodyType2D.Static;

        Vector2 offset = new Vector2(0, 2f);
        transform.position = new Vector2(mainCamera.position.x, mainCamera.transform.position.y + offset.y);
        if (manualCheck)
            DisableManual();
        RotateToZero();

        // avoid unity warnings
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ChangeGravity()
    {
        float gravityRotation = 180f;

        if (gravityEnabled)
        {
            gravityEnabled = true;
            Physics2D.gravity = new Vector2(0, -9.8f);

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    
        if (!gravityEnabled)
        {
            gravityEnabled = false;
            Physics2D.gravity = new Vector2(0, 9.8f); // inverts gravity

            transform.rotation = Quaternion.Euler(0, 0, gravityRotation);
        }

        IncreaseGravitySwaps();
    }

    public void ChangeGravityVariable()
    {
        gravityEnabled = !gravityEnabled;
    }

    public void DisableColliders()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void EnableColliders()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public void RotateToZero()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void IInmunity()
    {
        StartCoroutine(Inmunity());
        inmuneCorroutine = false;
    }

    IEnumerator Inmunity()
    {
        float timer = 0f;

        do
        {
            ChangeAlphaToZero();
            yield return new WaitForSeconds(0.25f);
            ChangeAlphaToFull();
            yield return new WaitForSeconds(0.25f);
            timer += 0.5f;

        } while (timer <= 7f);


        isInmune = false;
    }

    private void ChangeAlphaToZero()
    {
        Color32 temp;

        for (int i = 0; i < spritesList.Count; i++)
        {
            temp = spritesList[i].color;
            temp.a = (byte) 0f;
            spritesList[i].color = temp;
        }
    }
    private void ChangeAlphaToFull()
    {
        Color32 temp;

        for (int i = 0; i < spritesList.Count; i++)
        {
            temp = spritesList[i].color;
            temp.a = (byte) 255f;
            spritesList[i].color = temp;
        }
    }

    public void LoadSparkParticles()
    {
        Vector2 offset = new Vector2(-0.8f, -1f); // skate transform
        originalSparkPosition = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, 0);

        sparksObject = Instantiate(sparks,
            originalSparkPosition,
            Quaternion.Euler(0, -90, 0));
        sparksObject.transform.parent = gameObject.transform;
        sparksObject.transform.localScale = Vector3.one;
    }

    public void PlaySparkParticles()
    {
        sparksObject.GetComponent<ParticleSystem>().Play();
    }

    public void StopSparkParticles()
    {
        sparksObject.GetComponent<ParticleSystem>().Stop();
    }

    public void ManageSparkParticles(bool isGrinding)
    {
        if (grindObstacle && GetYVelocity() == 0 && !uIManager.hasGameEnded)
        {
            FindObjectOfType<AudioManager>().Play("Grind");
            PlaySparkParticles();
        }    
        else
        {
            FindObjectOfType<AudioManager>().Stop("Grind");
            StopSparkParticles();
        }
    }

    public void Rotate(int x, int y, int z)
    {
        transform.rotation = Quaternion.Euler(x, y, z);
    }

    public void IncreaseTricks()
    {
        tricks++;
    }

    public void IncreaseGravitySwaps()
    {
        gravitySwaps++;
    }
}
