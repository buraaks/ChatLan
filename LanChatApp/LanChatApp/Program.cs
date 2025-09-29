using System;
using System.Windows.Forms;

namespace LanChatApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyContext());
        }

        public class MyContext : ApplicationContext
        {
            private Form1 mainForm;
            private ChatForm? chatForm;

            public MyContext()
            {
                mainForm = new Form1();
                mainForm.FormSwitch += OpenChatForm;
                mainForm.Show();
            }

            private void OpenChatForm(bool isHost, string ipAddress, string nick)
            {
                chatForm = new ChatForm(isHost, ipAddress, nick);
                chatForm.FormClosed += (s, e) => { mainForm.Show(); };
                chatForm.Show();
                mainForm.Hide();
            }
        }
    }
}