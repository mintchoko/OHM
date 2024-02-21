using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

 
    private static GameObject zombieCamera1;
    private static GameObject zombieCamera2;
    private static GameObject zombie1;
    private static GameObject zombie2;
    private static GameObject health1;
    private static GameObject health2;
    private static GameObject statusText;
    private static GameObject cube1;
    private static GameObject cube2;
    private static Vector3 cube1Position;
    private static Vector3 cube2Position;

    private static bool[] usedItem1;
    private static bool[] usedItem2;
    private static int selectedItem;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            
            return _instance;
        }
    }

    void Awake()
    {
        Score = 0;
        power = -100.0f;
        Initial();
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    // 초기 오브젝트 initial
    public static void Initial()
    {
        ZombieCamera1 = GameObject.FindGameObjectWithTag("zombieCamera1");
        ZombieCamera2 = GameObject.FindGameObjectWithTag("zombieCamera2");


        Zombie1 = GameObject.FindGameObjectWithTag("zombie1");
        Zombie2 = GameObject.FindGameObjectWithTag("zombie2");

        Health1 = GameObject.FindGameObjectWithTag("healthbar1");
        Health2 = GameObject.FindGameObjectWithTag("healthbar2");

        Cube1 = GameObject.FindGameObjectWithTag("box1");
        Cube2 = GameObject.FindGameObjectWithTag("box2");
        // cube1 position 초기 위치 저장
        cube1Position = Cube1.transform.position;
        cube2Position = Cube2.transform.position;

        statusText = GameObject.FindGameObjectWithTag("statusText");

        usedItem1 = new bool[4] {false, false, false, false};
        usedItem2 = new bool[4] {false, false, false, false};
        selectedItem = 0;
    }

    // 이제 여기에 상태를 관리하는 로직을 추가할 수 있습니다.
    // 점수
    public int Score { get; set; }
    
    public static float power {get; set;}
    public void IncreaseScore(int points)
    {
        Score += points;
    }

    private static bool isFunctionRunning = false;
    public static bool IsFunctionRunning {
        get{return isFunctionRunning;}
        set{isFunctionRunning = value;}
    }

    private static bool isCollision = false;
    public static bool IsCollision {
        get{return isCollision;}
        set{isCollision = value;}
    }

    private static bool turn = false;
    // 플레이어 차례
    public static bool Turn {
        get { return turn; }
        set { turn = value; }
    }
    public static GameObject ZombieCamera1 {
        get {return zombieCamera1;}
        set { zombieCamera1 = value; }
    }

    public static GameObject ZombieCamera2 {
        get {return zombieCamera2;}
        set { zombieCamera2 = value; }

    }

    public static GameObject Zombie1{
        get {return zombie1;}
        set { zombie1 = value; }

    }

    public static GameObject Zombie2{
        get {return zombie2;}
        set { zombie2 = value; }

    }

    public static GameObject Health1{
        get{return health1;}
        set{health1 = value;}
    }
    public static GameObject Health2{
        get{return health2;}
        set{health2 = value;}
    }
    public void Start(){
        GameManager.Initial();
    }

    public static GameObject Cube1{
        get{return cube1;}
        set{cube1 = value;}
    }
    public static GameObject Cube2{
        get{return cube2;}
        set{cube2 = value;}
    }
    public static Vector3 Cube1Position {
        get{return cube1Position;}
        set{cube1Position = value;}
    }
    public static Vector3 Cube2Position {
        get{return cube2Position;}
        set{cube2Position = value;}
    }

    public static GameObject StatusText {
        get{return statusText;}
        set{statusText = value;}
    }
    public static void ChangeTurn() {
        turn = !turn;
        GameManager.SelectedItem = 0;
        GameManager.power = -100.0f;
        GameManager.Cube1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameManager.Cube2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
    public static bool[] UsedItem1{
        get{return usedItem1;}
        set{
            for (int i = 0; i < usedItem1.Length && i < value.Length; i++)
            {
                usedItem1[i] = value[i];
            }
}
    }

    public static bool[] UsedItem2{
        get{return usedItem2;}
        set{for (int i = 0; i < usedItem2.Length && i < value.Length; i++)
            {
                usedItem2[i] = value[i];
            }}
    }
    public static void Using(int i){
        if(!GameManager.Turn){
            usedItem1[i] = true;
        }
        else{
            usedItem2[i] = true;
        }

    }

    public static int SelectedItem{
        get{return selectedItem;}
        set{selectedItem = value;}
    }
}