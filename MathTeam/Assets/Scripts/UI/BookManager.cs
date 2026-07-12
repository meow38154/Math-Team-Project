using GDH;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class BookManager : MonoSingleton<BookManager>
    {
        [SerializeField] private GameObject o;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private TextMeshProUGUI choice1;
        [SerializeField] private TextMeshProUGUI choice2;
        [SerializeField] private TextMeshProUGUI choice3;
        
        [SerializeField] private UnityEvent yes;
        [SerializeField] private UnityEvent no;
        
        public int bookNum { get; private set; }
        
        [SerializeField] private UnityEvent[] bookClear;

        private int _dab;

        private UnityEvent _clear;
        private UnityEvent _no;
    
        public void SetQuestion(string questionData, UnityEvent clear, UnityEvent no, AudioClip audio)
        {
            PlayerManager.Instance.PlayerMovement.NoControl = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            o.SetActive(true);
            
            string[] texts = questionData.Split('-');

            question.text = texts[0];
            choice1.text = texts[1];
            choice2.text = texts[2];
            choice3.text = texts[3];
            _dab = int.Parse(texts[4]);

            audioSource.PlayOneShot(audio);
            
            _clear = clear;
            _no = no;
        }

        public void Dab(int num)
        {
            if (num == _dab)
            {
                _clear?.Invoke();
                yes?.Invoke();
            }
            else
            {
                _no?.Invoke();
                no?.Invoke();
            }
            bookClear[bookNum]?.Invoke();
            bookNum++;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerManager.Instance.PlayerMovement.NoControl = false;
            o.SetActive(false);
        }
    }
}