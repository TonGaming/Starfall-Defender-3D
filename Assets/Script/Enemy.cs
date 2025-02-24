using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Score")]
    ScoreBoard scoreBoard;

    [SerializeField] int scorePerKill = 100;

    [Header("Health")]
    [SerializeField] int enemyHealth = 100;
    [SerializeField] int subtractedPerHit = 10;

    [SerializeField] ParticleSystem shootedFX;
    [SerializeField] ParticleSystem explodedFX;
    GameObject parentGameObject;




    void Awake()
    {
        AddRigidbody();
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("RuntimeBin");
    }

    void AddRigidbody()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().useGravity = false;

        }
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void OnParticleCollision(GameObject other)
    {


        ProcessHit();
    }

    void ProcessHit()
    {
        if (enemyHealth > 1)
        {
            enemyHealth -= subtractedPerHit;

            ParticleSystem hitFX = Instantiate(shootedFX, transform.position, Quaternion.identity);
            hitFX.transform.parent = parentGameObject.transform;



        }
        else if (enemyHealth <= 1)
        {
            ParticleSystem explodeFX = Instantiate(explodedFX, transform.position, Quaternion.identity);
            explodeFX.transform.parent = parentGameObject.transform;

            scoreBoard.IncreaseScore(scorePerKill);


            Destroy(gameObject);

        }
    }

    void Update()
    {

    }


}
