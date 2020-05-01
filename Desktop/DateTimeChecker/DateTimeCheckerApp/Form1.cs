using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DateTimeCheckerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDay.Text = "";
            txtMonth.Text = "";
            txtYear.Text = "";
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            int day = getDateTime("Day", txtDay.Text, 31, 1);
            if (day == -1) return;
            int month = getDateTime("Month", txtMonth.Text, 12, 1);
            if (month == -1) return;
            int year = getDateTime("Year", txtYear.Text, 3000, 1000);
            if (year == -1) return;
            ShowStatusOfCheckDateTime(day, month, year, DateTimeCheckerUtils.Utils.IsValidDate(day, month, year));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit ?","Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                this.Close();
            }
        }
        private int getDateTime(String type, String dateTime, int max, int min)
        {
            int result = -1;
            try
            {
                result = int.Parse(dateTime);
                if (result > max || result < min)
                {
                    ShowMessageDateIsInOutOfRange(type);
                    return -1;
                }
            } catch (Exception ex)
            {
                ShowMessageDateIsIncorrectFormat(type);
                return -1;
            }
            return result;
        }
        private void ShowMessageDateIsIncorrectFormat(String typeError)
        {
            MessageBox.Show("Input data of " + typeError + " is incorrect format!", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }
        private void ShowMessageDateIsInOutOfRange(String typeError)
        {
            MessageBox.Show("Input data of " + typeError + " is out of range!", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        private void ShowStatusOfCheckDateTime(int day, int month, int year, bool isError )
        {
            String Str = day + "/" + month + "/" + year;
            if (!isError)
            {
                
                MessageBox.Show("Input data of " + Str + " is NOT correct date time!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Input data of " + Str + " is correct date time!", "Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
        }
    }
}
