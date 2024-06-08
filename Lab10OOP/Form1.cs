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
        private CancellationTokenSource cts1; // ����� ���������� ��� �������� 1
        private CancellationTokenSource cts2; // ����� ���������� ��� �������� 2
        private CancellationTokenSource cts3; // ����� ���������� ��� �������� 3

        private Task task1; // ������ ��� �������� 1
        private Task task2; // ������ ��� �������� 2
        private Task task3; // ������ ��� �������� 3

        private RSAParameters _publicKey; // �������� ���� RSA
        private RSAParameters _privateKey; // ��������� ���� RSA

        public Form1()
        {
            InitializeComponent();

            // ����������� ������ RSA
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }

        // ����� 1: �������� ���� LUCIFER (�������������� ������������� AES)
        public byte[] EncryptBlock(byte[] plaintext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("samplekey1234567"); // 16 ����� ����� ��� ��������
                aesAlg.Mode = CipherMode.ECB; // ����� ECB
                aesAlg.Padding = PaddingMode.PKCS7; // ����� ���������� PKCS7

                // ��������� ��'���� ��� ����������
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // ���������� �����
                return encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
            }
        }

        // ����� ��� ������������� ��������� ����� LUCIFER (AES)
        public byte[] DecryptBlock(byte[] ciphertext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("samplekey1234567");
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                // ��������� ��'���� ��� �������������
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // ������������� �����
                return decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            }
        }

        // ����� 2: ���-������� N-Hash (�������������� ������������� SHA256)
        public byte[] NHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // ��������� ������� �����
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        // ����� 3: ����������/������������� RSA
        public byte[] EncryptRSA(string plaintext)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_publicKey);
                // ���������� ����� � ������������� RSA
                return rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);
            }
        }

        public string DecryptRSA(byte[] ciphertext)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_privateKey);
                // ������������� ����� � ������������� RSA
                return Encoding.UTF8.GetString(rsa.Decrypt(ciphertext, false));
            }
        }

        // ����������� ����� ��� ��������� ������������
        private async Task DrawRect(CancellationToken token)
        {
            Random rnd = new Random();
            Graphics g = panel1.CreateGraphics();
            while (!token.IsCancellationRequested)
            {
                // ��������� ���������� ����� ��� ����������
                byte[] data = Encoding.UTF8.GetBytes(rnd.Next().ToString());

                // ���������� �����
                byte[] encryptedData = EncryptBlock(data);

                // ����������� ������������ ����� � ���� (������������ ���-������� ��� ����� � ������� �������)
                byte[] hash = NHash(Convert.ToBase64String(encryptedData));
                Color color = Color.FromArgb(hash[0], hash[1], hash[2]);

                // ��������� ������������ � �������� ��������
                g.DrawRectangle(new Pen(color), 0, 0, rnd.Next(this.Width), rnd.Next(this.Height));
                await Task.Delay(40);
            }
        }

        // ����������� ����� ��� ��������� �����
        private async Task DrawEllipse(CancellationToken token)
        {
            Random rnd = new Random();
            Graphics g = panel2.CreateGraphics();
            while (!token.IsCancellationRequested)
            {
                // ��������� ���������� ����� ��� ���������
                string data = rnd.Next().ToString();

                // ��������� �����
                byte[] hash = NHash(data);

                // ����������� ���� � ����
                Color color = Color.FromArgb(hash[0], hash[1], hash[2]);

                // ��������� ����� � �������� ��������
                g.DrawEllipse(new Pen(color), 0, 0, rnd.Next(this.Width), rnd.Next(this.Height));
                await Task.Delay(40);
            }
        }

        // ����������� ����� ��� ��������� ���������� ����� � ���������� RSA
        private async Task GenerateRandomNumbers(CancellationToken token)
        {
            Random rnd = new Random();
            while (!token.IsCancellationRequested)
            {
                // ��������� ����������� �����
                string randomNumber = rnd.Next().ToString();

                // ���������� ����������� ����� �� ��������� RSA
                byte[] encryptedNumber = EncryptRSA(randomNumber);

                // ����������� ������������� ����� � base64 �����
                string encryptedString = Convert.ToBase64String(encryptedNumber);

                // ��������� ������������ ������������ ���������� ������
                richtextbox1.Invoke((MethodInvoker)delegate ()
                {
                    richtextbox1.Text += encryptedString + Environment.NewLine;
                });
                await Task.Delay(1);
            }
        }

        // �������� ��䳿 ������ ��� ������� ��������� ������������
        private void button1_Click(object sender, EventArgs e)
        {
            if (cts1 == null || cts1.IsCancellationRequested)
            {
                cts1 = new CancellationTokenSource();
                task1 = Task.Run(() => DrawRect(cts1.Token), cts1.Token);
            }
        }

        // �������� ��䳿 ������ ��� ������� ��������� �����
        private void button3_Click(object sender, EventArgs e)
        {
            if (cts2 == null || cts2.IsCancellationRequested)
            {
                cts2 = new CancellationTokenSource();
                task2 = Task.Run(() => DrawEllipse(cts2.Token), cts2.Token);
            }
        }

        // �������� ��䳿 ������ ��� ��������� ���������� ����� � ���������� RSA
        private void button5_Click(object sender, EventArgs e)
        {
            if (cts3 == null || cts3.IsCancellationRequested)
            {
                cts3 = new CancellationTokenSource();
                task3 = Task.Run(() => GenerateRandomNumbers(cts3.Token), cts3.Token);
            }
        }

        // �������� ��䳿 ������ ��� ���������� ��������� ������������
        private void button2_Click(object sender, EventArgs e)
        {
            cts1?.Cancel(); // ���������� �������� 1
        }

        // �������� ��䳿 ������ ��� ���������� ��������� �����
        private void button4_Click(object sender, EventArgs e)
        {
            cts2?.Cancel(); // ���������� �������� 2
        }

        // �������� ��䳿 ������ ��� ���������� ��������� ���������� ����� � ���������� RSA
        private void button6_Click(object sender, EventArgs e)
        {
            cts3?.Cancel(); // ���������� �������� 3
        }

        // �������� ��䳿 ������ ��� ���������� ��� �������
        private void button8_Click(object sender, EventArgs e)
        {
            cts1?.Cancel(); // ���������� �������� 1
            cts2?.Cancel(); // ���������� �������� 2
            cts3?.Cancel(); // ���������� �������� 3
        }

        // �������� ��䳿 ������ ��� ������� ��� �������
        private void button7_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e); // ������ ��������� ������������
            button3_Click(sender, e); // ������ ��������� �����
            button5_Click(sender, e); // ������ ��������� ���������� ����� � ���������� RSA
        }

        // �������� ��䳿 �������� ����� ��� ���������� ��� �������
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts1?.Cancel(); // ���������� �������� 1
            cts2?.Cancel(); // ���������� �������� 2
            cts3?.Cancel(); // ���������� �������� 3
        }
    }
}