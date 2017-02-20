using UnityEngine;
using UnityEngine.UI;


namespace Script.UIBar {
    public class Account : MonoBehaviour
    {
        private static int _accountBalance = 0;



        public int AccountBalance
        {
            get
            {
                var accountData = GetComponent<Text>().text;
                int.TryParse(accountData, out _accountBalance);
                return _accountBalance;

            }
            set
            {
                _accountBalance = value;
                GetComponent<Text>().text = _accountBalance.ToString();
            }

        }
        public Text TestAmount;

        public void ButtonPress()
        {
            Debug.Log(TestAmount.text + "Input Amount \n");
            var amount = int.Parse(TestAmount.text);
            AccountBalance = amount;
            Debug.Log(AccountBalance);

        }


    }
}
