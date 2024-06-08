using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab10OOP
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cts1; // Токен скасування для завдання 1
        private CancellationTokenSource cts2; // Токен скасування для завдання 2
        private CancellationTokenSource cts3; // Токен скасування для завдання 3

        private Task task1; // Задача для завдання 1
        private Task task2; // Задача для завдання 2
        private Task task3; // Задача для завдання 3

        private RSAParameters _publicKey; // Публічний ключ RSA
        private RSAParameters _privateKey; // Приватний ключ RSA

        public Form1()
        {
            InitializeComponent();

            // Ініціалізація ключів RSA
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }

        // Метод 1: блоковий шифр LUCIFER (демонстраційно використовуємо AES)
        public byte[] EncryptBlock(byte[] plaintext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("samplekey1234567"); // 16 байтів ключа для простоти
                aesAlg.Mode = CipherMode.ECB; // Режим ECB
                aesAlg.Padding = PaddingMode.PKCS7; // Схема доповнення PKCS7

                // Створення об'єкта для шифрування
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Шифрування даних
                return encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
            }
        }

        // Метод для розшифрування блокового шифру LUCIFER (AES)
        public byte[] DecryptBlock(byte[] ciphertext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("samplekey1234567");
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Створення об'єкта для розшифрування
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Розшифрування даних
                return decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            }
        }

        // Метод 2: хеш-функція N-Hash (демонстраційно використовуємо SHA256)
        public byte[] NHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Хешування вхідних даних
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        // Метод 3: шифрування/розшифрування RSA
        public byte[] EncryptRSA(string plaintext)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_publicKey);
                // Шифрування даних з використанням RSA
                return rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);
            }
        }

        public string DecryptRSA(byte[] ciphertext)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_privateKey);
                // Розшифрування даних з використанням RSA
                return Encoding.UTF8.GetString(rsa.Decrypt(ciphertext, false));
            }
        }

        // Асинхронний метод для малювання прямокутників
        private async Task DrawRect(CancellationToken token)
        {
            Random rnd = new Random();
            Graphics g = panel1.CreateGraphics();
            while (!token.IsCancellationRequested)
            {
                // Генерація випадкових даних для шифрування
                byte[] data = Encoding.UTF8.GetBytes(rnd.Next().ToString());

                // Шифрування даних
                byte[] encryptedData = EncryptBlock(data);

                // Конвертація зашифрованих даних у колір (використання хеш-функції для впису в діапазон кольорів)
                byte[] hash = NHash(Convert.ToBase64String(encryptedData));
                Color color = Color.FromArgb(hash[0], hash[1], hash[2]);

                // Малювання прямокутника з вибраним кольором
                g.DrawRectangle(new Pen(color), 0, 0, rnd.Next(this.Width), rnd.Next(this.Height));
                await Task.Delay(40);
            }
        }

        // Асинхронний метод для малювання еліпсів
        private async Task DrawEllipse(CancellationToken token)
        {
            Random rnd = new Random();
            Graphics g = panel2.CreateGraphics();
            while (!token.IsCancellationRequested)
            {
                // Генерація випадкових даних для хешування
                string data = rnd.Next().ToString();

                // Хешування даних
                byte[] hash = NHash(data);

                // Конвертація хешу у колір
                Color color = Color.FromArgb(hash[0], hash[1], hash[2]);

                // Малювання еліпса з вибраним кольором
                g.DrawEllipse(new Pen(color), 0, 0, rnd.Next(this.Width), rnd.Next(this.Height));
                await Task.Delay(40);
            }
        }

        // Асинхронний метод для генерації випадкових чисел і шифрування RSA
        private async Task GenerateRandomNumbers(CancellationToken token)
        {
            Random rnd = new Random();
            while (!token.IsCancellationRequested)
            {
                // Генерація випадкового числа
                string randomNumber = rnd.Next().ToString();

                // Шифрування випадкового числа за допомогою RSA
                byte[] encryptedNumber = EncryptRSA(randomNumber);

                // Конвертація зашифрованого числа в base64 рядок
                string encryptedString = Convert.ToBase64String(encryptedNumber);

                // Оновлення річтекстбоксу зашифрованим випадковим числом
                richtextbox1.Invoke((MethodInvoker)delegate ()
                {
                    richtextbox1.Text += encryptedString + Environment.NewLine;
                });
                await Task.Delay(1);
            }
        }

        // Обробник події кнопки для запуску малювання прямокутників
        private void button1_Click(object sender, EventArgs e)
        {
            if (cts1 == null || cts1.IsCancellationRequested)
            {
                cts1 = new CancellationTokenSource();
                task1 = Task.Run(() => DrawRect(cts1.Token), cts1.Token);
            }
        }

        // Обробник події кнопки для запуску малювання еліпсів
        private void button3_Click(object sender, EventArgs e)
        {
            if (cts2 == null || cts2.IsCancellationRequested)
            {
                cts2 = new CancellationTokenSource();
                task2 = Task.Run(() => DrawEllipse(cts2.Token), cts2.Token);
            }
        }

        // Обробник події кнопки для генерації випадкових чисел і шифрування RSA
        private void button5_Click(object sender, EventArgs e)
        {
            if (cts3 == null || cts3.IsCancellationRequested)
            {
                cts3 = new CancellationTokenSource();
                task3 = Task.Run(() => GenerateRandomNumbers(cts3.Token), cts3.Token);
            }
        }

        // Обробник події кнопки для скасування малювання прямокутників
        private void button2_Click(object sender, EventArgs e)
        {
            cts1?.Cancel(); // Скасування завдання 1
        }

        // Обробник події кнопки для скасування малювання еліпсів
        private void button4_Click(object sender, EventArgs e)
        {
            cts2?.Cancel(); // Скасування завдання 2
        }

        // Обробник події кнопки для скасування генерації випадкових чисел і шифрування RSA
        private void button6_Click(object sender, EventArgs e)
        {
            cts3?.Cancel(); // Скасування завдання 3
        }

        // Обробник події кнопки для скасування всіх завдань
        private void button8_Click(object sender, EventArgs e)
        {
            cts1?.Cancel(); // Скасування завдання 1
            cts2?.Cancel(); // Скасування завдання 2
            cts3?.Cancel(); // Скасування завдання 3
        }

        // Обробник події кнопки для запуску всіх завдань
        private void button7_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e); // Запуск малювання прямокутників
            button3_Click(sender, e); // Запуск малювання еліпсів
            button5_Click(sender, e); // Запуск генерації випадкових чисел і шифрування RSA
        }

        // Обробник події закриття форми для скасування всіх завдань
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts1?.Cancel(); // Скасування завдання 1
            cts2?.Cancel(); // Скасування завдання 2
            cts3?.Cancel(); // Скасування завдання 3
        }
    }
}