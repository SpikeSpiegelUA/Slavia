using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Item : MonoBehaviour
{
    //Information about item
    public string itemName;
    public int itemDamage;
    public float itemCost;
    public int amountInStack;
    public string itemInventoryTag;
    public GameObject item;
    public string weaponType;
    private bool isLoading = false;
    private void Awake()
    {
        if (SaveLoad.isLoading)
        {
            isLoading = true;
            StartCoroutine("TurnSoundOn");
        }
    }
    private void Start()
    {
        if (itemName == "Waraxe" || itemName == "Топор" || itemName == "Сокира")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().waraxePrefab;
        if (itemName == "Bow" || itemName == "Лук")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().bowPrefab;
        if (itemName == "Dagger" || itemName == "Кинджал" || itemName == "Кинжал")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().daggerPrefab;
        if (itemName == "Arrow" || itemName == "Стрела" || itemName == "Стріла")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().arrowPrefab;
        if (itemName == "Fireball" || itemName == "Огненный шар" || itemName == "Вогняна куля")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().fireballPrefab;
        if (itemName == "Health recover" || itemName == "Восстановление HP" || itemName == "Відновлення HP")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().healthResurection;
        if (itemName == "Восстановление SP" || itemName == "Відновлення SP" || itemName == "Stamina recover")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().staminaResurection;
        if (itemName == "Summon melee" || itemName == "Призыв ББ" || itemName == "Виклик ББ")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().meleeSummon;
        if (itemName == "Summon archer" || itemName == "Призыв ДБ" || itemName == "Виклик ДБ")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().archerSummon;
        if (itemName== "Cavalry helmet" || itemName == "Кавалерійський шолом" || itemName == "Кавалерийский шлем")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().cavalryHelmet;
        if (itemName == "Militia helmet" || itemName == "Гайдамацький шолом" || itemName == "Шлем ополчения")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().militiaHelmet;
        if (itemName == "Iron helmet" || itemName == "Залізний шолом" || itemName == "Железный шлем")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().ironHelmet;
        if (itemName == "Soldier helmet" || itemName == "Солдатський шолом" || itemName == "Солдатский шлем")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().soldierHelmet;
        if (itemName == "Militia armor" || itemName == "Доспех ополчения" || itemName == "Гайдамацький обладунок")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().militiaArmor;
        if (itemName == "Iron armor" || itemName == "Железний доспех" || itemName == "Залізний обладунок")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().ironArmor;
        if (itemName == "Cavalry armor" || itemName == "Кавалерийский доспех" || itemName == "Кавалерійський обладунок")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().cavalryArmor;
        if (itemName == "Militia bow" || itemName == "Лук ополчения" || itemName == "Гайдамацький лук")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().militiaBow;
        if (itemName == "Soldier armor" || itemName == "Солдатський обладунок" || itemName == "Солдатский доспех ")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().soldierArmor;
        if (itemName == "Militia shield" || itemName == "Щит ополчения" || itemName == "Гайдамацький щит")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().militiaShield;
        if (itemName == "Cheap shield" || itemName == "Дешёвый щит" || itemName == "Дешевий щит")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().cheapShield;
        if (itemName == "Professional bow" || itemName == "Профессиональный лук" || itemName == "Професійний лук")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().professionalBow;
        if (itemName == "Big soldier sword" || itemName == "Большой меч солдата" || itemName == "Великий меч солдата")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().bigSoldierSword;
        if (itemName == "Small health potion" || itemName == "Маленькое зелье лечения" || itemName == "Маленьке лікувальне зілля")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().smallHealthPotion;
        if (itemName == "Simple health potion" || itemName == "Обычное зелье лечения" || itemName == "Звичайне лікувальне зілля")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().simpleHealthPotion;
        if (itemName == "Huge health potion" || itemName == "Большое зелье лечения" || itemName == "Велике лікувальне зілля")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().hugeHealthPotion;
        if (itemName == "Small stamina potion" || itemName == "Маленькое зелье выносливости" || itemName == "Маленьке зілля витривалості")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().smallStaminaPotion;
        if (itemName == "Simple stamina potion" || itemName == "Обычное зелье выносливости" || itemName == "Звичайне зілля витривалості")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().simpleStaminaPotion;
        if (itemName == "Huge stamina potion" || itemName == "Большое зелье выносливости" || itemName == "Велике зілля витривалості")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().hugeStaminaPotion;
        if (itemName == "Small mana potion" || itemName == "Маленьке зілля мани" || itemName == "Маленькое зелье маны")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().smallManaPotion;
        if (itemName == "Simple mana potion" || itemName == "Звичайне зілля мани" || itemName == "Обычное зелье маны")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().simpleManaPotion;
        if (itemName == "Huge mana potion" || itemName == "Велике зілля мани" || itemName == "Большое зелье маны")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().hugeManaPotion;
        if (itemName == "Iron sword" || itemName == "Залізний меч" || itemName == "Железный меч")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().ironSword;
        if (itemName == "Big iron sword" || itemName == "Великий залізний меч" || itemName == "Большой железный меч")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().bigIronSword;
        if (itemName == "Militia sword" || itemName == "Гайдамацький меч" || itemName == "Меч ополчения")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().militiaSword;
        if (itemName == "Big militia sword" || itemName == "Великий гайдамацький меч" || itemName == "Большой меч ополчения")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().bigMilitiaSword;
        if (itemName == "Soldier sword" || itemName == "Меч солдата" || itemName == "Меч солдата")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().soldierSword;
        if (itemName == "Big waraxe" || itemName == "Велика сокира" || itemName == "Большая секира")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().bigWaraxe;
        if (itemName == "Head note" || itemName == "Записка главы деревни" || itemName == "Записка голови села")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().headNote;
        if (itemName == "Meat" || itemName == "Мясо" || itemName == "М'ясо")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().meat;
        if (itemName == "Professional lockpick" || itemName == "Профессиональная отмычка" || itemName == "Професійна відмичка")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().professionalLockpick;
        if (itemName == "One-off lockpick" || itemName == "Одноразова відмичка" || itemName == "Одноразовая отмычка")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().oneLockpick;
        if (itemName == "Roasted meat" || itemName == "Жарене м'ясо" || itemName == "Жареное мясо")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().roastedMeat;
        if (itemName == "Wine" || itemName == "Вино" || itemName == "Жареное мясо")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().wineBottle;
        if (itemName == "Beer" || itemName == "Пиво")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().beerBottle;
        if (itemName == "Meat pie" || itemName == "Мясной пирог" || itemName == "М'ясний пиріг")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().meatPie;
        if (itemName == "Bread" || itemName == "Хлеб" || itemName == "Хліб")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().bread;
        if (itemName == "Egg" || itemName == "Яйцо" || itemName == "Яйце")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().egg;
        if (itemName == "Omelette" || itemName == "Омлет")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().omelette;
        if (itemName == "Slavia mushroom" || itemName == "Славійський гриб" || itemName == "Славийский гриб")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().slaviaMushroom;
        if (itemName == "Hell mushroom" || itemName == "Пекельний гриб" || itemName == "Адский гриб")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().hellMushroom;
        if (itemName == "Berserk potion" || itemName == "Зілля берсерка" || itemName == "Зелье берсерка")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().damagePotion;
        if (itemName == "Paladin potion" || itemName == "Зілля паладина" || itemName == "Зелье паладина")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().armorPotion;
        if (itemName == "Robber potion" || itemName == "Зілля крадія" || itemName == "Зелье вор")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().pickpocketingPotion;
        if (itemName == "Breaker potion" || itemName == "Зілля зломщика" || itemName == "Зелье взломщика")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().breakingPotion;
        if (itemName == "Warrior potion" || itemName == "Зілля воїна" || itemName == "Зелье воина")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().warriorPotion;
        if (itemName == "Archimage potion" || itemName == "Зілля архимага" || itemName == "Зелье архимага")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().archimagePotion;
        if (itemName == "Runner potion" || itemName == "Зілля бігуна" || itemName == "Зелье бегуна")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().runnerPotion;
        if (itemName == "Artelis mushroom" || itemName == "Гриб Артеліса" || itemName == "Гриб Артелиса")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().specialMushroom;
        if (itemName == "Royalists orders" || itemName == "Накази роялістів" || itemName == "Приказы роялистов")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().royalistsOrders;
        if (itemName == "Republican's orders" || itemName == "Накази республіканців" || itemName == "Приказы республиканцов")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().republicansOrders;
        if (itemName == "Artelit sword" || itemName == "Меч Артеліта" || itemName == "Меч Артелита")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().artelitSword;
        if (itemName == "Storm spell" || itemName == "Штормове закляття" || itemName == "Штурмовое заклинание")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().stormSpell;
        if (itemName == "Old book" || itemName == "Стара книга" || itemName == "Старая книжка")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().oldBook;
        if (itemName == "Dragon scroll" || itemName == "Драконний сувій" || itemName == "Драконний свиток")
            gameObject.GetComponent<Item>().item = GameObject.Find("GUIManager").GetComponent<Inventory>().dragonScroll;
        if (name!= "TimeObjectForInventory"&&name!= "TimeObjectForMoving")
        GameObject.Find("GameManager").GetComponent<GameManager>().listOfItemsOnScene.Add(gameObject);
    }
    private void OnCollisionEnter()
    {
        if (isLoading==false)
        {
            if (GetComponent<AudioSource>() != null)
            {
                if (GetComponent<AudioSource>().clip.name == "Body-Thud-1-www.fesliyanstudios.com")
                    GetComponent<AudioSource>().time = 0.65f;
                if (GetComponent<AudioSource>().enabled)
                    GetComponent<AudioSource>().Play();
            }
        }
    }
    private IEnumerator TurnSoundOn()
    {
        yield return new WaitForSeconds(0.3f);
        isLoading = false;
    }
}
