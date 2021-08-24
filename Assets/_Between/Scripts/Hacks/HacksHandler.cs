using Between.Data;
using Between.Saving;
using Between.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Hacks
{
    public partial class HacksHandler : MonoBehaviour
    {
        [SerializeField] private GameSettings _hackSettings;
        [SerializeField] private GameSettings _middleSettings;

        private List<Hack> _hacks = new List<Hack>();

        private void Start()
        {
            for (int i = 0; i < 9; i++)
            {
                int number = i;
                _hacks.Add(new Hack(GetKeyByNumber(number), () => LoadCheckPoint(number)));
            }

            _hacks.Add(new Hack(KeyCode.I, UpgradePlayer));
        }

        private void Update()
        {
            foreach (Hack item in _hacks)
            {
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(item.Key))
                    item.Execute();
            }
        }

        private KeyCode GetKeyByNumber(int number) => (KeyCode)((int)KeyCode.Alpha0 + number);

        private void LoadCheckPoint(int number)
        {
            PlayerData playerData = new PlayerData(2, number, CreateFilledList(100));
            SaveSystem.Save(playerData);

            if (LevelManager.Instance == null)
                SceneChanger.ChangeScene(1, 2);
            else
                SceneChanger.ChangeScene(2, 2);
        }

        private void UpgradePlayer()
        {
            if (Player.Instance == null)
                return;

            if (Player.Instance.Controller.Immortal)
            {
                _middleSettings.InitSettings();
                Player.Instance.Controller.Immortal = false;

                GameObject hackShieldZone = GameObject.Find("HackShieldZone");
                hackShieldZone?.SetActive(false);
            }
            else
            {
                _hackSettings.InitSettings();
                Player.Instance.Controller.Immortal = true;

                GameObject hackShieldZone = GameObject.Find("HackShieldZone");
                hackShieldZone?.SetActive(true);
            }
        }

        private List<int> CreateFilledList(int count)
        {
            var list = new List<int>();

            for (int i = 0; i < count; i++)
            {
                var number = i;
                list.Add(number);
            }

            return list;
        }
    }
}