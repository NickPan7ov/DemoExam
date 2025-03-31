using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGameShop
{
    public static class std
    {
        private static int exitCalls = 0;

        public static DialogResult question(string msg) => MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static void appExit(FormClosingEventArgs e)
        {
            if (exitCalls > 0) return;
            exitCalls++;
            var answer = question("Вы действительно хотите выйти?");
            if (answer == DialogResult.No) { e.Cancel = true; exitCalls = 0; }
            if (answer == DialogResult.Yes) Application.Exit();
        }

    }
}
