using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireBaseAuthentication : MonoBehaviour
{
    public static FireBaseAuthentication Instance;
    [SerializeField] private GameObject uiLogin;
    [SerializeField] private GameObject uiQuanDoi;
    [SerializeField] private GameObject uiDialog;
    [SerializeField] private Image avatarDefault;
    [SerializeField] private InputField ipUserName;
    [SerializeField] private InputField ipRegisterEmail;
    [SerializeField] private InputField ipRegisterPass;
    [SerializeField] private Button btnRigister;

    [SerializeField] private InputField ipLoginEmail;
    [SerializeField] private InputField ipLoginPass;
    [SerializeField] private Button btnOngame;

    [SerializeField] private Userform userform;
    [SerializeField] private ResourcesHub _resourcesHub;
    [SerializeField] private Dialog _dialog;
    
    private FirebaseUser _idUser;
    private FirebaseAuth _auth;
    private DatabaseReference _reference;

    private void Awake()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        Instance = this;
        btnOngame.onClick.AddListener(LoginEmailWithFirebase);
        btnRigister.onClick.AddListener(CreateAccountWithFirebase);
    }

    public void CreateAccountWithFirebase()
    {
        string email = ipRegisterEmail.text;
        string pass = ipRegisterPass.text;
        _auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if ( task.IsFaulted)
            {
                uiDialog.SetActive(true);
                StartCoroutine(_dialog.SetDialogSmooth("tạo tài khoản thất bại !"));
                return;
            }
            if (task.IsCanceled)
            {
                uiDialog.SetActive(true);
                StartCoroutine(_dialog.SetDialogSmooth("tạo tài khoản bị hủy xem lại kết nối mạng !"));
                return;
            }
            if (task.IsCompleted)
            {
                _idUser = task.Result.User;
                SaveUserInformation(ipUserName.text,1,1);
                SaveResources(0,0);
                SaveLevelWave(1);
                SaveLevelTower(1);
                SaveInforHero();
                SaveLevelSoldier(1);
                SaveUserKill();
                SaveItem(null);
                uiDialog.SetActive(true);
                StartCoroutine(_dialog.SetDialogSmooth("Tạo tài khoản thành công !"));
            }
            
        });
    }
    public void LoginEmailWithFirebase()
    {
        string email = ipLoginEmail.text;
        string pass = ipLoginPass.text;
        _auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                uiDialog.SetActive(true);
                StartCoroutine(_dialog.SetDialogSmooth("Đăng nhập thất bại mất kết nối !"));
                return;
            }
            if(task.IsFaulted)
            {
                uiDialog.SetActive(true);
                StartCoroutine(_dialog.SetDialogSmooth("Đăng nhập thất bại !"));
                return;
            }
            if(task.IsCompleted)
            {
                uiDialog.SetActive(true);
                StartCoroutine(_dialog.SetDialogSmooth("Đăng nhập thành công !"));
                _idUser = FirebaseAuth.DefaultInstance.CurrentUser;
                LoadUserInformation();
                LoadResources();
                LoadLevelWave((() => {}));
                LoadLevelTower(() => 
                {
                    uiQuanDoi.SetActive(true);
                    GameController.Instance.SetUpGameController();
                    UiQuanDoi.Instance.SetUpUiTower();
                });
                LoadLevelSoldier(() =>
                {
                    uiQuanDoi.SetActive(true);
                    GameController.Instance.SetUpGameController();
                    UiQuanDoi.Instance.SetUpUiSoldier();
                    
                });
                LoadInforHero();
                LoadUserSkill();
                LoadItem();
                uiLogin.SetActive(false);
            }
        });
    }
    public void SaveUserInformation(string userName,int level, int expCurrent)
    {
        UserInformation userInformation = new UserInformation(userName,level,expCurrent);
        _reference.Child("User").Child(_idUser.UserId).Child("userInformation").SetValueAsync(userInformation.ToString())
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu thông tin người dùng thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu thông tin thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu thông tin bị hủy");
                }
            });
    }

    public void LoadUserInformation()
    {
        _reference.Child("User").Child(_idUser.UserId).Child("userInformation").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load userInformation bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load userInformation that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                DataSnapshot dataSnapshot = task.Result;
                UserInformation userInformation =
                    JsonConvert.DeserializeObject<UserInformation>(dataSnapshot.Value.ToString());
                Debug.Log("load userInformation thanh cong ");
                userform.SetUpUserForm(userInformation.level,userInformation.expCurrent,userInformation.userName);
            }
        });
    }

    public void FirebaseUpdateWhenEndBattle()
    {
        SaveUserInformation(Userform.Instance.UserName,userform.Level,userform.ExpCurrent);
        SaveResources(_resourcesHub.Diamond,_resourcesHub.Monney);
    }

    public void SaveResources(int diamond, int monney)
    {
        Resources resources = new Resources(diamond, monney);
        _reference.Child("User").Child(_idUser.UserId).Child("Resources").SetValueAsync(resources.ToString())
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu tài nguyên thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu tài nguyên thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu tài nguyên bị hủy");
                }
            });
    }

    public void LoadResources()
    {
        _reference.Child("User").Child(_idUser.UserId).Child("Resources").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load userInformation bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load userInformation that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                DataSnapshot dataSnapshot = task.Result;
                Resources resources = JsonConvert.DeserializeObject<Resources>(dataSnapshot.Value.ToString());
                _resourcesHub.SetResources(resources.diamond, resources.monney);
            }
        });
    }
    
    public void SaveLevelTower(int level)
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelTower").SetValueAsync(level)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu cấp tháp thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu cấp tháp thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu cấp tháp bị hủy");
                }
            });
    }

    public void LoadLevelTower(Action ReLoad)
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelTower").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load cấp tháp bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load cấp tháp that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load level tháp thành công");
                TowerUnit.Instance.Level = Convert.ToInt32(task.Result.Value.ToString());;
                ReLoad?.Invoke();
            }
        });
    }

    public void SaveLevelWave(int level)
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelWave").SetValueAsync(level)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu cấp màn thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu cấp màn thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu cấp màn bị hủy");
                }
            });
    }

    public void LoadLevelWave(Action Reload)
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelWave").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load cấp màn bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load cấp màn that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load level màn thành công");
                SpamEnemy.Instance.LevelBattle = Convert.ToInt32(task.Result.Value.ToString());;
                Reload?.Invoke();
            }
        });
    }

    public void SaveInforHero()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        var listAllHero = ListAllHero.Instance.Heros;
        foreach (var hero in listAllHero)
        {
            string keyHero = hero.HeroBase.NewName;
            int level = hero.Level;
            data.Add(keyHero,level);
        }
        _reference.Child("User").Child(_idUser.UserId).Child("LevelHero").SetValueAsync(data)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu cấp tháp thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu cấp tháp thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu cấp tháp bị hủy");
                }
            });
    }

    public void LoadInforHero()
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelHero").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load Hero bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load hero màn that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load hero thành công");
                DataSnapshot snapshot = task.Result;
                Dictionary<string, object> data = snapshot.Value as Dictionary<string, object>;
                foreach (var hero in ListAllHero.Instance.Heros)
                {
                    foreach (KeyValuePair<string,object> kvp in data)
                    {
                        Debug.Log("vao");
                        if (kvp.Key == hero.HeroBase.NewName)
                        {
                            Debug.Log(kvp);
                            hero.Level = Convert.ToInt32(kvp.Value);
                        }   
                    }
                }
            }
        });
    }

    public void SaveItem(ListItem _listItem)
    {
        List<NameItemLevel> datas = new List<NameItemLevel>();
        var listItemUser = _listItem.Items;
        if(listItemUser.Count == 0) return;
        foreach (var itemUser in listItemUser)
        {
            NameItemLevel nameItemLevel = new NameItemLevel(itemUser.ItemBase.Name, itemUser.Level);
            datas.Add(nameItemLevel);
        }
        string jsonData = JsonConvert.SerializeObject(datas);
        _reference.Child("User").Child(_idUser.UserId).Child("Item").SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(
            task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("load Item bi huy bo ");
                }

                if (task.IsFaulted)
                {
                    Debug.Log("load Item màn that bai " + task.IsFaulted);
                }

                if (task.IsCompleted)
                {
                    Debug.Log("Load Item thành công");
                }
            }
        );
    }
    public void LoadItem()
    {
        _reference.Child("User").Child(_idUser.UserId).Child("Item").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load Hero bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load hero màn that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load hero thành công");
                string data = task.Result.GetRawJsonValue();
                List<NameItemLevel> nameItemLevel = JsonConvert.DeserializeObject<List<NameItemLevel>>(data);
                Debug.Log(data);
                if(data == null) return;
                foreach (var item in ListItemBase.Instance.ItemBases)
                {
                    foreach (var itemp in nameItemLevel)
                    {
                        if (itemp.itemName == item.Name)
                        {
                            Item pitem = new Item(item,itemp.level);
                            ListItem.Instante.Items.Add(pitem);
                        }   
                    }
                }
            }
        });
    }
    public void SaveItemHero(List<Hero> heroes)
    {
        List<ItemOfHero> datas = new List<ItemOfHero>();
        var listHero = heroes;
        foreach (var hero in listHero)
        {
            Item itemSword = hero.Sword;
            Item itemArmor = hero.Armor;
            ItemOfHero itemOfHeroSword = new ItemOfHero();
            ItemOfHero itemOfHeroArmor = new ItemOfHero();
            if (itemSword != null)
            {
                itemOfHeroSword = new ItemOfHero(itemSword.ItemBase.Name,itemSword.Level,hero.HeroBase.NewName,true);
            }

            if (itemArmor != null)
            {
                itemOfHeroArmor = new ItemOfHero(itemArmor.ItemBase.Name,itemArmor.Level,hero.HeroBase.NewName,false);
            }
            datas.Add(itemOfHeroSword);
            datas.Add(itemOfHeroArmor);
        }
        string jsonData = JsonConvert.SerializeObject(datas);
        _reference.Child("User").Child(_idUser.UserId).Child("ItemOfHero").SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(
            task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("load Item bi huy bo ");
                }

                if (task.IsFaulted)
                {
                    Debug.Log("load Item màn that bai " + task.IsFaulted);
                }

                if (task.IsCompleted)
                {
                    Debug.Log("Load Item thành công");
                }
            }
        );
    }
    public void LoadItemOfHero()
    {
        _reference.Child("User").Child(_idUser.UserId).Child("ItemOfHero").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load Hero bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load hero màn that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load hero thành công");
                string data = task.Result.GetRawJsonValue();
                List<ItemOfHero> itemOfHeroes = JsonConvert.DeserializeObject<List<ItemOfHero>>(data);
                if(data == null) return;
                foreach (var itemOfHero in itemOfHeroes)
                {
                    foreach (var hero in ListHeros.Instance.heroes)
                    {
                        if (itemOfHero.heroName == hero.HeroBase.NewName)
                        {
                            foreach (var itemBase in ListItemBase.Instance.ItemBases)
                            {
                                if (itemOfHero.itemName == itemBase.Name)
                                {
                                    Item item = new Item(itemBase, itemOfHero.level);
                                    if (itemOfHero.isSword == true)
                                    {
                                        hero.Sword = item;
                                    }
                                    else if (itemOfHero.isSword == false)
                                    {
                                        hero.Armor = item;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
    }
    public void SaveLevelSoldier(int level)
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelSoldier").SetValueAsync(level)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu cấp chiến binh thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu cấp chiến binh thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu cấp chiến binh bị hủy");
                }
            });
    }

    public void LoadLevelSoldier(Action ReLoad)
    {
        _reference.Child("User").Child(_idUser.UserId).Child("LevelSoldier").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load cấp tháp bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load cấp tháp that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load level tháp thành công");
                Soldiers.Instance.Level = Convert.ToInt32(task.Result.Value.ToString());;
                ReLoad?.Invoke();
            }
        });
    }
    public void SaveUserKill()
    {
        Dictionary<string, object> data = UserSkill.Instance.Data;
        _reference.Child("User").Child(_idUser.UserId).Child("UserSkill").SetValueAsync(data)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Lưu cấp tháp thành công");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Lưu cấp tháp thất bại: " + task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogWarning("Lưu cấp tháp bị hủy");
                }
            });
    }

    public void LoadUserSkill()
    {
        _reference.Child("User").Child(_idUser.UserId).Child("UserSkill").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("load Hero bi huy bo ");
            }
            if(task.IsFaulted)
            {
                Debug.Log("load hero màn that bai " + task.IsFaulted);
            }
            if(task.IsCompleted)
            {
                Debug.Log("Load hero thành công");
                DataSnapshot snapshot = task.Result;
                Dictionary<string, object> data = snapshot.Value as Dictionary<string, object>;
                UserSkill.Instance.Data = data;
                UserSkill.Instance.UpDataInUserSkill();
            }
        });
    }
}

public class UserInformation
{
    public string userName { get; set; }
    public int level { get; set; }
    public int expCurrent { get; set; }

    public UserInformation() { }

    public UserInformation(string userName, int level, int expCurrent)
    {
        this.userName = userName;
        this.level = level;
        this.expCurrent = expCurrent;
    }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
public class NameItemLevel
{
    public string itemName { get; set; }
    public int level { get; set; }

    public NameItemLevel() { }

    public NameItemLevel(string itemName, int level)
    {
        this.itemName = itemName;
        this.level = level;
    }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
public class ItemOfHero
{
    public string itemName { get; set; }
    public int level { get; set; }
    public string heroName { get; set; }
    public bool isSword { get; set; }

    public ItemOfHero() { }

    public ItemOfHero(string itemName, int level, string heroName, bool isSword)
    {
        this.itemName = itemName;
        this.level = level;
        this.heroName = heroName;
        this.isSword = isSword;
    }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class Resources
{
    public int diamond { get; set; }
    public int monney { get; set; }
    public Resources(){}

    public Resources(int diamond,int monney)
    {
        this.diamond = diamond;
        this.monney = monney;
    }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
