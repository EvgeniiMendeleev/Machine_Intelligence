﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FramesKnowledges
{
    public partial class SlotAddSettings : Form
    {
        private List<string> valuesFromSettings = new List<string>();
        public SlotAddSettings()
        {
            InitializeComponent();
            ptrToInheritanceComboBox.SelectedIndex = 0;
            ptrToTypeComboBox.SelectedIndex = 0;
        }
        private void showError(string nameOfError, string textOfError)
        {
            MessageBox.Show(textOfError, nameOfError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private string getConnectedString(ref List<string> substrings)
        {
            string result = substrings.First<string>();
            for (int i = 1; i < substrings.Count; i++)
            {
                result += ' ' + substrings[i];
            }

            return result;
        }
        private string getProcessedInputString(string inputStr)
        {
            List<string> splitedInputStr = new List<string>(inputStr.Split(' '));
            while (splitedInputStr.IndexOf("") != -1) splitedInputStr.Remove("");

            return getConnectedString(ref splitedInputStr);
        }
        public List<string> getDatasFromForm()
        {
            return valuesFromSettings;
        }
        private void acceptAddSlot(object sender, EventArgs e)
        {
            if (slotNameText.Text.Length == 0)
            {
                showError("Ошибка создания слота!", "Не указано имя слота!");
                return;
            }

            string nameOfSlot = getProcessedInputString(slotNameText.Text);

            foreach (char ch in nameOfSlot)
            {
                if (ch != ' ' && !Char.IsLetter(ch))
                {
                    showError("Ошибка добавления слота!", "В названии слота присутствуют сторонние символы!");
                    return;
                }
            }

            valuesFromSettings.Add(nameOfSlot);
            valuesFromSettings.Add(ptrToInheritanceComboBox.SelectedItem.ToString());
            valuesFromSettings.Add(ptrToTypeComboBox.SelectedItem.ToString());

            string data = dataTextBox.Text.Length == 0 ? null : getProcessedInputString(dataTextBox.Text);

            switch(ptrToTypeComboBox.Text)
            {
                case "FRAME":
                    if (data == null)
                    {
                        valuesFromSettings.Add(data);
                        break;
                    }

                    foreach (char ch in data)
                    {
                        if (ch != ' ' && !Char.IsLetter(ch))
                        {
                            showError("Ошибка добавления слота!", "В значении присутствуют сторонние символы!");
                            valuesFromSettings.Clear();
                            return;
                        }
                    }
                    valuesFromSettings.Add(data);
                    break;
                case "BOOL":
                    if (data != "true" && data != "false" && data != null)
                    {
                        showError("Ошибка добавления слота!", "В случае BOOL значение должно быть true или false!");
                        valuesFromSettings.Clear();
                        return;
                    }
                    valuesFromSettings.Add(data);
                    break;
                case "TEXT":
                    valuesFromSettings.Add(data);
                    break;
            }

            this.DialogResult = DialogResult.Yes;
        }
        private void exitFromSettings(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
